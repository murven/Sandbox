$accounts = (&.\Load-GW2-Accounts.ps1)
$apiConfig = (&.\Load-GW2-Api-Config.ps1)
$fileDateTimeFolder = (Get-Date -Format FileDateTimeUniversal)
$apiBaseUri = "$($apiConfig.schema)://$($apiConfig.host)/$($apiConfig.version)"
$apiEndpointUri = "$apiBaseUri/account"
foreach ($account in $accounts) {
    $headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
    $headers.Add("Authorization", "Bearer $($account.apiKey)")
    $response = Invoke-RestMethod $apiEndpointUri -Method 'GET' -Headers $headers
    $outputFolder = "$($account.outputFolder)\snapshot\$($account.name)\$($fileDateTimeFolder)" 
    $outputFile = "$outputFolder\account.json"
    New-Item -ItemType Directory -Force -Path $outputFolder
    $response | ConvertTo-Json | Out-File -FilePath $outputFile
}