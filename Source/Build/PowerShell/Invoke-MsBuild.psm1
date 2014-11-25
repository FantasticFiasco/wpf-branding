Function Invoke-MsBuild
{
<#
    .SYNOPSIS
    Builds specified solution using MSBuild.

    .PARAMETER SolutionPath
    The path of the Visual Studio solution or project to build (e.g. a .sln or .csproj file).

    .PARAMETER MSBuildPath
    The path to MsBuild.exe

    .PARAMETER Parameters
    Additional parameters to pass to the MsBuild command-line tool. This can be any valid MsBuild
    command-line parameters except for the path of the solution/project to build.
#>
    [CmdletBinding()]
    param
	(
        [parameter(Mandatory=$true)]
        [string] $SolutionPath,

        [parameter(Mandatory=$true)]
        [string] $MSBuildPath,

        [parameter(Mandatory=$false)]
        [string] $Parameters
    )

    BEGIN { }
	END { }
	PROCESS
	{
        $expression = "$MSBuildPath $SolutionPath $Parameters"
        Invoke-Expression $expression
    }    
}