BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' -DatabaseName 'MyDB'  | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection2' -DatabaseName 'MyDB'  | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Get-MongoDBDatabase" -Tag "PoshMongo", "GetDatabaseCmdlet", "Database" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Get-MongoDBDatabase | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing Parameters" {
  Context "DatabaseName parameter" {
   It "Should be String" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter DatabaseName -Type String
   }
   It "Should be Mandatory" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter DatabaseName -not -Mandatory
   }
  }
  Context "Client parameter" {
   It "Should be IMongoClient" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Type MongoDB.Driver.IMongoClient
   }
   It "Should be Mandatory" {
    Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Not -Mandatory
   }
  }
 }
 Context "Get-MongoDBDatabase Usage" {
  Context "Without a DatabaseName" {
   It "Should Return MongoDB.Driver.MongoDatabaseBase" {
    (Get-MongoDBDatabase).GetType().FullName | Should -Be 'System.Collections.Generic.List`1[[MongoDB.Driver.IMongoDatabase, MongoDB.Driver, Version=2.18.0.0, Culture=neutral, PublicKeyToken=null]]'
   }
  }
  Context "With a DatabaseName" {
   It "Should Return MongoDB.Driver.MongoDatabaseBase" {
    Get-MongoDBDatabase -DatabaseName 'MyDB' | Should -BeOfType MongoDB.Driver.MongoDatabaseBase
   }
  }
  Context "With an invalid DatabaseName" {
   It "Should throw an error: Database names must be non-empty and not contain '.' or the null character." {
    { Get-MongoDBDatabase -DatabaseName "my.db" } | Should -Throw
   }
  }
  Context "With an invalid MongoClient" {
   It "Should throw an error: Must be connected to a MongoDB instance." {
    { Get-MongoDBDatabase -DatabaseName "mydb" -Client (New-Object -TypeName MongoDB.Driver.MongoClient) } | Should -Throw "Must be connected to a MongoDB instance."
   }
  }
 }
}