# Get XML Configuration file
$InputFile = $configurationFilePath1
$groupXML = [xml] (Get-Content $InputFile)

Write-host""
Write-host -f Yellow "Starting update Role Assignment on web"

# Function Grant Permission to Group
function UpdateGroupPermission ($curWeb, $groupName, $permLevel)
{
     $group = $curWeb.SiteGroups[$groupName]
	 if ($group -ne $null)
	 {
		 $assignment = New-Object Microsoft.SharePoint.SPRoleAssignment($group)

		 $role = $curWeb.RoleDefinitions[$permLevel]
		 $assignment.RoleDefinitionBindings.Add($role)
		 $curWeb.RoleAssignments.Add($assignment)
		 Write-host -f Green "Group [$groupName] on [$curWeb] had been granted [$permLevel] permission"
		 Write-host ""
	 }
}


# foreach subsite
$groupXML.Site.SubSite| ForEach-Object {
    # Get SubSite Url
	$webUrl = $sitecollectionurl + $_.Url;
    $web = Get-SPWeb $webUrl
    
	write-host "Updating group permission on below web $webUrl"
    
    # Check if it is subSite
    if( $_.isSubSite -eq "True")
    {
		$web.update()
        $web.BreakRoleInheritance($True,$False)
        $web.update()
    }
           
    if($web -ne $null )
    {
        # foreach group in subsite
         $_.Group| ForEach-Object {

            # Check to see if SharePoint group already exists in the site collection
        	if ($web.SiteGroups[$_.Name] -eq $null)
        	{
        		#If the SharePoint group doesn't exist already - create it from the name and description values at the node
        		# Check if there is owneraccount 
        		$groupOwner = $web.CurrentUser;
        		if($_.OwnerAccount -ne $null)
        		{
        			$groupOwner = Get-SPUser -Identity $_.OwnerAccount -Web $web
        			if ($groupOwner -eq $null)
        			{
        				$groupOwner = $web.CurrentUser;
        			}
        		}
        		$newGroup = $web.SiteGroups.Add($_.Name, $groupOwner, $null, $_.Description)
                write-host -f Green "SharePoint Group $_.Name created";
        	} 
             
			#Get SharePoint group from the site collection
			$group = $web.SiteGroups[$_.Name]
			write-host "Begin to update $group permission" -f Yellow

			#Add the users defined in the XML to the SharePoint group
			if($_.Users.User -ne $null)
			{
				$_.Users.User | ForEach-Object {
					$group.Users.Add($_.Name, "", "", "")
				}
			}
              
        	if($_.PermissionLevels.PermissionLevel -ne $null) 
        	{
        		$_.PermissionLevels.PermissionLevel | ForEach-Object {
        			UpdateGroupPermission -curWeb $web -groupName $group -permLevel $_.Name
        		}
        	}
        }#end group foreach

		# remove publish site unnessary groups 
		if( $_.isSubSite -eq "False")
		{
			$siteGroupsIndex = $web.SiteGroups.Count -1
			for($siteGroupsIndex;$siteGroupsIndex -ge 0; $siteGroupsIndex--)
			{
				$flag = 0
				for($groupIndex = $_.Group.Count -1;$groupIndex -ge 0; $groupIndex--)
				{
                 
					if($web.SiteGroups[$siteGroupsIndex].Name.TrimEnd() -eq $_.Group[$groupIndex].Name.TrimEnd())
					{
						$flag = 1
					}   
				}
				if($flag -eq 0)
				{
					write-host -f Green " Group ["$web.SiteGroups[$siteGroupsIndex].Name"] will be deleted from current site."
					$web.SiteGroups.Remove($siteGroupsIndex)
				}
			}
		}
        
		if ($web -ne $null)
		{
			$web.Update();
			$web.Dispose();
		}
        
     }  
}