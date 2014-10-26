Import-Module .\PowerShell\Copy-Brand.psm1
Import-Module .\PowerShell\Invoke-MsBuild.psm1

Invoke-MsBuild -SolutionPath "WpfBranding.sln" -MSBuildPath "$env:windir\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe" -Parameters "/t:Rebuild /p:Configuration=Release /m"

Copy-Brand None
Copy-Brand CompanyA
Copy-Brand CompanyB