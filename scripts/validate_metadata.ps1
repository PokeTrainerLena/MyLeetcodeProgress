# Validate metadata headers in .cs files against problems.yml
# Usage: .\scripts\validate_metadata.ps1

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path -ErrorAction SilentlyContinue
if (-not $repoRoot) { $repoRoot = Get-Location }
$repoRoot = Resolve-Path $repoRoot

$yamlPath = Join-Path $repoRoot 'problems.yml'
if (-not (Test-Path $yamlPath)) {
    Write-Error "Central index 'problems.yml' not found at $yamlPath"
    exit 2
}

$yaml = Get-Content $yamlPath -Raw
# Simple YAML splitter for small, predictable file. Each entry starts with "- id:".
$entries = @()
$blocks = $yaml -split "(?m)^\-\s+id:" | Where-Object { $_.Trim() -ne "" }
foreach ($b in $blocks) {
    $block = "id:" + $b.TrimStart()
    $obj = [ordered]@{}
    if ($block -match "id:\s*(\d+)") { $obj.id = $matches[1] }
    if ($block -match "filename:\s*(\S+)") { $obj.filename = $matches[1] }
    if ($block -match "title:\s*\"?(.+?)\"?\s*(\r?\n|$)") { $obj.title = $matches[1].Trim() }
    if ($block -match "url:\s*\"?(.+?)\"?\s*(\r?\n|$)") { $obj.url = $matches[1].Trim() }
    if ($block -match "difficulty:\s*(\S+)") { $obj.difficulty = $matches[1] }
    if ($block -match "tags:\s*\[(.+?)\]") { $tags = $matches[1].Split(',') | ForEach-Object { $_.Trim() }; $obj.tags = $tags }
    if ($block -match "date_solved:\s*(\S+)") { $obj.date_solved = $matches[1] }
    $entries += (New-Object psobject -Property $obj)
}

$errors = @()
# Check each entry has a file and matching header
foreach ($e in $entries) {
    $filePath = Join-Path $repoRoot $e.filename
    if (-not (Test-Path $filePath)) {
        $errors += "Missing file for index entry: $($e.filename)"
        continue
    }
    $head = Get-Content $filePath -TotalCount 30 -Raw
    # Look for minimal header lines
    if ($head -notmatch "//\s*LeetCode\s+$($e.id)\s+\u2014\s*(.+)") {
        $errors += "Header title line missing or mismatched in $($e.filename)"
    }
    if ($head -notmatch "//\s*URL:\s*(.+)") {
        $errors += "Header URL line missing in $($e.filename)"
    }
    if ($head -notmatch "//\s*Difficulty:\s*(.+)") {
        $errors += "Header Difficulty line missing in $($e.filename)"
    }
    if ($head -notmatch "//\s*Tags:\s*(.+)") {
        $errors += "Header Tags line missing in $($e.filename)"
    }
    if ($head -notmatch "//\s*Solved:\s*(\d{4}-\d{2}-\d{2})") {
        $errors += "Header Solved date missing or malformed in $($e.filename)"
    }
}

# Check for .cs files that are missing index entries
$csFiles = Get-ChildItem -Path $repoRoot -Filter *.cs | Select-Object -ExpandProperty Name
$indexedFiles = $entries | Select-Object -ExpandProperty filename
foreach ($f in $csFiles) {
    if ($indexedFiles -notcontains $f) {
        $errors += "File not indexed in problems.yml: $f"
    }
}

if ($errors.Count -gt 0) {
    Write-Host "Validation completed: issues found:`n" -ForegroundColor Yellow
    $errors | ForEach-Object { Write-Host " - $_" }
    exit 1
} else {
    Write-Host "Validation completed: all headers and index entries are consistent." -ForegroundColor Green
    exit 0
}
