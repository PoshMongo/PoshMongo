BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 $Collection1 = New-MongoDBCollection -CollectionName 'myCollection1' -DatabaseName 'MyDB'
 $Collection2 = New-MongoDBCollection -CollectionName 'myCollection2' -DatabaseName 'MyDB'
 Add-MongoDBDocument -Document '{"_id":"19511","FirstName":"Joe","LastName":"Friday"}' -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19512","FirstName":"Ben","LastName":"Romero"}' -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19751","FirstName":"David","LastName":"Starsky"}' -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19752","FirstName":"Kenneth","Hutchinson":"Friday"}' -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19791","Bo":"Joe","LastName":"Duke"}' -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19792","Luke":"Joe","LastName":"Duke"}' -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19811","FirstName":"Rick","LastName":"Simon"}' -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19812","FirstName":"Andrew","LastName":"Simon"}' -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Remove-MongoDBDocument" -Tag "PoshMongo", "RemoveDocumentCmdlet", "Document" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Remove-MongoDBDocument | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "CollectionNameId ParameterSet" {
   It "ParameterSet should contain, DocumentId, CollectionName, DatabaseName" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.CollectionNameId | Should -Be $true
   }
   It "DocumentId should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionNameId.IsMandatory | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameId.IsMandatory | Should -Be $true
   }
   It "DatabaseName should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.CollectionNameId.IsMandatory | Should -Be $true
   }
  }
  Context "CollectionNameFilter ParameterSet" {
   It "ParameterSet should contain, Filter, CollectionName, DatabaseName" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionNameFilter | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameFilter | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.CollectionNameFilter | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameFilter.IsMandatory | Should -Be $true
   }
   It "Filter should be Hashtable" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter Filter -Type Hashtable
   }
   It "Filter should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionNameFilter.IsMandatory | Should -Be $true
   }
   It "DatabaseName should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.CollectionNameFilter.IsMandatory | Should -Be $true
   }
  }
  Context "CollectionId ParameterSet" {
   It "ParameterSet should contain, MongoCollection, DocumentId" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionId | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionId | Should -Be $true
   }
   It "MongoCollection should be IMongoCollection" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "MongoCollection should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionId.IsMandatory | Should -Be $true
   }
   It "DocumentId should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionId.IsMandatory | Should -Be $true
   }
  }
  Context "CollectionFilter ParameterSet" {
   It "ParameterSet should contain, MongoCollection, Filter" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionFilter | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionFilter | Should -Be $true
   }
   It "MongoCollection should be IMongoCollection" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "MongoCollection should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionFilter.IsMandatory | Should -Be $true
   }
   It "Filter should be Hashtable" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter Filter -Type Hashtable
   }
   It "Filter should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionFilter.IsMandatory | Should -Be $true
   }
  }
  Context "DocumentCollection ParameterSet" {
   It "ParameterSet should contain, Document, MongoCollection" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Document'].ParameterSets.DocumentCollection | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.DocumentCollection | Should -Be $true
   }
   It "Document should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter Document -Type String
   }
   It "Document should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Document'].ParameterSets.DocumentCollection.IsMandatory | Should -Be $true
   }
   It "MongoCollection should be IMongoCollection" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "MongoCollection should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.DocumentCollection.IsMandatory | Should -Be $true
   }
  }
  Context "DocumentCollectionName ParameterSet" {
   It "ParameterSet should contain, Document, CollectionName, DatabaseName" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Document'].ParameterSets.DocumentCollectionName | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.DocumentCollectionName | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.DocumentCollectionName | Should -Be $true
   }
   It "Document should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter Document -Type String
   }
   It "Document should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Document'].ParameterSets.DocumentCollectionName.IsMandatory | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.DocumentCollectionName.IsMandatory | Should -Be $true
   }
   It "DatabaseName should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.DocumentCollectionName.IsMandatory | Should -Be $true
   }
  }
 }
 Context "Remove-MongoDBDocument Usage" {
  Context "Remove-MongoDBDocument CollectionNameId ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -DocumentId '19791' -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Should -Be $null
    }
   }
   Context "With a null CollectionName" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -DocumentId '19791' -CollectionName $null -DatabaseName 'MyDB' } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId -CollectionName  -DatabaseName } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument CollectionNameFilter ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -Filter @{'_id' = '19511' } -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Should -Be $null
    }
   }
   Context "With a null Filter" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -Filter @{'_id' = '19511' } -CollectionName  $null  -DatabaseName 'MyDB'} | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId -CollectionName  -DatabaseName } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument CollectionId ParameterSet" {
   Context "With a MongoCollection" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -DocumentId '19791' -MongoCollection $Collection1 | Should -Be $null
    }
   }
   Context "With a null MongoCollection" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -DocumentId '19791' -MongoCollection $null } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a MongoCollection" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId -MongoCollection } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument CollectionNameFilter ParameterSet" {
   Context "With a MongoCollection" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -Filter @{'_id' = '19511' }  -MongoCollection $Collection1| Should -Be $null
    }
   }
   Context "With a null Filter" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -Filter @{'_id' = '19511' } -MongoCollection  $null } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a MongoCollection" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId -MongoCollection } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument DocumentCollectionName ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.String" {
     '{"_id":"19511","FirstName":"Joe","LastName":"Friday"}' | Remove-MongoDBDocument -CollectionName 'myCollection1' -DatabaseName 'MyDB' | Should -Be $null
    }
   }
   Context "With a null Filter" {
    It "Should throw an error: ParameterBindingValidationException" {
     { '{"_id":"19511","FirstName":"Joe","LastName":"Friday"}' | Remove-MongoDBDocument -CollectionName  $null -DatabaseName 'MyDB' } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: MissingArgument" {
     { '{"_id":"19511","FirstName":"Joe","LastName":"Friday"}' | Remove-MongoDBDocument -CollectionName -DatabaseName  } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument DocumentCollection ParameterSet" {
   Context "With a MongoCollection" {
    It "Should Return System.String" {
     '{"_id":"19791","Bo":"Joe","LastName":"Duke"}' | Remove-MongoDBDocument -MongoCollection $Collection1 | Should -Be $null
    }
   }
   Context "With a null Filter" {
    It "Should throw an error: ParameterBindingValidationException" {
     { '{"_id":"19791","Bo":"Joe","LastName":"Duke"}' | Remove-MongoDBDocument -MongoCollection  $null } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a MongoCollection" {
    It "Should throw an error: MissingArgument" {
     { '{"_id":"19791","Bo":"Joe","LastName":"Duke"}' | Remove-MongoDBDocument -MongoCollection } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
 }
}