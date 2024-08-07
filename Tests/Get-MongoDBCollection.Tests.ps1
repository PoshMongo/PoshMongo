BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Get-MongoDBCollection" -Tag "PoshMongo", "GetCollectionCmdlet", "Collection" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Get-MongoDBCollection | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "CollectionNamespace ParameterSet" {
   It "CollectionNamespace should be String" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionNamespace -Type String
   }
   It "CollectionNamespace should be Mandatory" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionNamespace -Mandatory
   }
  }
  Context "DatabaseName ParameterSet" {
   It "CollectionName should be String" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should not be Mandatory" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionName -not -Mandatory
   }
   It "DatabaseName should be String" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter DatabaseName -Mandatory
   }
  }
  Context "Database ParameterSet" {
   It "CollectionName should be String" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should not be Mandatory" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter CollectionName -not -Mandatory
   }
   It "MongoDatabase should be IMongoDatabase" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter MongoDatabase -Type MongoDB.Driver.IMongoDatabase
   }
   It "MongoDatabase should be Mandatory" {
    Get-Command Get-MongoDBCollection | Should -HaveParameter MongoDatabase -Mandatory
   }
  }
 }
 Context "Get-MongoDBCollection Usage" {
  Context "Get-MongoDBCollection CollectionNameSpace ParameterSet" {
   Context "With a CollectionNamespace" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     (Get-MongoDBCollection -CollectionNamespace 'MyDB.myCollection').GetType().FullName | Should -Be 'MongoDB.Driver.MongoCollectionImpl`1[[MongoDB.Bson.BsonDocument, MongoDB.Bson, Version=2.27.0.0, Culture=neutral, PublicKeyToken=null]]'
    }
   }
   Context "Without a CollectionNamespace" {
    It "Should throw an error: Missing an argument for parameter 'CollectionNamespace'." {
     { Get-MongoDBCollection -CollectionNamespace '' } | Should -Throw "Cannot bind argument to parameter 'CollectionNamespace' because it is an empty string."
    }
   }
  }
  Context "Get-MongoDBCollection DatabaseName ParameterSet" {
   Context "With a DatabaseName" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     (Get-MongoDBCollection -DatabaseName 'MyDB').GetType().FullName | Should -Be 'System.Object[]'
    }
   }
   Context "With a DatabaseName and CollectionName" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     (Get-MongoDBCollection -DatabaseName 'MyDB' -CollectionName 'myCollection2').GetType().FullName | Should -Be 'MongoDB.Driver.MongoCollectionImpl`1[[MongoDB.Bson.BsonDocument, MongoDB.Bson, Version=2.27.0.0, Culture=neutral, PublicKeyToken=null]]'
    }
   }
   Context "Without a DatabaseName" {
    It "Should throw an error: Cannot bind argument to parameter 'DatabaseName'" {
     { Get-MongoDBCollection -DatabaseName '' -CollectionName 'myCollection2' } | Should -Throw "Cannot bind argument to parameter 'DatabaseName' because it is an empty string."
    }
   }
   Context "Without a CollectionName" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     (Get-MongoDBCollection -DatabaseName 'MyDB' -CollectionName '').GetType().FullName | Should -Be 'System.Object[]'
    }
   }
  }
  Context "Get-MongoDBCollection Database ParameterSet" {
   Context "With a Database" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     $Database = Get-MongoDBDatabase -DatabaseName 'MyDB';
     (Get-MongoDBCollection -MongoDatabase $Database).GetType().FullName | Should -Be 'System.Object[]'
    }
   }
   Context "With a Database and CollectionName" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     $Database = Get-MongoDBDatabase -DatabaseName 'MyDB';
     (Get-MongoDBCollection -MongoDatabase $Database -CollectionName 'myCollection2').GetType().FullName | Should -Be 'MongoDB.Driver.MongoCollectionImpl`1[[MongoDB.Bson.BsonDocument, MongoDB.Bson, Version=2.27.0.0, Culture=neutral, PublicKeyToken=null]]'
    }
   }
   Context "Without a Database" {
    It "Should throw and error: Cannot bind argument to parameter 'MongoDatabase'" {
     { Get-MongoDBCollection -MongoDatabase $null } | Should -Throw "Cannot bind argument to parameter 'MongoDatabase' because it is null."
    }
   }
   Context "Without a CollectionName" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     $Database = Get-MongoDBDatabase -DatabaseName 'MyDB';
     (Get-MongoDBCollection -MongoDatabase $Database -CollectionName '').GetType().FullName | Should -Be 'System.Object[]'
    }
   }
  }
 }
}