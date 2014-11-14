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
        $source = "bin\$Brand\Brand.dll"

        $expression = ".\Build\CopyAssemblyInformation\bin\Release\CopyAssemblyInformation.exe /source=$source /target=bin\$Brand\WpfBranding.exe"
        Invoke-Expression $expression

        $expression = ".\Build\CopyAssemblyInformation\bin\Release\CopyAssemblyInformation.exe /source=$source /target=bin\$Brand\Brand.Fallback.dll"
        Invoke-Expression $expression
    }
}