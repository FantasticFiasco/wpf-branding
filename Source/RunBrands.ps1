# Build
$msbuild = "$env:windir\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe"
$rebuild = $msbuild + " WpfBranding.sln /t:Rebuild /p:Configuration=Release /m"
Invoke-Expression $rebuild

# Copy unbranded binaries
New-Item -path . -Name bin\Unbranded -ItemType directory -Force
Copy-Item WpfBranding\bin\Release\*.exe -Destination bin\Unbranded
Copy-Item Brand.None\bin\Release\*.dll -Destination bin\Unbranded

# Copy CompanyA binaries
New-Item -path . -Name bin\CompanyA -ItemType directory -Force
Copy-Item WpfBranding\bin\Release\*.exe -Destination bin\CompanyA
Copy-Item Brand.CompanyA\bin\Release\*.dll -Destination bin\CompanyA

# Copy CompanyB binaries
New-Item -path . -Name bin\CompanyB -ItemType directory -Force
Copy-Item WpfBranding\bin\Release\*.exe -Destination bin\CompanyB
Copy-Item Brand.CompanyB\bin\Release\*.dll -Destination bin\CompanyB

# Start all brands
Start-Process bin\Unbranded\WpfBranding.exe
Start-Process bin\CompanyA\WpfBranding.exe
Start-Process bin\CompanyB\WpfBranding.exe