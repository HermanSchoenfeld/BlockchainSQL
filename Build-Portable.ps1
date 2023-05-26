dotnet publish .\BlockchainSQL.Server\BlockchainSQL.Server.csproj -c Release /p:PublishProfile=BlockchainSQL.Server/Properties/PublishProfiles/portable.pubxml --output "Z:/Builds/BlockchainSQL/latest/portable"

dotnet publish .\BlockchainSQL.Web\BlockchainSQL.Web.csproj -c Release /p:PublishProfile=BlockchainSQL.Web/Properties/PublishProfiles/portable.pubxml --output "Z:/Builds/BlockchainSQL/latest/portable/web"

# Delete .pdb and .json files from the specified directory
$keepFiles = "appsettings.json", "web.config"
Get-ChildItem -Path "Z:/Builds/BlockchainSQL/latest/portable" -Include *.pdb, *.json, *.xml, *.config -Recurse | Where-Object { $_.Name -notin $keepFiles } | Remove-Item -Force