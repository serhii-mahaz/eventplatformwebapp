@ECHO OFF
IF "%1"=="" (
    Echo Usage: %0 [migration name]
) ELSE (
	dotnet ef migrations add %1 --startup-project ../MSA.EventPlatform.API
)
