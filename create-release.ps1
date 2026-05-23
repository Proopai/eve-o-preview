# Builds the Windows release zip and creates a GitHub release (requires: gh auth login).
# Usage: .\create-release.ps1
# Optional: .\create-release.ps1 -Tag v8.0.2.0

param(
    [string]$Tag = "v8.0.2.0"
)

$ErrorActionPreference = "Stop"
$RepoRoot = $PSScriptRoot
Set-Location $RepoRoot

$Version = $Tag.TrimStart("v")
$StagingDir = Join-Path $RepoRoot "release-staging\EVE-F-Preview-$Tag-Windows"
$ZipPath = Join-Path $RepoRoot "release-staging\Release-$Tag-Windows.zip"
$NotesFile = Join-Path $RepoRoot ".github\RELEASE_$Tag.md"

if (-not (Test-Path $NotesFile)) {
    $NotesFile = Join-Path $RepoRoot ".github\RELEASE_v8.0.2.0.md"
}

Write-Host "Publishing EVE-F-Preview $Version..."
dotnet publish "src\Eve-O-Preview\Eve-O-Preview.csproj" -c Release -r win-x64 `
    --self-contained false `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:AssemblyVersion=$Version `
    -p:FileVersion=$Version `
    -o $StagingDir

Write-Host "Creating zip..."
if (Test-Path $ZipPath) { Remove-Item $ZipPath -Force }
Compress-Archive -Path (Join-Path $StagingDir "*") -DestinationPath $ZipPath -Force

Write-Host "Creating GitHub release $Tag..."
gh release create $Tag --target main --title "EVE-F-Preview $Version" --notes-file $NotesFile $ZipPath

Write-Host "Done: https://github.com/Chris-Matthewson/eve-f-preview/releases/tag/$Tag"
