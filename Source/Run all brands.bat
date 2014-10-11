@echo off

echo Building...
set MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe
%MSBUILD% WpfBranding.sln /t:Rebuild /p:Configuration=Release /m

echo.
echo Copying unbranded binaries...
xcopy WpfBranding\bin\Release\*.exe bin\Unbranded\ /Y
xcopy WpfBranding\bin\Release\*.dll bin\Unbranded\ /Y
xcopy Brand.None\bin\Release\*.dll bin\Unbranded\ /Y

echo.
echo Copying CompanyA binaries...
xcopy WpfBranding\bin\Release\*.exe bin\CompanyA\ /Y
xcopy WpfBranding\bin\Release\*.dll bin\CompanyA\ /Y
xcopy Brand.CompanyA\bin\Release\*.dll bin\CompanyA\ /Y

echo.
echo Copying CompanyB binaries...
xcopy WpfBranding\bin\Release\*.exe bin\CompanyB\ /Y
xcopy WpfBranding\bin\Release\*.dll bin\CompanyB\ /Y
xcopy Brand.CompanyB\bin\Release\*.dll bin\CompanyB\ /Y

echo.
echo Running calculators...
start bin\Unbranded\WpfBranding.exe
start bin\CompanyA\WpfBranding.exe
start bin\CompanyB\WpfBranding.exe