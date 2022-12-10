BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection3' -DatabaseName 'MyDB' | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Remove-MongoDBCollection" -Tag "PoshMongo", "RemoveCollectionCmdlet", "Collection" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Remove-MongoDBCollection | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "DatabaseName ParameterSet" {
   It "CollectionName should be String" {
    Get-Command Remove-MongoDBCollection | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    Get-Command Remove-MongoDBCollection | Should -HaveParameter CollectionName -Mandatory
   }
   It "DatabaseName should be String" {
    Get-Command Remove-MongoDBCollection | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    Get-Command Remove-MongoDBCollection | Should -HaveParameter DatabaseName -Mandatory
   }
  }
  Context "Collection ParameterSet" {
   It "Collection should be IMongoCollection" {
    Get-Command Remove-MongoDBCollection | Should -HaveParameter Collection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "Collection should be Mandatory" {
    Get-Command Remove-MongoDBCollection | Should -HaveParameter Collection -Mandatory
   }
  }
 }
 Context "Remove-MongoDBCollection Usage" {
  Context "Remove-MongoDBCollection DatabaseName ParameterSet" {
   Context "With a DatabaseName" {
    It "Should Return null" {
     Remove-MongoDBCollection -DatabaseName 'MyDB1' -CollectionName 'myCollection2' | Should -Be $null
    }
   }
   Context "Without a DatabaseName" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBCollection -DatabaseName -CollectionName 'myCollection' } | Should -Throw -ErrorId 'MissingArgument,PoshMongo.Collection.RemoveCollectionCmdlet'
    }
   }
   Context "With an empty DatabaseName" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBCollection -DatabaseName '' -CollectionName 'myCollection' } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Collection.RemoveCollectionCmdlet'
    }
   }
   Context "With a null DatabaseName" {
    It "Should throw an error: ParameterArgumentValidationErrorNullNotAllowed" {
     { Remove-MongoDBCollection -DatabaseName $null -CollectionName 'myCollection' } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Collection.RemoveCollectionCmdlet'
    }
   }
  }
  Context "Remove-MongoDBCollection Collection ParameterSet" {
   Context "With a Collection" {
    It "Should Return null" {
     Remove-MongoDBCollection -Collection (Get-MongoDBCollection -CollectionName 'myCollection3' -DatabaseName 'MyDB') | Should -Be $null
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBCollection -Collection } | Should -Throw -ErrorId 'MissingArgument,PoshMongo.Collection.RemoveCollectionCmdlet'
    }
   }
   Context "With an empty Collection" {
    It "Should throw an error: CannotConvertArgumentNoMessage" {
     { Remove-MongoDBCollection -Collection '' } | Should -Throw -ErrorId 'CannotConvertArgumentNoMessage,PoshMongo.Collection.RemoveCollectionCmdlet'
    }
   }
   Context "With a null Collection" {
    It "Should throw an error: ParameterArgumentValidationErrorNullNotAllowed" {
     { Remove-MongoDBCollection -Collection $null } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Collection.RemoveCollectionCmdlet'
    }
   }
  }
 }
}