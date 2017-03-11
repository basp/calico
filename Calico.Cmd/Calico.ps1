$Script:ClientId = 1
Set-Alias Calico D:\dev\calico\Calico.Cmd\bin\Debug\calicmd.exe

function Get-Categories {
	param(
		[Parameter(Position = 0, Mandatory = $True)] $Path,
		[Parameter(Position = 1, Mandatory = $True)] [string] $Column)
	Calico CategorizeDataSet -PathToShapefile $Path -ColumnName $Column | ConvertFrom-Json
}

function Get-Classes {
	param(
		[Parameter(Position = 0, Mandatory = $True)] $Path,
		[Parameter(Position = 1, Mandatory = $True)] [string] $Column)
	Calico QuantifyDataSet -PathToShapefile $Path -ColumnName $Column -Normalize | ConvertFrom-Json | Format-List
}

function Remove-DataSet {
    param([Int] $Id)
    Calico DeleteDataSet -Id $Id
}

function Remove-FeatureType {
    param([Int] $Id)
    Calico DeleteFeatureType -Id $Id
}

function Remove-Plot {
    param([Int] $Id)
    Calico DeletePlot -Id $Id
}

function Get-Attributes {
	param([Int] $FeatureTypeId)
	Calico GetAttributes -FeatureTypeId $FeatureTypeId | ConvertFrom-Json
}

function Get-Clients {
    Calico GetClients | ConvertFrom-Json
}

function Get-DataSets {
    param([Int] $PlotId)
    Calico GetDataSets -PlotId $PlotId | ConvertFrom-Json
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

function Import-DataSet {
    param([Parameter(Position = 0, Mandatory = $True)] $Path)
    $Shapefile = Resolve-ShapeFile -Path $Path
    $Plot = ($Shapefile | Select-Object -Property Plots -First 1).Plots
    $FeatureType = ($Shapefile | Select-Object -Property FeatureTypes -First 1).FeatureTypes
    Calico ImportDataSet -PlotId $Plot.Id -FeatureTypeId $FeatureType.Id -PathToShapefile $Path     
}

function Import-FeatureType {
    param(
        [Parameter(Position = 0, Mandatory = $True)] $Path,
        [Parameter(Position = 1, Mandatory = $True)] [String] $Name)
    Calico ImportFeatureType -ClientId $Script:ClientId -PathToShapefile $Path -Name $Name
}

function Import-Plot {
    param([Parameter(Position = 0, Mandatory = $True)] $Path)
    $FeatureType = (Resolve-Shapefile -Path $Path | Select-Object -Property FeatureTypes -First 1).FeatureTypes
    Calico ImportPlot -ClientId $Script:ClientId -FeatureTypeId $FeatureType.Id -PathToShapefile $Path
}

function Resolve-Shapefile {
    param([Parameter(Position = 0, Mandatory = $True)] [String] $Path)
    Calico ScanShapefile -ClientId $Script:ClientId -PathToShapefile $Path | ConvertFrom-Json
}

function Resolve-Directory {
    Get-ChildItem *.shp | ForEach-Object { Resolve-Shapefile -Path $_.FullName }
}