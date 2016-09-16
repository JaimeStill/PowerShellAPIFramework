[CmdletBinding()]
Param(
	[Parameter()]
	[string]$query,
	[Parameter()]
	[string]$computername,
	[Parameter()]
	[string]$wmiNamespace
)
$wmi = Get-WmiObject -ComputerName $computername -Namespace $wmiNamespace -Query $query
$wmi | select * -excludeproperty "_*", "PSComputerName", "Scope", "Path", "Options", "ClassPath", "Properties", "SystemProperties", "Qualifiers", "Site", "Container"