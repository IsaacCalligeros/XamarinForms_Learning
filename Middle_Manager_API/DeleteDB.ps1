
$confirmation = Read-Host "Are you Sure You Want To Proceed *I will nuke your db*"
if ($confirmation -eq 'y') {
    del *.db
    del Migrations/*.cs
    dotnet ef migrations add init
    dotnet ef database update
}