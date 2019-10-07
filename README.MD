# TTS Monitor and WoW Addon • [Download](http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip)

<p align="justify">WoW Addon and Monitor reads <i>quests</i> and <i>chat messages</i> with text-to-speech voices. Program will choose correct gender and apply special audio effects depending on character type. It makes game more immersive and you will not miss messages in chat. Addon's text-to-speech feature offers the better gaming experience for visually impaired people.</p>

<hr />

**Note: WoW version 8.x requires new version of Addon and Monitor, which are using new message sending and capturing technique by using color pixels of the display. This addon works with official WoW Classic too.**

 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech Monitor 3.1.4.0 (2019-10-06) • Digitally Signed
 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech "World of Warcraft" Addon 8.2.5.2 (2019-10-06)
 
<hr />

### Screenshots

<p style="text-align: center;"><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Small_1.png" /></a> <a href="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon.png" target="_blank"><img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon_Small_GitHub.png" /></a></p>

### How it Works 

<p style="text-align: justify;">Step 1: When you open window with text (dialogue, quest, book, etc.) or receive chat message in game, <span class="white">"WoW Addon"</span> creates message between <code>&lt;message&gt;&lt;part&gt;text&lt;/part&gt;&lt;/message&gt;</code> tags. This message can include <code>text</code>, <code>name</code>, <code>gender</code>, <code>effect</code>, <code>group</code>, <code>pitch</code>, <code>rate</code>, <code>volume</code>, etc. and <code>command</code> value.</p>

<p style="text-align: justify;">Step 2: <span class="white">"WoW Addon"</span> converts text message to line of coloured pixels and shows this line inside your in-game window. Default line's position is Top(0) Left(0):</p>

<p><img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_Monitor_Display_2x.png" />
 <img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_Monitor_Display_Small.png" /></p>

<p style="color: #84e68c; text-align: justify;">Step 3: <span class="white">"TTS Monitor"</span> will capture this line of coloured pixels and convert it back to the text message if "Monitor: Display" capturin mode is enabled. Then, "TTS Monitor", <em>which must be running in the background</em>, converts it to SAPI XML and reads it with specific text-to-speech voice.</p>

<p style="text-align: justify;">Note: Make sure you do not cover this line with other game elements. TTS Monitor supports other message capturing methods.</p>

### YouTube video ... "WoW Addon" ( very old 2014-08-06 version ) with Ivona voices

[![addon video](http://img.youtube.com/vi/lhBGNJQvbUo/0.jpg)](http://www.youtube.com/watch?v=lhBGNJQvbUo)

### Installation • Exit WoW before installing or updating addon

<p>1. Download <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip">JocysComTextToSpeech-WoW.zip</a> file.<br />
2. Open "File Explorer" (Win+E), locate downloaded file and open it.<br />
3. Copy <i>"JocysCom-TextToSpeech-WoW"</i> folder (folder is inside downloaded zip file).</p>
<p><img src="http://www.jocys.com/Files/Software/Copy.png" /></p>
<p>4. Paste <i>"JocysCom-TextToSpeech-WoW"</i> folder inside <i>"AddOns"</i> folder of WoW. Usual directory: C:\Program Files\World of Warcraft\Interface\AddOns</p>
<p>5. Install additional text-to-speech voices, if needed. BETTER text-to-speech voices are developed by <a href="http://www.ivona.com" target="_blank">Ivona</a> and distributed (~$45 each) by <a href="http://harposoftware.com/en/content/10-trial" target="_blank">Harpo Software</a>.</p>
<p>6. Run <b>"JocysCom.TextToSpeech.Monitor.exe"</b> file (file is inside pasted folder). Usual directory: C:\Program Files\World of Warcraft\Interface\AddOns\JocysCom-TextToSpeech-WoW\</p>
<p>7. When <b>"TTS Monitor"</b>, will open, setup voices in <b>[Voices]</b> tab (example).</p>
<p><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices_2_2_35.png" /></a></p>
<p>8. Run "World of Warcraft" game.</p>

You can read more on how to use WoW addon here: https://www.jocys.com/TTS
