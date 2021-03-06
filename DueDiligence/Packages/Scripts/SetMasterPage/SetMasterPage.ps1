function set-masterpage([object]$spweb)
{
    foreach($web in $spweb.Webs)
    {   
        if($web.Title -ne "Search" -and $web -ne $null -and ($web.WebTemplate -ne "BLOG") )
        {
    	   #$web = Get-SPWeb $subSite
           $masterurl="/_catalogs/masterpage/" + $masterpagename
           $web.CustomMasterUrl =  $masterurl
           $web.Update()
           Write-host "`"$masterpagename`" was set for site: $web " -ForegroundColor Green
           if($web.Webs -ne $null)
           {
                set-masterpage $web
           }
        }

		if ($web -ne $null)
		{
			$web.Dispose();
		}
    }
    
}
$url = $sitecollectionurl
$masterpagename="PublishingSolution.master"
$spsite = Get-SPsite $url
$spweb=$spsite.rootweb


$masterurl="/_catalogs/masterpage/" + $masterpagename
$spweb.CustomMasterUrl =  $masterurl
$spweb.Update()
Write-host "`"$masterpagename`" was set for site: $spweb " -ForegroundColor Green
if($spweb -ne $null)
{
    set-masterpage $spweb
    $spWeb.Dispose()
}





