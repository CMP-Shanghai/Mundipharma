#********************************************************************
#	Set Master  Page
#	  
#********************************************************************


# Log the Power shell window text
$0 = $MyInvocation.MyCommand.Definition                 # Get complete file path of file 
$dp0 = [System.IO.Path]::GetDirectoryName($0)           # Get current Directory file path 
$bits = Get-Item $dp0 | Split-Path -Parent              # Get current Drive   (eg:  D:\)


## check to ensure Microsoft.SharePoint.PowerShell is loaded if not using the SharePoint Management Shell 
$snapin = Get-PSSnapin | Where-Object {$_.Name -eq 'Microsoft.SharePoint.Powershell'} 
if ($snapin -eq $null)  
{    
	Write-Host "Loading SharePoint Powershell Snapin..."    
	Add-PSSnapin "Microsoft.SharePoint.Powershell" 
    Write-Host "SharePoint Powershell Snapin Loaded"   
}

 if($sitecollectionurl -eq $null)
{

    $sitecollectionurl=read-host "Please input the site url:"
}
#This takes care of the disposable objects to prevent memory leak.   

#Step0 : Assign values for Variable
$scriptFolderPath = $dp0

#Step1 : Create Site Collection Application
$command = "& '$scriptFolderPath\SetMasterPage.ps1' "
Invoke-Expression $command
