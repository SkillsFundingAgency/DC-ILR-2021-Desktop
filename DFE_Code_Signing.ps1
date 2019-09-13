### Powershell Script to Sign DfE applicatuons in-line during Automated Build on VSTS/Azure ###
###
###


#Requires -Version 3.0
Param(

    [Parameter(Mandatory=$false)] [string]  $StartFolder = "$env:build_artifactstagingdirectory\DesktopApplication",
    [Parameter(Mandatory=$false)] [string]  $FileFilter = "ESFA.DC.*",

    [Parameter(Mandatory=$false)] [string]  $CodeSignPfxCertFileLocation = "C:\Users\BuildAdmin\Desktop\Codesign\Department-for-Education-CodeSigningCert-20190823.pfx",
    [Parameter(Mandatory=$false)] [string]  $CodeSignPwdDfE = "$($env:CODESIGNPWDDFE)",

    [Parameter(Mandatory=$false)] [string]  $TimestampServer = "http://timestamp.globalsign.com/scripts/timestamp.dll"

)

$cert = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new("$($CodeSignPfxCertFileLocation)","$($CodeSignPwdDfE)");
Write-Debug "Cert Thumbprint : $($Cert.Thumbprint)"

$exePath = Join-Path $StartFolde -ChildPath "DesktopApplication\$($FileFilter).exe" -Resolve
$dllPath = Join-Path $StartFolde -ChildPath "DesktopApplication\$($FileFilter).dll" -Resolve

Write-Debug "#####################################################################################"
Write-Debug "List of exe found"
Write-Debug $exePath
Write-Debug "-------------------------------------------------------------------------------------"

Write-Debug "List of Dlls found"
Write-Debug $exePath
Write-Debug "#####################################################################################"


Set-AuthenticodeSignature -FilePath $exePath -Certificate $cert -TimestampServer "$($TimestampServer)" -force;

Set-AuthenticodeSignature -FilePath $dllPath -Certificate $cert -TimestampServer "$($TimestampServer)" -force;

Write-Debug "Done"

<#
#$CodeSignPwdDfE = $($env:CODESIGNPWDDFE)

#$filePath1 =   [string]$StartFolde+'\DesktopApplication\*.exe' ;
#$filePath2 =   [string]$env:build_artifactstagingdirectory+'\DesktopApplication\*.dll' ;


$cert = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new("C:\Users\BuildAdmin\Desktop\Codesign\Department-for-Education-CodeSigningCert-20190823.pfx","$CodeSignPwdDfE");

echo "$env:build_artifactstagingdirectory";

Set-AuthenticodeSignature -FilePath $filePath1 -Certificate $cert -TimestampServer http://timestamp.globalsign.com/scripts/timestamp.dll -force;

Set-AuthenticodeSignature -FilePath $filePath2 -Certificate $cert -TimestampServer http://timestamp.globalsign.com/scripts/timestamp.dll -force;

#>

###  ###
### END ###