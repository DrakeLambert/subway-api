$ErrorActionPreference = 'Stop'

$baseUri = 'http://localhost:5001'

$allStations = Invoke-RestMethod "$baseUri/stations"

Write-Host "Got $($allStations.Count) stations"

$twoStationIds = $allStations
| Select-Object -First 2
| Select-Object -expand id
$station1Id = $twoStationIds[0]
$station2Id = $twoStationIds[1]

Write-Host "First two station ids: $station1Id, $station2Id"

$distanceBetweenFirstTwo = Invoke-RestMethod "$baseUri/distances?station1id=$station1Id&station2id=$station2Id"

Write-Host "Distance between first two: $distanceBetweenFirstTwo"

$username1 = "testUser-1-" + (Get-Random)
$password1 = "Password1!"

$addUserBody = @{
    username = $username1
    password = $password1
} | ConvertTo-Json

Invoke-RestMethod "$baseUri/users" -Method Post -ContentType "application/json" -Body $addUserBody | Out-Null

Write-Host "Added user with name $username1 and password $password1"

$addFrequentedStationBody = @{
    stationId = $station1Id
} | ConvertTo-Json

$bytes = [System.Text.Encoding]::UTF8.GetBytes("$($username1):$password1")
$base64 = [System.Convert]::ToBase64String($bytes)
$user1Headers = @{
    "Authorization" = "Basic $base64"
}

Invoke-RestMethod "$baseUri/frequented-stations" -Method Post -Headers $user1Headers -ContentType "application/json" -Body $addFrequentedStationBody | Out-Null

Write-Host "Added frequented station $station1Id for user $username1"

$user1FrequentedStations = Invoke-RestMethod "$baseUri/frequented-stations" -Headers $user1Headers | ConvertTo-Json

Write-Host "User 1 frequented stations: $user1FrequentedStations"

$username2 = "testUser-2-" + (Get-Random)
$password2 = "Password2!"

$addUserBody = @{
    username = $username2
    password = $password2
} | ConvertTo-Json

Invoke-RestMethod "$baseUri/users" -Method Post -ContentType "application/json" -Body $addUserBody | Out-Null

Write-Host "Added user with name $username2 and password $password2"

$bytes = [System.Text.Encoding]::UTF8.GetBytes("$($username2):$password2")
$base64 = [System.Convert]::ToBase64String($bytes)
$user2Headers = @{
    "Authorization" = "Basic $base64"
}

$user2FrequentedStations = Invoke-RestMethod "$baseUri/frequented-stations" -Headers $user2Headers | ConvertTo-Json

Write-Host "User 2 frequented stations: $user2FrequentedStations"