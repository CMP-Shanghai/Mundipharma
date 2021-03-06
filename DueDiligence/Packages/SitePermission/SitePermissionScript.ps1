# Get XML file
$InputFile =$configurationFilePath
$permissionXML = [xml] (Get-Content $InputFile)

Write-host ""
Write-host "Starting update premission level on site $sitecollectionurl" -f Yellow
# Get Current Web
$web = Get-SPWeb $sitecollectionurl
# Walk through each permission node
for ($permissionLevelIndex = $permissionXML.Site.PermissionLevel.Count - 1; $permissionLevelIndex -ge 0; $permissionLevelIndex--)
{
    if ($permissionXML.Site.PermissionLevel[$permissionLevelIndex].Name -ne $null)
    {
        $editRoleDefinitionName = $permissionXML.Site.PermissionLevel[$permissionLevelIndex].Name
        if($web.RoleDefinitions[$editRoleDefinitionName] -ne $null)
        {
            $editPermissionLevel = $web.RoleDefinitions[$editRoleDefinitionName]               
            $editbasePermission = "EmptyMask,"
            
			for ( $permissionIndex = $permissionXML.Site.PermissionLevel[$permissionLevelIndex].Permission.Count- 1; $permissionIndex -ge 0; $permissionIndex--)
            {
                $basePermission=$permissionXML.Site.PermissionLevel[$permissionLevelIndex].Permission[$permissionIndex]
                if ( $basePermission.Value -eq "Yes")
                {              
                    $editbasePermission += $basePermission.Name
                    $editbasePermission +=","
                }
            }
            
			$editbasePermission = $editbasePermission.Substring(0, ($editbasePermission.length - 1))
            
			Write-host ""
            Write-host -f Green "PermissionLevel: [" $editRoleDefinitionName "] will be updated with below permission string " $editbasePermission
			Write-host ""
            $editPermissionLevel.BasePermissions = $editbasePermission
            $editPermissionLevel.Update()
        }
        else
        {
            Write-host "No " + $permissionXML.Site.PermissionLevel[$permissionLevelIndex].Name + "Such Role Definition"
        }
    }
}

if ($web -ne $null)
{
	$web.Update() 
	$web.Dispose() 
}

Write-host "Update premission level on site $sitecollectionurl done" -f Green
Write-host ""
Write-host ""