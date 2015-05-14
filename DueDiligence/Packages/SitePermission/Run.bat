Powershell.exe -Command Set-ExecutionPolicy "Bypass"
Powershell.exe -Command "& {%~dp0UpdateSitePermission.ps1}"
Pause