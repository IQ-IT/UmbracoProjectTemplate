param($dbserver, $username, $password, $fileName)
$dbname = 'UmbracoProjectTemplate'
& sqlpackage /a:import /tsn:"$dbserver" /tdn:"$dbname" /tu:"$username" /tp:"$password" /sf:"$fileName"
