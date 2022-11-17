Task default -depends UpdateReadme

Task SetupModule -Description "Setup the PowerShell Module" -depends CreateModuleDirectory, CleanProject, BuildProject, CopyModuleFiles, CreateExternalHelp, CreateCabFile, CreateNuSpec, NugetPack, NugetPush, RemoveModuleDirectory

Task UpdateReadme -Description "Update the README file" -depends CreateModuleDirectory, CleanProject, BuildProject, CopyModuleFiles, RemoveModuleDirectory -Action {
 $moduleName =  'PoshMongo'
 $readMe = Get-Item .\README.md

 $TableHeaders = "| Latest Version | PowerShell Gallery | Build Status | Project Board | Issues | License |"
 $Columns = "|-----------------|-----------------|----------------|----------------|----------------|----------------|"
 $VersionBadge = "[![Latest Version](https://img.shields.io/github/v/tag/PoshMongo/PoshMongo)](https://github.com/PoshMongo/PoshMongo/tags)"
 $GalleryBadge = "[![Powershell Gallery](https://img.shields.io/powershellgallery/dt/PoshMongo)](https://www.powershellgallery.com/packages/PoshMongo)"
 $StatusBadge = "[![Build Status](https://dev.azure.com/patton-tech/SchemaModule/_apis/build/status/SchemaModule.PowerShell?repoName=SchemaModule%2FPowerShell&branchName=master)](https://dev.azure.com/patton-tech/SchemaModule/_build/latest?definitionId=9&repoName=SchemaModule%2FPowerShell&branchName=master)"
 $BoardBadge = "[![Board Status](https://dev.azure.com/patton-tech/cbaf64d2-5310-475c-8874-fb2b809e3f3b/1138b36a-5cb9-4d7d-8163-32041295538f/_apis/work/boardbadge/d9f5bd6d-43e7-45ac-a1aa-f1afc7f0cb17?columnOptions=1)](https://dev.azure.com/patton-tech/cbaf64d2-5310-475c-8874-fb2b809e3f3b/_boards/board/t/1138b36a-5cb9-4d7d-8163-32041295538f/Microsoft.RequirementCategory/)"
 $IssueBadge = "[![GitHub issues](https://img.shields.io/github/issues/PoshMongo/PoshMongo)](https://github.com/PoshMongo/PoshMongo/issues)"
 $LicenseBadge = "[![GitHub license](https://img.shields.io/github/license/PoshMongo/PoshMongo)](https://github.com/PoshMongo/PoshMongo/blob/master/LICENSE)"

 if (!(Get-Module -Name $moduleName )) {Import-Module -Name ".\$($moduleName)\Module\$($moduleName).psd1" }

 Write-Output $TableHeaders |Out-File $readMe.FullName -Force
 Write-Output $Columns |Out-File $readMe.FullName -Append
 Write-Output "| $($VersionBadge) | $($GalleryBadge) | $($StatusBadge) | $($BoardBadge) | $($IssueBadge) | $($LicenseBadge) |" |Out-File $readMe.FullName -Append

 Get-Content .\Docs\PoshMongo.md |Select-Object -Skip 8 |ForEach-Object {$_.Replace('(','(docs/')} |Out-File $readMe.FullName -Append
 Write-Output "" |Out-File $readMe.FullName -Append
 Get-Content .\Build.md |Out-File $readMe.FullName -Append
}

Task NewTaggedRelease -Description "Create a tagged release" -depends CreateModuleDirectory, CleanProject, BuildProject, CopyModuleFiles, RemoveModuleDirectory -Action {
 $moduleName =  'PoshMongo'

 if (!(Get-Module -Name $moduleName )) {Import-Module -Name ".\$($moduleName)\Module\$($moduleName).psd1" }
 $Version = (Get-Module -Name $moduleName |Select-Object -Property Version).Version.ToString()
 git tag -a v$version -m "$($moduleName) Version $($Version)"
 git push origin v$version
}

Task CleanProject -Description "Clean the project before building" -Action {
 dotnet clean .\PoshMongo\PoshMongo.csproj
}

Task BuildProject -Description "Build the project" -Action {
 dotnet build .\PoshMongo\PoshMongo.csproj -c Release
}

Task CreateModuleDirectory -Description "Create the module directory" -Action {
 New-Item Module -ItemType Directory
}

Task RemoveModuleDirectory -Description "Remove module directory" -Action {
 Remove-Item Module -Recurse -Force
}

Task CopyModuleFiles -Description "Copy files for the module" -Action {
 Copy-Item .\PoshMongo\bin\Release\net6.0\PoshMongo.dll Module
 Copy-Item .\PoshMongo.psd1 Module
}

Task CreateExternalHelp -Description "Create external help file" -Action {
 New-ExternalHelp -Path .\Docs -OutputPath .\Module\ -Force
}

Task CreateCabFile -Description "Create cab file for download" -Action {
 New-ExternalHelpCab -CabFilesFolder .\Module\ -LandingPagePath .\Docs\PoshMongo.md -OutputFolder .\cabs\
}

Task CreateNuSpec -Description "Create NuSpec file for upload" -Action {
 .\ConvertTo-NuSpec.ps1 -ManifestPath ".\Module\PoshMongo.psd1"
}

Task NugetPack -Description "Pack the nuget file" -Action {
 nuget pack .\Module\PoshMongo.nuspec -OutputDirectory .\Module
}

Task NugetPush -Description "Push nuget to PowerShell Gallery" -Action {
	$config = [xml](Get-Content .\nuget.config)
 nuget push .\Module\*.nupkg -NonInteractive -ApiKey "$($config.configuration.apikeys.add.value)" -ConfigFile .\nuget.config
}