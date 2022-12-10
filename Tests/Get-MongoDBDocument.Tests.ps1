BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 $Collection1 = New-MongoDBCollection -CollectionName 'myCollection1' -DatabaseName 'MyDB'
 Add-MongoDBDocument -Document '{"_id":"1","LastName":"Smith","FirstName":"John"}' -MongoCollection $Collection1 | Out-Null
 $Collection2 = New-MongoDBCollection -CollectionName 'myCollection2' -DatabaseName 'MyDB'
 Add-MongoDBDocument -Document '{"_id":"1","LastName":"Smith","FirstName":"John"}' -MongoCollection $Collection2 | Out-Null
 Add-MongoDBDocument -Document '{"_id":"2","LastName":"Doe","FirstName":"Jane"}' -MongoCollection $Collection2 | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Get-MongoDBDocument" -Tag "PoshMongo", "GetDocumentCmdlet", "Document" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Get-MongoDBDocument | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "CollectionNameId ParameterSet" {
   It "ParameterSet should contain, DocumentId, CollectionName, DatabaseName, HideId" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameId | Should -Be $true
   }
   It "DocumentId should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionNameId.IsMandatory | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Mandatory
   }
   It "DatabaseName should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DatabaseName -Mandatory
   }
   It "HideId should be a SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter HideId -Type System.Management.Automation.SwitchParameter
   }
   It "HideId should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameId.IsMandatory | Should -Not -Be $true
   }
  }
  Context "CollectionNameFilter ParameterSet" {
   It "ParameterSet should contain, CollectionName, Filter, HideId" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionNameFilter | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameFilter | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameFilter | Should -Be $true
   }
   It "Filter should be a HashTable" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter Filter -Type Hashtable
   }
   It "Filter should be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionNameFilter.IsMandatory | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Mandatory
   }
   It "DatabaseName should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DatabaseName -Mandatory
   }
   It "HideId should be a SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter HideId -Type System.Management.Automation.SwitchParameter
   }
   It "HideId should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameFilter.IsMandatory | Should -Not -Be $true
   }
  }
  Context "CollectionNameList ParameterSet" {
   It "ParameterSet should contain, CollectionName, List, HideId" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameList | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['DatabaseName'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['List'].ParameterSets.CollectionNameList | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameList | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter CollectionName -Mandatory
   }
   It "DatabaseName should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DatabaseName -Type String
   }
   It "DatabaseName should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DatabaseName -Mandatory
   }
   It "List should be SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter List -Type System.Management.Automation.SwitchParameter
   }
   It "List should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['List'].ParameterSets.CollectionNameList.IsMandatory | Should -Not -Be $true
   }
   It "HideId should be a SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter HideId -Type System.Management.Automation.SwitchParameter
   }
   It "HideId should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameList.IsMandatory | Should -Not -Be $true
   }
  }
  Context "CollectionId ParameterSet" {
   It "ParameterSet should contain, MongoCollection, DocumentId, HideId" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionId | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionId | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionId | Should -Be $true
   }
   It "Collection should be IMongoCollection" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "Collection should be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionId.IsMandatory | Should -Be $true
   }
   It "DocumentId should be String" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionId.IsMandatory | Should -Be $true
   }
   It "HideId should be a SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter HideId -Type System.Management.Automation.SwitchParameter
   }
   It "HideId should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameId.IsMandatory | Should -Not -Be $true
   }
  }
  Context "CollectionFilter ParameterSet" {
   It "ParameterSet should contain, MongoCollection, Filter, HideId" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionFilter | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionFilter | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionFilter | Should -Be $true
   }
   It "Collection should be IMongoCollection" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "Collection should be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionFilter.IsMandatory | Should -Be $true
   }
   It "Filter should be Hashtable" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter Filter -Type Hashtable
   }
   It "Filter should be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionFilter.IsMandatory | Should -Be $true
   }
   It "HideId should be a SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter HideId -Type System.Management.Automation.SwitchParameter
   }
   It "HideId should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionNameId.IsMandatory | Should -Not -Be $true
   }
  }
  Context "CollectionList ParameterSet" {
   It "ParameterSet should contain, MongoCollection, List, HideId" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['MongoCollection'].ParameterSets.CollectionList | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['List'].ParameterSets.CollectionList | Should -Be $true
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionList | Should -Be $true
   }
   It "MongoCollection should be IMongoCollection" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter MongoCollection -Type 'MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]'
   }
   It "MongoCollection should be Mandatory" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter MongoCollection -Mandatory
   }
   It "List should be SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter List -Type System.Management.Automation.SwitchParameter
   }
   It "List should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['List'].ParameterSets.CollectionList.IsMandatory | Should -Not -Be $true
   }
   It "HideId should be a SwitchParameter" {
    Get-Command Get-MongoDBDocument | Should -HaveParameter HideId -Type System.Management.Automation.SwitchParameter
   }
   It "HideId should not be Mandatory" {
    (Get-Command -Name 'Get-MongoDBDocument').Parameters['HideId'].ParameterSets.CollectionList.IsMandatory | Should -Not -Be $true
   }
  }
 }
 Context "Get-MongoDBDocument Usage" {
  Context "Get-MongoDBDocument CollectionNameList ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: Missing an argument for parameter 'CollectionName'" {
     { Get-MongoDBDocument -CollectionName  -DatabaseName 'MyDB' } | Should -Throw "Missing an argument for parameter 'CollectionName'. Specify a parameter of type 'System.String' and try again."
    }
   }
  }
  Context "Get-MongoDBDocument CollectionList ParameterSet" {
   Context "With a Collection" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -MongoCollection $Collection1 | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: Missing an argument for parameter 'MongoCollection'" {
     { Get-MongoDBDocument -MongoCollection } | Should -Throw -ErrorId "MissingArgument,PoshMongo.Document.GetDocumentCmdlet"
    }
   }
  }
  Context "Get-MongoDBDocument CollectionNameId ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "With a CollectionName and DocumentId" {
    It "Should Return System.String" {
     Get-MongoDBDocument -DocumentId '1' -CollectionName 'myCollection2' -DatabaseName 'MyDB' | Should -BeOfType System.String
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: Value cannot be null. Cannot bind argument to parameter 'CollectionName' because it is an empty string." {
     { Get-MongoDBDocument -DocumentId '1' -CollectionName '' -DatabaseName 'MyDB' } | Should -Throw "Cannot bind argument to parameter 'CollectionName' because it is an empty string."
    }
   }
  }
  Context "Get-MongoDBDocument CollectionNameFilter ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -CollectionName 'myCollection2' -Database 'MyDB' | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "With a CollectionName and Filter" {
    It "Should Return System.String" {
     Get-MongoDBDocument -Filter @{'_id' = '1' } -CollectionName 'myCollection2' -Database 'MyDB'| Should -BeOfType System.String
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: Value cannot be null." {
     { Get-MongoDBDocument -MongoCollection $null -DocumentId '1' } | Should -Throw "Cannot bind argument to parameter 'MongoCollection' because it is null."
    }
   }
  }
  Context "Get-MongoDBDocument CollectionId ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -MongoCollection $Collection1 | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "With a CollectionName and DocumentId" {
    It "Should Return System.String" {
     Get-MongoDBDocument -MongoCollection $Collection1 -DocumentId '1' | Should -BeOfType System.String
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: Cannot bind argument to parameter 'MongoCollection' because it is null." {
     { Get-MongoDBDocument -MongoCollection $null -DocumentId '1' } | Should -Throw -ErrorId "ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.GetDocumentCmdlet"
    }
   }
  }
  Context "Get-MongoDBDocument CollectionFilter ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.Collections.Generic.List[string]" {
     Get-MongoDBDocument -MongoCollection $Collection2  | Should -BeOfType 'System.Collections.Generic.List[string]'
    }
   }
   Context "With a CollectionName and Filter" {
    It "Should Return System.String" {
     Get-MongoDBDocument -MongoCollection $Collection2 -Filter @{'_id' = '1' } | Should -BeOfType System.String
    }
   }
   Context "Without a Collection" {
    It "Should throw an error: Value cannot be null." {
     { Get-MongoDBDocument -MongoCollection $null -Filter @{'_id' = '1' } } | Should -Throw "Cannot bind argument to parameter 'MongoCollection' because it is null."
    }
   }
  }
 }
}