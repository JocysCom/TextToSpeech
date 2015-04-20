SET prg="%ProgramFiles%\Git\bin\git.exe"
IF NOT EXIST %prg% SET prg="%ProgramFiles(x86)%\Git\bin\git.exe"
IF NOT EXIST %prg% SET prg="%ProgramW6432%\Git\bin\git.exe"
%prg% clone https://github.com/JocysCom/TextToSpeech.git "D:\Projects\Jocys.com\TextToSpeech.Temp"
pause