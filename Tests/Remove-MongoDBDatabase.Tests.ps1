BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
 Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 New-MongoDBDatabase -DatabaseName 'MyDB1' | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection1' -DatabaseName 'MyDB'  | Out-Null
 New-MongoDBCollection -CollectionName 'myCollection2' -DatabaseName 'MyDB'  | Out-Null
}
AfterAll {
 Remove-MongoDBDatabase -DatabaseName 'MyDB' | Out-Null
 Remove-MongoDBDatabase -DatabaseName 'MyDB1' | Out-Null
}
Describe "Remove-MongoDBDatabase" -Tag "PoshMongo", "RemoveDatabaseCmdlet", "Database" {
 Context "Cmdlet Tests" {
  It "Should have HelpUri defined in Cmdlet() declaration" {
   [System.Uri]::new((Get-Command Remove-MongoDBDatabase | Select-Object -ExpandProperty HelpUri)).GetType().FullName | Should -Be 'System.Uri'
  }
 }
 Context "Testing ParameterSets" {
  Context "Default ParameterSet" {
   It "ParameterSet should contain, DatabaseName, Client" {
    (Get-Command -Name 'Remove-MongoDBDatabase').Parameters['DatabaseName'].ParameterSets.Default | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDatabase').Parameters['Client'].ParameterSets.Default | Should -Be $true
   }
   Context "DatabaseName parameter" {
    It "Should be String" {
     Get-Command Remove-MongoDBDatabase | Should -HaveParameter DatabaseName -Type String
    }
    It "Should be Mandatory" {
     Get-Command Remove-MongoDBDatabase | Should -HaveParameter DatabaseName -Mandatory
    }
   }
   Context "Client parameter" {
    It "Should be IMongoClient" {
     Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Type MongoDB.Driver.IMongoClient
    }
    It "Should be Mandatory" {
     Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Not -Mandatory
    }
   }
  }
  Context "Database ParameterSet" {
   It "ParameterSet should contain, Database, Client" {
    (Get-Command -Name 'Remove-MongoDBDatabase').Parameters['Database'].ParameterSets.Database | Should -Be $true
    (Get-Command -Name 'Remove-MongoDBDatabase').Parameters['Client'].ParameterSets.Database | Should -Be $true
   }
   Context "Database parameter" {
    It "Should be IMongoDatabase" {
     Get-Command Remove-MongoDBDatabase | Should -HaveParameter Database -Type MongoDB.Driver.IMongoDatabase
    }
    It "Should be Mandatory" {
     Get-Command Remove-MongoDBDatabase | Should -HaveParameter Database -Mandatory
    }
   }
   Context "Client parameter" {
    It "Should be IMongoClient" {
     Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Type MongoDB.Driver.IMongoClient
    }
    It "Should be Mandatory" {
     Get-Command Get-MongoDBDatabase | Should -HaveParameter Client -Not -Mandatory
    }
   }
  }
 }
 Context "Remove-MongoDBDatabase Usage" {
  Context "Remove-MongoDBDatabase Default ParameterSet" {
   Context "Without a DatabaseName" {
    It "Should throw an error: Cannot bind argument to parameter 'DatabaseName' because it is null." {
     { Remove-MongoDBDatabase -DatabaseName $null } | Should -Throw
    }
   }
   Context "With a DatabaseName" {
    It "Should Return null" {
     Remove-MongoDBDatabase -DatabaseName 'MyDB' | Should -Be $null
    }
   }
   Context "With an invalid DatabaseName" {
    It "Should throw an error: Database names must be non-empty and not contain '.' or the null character." {
     { Remove-MongoDBDatabase -DatabaseName "my.db" } | Should -Throw
    }
   }
   Context "With an invalid MongoClient" {
    It "Should throw an error: Must be connected to a MongoDB instance." {
     { Get-MongoDBDatabase -DatabaseName "MyDB" -Client (New-Object -TypeName MongoDB.Driver.MongoClient) } | Should -Throw "Must be connected to a MongoDB instance."
    }
   }
  }
  Context "Remove-MongoDBDatabase Database ParameterSet" {
   Context "Without a Database" {
    It "Should throw an error: Cannot bind argument to parameter 'Database' because it is null." {
     { Remove-MongoDBDatabase -Database $null } | Should -Throw -ErrorId 'ParameterArgumentValidationErrorNullNotAllowed,PoshMongo.Database.RemoveDatabase'
    }
   }
   Context "With a Database" {
    It "Should Return null" {
     $Database = Get-MongoDBDatabase -DatabaseName 'MyDB1'
     Remove-MongoDBDatabase -Database $Database | Should -Be $null
    }
   }
   Context "With an invalid MongoClient" {
    It "Should throw an error: Must be connected to a MongoDB instance." {
     { Get-MongoDBDatabase -Database $Database -Client (New-Object -TypeName MongoDB.Driver.MongoClient) } | Should -Throw "Must be connected to a MongoDB instance."
    }
   }
  }
 }
}