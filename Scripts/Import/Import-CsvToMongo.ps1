Function Import-CsvToMongo{
    param($path, $dbUrl, $dbCollection)

    Add-Type -Path "c:\mongodb\bin\MongoDB.Bson.dll"
    Add-Type -Path "c:\mongodb\bin\MongoDB.Driver.dll"

    $db = [MongoDB.Driver.MongoDatabase]::Create("mongodb://" + $dbUrl +"?safe=true;slaveok=true")
    $collection = $db[$dbCollection]

    Import-Csv $path | % {
        $i = $_; 
        [MongoDB.Bson.BsonDocument] $doc = @{
            "_id"= [MongoDB.Bson.ObjectId]::GenerateNewId()
        }
    
        $i | Get-Member -MemberType NoteProperty | % {
            $doc.Add($_.Name, [MongoDB.Bson.BsonValue] $i.$($_.Name))
        }

        $collection.Insert($doc)
    }
}

Import-CsvToMongo -path "C:\Users\gokulives\Downloads\Data\Population Density 2011\Administrative Hierarchy\clean_QS102EW_2671_2011Admin_WARD.csv" `
    -dbUrl "localhost/trumptown" `
    -dbCollection "areas" `
    -idCol "ward_code"
