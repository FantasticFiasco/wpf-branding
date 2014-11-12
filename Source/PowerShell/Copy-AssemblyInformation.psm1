Function Copy-AssemblyInfo
{
<#
    .SYNOPSIS
    Copy assembly information from one assembly to another.

    .PARAMETER Source
    The file name of the source assembly.

    .PARAMETER Target
    The file name of the target assembly.

    .PARAMETER ToolPath
    The file name of CopyAssemblyInformation.exe console application.
#>
    [CmdletBinding()]
    param
	(
        [parameter(Mandatory=$true)]
        $Source,

        [parameter(Mandatory=$true)]
        $Target,

        [parameter(Mandatory=$true)]
        $ToolPath
    )
    BEGIN { }
	END { }
	PROCESS
	{
        $expression = "$ToolPath /source=$Source /target=$Target"
        Invoke-Expression $expression
    }
}