BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection2' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"123","FirstName":"Joe","LastName":"Friday"}' -CollectionName 'myCollection1' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"456","FirstName":"Ben","LastName":"Romero"}' -CollectionName 'myCollection1' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19751","FirstName":"David","LastName":"Starsky"}' -CollectionName 'myCollection1' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19752","FirstName":"Kenneth","Hutchinson":"Friday"}' -CollectionName 'myCollection1' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19791","Bo":"Joe","LastName":"Duke"}' -CollectionName 'myCollection2' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19792","Luke":"Joe","LastName":"Duke"}' -CollectionName 'myCollection2' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19811","FirstName":"Rick","LastName":"Simon"}' -CollectionName 'myCollection2' | Out-Null
 Add-MongoDBDocument -Document '{"_id":"19812","FirstName":"Andrew","LastName":"Simon"}' -CollectionName 'myCollection2' | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Remove-MongoDBDocument" -Tag $Module, "RemoveDocumentCmdlet", "Document" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Remove-MongoDBDocument | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "DocumentId ParameterSet" {
   It "ParameterSet should contain, DocumentId" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.DocumentId | Should -Be $true
   }
   It "DocumentId should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.DocumentId.IsMandatory | Should -Be $true
   }
  }
  Context "Filter ParameterSet" {
   It "ParameterSet should contain, Filter" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.Filter | Should -Be $true
   }
   It "Filter should be Hashtable" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter Filter -Type Hashtable
   }
   It "Filter should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.Filter.IsMandatory | Should -Be $true
   }
  }
  Context "CollectionNameId ParameterSet" {
   It "ParameterSet should contain, CollectionName, DocumentId" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameId | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionNameId | Should -Be $true
   }
   It "CollectionName should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter CollectionName -Type String
   }
   It "CollectionName should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameId.IsMandatory | Should -Be $true
   }
   It "DocumentId should be String" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter DocumentId -Type String
   }
   It "DocumentId should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['DocumentId'].ParameterSets.CollectionNameId.IsMandatory | Should -Be $true
   }
  }
  Context "CollectionNameFilter ParameterSet" {
   It "ParameterSet should contain, CollectionNameFilter, Filter" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['CollectionName'].ParameterSets.CollectionNameFilter | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.CollectionNameFilter | Should -Be $true
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
  }
 }
 Context "Remove-MongoDBDocument Usage" {
  Context "Remove-MongoDBDocument DocumentId ParameterSet" {
   Context "With a DocumentId" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -DocumentId '123' | Should -Be $null
    }
   }
   Context "With a null DocumentId" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -DocumentId $null } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a DocumentId" {
    It "Should throw an error: AmbiguousParameterSet" {
     { Remove-MongoDBDocument } | Should -throw -ErrorId 'AmbiguousParameterSet,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a DocumentId" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "With an invalid DocumentId" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId '' } | Should -throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument Filter ParameterSet" {
   Context "With a Filter" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -Filter @{'_id'='123'} | Should -Be $null
    }
   }
   Context "With a null Filter" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -Filter $null } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a DocumentId" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "With an invalid DocumentId" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId '' } | Should -throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument CollectionNameId ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -DocumentId '123' -CollectionName 'myCollection1' | Should -Be $null
    }
   }
   Context "With a null CollectionName" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -DocumentId '123' -CollectionName $null } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId -CollectionName } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
  Context "Remove-MongoDBDocument CollectionNameFilter ParameterSet" {
   Context "With a CollectionName" {
    It "Should Return System.String" {
     Remove-MongoDBDocument -Filter @{'_id'='123'} -CollectionName 'myCollection1'| Should -Be $null
    }
   }
   Context "With a null Filter" {
    It "Should throw an error: ParameterBindingValidationException" {
     { Remove-MongoDBDocument -Filter @{'_id'='123'} -CollectionName  $null } | Should -throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
   Context "Without a CollectionName" {
    It "Should throw an error: MissingArgument" {
     { Remove-MongoDBDocument -DocumentId -CollectionName } | Should -throw -ErrorId 'MissingArgument,PoshMongo.Document.RemoveDocumentCmdlet'
    }
   }
  }
 }
}