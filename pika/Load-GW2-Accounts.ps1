return (Get-Content .\gw2-accounts.json) | Out-String | ConvertFrom-Json