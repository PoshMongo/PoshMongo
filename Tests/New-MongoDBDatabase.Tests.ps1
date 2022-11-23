BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "New-MongoDBDatabase" -Tag $Module, "NewDatabaseCmdlet", "Database" {
 Context "Testing Parameters" {
  Context "DatabaseName parameter" {
   It "Should be String" {
    Get-Command New-MongoDBDatabase | Should -HaveParameter DatabaseName -Type String
   }
   It "Should be Mandatory" {
    Get-Command New-MongoDBDatabase | Should -HaveParameter DatabaseName -Mandatory
   }
  }
 }
 Context "Without a DatabaseName" {
  It "Should throw an error: Cannot bind argument to parameter 'DatabaseName' because it is null." {
   { New-MongoDBDatabase -DatabaseName $null } | Should -Throw
  }
 }
 Context "New-MongoDBDatabase Usage" {
  Context "With a DatabaseName" {
   It "Should Return MongoDB.Driver.MongoDatabaseBase" {
    New-MongoDBDatabase -DatabaseName 'MyDB' | Should -BeOfType MongoDB.Driver.MongoDatabaseBase
   }
  }
  Context "With an invalid DatabaseName" {
   It "Should throw an error: Database names must be non-empty and not contain '.' or the null character." {
    { New-MongoDBDatabase -DatabaseName "my.db" } | Should -Throw
   }
  }
 }
}