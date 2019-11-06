# TTS Monitor and WoW Addon • [Download](http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip)

<p align="justify">WoW Addon and TTS Monitor reads <i>quests</i> and <i>chat messages</i> with text-to-speech voices. Program will choose correct gender and apply special audio effects depending on character type. It makes game more immersive and you will not miss messages in chat. Addon's text-to-speech feature offers the better gaming experience for visually impaired people. <b>Now works with WoW version 8.x and official WoW Classic.</b> E-mail: <a href="mailto:info@jocys.com?subject=Jocys.com%20Text-to-Speech%20Monitor">info@jocys.com</a></p>

 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech Monitor 3.2.7.0 (2019-10-22) • Digitally Signed
 * <a target="_blank" href="http://www.jocys.com">Jocys.com</a> Text to Speech "World of Warcraft" Addon 8.2.5.10 (2019-11-05)
 
<hr />
Monitor supports locally installed and <a href="https://aws.amazon.com/polly/">Amazon Polly</a> (Cloud) Text-To-Speech Standard and Neural voices. You must create account for Amazon voices on https://aws.amazon.com. More help on TTS Monitor > "Options" tab > "Amazon Poly" tab.

You can compare various voices below:

<table>
<tr>
<th colspan="2"></th>
<th><img src="http://www.jocys.com/TTS/Images/Icon_Achievement_Character_Human_Female.png" />

Female</th>
<th><img src="http://www.jocys.com/TTS/Images/Icon_Achievement_Character_Human_Male.png" />

Male</th>
<th><img src="http://www.jocys.com/TTS/Images/Icon_INV_Misc_Head_Dragon_Nexus.png" />

Dragon</th>
<th><img src="http://www.jocys.com/TTS/Images/Icon_INV_Misc_Head_ClockworkGnome_01.png" />

Robot</th>
</tr><tr>
	<th>Voice Developer</th><th>TTS Quality</th><td colspan="4"><b>Quest Text:</b> "I bring greetings from King Wrynn, Mage. Behind me is the Hero's Call board, a collection of proclamations from the King of Stormwind directing adventurers to locations where their talents can best aid the Alliance."</td></tr><tr>
<td><a href="https://developer.microsoft.com/en-us/microsoft-edge/testdrive/demos/speechsynthesis/">Microsoft</a></td><td>Poor</td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Microsoft_Female.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Microsoft_Male.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Microsoft_Male_Dragonkin.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Microsoft_Male_Mechanical.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
</tr><tr>
	<td><a href="https://harposoftware.com/en/2-all-speech-synthesis-products/s-1/brand-ivona">Ivona</a></td><td>Good</td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Ivona_Female.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Ivona_Male.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Ivona_Male_Dragonkin.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Ivona_Male_Mechanical.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
</tr><tr>
<td><a href="https://aws.amazon.com/polly/">Amazon (Neural)</a></td><td>Best</td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Amazon_Female.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Amazon_Male.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Amazon_Male_Dragonkin.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
<td align="center"><a href="https://www.jocys.com/TTS/Audio/Amazon_Male_Mechanical.mp3" target="_blank"><img src="https://www.jocys.com/TTS/Images/Play.png" /></a></a></td>
</tr>
</table>

### Screenshots

<p style="text-align: center;"><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Small_1.png" /></a> <a href="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon.png" target="_blank"><img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon_Small_GitHub.png" /></a></p>

### YouTube Video (very old version of "WoW Addon" with Ivona voices - 2014-08-06)

[![addon video](http://img.youtube.com/vi/lhBGNJQvbUo/0.jpg)](http://www.youtube.com/watch?v=lhBGNJQvbUo)

### How it Works (new version)

<ol>
<li>"WoW Addon" creates XML message <code>&lt;message&gt;&lt;part&gt;text&lt;/part&gt;&lt;/message&gt;</code> when you open window with text (dialogue, quest, book, etc.) or receive chat message in game. This message can include <code>text</code>, <code>name</code>, <code>gender</code>, <code>effect</code>, <code>group</code>, <code>pitch</code>, <code>rate</code>, <code>volume</code>, etc. and <code>command</code> values.</li>
<li>"WoW Addon" converts XML text message to line of coloured pixels and shows this line inside your in-game window. 1 RGB pixel can hold 3 bytes or 3 ASCII characters or 1 UTF8 Japanese character:<br />
<p><img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon_Pixels_x4.png" /><br />
<img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_WoW_Addon_Pixels.png" /> <img src="http://www.jocys.com/TTS/Images/JocysCom_TTS_Monitor_Display_Small.png" /></p></li>
<li>"TTS Monitor" <i>(must be running in the background)</i> captures this line of coloured pixels and converts back to the text message &gt; then converts it to SAPI XML &gt; then reads it with specific text-to-speech voice, <i>if "Monitor: Display" capturing mode is enabled</i>.</li>
</ol>
<p>Note: Make sure you do not cover this coloured pixel line with other game elements. TTS Monitor supports other message capturing methods and "Cache" option which allows to use manually pre-recorded voices.</p>

### How to Install

<ol>
<li>Exit WoW before installing or updating addon.</li>
<li>Download <a href="http://www.jocys.com/Files/Software/JocysCom-TextToSpeech-WoW.zip">JocysComTextToSpeech-WoW.zip</a> file.</li>
<li>Open "File Explorer" (Win+E), locate downloaded file and open it.</li>
<li>Copy <i>"JocysCom-TextToSpeech-WoW"</i> folder (folder is inside downloaded zip file).
	<p><img src="http://www.jocys.com/Files/Software/Copy.png" /></p></li>
<li>Paste <i>"JocysCom-TextToSpeech-WoW"</i> folder inside <i>"AddOns"</i> folder of WoW.
	<p><i>Usual directory: C:\Program Files\World of Warcraft\_retail_Interface\AddOns</i></p></li>
<li>Install additional TTS voices, if needed. BETTER voices are developed by <a href="http://www.ivona.com" target="_blank">Ivona</a> and distributed by <a href="http://harposoftware.com/en/content/10-trial" target="_blank">Harpo Software</a>.</li>
<li>Run <b>"JocysCom.TextToSpeech.Monitor.exe"</b> file (file is inside pasted folder).
	<p><i>Usual directory: C:\Program Files\World of Warcraft\_retail_\Interface\AddOns\JocysCom-TextToSpeech-WoW\</i></p></li>
<li>When <b>"TTS Monitor"</b> will open, setup voices in <b>[Voices]</b> tab (example).
<p><a href="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices.png" target="_blank"><img src="http://www.jocys.com/Files/Software/JocysCom_TTS_Monitor_Voices_2_2_35.png" /></a></p></li>
<li>Run "World of Warcraft" game.</li>
</ol>

You can read more on how to use "WoW Addon" and "TTS Monitor" here: <a href="https://www.jocys.com/TTS" target="_blank">Jocys.com/TTS</a>
