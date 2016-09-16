[CmdletBinding()]
Param(
	[Parameter()]
	[string]$query,
	[Parameter()]
	[string]$computername,
	[Parameter()]
	[string]$wmiNamespace,
	[Parameter()]
	[PSCredential]$credential
)
$wmi = Get-WmiObject -ComputerName $computername -Namespace $wmiNamespace -Query $query -Credential $credential
$wmi | select * -excludeproperty "_*", "PSComputerName", "Scope", "Path", "Options", "ClassPath", "Properties", "SystemProperties", "Qualifiers", "Site", "Container"