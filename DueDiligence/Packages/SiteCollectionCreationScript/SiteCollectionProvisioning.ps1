$filepath = $MyInvocation.MyCommand.Definition                 		# Get complete file path (eg:  E:\SP2010AppServer\xxxx.ps1)
$directorypath = [System.IO.Path]::GetDirectoryName($filepath)          # Get current Directory  file path  (eg:  E:\SP2010AppServer)

$0 = $MyInvocation.MyCommand.Definition                 # Get complete file path (eg:  D:\Xerpx.Publishing\Xerpx.Publishing.Master.ps1)
$dp0 = [System.IO.Path]::GetDirectoryName($0)           # Get current Directory  file path  (eg:  D:\Xerpx.Publishing)
$bits = Get-Item $dp0 | Split-Path -Parent              # Get current Drive   (eg:  D:\)



$snapin = Get-PSSnapin | Where-Object {$_.Name -eq 'Microsoft.SharePoint.Powershell'} 
if ($snapin -eq $null) 
{    
	Write-Host "Loading SharePoint Powershell Snapin..."    
	Add-PSSnapin "Microsoft.SharePoint.Powershell" 
}

#Start-Transcript $logfile 

function WriteErrorLog($errorMsg)
{
    Write-host $errorMsg
}

function WriteInformation($infoMsg)
{
    Write-host -ForegroundColor White $infoMsg
}

#Region Load xml configuration file
function LoadXMLDocument($fileName)
{
    [System.Xml.XmlDocument] $xmlinput = new-object System.Xml.XmlDocument
    $xmlinput.load($fileName)
    return $xmlinput
}
#EndRegion

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


function ActivateFeatures($SiteURL, $siteCollectionXmlDoc)
{
	    $FTitemCollection = $siteCollectionXmlDoc.Features.Feature
	
        $SiteColl = $SiteURL
        Write-host "Features activating in $SiteURL site "
	 	 	   
		foreach($featureItem in $FTitemCollection)   		
		{
			$featureName = $featureItem.Name
			$scope = $featureItem.Scope;
			$featureId = $featureItem.ID;
			$featureDescription = $featureItem.Description;
			Write-Host -f Yellow "(Scope:" $scope ") Activating " $featureName " (ID: " $featureId ") feature.  [Description:" $featureDescription "]";

			#$site = Get-SPSite $SiteColl;
			$targetUrl = $null;
			$FeatureActive  = $null;
			if ($scope -eq "Site")
			{
				$targetUrl = $SiteUrl;
				$FeatureActive = Get-SPFeature -Site $targetUrl | where {$_.Id -eq $FeatureId}
				
			}
			elseif($scope -eq "Web")
			{
				$targetUrl =  $SiteUrl + "/";
				$FeatureActive = Get-SPFeature -Web $targetUrl | where {$_.Id -eq $FeatureId}
				
			}
			else
			{
				#$targetUrl = $null;
				$FeatureActive  = $null;
			}

			if ($targetUrl -ne $null )
			{
				if ($FeatureActive -eq $null)
				{
					Enable-SPFeature -Identity $featureId -Url $targetUrl -ErrorAction SilentlyContinue
					Write-Host -f green "Activated"
					Start-Sleep -s 5
				}
				else
				{
					Write-Host -f green "Feature already activated";
				}
			}
		}
		
}

#Region Create site collection in the web application, if the site collection exsits then return the existed site collection
#otherwise create a new site collection  
$siteCollectionAlreadyexisted = $false;
Function CreateSiteCollection($name,$url, $description,$webApplication,$ownerAlias, $secondaryOwnerAlias,$contentdatabase,$Template,$LCID,$quotaTemplate)
{
    Try
    {
		$siteCollectionAlreadyexisted = $false;
        Write-Host -ForegroundColor White "- Start to create the Site Collection: `"$name`"..."
        $sc = get-spsite -identity $url -ErrorAction SilentlyContinue
        
        if($sc -eq $null)
        {

			$cntDB = get-spcontentdatabase -webapplication $webApplication | where-object {$_.name -eq $contentdatabase}
            if($cntDB -eq $null)
            {
                $cntDB = new-spcontentdatabase -name $contentdatabase -webapplication $webApplication 
            }

            $sc = New-SPSite -Url $url -OwnerAlias $ownerAlias -SecondaryOwnerAlias $secondaryOwnerAlias -ContentDatabase $contentdatabase -Description $description -Name $name -Language $LCID
            if($Template -ne $null -and $Template -ne "")
            {
                Set-SPSite -identity $sc -template $Template
            }
            if($quotaTemplate -ne $null -and $quotaTemplate -ne "")
            {
                Set-SPSite -identity $sc -QuotaTemplate $quotaTemplate
            }
        }
        else
        {
            #$sc = $null
			$siteCollectionAlreadyexisted = $true;
            Write-Host -ForegroundColor White "- Site collection: `"$name`" already exists, continuing..."
        }
    }
    catch
    {
        Write-Host $_
    }
    return $sc
}
#EndRegion

#Region Setup User Group for site collection
function SetupUserGroups($site,$owners,$members,$vistors)
{
    if($site -ne $null -and $site -ne "")
    {
        $web = $site.OpenWeb()
        $ownerGroup = $web.AssociatedOwnerGroup
        $memberGroup =$web.AssociatedMemberGroup
        $visitorGroup = $web.AssociatedVisitorGroup
        $xpath = "Identity"
        if($owners -ne $null -and $owners -ne "")
        {
            $users = GetNodes -xmlDoc $owners -xpath $xpath
            foreach($user in $users)
            {
                if($user -ne $null -and $user -ne "")
                {
                    if($user.InnerText -ne $null -and $user.InnerText -ne "")
                    {
                        New-SPUser -UserAlias $user.InnerText -Web $web -Group $ownerGroup
                    }
                }
            }
        }
        if($members -ne $null -and $members -ne "")
        {
            $users = GetNodes -xmlDoc $members -xpath $xpath
            foreach($user in $users)
            {
                if($user -ne $null -and $user -ne "")
                {
                    if($user.InnerText -ne $null -and $user.InnerText -ne "")
                    {
                        New-SPUser -UserAlias $user.InnerText -Web $web -Group $memberGroup
                    }
                }
            }
        }
        if($vistors -ne $null -and $vistors -ne "")
        {
            $users = GetNodes -xmlDoc $vistors -xpath $xpath
            foreach($user in $users)
            {
                if($user -ne $null -and $user -ne "")
                {
                    if($user.InnerText -ne $null -and $user.InnerText -ne "")
                    {
                        New-SPUser -UserAlias $user.InnerText -Web $web -Group $visitorGroup
                    }
                }
            }
        }
    }
    
}
#EndRegion

#Region Set if the site content can be searched
function SetSearchSetting($site,$searchable)
{
    if($site -ne $null)
    {
        $rootWeb = $site.RootWeb
        if($searchable -eq $false)
        {
            [Reflection.Assembly]::LoadWithPartialName("Microsoft.SharePoint")
            $rootWeb.AllowAutomaticASPXPageIndexing = $false;
            $rootWeb.ASPXPageIndexMode = [Microsoft.SharePoint.WebASPXPageIndexMode]::Never;
            $rootWeb.NoCrawl = $true;
            $rootWeb.Update();

        }
        
    }
}
#EndRegion

function SetAuditSettings
{
    param ([string]$SiteURL)
    $Site = Get-SPSite $SiteURL
    
	if ($Site -eq $null) 
	{
		Write-Host "--------------------------------------------------------------------"
		Write-Host "Unable to get the Site..."
		Write-Host "--------------------------------------------------------------------"
	}
	else
	{
		Write-Host "--------------------------------------------------------------------"
		$Site.Audit.AuditFlags =([Microsoft.SharePoint.SPAuditMaskType]::All)
		$Site.Audit.Update();
		
		Write-Host "Audit configuration updated successfully on Site collection - " $Site.Url
		Write-Host "--------------------------------------------------------------------"
	}
    $recordresourcesFeature = get-spfeature -id "RecordResources" -site $SiteURL -ErrorAction SilentlyContinue
    if($recordresourcesFeature -eq $null -or $recordresourcesFeature -eq "")
    {
        Enable-SPFeature -id "RecordResources" -url $SiteURL
    }
    $holdFeature = get-spfeature -id "Hold" -web $SiteURL -ErrorAction SilentlyContinue
    if($holdFeature -eq $null -or $holdFeature -eq "")
    {
        Enable-SPFeature -id "Hold" -url $SiteURL
    }
	

	Write-Host "Hold and eDiscovery Feature activated successfully on the Site- " $SiteURL
	Write-Host "----------------------------------------------------------------"

}

function EnableSubscription($site, $subID)
{
    if($site -ne $null -and $site -ne "")
    {
        if($subID -eq $null -or $subID -eq "")
        {
            $sub = New-SPSiteSubscription -ErrorAction SilentlyContinue
        }
        else
        {
            $sub = Get-SPSiteSubscription $subID -ErrorAction SilentlyContinue
        }
        if($sub -ne $null -and $sub -ne "")
        {
            set-spsite -identity $site -sitesubscription $sub  -confirm:$false -force
        }
    }
}

function RegisterContentHubProviderUrl($hubSiteUrl,$metadataServiceName,$proxyInstanceName)
{
	write-host -f yellow "Register Content Hub Provider in MetaData Service Application."
	
	$title = ""
	$message = "Press Y to continue if you want to update content type hub url in metadata service application. Please confirm before  you do this step"

	$yes = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes", `
	"I confirmed that this site collection $hubSiteUrl will be the Content Type Hub and this is the first time this site to be registerd in Metadata service."

	$no = New-Object System.Management.Automation.Host.ChoiceDescription "&No", `
	"No, this site had already registerd before. Or there is another site collection to be the Content Type Hub in current Metadata Service"

	$options = [System.Management.Automation.Host.ChoiceDescription[]]($yes, $no)
	$result = $host.ui.PromptForChoice($title, $message, $options, 0) 

	switch ($result)
	{
		0 
		{ 
			Get-SPServiceApplication | ForEach-Object { 
				if ($_.TypeName -eq $metadataServiceName) { $MetadataInstance = $_ } 
			}
			if ($MetadataInstance -ne $null)
			{
				Set-SPMetadataServiceApplication -Identity $MetadataInstance -HubURI $hubSiteUrl
				write-host -f Green "Site $siteUrl had already been Content Hub Provider in MetaData Service Application."

				
				Get-SPServiceApplicationProxy | ForEach-Object { 
					if ($_.TypeName -eq $proxyInstanceName) { $MetadataPrxInstance = $_ } 
				} 
					
				if ($MetadataPrxInstance -ne $null)
				{
					write-host -f yellow "Update Metadata service proxy settings."
					Set-SPMetadataServiceApplicationProxy -Identity $MetadataPrxInstance -ContentTypeSyndicationEnabled -ContentTypePushdownEnabled 
					write-host -f green  "Metadata service proxy settings had been updated."
				}
			}
		}
		1 {"Site url not registed in script"}
	}
}


Function Main()
{
    try
    {
        $xmlInput = LoadXMLDocument -fileName "$directorypath\SetupInputs.xml"
        $xpath = "/SiteCollections/SPSiteCollection"
        $siteCollections = GetNodes -xmlDoc $xmlInput -xpath $xpath
        if($siteCollections -ne $null -and $siteCollections -ne "")
        {
            foreach($siteCollection in $siteCollections)
            {
                $webAppIdentity = $siteCollection.GetAttribute("webApplication")
                $webApp = Get-SpWebApplication | where{$_.Displayname -eq $webAppIdentity -or $_.Url -eq $webAppIdentity}
                if($webApp -ne $null)
                {
                    $siteUrl = $webApp.Url+$siteCollection.ManagedPath
                    $site = CreateSiteCollection -name $siteCollection.Name -url $siteUrl -description $siteCollection.Description -webApplication $webApp -ownerAlias $siteCollection.OwnerAlias -secondaryOwnerAlias $siteCollection.SecondaryOwnerAlias -contentdatabase $siteCollection.ContentDatabase -Template $siteCollection.Template -LCID $siteCollection.LCID -QuotaTemplate $siteCollection.QuotaTemplate
                    if($site -ne $null -and $site -ne "")
                    {
						if ($siteCollectionAlreadyexisted -eq $false)
						{
							$xpath = "UserGroups/SiteOwners"
							$siteOwners = GetNodes -xmlDoc $siteCollection -xpath $xpath
							$xpath = "UserGroups/SiteMembers"
							$siteMembers = GetNodes -xmlDoc $siteCollection -xpath $xpath
							$xpath = "UserGroups/SiteVisitors"
							$siteVisitors = GetNodes -xmlDoc $siteCollection -xpath $xpath
                        
							$web = $site.RootWeb
							$PrimaryLogin = $web.SiteAdministrators[0] #get-spuser -web $web | where{$_.DisplayName -eq $siteCollection.OwnerAlias}
							$SecondaryLogin = $web.SiteAdministrators[1] #get-spuser -web $web | where{$_.DisplayName -eq $siteCollection.SecondaryOwnerAlias}

							$web.CreateDefaultAssociatedGroups($PrimaryLogin,$SecondaryLogin,"")
                        
							SetupUserGroups -site $site -owners $siteOwners -members $siteMembers -vistors $siteVisitors
                        
							$searchable = [bool][int]$siteCollection.SearchSetting.IncluedInSearchResult
							SetSearchSetting -site $site -searchable $searchable
                        
							SetAuditSettings -SiteURL $siteUrl  
                        
							$tenancyEnabled = [bool][int]$siteCollection.Tenancy.getattribute("enabled")
							if($tenancyEnabled -eq $true)
							{
								$subID = $siteCollection.Tenancy.SiteCollectionURL
								EnableSubscription -site $site -subID $subID
							}
                        
							write-host -f green $siteUrl "is created"

							if ($siteCollection.RegisterInMetaDataServiceForContentHub.InnerText -eq $true)
							{
								$serviceApplicationName = $siteCollection.RegisterInMetaDataServiceForContentHub.GetAttribute("MetadataServiceApplicationName");
								$proxyName = $siteCollection.RegisterInMetaDataServiceForContentHub.getattribute("ProxyInstanceName");
								RegisterContentHubProviderUrl -hubSiteUrl $siteUrl -metadataServiceName $serviceApplicationName -proxyInstanceName $proxyName;
							}
							else
							{
								write-host -f red $siteCollection.RegisterInMetaDataServiceForContentHub
							}
						}
						$title = ""
						$message = "Press Y to continue if you want to activating features"

						$yesEntry = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes", `
						"Yes, Please confirm that you want to activate features."

						$noEntry = New-Object System.Management.Automation.Host.ChoiceDescription "&No", `
						"No, I will activate feature manually."

						$options = [System.Management.Automation.Host.ChoiceDescription[]]($yesEntry, $noEntry)
						$result = $host.ui.PromptForChoice($title, $message, $options, 0) 

						switch ($result)
						{
							0 { ActivateFeatures -SiteURL $siteUrl -siteCollectionXmlDoc $siteCollection	}
							1 {"Features are not activated."}
						}
                                
                    }
                    
                }
                else
                {
                    write-output "The web application: $webAppIdentity does not exist, the site collection cannot be created"                    
                }
            }
        }
     }
     catch
     {
        write-output $_
     }
}

Main
