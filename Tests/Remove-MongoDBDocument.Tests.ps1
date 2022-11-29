BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"123","FirstName":"Joe","LastName":"Friday"}' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"456","FirstName":"Ben","LastName":"Romero"}' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"19751","FirstName":"David","LastName":"Starsky"}' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"19752","FirstName":"Kenneth","Hutchinson":"Friday"}' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"19791","Bo":"Joe","LastName":"Duke"}' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"19792","Luke":"Joe","LastName":"Duke"}' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"19811","FirstName":"Rick","LastName":"Simon"}' | Out-Null
 $Collection | Add-MongoDBDocument -Document '{"_id":"19812","FirstName":"Andrew","LastName":"Simon"}' | Out-Null
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
   It "ParameterSet should contain, DocumentId" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.Filter | Should -Be $true
   }
   It "Filter should be Hashtable" {
    Get-Command Remove-MongoDBDocument | Should -HaveParameter Filter -Type Hashtable
   }
   It "Filter should be Mandatory" {
    (Get-Command -Name 'Remove-MongoDBDocument').Parameters['Filter'].ParameterSets.Filter.IsMandatory | Should -Be $true
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
 }
}