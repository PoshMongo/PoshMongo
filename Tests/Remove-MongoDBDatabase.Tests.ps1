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
Describe "Remove-MongoDBDatabase" -Tag $Module, "RemoveDatabaseCmdlet", "Database" {
 Context "Testing Parameters" {
  Context "DatabaseName parameter" {
   It "Should be String" {
    Get-Command Remove-MongoDBDatabase | Should -HaveParameter DatabaseName -Type String
   }
   It "Should be Mandatory" {
    Get-Command Remove-MongoDBDatabase | Should -HaveParameter DatabaseName -Mandatory
   }
  }
  Context "Client parameter" {
   It "Should be MongoClient" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Type MongoDB.Driver.MongoClient
   }
   It "Should be Mandatory" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Not -Mandatory
   }
  }
 }
 Context "Without a DatabaseName" {
  It "Should throw an error: Cannot bind argument to parameter 'DatabaseName' because it is null." {
   { Remove-MongoDBDatabase -DatabaseName $null } | Should -Throw
  }
 }
 Context "Remove-MongoDBDatabase Usage" {
  Context "With a DatabaseName" {
   It "Should Return MongoDB.Driver.MongoDatabaseBase" {
    Remove-MongoDBDatabase -DatabaseName 'MyDB' | Should -BeOfType MongoDB.Driver.MongoDatabaseBase
   }
  }
  Context "With an invalid DatabaseName" {
   It "Should throw an error: Database names must be non-empty and not contain '.' or the null character." {
    { Remove-MongoDBDatabase -DatabaseName "my.db" } | Should -Throw
   }
  }
  Context "With an invalid MongoClient" {
   It "Should throw an error: Must be connected to a MongoDB instance." {
    { Get-MongoDBDatabase -DatabaseName "MyDB" -Client (New-Object -TypeName MongoDB.Driver.MongoClient) } | Should -Throw "Must be connected to a MongoDB instance."
   }
  }
 }
}