@ECHO OFF
dotnet ef database update --verbose --startup-project ../MSA.EventPlatform.API
