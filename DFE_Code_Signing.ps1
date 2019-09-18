
#Requires -Version 3.0
Param(
    [Parameter(Mandatory=$true)]  [string]  $StartFolder,
    [Parameter(Mandatory=$false)] [string]  $FileFilter = "ESFA.DC.*",
    [Parameter(Mandatory=$false)] [string]  $TimestampServer = "http://timestamp.globalsign.com/scripts/timestamp.dll"
)

$cert = $env:CODESIGNCERT;

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

        Set-AuthenticodeSignature -FilePath $exePath -Certificate $certObject -TimestampServer $TimestampServer -force;
        Set-AuthenticodeSignature -FilePath $dllPath -Certificate $certObject -TimestampServer $TimestampServer -force;
    }
}
