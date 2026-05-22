@echo off
setlocal EnableExtensions

rem ============================================================================
rem  Configuration — edit DEPLOY_DIR to change where EVE-O-Preview.exe is copied
rem ============================================================================
set "DEPLOY_DIR=C:\Eve"

rem ============================================================================
rem  Build and deploy (paths are relative to this repo root)
rem ============================================================================
set "REPO_ROOT=%~dp0"
cd /d "%REPO_ROOT%"

set "PUBLISH_EXE=bin\net8.0-windows8.0\win-x64\publish\EVE-O-Preview.exe"

echo Building solution...
dotnet build "src\EVE-O-Preview.sln" -c Release --no-incremental
if errorlevel 1 goto :failed

echo Publishing single-file executable...
dotnet publish "src\Eve-O-Preview\Eve-O-Preview.csproj" -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
if errorlevel 1 goto :failed

if not exist "%PUBLISH_EXE%" (
    echo ERROR: Published exe not found: %REPO_ROOT%%PUBLISH_EXE%
    goto :failed
)

if not exist "%DEPLOY_DIR%\" mkdir "%DEPLOY_DIR%"

set "DEPLOY_EXE=%DEPLOY_DIR%\EVE-O-Preview.exe"

echo Stopping EVE-O-Preview...
taskkill /IM EVE-O-Preview.exe /F >nul 2>&1
timeout /t 1 /nobreak >nul

echo Copying to %DEPLOY_DIR%...
copy /Y "%PUBLISH_EXE%" "%DEPLOY_EXE%"
if errorlevel 1 goto :failed

echo Starting EVE-O-Preview...
rem /D sets working directory so EVE-O-Preview.json next to the exe is found
start "" /D "%DEPLOY_DIR%" "%DEPLOY_EXE%"

echo.
echo Deploy complete: %DEPLOY_EXE%
exit /b 0

:failed
echo.
echo Deploy failed.
exit /b 1
