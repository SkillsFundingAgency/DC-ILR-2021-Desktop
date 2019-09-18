
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


#Write-Host  "Certificate pwdEnv has a value : $env:CODESIGNPASSWORD"; 
#Write-Host "Certificate CERT has a value : $env:CODESIGNCERT"; 


$cert = $env:CODESIGNCERT;
#Write-Host "Code Sign Cert DataType : $($cert.GetType().FullName)"

#$certPwd = $env:CODESIGNPASSWORD;
#Write-Host "Code Sign Cert Pwd DataType : $($certPwd.GetType().FullName)"

if ($null-eq$env:CODESIGNCERT)
{   Write-Host " Certificate Error"; }
else
{
    $PrivateCertKVBytes = [System.Convert]::FromBase64String($cert)
    
    $certObject = [System.Security.Cryptography.X509Certificates.X509Certificate2]::new($PrivateCertKVBytes,"");

    if ($null-eq$certObject)
    { Write-Host "Cert Not Found"; }
    else
    {
        $exePath = "$($StartFolder)\$($FileFilter).exe"
        $dllPath = "$($StartFolder)\$($FileFilter).dll"

        Write-Host "codeSign - EXEs"
        Set-AuthenticodeSignature -FilePath $exePath -Certificate $certObject -TimestampServer $TimestampServer -force;

        Write-Host "codeSign - Dlls"
        Set-AuthenticodeSignature -FilePath $dllPath -Certificate $certObject -TimestampServer $TimestampServer -force;
    }
}

###  ###
### END ###