BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"1","LastName":"Smith","FirstName":"John"}' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection2' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"1","LastName":"Smith","FirstName":"John"}' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"2","LastName":"Doe","FirstName":"Jane"}' | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Get-MongoDBDocument" -Tag $Module, "GetDocumentCmdlet", "Document" {
 Context "Testing ParameterSets" {
  Context "DocumentId ParameterSet" {
   It "DocumentId should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should not be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Not -Mandatory
   }
  }
  Context "CollectionName ParameterSet" {
   It "CollectionName should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Mandatory
   }
   It "DocumentId should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should not be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Not -Mandatory
   }
  }
  Context "Collection ParameterSet" {
   It "Collection should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "Collection should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter MongoCollection -Mandatory
   }
   It "DocumentId should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should not be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Not -Mandatory
   }
  }
 }
 Context "Get-MongoDBDocument Usage" {
  Context "Get-MongoDBDocument DocumentId ParameterSet" {
   Context "Without a DocumentId" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "With a DocumentId" {
    It "Should Return System.String" {
     Get-MongoDBDocument -DocumentId '1' | Should -BeOfType System.String
    }
   }
   Context "With an invalid DocumentId" {
    It "Should throw an error: Value cannot be null. (Parameter 'source')." {
     { Get-MongoDBDocument -DocumentId '3' } | Should -Throw "Value cannot be null. (Parameter 'source')"
    }
   }
  }
  Context "Get-MongoDBDocument CollectionName ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -CollectionName 'myCollection2' | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "With a CollectionName and DocumentId" {
    It "Should Return System.String" {
     Get-MongoDBDocument -CollectionName 'myCollection2' -DocumentId '1' | Should -BeOfType System.String
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: Value cannot be null. Cannot bind argument to parameter 'CollectionName' because it is an empty string." {
     { Get-MongoDBDocument -CollectionName '' -DocumentId '1' } | Should -Throw "Cannot bind argument to parameter 'CollectionName' because it is an empty string."
    }
   }
  }
  Context "Get-MongoDBDocument Collection ParameterSet" {
   Context "With a Collection" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -MongoCollection $Collection | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "With a Collection and DocumentId" {
    It "Should Return System.String" {
     Get-MongoDBDocument -MongoCollection $Collection -DocumentId '1' | Should -BeOfType System.String
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: Value cannot be null." {
     { Get-MongoDBDocument -MongoCollection $null -DocumentId '1' } | Should -Throw "Cannot bind argument to parameter 'MongoCollection' because it is null."
    }
   }
  }
 }
}