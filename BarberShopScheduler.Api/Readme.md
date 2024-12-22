    New-SelfSignedCertificate `
    -DnsName "192.168.2.242:7035" `
    -CertStoreLocation "Cert:\CurrentUser\My" `
    -FriendlyName "BarberShopSchedulerCert" `
    -NotAfter (Get-Date).AddYears(5) `
    -KeyUsage DigitalSignature, KeyEncipherment `
    -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.1") `
    -Type SSLServerAuthentication


    $cert = Get-ChildItem -Path Cert:\CurrentUser\My | Where-Object { $_.FriendlyName -eq "BarberShopSchedulerCert" } 
    Export-PfxCertificate -Cert $cert.PSPath -FilePath "C:\Users\Maicon\AppData\Roaming\Microsoft\Crypto\RSA\MachineKeys\BarberShopSchedulerCert.pfx" -Password (ConvertTo-SecureString -String "Y6t5r4e3w2q1@1" -Force -AsPlainText)


    Import-PfxCertificate -FilePath "C:\Users\Maicon\AppData\Roaming\Microsoft\Crypto\RSA\MachineKeys\BarberShopSchedulerCert.pfx" -CertStoreLocation "Cert:\LocalMachine\Root" -Password (ConvertTo-SecureString -String "Y6t5r4e3w2q1@1" -Force -AsPlainText)

