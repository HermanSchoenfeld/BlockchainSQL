$outputPath=$args[0]
cd $PSScriptRoot
dotnet publish .\BlockchainSQL.Server\BlockchainSQL.Server.csproj --runtime=win-x64 --no-self-contained -p:PublishTrimmed=false -p:PublishSingleFile=false -p:PublishReadyToRun=false --configuration=Release -o $outputPath


$webPath = Join-Path $outputPath "web";
dotnet publish .\BlockchainSQL.Web\BlockchainSQL.Web.csproj --runtime=win-x64 --no-self-contained -p:PublishTrimmed=false -p:PublishSingleFile=false -p:PublishReadyToRun=false --configuration=Release -o $webPath
