BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB1' | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB2' | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB1' | Out-Null
 Remove-MongoDBDatabase -DatabaseName 'MyDB2' | Out-Null
}
Describe "New-MongoDBCollection" -Tag $Module, "NewCollectionCmdlet", "Collection" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command New-MongoDBCollection | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "CollectionName ParameterSet" {
   It "CollectionName should be String" {
    Get-Command New-MongoDBCollection | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should not be Mandatory" {
    Get-Command New-MongoDBCollection | Should -HaveParameter CollectionName -Mandatory
   }
  }
  Context "DatabaseName ParameterSet" {
   It "CollectionName should be String" {
    Get-Command New-MongoDBCollection | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should  be Mandatory" {
    Get-Command New-MongoDBCollection | Should -HaveParameter CollectionName -Mandatory
   }
   It "DatabaseName should be String" {
    Get-Command New-MongoDBCollection | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    Get-Command New-MongoDBCollection | Should -HaveParameter DatabaseName -Mandatory
   }
  }
  Context "Database ParameterSet" {
   It "CollectionName should be String" {
    Get-Command New-MongoDBCollection | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should  be Mandatory" {
    Get-Command New-MongoDBCollection | Should -HaveParameter CollectionName -Mandatory
   }
   It "MongoDatabase should be MongoDatabase" {
    Get-Command New-MongoDBCollection | Should -HaveParameter MongoDatabase -Type MongoDB.Driver.MongoDatabaseBase
   }
   It "MongoDatabase should be Mandatory" {
    Get-Command New-MongoDBCollection | Should -HaveParameter MongoDatabase -Mandatory
   }
  }
 }
 Context "New-MongoDBCollection Usage" {
  Context "New-MongoDBCollection CollectionName ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     (New-MongoDBCollection -CollectionName 'myCollection1').GetType().FullName | Should -Be 'MongoDB.Driver.MongoCollectionImpl`1[[MongoDB.Bson.BsonDocument, MongoDB.Bson, Version=2.18.0.0, Culture=neutral, PublicKeyToken=null]]'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: MissingArgument" {
     { New-MongoDBCollection -CollectionName } | Should -Throw -ErrorId 'MissingArgument,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With an empty CollectionName" {
    It "Should throw an error: MissingArgument" {
     { New-MongoDBCollection -CollectionName '' } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With a null CollectionName" {
    It "Should throw an error: ParameterArgumentValidationErrorNullNotAllowed" {
     { New-MongoDBCollection -CollectionName $null } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With an invalid CollectionName" {
    It "Should throw an error: InvalidNamespace" {
     { New-MongoDBCollection -CollectionName 'this$collection' } | Should -Throw -ErrorId 'MongoDB.Driver.MongoCommandException,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
  }
  Context "New-MongoDBCollection DatabaseName ParameterSet" {
   Context "With a DatabaseName" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     (New-MongoDBCollection -DatabaseName 'MyDB1' -CollectionName 'myCollection2').GetType().FullName | Should -Be 'MongoDB.Driver.MongoCollectionImpl`1[[MongoDB.Bson.BsonDocument, MongoDB.Bson, Version=2.18.0.0, Culture=neutral, PublicKeyToken=null]]'
    }
   }
   Context "Without a DatabaseName" {
    It "Should throw an error: MissingArgument" {
     { New-MongoDBCollection -DatabaseName -CollectionName 'myCollection' } | Should -Throw -ErrorId 'MissingArgument,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With an empty DatabaseName" {
    It "Should throw an error: MissingArgument" {
     { New-MongoDBCollection -DatabaseName '' -CollectionName 'myCollection' } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With a null DatabaseName" {
    It "Should throw an error: ParameterArgumentValidationErrorNullNotAllowed" {
     { New-MongoDBCollection -DatabaseName $null -CollectionName 'myCollection' } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With an invalid DatabaseName" {
    It "Should throw an error: InvalidNamespace" {
     { New-MongoDBCollection -DatabaseName 'My$DB' -CollectionName 'myCollection' } | Should -Throw -ErrorId 'MongoDB.Driver.MongoCommandException,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
  }
  Context "New-MongoDBCollection DatabaseName ParameterSet" {
   Context "With a Database" {
    It "Should Return MongoDB.Driver.IMongoCollection" {
     (New-MongoDBCollection -MongoDatabase $Database -CollectionName 'myCollection3').GetType().FullName | Should -Be 'MongoDB.Driver.MongoCollectionImpl`1[[MongoDB.Bson.BsonDocument, MongoDB.Bson, Version=2.18.0.0, Culture=neutral, PublicKeyToken=null]]'
    }
   }
   Context "Without a DatabaseName" {
    It "Should throw an error: MissingArgument" {
     { New-MongoDBCollection -MongoDatabase -CollectionName 'myCollection' } | Should -Throw -ErrorId 'MissingArgument,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With an empty DatabaseName" {
    It "Should throw an error: CannotConvertArgumentNoMessage" {
     { New-MongoDBCollection -MongoDatabase '' -CollectionName 'myCollection' } | Should -Throw -ErrorId 'CannotConvertArgumentNoMessage,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
   Context "With a null DatabaseName" {
    It "Should throw an error: ParameterArgumentValidationErrorNullNotAllowed" {
     { New-MongoDBCollection -MongoDatabase $null -CollectionName 'myCollection' } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Collection.NewCollectionCmdlet'
    }
   }
  }
 }
}