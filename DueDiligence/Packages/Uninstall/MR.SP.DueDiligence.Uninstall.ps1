##================================================================================================
## Document Management & Look and FeelPowershell script to Install all WSPs, Scripts and XML files 
## Note:- Need to update the XML file path before running the script.
## Author	: 
## Date		: 03-May-2011
##================================================================================================

# ============================================ Setup Input Paths ===========================================================

$0 = $MyInvocation.MyCommand.Definition                 # Get complete file path (eg:  D:\AZESPMasterScript\AZESPMaster.ps1)
$dp0 = [System.IO.Path]::GetDirectoryName($0)           # Get current Directory  file path  (eg:  D:\AZESPMasterScript)
$bits = Get-Item $dp0 | Split-Path -Parent              # Get current Drive   (eg:  D:\)
$Host.UI.RawUI.WindowTitle = "-- Xerox Publishing Deployment Script --"

# ============================================Logging ======================================================================

## Start logging
$LogTime = Get-Date -Format yyyy-MM-dd_hh-mm
$LogFile = "$dp0\Patch-$LogTime.rtf"

$StartDate = Get-Date
Write-Host -ForegroundColor White "------------------------------------"
Write-Host -ForegroundColor White "| Automated Xerox Publishing Deployment Script|"
Write-Host -ForegroundColor White "| Started on: $StartDate |"
Write-Host -ForegroundColor White "------------------------------------"

## check to ensure Microsoft.SharePoint.PowerShell is loaded if not using the SharePoint Management Shell 
$snapin = Get-PSSnapin | Where-Object {$_.Name -eq 'Microsoft.SharePoint.Powershell'} 
if ($snapin -eq $null) 
{    
	Write-Host "Loading SharePoint Powershell Snapin..."    
	Add-PSSnapin "Microsoft.SharePoint.Powershell" 
}
 
#This takes care of the disposable objects to prevent memory leak.   
Start-Transcript $logfile  


[string]$ConfigFile = $("$dp0\Xerox.Publishing.Uninstall.xml")
[string]$WspPath = $("$dp0\WSPs\")

$xmlinput = [xml] (get-content $ConfigFile)
$Deployment = $xmlinput.Deployment


## Get The valude of the sitecollection
$sitecollectionurl = $Deployment.WebApplication + $Deployment.SiteCollection;
if ( $sitecollectionurl -eq $null -or $sitecollectionurl -eq "")
{
	$sitecollectionurl = read-host "Please input the site url:"
}

##=============================================Start Create Site Collections================================================
#Calling CreateSiteCollections script file. This script file will create Search, Content type hub site collections
if($Deployment.CreateSCCTHSiteCollections -eq "True")		
	{					
		Invoke-Expression "$dp0\SiteCollectionCreationScript\SiteCollectionProvisioning.ps1"
		#Start-Sleep -s 60
	}

##=============================================End Create Site Collections==================================================

# =================================================================================================================
# Func: DeleteSolution
# Desc: Delete Solution at Farm
# TODO: this method will delete and retract the solution. You will need to call it
		#with the solution id and the webapplication. If it is globally deployed just leave the web application empty
# =================================================================================================================
function DeleteSolution
{
	param ([string]$solutionID, [string]$webApplication)
	#Get all the details of the farm 
    $farm = Get-SPFarm
    
	#Get all the solutions deployed in the farm 
    $sol = $farm.Solutions[$solutionID]
    
	if($sol)
	{
		Write-host ""
		Write-Host -f Yellow "Going to uninstall $solutionID on web application $webApplication"

		if( $sol.Deployed -eq $TRUE ) 
		{
			if ( $webApplication -eq "" )
			{
                #Retracts the deployed SharePoint solution in preparation for removing it from the farm entirely and removes files from the front-end Web server
				Uninstall-SPSolution -Identity $solutionID -Confirm:0
			}
			else 
			{
				#Uninstalls the solution for the specified SharePoint Web application
                Uninstall-SPSolution -Identity $solutionID -Confirm:0 -Webapplication $webApplication
			}
           
            Write-Host "waiting for retraction..." -NoNewline
			while( $sol.JobExists ) 
			{
                Write-Host "." -NoNewline				
                sleep 2
			}
		}
        Write-Host "uninstalled." 
		Write-Host -f Yellow "Removing $solutionID ..."
        
        #To delete the solution package from the solution store of the farm
		Remove-SPSolution -Identity $solutionID -Force -Confirm:0

		Write-Host -f Green "$solutionID is deleted from this Farm" `n
        #Wait for 60 sec after removing the solutions
        Start-Sleep -s 60
	}
	else
	{
		Write-Host -f Yellow "Solution $solutionID not found"
	}
}

# =================================================================================================================
# Func: DeploySolution
# Desc: Install and deploy Solutions
# TODO: This method installs and deploys a solution based on its solution id and  path 
		#If the solution should be deployed globally just leave the web application empty
# =================================================================================================================
function DeploySolution
{     
	param ([string]$solutionID, [string]$webApplication)
	
    #Get the WSP name with literal path
    $filename = "$dp0\WSPs\$solutionID"
	Write-host ""
    Write-Host -f White "Deploy solution $solutionID from $filename to webapplication $webApplication"

	#Uploads the solution package to the farm
    Add-SPSolution $filename -Confirm:0

	Write-Host -f Green "Solution $solutionID added"
	Write-Host -f Yellow "Installing $solutionID into web application..."

	if ( $webApplication -eq "" )
	{		
        #Deploys an installed  solution in the farm
        Install-SPSolution –Identity $solutionID -GACDeployment -Force
	}
	else
	{
		#Deploys the SharePoint solution for the specified SharePoint Web application.
        Install-SPSolution –Identity $solutionID –WebApplication $webApplication -GACDeployment -Force
	}
    
    $farm = Get-SPFarm
	$sol = $farm.Solutions[$solutionID]

	if($sol)
	{
		Write-Host -f Yellow "Going to install $solutionID"        
        Write-Host "waiting for installation..."-NoNewline
		while( $sol.Deployed -eq $FALSE )
		{			
            Write-Host "." -NoNewline				
            sleep 2			
		}
        Write-Host "."        
		Write-Host -f Green "Solution $solutionID is installed" `n
       
       #wait for 60 sec after the solution is deployed
       Start-Sleep -s 60
	}
	else
	{
		Write-Host -f Red "Installing $solutionID has failed. Solution is not found."
	}
       
}

#----------------------------------------------------Load XML Document Function -------------------------
#Region Load xml configuration file
function LoadXMLDocument($fileName)
{
    [System.Xml.XmlDocument] $xmlinput = new-object System.Xml.XmlDocument
    $xmlinput.load($fileName) 
    return $xmlinput
}
#EndRegion

#----------------------------------------------------Get Single Node Function -----------------------------
#Region Get single node from xml file by xpath, if find then return the node, otherwise return null
function GetSingleNode($xmlDoc,$xpath)
{
    $node = $xmlDoc.selectsinglenode($xpath) 
    return $node 
}
#EndRegion

#Region Get node list from xml file by xpath, if find then return the nodes, otherwise return null
function GetNodes($xmlDoc,$xpath)
{
    $nodes = $xmlDoc.selectnodes($xpath)
    return $nodes
}
#EndRegion


# =================================================================================================================
# Func: ActivatingFeatures
# Desc: Activate features after installing them unless they are WebApplication scoped or Farm scoped Features.
# TODO: Activate features at Site, Web, Site collection Scope 
# =================================================================================================================
function ActivateFeatures($FeatureName, $FeatureId, $FeatureUrl, $FeatureScope)
{	
	$Featurepreactivate = $feature.getattribute("preactivate")
	$Featurepostactivate = $feature.getattribute("postactivate")	
	$FeatureActive = $null;
	$FeatureEffectiveUrl = $FeatureUrl;

	if($Featurepreactivate -ne "")		
	{
		Write-Host -ForegroundColor white "Calling Pre-Activate function - " $Featurepreactivate
		Invoke-Expression "$dp0\Scripts\$Featurepreactivate"
		#Start-Sleep -s 60
	}
	switch($FeatureScope)
	{		
		"Farm"
		{
			$FeatureActive = Get-SPFeature -Farm | where {$_.Id -eq $FeatureId}
			$FeatureEffectiveUrl = "Current Farm";
		}
		"WebApplication"
		{			
			if ($FeatureUrl -eq "")
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication;
			}
			$FeatureActive = Get-SPFeature -WebApplication $FeatureEffectiveUrl -Identity $FeatureId -ErrorAction SilentlyContinue
		}

		"Web"
		{		
			if ($FeatureUrl -eq "")
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication  +$Deployment.SiteCollection+ $Deployment.Web;
			}
			elseif ($FeatureUrl.StartsWith("http://","CurrentCultureIgnoreCase") -or $FeatureUrl.StartsWith("https://","CurrentCultureIgnoreCase"))	
			{
				$FeatureEffectiveUrl = $FeatureEffectiveUrl;
			}
			else
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication +$Deployment.SiteCollection+ $FeatureEffectiveUrl
			}
			$FeatureActive = Get-SPFeature -Web $FeatureEffectiveUrl -Identity $FeatureId -ErrorAction SilentlyContinue
		}
		
		"Site"
		{		
			if ($FeatureUrl -eq "")
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication+$Deployment.SiteCollection;
			}	
			elseif ($FeatureUrl.StartsWith("http://","CurrentCultureIgnoreCase") -or $FeatureUrl.StartsWith("https://","CurrentCultureIgnoreCase"))	
			{
				$FeatureEffectiveUrl = $FeatureEffectiveUrl;
			}
			else
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication + $FeatureEffectiveUrl
			}
			$FeatureActive = Get-SPFeature -Site $FeatureEffectiveUrl -Identity $FeatureId -ErrorAction SilentlyContinue
		}
	}
	
	Write-host ""
	if ($FeatureActive -eq $null)
	{
		Write-Host -f Yellow "Activating " $FeatureName "Feature at " $FeatureScope " level in" $FeatureEffectiveUrl			
		Enable-SPFeature -identity $FeatureId -URL $FeatureEffectiveUrl -force -Confirm:0
		Write-Host -f Green $FeatureName" Feature Activated Successfully `n"
		#Start-Sleep -s 60
	}
	else
    {
        Write-Host "Feature $FeatureName already activate at: $FeatureEffectiveUrl" -ForegroundColor White
    }		
	if($Featurepostactivate -ne "")		
	{
		Write-Host -ForegroundColor white "Calling Post-Activate function - " $Featurepostactivate
		Invoke-Expression "$dp0\Scripts\$Featurepostactivate"
		#Start-Sleep -s 60
	}
	Write-host ""
}
#End Activating Features

# =================================================================================================================
# Func: DeactivateFeatures
# Desc: Deactivate features before uninstalling them unless they are WebApplication scoped or Farm scoped Features.
# TODO: Deactivate features at Site, Web, Site collection Scope 
# =================================================================================================================
function DeactivateFeatures($FeatureName, $FeatureId, $FeatureUrl, $FeatureScope)
{	
	$Featurepredeactivate = $feature.getattribute("predeactivate")
	$Featurepostdeactivate = $feature.getattribute("postdeactivate")
	$FeatureEffectiveUrl = $FeatureUrl;
	

	if($Featurepredeactivate -ne "")		
	{
		Write-Host -ForegroundColor white "calling Pre-Deactivate function - " $Featurepredeactivate                   
		Invoke-Expression "$dp0\Scripts\$Featurepredeactivate"
		#Start-Sleep -s 60
	}
	switch($FeatureScope)
	{		
		"Farm"
		{
			$FeatureActive = Get-SPFeature -Farm | where {$_.Id -eq $FeatureId}
			$FeatureEffectiveUrl = "Current Farm";
		}
		"WebApplication"
		{			
			if ($FeatureUrl -eq "")
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication;
			}
			$FeatureActive = Get-SPFeature -WebApplication $FeatureEffectiveUrl -Identity $FeatureId -ErrorAction SilentlyContinue
		}

		"Web"
		{		
			if ($FeatureUrl -eq "")
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication  +$Deployment.SiteCollection+ $Deployment.Web;
			}
			elseif ($FeatureUrl.StartsWith("http://","CurrentCultureIgnoreCase") -or $FeatureUrl.StartsWith("https://","CurrentCultureIgnoreCase"))	
			{
				$FeatureEffectiveUrl = $FeatureEffectiveUrl;
			}	
			else
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication +$Deployment.SiteCollection + $FeatureEffectiveUrl
			}
			$FeatureActive = Get-SPFeature -Web $FeatureEffectiveUrl -Identity $FeatureId -ErrorAction SilentlyContinue 
		}
		
		"Site"
		{		
			if ($FeatureUrl -eq "")
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication+$Deployment.SiteCollection;
			}
			elseif ($FeatureUrl.StartsWith("http://","CurrentCultureIgnoreCase") -or $FeatureUrl.StartsWith("https://","CurrentCultureIgnoreCase"))	
			{
				$FeatureEffectiveUrl = $FeatureEffectiveUrl;
			}	
			else
			{
				$FeatureEffectiveUrl = $Deployment.WebApplication + $FeatureEffectiveUrl
			}
			$FeatureActive = Get-SPFeature -Site $FeatureEffectiveUrl -Identity $FeatureId -ErrorAction SilentlyContinue
		}
	}	

	Write-host ""
	if ($FeatureActive -eq $null)
	{
		Write-Host -ForegroundColor White "Feature $FeatureName already deactivate at :" $FeatureEffectiveUrl
	}
	else
    {
		Write-Host -f Yellow "Deactivating "$FeatureName "Feature at:" $FeatureEffectiveUrl
		Disable-SPFeature -identity $FeatureId -URL $FeatureEffectiveUrl	-confirm:$false -ErrorAction SilentlyContinue   
		Write-Host -f Green $FeatureName "Feature Deactivated Successfully"
		#Start-Sleep -s 60                   
    }
	if($Featurepostdeactivate -ne "")		
	{
		Write-Host -ForegroundColor white "calling Post-Deactivate function - " $Featurepostdeactivate
		Invoke-Expression "$dp0\Scripts\$Featurepostdeactivate"
		#Start-Sleep -s 60
	}
	Write-host ""
}
#End Deactivating Features

function DeploySolutions($fileName)
{
    $xmldoc = LoadXMLDocument -fileName $fileName
    $xpathforSolutions = "/Deployment/Solutions"
	$xpath = "/Deployment/Solutions/Solution"	
    
    #Delete solution before deploying new solution $xmlDoc,$xpath
	$solutionsParent = GetSingleNode -xmldoc $xmldoc -xpath $xpathforSolutions
    $solutions = $xmldoc.selectnodes($xpath) | sort -descending {[int]$_.sequence}
		
	####Call Delete Solutions
    if(($solutions -ne $null) -and ($Deployment.OnContentDeployment -ne $True))
    { 	   
        foreach($solution in $solutions)
        {           
		    $solutionName = $solution.getattribute("name")
		    
			
			$predelete = $solution.getattribute("predelete") 
			$postdelete = $solution.getattribute("postdelete")
			$globaldeploy = $solution.getattribute("globaldeploy")
					
				
				$xpath1 = ".//Features/Feature"  
				$features = GetNodes -xmlDoc $solution -xpath $xpath1 |  where {$_.IsActivate -eq "true"} |  sort -descending {[int]$_.FeatureSequence}	
		
				########### calling DeactivateFeatures function ##########
				if($features -ne $null)
				{
					foreach($feature in $features)
					{				 
						$DeactFeatureName = $feature.getattribute("Name")
						$DeactFeatureScope = $feature.getattribute("Scope")
						$DeactFeatureId = $feature.getattribute("Id")
						$DeactFeatureUrl = $feature.getattribute("Url")							
						
						DeactivateFeatures -FeatureName $DeactFeatureName -FeatureId $DeactFeatureId -FeatureScope $DeactFeatureScope -FeatureUrl $DeactFeatureUrl
					}	
				}			
		            
				#Check if the solution exists, if yes delete    
				$sl = get-spsolution | where {$_.name -eq $solutionName}
				if($sl -ne $null)
				{				   
           			if($predelete -ne "")		
					{
						Write-Host -ForegroundColor Magenta "Calling Pre-delete function" $predelete
						Invoke-Expression "$dp0\Scripts\$predelete"
						#Start-Sleep -s 60
					}
					########### calling DeleteSolution function #######
					#Write-Host -ForegroundColor Cyan "Delete solution" $sl
					if ($globaldeploy -eq "true")
					{
						$retractSiteUrl = ""
					}
					else
					{
						$retractSiteUrl = $Deployment.WebApplication
					}
            		DeleteSolution -solutionID $solutionName -webApplication $retractSiteUrl 
					if($postdelete -ne "")		
					{
						Write-Host -ForegroundColor Magenta "Calling Post-delete function" $postdelete
						Invoke-Expression "$dp0\Scripts\$postdelete"
						#Start-Sleep -s 60
					}				
				}
			
        }
    }

#wait for iisreset to clean the server cache	
write-host -ForegroundColor yellow ""
write-host -ForegroundColor yellow "***********************************************************************"
write-host -ForegroundColor yellow "***********************************************************************"
write-host -ForegroundColor white  "Please do IISRESET in another command window before we start deploying package into current farm"
write-host -ForegroundColor white  "Press anykey to continue after iisreset finished"
write-host -ForegroundColor yellow "***********************************************************************"
write-host -ForegroundColor yellow "***********************************************************************"
#read-host 


    #Call Deploy Solution
	$solutions = $xmldoc.selectnodes($xpath) | where {$_.IsDeploy -eq "true"} |  sort {[int]$_.sequence}	
    if($solutions -ne $null)
    { 
        foreach($solution in $solutions)
        {            
            $solutionName = $solution.getattribute("name")
			$preinstall = $solution.getattribute("preinstall") 
			$postinstall = $solution.getattribute("postinstall")     
			    
			$globaldeploy = $solution.getattribute("globaldeploy")
            
			
				if($preinstall -ne "")		
				{
					Write-Host -ForegroundColor white "Calling Pre-deploy function" $preinstall
					Invoke-Expression "$dp0\Scripts\$preinstall"
					#Start-Sleep -s 60
				}
				
				#Write-Host -ForegroundColor Cyan "Deploy solution" $wspFile
				########### calling DeploySolution function #######
				$sl = get-spsolution | where {$_.name -eq $solutionName}
				if($sl -eq $null)
				{
					if ($globaldeploy -eq "true")
					{
						$deploySiteUrl = ""
					}
					else
					{
						$deploySiteUrl = $Deployment.WebApplication
					}
					DeploySolution -solutionID $solutionName  -webApplication $deploySiteUrl
				}
				else
				{
					if ($globaldeploy -eq "true")
					{
						$deploySiteUrl = ""
					}
					else
					{
						$deploySiteUrl = $Deployment.WebApplication
						
					}
					$alreadyDeploy = $false
					$urls = $sl.DeployedWebApplications
					foreach($innerwebappurl in $urls)
					{
						if ($innerwebappurl.Url.Contains($deploySiteUrl))
						{
							$alreadyDeploy = $true;
							break								
						}
					}
					if ($alreadyDeploy -eq $false)
					{
						DeploySolution -solutionID $solutionName  -webApplication $deploySiteUrl
					}
					else
					{
						Write-Host -ForegroundColor yellow  "`"$solutionName`" is already exist in this WebAppliaction."
					}
				}

				#################
				if($postinstall -ne "")		
				{
					Write-Host -ForegroundColor white "Calling Post-deploy function" $postinstall
					Invoke-Expression "$dp0\Scripts\$postinstall"
					#Start-Sleep -s 60
				}
				#####################
                    
				$xpath1 = ".//Features/Feature" 
				$features = GetNodes -xmlDoc $solution -xpath $xpath1  | where {$_.IsActivate -eq "true"} | sort {[int]$_.FeatureSequence}	
		
				########### calling ActivateFeatures function ##########
				if($features -ne $null)
				{
					foreach($feature in $features)
					{				 
						$ActFeatureName = $feature.getattribute("Name")
						$ActFeatureScope = $feature.getattribute("Scope")
						$ActFeatureId = $feature.getattribute("Id")
						$ActFeatureUrl = $feature.getattribute("Url")							
					
						ActivateFeatures -FeatureName $ActFeatureName -FeatureId $ActFeatureId -FeatureScope $ActFeatureScope -FeatureUrl $ActFeatureUrl
					}			
				}
			
		} 	
    }		
}


#===========================================Deploy Solutions===========================================================

if($Deployment.CreateSiteCollection -eq "True")		
{					
	Write-Host -ForegroundColor white "Calling CreateSiteCollection PowerShell script" 
	Invoke-Expression "$dp0\SiteCollectionCreationScript\SiteCollectionProvisioning.ps1" 	
	Write-host ""
}


#===========================================De-Activate post-deployment feature start=========================================
$xmldoc = LoadXMLDocument -fileName $ConfigFile
$xpath = "/Deployment/PostDeploymentActivateFeatures/Feature"	
    
$postDeploymentActivatedFeatures = $xmldoc.selectnodes($xpath) | sort  {[int]$_.sequence}
		
########### calling ActivateFeatures function ##########
if($postDeploymentActivatedFeatures -ne $null)
{
	foreach($feature in $postDeploymentActivatedFeatures)
	{				 
		$ActFeatureName = $feature.getattribute("Name")
		$ActFeatureScope = $feature.getattribute("Scope")
		$ActFeatureId = $feature.getattribute("Id")
		$ActFeatureUrl = $feature.getattribute("Url")							
					
		DeactivateFeatures -FeatureName $ActFeatureName -FeatureId $ActFeatureId -FeatureScope $ActFeatureScope -FeatureUrl $ActFeatureUrl
	}			
}


#===========================================De-Activate post-deployment feature end=========================================


#Calling Deploy solutions function which will uninstall, Remove, Add & Install the solutions
#############################################################################################
#############################################################################################
DeploySolutions -fileName $ConfigFile 
#############################################################################################
#############################################################################################

#===========================================Activate post-deployment feature start=========================================
$xmldoc = LoadXMLDocument -fileName $ConfigFile
$xpath = "/Deployment/PostDeploymentActivateFeatures/Feature"	
    
$postDeploymentActivatedFeatures = $xmldoc.selectnodes($xpath) | sort  {[int]$_.sequence}
		
########### calling ActivateFeatures function ##########
if($postDeploymentActivatedFeatures -ne $null)
{
	foreach($feature in $postDeploymentActivatedFeatures)
	{				 
		$ActFeatureName = $feature.getattribute("Name")
		$ActFeatureScope = $feature.getattribute("Scope")
		$ActFeatureId = $feature.getattribute("Id")
		$ActFeatureUrl = $feature.getattribute("Url")							
					
		ActivateFeatures -FeatureName $ActFeatureName -FeatureId $ActFeatureId -FeatureScope $ActFeatureScope -FeatureUrl $ActFeatureUrl
	}			
}


#===========================================Activate post-deployment feature end=========================================


#===========================================Set Permission===========================================================
if($Deployment.UpdateSitePermission -eq "True")		
{					
	Write-Host -ForegroundColor white "Calling Update Site Permission PowerShell script" 
	Invoke-Expression "$dp0\SitePermission\UpdateSitePermission.ps1" 	
	Write-host ""
}
      
     
#=========================================== Content Deployment==================================
if ($Deployment.ContentDeployment -eq "True")
{
	Write-Host -ForegroundColor white "Calling Content Deployment initial script" 
	Invoke-Expression "$dp0\Scripts\ContentDeploymentScript\Xerox.Publishing.BusinessProcess.ps1" 	
	Write-host ""
}

Write-Host -ForegroundColor White " - Completed!`a"
$Host.UI.RawUI.WindowTitle = " -- Completed -- "
$EndDate = Get-Date
Write-Host -ForegroundColor White "------------------------------------"
Write-Host -ForegroundColor White "| Automated Xerox Publishing Portal Deploymentscript|"
Write-Host -ForegroundColor White "| Started on: $StartDate |"
Write-Host -ForegroundColor White "| Completed:  $EndDate |"
Write-Host -ForegroundColor White "-----------------------------------"
	                               
Stop-Transcript

#Invoke-Item $LogFile