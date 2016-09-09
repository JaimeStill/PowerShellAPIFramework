[CmdletBinding()]
Param(
	[Parameter()]
	[string]$query,
	[Parameter()]
	[string[]]$properties,
	[Parameter()]
	[string]$computername,
	[Parameter()]
	[string]$wmiNamespace
)
$wmi = Get-WmiObject -ComputerName $computername -Namespace $wmiNamespace -Query $query
$wmi | select $properties