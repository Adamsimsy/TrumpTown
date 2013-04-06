Add-Type -Path "c:\mongodb\bin\MongoDB.Bson.dll"
Add-Type -Path "c:\mongodb\bin\MongoDB.Driver.dll"

Function Import-CsvToMongo{
    param($path, $dbUrl, $dbCollection, $matchCol)

    $db = [MongoDB.Driver.MongoDatabase]::Create("mongodb://" + $dbUrl +"?safe=true;slaveok=true")
    $collection = $db[$dbCollection]
    $sort = [MongoDB.Driver.Builders.SortBy]::Null

    Import-Csv $path | % {
        $q = [MongoDB.Driver.QueryDocument] @{ $matchCol = $_.$matchCol}
        $update = New-Object MongoDB.Driver.Builders.UpdateBuilder
        $i = $_
        $i | Get-Member -MemberType NoteProperty | % {
            $update.Set($_.Name, [MongoDB.Bson.BsonValue] $i.$($_.Name))
        }

        $collection.FindAndModify($q, $sort, $update, $true, $true)
    }
}


Import-CsvToMongo -path "C:\Users\gokulives\Downloads\Data\Population Density 2011\Administrative Hierarchy\clean_QS102EW_2671_2011Admin_WARD.csv" `
    -dbUrl "localhost/trumptown" `
    -dbCollection "areas" `
    -matchCol "WARD_CODE"