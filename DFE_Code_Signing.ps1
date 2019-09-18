﻿
#Login-AzureRmAccount
#Connect-Azure
#Get-AzureRmSubscription | Out-GridView -PassThru | Set-AzureRmContext
#Get-AzureRmContext

#Requires -Version 3.0
Param(

    #[Parameter(Mandatory=$false)] [string]  $KeyValutName ='',
    #[Parameter(Mandatory=$false)] [string]  $CertificateName ='',
    
    [Parameter(Mandatory=$true)]  [string]  $StartFolder,
    [Parameter(Mandatory=$false)] [string]  $FileFilter = "ESFA.DC.*",
    [Parameter(Mandatory=$false)] [string]  $TimestampServer = "http://timestamp.globalsign.com/scripts/timestamp.dll"
)

### Powershell Script to Sign DfE applicatuons in-line during Automated Build on VSTS/Azure ###
###
###

#$StartFolder,
#$FileFilter = "ESFA.DC.*",
#$TimestampServer = "http://timestamp.globalsign.com/scripts/timestamp.dll"

#$AzureKeyVaultSecret=Get-AzureKeyVaultSecret -VaultName $KeyValutName.tolower().Replace(".vault.azure.net","") -Name $CertificateName -ErrorAction SilentlyContinue

write-output "Certificate pwd has a value : $env:CODESIGNPWDDFE"; 
write-output "Certificate has a value : $env:CODESIGNCERTIFICATEPFX"; 

write-output "Certificate pwdEnv has a value : $env:CODESIGNPASSWORD"; 
write-output "Certificate CERT has a value : $env:CODESIGNCERT"; 

if ($null-eq$env:CODESIGNCERT)
{   write-output " Certificate Error"; }
else
{
    #write-host "Cert pwd has a value : $env:CERTIFICATEPWD"; 
    #write-host "Cert var has a value : $env:CERTIFICATE"; 
    write-host "Cert tyep : $(($env:CODESIGNCER).GetType())";

    $PrivateCertKVBytes = [System.Convert]::FromBase64String($env:CODESIGNCERT)
    
    write-host "a : $PrivateCertKVBytes"; 

    #$certObject = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new("$PrivateCertKVBytes","$env:CODESIGNPASSWORD");
    $certObject = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new("$PrivateCertKVBytes",$null,"Exportable, PersistKeySet")
    write-host "B"; 

    if ($null-eq$certObject)
    { write-host "Cert Not Found"; }
    else
    {
        write-host "C"; 
        Write-Host "Cert Thumbprint : $($certObject.Thumbprint.ToString())"

        $exePath = "$($StartFolder)\$($FileFilter).exe"
        $dllPath = "$($StartFolder)\$($FileFilter).dll"

        Write-Debug "codeSign - EXEs"
        Set-AuthenticodeSignature -FilePath $exePath -Certificate $certObject -TimestampServer $TimestampServer -force; # | SELECT Path | Split-Path -Leaf -Resolve

        Write-Debug "codeSign - Dlls"
        $DllLost = Set-AuthenticodeSignature -FilePath $dllPath -Certificate $certObject -TimestampServer $TimestampServer -force;

        Write-Debug "Done"
    }
}

###  ###
### END ###