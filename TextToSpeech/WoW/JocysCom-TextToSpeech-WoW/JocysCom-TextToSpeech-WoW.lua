-- Show or hide frame names: /fstack
--#:\Program Files (x86)\World of Warcraft\WTF\Account\ACCOUNTNAME\SavedVariables.lua - Blizzard's saved variables. 
--#:\Program Files (x86)\World of Warcraft\WTF\Account\ACCOUNTNAME\SavedVariables\JocysCom-TextToSpeech-WoW.lua - Per-account settings for each individual AddOn. 
--#:\Program Files (x86)\World of Warcraft\WTF\Account\ACCOUNTNAME\RealmName\CharacterName\JocysCom-TextToSpeech-WoW.lua - Per-character settings for each individual AddOn. 

-- Debug mode true(enabled) or false(disabled).
local DebugEnabled = false;

-- Set variables.
local addonName = "JocysCom-TextToSpeech-WoW";
local addonPrefix = "JocysComTTS";
local unitName = GetUnitName("player");
local customName = GetUnitName("player");
local unitClass = UnitClass("player");
local realmName = GetRealmName();
local questMessage = nil;
local speakMessage = nil;
local objectivesHeader = nil;
local NameIntro = false;
local arg1 = nil;
local arg2 = nil;
local arg2Number = nil;
local arg2Name = nil;
local realName = false;
local lastArg = nil;
local NPCSex = nil;
local NPCGender = nil;
local stopWhenClosing = 1;
local dashIndex = nil;
local hashIndex = nil;
local group = nil;
local messageType = nil;
local messageLeader = nil;
local messagePlayer = nil;
local messageDoNotDisturb = "Please wait... NPC dialog window is open and text-to-speech is enabled.";
local messageStop = "<message command=\"stop\" />";
local NPCNames = {};
local macroName = "NPCSaveTTSMacro"
local macroIndex = GetMacroIndexByName(macroName);
local macroIcon = "INV_Misc_GroupNeedMore";
local macroMessage = "/targetfriend";

function JocysCom_UpdateMacro()
	--Create macro if it doesn't exist and slot is available.
	macroIndex = GetMacroIndexByName(macroName);
	if macroIndex == 0 then
		local macroIndexOld = GetMacroIndexByName("NPCSaveJocysComTTS");
		if macroIndexOld > 0 then 
			macroIndex = EditMacro(macroIndexOld, macroName, macroIcon, macroMessage, 1);
		else
			local numglobal, numperchar = GetNumMacros();
			if numperchar < 17 then
				macroIndex = CreateMacro(macroName, macroIcon, macroMessage, 1);
				print("|cffffff00" .. macroName .. "|r |cff88aaff in|r |cffffff00" .. unitName .. " Specific Macros|r |cff88aaffcreated.|r");
			end
		end
	end
	--Update macro if exists.
	macroIndex = GetMacroIndexByName(macroName);
	if macroIndex > 0 then
		--Macro settings.  targetlasttarget 
		if setCount(NPCNames) > 0 then
			macroIcon = "INV_Misc_GroupNeedMore";
			macroMessage = "";
			for i, v in pairs(NPCNames) do
				macroMessage = macroMessage .. "/target " .. v .. "\n";
			end
			macroMessage = macroMessage .. "/w " ..  unitName .. " <messageMacro/>\n"; 
		else
			macroIcon = "INV_Misc_GroupLooking";
			macroMessage = "/targetfriend";
		end
		--Update macro.
		macroIndex = EditMacro(macroIndex, macroName, macroIcon, macroMessage, 1);
	end
end

function setContainsSaved(s, k)
	for i, v in pairs(s) do
		if i == k then
			return true;
		end
	end
  return nil;
end

--Count NPCs in the list.
function setCount(s)
  local c = 0;
  for _ in pairs(s) do c = c + 1 end;
  return c;
end

--Remove NPC name from the target list.
function setRemove(s, k)
	if setCount(s) > 0 then
		for i, v in pairs(s) do
			if v == k then
				table.remove(s, i);
			end
		end
	end
end

--Add NPC name to the target list (max 9)
function setInsert(s, k)
	for i, v in pairs(s) do
		if v == k then
			table.remove(s, i);
		end
	end
	table.insert(s, k);
	if setCount(s) > 9 then
	table.remove(s, 1);
	end
end

--Get name of the last NPC in the list.
--function getLast(s)
  --local i = 0;
  --local l = nil;
  --if setCount(s) > 0 then
	--for k, v in pairs(s) do
		--i = i + 1;
		--print(tostring(i) .. ". " .. tostring(v));
		--l = v;
		--end
  --end
	--return l;
--end

--Save NPC name to list.
function addToSet(s, k, v1, v2)
	s[k] = {v1, v2};
end

-- Set text.
function JocysCom_Text_EN()
	-- OptionsFrame title.
	JocysCom_OptionsFrame.TitleText:SetText("Jocys.com Text to Speech World of Warcraft Addon 2.3.4 ( 2019-06-26 )");
	-- CheckButtons (Options) text.
	JocysCom_FilterCheckButton.text:SetText("|cff808080 Hide addon|r |cffffffff<messages>|r |cff808080in chat window.|r");
	JocysCom_SaveCheckButton.text:SetText("|cff808080 Hide addon|r |cffffffffSave in Monitor <NPC>|r |cff808080 " .. macroName .. " related messages.|r");
	JocysCom_LockCheckButton.text:SetText("|cff808080 Lock mini frame with |cffffffff[Options]|r |cff808080and|r |cffffffff[Stop]|r |cff808080buttons. Grab frame by clicking on dark background around buttons.|r");
	JocysCom_MenuCheckButton.text:SetText("|cff808080 [Checked] show menu on the right side of |cffffffff[Options]|r |cff808080button. [Unchecked] show menu on the left side.|r");
	-- Font Strings.
	JocysCom_DialogueScrollFrame_FontString:SetText("When mouse pointer is over this frame...\n\nSCROLL UP will START SPEECH\n\nSCROLL DOWN will STOP SPEECH");
	JocysCom_DescriptionFrameFontString:SetText("Text-to-speech voices, pitch, rate, effects, etc. ... you will find all options in |cff77ccffJocys.Com Text to Speech Monitor|r.\n\nHow it works: When you open NPC dialogue window, |cff77ccffJocys.Com Text to Speech WoW Addon|r creates and sends special addon message to yourself (message includes dialogue text, character name and effect name). Then, |cff77ccffJocys.Com Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.");
	JocysCom_ReplaceNameFontString:SetText("Here you can change your name for text to speech from |cff00ff00" .. unitName .. "|r to something else.");
	JocysCom_MessageForMonitorFrameFontString:SetText("Addon message for |cff77ccffJocys.Com Text to Speech Monitor|r ... it must be runninng in background:");
end

-- Unlock frames.
function JocysCom_OptionsFrame_OnShow()
	JocysCom_StopButtonFrame_Texture:SetVertexColor(0, 0, 0, 0.8);
	JocysCom_DialogueScrollFrame:EnableMouse(true);
	JocysCom_DialogueScrollFrame_Texture:SetVertexColor(0, 0, 0, 0.8);
	JocysCom_DialogueScrollFrame_FontString:Show();
	JocysCom_DialogueScrollFrameResizeButton:Show();
end

-- Lock frames.
function JocysCom_OptionsFrame_OnHide()
	if string.len(JocysCom_ReplaceNameEditBox:GetText()) < 2 then
		JocysCom_ReplaceNameEditBox:SetText(unitName);
	end
	JocysCom_StopButtonFrame_Texture:SetVertexColor(0, 0, 0, 0);
	JocysCom_DialogueScrollFrame:EnableMouse(false);
	JocysCom_DialogueScrollFrame_Texture:SetVertexColor(0, 0, 0, 0);
	JocysCom_DialogueScrollFrame_FontString:Hide();
	JocysCom_DialogueScrollFrameResizeButton:Hide();
end

-- MessageStop function.
function JocysCom_SendChatMessageStop(CloseOrButton, group)
	-- Disable DND <Busy> if checked.
	if JocysCom_DndCheckButton:GetChecked() == true and UnitIsDND("player") == true then
		SendChatMessage("", "DND");
	end
	if CloseOrButton ~= 0 then
		if JocysCom_StartOnOpenCheckButton:GetChecked() ~= true and CloseOrButton ~= 2  then
			return;
		end
	end
	-- Add group value if exists.
	if group == nil or group == "" then
		messageStop = "<message command=\"stop\" />";
	else
		messageStop = "<message command=\"stop\" group=\"" .. group .. "\" />";
	end			
	if (stopWhenClosing == 1 or group ~= nil or group ~= "") and (JocysCom_StopOnCloseCheckButton:GetChecked() == true or (CloseOrButton == 1 or CloseOrButton == 2)) then
		--SendChatMessage(messageStop, "WHISPER", "Common", unitName);
		C_ChatInfo.SendAddonMessage(addonPrefix, messageStop, "WHISPER", unitName);
		stopWhenClosing = 0;
		JocysCom_OptionsEditBox:SetText("|cff808080" .. messageStop .. "|r");
	end
end

-- Send sound intro function.
function JocysCom_SendSoundIntro(group)
	--SendChatMessage("<message command=\"sound\" group=\"" .. group .. "\" />", "WHISPER", "Common", unitName);
	C_ChatInfo.SendAddonMessage(addonPrefix, "<message command=\"sound\" group=\"" .. group .. "\" />", "WHISPER", unitName);
end

-- Register events.
function JocysCom_RegisterEvents()
	JocysCom_OptionsFrame:SetScript("OnEvent", JocysCom_OptionsFrame_OnEvent);
	-- Addon loaded event.
	JocysCom_OptionsFrame:RegisterEvent("ADDON_LOADED");

	-- Chat GOSSIP / Dialogue open frames events.
	JocysCom_OptionsFrame:RegisterEvent("GOSSIP_SHOW");

	-- Chat QUEST events.
	JocysCom_OptionsFrame:RegisterEvent("QUEST_GREETING");
	JocysCom_OptionsFrame:RegisterEvent("QUEST_DETAIL");
	JocysCom_OptionsFrame:RegisterEvent("QUEST_PROGRESS");
	JocysCom_OptionsFrame:RegisterEvent("QUEST_COMPLETE");

	-- Books, scrolls, saved copies of mail messages, plaques, gravestones events.
	JocysCom_OptionsFrame:RegisterEvent("ITEM_TEXT_READY");
	
	-- Chat GOSSIP / Dialogue close frames.
	JocysCom_OptionsFrame:RegisterEvent("GOSSIP_CLOSED"); --"QUEST_FINISHED", "QUEST_ACCEPTED"

	-- Chat ADDON events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_ADDON"); 

	-- Chat MONSTER events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_EMOTE");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_PARTY");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_SAY");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_WHISPER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_YELL");
	
	-- Chat WHISPER events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_WHISPER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_WHISPER_INFORM");
	-- Chat WHISPER_BN events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_BN_WHISPER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_BN_WHISPER_INFORM");

	-- Chat EMOTE events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_EMOTE");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_TEXT_EMOTE");
	-- Chat SAY events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_SAY");
	-- Chat YELL events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_YELL");
	-- Chat PARTY events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_PARTY");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_PARTY_LEADER");
	-- Chat GUILD / OFFICER events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_GUILD");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_OFFICER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_GUILD_ACHIEVEMENT");
	-- Chat RAID events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_RAID");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_RAID_LEADER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_RAID_WARNING");
	-- Chat BATTLEGROUND events.
	-- JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_BATTLEGROUND");
	-- JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_BATTLEGROUND_LEADER");
	-- Chat INSTANCE events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_INSTANCE_CHAT");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_INSTANCE_CHAT_LEADER");
	-- Target event.
	JocysCom_OptionsFrame:RegisterEvent("PLAYER_TARGET_CHANGED");
	-- Logout event.
	JocysCom_OptionsFrame:RegisterEvent("PLAYER_LEAVING_WORLD");
	JocysCom_OptionsFrame:RegisterEvent("PLAYER_LOGOUT");
end

function JocysCom_OptionsFrame_OnEvent(self, event, arg1, arg2)
	-- don't proceed messages with <message> tags and your own incoming whispers.
	if event == "CHAT_MSG_WHISPER" and string.find(arg1, "<messageMacro") then
		NPCNames = {};
		JocysCom_UpdateMacro();
	end

	if string.find(tostring(arg1), "<message") ~= nil then
		return;
	end
	-- Reset realName and presenceID.
	realName = false;
	-- Events.  
	if event == "ADDON_LOADED" and arg1 == addonName then
		JocysCom_LoadTocFileSettings();
		JocysCom_UpdateMacro();
		return;
	elseif event == "PLAYER_LOGOUT" then
		JocysCom_SaveTocFileSettings();
		return;
	elseif event == "PLAYER_LEAVING_WORLD" then
		JocysCom_UpdateMacro();
		return;
	elseif event == "PLAYER_TARGET_CHANGED" then
		JocysCom_SaveNPC();
		return;
	elseif event == "CHAT_MSG_ADDON" and arg1 == addonPrefix and JocysCom_FilterCheckButton:GetChecked() ~= true then
		print("|cff88aaff" .. arg1 .. "|r " .. arg2);
		return;
	elseif JocysCom_DialogueMiniFrame:IsShown() and (string.find(event, "QUEST") ~= nil or event == "GOSSIP_SHOW" or event == "ITEM_TEXT_READY") then
		group = "Quest";
		questMessage = nil;
		speakMessage = nil;
		objectivesHeader = nil;
		if event == "QUEST_GREETING" then 
			speakMessage = GetGreetingText();
		elseif GossipFrame:IsShown() and event == "GOSSIP_SHOW" then
			speakMessage = GetGossipText();
		elseif event == "QUEST_DETAIL" then
			objectivesHeader = QuestInfoObjectivesHeader:GetText();
			if (objectivesHeader == nil) then
				objectivesHeader = "";
			else
				objectivesHeader = string.gsub(objectivesHeader, "Quest ", "Your ");
				objectivesHeader = objectivesHeader .. ".";
			end
			if JocysCom_ObjectivesCheckButton:GetChecked() == true then		
				speakMessage = GetQuestText() .. " " .. objectivesHeader .. " " .. GetObjectiveText();
			else
				speakMessage = GetQuestText();
			end
		elseif event == "QUEST_PROGRESS" then
			speakMessage = GetProgressText();
		elseif event == "QUEST_COMPLETE" then
			speakMessage = GetRewardText();
		elseif event == "ITEM_TEXT_READY" then
			speakMessage = ItemTextGetText();
		end	
		arg2 = GetUnitName("npc");
		questMessage = speakMessage;
		if JocysCom_NameQuestCheckButton:GetChecked() == true then
			NameIntro = true
			questMessage = arg2 .. " says. " .. speakMessage;
		end	
		if JocysCom_QuestCheckButton:GetChecked() ~= true then return end -- Don't proceed if "auto-start" speech check-box is disabled.
	elseif event == "GOSSIP_CLOSED" then
		JocysCom_DialogueMiniFrame_Hide();
		return;
	-- Chat events.

	elseif JocysCom_MonsterCheckButton:GetChecked() == true and string.find(event, "MSG_MONSTER") ~= nil then	
		-- don't proceed repetitive NPC messages by the same NPC.
		if (lastArg == arg2 .. arg1) then return else lastArg = arg2 .. arg1 end
		group = "Monster";
		if JocysCom_SoundMonsterCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameMonsterCheckButton:GetChecked() == true or (event == "CHAT_MSG_MONSTER_EMOTE") then NameIntro = true end
	elseif JocysCom_WhisperCheckButton:GetChecked() == true and (event == "CHAT_MSG_WHISPER" or event == "CHAT_MSG_WHISPER_INFORM" or event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_BN_WHISPER_INFORM") then --or event == "CHAT_MSG_BN_CONVERSATION"
		group = "Whisper";
		if JocysCom_SoundWhisperCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameWhisperCheckButton:GetChecked() == true then NameIntro = true end
		-- replace friend's real name with character name.
		if event == "CHAT_MSG_BN_WHISPER_INFORM" then
			-- print(tostring(event));
			arg2 = GetUnitName("player");
		elseif event == "CHAT_MSG_BN_WHISPER" then --or event == "CHAT_MSG_BN_CONVERSATION"
			-- print(tostring(event));
			-- bnetIDAccount, accountName, battleTag, isBattleTagPresence, characterName, bnetIDGameAccount, client, isOnline, lastOnline, isAFK, isDND, messageText, noteText, isRIDFriend, messageTime, canSoR, isReferAFriend, canSummonFriend = BNGetFriendInfo(friendIndex)
			-- Extract friend's presenceID "|Kg00|kTestas|k".
			-- |K[gsf][0-9]+|k[0]+|k
			-- The 3rd character indicates given name, surname, or full name.
			-- The number which follows it represents the friend's Bnet Presence ID.
			-- The zeros between the |k form a string of the same length as the name which will replace it. E.g. if your first name is John and your presence id is 30, your given name (John) would be represented by the string |Kg30|k0000|k

            arg2Number = string.match(arg2, "|K.(%d*)|k");
            _, accountName, battleTag, _, characterName = BNGetFriendInfo(arg2Number);

			print(arg2Number);
			print(accountName); 
			print(battleTag);
			print(characterName);

			-- Set name.
			if characterName ~= nil then
				arg2 = characterName;
				realName = false;
			elseif accountName ~= nil then
				arg2 = accountName;
				realName = true;
			elseif battleTag ~= nil then
				hashIndex = string.find(battleTag, "#");
				if hashIndex ~= nil then arg2 = string.sub(arg2, 1, hashIndex - 1) end
				realName = true;
			else
				arg2 = "Your friend";
				realName = true;
			end
		end	
	elseif JocysCom_EmoteCheckButton:GetChecked() == true and ((event == "CHAT_MSG_EMOTE") or (event == "CHAT_MSG_TEXT_EMOTE")) then group = "Emote";	
		if JocysCom_SoundEmoteCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if (event == "CHAT_MSG_EMOTE") then NameIntro = true end
	elseif JocysCom_SayCheckButton:GetChecked() == true and (event == "CHAT_MSG_SAY") then group = "Say";
		if JocysCom_SoundSayCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameSayCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_YellCheckButton:GetChecked() == true and (event == "CHAT_MSG_YELL") then group = "Yell";
		if JocysCom_SoundYellCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameYellCheckButton:GetChecked() == true then NameIntro = true end
	-- GUILD / OFFICER.
	elseif JocysCom_GuildCheckButton:GetChecked() == true and (event == "CHAT_MSG_GUILD") then group = "Guild";
		if JocysCom_SoundGuildCheckButton:GetChecked() == true then	JocysCom_SendSoundIntro(group) end
		if JocysCom_NameGuildCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_OfficerCheckButton:GetChecked() == true and (event == "CHAT_MSG_OFFICER") then group = "Officer";
		if JocysCom_SoundOfficerCheckButton:GetChecked() == true then	JocysCom_SendSoundIntro(group) end
		if JocysCom_NameOfficerCheckButton:GetChecked() == true then NameIntro = true end
	-- RAID.
	elseif JocysCom_RaidCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID") then group = "Raid";
		if JocysCom_SoundRaidCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameRaidCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_RaidLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING") then group = "RaidLeader";
		if JocysCom_SoundRaidLeaderCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameRaidLeaderCheckButton:GetChecked() == true then NameIntro = true end
	-- PARTY.	
	elseif JocysCom_PartyCheckButton:GetChecked() == true and (event == "CHAT_MSG_PARTY") then group = "Party";
		if JocysCom_SoundPartyCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NamePartyCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_PartyLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_PARTY_LEADER") then group = "PartyLeader";
		if JocysCom_SoundPartyLeaderCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NamePartyLeaderCheckButton:GetChecked() == true then NameIntro = true end
	-- BATTLEGROUND.
	-- elseif JocysCom_BattlegroundCheckButton:GetChecked() == true and string.find(event, "MSG_BATTLEGROUND") ~= nil then group = "Battleground";
		-- if JocysCom_SoundBattlegroundCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		-- if JocysCom_NameBattlegroundCheckButton:GetChecked() == true then NameIntro = true end
	-- elseif JocysCom_BattlegroundLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_BATTLEGROUND_LEADER") then group = "BattlegroundLeader";
		-- if JocysCom_SoundBattlegroundLeaderCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		-- if JocysCom_NameBattlegroundLeaderCheckButton:GetChecked() == true then NameIntro = true end
	-- INSTANCE.
	elseif JocysCom_InstanceCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT") then group = "Instance";
		if JocysCom_SoundInstanceCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameInstanceCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_InstanceLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then group = "InstanceLeader";
		if JocysCom_SoundInstanceLeaderCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameInstanceLeaderCheckButton:GetChecked() == true then NameIntro = true end
	else
		return;
	end
	-- If event CHAT, speakMessage is arg1.
	if string.find(event, "CHAT") ~= nil then
		speakMessage = arg1;
	end
	-- Remove realm name from name.
    if arg2 ~= nil then dashIndex = string.find(arg2, "-") else dashIndex = nil end
	if dashIndex ~= nil then arg2 = string.sub(arg2, 1, dashIndex - 1) end
	-- add leader intro.
	messageLeader = "";
	if string.find(group, "Leader") ~= nil then
	messageLeader = string.gsub(group, "Leader", " leader");
	end
	-- Set "whispers", "says" or "yells".
	messageType = "";
	if string.find(event, "QUEST") ~= nil or event == "GOSSIP_SHOW" or event == "ITEM_TEXT_READY" or (string.find(event, "CHAT") ~= nil and string.find(event, "EMOTE") == nil) then messageType = " says. " end
	if string.find(event, "WHISPER") ~= nil then messageType = " whispers. " end
	if string.find(event, "YELL") ~= nil then messageType = " yells. " end

	-- Final message.
	if (NameIntro == true) then
		speakMessage = messageLeader .. arg2 .. " " .. messageType .. speakMessage;
	else
		speakMessage = speakMessage;
	end
	NameIntro = false;
	JocysCom_SpeakMessage(speakMessage, event, arg2, group, realName);
end

function JocysCom_Replace(m)
	m = m .. " ";
	-- Remove HTML <> tags and text between them.
	if string.find(m, "HTML") ~= nil then
		m = string.gsub(m, "<.->", "");
		m = string.gsub(m, "\"", "");
	end
	-- Remove colors / class / race.
	if string.find(m, "|") ~= nil then
		m = string.gsub(m, "|+", "|");
		-- Remove colors.
		m = string.gsub(m, "|c........", "");
		m = string.gsub(m, "|r", "");
		-- Remove hyperlinks.
		m = string.gsub(m, "|H.-|h", "");
		m = string.gsub(m, "|h", "");
		-- Replace brackets.
		m = string.gsub(m, "%[", " \"");
		m = string.gsub(m, "%]", "\" ");
		-- Fix class / race.
		m = string.gsub(m, "|[%d]+.[%d]+%((.-)%)", "%1");
	end
	-- Remove / Replace.
	m = string.gsub(m, "&", " and ");
	m = string.gsub(m, "%c(%u)", ".%1");
	m = string.gsub(m, "%c", " ");
	m = string.gsub(m, "%s", " ");
    m = string.gsub(m, "%%s ", " ");
	m = string.gsub(m, "[ ]+", " ");
	m = string.gsub(m, "%!+", "!");
	m = string.gsub(m, "%?+", "?");
	m = string.gsub(m, " %.", ".");
	m = string.gsub(m, "%.+", ".");
	m = string.gsub(m, "%!%.", "!");
	m = string.gsub(m, "%?%.", "?");
	m = string.gsub(m, "%?%-", "?");
	m = string.gsub(m, "%?%!%?", "?!");
	m = string.gsub(m, "%!%?%!", "?!");
	m = string.gsub(m, "%!%?", "?!");
	m = string.gsub(m, "%!", "! ");
	m = string.gsub(m, "%?", "? ");
	m = string.gsub(m, " %!", "!");
	m = string.gsub(m, "^%.+", "");
	m = string.gsub(m, ">%.", ".>");
	m = string.gsub(m, "%.+", ". ");
	m = string.gsub(m, "<", " [comment] ");
	m = string.gsub(m, ">", " [/comment] ");
	m = string.gsub(m, "[ ]+", " ");
	m = string.gsub(m, "lvl", "level");
	return m
end

--Messages.
function JocysCom_SpeakMessage(speakMessage, event, name, group, rName)
	if speakMessage == nil then return end
	-- Replace player name.
	local newUnitName = JocysCom_ReplaceNameEditBox:GetText();
	if string.len(newUnitName) > 1 and newUnitName ~= unitName then
		customName = newUnitName;
		speakMessage = string.gsub(speakMessage, unitName, newUnitName);
	else
		customName = unitName;
	end

	-- Send player Name, Class and custom Name to Monitor.
	if string.find(speakMessage, customName) ~= nil or string.find(string.lower(speakMessage), string.lower(unitClass)) ~= nil  then
		messagePlayer = "<message command=\"player\" name=\"" .. unitName .. "," .. customName .. "," .. unitClass ..  "\" />";
		C_ChatInfo.SendAddonMessage(addonPrefix, messagePlayer, "WHISPER", unitName);
	end

	--Replace text in message.
	speakMessage = JocysCom_Replace(speakMessage);
	if speakMessage == nil then return end
	--If not chat event.
	local NPCName = nil;
	local NPCSex = nil;
	if string.find(event, "CHAT") ~= nil then
		NPCName = name;
		if string.find(event, "CHAT_MSG_MONSTER") == nil or rName == false then
			NPCSex = UnitSex(name);
		else
			--Update macro.
			setInsert(NPCNames, name);
			JocysCom_UpdateMacro();
		end
	else
		NPCName = GetUnitName("npc");
		NPCSex = UnitSex("npc");
		if JocysCom_StartOnOpenCheckButton:GetChecked() == true then 	
			JocysCom_SendChatMessageStop(1); -- Send stop message.
		end
		if (string.find(event, "QUEST") ~= nil or event == "GOSSIP_SHOW" or event == "ITEM_TEXT_READY") and JocysCom_SoundQuestCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
	end
	-- Set NPC name.
	if NPCName == nil then
		NPCName = "";
	else
		NPCName = string.gsub(NPCName, "&", " and ");
		NPCName = string.gsub(NPCName, "\"", "");
		NPCName = " name=\"" .. tostring(NPCName) .. "\"";
	end
	-- Set NPC gender. 
	if NPCSex == nil then
		NPCSex = "";
	else
		if NPCSex == 2 then NPCSex = "Male";
		elseif NPCSex == 3 then NPCSex = "Female";
		else NPCSex = "Neutral" end
		NPCSex = " gender=\"" .. NPCSex .. "\"";  
	end
	-- Set NPC type (Beast, Dragonkin, Demon, Elemental, Giant, Undead, Humanoid, Critter, Mechanical, Not specified, Totem, Non-combat Pet, Gas Cloud).							
	local NPCType = UnitCreatureType("npc");
	if NPCType == nil then
		NPCType = "";
	else
		NPCType = " effect=\"" .. tostring(NPCType) .. "\"";
	end
	-- Set group.
	if group == nil then
		group = "";
	else
		group = " group=\"" .. group .. "\"";
	end
	-- Format and send whisper message.
		local chatMessageSP = "<message command=\"play\"" .. group .. NPCName .. NPCSex .. NPCType .. "><part>";
		local chatMessageSA = "<message command=\"add\"><part>";
		local chatMessageE = "</part></message>";
		local chatMessage;
		local chatMessageLimit = 240;
		local sizeAdd = chatMessageLimit - string.len(chatMessageSA) - string.len(chatMessageE) - string.len(addonPrefix);
		local sizePlay = chatMessageLimit - string.len(chatMessageSP) - string.len(chatMessageE) - string.len(addonPrefix);
		if DebugEnabled then print("Add message size: " ..  sizeAdd .. " Play message size: " .. sizePlay) end	
		local startIndex = 1;
		local endIndex = 1;
		local part = "";
		local speakMessageLen = string.len(speakMessage);
		local speakMessageRemainingLen = speakMessageLen;
		while true do
			local command = "";
			local index = string.find(speakMessage, " ", endIndex);
			if index == speakMessageLen or index == nil then
				index = speakMessageLen;
				endIndex = speakMessageLen + 1; -- 0-1000
			end
			-- If text length less than 100 then...
			if speakMessageRemainingLen <= sizePlay then
				part = string.sub(speakMessage, startIndex);
				chatMessage = chatMessageSP .. part .. chatMessageE;
				stopWhenClosing = 1;
				C_ChatInfo.SendAddonMessage(addonPrefix, chatMessage, "WHISPER", unitName);
				if DebugEnabled then print("[" .. tostring(index) .. "] [" .. startIndex .. "] '" .. part .. "'") end
				break;
			-- If text length more than 100 then...
			elseif (index - startIndex) > sizeAdd or (index >= speakMessageLen and speakMessageRemainingLen > sizePlay) then
				-- If space is out of size then...
				part = string.sub(speakMessage, startIndex, endIndex - 1);
				chatMessage = chatMessageSA .. part .. chatMessageE;
				stopWhenClosing = 1;
				C_ChatInfo.SendAddonMessage(addonPrefix, chatMessage, "WHISPER", unitName);
				if DebugEnabled then print("[" .. tostring(index) .. "] [" .. startIndex .. "-" .. (endIndex - 1) .. "] '" .. part .. "'") end
				startIndex = endIndex;
				speakMessageRemainingLen = string.len(string.sub(speakMessage, startIndex));
			end
			-- look for next space.
			endIndex = index + 1;
		end
		-- Fill EditBox.
		local questEditBox = string.gsub(speakMessage, "%[comment]", "|cff808080[comment]|r|cfff7e593");
		questEditBox = string.gsub(questEditBox, "%[/comment]", "|r|cff808080[/comment]|r");
		questEditBox = "|cff808080" .. chatMessageSP .. "|r\n" .. questEditBox .. "\n|cff808080" .. chatMessageE .. "|r";
		JocysCom_OptionsEditBox:SetText(questEditBox);
		-- Enable DND <Busy> if checked.
		if JocysCom_DndCheckButton:GetChecked() == true and string.find(event, "CHAT") == nil then
		--if UnitIsDND("player") == false then
		SendChatMessage("<" .. unitName .. ">: " .. messageDoNotDisturb, "DND");
		--end		
		end
end

-- Play sound and save settings.
function JocysCom_PlaySoundAndSaveSettings()
	PlaySound(856);
	JocysCom_SaveTocFileSettings();
end

-- DND.
function JocysCom_DndCheckButton_OnClick(self)
	local DNDIsShown = JocysCom_DialogueMiniFrame:IsShown();
	PlaySound(856);
	 -- Disable DND.
	if JocysCom_DndCheckButton:GetChecked() == false then
		if UnitIsDND("player") == true then
			SendChatMessage("", "DND");
		end
	else
		if UnitIsDND("player") == false and DNDIsShown == true then
			SendChatMessage("<" .. unitName .. ">: " .. messageDoNotDisturb, "DND");
		end	
	end
	JocysCom_SaveTocFileSettings();
end

-- Lock Enable / Disable.
function JocysCom_LockCheckButton_OnClick()
	PlaySound(856);
	if JocysCom_LockCheckButton:GetChecked() == true then
		JocysCom_StopButtonFrame:RegisterForDrag();
	else
		JocysCom_StopButtonFrame:RegisterForDrag("LeftButton");
	end
	JocysCom_SaveTocFileSettings();
end

 -- StopButtonFrame position Right or Left.
function JocysCom_MenuCheckButton_OnClick()
	PlaySound(856);
 	JocysCom_MiniMenuFrame:ClearAllPoints();
	if JocysCom_MenuCheckButton:GetChecked() == true then
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMLEFT", JocysCom_StopButtonFrame, "BOTTOMRIGHT", -6, 1);
	else
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMRIGHT", JocysCom_StopButtonFrame, "BOTTOMLEFT", 2, 1);
	end
	JocysCom_SaveTocFileSettings();
end

-- Enable disable check-boxes (speech).
function JocysCom_CheckButton_OnClick(self, name)
	PlaySound(856);
	if self:GetChecked() == true then
		-- if name == "Party" then
			-- JocysCom_PartyLeaderCheckButton:SetChecked(false);
		-- elseif name == "PartyLeader" and JocysCom_PartyCheckButton:GetChecked() == true then
			-- JocysCom_PartyCheckButton:SetChecked(false);
			-- JocysCom_SendChatMessageStop(1, "Party");
		-- elseif name == "PartyLeader" and JocysCom_PartyCheckButton:GetChecked() == true then
			-- JocysCom_PartyCheckButton:SetChecked(false);
			-- JocysCom_SendChatMessageStop(1, "Party");
		-- elseif name == "Battleground" then
			-- JocysCom_BattlegroundLeaderCheckButton:SetChecked(false);
		-- elseif name == "BattlegroundLeader" and JocysCom_BattlegroundCheckButton:GetChecked() == true  then
			-- JocysCom_BattlegroundCheckButton:SetChecked(false);
			-- JocysCom_SendChatMessageStop(1, "Battleground");
		-- elseif name == "Raid" then
			-- JocysCom_RaidLeaderCheckButton:SetChecked(false);
		-- elseif name == "RaidLeader" and JocysCom_RaidCheckButton:GetChecked() == true then JocysCom_RaidCheckButton:SetChecked(false);
			-- JocysCom_SendChatMessageStop(1, "Raid");		
		-- elseif name == "Instance" then
			-- JocysCom_InstanceLeaderCheckButton:SetChecked(false);
		-- elseif name == "InstanceLeader" and JocysCom_InstanceCheckButton:GetChecked() == true then
			-- JocysCom_InstanceCheckButton:SetChecked(false);
			-- JocysCom_SendChatMessageStop(1, "Instance");
		-- end 
	else
		JocysCom_SendChatMessageStop(1, name);
	end
	JocysCom_SaveTocFileSettings();
end

-- Show MiniMenuFrame and set text.
 function JocysCom_MiniMenuFrame_Show(name)
 	local fontString = "";
	JocysCom_MiniMenuFrame_FontString:Show();
	if name == "Options" then
		fontString = "|cffddddddMouse over [=] shows Quick Menu.\nMouse click opens Options Window.|r";
	elseif name == "Save" then
		fontString = "|cffddddddSave target name, gender, type in Monitor.\n|r";
	elseif name == "Stop" then
		fontString = "|cffddddddStop text-to-speech and clear all playlist.\n|r";
	elseif name == "Busy" then
		fontString = "|cff6464ffShow <Busy> over your character\nwhen window is open and speech is on.|r";
	elseif name == "Objectives" then
		fontString = "|cffefc176Include QUEST OBJECTIVES\nin text-to-speech.|r";
	elseif name == "StartOnOpen" then
		fontString = "|cffefc176Instantly START to play DIALOGUE,\nBOOK, etc. text on opening window.|r";
	elseif name == "StopOnClose" then
		fontString = "|cffefc176Instantly STOP to play DIALOGUE,\nBOOK, etc. text on closing window.|r";
	elseif name == "Quest" then
		fontString = "|cffefc176Play DIALOGUE, BOOK, PLAQUE, etc.\nwindow text.|r";
	elseif name == "Monster" then
		fontString = "|cfffffb9fPlay NPC chat messages.\n|r";
	elseif name == "Whisper" then
		fontString = "|cffffb2ebPlay WHISPER chat messages.\n|r";
	elseif name == "Emote" then
		fontString = "|cffff6b26Play player EMOTE  chat messages.\n|r";
	elseif name == "Say" then
		fontString = "|cffffffffPlay SAY chat messages.\n|r";
	elseif name == "Yell" then
		fontString = "|cffff3f40Play YELL chat messages.\n|r";
	elseif name == "Guild" then
		fontString = "|cff40fb40Play GUILD member chat messages.\n|r";
	elseif name == "Officer" then
		fontString = "|cff40fb40Play GUILD OFFICER chat messages.\n|r";
	elseif name == "RaidLeader" then
		fontString = "|cffff4709Play RAID LEADER chat messages.\n|r";
	elseif name == "Raid" then
		fontString = "|cffff7d00Play RAID member chat messages.\n|r";

	elseif name == "PartyLeader" then
		fontString = "|cffaaa7ffPlay PARTY LEADER chat messages.\n|r";

	elseif name == "Party" then
		fontString = "|cffaaa7ffPlay PARTY member chat messages.\n|r";


	-- elseif name == "Battleground" then
		-- fontString = "|cffff7d00Play BATTLEGROUND chat messages.\n|r";
	-- elseif name == "BattlegroundLeader" then
		-- fontString = "|cffff4709Play BATTLEGROUND LEADER chat messages.\n|r";

	elseif name == "InstanceLeader" then
		fontString = "|cffff4709Play INSTANCE LEADER chat messages.\n|r";
	elseif name == "Instance" then
		fontString = "|cffff7d00Play INSTANCE member chat messages.\n|r";
	-- Play intro sound check-boxes.
	elseif name == "SoundQuest" then
		fontString = "|cffefc176Play intro sound at the beginning of\nDIALOGUE, BOOK, etc. window text.|r";
	elseif name == "SoundMonster" then
		fontString = "|cfffffb9fPlay intro sound at the beginning of\nNPC messages.|r";
	elseif name == "SoundWhisper" then
		fontString = "|cffffb2ebPlay intro sound at the beginning of\nWHISPER messages.|r";
		elseif name == "SoundEmote" then
		fontString = "|cffff6b26Play intro sound at the beginning of\nplayer EMOTE messages.|r";
	elseif name == "SoundSay" then
		fontString = "|cffffffffPlay intro sound at the beginning of\nSAY messages.|r";
	elseif name == "SoundYell" then
		fontString = "|cffff3f40Play intro sound at the beginning of\nYELL messages.|r";
	elseif name == "SoundOfficer" then
		fontString = "|cff40fb40Play intro sound at the beginning of\nGUILD OFFICER messages.|r";
	elseif name == "SoundGuild" then
		fontString = "|cff40fb40Play intro sound at the beginning of\nGUILD member messages.|r";
	elseif name == "SoundRaidLeader" then
		fontString = "|cffff4709Play intro sound at the beginning of\nRAID LEADER messages.|r";
	elseif name == "SoundRaid" then
		fontString = "|cffff7d00Play intro sound at the beginning of\nRAID member messages.|r";

	elseif name == "SoundPartyLeader" then
		fontString = "|cffaaa7ffPlay intro sound at the beginning of\nPARTY LEADER messages.|r";
	elseif name == "SoundParty" then
		fontString = "|cffaaa7ffPlay intro sound at the beginning of\nPARTY member messages.|r";

	-- elseif name == "SoundBattlegroundLeader" then
		-- fontString = "|cffff4709Play intro sound at the beginning of\nBATTLEGROUND LEADER messages.|r";
	-- elseif name == "SoundBattleground" then
		-- fontString = "|cffff7d00Play intro sound at the beginning of\nBATTLEGROUND member messages.|r";

	elseif name == "SoundInstanceLeader" then
		fontString = "|cffff4709Play intro sound at the beginning of\nINSTANCE LEADER messages.|r";
	elseif name == "SoundInstance" then
		fontString = "|cffff7d00Play intro sound at the beginning of\nINSTANCE member messages.|r";
	-- Add name check-boxes.
	elseif name == "NameQuest" then
		fontString = "|cffefc176Add \"<CharacterName> says.\" to\nDIALOGUE, BOOK, etc. window text.|r";
	elseif name == "NameMonster" then
		fontString = "|cfffffb9fAdd \"<Name> whispers \\ says \\ yells.\" to\nNPC messages.|r";
	elseif name == "NameWhisper" then
		fontString = "|cffffb2ebAdd \"<CharacterName> whispers.\" to\nWHISPER messages.|r";
	elseif name == "NameSay" then
		fontString = "|cffffffffAdd \"<CharacterName> says.\" to\nSAY messages.|r";
	elseif name == "NameYell" then
		fontString = "|cffff3f40Add \"<CharacterName> yells.\" to\nYELL messages.|r";
	elseif name == "NameOfficer" then
		fontString = "|cff40fb40Add \"<CharacterName> says.\" to\nGUILD OFFICER messages.|r";
	elseif name == "NameGuild" then
		fontString = "|cff40fb40Add \"<CharacterName> says.\" to\nGUILD memeber messages.|r";
	elseif name == "NameRaidLeader" then
		fontString = "|cffff4709Add \"<CharacterName> says.\" to\nRAID LEADER messages.|r";
	elseif name == "NameRaid" then
		fontString = "|cffff7d00Add \"<CharacterName> says.\" to\nRAID member messages.|r";
	elseif name == "NamePartyLeader" then
		fontString = "|cffaaa7ffAdd \"<CharacterName> says.\" to\nPARTY LEADER messages.|r";
	elseif name == "NameParty" then
		fontString = "|cffaaa7ffAdd \"<CharacterName> says.\" to\nPARTY memeber messages.|r";
	-- elseif name == "NameBattlegroundLeader" then
		-- fontString = "|cffff4709Add \"<CharacterName> says.\" to\nBATTLEGROUND LEADER messages.|r";
	-- elseif name == "NameBattleground" then
		-- fontString = "|cffff7d00Add \"<CharacterName> says.\" to\nBATTLEGROUND memeber messages.|r";
	elseif name == "NameInstanceLeader" then
		fontString = "|cffff4709Add \"<CharacterName> says.\" to\nINSTANCE LEADER messages.|r";
	elseif name == "NameInstance" then
		fontString = "|cffff7d00Add \"<CharacterName> says.\" to\nINSTANCE memeber messages.|r";
	else
		JocysCom_MiniMenuFrame_FontString:Hide();
	end
	JocysCom_MiniMenuFrame_FontString:SetText(fontString);
	JocysCom_MiniMenuFrameBorder:SetBackdrop( { edgeFile = "Interface/AddOns/JocysCom-TextToSpeech-WoW/Images/JocysCom-MiniMenuFrame-Border", edgeSize = 23 });
	JocysCom_MiniMenuFrame:Show();
	--JocysCom_OptionsButton:Show();
	--JocysCom_SaveButton:Show();
 end

 -- Hide MiniMenuFrame.
 function JocysCom_MiniMenuFrame_Hide()
	JocysCom_MiniMenuFrame:Hide();
	--JocysCom_OptionsButton:Hide();
	--JocysCom_SaveButton:Hide();
 end
 
-- [ Stop ] dialog button.
function JocysCom_StopButton_OnClick(name)
	PlaySound(856);
	if name == "Quest" then
		JocysCom_SendChatMessageStop(2, name);
	else
		JocysCom_SendChatMessageStop(2);
	end
end

-- ScrollFrame - scroll-up or scroll-down.
function JocysCom_DialogueScrollFrame_OnMouseWheel(self, delta)
	if delta == 1 then
			JocysCom_PlayButtonButton_OnClick();
		else
			JocysCom_SendChatMessageStop(2, "Quest");
		end
end

-- Show or Hide JocysCom frames.
function JocysCom_AttachAndShowFrames(frame)
	-- Set ScrollFrame.
	local width = 100;
	JocysCom_DialogueScrollFrame:ClearAllPoints();
	-- Different scrollFrame position and size... if frame is WorldMapFrame.	
	if frame == WorldMapFrame then
	width = QuestMapDetailsScrollFrame:GetWidth();
	JocysCom_DialogueScrollFrame:SetParent(QuestMapDetailsScrollFrame);
	JocysCom_DialogueScrollFrame:SetPoint("TOPLEFT", 0, -4);
	JocysCom_DialogueScrollFrame:SetWidth(width - 4);
	else
	width = frame:GetWidth();
	JocysCom_DialogueScrollFrame:SetParent(frame);
	JocysCom_DialogueScrollFrame:SetWidth(width - 50);
	JocysCom_DialogueScrollFrame:SetPoint("TOPLEFT", 12, -67);
	end
	JocysCom_DialogueScrollFrame:SetFrameLevel(100);
	JocysCom_DialogueScrollFrame:Show();
	-- Set MiniFrame.		
	JocysCom_DialogueMiniFrame:ClearAllPoints();
	JocysCom_DialogueMiniFrame:SetParent(frame);
	JocysCom_DialogueMiniFrame:SetPoint("TOPRIGHT", frame, "BOTTOMRIGHT", 0, 0);
	JocysCom_DialogueMiniFrame:SetBackdrop( { bgFile="Interface/AddOns/JocysCom-TextToSpeech-WoW/Images/JocysCom-DialogueMiniFrame-Background" });
	JocysCom_DialogueMiniFrame:Show();
end

-- Close all JocysCom frames.
function JocysCom_DialogueMiniFrame_OnHide()
	JocysCom_SendChatMessageStop(0, "Quest");
end

function JocysCom_DialogueMiniFrame_Hide()
	JocysCom_DialogueMiniFrame:Hide();
end

-- [ Options ] button.
function JocysCom_OptionsButton_OnClick()
	if JocysCom_OptionsFrame:IsShown() then
		JocysCom_OptionsFrame:Hide();
	else
		JocysCom_OptionsFrame:Show();
	end
end

-- [ Save ] button.
function JocysCom_SaveNPC()
	if UnitIsPlayer("target") or UnitPlayerControlled("target") or GetUnitName("target") == nil or UnitSex("target") == nil then 
		if JocysCom_SaveCheckButton:GetChecked() ~= true then
		print("|cff888888Only uncontrollable by players NPC targets will be saved.|r");
		end
	else
		-- Target Name.
		local name = GetUnitName("target");
		local targetName = string.gsub(name, "&", " and ");
		targetName = string.gsub(targetName, "\"", "");
		-- Target Gender.
		local targetSex = UnitSex("target");
		if targetSex == 2 then targetSex = "Male";
		elseif targetSex == 3 then targetSex = "Female";
		else targetSex = "Neutral"; end
		-- Target Type.
		local targetType = UnitCreatureType("target");
		if targetType == nil then
			targetType = "";
		end
		local saveMessage = "<message command=\"Save\" name=\"" .. targetName .. "\" gender=\"" .. targetSex .. "\" effect=\"" .. targetType .. "\" />";
		--Send message for "Monitor".
		C_ChatInfo.SendAddonMessage(addonPrefix, saveMessage, "WHISPER", unitName);
		--Fill "Options" window EditBox.
		JocysCom_OptionsEditBox:SetText("|cff808080" .. saveMessage .. "|r");
		--Print information in to chat window.
		if JocysCom_SaveCheckButton:GetChecked() ~= true then
		print("|cffffff20Save in Monitor: " .. targetName .. " : " .. targetSex .. " : " .. targetType .. "|r");
		end
	end
end

-- [ Play ] button.
function JocysCom_PlayButtonButton_OnClick(self)
	if QuestLogPopupDetailFrame:IsShown() or WorldMapFrame:IsShown() then
		local questDescription, questObjectives = GetQuestLogQuestText();
		questMessage = questObjectives .. " Description. " .. questDescription;
		-- questPortrait, questPortraitText, questPortraitName = GetQuestLogPortraitGiver();
	end
	JocysCom_SpeakMessage(questMessage, "SROLL_UP_OR_PLAY_BUTTON", "", "Quest");
end

-- Enable/Disable message filter.
function JocysCom_CHAT_MSG_WHISPER(self, event, msg)
	if string.find(msg, "<message") ~= nil then
		return true;
	end
end
-- Enable/Disable message filter
function JocysCom_CHAT_MSG_DND(self, event, msg)
	if string.find(msg, "<" .. unitName .. ">: ") ~= nil and JocysCom_FilterCheckButton:GetChecked() == true then
		return true;
	end
end
-- Enable/Disable message filter
function JocysCom_CHAT_MSG_SYSTEM(self, event, msg)
	if string.find(msg, "You are no") ~= nil and JocysCom_FilterCheckButton:GetChecked() == true then
		return true;
	end
end

-- Load and apply settings from toc file.
function JocysCom_LoadTocFileSettings()
	-- Set (Options) CheckButtons.
	if JocysCom_DndCB == false then JocysCom_DndCheckButton:SetChecked(false) else JocysCom_DndCheckButton:SetChecked(true) end
	-- Set LockCheckButton and StopButtonFrame.
	if JocysCom_LockCB == true then JocysCom_LockCheckButton:SetChecked(true) else JocysCom_LockCheckButton:SetChecked(false) end
	if JocysCom_LockCheckButton:GetChecked() == true then JocysCom_StopButtonFrame:RegisterForDrag() else JocysCom_StopButtonFrame:RegisterForDrag("LeftButton") end
	-- Set MenuCheckButton and MiniMenuFrame.
	if JocysCom_MenuCB == false then JocysCom_MenuCheckButton:SetChecked(false) else JocysCom_MenuCheckButton:SetChecked(true) end
	JocysCom_MiniMenuFrame:ClearAllPoints();
	if JocysCom_MenuCheckButton:GetChecked() == true then
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMLEFT", JocysCom_StopButtonFrame, "BOTTOMRIGHT", -6, 1);
	else
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMRIGHT", JocysCom_StopButtonFrame, "BOTTOMLEFT", 2, 1);
	end
	-- Set FilterCheckButton and Message filters.
	if JocysCom_FilterCB == false then JocysCom_FilterCheckButton:SetChecked(false) else JocysCom_FilterCheckButton:SetChecked(true) end
	if JocysCom_SaveCB == false then JocysCom_SaveCheckButton:SetChecked(false) else JocysCom_SaveCheckButton:SetChecked(true) end
	-- Add chat message filters.
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER_INFORM", JocysCom_CHAT_MSG_WHISPER);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER", JocysCom_CHAT_MSG_WHISPER);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_DND", JocysCom_CHAT_MSG_DND);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_SYSTEM", JocysCom_CHAT_MSG_SYSTEM);
	-- Set (MiniFrame) CheckButtons.
	if JocysCom_ReplaceNameEB == "" or JocysCom_ReplaceNameEB == nil then JocysCom_ReplaceNameEB = unitName else JocysCom_ReplaceNameEditBox:SetText(JocysCom_ReplaceNameEB) end
	if JocysCom_QuestCB == false then JocysCom_QuestCheckButton:SetChecked(false) else JocysCom_QuestCheckButton:SetChecked(true) end
	if JocysCom_MonsterCB == true then JocysCom_MonsterCheckButton:SetChecked(true) else JocysCom_MonsterCheckButton:SetChecked(false) end
	if JocysCom_WhisperCB == false then JocysCom_WhisperCheckButton:SetChecked(false) else JocysCom_WhisperCheckButton:SetChecked(true) end
	if JocysCom_EmoteCB == false then JocysCom_EmoteCheckButton:SetChecked(false) else JocysCom_EmoteCheckButton:SetChecked(true) end
	if JocysCom_SayCB == false then JocysCom_SayCheckButton:SetChecked(false) else JocysCom_SayCheckButton:SetChecked(true) end
	if JocysCom_YellCB == false then JocysCom_YellCheckButton:SetChecked(false) else JocysCom_YellCheckButton:SetChecked(true) end
	if JocysCom_GuildCB == false then JocysCom_GuildCheckButton:SetChecked(false) else JocysCom_GuildCheckButton:SetChecked(true) end
	if JocysCom_OfficerCB == false then JocysCom_OfficerCheckButton:SetChecked(false) else JocysCom_OfficerCheckButton:SetChecked(true) end
	if JocysCom_PartyCB == false then JocysCom_PartyCheckButton:SetChecked(false) else JocysCom_PartyCheckButton:SetChecked(true) end
	if JocysCom_PartyLCB == false then JocysCom_PartyLeaderCheckButton:SetChecked(false) else JocysCom_PartyLeaderCheckButton:SetChecked(true) end
	-- if JocysCom_BGCB == false then JocysCom_BattlegroundCheckButton:SetChecked(false) else JocysCom_BattlegroundCheckButton:SetChecked(true) end
	-- if JocysCom_BGLCB == true then JocysCom_BattlegroundLeaderCheckButton:SetChecked(true) else JocysCom_BattlegroundLeaderCheckButton:SetChecked(false) end
	if JocysCom_RaidCB == false then JocysCom_RaidCheckButton:SetChecked(false) else JocysCom_RaidCheckButton:SetChecked(true) end
	if JocysCom_RaidLCB == false then JocysCom_RaidLeaderCheckButton:SetChecked(false) else JocysCom_RaidLeaderCheckButton:SetChecked(true) end
	if JocysCom_InstanceCB == false then JocysCom_InstanceCheckButton:SetChecked(false) else JocysCom_InstanceCheckButton:SetChecked(true) end
	if JocysCom_InstanceLCB == false then JocysCom_InstanceLeaderCheckButton:SetChecked(false) else JocysCom_InstanceLeaderCheckButton:SetChecked(true) end
	if JocysCom_ObjectivesCB == false then JocysCom_ObjectivesCheckButton:SetChecked(false) else JocysCom_ObjectivesCheckButton:SetChecked(true) end
	if JocysCom_StartOnOpenCB == false then JocysCom_StartOnOpenCheckButton:SetChecked(false) else JocysCom_StartOnOpenCheckButton:SetChecked(true) end
	if JocysCom_StopOnCloseCB == false then JocysCom_StopOnCloseCheckButton:SetChecked(false) else JocysCom_StopOnCloseCheckButton:SetChecked(true) end
	-- Set (MiniFrame) Sound CheckButtons.
	if JocysCom_SQuestCB == true then JocysCom_SoundQuestCheckButton:SetChecked(true) else JocysCom_SoundQuestCheckButton:SetChecked(false) end
	if JocysCom_SMonsterCB == false then JocysCom_SoundMonsterCheckButton:SetChecked(false) else JocysCom_SoundMonsterCheckButton:SetChecked(true) end
	if JocysCom_SWhisperCB == false then JocysCom_SoundWhisperCheckButton:SetChecked(false) else JocysCom_SoundWhisperCheckButton:SetChecked(true) end
	if JocysCom_SEmoteCB == false then JocysCom_SoundEmoteCheckButton:SetChecked(false) else JocysCom_SoundEmoteCheckButton:SetChecked(true) end
	if JocysCom_SSayCB == false then JocysCom_SoundSayCheckButton:SetChecked(false) else JocysCom_SoundSayCheckButton:SetChecked(true) end
	if JocysCom_SYellCB == false then JocysCom_SoundYellCheckButton:SetChecked(false) else JocysCom_SoundYellCheckButton:SetChecked(true) end
	if JocysCom_SGuildCB == false then JocysCom_SoundGuildCheckButton:SetChecked(false) else JocysCom_SoundGuildCheckButton:SetChecked(true) end
	if JocysCom_SOfficerCB == false then JocysCom_SoundOfficerCheckButton:SetChecked(false) else JocysCom_SoundOfficerCheckButton:SetChecked(true) end
	if JocysCom_SPartyCB == false then JocysCom_SoundPartyCheckButton:SetChecked(false) else JocysCom_SoundPartyCheckButton:SetChecked(true) end
	if JocysCom_SPartyLCB == false then JocysCom_SoundPartyLeaderCheckButton:SetChecked(false) else JocysCom_SoundPartyLeaderCheckButton:SetChecked(true) end
	-- if JocysCom_SBGCB == false then JocysCom_SoundBattlegroundCheckButton:SetChecked(false) else JocysCom_SoundBattlegroundCheckButton:SetChecked(true) end
	-- if JocysCom_SBGLCB == false then JocysCom_SoundBattlegroundLeaderCheckButton:SetChecked(false) else JocysCom_SoundBattlegroundLeaderCheckButton:SetChecked(true) end
	if JocysCom_SRaidCB == false then JocysCom_SoundRaidCheckButton:SetChecked(false) else JocysCom_SoundRaidCheckButton:SetChecked(true) end
	if JocysCom_SRaidLCB == false then JocysCom_SoundRaidLeaderCheckButton:SetChecked(false) else JocysCom_SoundRaidLeaderCheckButton:SetChecked(true) end
	if JocysCom_SInstanceCB == false then JocysCom_SoundInstanceCheckButton:SetChecked(false) else JocysCom_SoundInstanceCheckButton:SetChecked(true) end
	if JocysCom_SInstanceLCB == false then JocysCom_SoundInstanceLeaderCheckButton:SetChecked(false) else JocysCom_SoundInstanceLeaderCheckButton:SetChecked(true) end
	-- Set (MiniFrame) Name CheckButtons.
	if JocysCom_NQuestCB == true then JocysCom_NameQuestCheckButton:SetChecked(true) else JocysCom_NameQuestCheckButton:SetChecked(false) end
	if JocysCom_NMonsterCB == true then JocysCom_NameMonsterCheckButton:SetChecked(true) else JocysCom_NameMonsterCheckButton:SetChecked(false) end
	if JocysCom_NWhisperCB == true then JocysCom_NameWhisperCheckButton:SetChecked(true) else JocysCom_NameWhisperCheckButton:SetChecked(false) end
	if JocysCom_NSayCB == true then JocysCom_NameSayCheckButton:SetChecked(true) else JocysCom_NameSayCheckButton:SetChecked(false) end
	if JocysCom_NYellCB == true then JocysCom_NameYellCheckButton:SetChecked(true) else JocysCom_NameYellCheckButton:SetChecked(false) end
	if JocysCom_NGuildCB == true then JocysCom_NameGuildCheckButton:SetChecked(true) else JocysCom_NameGuildCheckButton:SetChecked(false) end
	if JocysCom_NOfficerCB == true then JocysCom_NameOfficerCheckButton:SetChecked(true) else JocysCom_NameOfficerCheckButton:SetChecked(false) end
	if JocysCom_NPartyCB == true then JocysCom_NamePartyCheckButton:SetChecked(true) else JocysCom_NamePartyCheckButton:SetChecked(false) end
	if JocysCom_NPartyLCB == true then JocysCom_NamePartyLeaderCheckButton:SetChecked(true) else JocysCom_NamePartyLeaderCheckButton:SetChecked(false) end
	-- if JocysCom_NBGCB == true then JocysCom_NameBattlegroundCheckButton:SetChecked(true) else JocysCom_NameBattlegroundCheckButton:SetChecked(false) end
	-- if JocysCom_NBGLCB == true then JocysCom_NameBattlegroundLeaderCheckButton:SetChecked(true) else JocysCom_NameBattlegroundLeaderCheckButton:SetChecked(false) end
	if JocysCom_NRaidCB == true then JocysCom_NameRaidCheckButton:SetChecked(true) else JocysCom_NameRaidCheckButton:SetChecked(false) end
	if JocysCom_NRaidLCB == true then JocysCom_NameRaidLeaderCheckButton:SetChecked(true) else JocysCom_NameRaidLeaderCheckButton:SetChecked(false) end
	if JocysCom_NInstanceCB == true then JocysCom_NameInstanceCheckButton:SetChecked(true) else JocysCom_NameInstanceCheckButton:SetChecked(false) end
	if JocysCom_NInstanceLCB == true then JocysCom_NameInstanceLeaderCheckButton:SetChecked(true) else JocysCom_NameInstanceLeaderCheckButton:SetChecked(false) end
end

-- Save settings.
function JocysCom_SaveTocFileSettings()
	-- Save check buttons.
	JocysCom_ReplaceNameEB = JocysCom_ReplaceNameEditBox:GetText();
	JocysCom_DndCB = JocysCom_DndCheckButton:GetChecked();
	JocysCom_LockCB = JocysCom_LockCheckButton:GetChecked();
	JocysCom_MenuCB = JocysCom_MenuCheckButton:GetChecked();
	JocysCom_FilterCB = JocysCom_FilterCheckButton:GetChecked();
	JocysCom_SaveCB = JocysCom_SaveCheckButton:GetChecked();
	JocysCom_QuestCB = JocysCom_QuestCheckButton:GetChecked();
	JocysCom_MonsterCB = JocysCom_MonsterCheckButton:GetChecked();
	JocysCom_WhisperCB = JocysCom_WhisperCheckButton:GetChecked();
	JocysCom_EmoteCB = JocysCom_EmoteCheckButton:GetChecked();
	JocysCom_SayCB = JocysCom_SayCheckButton:GetChecked();
	JocysCom_YellCB = JocysCom_YellCheckButton:GetChecked();
	JocysCom_GuildCB = JocysCom_GuildCheckButton:GetChecked();
	JocysCom_OfficerCB = JocysCom_OfficerCheckButton:GetChecked();
	JocysCom_PartyCB = JocysCom_PartyCheckButton:GetChecked();
	JocysCom_PartyLCB = JocysCom_PartyLeaderCheckButton:GetChecked();
	-- JocysCom_BGCB = JocysCom_BattlegroundCheckButton:GetChecked();
	-- JocysCom_BGLCB = JocysCom_BattlegroundLeaderCheckButton:GetChecked();
	JocysCom_RaidCB = JocysCom_RaidCheckButton:GetChecked();
	JocysCom_RaidLCB = JocysCom_RaidLeaderCheckButton:GetChecked();
	JocysCom_InstanceCB = JocysCom_InstanceCheckButton:GetChecked();
	JocysCom_InstanceLCB = JocysCom_InstanceLeaderCheckButton:GetChecked();
	JocysCom_ObjectivesCB = JocysCom_ObjectivesCheckButton:GetChecked();
	JocysCom_StartOnOpenCB = JocysCom_StartOnOpenCheckButton:GetChecked();
	JocysCom_StopOnCloseCB = JocysCom_StopOnCloseCheckButton:GetChecked();
	-- Save sound check buttons.
	JocysCom_SQuestCB = JocysCom_SoundQuestCheckButton:GetChecked();
	JocysCom_SMonsterCB = JocysCom_SoundMonsterCheckButton:GetChecked();
	JocysCom_SWhisperCB = JocysCom_SoundWhisperCheckButton:GetChecked();
	JocysCom_SEmoteCB = JocysCom_SoundEmoteCheckButton:GetChecked();
	JocysCom_SSayCB = JocysCom_SoundSayCheckButton:GetChecked();
	JocysCom_SYellCB = JocysCom_SoundYellCheckButton:GetChecked();
	JocysCom_SGuildCB = JocysCom_SoundGuildCheckButton:GetChecked();
	JocysCom_SOfficerCB = JocysCom_SoundOfficerCheckButton:GetChecked();
	JocysCom_SPartyCB = JocysCom_SoundPartyCheckButton:GetChecked();
	JocysCom_SPartyLCB = JocysCom_SoundPartyLeaderCheckButton:GetChecked();
	-- JocysCom_SBGCB = JocysCom_SoundBattlegroundCheckButton:GetChecked();
	-- JocysCom_SBGLCB = JocysCom_SoundBattlegroundLeaderCheckButton:GetChecked();
	JocysCom_SRaidCB = JocysCom_SoundRaidCheckButton:GetChecked();
	JocysCom_SRaidLCB = JocysCom_SoundRaidLeaderCheckButton:GetChecked();
	JocysCom_SInstanceCB = JocysCom_SoundInstanceCheckButton:GetChecked();
	JocysCom_SInstanceLCB = JocysCom_SoundInstanceLeaderCheckButton:GetChecked();
	-- Save name check buttons.
	JocysCom_NQuestCB = JocysCom_NameQuestCheckButton:GetChecked();
	JocysCom_NMonsterCB = JocysCom_NameMonsterCheckButton:GetChecked();
	JocysCom_NWhisperCB = JocysCom_NameWhisperCheckButton:GetChecked();
	JocysCom_NSayCB = JocysCom_NameSayCheckButton:GetChecked();
	JocysCom_NYellCB = JocysCom_NameYellCheckButton:GetChecked();
	JocysCom_NGuildCB = JocysCom_NameGuildCheckButton:GetChecked();
	JocysCom_NOfficerCB = JocysCom_NameOfficerCheckButton:GetChecked();
	JocysCom_NPartyCB = JocysCom_NamePartyCheckButton:GetChecked();
	JocysCom_NPartyLCB = JocysCom_NamePartyLeaderCheckButton:GetChecked();
	-- JocysCom_NBGCB = JocysCom_NameBattlegroundCheckButton:GetChecked();
	-- JocysCom_NBGLCB = JocysCom_NameBattlegroundLeaderCheckButton:GetChecked();
	JocysCom_NRaidCB = JocysCom_NameRaidCheckButton:GetChecked();
	JocysCom_NRaidLCB = JocysCom_NameRaidLeaderCheckButton:GetChecked();
	JocysCom_NInstanceCB = JocysCom_NameInstanceCheckButton:GetChecked();
	JocysCom_NInstanceLCB = JocysCom_NameInstanceLeaderCheckButton:GetChecked();
end

function JocysCom_SetInterfaceSettings()
	C_ChatInfo.RegisterAddonMessagePrefix(addonPrefix);
	-- Attach OnShow and OnHide sripts to WoW frames.
	QuestLogPopupDetailFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	GossipFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	QuestFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	ItemTextFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	QuestMapDetailsScrollFrame:SetScript("OnHide", JocysCom_DialogueMiniFrame_Hide);
	JocysCom_OptionsFrame:SetScript("OnShow", JocysCom_OptionsFrame_OnShow);
	JocysCom_OptionsFrame:SetScript("OnHide", JocysCom_OptionsFrame_OnHide);
	QuestMapDetailsScrollFrame:SetScript("OnShow", function()
	JocysCom_AttachAndShowFrames(WorldMapFrame);
	end);
	-- Attach (and show) DialogueScroll and DialogueMini frames to WoW (GossipFrame) frame.
	JocysCom_AttachAndShowFrames(GossipFrame);
	-- Hide frames.
	JocysCom_OptionsFrame:Hide();
	JocysCom_MiniMenuFrame:Hide();
	--JocysCom_SaveButton:Hide();
	JocysCom_OptionsButton:Hide();
	-- Load descriptions.
	JocysCom_Text_EN();	
end

-- Load UI settings.
JocysCom_SetInterfaceSettings();

-- Register events (and on ADDON_LOADED event load toc file values and set them).
JocysCom_RegisterEvents();