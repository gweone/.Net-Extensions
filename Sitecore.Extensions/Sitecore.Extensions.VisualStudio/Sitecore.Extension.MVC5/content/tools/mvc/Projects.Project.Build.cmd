echo %1
Set gitpath="C:\Projects\BCACOID"
Set publishPath="C:\inetpub\wwwroot\sitecore93.sc"
Set publishnetPath="C:\inetpub\wwwroot\aspnet\aspnet.mvc"

echo "Build .Net Core"
dotnet build AspNet\AspNetCore.Web

echo "Publish .Net Core"
dotnet publish AspNet\AspNetCore.Web -c Debug --output C:\inetpub\wwwroot\aspnet\aspnet.core

echo "Restore Nuget Package"
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" AspNet\AspNet.sln /t:restore /p:RestorePackagesConfig=true

echo "Build and Publish"
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" AspNet\AspNet.MVCWeb /t:Rebuild /p:Configuration=Debug /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:publishUrl=C:\inetpub\wwwroot\aspnet\aspnet.mvc


rem "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" %gitpath %\BCACOID.sln /t:Feature\Library\Sitecore_Feature_BCACOID_Library:Rebuild /p:DeployOnBuild=true /p:Configuration=Debug /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:publishUrl=%publishPath %