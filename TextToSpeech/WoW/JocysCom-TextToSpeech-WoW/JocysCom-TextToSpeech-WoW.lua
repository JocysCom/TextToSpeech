-- Show or hide frame names: /fstack
-- Show or hide FriendsMicroButton ("Social" button): /run local m=FriendsMicroButton if m:IsShown()then m:Hide()else m:Show()end

-- Debug mode true(enabled) or false(disabled).
local DebugEnabled = false;

-- Set variables.
local unitName = UnitName("player");
local realmName = GetRealmName();
local questMessage = nil;
local speakMessage = nil;
local objectivesHeader = nil;
local NameIntro = false;
local arg1 = nil;
local arg2 = nil;
local lastArg = nil;
local NPCSex = nil;
local NPCGender = nil;
local stopWhenClosing = 1;
local dashIndex = nil;
local group = nil;
local messageType = nil;
local messageLeader = nil;
local messageDoNotDisturb = "Please wait... NPC dialog window is open and text-to-speech is enabled.";
local messageStop = "<message command=\"stop\" />";

-- Set text.
function JocysCom_Text_EN()
	-- OptionsFrame title.
	JocysCom_OptionsFrame.TitleText:SetText("Jocys.com Text to Speech World of Warcraft Addon 2.2.50 ( 2015-12-01 )");
	-- CheckButtons (Options) text.
	JocysCom_FilterCheckButton.text:SetText("|cff808080 Hide addon's whisper messages in chat window.|r");
	JocysCom_LockCheckButton.text:SetText("|cff808080 Lock mini frame with |cffffffff[Stop]|r |cff808080button.|r");
	JocysCom_MenuCheckButton.text:SetText("|cff808080 [Checked] show menu on the right side of |cffffffff[Stop]|r |cff808080button. [Unchecked] show menu on the left side.|r");
	-- Font Strings.
	JocysCom_DialogueScrollFrame_FontString:SetText("When mouse pointer is over this frame...\n\nSCROLL UP will START SPEECH\n\nSCROLL DOWN will STOP SPEECH");
	JocysCom_DescriptionFrameFontString:SetText("Text-to-speech voices, pitch, rate, effects, etc. ... you will find all options in |cff77ccffJocys.Com Text to Speech Monitor|r.\n\nHow it works: When you open NPC dialogue window, |cff77ccffJocys.Com Text to Speech WoW Addon|r creates and sends special whisper message to yourself (message includes dialogue text, character name and effect name). Then, |cff77ccffJocys.Com Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.");
	JocysCom_ReplaceNameFontString:SetText("Here you can change your name for text to speech from |cff00ff00" .. unitName .. "|r to something else.");
	JocysCom_MessageForMonitorFrameFontString:SetText("Whisper message for |cff77ccffJocys.Com Text to Speech Monitor|r ... it must be runninng in background:");	
end

-- Unlock frames.
function JocysCom_OptionsFrame_OnShow()
	JocysCom_StopButtonFrame_Texture:SetTexture(0, 0, 0, 0.5);
	JocysCom_DialogueScrollFrame:EnableMouse(true);
	JocysCom_DialogueScrollFrame_Texture:SetTexture(0, 0, 0, 0.8);
	JocysCom_DialogueScrollFrame_FontString:Show();
	JocysCom_DialogueScrollFrameResizeButton:Show();
end

-- Lock frames.
function JocysCom_OptionsFrame_OnHide()
	if string.len(JocysCom_ReplaceNameEditBox:GetText()) < 2 then
		JocysCom_ReplaceNameEditBox:SetText(unitName);
	end
	JocysCom_StopButtonFrame_Texture:SetTexture(0, 0, 0, 0);
	JocysCom_DialogueScrollFrame:EnableMouse(false);
	JocysCom_DialogueScrollFrame_Texture:SetTexture(0, 0, 0, 0);
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
		SendChatMessage(messageStop, "WHISPER", "Common", unitName);
		stopWhenClosing = 0;
		JocysCom_OptionsEditBox:SetText("|cff808080" .. messageStop .. "|r");
	end
end

-- Send sound intro function.
function JocysCom_SendSoundIntro(group)
	SendChatMessage("<message command=\"sound\" group=\"" .. group .. "\" />", "WHISPER", "Common", unitName);
end

-- Register events.
function JocysCom_RegisterEvents()
	JocysCom_OptionsFrame:SetScript("OnEvent", JocysCom_OptionsFrame_OnEvent);
	-- Addon loaded event.
	JocysCom_OptionsFrame:RegisterEvent("ADDON_LOADED");
	-- Dialogue (open) events.
	JocysCom_OptionsFrame:RegisterEvent("GOSSIP_SHOW");
	JocysCom_OptionsFrame:RegisterEvent("QUEST_GREETING");
	JocysCom_OptionsFrame:RegisterEvent("QUEST_DETAIL");
	JocysCom_OptionsFrame:RegisterEvent("QUEST_PROGRESS");
	JocysCom_OptionsFrame:RegisterEvent("QUEST_COMPLETE");
	-- books, scrolls, saved copies of mail messages, plaques, gravestones events.
	JocysCom_OptionsFrame:RegisterEvent("ITEM_TEXT_READY"); 
	-- Close frames.
	JocysCom_OptionsFrame:RegisterEvent("GOSSIP_CLOSED"); --"QUEST_FINISHED", "QUEST_ACCEPTED"
	-- Chat MONSTER events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_EMOTE");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_WHISPER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_SAY");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_PARTY");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_MONSTER_YELL");
	-- Chat WHISPER events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_WHISPER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_WHISPER_INFORM");
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
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_GUILD");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_GUILD_ACHIEVEMENT");
	-- Chat RAID events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_RAID");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_RAID_LEADER");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_RAID_WARNING");
	-- Chat BATTLEGROUND events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_BATTLEGROUND");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_BATTLEGROUND_LEADER");
	-- Chat INSTANCE events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_INSTANCE_CHAT");
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_INSTANCE_CHAT_LEADER");
	-- Logout event.
	JocysCom_OptionsFrame:RegisterEvent("PLAYER_LOGOUT");
end

function JocysCom_OptionsFrame_OnEvent(self, event, arg1, arg2)
	 -- don't proceed messages with <message> tags and your own incoming whispers.
	if string.find(tostring(arg1), "<message") ~= nil or (event == "CHAT_MSG_WHISPER" and string.find(tostring(arg2), tostring(unitName)) ~= nil) then return end
	-- Debug.
	if DebugEnabled then print("1. " .. event) end
	-- Events.
	if event == "ADDON_LOADED" then
		JocysCom_LoadTocFileSettings();
		return;
	elseif event == "PLAYER_LOGOUT" then
		JocysCom_SaveTocFileSettings();
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
	elseif JocysCom_WhisperCheckButton:GetChecked() == true and (event == "CHAT_MSG_WHISPER" or event == "CHAT_MSG_WHISPER_INFORM" or event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_BN_WHISPER_INFORM") then
		group = "Whisper";
		if JocysCom_SoundWhisperCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameWhisperCheckButton:GetChecked() == true then NameIntro = true end
		-- replace friend's real name with character name.
		if event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_BN_WHISPER_INFORM" then
			-- totalBNet, numBNetOnline = BNGetNumFriends();
			-- presenceID, presenceName, battleTag, isBattleTagPresence, toonName, toonID, client, isOnline, lastOnline, isAFK, isDND, messageText, noteText, isRIDFriend, broadcastTime, canSoR = BNGetFriendInfoByID(7);		
			arg2 = string.gsub(arg2, "|", " ");
			for i in string.gmatch(arg2, "%S+") do
				if string.find(i, "Kf") ~= nil then
					arg2 = string.gsub(i, "Kf", "");
				end
			end	
			presenceID, presenceName, battleTag, isBattleTagPresence, toonName = BNGetFriendInfoByID(arg2);
			arg2 = toonName;
		end	
	elseif JocysCom_EmoteCheckButton:GetChecked() == true and ((event == "CHAT_MSG_EMOTE") or (event == "CHAT_MSG_TEXT_EMOTE")) then
		group = "Emote"	
		if JocysCom_SoundEmoteCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if (event == "CHAT_MSG_EMOTE") then NameIntro = true end
	elseif JocysCom_SayCheckButton:GetChecked() == true and (event == "CHAT_MSG_SAY") then
		group = "Say";
		if JocysCom_SoundSayCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameSayCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_YellCheckButton:GetChecked() == true and (event == "CHAT_MSG_YELL") then
		group = "Yell";
		if JocysCom_SoundYellCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameYellCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_PartyCheckButton:GetChecked() == true and string.find(event, "MSG_PARTY") ~= nil then
		group = "Party";
		if JocysCom_SoundPartyCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NamePartyCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_GuildCheckButton:GetChecked() == true and (event == "CHAT_MSG_GUILD") then
		group = "Guild";
		if JocysCom_SoundGuildCheckButton:GetChecked() == true then	JocysCom_SendSoundIntro(group) end
		if JocysCom_NameGuildCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_RaidCheckButton:GetChecked() == true and string.find(event, "MSG_RAID") ~= nil then
		group = "Raid";
		if JocysCom_SoundRaidCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameRaidCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_RaidLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING") then
		group = "RaidLeader";
		if JocysCom_SoundRaidLeaderCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameRaidLeaderCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_BattlegroundCheckButton:GetChecked() == true and string.find(event, "MSG_BATTLEGROUND") ~= nil then 
		group = "Battleground";
		if JocysCom_SoundBattlegroundCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameBattlegroundCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_BattlegroundLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_BATTLEGROUND_LEADER") then
		group = "BattlegroundLeader";
		if JocysCom_SoundBattlegroundLeaderCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameBattlegroundLeaderCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_InstanceCheckButton:GetChecked() == true and string.find(event, "MSG_INSTANCE") ~= nil then 
		group = "Instance";
		if JocysCom_SoundInstanceCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
		if JocysCom_NameInstanceCheckButton:GetChecked() == true then NameIntro = true end
	elseif JocysCom_InstanceLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then
		group = "InstanceLeader";
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
	dashIndex = string.find(arg2, "-");
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
	JocysCom_SpeakMessage(speakMessage, event, arg2, group);
end

function JocysCom_SpeakMessage(speakMessage, event, name, group)
	-- Remove HTML <> tags and text between them.
	if speakMessage ~= nil then
		if string.find(speakMessage, "HTML") ~= nil then
			speakMessage = string.gsub(speakMessage, "%<(.-)%>", "");
			speakMessage = string.gsub(speakMessage, "\"", "");
			--speakMessage = string.gsub(speakMessage, "%b<>", "");
		end
	end	
	if DebugEnabled then print("2. " .. event) end
	if speakMessage == nil then return end
	if string.find(event, "CHAT") ~= nil then
		if string.find(speakMessage, "|") ~= nil then
			speakMessage = string.gsub(speakMessage, "%|cff(.-)%|h", "");
			speakMessage = string.gsub(speakMessage, "%|h%|r", "");
			speakMessage = string.gsub(speakMessage, "%[", " \"");
			speakMessage = string.gsub(speakMessage, "%]", "\" ");
		end
	else
		if JocysCom_StartOnOpenCheckButton:GetChecked() == true then 	
			JocysCom_SendChatMessageStop(1); -- Send stop message.
		end
		if (string.find(event, "QUEST") ~= nil or event == "GOSSIP_SHOW" or event == "ITEM_TEXT_READY") and JocysCom_SoundQuestCheckButton:GetChecked() == true then JocysCom_SendSoundIntro(group) end
	end
	-- Set NPC name.
	local NPCName = nil;
	if string.find(event, "CHAT") ~= nil then
		NPCName = name;
	else
		NPCName = GetUnitName("npc");
	end
	if NPCName == nil then
		NPCName = "";
	else
	NPCName = string.gsub(NPCName, "&", " and ");
	NPCName = string.gsub(NPCName, "\"", "");
	NPCName = " name=\"" .. tostring(NPCName) .. "\"";
	end
	-- Set NPC gender (1 = Neutrum / Unknown, 2 = Male, 3 = Female). 
	NPCSex = nil;
	if string.find(event, "CHAT") ~= nil and string.find(event, "CHAT_MSG_MONSTER") == nil then
		NPCSex = UnitSex(name);
	elseif string.find(event, "CHAT_MSG_MONSTER") ~= nil then
		--local unitGuid = UnitGUID("target");
		NPCSex = UnitSex(name);
	else
		NPCSex = UnitSex("npc");	
	end
	NPCGender = nil;
	if NPCSex == 1 then NPCGender = "Neutral" end
	if NPCSex == 2 then NPCGender = "Male" end
	if NPCSex == 3 then NPCGender = "Female" end
	if NPCGender == nil then
		NPCGender = "";
	else
	NPCGender = " gender=\"" .. tostring(NPCGender) .. "\"";
	end
	-- Get NPC type (Beast, Dragonkin, Demon, Elemental, Giant, Undead, Humanoid, Critter, Mechanical, Not specified, Totem, Non-combat Pet, Gas Cloud).							
	local NPCType = UnitCreatureType("npc");
	if NPCType == nil then
		NPCType = "";
	else
		NPCType = " effect=\"" .. tostring(NPCType) .. "\"";
	end
	-- Set group value.
	if group == nil or group == "" then
	group = "";
	else
	group = " group=\"" .. group .. "\"";
	end
	-- Replace player name.
	local newUnitName = JocysCom_ReplaceNameEditBox:GetText();
	if string.len(newUnitName) > 1 and newUnitName ~= unitName then
		speakMessage = string.gsub(speakMessage, unitName, newUnitName);
	end
	-- Replace in speakMessage. %c %p
	speakMessage = speakMessage .. " ";
	speakMessage = string.gsub(speakMessage, "&", " and ");
	speakMessage = string.gsub(speakMessage, "%c(%u)", ".%1");
	speakMessage = string.gsub(speakMessage, "%c", " ");
	speakMessage = string.gsub(speakMessage, "[ ]+", " ");
	speakMessage = string.gsub(speakMessage, " %.", ".");
	speakMessage = string.gsub(speakMessage, "[(%p)]+", "%1");
	speakMessage = string.gsub(speakMessage, "[%!]+", "!");
	speakMessage = string.gsub(speakMessage, "[%?]+", "?");
	speakMessage = string.gsub(speakMessage, "%!%.", "!");
	speakMessage = string.gsub(speakMessage, "%?%.", "?");
	speakMessage = string.gsub(speakMessage, "%?%-", "?");
	speakMessage = string.gsub(speakMessage, "%?%!%?", "?!");
	speakMessage = string.gsub(speakMessage, "%!%?%!", "?!");
	speakMessage = string.gsub(speakMessage, "%!%?", "?!");
	speakMessage = string.gsub(speakMessage, "%!", "! ");
	speakMessage = string.gsub(speakMessage, "%?", "? ");
	speakMessage = string.gsub(speakMessage, " %!", "!");
	speakMessage = string.gsub(speakMessage, "^%.", "");
	speakMessage = string.gsub(speakMessage, ">%.", ".>");
	speakMessage = string.gsub(speakMessage, "[%.]+", ". ");
	speakMessage = string.gsub(speakMessage, "%? %.", "");
	speakMessage = string.gsub(speakMessage, "<", " [comment] ");
	speakMessage = string.gsub(speakMessage, ">", " [/comment] ");
	speakMessage = string.gsub(speakMessage, "[ ]+", " ");
	speakMessage = string.gsub(speakMessage, "%%s", "");
	speakMessage = string.gsub(speakMessage, "lvl", "level");
	-- Format and send whisper message.
		local chatMessageSP = "<message command=\"play\"" .. group .. NPCName .. NPCGender .. NPCType .. "><part>";
		if NPCGender == "" then
		chatMessageSP = "<message" .. group .. NPCName .. NPCType .. " command=\"play\"><part>";
		end
		local chatMessageSA = "<message command=\"add\"><part>";
		local chatMessageE = "</part></message>";
		local chatMessage;
		local chatMessageLimit = 240;
		local sizeAdd = chatMessageLimit - string.len(chatMessageSA) - string.len(chatMessageE);
		local sizePlay = chatMessageLimit - string.len(chatMessageSP) - string.len(chatMessageE);
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
			-- if text length less than 100 then...
			if speakMessageRemainingLen <= sizePlay then
				part = string.sub(speakMessage, startIndex);
				chatMessage = chatMessageSP .. part .. chatMessageE;
				stopWhenClosing = 1;
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
				if DebugEnabled then print("[" .. tostring(index) .. "] [" .. startIndex .. "] '" .. part .. "'") end
				break;
			-- if text length more than 100 then...
			elseif (index - startIndex) > sizeAdd or (index >= speakMessageLen and speakMessageRemainingLen > sizePlay) then
				-- if space is out of size then...
				part = string.sub(speakMessage, startIndex, endIndex - 1);
				chatMessage = chatMessageSA .. part .. chatMessageE;
				stopWhenClosing = 1;
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
				if DebugEnabled then print("[" .. tostring(index) .. "] [" .. startIndex .. "-" .. (endIndex - 1) .. "] '" .. part .. "'") end
				startIndex = endIndex;
				speakMessageRemainingLen = string.len(string.sub(speakMessage, startIndex));
			end
			-- look for next space.
				endIndex = index + 1;
		end
		-- FILL EDITBOXES  .. "<" .. event .. " />" ..
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
	PlaySound("igMainMenuOptionCheckBoxOn");
	JocysCom_SaveTocFileSettings();
end

-- DND.
function JocysCom_DndCheckButton_OnClick(self)
    local DNDIsShown = JocysCom_DialogueMiniFrame:IsShown();
	PlaySound("igMainMenuOptionCheckBoxOn");
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
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_LockCheckButton:GetChecked() == true then
		JocysCom_StopButtonFrame:RegisterForDrag();
	else
		JocysCom_StopButtonFrame:RegisterForDrag("LeftButton");
	end
	JocysCom_SaveTocFileSettings();
end

 -- StopButtonFrame position Right or Left.
function JocysCom_MenuCheckButton_OnClick()
	PlaySound("igMainMenuOptionCheckBoxOn");
 	JocysCom_MiniMenuFrame:ClearAllPoints();
	if JocysCom_MenuCheckButton:GetChecked() == true then
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMLEFT", JocysCom_StopButtonFrame, "BOTTOMRIGHT", 0, -4);
	else
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMRIGHT", JocysCom_StopButtonFrame, "BOTTOMLEFT", 0, -4);
	end
	JocysCom_SaveTocFileSettings();
end

-- Enable disable speech.
function JocysCom_CheckButton_OnClick(self, name)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if self:GetChecked() == true then
		if name == "Battleground" then
			JocysCom_BattlegroundLeaderCheckButton:SetChecked(false);
		elseif name == "BattlegroundLeader" and JocysCom_BattlegroundCheckButton:GetChecked() == true  then
			JocysCom_BattlegroundCheckButton:SetChecked(false);
			JocysCom_SendChatMessageStop(1, "Battleground");
		elseif name == "Raid" then
			JocysCom_RaidLeaderCheckButton:SetChecked(false);
		elseif name == "RaidLeader" and JocysCom_RaidCheckButton:GetChecked() == true then
			JocysCom_RaidCheckButton:SetChecked(false);
			JocysCom_SendChatMessageStop(1, "Raid");		
		elseif name == "Instance" then
			JocysCom_InstanceLeaderCheckButton:SetChecked(false);
		elseif name == "InstanceLeader" and JocysCom_InstanceCheckButton:GetChecked() == true then
			JocysCom_InstanceCheckButton:SetChecked(false);
			JocysCom_SendChatMessageStop(1, "Instance");
		end 
	else
		JocysCom_SendChatMessageStop(1, name);
	end
	JocysCom_SaveTocFileSettings();
end

-- Show MiniMenuFrame and set text.
 function JocysCom_MiniMenuFrame_Show(name)
 	local fontString = "";
	JocysCom_MiniMenuFrame_FontString:Show();
	if name == "Stop" then
		fontString = "|cffddddddStop text-to-speech and clear all playlist.|r";
	elseif name == "Save" then
		fontString = "|cffddddddSave target name, gender, type in Monitor.|r";
	elseif name == "Options" then
		fontString = "|cffddddddOpen text-to-speech Options window.|r";
	elseif name == "Busy" then
		fontString = "|cff6464ffShow <Busy> over your character when window is open and speech is on.|r";
	elseif name == "Objectives" then
		fontString = "|cfffffb9fInclude QUEST OBJECTIVES in text-to-speech.|r";
	elseif name == "Quest" then
		fontString = "|cfffffb9fPlay DIALOGUE, BOOK, PLAQUE, etc. window text.|r";
	elseif name == "Monster" then
		fontString = "|cfffffb9fPlay NPC chat messages.|r";
	elseif name == "Whisper" then
		fontString = "|cffffb2ebPlay WHISPER chat messages.|r";
	elseif name == "Emote" then
		fontString = "|cffff6b26Play player EMOTE  chat messages.|r";
	elseif name == "Say" then
		fontString = "|cffffffffPlay SAY chat messages.|r";
	elseif name == "Yell" then
		fontString = "|cffff3f40Play YELL chat messages.|r";
	elseif name == "Party" then
		fontString = "|cffaaa7ffPlay PARTY chat messages.|r";
	elseif name == "Guild" then
		fontString = "|cff40fb40Play GUILD chat messages.|r";
	elseif name == "Raid" then
		fontString = "|cffff7d00Play all RAID chat messages.|r";
	elseif name == "RaidLeader" then
		fontString = "|cffff4709Play RAID LEADER chat messages only.|r";
	elseif name == "Battleground" then
		fontString = "|cffff7d00Play all BATTLEGROUND chat messages.|r";
	elseif name == "BattlegroundLeader" then
		fontString = "|cffff4709Play BATTLEGROUND LEADER chat messages only.|r";
	elseif name == "Instance" then
		fontString = "|cffff7d00Play all INSTANCE chat messages.|r";
	elseif name == "InstanceLeader" then
		fontString = "|cffff4709Play INSTANCE LEADER chat messages only.|r";
	elseif name == "StartOnOpen" then
		fontString = "|cffefc176Instantly START to play DIALOGUE, BOOK, etc. text on opening window.|r";
	elseif name == "StopOnClose" then
		fontString = "|cffefc176Instantly STOP to play DIALOGUE, BOOK, etc. text on closing window.|r";
	-- Play intro sound check-boxes.
	elseif name == "SoundQuest" then
		fontString = "|cfffffb9fPlay sound at the beginning of DIALOGUE, BOOK, PLAQUE, etc. window text.|r";
	elseif name == "SoundMonster" then
		fontString = "|cfffffb9fPlay sound at the beginning of NPC messages.|r";
	elseif name == "SoundWhisper" then
		fontString = "|cffffb2ebPlay sound at the beginning of WHISPER messages.|r";
		elseif name == "SoundEmote" then
		fontString = "|cffff6b26Play sound at the beginning of player EMOTE messages.|r";
	elseif name == "SoundSay" then
		fontString = "|cffffffffPlay sound at the beginning of SAY messages.|r";
	elseif name == "SoundYell" then
		fontString = "|cffff3f40Play sound at the beginning of YELL messages.|r";
	elseif name == "SoundParty" then
		fontString = "|cffaaa7ffPlay sound at the beginning of PARTY messages.|r";
	elseif name == "SoundGuild" then
		fontString = "|cff40fb40Play sound at the beginning of GUILD messages.|r";
	elseif name == "SoundRaid" then
		fontString = "|cffff7d00Play sound at the beginning of RAID messages.|r";
	elseif name == "SoundRaidLeader" then
		fontString = "|cffff4709Play sound at the beginning of RAID LEADER messages.|r";
	elseif name == "SoundBattleground" then
		fontString = "|cffff7d00Play sound at the beginning of BATTLEGROUND messages.|r";
	elseif name == "SoundBattlegroundLeader" then
		fontString = "|cffff4709Play sound at the beginning of BATTLEGROUND LEADER messages.|r";
	elseif name == "SoundInstance" then
		fontString = "|cffff7d00Play sound at the beginning of INSTANCE messages.|r";
	elseif name == "SoundInstanceLeader" then
		fontString = "|cffff4709Play sound at the beginning of INSTANCE LEADER messages.|r";
	-- Add name check-boxes.
	elseif name == "NameQuest" then
		fontString = "|cfffffb9fAdd \"<Name> says.\" to DIALOGUE, BOOK, PLAQUE, etc. window text.|r";
	elseif name == "NameMonster" then
		fontString = "|cfffffb9fAdd \"<Name> whispers \\ says \\ yells.\" to NPC messages.|r";
	elseif name == "NameWhisper" then
		fontString = "|cffffb2ebAdd \"<Name> whispers.\" to WHISPER messages.|r";
	elseif name == "NameSay" then
		fontString = "|cffffffffAdd \"<Name> says.\" to SAY messages.|r";
	elseif name == "NameYell" then
		fontString = "|cffff3f40Add \"<Name> yells.\" to YELL messages.|r";
	elseif name == "NameParty" then
		fontString = "|cffaaa7ffAdd \"<Name> says.\" to PARTY messages.|r";
	elseif name == "NameGuild" then
		fontString = "|cff40fb40Add \"<Name> says.\" to GUILD messages.|r";
	elseif name == "NameRaid" then
		fontString = "|cffff7d00Add \"<Name> says.\" to RAID messages.|r";
	elseif name == "NameRaidLeader" then
		fontString = "|cffff4709Add \"<Name> says.\" to RAID LEADER messages.|r";
	elseif name == "NameBattleground" then
		fontString = "|cffff7d00Add \"<Name> says.\" to BATTLEGROUND messages.|r";
	elseif name == "NameBattlegroundLeader" then
		fontString = "|cffff4709Add \"<Name> says.\" to BATTLEGROUND LEADER messages.|r";
	elseif name == "NameInstance" then
		fontString = "|cffff7d00Add \"<Name> says.\" to INSTANCE messages.|r";
	elseif name == "NameInstanceLeader" then
		fontString = "|cffff4709Add \"<Name> says.\" to INSTANCE LEADER messages.|r";
	else
		JocysCom_MiniMenuFrame_FontString:Hide();
	end
	--JocysCom_MiniMenuFrame:SetBackdrop({
	----bgFile="Interface/AddOns/JocysCom-TextToSpeech-WoW/Images/JocysCom-MiniMenuFrame-Background",
	----tile = true,
	----tileSize = 32,
	----insets = { left = 0, right = 0, top = 0, bottom = 0 }
	--});
	JocysCom_MiniMenuFrame_FontString:SetText(fontString);
	JocysCom_MiniMenuFrameBorder:SetBackdrop( { edgeFile = "Interface/AddOns/JocysCom-TextToSpeech-WoW/Images/JocysCom-MiniMenuFrame-Border", edgeSize = 23 });
	JocysCom_MiniMenuFrame:Show();
	JocysCom_OptionsButton:Show();
	JocysCom_SaveButton:Show();
 end

 -- Hide MiniMenuFrame.
 function JocysCom_MiniMenuFrame_Hide()
	JocysCom_MiniMenuFrame:Hide();
	JocysCom_OptionsButton:Hide();
	JocysCom_SaveButton:Hide();
 end
 
-- [ Stop ] dialog button.
function JocysCom_StopButton_OnClick(name)
	PlaySound("igMainMenuOptionCheckBoxOn");
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
	if DebugEnabled then print("MiniFrame Hide") end
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
function JocysCom_SaveButton_OnClick(self)
	-- Target Name.
	local targetName = GetUnitName("target");
	if targetName == nil then
		print("1. Select a target.");
		print("2. Click [S] button to save target's name, gender and type in Monitor.");
		return;
	else
		targetName = string.gsub(targetName, "&", " and ");
		targetName = string.gsub(targetName, "\"", "");
	end
	-- Target Gender.
	local targetSex = UnitSex("target");
	if targetSex == 2 then
		targetSex = "Male";
	elseif targetSex == 3 then
		targetSex = "Female";
	else
		targetSex = "Neutral";
	end
	-- Target Type.
	local targetType = UnitCreatureType("target");
	if targetType == nil then
		local targetTypeValue = "";
	else
		local targetTypeValue = targetType
	end
	print("Save : " .. targetName .. " : " .. targetSex .. " : " .. targetType);
	local saveMessage = "<message command=\"Save\" name=\"" .. targetName .. "\" gender=\"" .. targetSex .. "\" effect=\"" .. targetType .. "\" />";
	SendChatMessage(saveMessage, "WHISPER", "Common", unitName);
	JocysCom_OptionsEditBox:SetText("|cff808080" .. saveMessage .. "|r");
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

-- Enable message filter.
function JocysCom_CHAT_MSG_WHISPER_INFORM(self, event, msg)
	if string.find(msg, "<message") ~= nil then
		return true;
	end
end
-- Enable/Disable message filter.
function JocysCom_CHAT_MSG_WHISPER(self, event, msg)
	if string.find(msg, "<message") ~= nil and JocysCom_FilterCheckButton:GetChecked() == true then
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
	if JocysCom_LockCB == false then JocysCom_LockCheckButton:SetChecked(false) else JocysCom_LockCheckButton:SetChecked(true) end
	if JocysCom_LockCheckButton:GetChecked() == true then JocysCom_StopButtonFrame:RegisterForDrag() else JocysCom_StopButtonFrame:RegisterForDrag("LeftButton") end
	-- Set MenuCheckButton and MiniMenuFrame.
	if JocysCom_MenuCB == false then JocysCom_MenuCheckButton:SetChecked(false) else JocysCom_MenuCheckButton:SetChecked(true) end
	JocysCom_MiniMenuFrame:ClearAllPoints();
	if JocysCom_MenuCheckButton:GetChecked() == true then
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMLEFT", JocysCom_StopButtonFrame, "BOTTOMRIGHT", 0, -4);
	else
	JocysCom_MiniMenuFrame:SetPoint("BOTTOMRIGHT", JocysCom_StopButtonFrame, "BOTTOMLEFT", 0, -4);
	end
	-- Set FilterCheckButton and Message filters.
	if JocysCom_FilterCB == false then JocysCom_FilterCheckButton:SetChecked(false) else JocysCom_FilterCheckButton:SetChecked(true) end
	-- Add chat message filters.
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER_INFORM", JocysCom_CHAT_MSG_WHISPER_INFORM);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER", JocysCom_CHAT_MSG_WHISPER);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_DND", JocysCom_CHAT_MSG_DND);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_SYSTEM", JocysCom_CHAT_MSG_SYSTEM);
	-- Set (MiniFrame) CheckButtons.
	if JocysCom_ReplaceNameEB == "" or JocysCom_ReplaceNameEB == nil then JocysCom_ReplaceNameEB = unitName else JocysCom_ReplaceNameEditBox:SetText(JocysCom_ReplaceNameEB) end
	if JocysCom_QuestCB == false then JocysCom_QuestCheckButton:SetChecked(false) else JocysCom_QuestCheckButton:SetChecked(true) end
	if JocysCom_MonsterCB == false then JocysCom_MonsterCheckButton:SetChecked(false) else JocysCom_MonsterCheckButton:SetChecked(true) end
	if JocysCom_WhisperCB == false then JocysCom_WhisperCheckButton:SetChecked(false) else JocysCom_WhisperCheckButton:SetChecked(true) end
	if JocysCom_EmoteCB == false then JocysCom_EmoteCheckButton:SetChecked(false) else JocysCom_EmoteCheckButton:SetChecked(true) end
	if JocysCom_SayCB == false then JocysCom_SayCheckButton:SetChecked(false) else JocysCom_SayCheckButton:SetChecked(true) end
	if JocysCom_YellCB == false then JocysCom_YellCheckButton:SetChecked(false) else JocysCom_YellCheckButton:SetChecked(true) end
	if JocysCom_PartyCB == false then JocysCom_PartyCheckButton:SetChecked(false) else JocysCom_PartyCheckButton:SetChecked(true) end
	if JocysCom_GuildCB == false then JocysCom_GuildCheckButton:SetChecked(false) else JocysCom_GuildCheckButton:SetChecked(true) end
	if JocysCom_BattlegroundCB == false then JocysCom_BattlegroundCheckButton:SetChecked(false) else JocysCom_BattlegroundCheckButton:SetChecked(true) end
	if JocysCom_BattlegroundLCB == true then JocysCom_BattlegroundLeaderCheckButton:SetChecked(true) else JocysCom_BattlegroundLeaderCheckButton:SetChecked(false) end
	if JocysCom_RaidCB == false then JocysCom_RaidCheckButton:SetChecked(false) else JocysCom_RaidCheckButton:SetChecked(true) end
	if JocysCom_RaidLCB == true then JocysCom_RaidLeaderCheckButton:SetChecked(true) else JocysCom_RaidLeaderCheckButton:SetChecked(false) end
	if JocysCom_InstanceCB == false then JocysCom_InstanceCheckButton:SetChecked(false) else JocysCom_InstanceCheckButton:SetChecked(true) end
	if JocysCom_InstanceLCB == true then JocysCom_InstanceLeaderCheckButton:SetChecked(true) else JocysCom_InstanceLeaderCheckButton:SetChecked(false) end
	if JocysCom_ObjectivesCB == false then JocysCom_ObjectivesCheckButton:SetChecked(false) else JocysCom_ObjectivesCheckButton:SetChecked(true) end
	if JocysCom_StartOnOpenCB == false then JocysCom_StartOnOpenCheckButton:SetChecked(false) else JocysCom_StartOnOpenCheckButton:SetChecked(true) end
	if JocysCom_StopOnCloseCB == false then JocysCom_StopOnCloseCheckButton:SetChecked(false) else JocysCom_StopOnCloseCheckButton:SetChecked(true) end
	-- Set (MiniFrame) Sound CheckButtons.
	if JocysCom_SMonsterCB == false then JocysCom_SoundMonsterCheckButton:SetChecked(false) else JocysCom_SoundMonsterCheckButton:SetChecked(true) end
	if JocysCom_SWhisperCB == false then JocysCom_SoundWhisperCheckButton:SetChecked(false) else JocysCom_SoundWhisperCheckButton:SetChecked(true) end
	if JocysCom_SEmoteCB == false then JocysCom_SoundEmoteCheckButton:SetChecked(false) else JocysCom_SoundEmoteCheckButton:SetChecked(true) end
	if JocysCom_SSayCB == false then JocysCom_SoundSayCheckButton:SetChecked(false) else JocysCom_SoundSayCheckButton:SetChecked(true) end
	if JocysCom_SYellCB == false then JocysCom_SoundYellCheckButton:SetChecked(false) else JocysCom_SoundYellCheckButton:SetChecked(true) end
	if JocysCom_SPartyCB == false then JocysCom_SoundPartyCheckButton:SetChecked(false) else JocysCom_SoundPartyCheckButton:SetChecked(true) end
	if JocysCom_SGuildCB == false then JocysCom_SoundGuildCheckButton:SetChecked(false) else JocysCom_SoundGuildCheckButton:SetChecked(true) end
	if JocysCom_SBattlegroundCB == false then JocysCom_SoundBattlegroundCheckButton:SetChecked(false) else JocysCom_SoundBattlegroundCheckButton:SetChecked(true) end
	if JocysCom_SBattlegroundLCB == false then JocysCom_SoundBattlegroundLeaderCheckButton:SetChecked(false) else JocysCom_SoundBattlegroundLeaderCheckButton:SetChecked(true) end
	if JocysCom_SRaidCB == false then JocysCom_SoundRaidCheckButton:SetChecked(false) else JocysCom_SoundRaidCheckButton:SetChecked(true) end
	if JocysCom_SRaidLCB == false then JocysCom_SoundRaidLeaderCheckButton:SetChecked(false) else JocysCom_SoundRaidLeaderCheckButton:SetChecked(true) end
	if JocysCom_SInstanceCB == false then JocysCom_SoundInstanceCheckButton:SetChecked(false) else JocysCom_SoundInstanceCheckButton:SetChecked(true) end
	if JocysCom_SInstanceLCB == false then JocysCom_SoundInstanceLeaderCheckButton:SetChecked(false) else JocysCom_SoundInstanceLeaderCheckButton:SetChecked(true) end
	-- Set (MiniFrame) Name CheckButtons.
	if JocysCom_NMonsterCB == true then JocysCom_NameMonsterCheckButton:SetChecked(true) else JocysCom_NameMonsterCheckButton:SetChecked(false) end
	if JocysCom_NWhisperCB == true then JocysCom_NameWhisperCheckButton:SetChecked(true) else JocysCom_NameWhisperCheckButton:SetChecked(false) end
	if JocysCom_NSayCB == true then JocysCom_NameSayCheckButton:SetChecked(true) else JocysCom_NameSayCheckButton:SetChecked(false) end
	if JocysCom_NYellCB == true then JocysCom_NameYellCheckButton:SetChecked(true) else JocysCom_NameYellCheckButton:SetChecked(false) end
	if JocysCom_NPartyCB == true then JocysCom_NamePartyCheckButton:SetChecked(true) else JocysCom_NamePartyCheckButton:SetChecked(false) end
	if JocysCom_NGuildCB == true then JocysCom_NameGuildCheckButton:SetChecked(true) else JocysCom_NameGuildCheckButton:SetChecked(false) end
	if JocysCom_NBattlegroundCB == true then JocysCom_NameBattlegroundCheckButton:SetChecked(true) else JocysCom_NameBattlegroundCheckButton:SetChecked(false) end
	if JocysCom_NBattlegroundLCB == true then JocysCom_NameBattlegroundLeaderCheckButton:SetChecked(true) else JocysCom_NameBattlegroundLeaderCheckButton:SetChecked(false) end
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
	JocysCom_QuestCB = JocysCom_QuestCheckButton:GetChecked();
	JocysCom_MonsterCB = JocysCom_MonsterCheckButton:GetChecked();
	JocysCom_WhisperCB = JocysCom_WhisperCheckButton:GetChecked();
	JocysCom_EmoteCB = JocysCom_EmoteCheckButton:GetChecked();
	JocysCom_SayCB = JocysCom_SayCheckButton:GetChecked();
	JocysCom_YellCB = JocysCom_YellCheckButton:GetChecked();
	JocysCom_PartyCB = JocysCom_PartyCheckButton:GetChecked();
	JocysCom_GuildCB = JocysCom_GuildCheckButton:GetChecked();
	JocysCom_BattlegroundCB = JocysCom_BattlegroundCheckButton:GetChecked();
	JocysCom_BattlegroundLCB = JocysCom_BattlegroundLeaderCheckButton:GetChecked();
	JocysCom_RaidCB = JocysCom_RaidCheckButton:GetChecked();
	JocysCom_RaidLCB = JocysCom_RaidLeaderCheckButton:GetChecked();
	JocysCom_InstanceCB = JocysCom_InstanceCheckButton:GetChecked();
	JocysCom_InstanceLCB = JocysCom_InstanceLeaderCheckButton:GetChecked();
	JocysCom_ObjectivesCB = JocysCom_ObjectivesCheckButton:GetChecked();
	JocysCom_StartOnOpenCB = JocysCom_StartOnOpenCheckButton:GetChecked();
	JocysCom_StopOnCloseCB = JocysCom_StopOnCloseCheckButton:GetChecked();
	-- Save sound check buttons.
	JocysCom_SMonsterCB = JocysCom_SoundMonsterCheckButton:GetChecked();
	JocysCom_SWhisperCB = JocysCom_SoundWhisperCheckButton:GetChecked();
	JocysCom_SEmoteCB = JocysCom_SoundEmoteCheckButton:GetChecked();
	JocysCom_SSayCB = JocysCom_SoundSayCheckButton:GetChecked();
	JocysCom_SYellCB = JocysCom_SoundYellCheckButton:GetChecked();
	JocysCom_SPartyCB = JocysCom_SoundPartyCheckButton:GetChecked();
	JocysCom_SGuildCB = JocysCom_SoundGuildCheckButton:GetChecked();
	JocysCom_SBattlegroundCB = JocysCom_SoundBattlegroundCheckButton:GetChecked();
	JocysCom_SBattlegroundLCB = JocysCom_SoundBattlegroundLeaderCheckButton:GetChecked();
	JocysCom_SRaidCB = JocysCom_SoundRaidCheckButton:GetChecked();
	JocysCom_SRaidLCB = JocysCom_SoundRaidLeaderCheckButton:GetChecked();
	JocysCom_SInstanceCB = JocysCom_SoundInstanceCheckButton:GetChecked();
	JocysCom_SInstanceLCB = JocysCom_SoundInstanceLeaderCheckButton:GetChecked();
	-- Save name check buttons.
	JocysCom_NMonsterCB = JocysCom_NameMonsterCheckButton:GetChecked();
	JocysCom_NWhisperCB = JocysCom_NameWhisperCheckButton:GetChecked();
	JocysCom_NSayCB = JocysCom_NameSayCheckButton:GetChecked();
	JocysCom_NYellCB = JocysCom_NameYellCheckButton:GetChecked();
	JocysCom_NPartyCB = JocysCom_NamePartyCheckButton:GetChecked();
	JocysCom_NGuildCB = JocysCom_NameGuildCheckButton:GetChecked();
	JocysCom_NBattlegroundCB = JocysCom_NameBattlegroundCheckButton:GetChecked();
	JocysCom_NBattlegroundLCB = JocysCom_NameBattlegroundLeaderCheckButton:GetChecked();
	JocysCom_NRaidCB = JocysCom_NameRaidCheckButton:GetChecked();
	JocysCom_NRaidLCB = JocysCom_NameRaidLeaderCheckButton:GetChecked();
	JocysCom_NInstanceCB = JocysCom_NameInstanceCheckButton:GetChecked();
	JocysCom_NInstanceLCB = JocysCom_NameInstanceLeaderCheckButton:GetChecked();
end

function JocysCom_SetInterfaceSettings()
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
	JocysCom_SaveButton:Hide();
	JocysCom_OptionsButton:Hide();
	-- Load descriptions.
	JocysCom_Text_EN();	
end

-- Load UI settings.
JocysCom_SetInterfaceSettings();

-- Register events (and on ADDON_LOADED event load toc file values and set them).
JocysCom_RegisterEvents();

