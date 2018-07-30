# Monitor and WoW Addon • [Download](http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip)

 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech Monitor 2.6.13 (2018-07-29) • Digitally Signed
 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech “World of Warcraft” Addon 2.3.2 (2018-07-29)
 
 * Old versions: <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2017-12-02.zip">2017-02</a> • <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2017-11-30.zip">2017-11</a> • <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2017-09-27.zip">2017-09</a> • <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2017-04-03.zip">2017-04</a> • <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2016-11-01.zip">2016-11</a> • <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2016-07-24.zip">2016-07</a> • <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2014-08-27.zip">2014-08</a> • <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW-2014-10-16.zip">2014-10</a>

<hr />

<p align="justify">Reads <i>quests</i> and <i>chat messages</i> with text-to-speech voices. Makes game more immersive and you will not miss messages in chat. If “Monitor” doesn't work, you may need to disable IPv6 in WoW game. Read... <em><a href="#problems-and-solutions">Problems and Solutions</a></em>.<br />
<br />
How it works: When you open window with text (dialogue, quest, book, etc.) or receive chat message in game, WoW <b>“Addon”</b> creates and sends message to you between <code>&lt;message&gt;&lt;part&gt;text&lt;/part&gt;&lt;/message&gt;</code> tags. This message can include <code>text</code>, <code>name</code>, <code>gender</code>, <code>effect</code>, <code>group</code>, <code>pitch</code>, <code>rate</code>, <code>volume</code>, etc. and <code>command</code> value. Then, <b>“Monitor”</b>, <em>which must be running in background, while gaming,</em> picks-up this message from your local network traffic, converts it to SAPI XML and reads it with specific text-to-speech voice.

<blockquote>
<sub>After Addon update 2.2.88 <b>[■]</b> (Stop text-to-speech) button can appear at the <b>center of the screen</b>. <strong>To move mini frame with [≡][■] buttons:</strong> Click on <b>[≡]</b> (Options) button, to open “Options” window. Unlock mini frame by unchecking first check-box in “Options” window. Move it, wherever you like. Lock it again, by checking the same check-box.</sub>
</blockquote>

<blockquote>
You can create your own text-to-speech addon (if game supports addons) or send messages directly to “Monitor” from your own game, to add text-to-speech feature to your game.
</blockquote>

<blockquote>
We don't mind if code from our WoW Addon will be used to add text-to-speech features to other addons. How and what messages to send with SendAddonMessage(...) in WoW, so “Monitor” could see them, can be founded in our addon's lua file. These messages can be visible in chat window, <i>if check-box “Hide addon &lt;messages&gt; in chat window” is unchecked in our addon's “Options” window</i>. Messages are also visible in “Options” window itself and in “Monitor”'s [1. Incoming Messages] tab. If other addon will take over one of the text categories, this category can be disabled in our addon, by unchecking corresponding check-box, to avoid text-to-speech duplication. There is also help about &lt;messages&gt; with examples in “Monitor”'s [Help] tab.
</blockquote>

<p style="text-align: center; margin: 10px;"><strong>Text-to-Speech Voices by <a href="http://windows.microsoft.com/en-us/windows/language-packs" target="_blank">Microsoft</a> or <a href="http://harposoftware.com/en/content/10-trial" target="_blank">Ivona</a> / <a href="http://harposoftware.com/en/content/10-trial" target="_blank">Harpo Software</a></strong></p>

FREE text-to-speech voices can be installed with <a target="_blank" href="http://windows.microsoft.com/en-us/windows/language-packs">Microsoft language packs</a>.<br />
BETTER text-to-speech voices are developed by <a target="_blank"  href="http://www.ivona.com">Ivona.com</a> and distributed by <a target="_blank"  href="http://harposoftware.com/en/content/10-trial">HarpoSoftware.com</a>.</p>

<p style="text-align: center;"><a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/309_Brian.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Brian_G1.jpg" style="width: 102px; height: 78px;" /></a> <a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/308_Amy.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Amy_G1.jpg" style="width: 102px; height: 78px;" /></a> <a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/347_Maxim.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Maxim_G1.jpg" style="width: 102px; height: 78px;" /></a> <a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/348_Tatyana.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Tatyana_G1.jpg" style="width: 102px; height: 78px;" /></a> <a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/304_Hans.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Hans_G1.jpg" style="width: 102px; height: 78px;" /></a> <a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/305_Marlene.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Marlene_G1.jpg" style="width: 102px; height: 78px;" /></a> <a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/329_Mathieu.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Mathieu_G1.jpg" style="width: 102px; height: 78px;" /></a> <a href="http://dnld.harposoftware.com/6oj4uhk7pn8gmjing/328_Celine.exe"><img alt="" src="http://www.jocys.com/files/software/TTS_Celine_G1.jpg" style="width: 102px; height: 78px;" /></a></p>

<p style="text-align: center;"><strong><a href="http://harposoftware.com/en/content/10-trial" target="_blank">OTHER LANGUAGES...</a></strong></p>


<sub>About... <a href="http://www.jocys.com/files/software/JocysCom.TextToSpeech.Monitor.rtf">JocysCom.TextToSpeech.Monitor.rtf</a> • Tested on Windows 8 and 10 • All code is available under open source licence.</sub><br />
<sup>You can use “Monitor” and create your own text-to-speech addons for games, if they support addons.<sup>

<p style="text-align: center;"><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Small_1.png" /></a> <a href="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon_Small.png" /></a></p>

### YouTube video ...“WoW Addon” (old version) with Ivona voices
<hr />

<p>Old versions of “Monitor” and “Addon” (2014-08-06) are presented in this video.<br />
New versions (2016-12-18) with more options can read quests and chat messages.</p>

[![addon video](http://img.youtube.com/vi/lhBGNJQvbUo/0.jpg)](http://www.youtube.com/watch?v=lhBGNJQvbUo)

### Security and concerns
<hr />

<p align="justify"><sub>This “WoW Addon” and “Monitor” does not give you more power over NPCs or other players in game. It does not send data back to game or anywhere else. It only reads local network messages between <code>&lt;message&gt;</code> tags or text from your clipboard with installed text-to-speech voices, when you want it. It makes game more involving, fun and interesting... and easier for people with <b>dyslexia</b> too. This project is open source... anyone can check all code at any time. We digitally sign our software and put our real names on it, so everyone could know, who to blame, if something will go wrong. It is not in our interest to ruin our reputation with hacks, exploits, malware or spyware. Please download this “WoW Addon” and “Monitor” only from trustworthy sources. Make sure that “Monitor” is always <i>signed</i> by verified publisher with signature, issued by trusted certificate authority.</sub></p>

### Installation • Exit WoW before installing or updating addon.
<hr />

<p>1. Download <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip">JocysComTextToSpeech-WoW.zip</a> file.<br />
2. Open “File Explorer” (Win+E), locate downloaded file and open it.<br />
3. Copy <i>“JocysCom-TextToSpeech-WoW”</i> folder (folder is inside downloaded zip file).</p>
<p><img src="http://www.jocys.com/Files/Software/Copy.png" /></p>
<p>4. Paste <i>“JocysCom-TextToSpeech-WoW”</i> folder inside <i>“AddOns”</i> folder of WoW.</p>
<blockquote>
<p><sub>Usual directory: C:\Program Files\World of Warcraft\Interface\AddOns</sub></p>
</blockquote>
<p>5. Install additional text-to-speech voices, if needed.</p>

<blockquote>
<p align="justify">BETTER are Ivona text-to-speech voices (<a href="http://harposoftware.com/en" target="_blank">$45 each</a>). Test voices online (developer): <a href="http://www.ivona.com" target="_blank">Ivona.com</a><br />
Download and test voices 30 days for free (distributor): <a href="http://harposoftware.com/en/content/10-trial" target="_blank">Harposoftware.com/en/content/10-trial</a><br />
Good Ivona voices are English-British <b>“Amy”</b> and <b>“Brian”</b>. Some other voices are not bad too. If you know free of charge text-to-speech voices as good, as voices by Ivona, please, write us.</p>
</blockquote>

<blockquote>
<p align="justify">FREE text-to-speech voices can be installed (on Windows 8.#) with <a target="_blank"  href="http://windows.microsoft.com/en-us/windows/language-packs">Microsoft language packs</a>.</p>

<p>1. Right-click the “Start Button” > Control Panel > Language.<br />
 2. Add additional languages like... English (United States) or English (United Kingdom)... etc.<br />
 3. Go to “Options” of each language and... [ Download and install language pack ].</p>
 
<p align="justify">When you will run <b>“JocysCom.TextToSpeech.Monitor.exe”</b>, additional text-to-speech voices should be visible now. Not all languages have text-to-speech voices. To switch between languages in Windows, you can use (Win+Space).</p>

<p>Windows 8.1 language packs and text-to-speech voices (<a target="_blank"  href="http://en.wikipedia.org/wiki/Microsoft_text-to-speech_voices">Wikipedia</a>):</p>

<p><sub>• English (United States) - David (male), Zira (female).<br />
• English (United Kingdom) - Heera (female), Hazel (female), Zira (female).<br />
• Chinese Simplified - Hanhan, Huihui.<br />
• Chinese Traditional - Tracy.<br />
• French - Hortense.<br />
• German - Hedda.<br />
• Japanese Haruka.<br />
• Korean - Heami.<br />
• Italian - Elsa.<br />
• Polish - Paulina.<br />
• Portuguese - Maria.<br />
• Russian - Irina.<br />
• Spanish - Helena, Sabina.</sub></p>
</blockquote>

<p>6. Run <b>“JocysCom.TextToSpeech.Monitor.exe”</b> file (file is inside pasted folder).</p>

<blockquote>
<p><sub>Usual directory: C:\Program Files\World of Warcraft\Interface\AddOns\JocysCom-TextToSpeech-WoW\</sub></p>
</blockquote>

<blockquote>
<p align="justify"><sub>You can pin “Monitor” to Windows OS taskbar, if you plan to use it frequently. Administrator rights are required to run this program. Make sure that you are using <i>digitally signed software</i>. Any modification in code of software, after it is signed, <i>invalidates</i> digital signature. <i>Digitally signed software</i> ensures that users can verify origin of software and that no one has tampered with it.</sub></p>
</blockquote>

<p>7. User Account Control window must show <b>“Verified Publisher: Jocys.com”</b> or <b>“Verified Publisher: Evaldas Jocys”</b>.</p>

<blockquote>
<p>Do not trust executables from <b>“Publisher: Unknown”</b>.</p>
</blockquote>

<p><img src="http://www.jocys.com/Files/Software/Monitor_Signed_UAC.png" /></p> 

<p>8. Click <b>[ Yes ]</b> button.</p>

<p>“Monitor Clipboard” option doesn't work and must be [Disabled] in Windows XP.<br />
“Addon” requires latest version of “World of Warcraft” game.</p>

<p>9. When <b>“Monitor”</b>, will open, setup voices in <b>[Voices]</b> tab (example).</p>

<p><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices_2_2_35.png" /></a></p>

<blockquote>
<p><sub>3 voices are enabled in this setup:<br />
“IVONA 2 Brian” -- will be used as Male (100) and Neutral (100) voice.<br />
“IVONA 2 Amy” -- will be used as Female (100) voice.<br />
“Microsoft Hazel Desktop” -- will be used as Neutral (50) voice.<br />
<br />
Number ( from 0 to 100 ) sets, how often voice should be used, relatively to other voices in the same category (Male, Female or Neutral). If you have IVONA male and female voices, you can disable “Microsoft (...) Desktop” voices ( as they are quite bad ).<br />
<br />
Monitor can't play voices, if their trial period is expired. You can test, which ones are expired, by enabling one voice at a time and pressing [ Speak ] button in Monitor. Such voices must be disabled in [ Voices ] tab.</sub></p>
</blockquote>

Speed of voices can be adjusted in “Monitor's” “Rate” fields. Values can be from “-10” (very slow) to “10” (very fast). Default value is “2”. You can select “1” or “0” in both fields to make voices slower.

<p><img src="https://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Rate_Speed.png" /></p>

<p>10. Run <b>“World of Warcraft”</b> game.<br />


<p>11. <b>[■]</b> (Stop text-to-speech) button will appear at the <b>center of the screen</b>. If you will move mouse pointer over <b>[≡]</b> (Options) button, menu with various <b>check-boxes</b> will appear. Move mouse pointer over any <strong>check-box</strong> and title will show, what it enables/disables.</p>

<blockquote>
<sub><strong>To move mini frame with [≡][■] buttons:</strong> Click on <b>[≡]</b> (Options) button, to open “Options” window. Unlock mini frame by unchecking first check-box in “Options” window. Move it, wherever you like. Lock it again, by checking the same check-box.</sub>
</blockquote>

<p><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon_MiniFrame_2018_07_27.png"/></p>

<blockquote>
<sub>If “Social-Friends” button is not visible:<br />
1. Open <i>JocysCom-TextToSpeech-WoW.xml</i> file with Notepad.<br />
2. Find and change <code>"QuickJoinToastButton"</code> to <code>"UIParent"</code><br />
3. Reload WoW. -- [■] (Stop) button should appear at the top-center of your screen. You can unlock [■] (Stop) button in [≡] (Options) and move it to desired position.<br />
Usual file directory: C:\Program Files\World of Warcraft\Interface\AddOns\JocysCom-TextToSpeech-WoW\</sub>
</blockquote>

12. <b>[■]</b> (Stop) and <b>[►]</b> (Play) buttons will appear at the bottom of some windows too.</p>

<p></a target="_blank" href="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon_Buttons.png"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon_Buttons.png" /></a></p>

<p>13. <b>“Addon”</b> gets genders of targeted (you are interacting with) NPCs. <b>“Addon”</b> also gets conversations-chat, but not genders of NPCs, who are walking and talking around you. Therefore, <strong>“Addon”</strong> automatically saves every NPC (name, gender and type), which you are targeting, in <b>“Monitor“</b> (must be running in background). Also, if slot is available, <b>“Addon“</b> will create <strong>“NPCSaveTTSMacro”</strong> and will update it during the game. You can use this macro to target at once up to 9 last chatting NPCs or any nearest friendly NPC (to save them in <b>“Monitor“</b>). Saved in <b>“Monitor“</b> NPCs have genders (male and female voices) in their conversations. I didn't figure out yet, how to get NPCs genders directly from NPC chat event or some database (although, there is no problem with players). For example: If you see two female NPCs chatting, but they have male voices... just click on them (or use macro to target them) and they should get female voices from that moment. Enter <strong>/m</strong> in chat window to show macros:</p>

<p></a target="_blank" href="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon_Macro.png"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon_Macro.png" /></a></p>

<blockquote>
<sub>Usual per-account settings directory:<br />
#:\Program Files\World of Warcraft\WTF\Account\&lt;AccountName&gt;\SavedVariables\JocysCom-TextToSpeech-WoW.lua<br />
Usual per-character settings directory, where Addon settings are saved:<br />
#:\Program Files\World of Warcraft\WTF\Account\&lt;AccountName&gt;\&lt;RealmName&gt;\&lt;CharacterName&gt;\JocysCom-TextToSpeech-WoW.lua<br />
You can delete these files to reset settings or clear saved NPCs (game must be closed).
</sub>
</blockquote>

#### “Stop text-to-speech” macro and hotkey setup in “World of Warcraft” game
<hr />

<p>1. Game Menu > [Macros] > [New]<br />
2. Enter macro name (for example: “Stop TTS”).<br />
3. Choose an icon for macro.<br />
4. Click [Okay] button.<br />
5. Write in “Enter Macro Commands” field:</p>
<p><sup><code>/run SendChatMessage("&lt;message command=\"stop\" /&gt;", "WHISPER", "Common", UnitName("player"))</code><sup></p>
<p>6. Click [Save] button.<br />
7. Drag “Stop TTS” macro icon from “General Macros” tab on desired “Action Bar” button.<br />
8. Click [Exit] button.<br />
9. Game Menu > [Key Bindings] > [Action Bar]<br />
10. Bind desired keyboard key to "Action Bar" button with “Stop TTS” macro. After pressing this key on keyboard, text-to-speech will stop.</p>

### Desired improvements in “World of Warcraft” game
<hr />

<p>• Getting <b>UnitSex</b> of NPC from chat and quest log -- no need to select it to get gender.<br />
• Getting <b>UnitAge</b> of NPC (%) -- (1-100) in order to apply “Child", “Adult” or “Senior” effects.<br />
• Getting <b>UnitRace</b> of NPC -- Sound effects on NPCs are applied by <strong>UnitCreatureType</strong> now.<br />
• Getting <b>value</b> if NPC has voice over for all text -- in order not to start TTS speech.<br />
• NPC “talking” animation should loop, depending on length of the text.</p>

### Problems and Solutions
<hr />

<p>Sometimes, “Monitor” doesn't work with “Internet Protocol Version 6” (IPv6). To test this, you can:<br />
<br />
<strong>A)</strong> Disable IPv6 in latest version of “World of Warcraft” game:<br />
<br />
1. WoW Main Menu &gt; System &gt; Network &gt; Uncheck (disable) “[ ] Enable IPv6 when available” check-box.<br />
2. Restart WoW game.</p>

<p><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Addon_Network.png" style="width: 562px; height: 217px;" /></p>

<hr />

<p>or...<br />
<br />
<strong>B)</strong> Temporary disable IPv6 on your network card:</p>

<p>1. Close WoW game and “Monitor”.<br />
2. Click [Win]+[X] keyboard keys together.<br />
3. Select “Control Panel” from menu.<br />
4. Select “Small icons” in “View by:” drop-down menu.<br />
5. Find and select “Network and Sharing Centre”.<br />
6. Click on “Change adapter settings”.<br />
7. Click with second mouse button on network card, you are using.<br />
8. Select “Properties” from menu.<br />
9. Uncheck (disable) “Internet Protocol Version 6”.<br />
10. Click [ OK ] button.<br />
11. Run WoW game and “Monitor”.</p>

<p>Don't forget to enable (check) “Internet Protocol Version 6” again later.</p>

<img src="http://www.jocys.com/Files/Software/Ethernet_Properties_IP6.png" />

### Cache Options (Create Text-to-Speech Audio Files)
<hr />

<p>If your computer is slow, you can use “Monitor's” “Cache Options” to write and read audio files. The next time, when you will encounter the same dialogue-text, “Monitor” will not use TTS voices and will not create audio file again, but will play instantly previously created audio file instead.</p>

<p>“Monitor” &gt; [Options] tab &gt; “Cache Options” section</p>

<p><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Monitor_Cache_Options.png" style="width: 223px; height: 137px;" /></p>

<p><sub>Usual directory for audio files: C:\ProgramData\Jocys.com\Text to Speech Monitor\Cache\</sub></p>

<p><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Monitor_Cache_Folder.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_WoW_Monitor_Cache_Folder.png" style="width: 600px; height: 203px;" /></a></p>

<p>Note: If you put custom recorded *.MP3 file with the same name then "Monitor" will play *.MP3 file, instead of *.WAV.</p>
