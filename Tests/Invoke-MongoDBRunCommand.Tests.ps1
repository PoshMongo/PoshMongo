BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 $Database = New-MongoDBDatabase -DatabaseName 'MyDB'
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
}
Describe "Invoke-MongoDBRunCommand" -Tag "PoshMongo", "InvokeRunCommandCmdlet", "Invoke" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Invoke-MongoDBRunCommand | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing Parameters" {
  Context "DatabaseName ParameterSet" {
   Context "DatabaseName parameter" {
    It "Should be String" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter DatabaseName -Type String
    }
    It "Should be Mandatory" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter DatabaseName -Mandatory
    }
   }
   Context "CommandString parameter" {
    It "Should be String" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter CommandString -Type String
    }
    It "Should be Mandatory" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter CommandString -Mandatory
    }
   }
  }
  Context "Database ParameterSet" {
   Context "MongoDatabase parameter" {
    It "Should be IMongoDatabase" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter MongoDatabase -Type MongoDB.Driver.IMongoDatabase
    }
    It "Should be Mandatory" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter MongoDatabase -Mandatory
    }
   }
   Context "CommandString parameter" {
    It "Should be String" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter CommandString -Type String
    }
    It "Should be Mandatory" {
     Get-Command Invoke-MongoDBRunCommand | Should -HaveParameter CommandString -Mandatory
    }
   }
  }
 }
 Context "Invoke-MongoDBRunCommand Usage" {
  Context "Invoke-MongoDBRunCommand DatabaseName ParameterSet" {
   Context "With a DatabaseName" {
    It "Should Return System.Text.Json" {
     Invoke-MongoDBRunCommand -DatabaseName MyDB -CommandString '{ dbStats: 1 }'  | Should -BeOfType 'System.String'
    }
   }
   Context "Without a DatabaseName" {
    It "Should return an error: ParameterArgumentValidationErrorEmptyStringNotAllowed" {
     {Invoke-MongoDBRunCommand -DatabaseName '' -CommandString '{ dbStats: 1 }'} | Should -Throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Invoke.InvokeRunCommand'
    }
   }
   Context "Without a CommandString" {
    It "Should return an error: ParameterArgumentValidationErrorEmptyStringNotAllowed" {
     {Invoke-MongoDBRunCommand -DatabaseName MyDB -CommandString ''} | Should -Throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Invoke.InvokeRunCommand'
    }
   }
  }
  Context "Invoke-MongoDBRunCommand Database ParameterSet" {
   Context "With a MongoDatabase" {
    It "Should Return System.Text.Json" {
     Invoke-MongoDBRunCommand -MongoDatabase $Database -CommandString '{ dbStats: 1 }'  | Should -BeOfType 'System.String'
    }
   }
   Context "Without a Database" {
    It "Should return an error: ParameterArgumentValidationErrorNullNotAllowed" {
     {Invoke-MongoDBRunCommand -MongoDatabase $null -CommandString '{ dbStats: 1 }'} | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Invoke.InvokeRunCommand'
    }
   }
   Context "Without a CommandString" {
    It "Should return an error: ParameterArgumentValidationErrorEmptyStringNotAllowed" {
     {Invoke-MongoDBRunCommand -MongoDatabase $Database -CommandString ''} | Should -Throw -ErrorId 'ParameterArgumentValidationErrorEmptyStringNotAllowed,PoshMongo.Invoke.InvokeRunCommand'
    }
   }
  }
 }
}