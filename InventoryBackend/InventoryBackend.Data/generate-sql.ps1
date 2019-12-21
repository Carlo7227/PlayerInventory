param (
    [string]$filename = ""
)

if ($filename -eq "") {
@'
    usage: ./generate-table.ps1 filename'
    example: ./generate-table.ps1 ./data/global/excel/armor.txt
'@
    return
}

$tableName = """{0}""" -f ([io.fileinfo]$filename).basename;

$line = Get-Content $filename -First 1
$headers = $line -split "`t"
#$headers

$names = @{}

$formattedHeaders = $headers | % { 
    $name = $_
    $first = $True
    if ($names.ContainsKey($name)) {
        continue
    }
    $names[$name] = 0
    "`t""{0}"" TEXT" -f $name 
}
$columnDefinitions = $formattedHeaders -join ",`n"


$tableDefinition = 
@'
CREATE TABLE {0} (
{1}
)
'@ -f $tableName, $columnDefinitions

$tableDefinition