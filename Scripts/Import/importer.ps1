clear
#param(
    $dataRoot = "C:\Users\gokulives\Downloads\Data\Deaths"
    #$dbUrl = "mongodb://localhost/trumptown?safe=true;slaveok=true"
    $dbUrl = "mongodb://MongoLab-2:OodroYtw5YXihgJPNlMlH79xc9KmMQ5nswjedkk73Xo-@ds045087.mongolab.com:45087/MongoLab-2"
    $dbCollection = "areas"
#)

Add-Type -Path "c:\mongodb\bin\MongoDB.Bson.dll"
Add-Type -Path "c:\mongodb\bin\MongoDB.Driver.dll"

$fileIdCols = @{
    "_rgn" = "GOR_CODE"
    "_ward" = "WARD_CODE"
    "_la" = "LA_CODE"
    "_cty" = "CTY_CODE"
    "_msoa" = "MSOA_CODE"
    "_lsoa" = "LSOA_CODE"
}

$regStub = $fileIdCols.Keys -join ')|('

Function Process-Files{

    gci $dataRoot -Recurse -Exclude parsed_* | 
        ? { $_.Name -match "($regStub)$"} |
        % { 
            Push-Location $_.Directory
            $content = Get-Content $_
            $hr = $content[2] -split ','
            $titleIndex = 0
            for($i = 0; $i -lt $hr.Length; $i++){
                if($hr[$i] -eq "HEADING"){
                    $titleIndex = $i+1
                    break
                }
            }
        
            $headings = $content[5] -split ','
            for($i = $titleIndex; $i -lt $hr.Length; $i++){
                $headings[$i] = $hr[$i]
            }
        
            $header =  $headings -join '","'
            $header = '"' + $header + '"'
            $parsedPath = "parsed_" + $_.Name
            $header | Set-Content $parsedPath
            $content | select -Skip 6 | Add-Content $parsedPath
            $_.FullName
            $idKey = $fileIdCols.Keys | ? { $parsedPath -match $_}
            Import-CsvToMongo (Resolve-Path $parsedPath) $fileIdCols[$idKey]
            Pop-Location
        }
}

Function Import-CsvToMongo{
    param($path, $matchCol)

    $db = [MongoDB.Driver.MongoDatabase]::Create($dbUrl)
    $collection = $db[$dbCollection]
    $sort = [MongoDB.Driver.Builders.SortBy]::Null

    Import-Csv $path | % {
        $q = [MongoDB.Driver.QueryDocument] @{ $matchCol = $_.$matchCol}
        $update = New-Object MongoDB.Driver.Builders.UpdateBuilder
        $i = $_
        $i | Get-Member -MemberType NoteProperty | % {
            $update.Set($_.Name, [MongoDB.Bson.BsonValue] $i.$($_.Name)) | out-null
        }

        $collection.FindAndModify($q, $sort, $update, $true, $true)
    }
}

Process-Files