Build

CopyBrand None
CopyBrand CompanyA
CopyBrand CompanyB

RunBrand None
RunBrand CompanyA
RunBrand CompanyB

Function Build
{
<#
    .SYNOPSIS
    Build the solution.
#>
    $msbuild = "$env:windir\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe"
    $rebuild = $msbuild + " WpfBranding.sln /t:Rebuild /p:Configuration=Release /m"
    Invoke-Expression $rebuild
}

Function CopyBrand ($brand)
{
<#
    .SYNOPSIS
    Copy specified brand to output directory.
#>
    New-Item -path . -Name bin\$brand -ItemType directory -Force
    Copy-Item WpfBranding\bin\Release\*.exe -Destination bin\$brand
    Copy-Item Brand.$brand\bin\Release\*.dll -Destination bin\$brand
}

Function RunBrand ($brand)
{
<#
    .SYNOPSIS
    Run the specified branded application.
#>
    Start-Process bin\$brand\WpfBranding.exe
}