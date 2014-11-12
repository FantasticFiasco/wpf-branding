Function Copy-AssemblyInformation
{
<#
    .SYNOPSIS
    Copy assembly information from one assembly to another.

    .PARAMETER Brand
    The name of the brand to copy assembly information from.
#>
    [CmdletBinding()]
    param
	(
        [parameter(Mandatory=$true)]
        $Brand
    )
    BEGIN { }
	END { }
	PROCESS
	{
        $expression = ".\CopyAssemblyInformation\bin\Release\CopyAssemblyInformation.exe /source=bin\$Brand\Brand.dll /target=bin\$Brand\WpfBranding.exe"
        Invoke-Expression $expression
    }
}