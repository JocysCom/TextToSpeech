@echo off
:: Get current directory.
for /f "tokens=*" %%a in ('cd') DO SET DIR0=%%a

:: Set destination file, source list and exclusion list.
:: %~n0 equals to current batch file name without extension.
SET fil=%~n0
SET dst="%DIR0%\%fil%.zip"
SET lst="%DIR0%\%fil%-Include.txt"
SET img="%DIR0%\%fil%-Images.txt"
SET psp="%DIR0%\%fil%-Presets.txt"
SET osp="%DIR0%\%fil%-Overrides.txt"
SET ssp="%DIR0%\%fil%-Sounds.txt"
SET exc="%DIR0%\%fil%-Exclude.txt"
SET wra="%ProgramFiles%\WinRAR\winrar.exe"
if NOT EXIST %wra% SET wra="%ProgramFiles(x86)%\WinRAR\winrar.exe"
if NOT EXIST %wra% SET wra="%ProgramW6432%\WinRAR\winrar.exe"
SET zip=%wra% a -r -afzip -ep -x@%exc%
echo DST=%dst%
echo LST=%lst%
echo EXC=%exc%
echo ZIP=%zip%

:: ---------------------------------------------------------------------------
:: Go to current folder
cd %DIR0%
echo --- Delete file if file with same name already exist.
IF EXIST %dst% del %dst%
:: Go back to Solution root folder.
cd "..\.."
cd
echo --- Pack all files in the list.
%zip% -ap"%fil%" %dst% @%lst%
%zip% -ap"%fil%\Presets" %dst% @%psp%
%zip% -ap"%fil%\Overrides" %dst% @%osp%
%zip% -ap"%fil%\Sounds" %dst% @%ssp%
%zip% -ap"%fil%\Images" %dst% @%img%
:: Go back to home/current folder.
cd %DIR0%

:end
pause