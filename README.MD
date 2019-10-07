# TTS Monitor and WoW Addon • [Download](http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip)

<p align="justify">WoW Addon and TTS Monitor reads <i>quests</i> and <i>chat messages</i> with text-to-speech voices. Program will choose correct gender and apply special audio effects depending on character type. It makes game more immersive and you will not miss messages in chat. Addon's text-to-speech feature offers the better gaming experience for visually impaired people.</p>

**Note: WoW version 8.x requires new version of Addon and Monitor, which are using new message sending and capturing technique by using color pixels of the display. This addon works with official WoW Classic too.**

 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech Monitor 3.1.4.0 (2019-10-06) • Digitally Signed
 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech "World of Warcraft" Addon 8.2.5.2 (2019-10-06)
 
<hr />

### Screenshots

<p style="text-align: center;"><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Small_1.png" /></a> <a href="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon.png" target="_blank"><img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon_Small_GitHub.png" /></a></p>

### YouTube Video (very old version of "WoW Addon" with Ivona voices - 2014-08-06)

[![addon video](http://img.youtube.com/vi/lhBGNJQvbUo/0.jpg)](http://www.youtube.com/watch?v=lhBGNJQvbUo)

### How it Works (new version)

<ol>
<li>"WoW Addon" creates message between <code>&lt;message&gt;&lt;part&gt;text&lt;/part&gt;&lt;/message&gt;</code> tags when you open window with text (dialogue, quest, book, etc.) or receive chat message in game. This message can include <code>text</code>, <code>name</code>, <code>gender</code>, <code>effect</code>, <code>group</code>, <code>pitch</code>, <code>rate</code>, <code>volume</code>, etc. and <code>command</code> value.</li>
<li>"WoW Addon" converts text message to line of coloured pixels and shows this line inside your in-game window. 1 RGB pixel can hold 3 bytes or 3 ASCII characters or 1 UTF8 Japanese character. Default line's position is Top(0) Left(0):<br />
<img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon_Pixels.png" /> <img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_Monitor_Display_Small.png" /></li>
<li>"TTS Monitor" captures this line of coloured pixels and converts back to the text message if "Monitor: Display" capturing mode is enabled. Then, "TTS Monitor", <em>which must be running in the background</em>, converts it to SAPI XML and reads it with specific text-to-speech voice. Addon supports "Cache" option which allows to use manually pre-recorded voices.</li>
</ol>
<p>Note: Make sure you do not cover this line with other game elements. TTS Monitor supports other message capturing methods.</p>

### How to Install

<ol>
<li>Exit WoW before installing or updating addon.</li>
<li>Download <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip">JocysComTextToSpeech-WoW.zip</a> file.</li>
<li>Open "File Explorer" (Win+E), locate downloaded file and open it.</li>
<li>Copy <i>"JocysCom-TextToSpeech-WoW"</i> folder (folder is inside downloaded zip file).<br />
<img src="http://www.jocys.com/Files/Software/Copy.png" /></li
<li>Paste <i>"JocysCom-TextToSpeech-WoW"</i> folder inside <i>"AddOns"</i> folder of WoW.<br/>Usual directory: C:\Program Files\World of Warcraft\Interface\AddOns</li>
<li>Install additional text-to-speech voices, if needed. BETTER text-to-speech voices are developed by <a href="http://www.ivona.com" target="_blank">Ivona</a> and distributed by <a href="http://harposoftware.com/en/content/10-trial" target="_blank">Harpo Software</a>.</li>
<li>Run <b>"JocysCom.TextToSpeech.Monitor.exe"</b> file (file is inside pasted folder).<br/>Usual directory: C:\Program Files\World of Warcraft\Interface\AddOns\JocysCom-TextToSpeech-WoW\</li>
<li>When <b>"TTS Monitor"</b>, will open, setup voices in <b>[Voices]</b> tab (example).</li>
<li><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices_2_2_35.png" /></a></li>
<li>Run "World of Warcraft" game.</li>
</ol>

You can read more on how to use WoW addon here: https://www.jocys.com/TTS
