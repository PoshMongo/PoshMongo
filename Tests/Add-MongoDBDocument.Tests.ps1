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
Describe "Add-MongoDBDocument" -Tag $Module, "AddDocumentCmdlet", "Document" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Get-MongoDBDocument | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "CollectionName ParameterSet" {
   It "ParameterSet should contain, Document, CollectionName" {
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['Document'].ParameterSets.CollectionName | Should -Be $true
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionName | Should -Be $true
   }
   It "Document should be String" {
    Get-Command Add-MongoDBDocument | Should -HaveParameter Document -Type String
   }
   It "Document should be Mandatory" {
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['Document'].ParameterSets.CollectionName.IsMandatory | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Add-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionName.IsMandatory | Should -Be $true
   }
  }
  Context "Collection ParameterSet" {
   It "ParameterSet should contain, Document, MongoCollection" {
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['Document'].ParameterSets.Collection | Should -Be $true
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.Collection | Should -Be $true
   }
   It "Document should be String" {
    Get-Command Add-MongoDBDocument | Should -HaveParameter Document -Type String
   }
   It "Document should be Mandatory" {
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['Document'].ParameterSets.CollectionName.IsMandatory | Should -Be $true
   }
   It "MongoCollection should be IMongoCollection" {
    Get-Command Add-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "MongoCollection should be Mandatory" {
    (Get-Command -Name 'Add-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.Collection.IsMandatory | Should -Be $true
   }
  }
 }
 Context "Add-MongoDBDocument Usage" {
  Context "CollectionName ParameterSet" {
   Context "With a Document" {
    It "Should Return System.String" {
     Add-MongoDBDocument -Document '{"_id":"123","FirstName":"Joe","LastName":"Friday"}' | Should -BeOfType System.String
    }
   }
   Context "With a Document and CollectionName" {
    It "Should Return System.String" {
     Add-MongoDBDocument -Document '{"_id":"456","FirstName":"Joe","LastName":"Friday"}' -CollectionName 'myCollection1' | Should -BeOfType System.String
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: ParameterArgumentValidationErrorEmptyStringNotAllowed" {
     { Add-MongoDBDocument -Document '{"_id":"789","FirstName":"Joe","LastName":"Friday"}' -CollectionName '' } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Document.AddDocumentCmdlet'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: ParameterArgumentValidationErrorNullNotAllowed" {
     { Add-MongoDBDocument -Document '{"_id":"101","FirstName":"Joe","LastName":"Friday"}' -CollectionName $null } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.AddDocumentCmdlet'
    }
   }
  }
  Context "Collection ParameterSet" {
   Context "With a Document" {
    It "Should Return System.String" {
     Add-MongoDBDocument -Document '{"_id":"112","FirstName":"Joe","LastName":"Friday"}' | Should -BeOfType System.String
    }
   }
   Context "With a Document and Collection" {
    It "Should Return System.String" {
     $Collection | Add-MongoDBDocument -Document '{"_id":"131","FirstName":"Joe","LastName":"Friday"}' | Should -BeOfType System.String
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: CannotConvertArgumentNoMessage" {
     { Add-MongoDBDocument -Document '{"_id":"415","FirstName":"Joe","LastName":"Friday"}' -MongoCollection '' } | Should -Throw -ErrorId 'CannotConvertArgumentNoMessage,PoshMongo.Document.AddDocumentCmdlet'
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: ParameterArgumentValidationErrorNullNotAllowed" {
     { Add-MongoDBDocument -Document '{"_id":"167","FirstName":"Joe","LastName":"Friday"}' -MongoCollection $null } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.AddDocumentCmdlet'
    }
   }
  }
 }
}