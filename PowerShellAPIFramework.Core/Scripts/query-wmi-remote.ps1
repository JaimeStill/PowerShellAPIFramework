[CmdletBinding()]
Param(
	[Parameter()]
	[string]$query,
	[Parameter()]
	[string[]]$properties,
	[Parameter()]
	[string]$computername,
	[Parameter()]
	[string]$wmiNamespace,
	[Parameter()]
	[PSCredential]$credential
)
$wmi = Get-WmiObject -ComputerName $computername -Namespace $wmiNamespace -Query $query -Credential $credential
$wmi | select $properties