Set-Alias Calico D:\dev\calico\Calico.Cmd\bin\Debug\calicmd.exe

$Script:ClientId = 1

function Get-Attributes {
	param([Int] $FeatureTypeId)
	Calico GetAttributes -FeatureTypeId $FeatureTypeId | ConvertFrom-Json
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

function Resolve-Shapefile {
    param(
        [Parameter(Position=0)]
        [String] $Path)
    Calico ScanShapefile -ClientId $Script:ClientId -PathToShapefile $Path | ConvertFrom-Json
}

function Resolve-Directory {
    Get-ChildItem *.shp | ForEach-Object { Resolve-Shapefile -Path $_.FullName }
}

