BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection2' | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Get-MongoDBCollection" -Tag $Module, "GetCollectionCmdlet", "Collection" {
 Context "Testing Parameters" {
  Context "CollectionName parameter" {
   It "Should be String" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionName -Type String
   }
   It "Should be Mandatory" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionName -not -Mandatory
   }
  }
 }
 Context "Get-MongoDBCollection Usage" {
  Context "Without a CollectionName" {
   It "Should Return MongoDB.Driver.MongoCollectionImpl`1[MongoDB.Bson.BsonDocument]" {
    (Get-MongoDBCollection).GetType().FullName | Should -Be 'System.Object[]'
   }
  }
  Context "With a CollectionName" {
   It "Should Return MongoDB.Driver.IMongoCollection" {
    (Get-MongoDBCollection -CollectionName 'myCollection').GetType().FullName | Should -Be 'MongoDB.Driver.MongoCollectionImpl`1[[MongoDB.Bson.BsonDocument, MongoDB.Bson, Version=2.18.0.0, Culture=neutral, PublicKeyToken=null]]'
   }
  }
  Context "With an invalid CollectionName" {
   It "Should throw an error: Database names must be non-empty and not contain '.' or the null character." {
    { Get-MongoDBCollection -CollectionName " " } | Should -Throw
   }
  }
 }
}