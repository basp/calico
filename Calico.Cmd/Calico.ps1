Set-Alias Calico D:\dev\calico\Calico.Cmd\bin\Debug\calicmd.exe

$Script:ClientId = 0

function Set-ClientId {
    param([Int] $ClientId)
    $Script:ClientId = $ClientId
}

function Resolve-Shapefile {
    param([String] $Path)
    Calico ScanShapefile -ClientId $Script:ClientId -PathToShapefile $Path | ConvertFrom-Json
}

function Import-DataSet {

}

function Import-Directory {
    Get-ChildItem *.shp | ForEach-Object { $_.FullName }

}