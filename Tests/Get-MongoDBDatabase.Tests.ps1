BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
}
Describe "Get-MongoDBDatabase" -Tag $Module, "GetDatabaseCmdlet", "Database" {
 Context "Testing Parameters" {
  Context "DatabaseName parameter" {
   It "Should be String" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter DatabaseName -Type String
   }
   It "Should be Mandatory" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter DatabaseName -not -Mandatory
   }
  }
 }
 Context "Get-MongoDBDatabase Usage" {
  Context "With a DatabaseName" {
   It "Should Return MongoDB.Driver.MongoDatabaseBase" {
    $dbName = (Get-MongoDBDatabase)[0].DatabaseNamespace.DatabaseName;
    Get-MongoDBDatabase -DatabaseName $dbName | Should -BeOfType MongoDB.Driver.MongoDatabaseBase
   }
  }
  Context "Without a DatabaseName" {
   It "Should Return MongoDB.Driver.MongoDatabaseBase" {
    Get-MongoDBDatabase | Should -BeOfType MongoDB.Driver.MongoDatabaseBase
   }
  }
  Context "With an invalid DatabaseName" {
   It "Should throw an error: Database names must be non-empty and not contain '.' or the null character." {
    { Get-MongoDBDatabase -DatabaseName "my.db" } | Should -Throw
   }
  }
 }
}