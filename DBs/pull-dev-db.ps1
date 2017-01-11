param($dbserver, $username, $password)
$dbname = 'UmbracoProjectTemplate'
$timestamp = Get-Date -Format 'yyyyMMddTHHmmss'
& sqlpackage /a:export /ssn:"$dbserver" /sdn:"$dbname" /su:"$username" /sp:"$password" /tf:"$dbname-$timestamp.bacpac"
