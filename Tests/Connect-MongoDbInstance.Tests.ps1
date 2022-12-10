BeforeAll {
 $Module = "PoshMongo";
 $RootPath = (Get-Item -Path .).FullName;
 Import-Module "$($RootPath)\Module\$($Module).psd1" -Force
}
Describe "Connect-MongoDBInstance" -Tag "PoshMongo", "ConnectInstanceCmdlet", "Connection" {
 Context "Testing Parameters" {
  Context "ConnectionString parameter" {
   It "Should be String" {
    Get-Command Connect-MongoDBInstance | Should -HaveParameter ConnectionString -Type String
   }
   It "Should be Mandatory" {
    Get-Command Connect-MongoDBInstance | Should -HaveParameter ConnectionString -Mandatory
   }
  }
  Context "ForceTls12 parameter" {
   It "Should be SwitchParameter" {
    Get-Command Connect-MongoDBInstance | Should -HaveParameter ForceTls12 -Type System.Management.Automation.SwitchParameter
   }
   It "Should not be Mandatory" {
    Get-Command Connect-MongoDBInstance | Should -HaveParameter ForceTls12 -not -Mandatory
   }
  }
 }
 Context "Connect-MongoDBInstance Usage" {
  Context "With a valid ConnectionString" {
   It "Should Return System.Data.DataSet" {
    Connect-MongoDBInstance -ConnectionString (Get-Content .\ConnectionSettings) | Should -BeOfType MongoDB.Driver.MongoClientBase
   }
  }
  Context "With an invalid Path" {
   It "Should throw an error: The connection string is not valid." {
    { Connect-MongoDBInstance -ConnectionString $Rootpath } | Should -Throw
   }
  }
  Context "Without a ConnectionString" {
   It "Should throw an error:  Cannot bind argument to parameter 'ConnectionString' because it is null" {
    { Connect-MongoDBInstance -ConnectionString $null } | Should -Throw
   }
  }
 }
}