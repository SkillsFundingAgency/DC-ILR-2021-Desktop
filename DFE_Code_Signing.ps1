
#Login-AzureRmAccount
#Connect-Azure
#Get-AzureRmSubscription | Out-GridView -PassThru | Set-AzureRmContext
#Get-AzureRmContext

#Requires -Version 3.0
Param(

    
    [Parameter(Mandatory=$true)]  [string]  $Certificate,
    [Parameter(Mandatory=$true)]  [string]  $CertificatePath,
    [Parameter(Mandatory=$true)]  [string]  $CertificatePwd,
    [Parameter(Mandatory=$true)]  [string]  $StartFolder,
    [Parameter(Mandatory=$false)] [string]  $KeyValutName ='',
    [Parameter(Mandatory=$false)] [string]  $CertificateName ='',
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


echo "$env:build_artifactstagingdirectory";

echo "Cert pwd has a value : $env:CERTIFICATEPWD"; 
echo "Cert var has a value : $env:CERTIFICATE"; 

#if ($null-eq$env:CERTIFICATE)
#{   write-output " Certificate Error"; }
#else
{
    write-output "Cert pwd has a value : $env:CertificatePwd"; 
    write-output "Cert var has a value : $env:CERTIFICATE"; 
        
    $PrivateCertKVBytes = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new("$CertificatePath","$CertificatePwd");
    
    write-output "a"; 

    $certObject = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new("$PrivateCertKVBytes","$CertificatePwd");
    write-output "B"; 

    if ($null-eq$certObject)
    { write-output "Cert Not Found"; }
    else
    {
        write-output "C"; 
        write-output "Cert Thumbprint : $($certObject.Thumbprint.ToString())"

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