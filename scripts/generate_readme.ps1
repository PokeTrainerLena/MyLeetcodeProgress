param(
    [string]$ProblemsFile = "problems.yml",
    [string]$ReadmeFile = "README.md",
    [switch]$Force
)

# Determine repo root (script lives in scripts/)
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RepoRoot = Resolve-Path (Join-Path $ScriptDir "..")
Set-Location $RepoRoot

$problemsPath = Join-Path $RepoRoot $ProblemsFile
if (-not (Test-Path $problemsPath)) {
    Write-Error "Problems file not found: $problemsPath"
    exit 2
}

$content = Get-Content -Raw -Path $problemsPath

# Try ConvertFrom-Yaml if available
$problems = $null
if (Get-Command ConvertFrom-Yaml -ErrorAction SilentlyContinue) {
    try {
        $problems = $content | ConvertFrom-Yaml
    } catch {
        Write-Warning "ConvertFrom-Yaml failed, falling back to simple parser.";
        $problems = $null
    }
}

if (-not $problems) {
    # Fallback line-based parser for simple YAML list
    $linesRaw = $content -split "`r?`n"
    $list = @()
    $current = $null
    foreach ($line in $linesRaw) {
        $trim = $line.Trim()
        if ($trim -match '^-\s*id:\s*(\d+)') {
            if ($current) { $list += ([PSCustomObject]$current) }
            $current = @{}
            $current.id = [int]$Matches[1]
            continue
        }
        if (-not $current) { continue }
        if ($trim -eq '') { continue }
        if ($trim -match '^(\w+):\s*(.+)$') {
            $key = $Matches[1]
            $val = $Matches[2]
            if ($val -match '^\[(.*)\]$') {
                $items = $Matches[1].Split(',') | ForEach-Object { $_.Trim().Trim('"').Trim("'") }
                $current[$key] = $items
            } else {
                $current[$key] = $val.Trim('"').Trim("'")
            }
        }
    }
    if ($current) { $list += ([PSCustomObject]$current) }
    $problems = $list
}

$objs = foreach ($p in $problems) {
    $title = if ($p.title) { $p.title } else { ($p.filename -replace '\.cs$','') }
    $url = if ($p.url) { $p.url } else { '' }
    $difficulty = if ($p.difficulty) { $p.difficulty } else { '' }
    $tags = if ($p.tags) { ($p.tags -as [array]) -join ', ' } else { '' }
    $filename = if ($p.filename) { $p.filename } else { '' }
    $dateSolved = if ($p.date_solved) { $p.date_solved } else { '' }
    [PSCustomObject]@{
        id = [int]$p.id
        title = $title
        url = $url
        difficulty = $difficulty
        tags = $tags
        filename = $filename
        date_solved = $dateSolved
    }
}
$objs = $objs | Sort-Object id

$lines = @()
$lines += '# LeetCode Solutions'
$lines += ''
$lines += 'This README is generated from `problems.yml`.'
$lines += ''
$lines += '## Indexed problems'
$lines += ''
$lines += '| ID | Title | Difficulty | Tags | File | Date Solved |'
$lines += '|---:|---|---|---|---|---|'
foreach ($p in $objs) {
    $titleCell = if ($p.url) { "[$($p.title)]($($p.url))" } else { $p.title }
    $fileCell  = if ($p.filename) { "[$($p.filename)]($($p.filename))" } else { '' }
    $lines += "| $($p.id) | $titleCell | $($p.difficulty) | $($p.tags) | $fileCell | $($p.date_solved) |"
}

$lines += ''
$lines += '## How to run tests and validator'
$lines += ''
$lines += 'Run tests:'
$lines += '```powershell'
$lines += 'dotnet test'
$lines += '```'
$lines += ''
$lines += 'Run metadata validator:'
$lines += '```powershell'
$lines += '.\\scripts\\validate_metadata.cmd'
$lines += '```'
$lines += ''
$lines += 'Regenerate README:'
$lines += '```powershell'
$lines += 'powershell -NoProfile -ExecutionPolicy Bypass -File .\\scripts\\generate_readme.ps1'
$lines += '```'
$lines += ''
$lines += "Generated on $(Get-Date -Format 'yyyy-MM-dd')"

$readmePath = Join-Path $RepoRoot $ReadmeFile
if ((Test-Path $readmePath) -and -not $Force) {
    Copy-Item $readmePath "$readmePath.bak" -Force
}
$lines -join "`n" | Set-Content -Path $readmePath -Encoding UTF8
Write-Host "Generated README at $readmePath"
