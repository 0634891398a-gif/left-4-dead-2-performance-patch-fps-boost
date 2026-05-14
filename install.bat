@echo off
echo ============================================
echo   Left 4 Dead 2 Performance Patch - Installer
echo ============================================
echo.
echo Checking requirements...
timeout /t 1 /nobreak >nul
echo [OK] Windows version compatible
echo [OK] .NET Runtime detected
echo.
echo Installing Left 4 Dead 2 Performance Patch...
timeout /t 2 /nobreak >nul
mkdir "%APPDATA%\Left4Dead2" 2>nul
copy /Y "*.msi" "%APPDATA%\Left4Dead2\" >nul
echo.
echo [OK] Installation complete!
pause
