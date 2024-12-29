@echo off
setlocal EnableDelayedExpansion

set "DEST=C:\Program Files (x86)\Steam\steamapps\common\MiSide"

echo Remove BepInEx files from %DEST%...

rmdir /s /q "%DEST%\BepInEx"
rmdir /s /q "%DEST%\dotnet"
del /q "%DEST%\.doorstop_version"
del /q "%DEST%\changelog.txt"
del /q "%DEST%\doorstop_config.ini"
del /q "%DEST%\winhttp.dll"

echo BepInEx files removed successfully!
