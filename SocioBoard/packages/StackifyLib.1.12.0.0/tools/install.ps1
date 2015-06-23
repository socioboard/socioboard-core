param($installPath, $toolsPath, $package, $project)
Import-Module (Join-Path $toolsPath StackifyInstallModule.psm1)
get_activation_key $project
$DTE.ItemOperations.Navigate("https://github.com/stackify/stackify-api-dotnet")