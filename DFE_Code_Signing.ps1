
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

write-host "Cert pwd has a value : $env:CodeSignPwdDfE"; 
write-host "Cert var has a value : $env:CodeSignCertificatePFX"; 


if ($null-eq$env:CERTIFICATE)
{   writre-host " Certificate Error"; }
else
{
    write-host "Cert pwd has a value : $env:CERTIFICATEPWD"; 
    write-host "Cert var has a value : $env:CERTIFICATE"; 

    $PrivateCertKVBytes = [System.Convert]::FromBase64String($env:CERTIFICATE)
    
    write-host "a"; 

    $certObject = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new("$PrivateCertKVBytes","$CertificatePwd");
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