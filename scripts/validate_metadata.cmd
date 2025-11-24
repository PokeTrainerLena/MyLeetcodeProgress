@echo off
REM Wrapper to run the PowerShell validator with ExecutionPolicy bypass so local policy doesn't block it.
REM Usage: scripts\validate_metadata.cmd

SET scriptPath=%~dp0validate_metadata.ps1
powershell -NoProfile -ExecutionPolicy Bypass -File "%scriptPath%" %*
EXIT /B %ERRORLEVEL%
