#Requires -RunAsAdministrator

# This script will install RAD PDF or update it to the latest release

# Configuration - for information about installer arguments, see: https://www.advancedinstaller.com/user-guide/exe-setup-file.html
$DownloadPath = $env:USERPROFILE
$InstallerArguments = "/exenoui /qn"
$RegistryPathInstall = "HKLM:\SOFTWARE\Red Software\RadPdf\Install"
$RegistryVersion = "Version"
$UpdatesUrl = "https://www.radpdf.com/releases/updates/"

# Download updates information
$UpdatesLines = (Invoke-WebRequest -Uri $UpdatesUrl -UseBasicParsing).Content.Split([Environment]::NewLine)
foreach ($line in $UpdatesLines)
{
	# remove all whitespace for consistent processing
	$line = $line -Replace '\s', ''

	# example line: URL = https://downloads.radpdf.com/RadPdfInstaller.3.41.0.0.exe
	# becomes line: URL=https://downloads.radpdf.com/RadPdfInstaller.3.41.0.0.exe
	if ($line.StartsWith("URL="))
	{
		$UpdateUrl = $line.Substring(4)
	}
	# example line: Version = >= 3.41.0.0
	# becomes line: Version=>=3.41.0.0
	if ($line.StartsWith("Version=>="))
	{
		$UpdateVersion = $line.Substring(10)
	}
}

# If update information is missing
if ((-not $UpdateUrl) -or (-not $UpdateVersion))
{
    Write-Host "Update information could not be retrieved!"
    Exit -1
}

# Get version installed (if any)
$InstalledVersion = (Get-ItemProperty -Path $RegistryPathInstall -Name $RegistryVersion -ErrorAction Ignore).$RegistryVersion

# If the version installed (if any) is not the latest version
if ($InstalledVersion -ne $UpdateVersion)
{
	$InstallerFileName = $UpdateUrl.Split("/")[-1]
	$InstallerPath = $DownloadPath + "\" + $InstallerFileName

	Invoke-WebRequest -Uri $UpdateUrl -OutFile $InstallerPath
	
	# If there is a previous version installed
	if ($InstalledVersion)
	{
		Stop-Service -Name "RadPdfService" -Force
	}

	Start-Process -FilePath $InstallerPath -ArgumentList $InstallerArguments -Wait

	Start-Service -Name "RadPdfService"
}
else
{
	Write-Host "RAD PDF is up to date"
}