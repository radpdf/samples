#Requires -RunAsAdministrator

# This script will download the RAD PDF NuGet package and then configure, install, and start the RAD PDF System Service
# RAD PDF rendering cache will be configured for the local Users group to use a folder similar to: C:\ProgramData\RadPdf\Cache\ via the below access rule (set to null if permissions should not be assigned)

# Set Paths
$RegistryPathBase = "HKLM:\SOFTWARE\Red Software\RadPdf\"
$RegistryPathInstall = $RegistryPathBase + "Install"
$RegistryPathRendering = $RegistryPathBase + "Rendering"
$RegistryPathWcf= $RegistryPathBase + "Wcf"

$RenderCacheLocation = "$Env:ProgramData\RadPdf\Cache\"
$RenderCacheLocationAr = New-Object System.Security.AccessControl.FileSystemAccessRule("Users", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow")
$RenderHelperArch = "win-x86"

# Register nuget.org as package source
Register-PackageSource -Name MyNuGet -Location https://www.nuget.org/api/v2 -ProviderName NuGet -Force

# Install latest RAD PDF package
Install-Package -Name RadPdf -SkipDependencies -Source MyNuGet -Force

# Get folder package RAD PDF was installed into
$FolderName = (Get-ChildItem "$Env:ProgramFiles\PackageManagement\NuGet\Packages" -Directory -Filter 'RadPdf.*' | Sort-Object -Property Name -Descending).Name
$FolderPackage = "$Env:ProgramFiles\PackageManagement\NuGet\Packages\" + $FolderName + "\"
$FolderService = $FolderPackage + "service\win\"

$ServiceExe = $FolderService + "RadPdfService.exe"

# Copy rendering DLL
$DllPath = $FolderPackage + "runtimes\" + $RenderHelperArch + "\native\RadPdfD.dll"
Copy-Item -Path $DllPath -Destination $FolderService -Force

# Install RAD PDF System Service
Start-Process -FilePath $ServiceExe -ArgumentList "-i" -NoNewWindow -Wait

# Set resources path in registry
$FolderResources = $Folder + "resources"

New-Item $RegistryPathInstall -Force
Set-ItemProperty -Path $RegistryPathInstall -Name "Resources" -Value $FolderResources -Type String -Force 

# Create render cache location folder
New-Item $RenderCacheLocation -Force -ItemType "directory"
if ($RenderCacheLocationAr -ne $null) {
	$Acl = Get-Acl $RenderCacheLocation
	$Acl.SetAccessRule($RenderCacheLocationAr)
	Set-Acl $RenderCacheLocation $Acl
}

# Set render cache location in registry and expiration (in minutes)
New-Item $RegistryPathRendering -Force
Set-ItemProperty -Path $RegistryPathRendering -Name "RenderCacheLocation" -Value $RenderCacheLocation -Type String -Force
Set-ItemProperty -Path $RegistryPathRendering -Name "RenderCacheTimeout" -Value 720 -Type Dword -Force

# Set up WCF
New-Item $RegistryPathWcf -Force
Set-ItemProperty -Path $RegistryPathWcf -Name "ServicePort" -Value 18104 -Type Dword -Force
Set-ItemProperty -Path $RegistryPathWcf -Name "UseImpersonation" -Value 0xFFFFFFFF -Type Dword -Force

# Start RAD PDF System Service
Start-Service -Name "RadPdfService"