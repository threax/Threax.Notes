param(
    [Parameter(Position=0,mandatory=$true)]
    [string] $migrationName
)

$scriptPath = Split-Path $script:MyInvocation.MyCommand.Path

function Test-Error([string] $msg, [int] $code = 0){
    if($LastExitCode -ne $code){
        throw $msg;
    }
}

Push-Location "$scriptPath/Notes.Db.Sqlite"

dotnet ef migrations add -c AppDbContext $migrationName; Test-Error "Error updating db context"

Pop-Location


Push-Location "$scriptPath/Notes.Db.SqlServer"

dotnet ef migrations add -c AppDbContext $migrationName; Test-Error "Error updating db context"

Pop-Location