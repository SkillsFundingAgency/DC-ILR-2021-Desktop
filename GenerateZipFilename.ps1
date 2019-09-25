
param (
        [Parameter(Mandatory=$true)][string]$BuildNumber,
	    [Parameter(Mandatory=$true)][string]$Environment,
	    [Parameter(Mandatory=$false)][int]$BuildPartToUse=4
      )


$BuildNumberParts = $BuildNumber.Split('.')
if ($BuildNumberParts.Length-ne$BuildPartToUse)
{
   Throw "Build Number does not have the right number of Part. MUST be $($BuildPartToUse) parts: 0.0.0.0"
}

switch ($Environment.ToUpper()) 
{		"PP"         {$BuildPartToUse = 3; break}
		"PD"         {$BuildPartToUse = 3; break}	

		default      {$BuildPartToUse = 4; break}
}

$val=0;
$NewBuildNumber="";
while($val -ne $BuildPartToUse)
{
    if ($val-gt0)
    { $NewBuildNumber = "$($NewBuildNumber)."}

    $NewBuildNumber = "$($NewBuildNumber)$($BuildNumberParts[$val])"
    $val++
}

Write-Host "##vso[task.setvariable variable=ZipFileName]$($NewBuildNumber)";


