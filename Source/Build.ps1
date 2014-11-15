Import-Module .\Build\PowerShell\Copy-Brand.psm1
Import-Module .\Build\PowerShell\Invoke-MsBuild.psm1
Import-Module .\Build\PowerShell\Invoke-BrandFiles.psm1

Invoke-MsBuild -SolutionPath "WpfBranding.sln" -MSBuildPath "$env:windir\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe" -Parameters "/t:Rebuild /p:Configuration=Release /m"

Copy-Brand "None"
Copy-Brand "CompanyA"
Copy-Brand "CompanyB"

Invoke-BrandFiles "None"
Invoke-BrandFiles "CompanyA"
Invoke-BrandFiles "CompanyB"