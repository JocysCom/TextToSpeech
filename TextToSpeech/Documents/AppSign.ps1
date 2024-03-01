Import-Module "d:\_Backup\Configuration\SSL\Tools\app_signModule.ps1" -Force

[string[]]$appFiles = @(
    "..\bin\Debug\app.publish\JocysCom.TextToSpeech.Monitor.exe"
)
[string]$appName = "Jocys.com Text to Speech Monitor"
[string]$appLink = "https://www.jocys.com"

ProcessFiles $appName $appLink $appFiles
pause