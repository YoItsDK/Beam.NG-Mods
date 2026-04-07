@echo off
setlocal

set "SCRIPT_DIR=%~dp0"
set "PRESET=%~1"
set "PORT=%~2"
set "SHAPES_DIR=%~3"
set "SEARCH=%~4"
set "MODEL_CONTAINS=%~5"

if "%PRESET%"=="" set "PRESET=default"
if "%PORT%"=="" set "PORT=8765"

set "CMD=powershell -NoProfile -ExecutionPolicy Bypass -File "%SCRIPT_DIR%launch-asset-preview-web.ps1" -Preset "%PRESET%" -Port %PORT%"
if defined SHAPES_DIR set "CMD=%CMD% -ShapesDir "%SHAPES_DIR%""
if defined SEARCH set "CMD=%CMD% -Search "%SEARCH%""
if defined MODEL_CONTAINS set "CMD=%CMD% -ModelContains "%MODEL_CONTAINS%""

%CMD%