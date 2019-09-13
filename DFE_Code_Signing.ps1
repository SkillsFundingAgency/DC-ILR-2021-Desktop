
#Login-AzureRmAccount
#Connect-Azure
#Get-AzureRmSubscription | Out-GridView -PassThru | Set-AzureRmContext
#Get-AzureRmContext

#Requires -Version 3.0
Param(
    [Parameter(Mandatory=$true)] [string]  $Certificate,
    [Parameter(Mandatory=$true)] [string]  $CertificatePwd,

    #[Parameter(Mandatory=$false)] [string]  $KeyValutName ='',
    #[Parameter(Mandatory=$false)] [string]  $CertificateName ='',

    [Parameter(Mandatory=$true)]  [string]  $StartFolder,
    [Parameter(Mandatory=$false)] [string]  $FileFilter = "ESFA.DC.*",
    [Parameter(Mandatory=$false)] [string]  $TimestampServer = "http://timestamp.globalsign.com/scripts/timestamp.dll"
)

### Powershell Script to Sign DfE applicatuons in-line during Automated Build on VSTS/Azure ###
###
###

#$AzureKeyVaultSecret=Get-AzureKeyVaultSecret -VaultName $KeyValutName.tolower().Replace(".vault.azure.net","") -Name $CertificateName -ErrorAction SilentlyContinue

if ($null-eq$Certificate)
{   writre-host " Unable to download Certificate from Key Vault | Valut name : $($KeyValutName) - CertName : $($) "; }
else
{

    write-host "Cert pwd has a value : $env:CertificatePwd"; 
    write-host "Cert var has a value : $Certificate"; 

    
    #Convert private cert to bytes
    #$PrivateCertKVBytes = [System.Convert]::FromBase64String($AzureKeyVaultSecret.SecretValueText)

    #Convert Bytes to Certificate (flagged as exportable & retaining private key)
    #possible flags: https://msdn.microsoft.com/en-us/library/system.security.cryptography.x509certificates.x509keystorageflags(v=vs.110).aspx
    #$certObject = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2 -argumentlist $PrivateCertKVBytes,$CertificatePwd

    $PrivateCertKVBytes = [System.Convert]::FromBase64String($Certificate)

    #write-host $PrivateCertKVBytes
    
    write-host "a"; 

    $certObject = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new($PrivateCertKVBytes,$CertificatePwd);
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