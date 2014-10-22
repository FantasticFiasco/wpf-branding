Build

CopyBrand None
CopyBrand CompanyA
CopyBrand CompanyB

RunBrand None
RunBrand CompanyA
RunBrand CompanyB

# Build the solution
Function Build
{
    $msbuild = "$env:windir\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe"
    $rebuild = $msbuild + " WpfBranding.sln /t:Rebuild /p:Configuration=Release /m"
    Invoke-Expression $rebuild
}

# Copy specified brand to output directory
Function CopyBrand ($brand)
{
    New-Item -path . -Name bin\$brand -ItemType directory -Force
    Copy-Item WpfBranding\bin\Release\*.exe -Destination bin\$brand
    Copy-Item Brand.$brand\bin\Release\*.dll -Destination bin\$brand
}

# Runs the specified branded application
Function RunBrand ($brand)
{
    Start-Process bin\$brand\WpfBranding.exe
}