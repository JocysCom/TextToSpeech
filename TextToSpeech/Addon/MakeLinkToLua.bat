::@echo off
:: Run As Administrator.
SET add=d:\Program Files\aswow
SET wow=d:\Program Files\World of Warcraft
SET pro=d:\Projects\Jocys.com\WoW\TextToSpeech\Addon
SET dst=%wow%\Interface\AddOns\JocysCom-WoW-TextToSpeech
SET art=%wow%\BlizzardInterfaceArt\Interface
SET cod=%wow%\BlizzardInterfaceCode\Interface
::mklink "%dst%\JocysCom-WoW-TextToSpeech.lua" "%src%\JocysCom-WoW-TextToSpeech.lua"
::mklink "%dst%\JocysCom-WoW-TextToSpeech.toc" "%src%\JocysCom-WoW-TextToSpeech.toc"
::mklink "%dst%\JocysCom-WoW-TextToSpeech.xml" "%src%\JocysCom-WoW-TextToSpeech.xml"
:: Map FrameXML.
SET dir=%add%\Extensions\Tools\WoWBench\BlizzardToolkit\Interface\FrameXML
IF EXIST "%dir%" RMDIR /Q /S "%dir%"
MKLINK /D "%dir%" "%cod%\FrameXML"
:: Map FrameXML (project).
::SET dir=%pro%\FrameXML
::IF EXIST "%dir%" RMDIR /Q /S "%dir%"
::MKLINK /D "%dir%" "%cod%\FrameXML"
:: Map AddOns
SET dir=%add%\Extensions\Tools\WoWBench\wow\Interface\AddOns
IF EXIST "%dir%" RMDIR /Q /S "%dir%"
MKLINK /D "%dir%" "%cod%\AddOns"
:: Update schema
SET fil=%add%\Xml\Schemas\UI.xsd
IF EXIST "%fil%" DEL /Q "%fil%"
MKLINK "%fil%" "%cod%\FrameXML\UI.xsd"
pause
