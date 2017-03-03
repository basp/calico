# Yeah, we'll def need a better way to do this
# For now though, you'll just have to edit this to 
# the -ClientId context that you're working in.
$Script:ClientId = 1
Set-Alias Calico D:\dev\calico\Calico.Cmd\bin\Debug\calicmd.exe

function Get-Attributes {
	param([Int] $FeatureTypeId)
	Calico GetAttributes -FeatureTypeId $FeatureTypeId | ConvertFrom-Json
}

function Get-Clients {
    Calico GetClients | ConvertFrom-Json
}

function Get-DataTypes {
	Calico GetDataTypes | ConvertFrom-Json
}

function Get-FeatureTypes {
	Calico GetFeatureTypes -ClientId $Script:ClientId | ConvertFrom-Json
}

function Get-Plots {
	Calico GetPlots -ClientId $Script:ClientId | ConvertFrom-Json
}

function New-Client {
   
    param(
        [Parameter(Position = 0, Mandatory = $True)]
        [String] $Name)
    Calico NewClient -Name $Name
}

function Resolve-Shapefile {
    param(
        [Parameter(Position = 0, Mandatory = $True)]
        [String] $Path)
    Calico ScanShapefile -ClientId $Script:ClientId -PathToShapefile $Path | ConvertFrom-Json
}

function Resolve-Directory {
    Get-ChildItem *.shp | ForEach-Object { Resolve-Shapefile -Path $_.FullName }
}

