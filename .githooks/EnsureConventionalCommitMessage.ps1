[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingWriteHost', '', Justification = 'This script is not intended to have any output piped')]
[CmdletBinding()]
Param(
    [Parameter(Position = 0)]
    $File
)

$commitMessage = Get-Content $File -Raw
$regex = "(?-i)^(initial commit|merge [^\r\n]+|(build|chore|ci|docs|feat|fix|perf|refactor|revert|style|test){1}(\([\w\-\.]+\))?(!)?: ([\w ])+([\s\S]*))"

if ($commitMessage -notmatch $regex) {
    Write-Host ("{0}: The commit message does not match the Conventional Commit rules regex" -f "EnsureConventionalCommitMessage.ps1")
    exit 1
}
