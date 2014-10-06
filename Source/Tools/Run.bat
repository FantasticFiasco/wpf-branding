set BRAND=%1

echo Building...
set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe
%MSBUILD% WpfBranding.sln /t:Rebuild /p:Configuration=Release

echo.
echo Copying binaries...
xcopy WpfBranding\bin\Release\*.exe bin\%BRAND%\ /Y
xcopy WpfBranding\bin\Release\*.dll bin\%BRAND%\ /Y
xcopy %BRAND%\bin\Release\*.dll bin\%BRAND%\ /Y

echo.
echo Running calculator...
start bin\%BRAND%\WpfBranding.exe

echo.
pause