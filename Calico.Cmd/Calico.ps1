Set-Alias Calico D:\dev\calico\Calico.Cmd\bin\Debug\calicmd.exe

$Script:ClientId = 1

function Set-ClientId {
    param([Int] $ClientId)
    $Script:ClientId = $ClientId
}

function Resolve-Shapefile {
    param(
        [Parameter(Position=0, ValueFromPipeline=$True)]
        [String] $Path)
    Calico ScanShapefile -ClientId $Script:ClientId -PathToShapefile $Path | ConvertFrom-Json
}

function Resolve-Directory {
    Get-ChildItem *.shp | ForEach-Object { Resolve-Shapefile -Path $_.FullName }
}

