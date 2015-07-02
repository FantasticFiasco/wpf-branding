Function Copy-Brand
{
<#
    .SYNOPSIS
    Copy specified brand to output directory.

    .PARAMETER Brand
    The name of the brand to copy.
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
        New-Item -path . -Name bin\$Brand -ItemType directory -Force
        Copy-Item WpfBranding\bin\Release\*.exe -Destination bin\$Brand
        Copy-Item Brand.$Brand\bin\Release\*.dll -Destination bin\$Brand
    }
}