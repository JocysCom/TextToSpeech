-- /fstack - show frame names.
-- /run local m=MinimapCluster if m:IsShown()then m:Hide()else m:Show()end - Show / Hide MinimapCluster.

-- Debug mode true(enabled) or false(disabled).
local DebugEnabled = false;

-- Set variables.
local unitName = UnitName("player");
local realmName = GetRealmName();
local questMessage = nil;
local speakMessage = nil;
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
local objectiveTitleText = "Your objective is to";
local messageDoNotDisturb = "Please wait... NPC dialog window is open and text-to-speech is enabled.";
local messageStop = "<voice command=\"stop\" />";

-- Set text.
local function JocysCom_Text_EN()
	-- Title.
	JocysCom_OptionsFrame.TitleText:SetText("Jocys.com Text to Speech World of Warcraft Addon 2.2.26 ( 2015-04-26 )");
	-- CheckButtons.
	JocysCom_FilterCheckButton.text:SetText("|cff808080 Hide addon's whisper messages in chat window.|r");
	JocysCom_DndCheckButton.text:SetText("|cff808080 Show |cffffffff<Busy>|r |cff808080over my character for other players, when NPC dialogue window is open and speech is on.|r");
	JocysCom_LockCheckButton.text:SetText("|cff808080 Lock mini frame with |cffffffff[Stop]|r |cff808080button.|r");
	JocysCom_MenuCheckButton.text:SetText("|cff808080 [Checked] show menu on the right side of |cffffffff[Stop]|r |cff808080button. [Unchecked] show menu on the left side.|r");
	JocysCom_StopOnCloseCheckButton.text:SetText(" |cff808080Play after closing|r");
	JocysCom_QuestCheckButton.text:SetText("|cffdddddd Q|r");
	JocysCom_MonsterCheckButton.text:SetText("|cfffffb9f N|r");
	JocysCom_WhisperCheckButton.text:SetText("|cffffb2eb W|r");
	JocysCom_SayCheckButton.text:SetText("|cffffffff S|r");
	JocysCom_PartyCheckButton.text:SetText("|cffaaa7ff P|r");
	JocysCom_MiniMenuFrame_FontString:SetText("");
	JocysCom_GuildCheckButton.text:SetText("|cff40fb40 G|r");	
	JocysCom_RaidCheckButton.text:SetText("|cffff7d00 R|r");
	JocysCom_RaidLeaderCheckButton.text:SetText("|cffff4709 RL|r");
	JocysCom_BattlegroundCheckButton.text:SetText("|cffff7d00 B|r");
	JocysCom_BattlegroundLeaderCheckButton.text:SetText("|cffff4709 BL|r");
	JocysCom_InstanceCheckButton.text:SetText("|cffff7d00 I|r");
	JocysCom_InstanceLeaderCheckButton.text:SetText("|cffff4709 IL|r");
	-- Font Strings.
	JocysCom_DialogueScrollFrame_FontString:SetText("When mouse pointer is over this frame...\n\nSCROLL UP will START SPEECH\n\nSCROLL DOWN will STOP SPEECH");
	JocysCom_DescriptionFrameFontString:SetText("Text-to-speech voices, pitch, rate, effects, etc. ... you will find all options in |cff77ccffJocys.Com Text to Speech Monitor|r.\n\nHow it works: When you open NPC dialogue window, |cff77ccffJocys.Com Text to Speech WoW Addon|r creates and sends special whisper message to yourself (message includes dialogue text, character name and effect name). Then, |cff77ccffJocys.Com Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.");
	JocysCom_ReplaceNameFontString:SetText("Here you can change your name for text to speech from |cff00ff00" .. unitName .. "|r to something else.");
	JocysCom_MessageForMonitorFrameFontString:SetText("Whisper message for |cff77ccffJocys.Com Text to Speech Monitor|r ... it must be runninng in background:");	
end

-- Unlock frames.
local function JocysCom_UnLockFrames()
	JocysCom_StopButtonFrame_Texture:SetTexture(0, 0, 0, 0.5);
	JocysCom_DialogueScrollFrame:EnableMouse(true);
	JocysCom_DialogueScrollFrame_Texture:SetTexture(0, 0, 0, 0.8);
	JocysCom_DialogueScrollFrame_FontString:Show();
	JocysCom_DialogueScrollFrameResizeButton:Show();
end

-- Lock frames.
local function JocysCom_LockFrames()
	JocysCom_StopButtonFrame_Texture:SetTexture(0, 0, 0, 0);
	JocysCom_DialogueScrollFrame:EnableMouse(false);
	JocysCom_DialogueScrollFrame_Texture:SetTexture(0, 0, 0, 0);
	JocysCom_DialogueScrollFrame_FontString:Hide();
	JocysCom_DialogueScrollFrameResizeButton:Hide();
end

-- MessageStop function.
local function sendChatMessageStop(CloseOrButton, group)
	-- Add group value if exists.
	if group == nil or group == "" then
		messageStop = "<voice command=\"stop\" />";
	else
		messageStop = "<voice command=\"stop\" group=\"" .. group .. "\" />";
	end			
	-- Disable DND <Busy> if checked.
	if JocysCom_DndCheckButton:GetChecked() == true and UnitIsDND("player") == true then
		SendChatMessage("", "DND");
	end
	if (stopWhenClosing == 1 or group ~= nil or group ~= "") and (JocysCom_StopOnCloseCheckButton:GetChecked() ~= true or CloseOrButton == 1) then
		SendChatMessage(messageStop, "WHISPER", "Common", unitName);
		stopWhenClosing = 0;
		JocysCom_OptionsEditBox:SetText("|cff808080" .. messageStop .. "|r");
	end
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
	-- Chat SAY events.
	JocysCom_OptionsFrame:RegisterEvent("CHAT_MSG_SAY");
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
	-- Debug.
	if DebugEnabled then print("1. " .. event) end
	-- Events.
	if event == "ADDON_LOADED" then
		JocysCom_LoadTocFileSettings();
		JocysCom_SetInterfaceSettings();
		return;
	elseif event == "PLAYER_LOGOUT" then
		JocysCom_SaveTocFileSettings();
		return;
	elseif JocysCom_DialogueMiniFrame:IsShown() and (string.find(event, "QUEST") ~= nil or event == "GOSSIP_SHOW" or event == "ITEM_TEXT_READY") then
		group = "Quest";
		questMessage = nil;
		if event == "QUEST_GREETING" then 
			speakMessage = GetGreetingText();
		elseif GossipFrame:IsShown() and event == "GOSSIP_SHOW" then
			speakMessage = GetGossipText();
		elseif event == "QUEST_DETAIL" then
			speakMessage = GetQuestText() .. " " .. objectiveTitleText .. " " .. GetObjectiveText();
		elseif event == "QUEST_PROGRESS" then
			speakMessage = GetProgressText();
		elseif event == "QUEST_COMPLETE" then
			speakMessage = GetRewardText();
		elseif event == "ITEM_TEXT_READY" then
			speakMessage = ItemTextGetText();
		end	
		questMessage = speakMessage;
		arg2 = "";	
		if JocysCom_QuestCheckButton:GetChecked() ~= true then return end -- Don't proceed if "auto-start" speech check-box is disabled.	
	elseif event == "GOSSIP_CLOSED" then
		JocysCom_DialogueMiniFrame_Hide();
		return;
	-- Chat events.
	elseif JocysCom_MonsterCheckButton:GetChecked() == true and string.find(event, "MSG_MONSTER") ~= nil then	
		-- don't proceed repetitive NPC messages by the same NPC.
		if (lastArg == arg2 .. arg1) then return else lastArg = arg2 .. arg1 end
		group = "Monster";
	elseif JocysCom_WhisperCheckButton:GetChecked() == true and (event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_WHISPER") then
		group = "Whisper";
		-- replace friend's real name with character name.
		if event == "CHAT_MSG_BN_WHISPER" then
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
	elseif JocysCom_WhisperCheckButton:GetChecked() == true and string.find(event, "WHISPER_INFORM") ~= nil then
		group = "Whisper";
		-- arg2 = unitName;
	elseif JocysCom_SayCheckButton:GetChecked() == true and (event == "CHAT_MSG_SAY") then
		group = "Say";
	elseif JocysCom_PartyCheckButton:GetChecked() == true and string.find(event, "MSG_PARTY") ~= nil then
		group = "Party";
	elseif JocysCom_GuildCheckButton:GetChecked() == true and (event == "CHAT_MSG_GUILD") then
		group = "Guild";
	elseif JocysCom_RaidCheckButton:GetChecked() == true and string.find(event, "MSG_RAID") ~= nil then
		group = "Raid";
	elseif JocysCom_RaidLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING") then
		group="RaidLeader";
	elseif JocysCom_BattlegroundCheckButton:GetChecked() == true and string.find(event, "MSG_BATTLEGROUND") ~= nil then 
		group="BG";
	elseif JocysCom_BattlegroundLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_BATTLEGROUND_LEADER") then
		group="BGLeader";
	elseif JocysCom_InstanceCheckButton:GetChecked() == true and string.find(event, "MSG_INSTANCE") ~= nil then 
		group="Instance";
	elseif JocysCom_InstanceLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then
		group="InstanceLeader";
	else
		return;
	end
	-- If event CHAT, speakMessage is arg1.
	if string.find(event, "CHAT") ~= nil then
		speakMessage = arg1;
	end
	 -- don't proceed messages with <voice> tags and your own incoming whispers.
	if string.find(speakMessage, "<voice") ~= nil or (event == "CHAT_MSG_WHISPER" and string.find(arg2, unitName) ~= nil) then return end
	-- Remove realm name from name.
	dashIndex = string.find(arg2, "-");
	if dashIndex ~= nil then arg2 = string.sub(arg2, 1, dashIndex - 1) end
	-- add leader intro.
	messageLeader = "";
	if string.find(group, "Leader") ~= nil then
	messageLeader = string.gsub(group, "BG", "Battleground");
	messageLeader = string.gsub(group, "Leader", " leader");
	end
	-- Set "whispers", "says" or "yells".
	messageType = "";
	if string.find(event, "CHAT") ~= nil and string.find(event, "EMOTE") == nil then messageType = " says. " end
	if string.find(event, "WHISPER") ~= nil then messageType = " whispers. " end
	if string.find(event, "YELL") ~= nil then messageType = " yells. " end
	-- Final message.
	speakMessage = messageLeader .. arg2 .. messageType .. speakMessage;
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
		sendChatMessageStop(1); -- Send stop message.
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
		local chatMessageSP = "<voice command=\"play\"" .. group .. NPCName .. NPCGender .. NPCType .. "><part>";
		if NPCGender == "" then
		chatMessageSP = "<voice" .. group .. NPCName .. NPCType .. " command=\"play\"><part>";
		end
		local chatMessageSA = "<voice command=\"add\"><part>";
		local chatMessageE = "</part></voice>";
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
	JocysCom_MiniMenuFrame:SetPoint("LEFT", JocysCom_StopButtonFrame, "RIGHT", 0, 0);
	else
	JocysCom_MiniMenuFrame:SetPoint("RIGHT", JocysCom_StopButtonFrame, "LEFT", 0, 0);
	end
	JocysCom_SaveTocFileSettings();
end

-- Enable disable speech.
function JocysCom_CheckButton_OnClick(self, name)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if self:GetChecked() == true then
		if name == "StopAll" then
			sendChatMessageStop(1);
		elseif name == "Battleground" then
			JocysCom_BattlegroundLeaderCheckButton:SetChecked(false);
		elseif name == "BattlegroundLeader" and JocysCom_BattlegroundCheckButton:GetChecked() == true  then
			JocysCom_BattlegroundCheckButton:SetChecked(false);
			sendChatMessageStop(1, "Battleground");
		elseif name == "Raid" then
			JocysCom_RaidLeaderCheckButton:SetChecked(false);
		elseif name == "RaidLeader" and JocysCom_RaidCheckButton:GetChecked() == true then
			JocysCom_RaidCheckButton:SetChecked(false);
			sendChatMessageStop(1, "Raid");		
		elseif name == "Instance" then
			JocysCom_InstanceLeaderCheckButton:SetChecked(false);
		elseif name == "InstanceLeader" and JocysCom_InstanceCheckButton:GetChecked() == true then
			JocysCom_InstanceCheckButton:SetChecked(false);
			sendChatMessageStop(1, "Instance");
		end 
	else
		sendChatMessageStop(1, name);
	end
	JocysCom_SaveTocFileSettings();
end

-- Show MiniMenuFrame and set text.
 function JocysCom_MiniMenuFrame_Show(name)
 	local fontString = "";
	JocysCom_MiniMenuFrame_FontString:Show();
	if name == "Stop" then
		fontString = "|cffddddddStop text-to-speech and clear all playlist|r";
	elseif name == "Save" then
		fontString = "|cffddddddSave target name, gender and type in Monitor|r";
	elseif name == "Options" then
		fontString = "|cffddddddOpen text-to-speech Options window|r";
	elseif name == "Quest" then
		fontString = "|cffddddddPlay QUEST-DIALOGUE, BOOK, PLAQUE ... text|r";
	elseif name == "Monster" then
		fontString = "|cfffffb9fPlay NPC messages|r";
	elseif name == "Whisper" then
		fontString = "|cffffb2ebPlay WHISPER messages|r";
	elseif name == "Say" then
		fontString = "|cffffffffPlay SAY messages|r";
	elseif name == "Party" then
		fontString = "|cffaaa7ffPlay PARTY messages|r";
	elseif name == "Guild" then
		fontString = "|cff40fb40Play GUILD messages|r";
	elseif name == "Raid" then
		fontString = "|cffff7d00Play all RAID messages|r";
	elseif name == "RaidLeader" then
		fontString = "|cffff4709Play RAID leader messages only|r";
	elseif name == "Battleground" then
		fontString = "|cffff7d00Play all BATTLEGROUND messages|r";
	elseif name == "BattlegroundLeader" then
		fontString = "|cffff4709Play BATTLEGROUND leader messages only|r";
	elseif name == "Instance" then
		fontString = "|cffff7d00Play all INSTANCE messages|r";
	elseif name == "InstanceLeader" then
		fontString = "|cffff4709Play INSTANCE leader messages only|r";
	else
		JocysCom_MiniMenuFrame_FontString:Hide();
	end
	JocysCom_MiniMenuFrame:SetBackdrop({bgFile="Interface/AddOns/JocysCom-TextToSpeech-WoW/Images/JocysCom-MiniMenuFrame-Background"});
	JocysCom_MiniMenuFrame_FontString:SetText(fontString);
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
		sendChatMessageStop(1, name);
	else
		sendChatMessageStop(1);
	end
end

-- ScrollFrame - scroll-up or scroll-down.
function JocysCom_DialogueScrollFrame_OnMouseWheel(self, delta)
	if delta == 1 then
			JocysCom_PlayButtonButton_OnClick();
		else
			sendChatMessageStop(1, "Quest");
		end
end
-- ScrollFrame - start resizing.
function JocysCom_DialogueScrollFrameResizeButton_OnMouseDown(self) 
	self:GetParent():StartSizing("BOTTOMRIGHT");
	--self:GetParent():SetUserPlaced(true);
end
-- ScrollFrame - stop resizing.
function JocysCom_DialogueScrollFrameResizeButton_OnMouseUp(self)
	self:GetParent():StopMovingOrSizing();
end

-- Show or Hide JocysCom frames.
local function JocysCom_AttachAndShowFrames(frame)
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
	JocysCom_DialogueMiniFrame:Show();
	JocysCom_DialogueMiniFrame:SetBackdrop({bgFile="Interface/AddOns/JocysCom-TextToSpeech-WoW/Images/JocysCom-MiniFrame-Background"});
end

-- Close all JocysCom frames.
function JocysCom_DialogueMiniFrame_OnHide()
	if DebugEnabled then print("MiniFrame Hide") end
	sendChatMessageStop(0, "Quest");
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
	local saveMessage = "<voice command=\"Save\" name=\"" .. targetName .. "\" gender=\"" .. targetSex .. "\" effect=\"" .. targetType .. "\" />";
	SendChatMessage(saveMessage, "WHISPER", "Common", unitName);
	JocysCom_OptionsEditBox:SetText("|cff808080" .. saveMessage .. "|r");
end

function JocysCom_OptionsFrame_OnHide()
	if string.len(JocysCom_ReplaceNameEditBox:GetText()) < 2 then
		JocysCom_ReplaceNameEditBox:SetText(unitName);
	end
	JocysCom_LockFrames();
end

-- [ Play ] button.
function JocysCom_PlayButtonButton_OnClick(self)
	if QuestLogPopupDetailFrame:IsShown() or WorldMapFrame:IsShown() then
		local questDescription, questObjectives = GetQuestLogQuestText();
		questMessage = questObjectives .. " Description. " .. questDescription;
		-- questPortrait, questPortraitText, questPortraitName = GetQuestLogPortraitGiver();
		-- print(" questPortrait: " .. tostring(questPortrait) .. " questPortraitText: " .. tostring(questPortraitText) .. " questPortraitName: " .. tostring(questPortraitName));
	end
	JocysCom_SpeakMessage(questMessage, "SROLL_UP_OR_PLAY_BUTTON", "", "Quest");
end

-- Enable message filter.
function JocysCom_CHAT_MSG_WHISPER_INFORM(self, event, msg)
	if string.find(msg, "<voice") ~= nil then
		return true;
	end
end
-- Enable/Disable message filter.
function JocysCom_CHAT_MSG_WHISPER(self, event, msg)
	if string.find(msg, "<voice") ~= nil and JocysCom_FilterCheckButton:GetChecked() == true then
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

-- Load settings from toc file.
function JocysCom_LoadTocFileSettings()
	-- Set CheckBoxes.
	if JocysCom_DndCheckButtonValue == false then JocysCom_DndCheckButton:SetChecked(false) else JocysCom_DndCheckButton:SetChecked(true) end
	if JocysCom_LockCheckButtonValue == false then JocysCom_LockCheckButton:SetChecked(false) else JocysCom_LockCheckButton:SetChecked(true) end
	if JocysCom_MenuCheckButtonValue == false then JocysCom_MenuCheckButton:SetChecked(false) else JocysCom_MenuCheckButton:SetChecked(true) end
	if JocysCom_FilterCheckButtonValue == false then JocysCom_FilterCheckButton:SetChecked(false) else JocysCom_FilterCheckButton:SetChecked(true) end
	if JocysCom_QuestCheckButtonValue == false then JocysCom_QuestCheckButton:SetChecked(false) else JocysCom_QuestCheckButton:SetChecked(true) end
	if JocysCom_MonsterCheckButtonValue == false then JocysCom_MonsterCheckButton:SetChecked(false) else JocysCom_MonsterCheckButton:SetChecked(true) end
	if JocysCom_WhisperCheckButtonValue == false then JocysCom_WhisperCheckButton:SetChecked(false) else JocysCom_WhisperCheckButton:SetChecked(true) end
	if JocysCom_SayCheckButtonValue == false then JocysCom_SayCheckButton:SetChecked(false) else JocysCom_SayCheckButton:SetChecked(true) end
	if JocysCom_PartyCheckButtonValue == false then JocysCom_PartyCheckButton:SetChecked(false) else JocysCom_PartyCheckButton:SetChecked(true) end
	if JocysCom_GuildCheckButtonValue == false then JocysCom_GuildCheckButton:SetChecked(false) else JocysCom_GuildCheckButton:SetChecked(true) end
	if JocysCom_BattlegroundCheckButtonValue == false then JocysCom_BattlegroundCheckButton:SetChecked(false) else JocysCom_BattlegroundCheckButton:SetChecked(true) end
	if JocysCom_BattlegroundLeaderCheckButtonValue == true then JocysCom_BattlegroundLeaderCheckButton:SetChecked(true) else JocysCom_BattlegroundLeaderCheckButton:SetChecked(false) end
	if JocysCom_RaidCheckButtonValue == false then JocysCom_RaidCheckButton:SetChecked(false) else JocysCom_RaidCheckButton:SetChecked(true) end
	if JocysCom_RaidLeaderCheckButtonValue == true then JocysCom_RaidLeaderCheckButton:SetChecked(true) else JocysCom_RaidLeaderCheckButton:SetChecked(false) end
	if JocysCom_InstanceCheckButtonValue == false then JocysCom_InstanceCheckButton:SetChecked(false) else JocysCom_InstanceCheckButton:SetChecked(true) end
	if JocysCom_InstanceLeaderCheckButtonValue == true then JocysCom_InstanceLeaderCheckButton:SetChecked(true) else JocysCom_InstanceLeaderCheckButton:SetChecked(false) end
	if JocysCom_StopOnCloseCheckButtonValue == true then JocysCom_StopOnCloseCheckButton:SetChecked(true) else JocysCom_StopOnCloseCheckButton:SetChecked(false) end
	if JocysCom_ReplaceNameEditBoxValue == "" or JocysCom_ReplaceNameEditBoxValue == nil then JocysCom_ReplaceNameEditBoxValue = unitName else JocysCom_ReplaceNameEditBox:SetText(JocysCom_ReplaceNameEditBoxValue) end
end

-- Save settings.
function JocysCom_SaveTocFileSettings()
	-- Save check buttons.
	JocysCom_DndCheckButtonValue = JocysCom_DndCheckButton:GetChecked();
	JocysCom_LockCheckButtonValue = JocysCom_LockCheckButton:GetChecked();
	JocysCom_MenuCheckButtonValue = JocysCom_MenuCheckButton:GetChecked();
	JocysCom_FilterCheckButtonValue = JocysCom_FilterCheckButton:GetChecked();
	JocysCom_QuestCheckButtonValue = JocysCom_QuestCheckButton:GetChecked();
	JocysCom_MonsterCheckButtonValue = JocysCom_MonsterCheckButton:GetChecked();
	JocysCom_WhisperCheckButtonValue = JocysCom_WhisperCheckButton:GetChecked();
	JocysCom_SayCheckButtonValue = JocysCom_SayCheckButton:GetChecked();
	JocysCom_PartyCheckButtonValue = JocysCom_PartyCheckButton:GetChecked();
	JocysCom_GuildCheckButtonValue = JocysCom_GuildCheckButton:GetChecked();
	JocysCom_BattlegroundLeaderCheckButtonValue = JocysCom_BattlegroundLeaderCheckButton:GetChecked();
	JocysCom_BattlegroundCheckButtonValue = JocysCom_BattlegroundCheckButton:GetChecked();
	JocysCom_RaidCheckButtonValue = JocysCom_RaidCheckButton:GetChecked();
	JocysCom_RaidLeaderCheckButtonValue = JocysCom_RaidLeaderCheckButton:GetChecked();
	JocysCom_InstanceLeaderCheckButtonValue = JocysCom_InstanceLeaderCheckButton:GetChecked();
	JocysCom_InstanceCheckButtonValue = JocysCom_InstanceCheckButton:GetChecked();
	JocysCom_StopOnCloseCheckButtonValue = JocysCom_StopOnCloseCheckButton:GetChecked();
	JocysCom_ReplaceNameEditBoxValue = JocysCom_ReplaceNameEditBox:GetText();
end

function JocysCom_SetInterfaceSettings()
	-- Add chat message filters.
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER_INFORM", JocysCom_CHAT_MSG_WHISPER_INFORM);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER", JocysCom_CHAT_MSG_WHISPER);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_DND", JocysCom_CHAT_MSG_DND);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_SYSTEM", JocysCom_CHAT_MSG_SYSTEM);
	-- Set OptionsFrame movable.
	JocysCom_OptionsFrame:Hide();
	JocysCom_OptionsFrame:SetMovable(true);
	JocysCom_OptionsFrame:EnableMouse(true);
	JocysCom_OptionsFrame:RegisterForDrag("LeftButton");
	JocysCom_OptionsFrame:SetScript("OnDragStart", JocysCom_OptionsFrame.StartMoving);
	JocysCom_OptionsFrame:SetScript("OnDragStop", JocysCom_OptionsFrame.StopMovingOrSizing);
	JocysCom_OptionsFrame:SetScript("OnShow", JocysCom_UnLockFrames);
	-- Set OptionsEditBox child of OptionsScrollFrame.
	JocysCom_OptionsFrameScroll:SetScrollChild(JocysCom_OptionsEditBox);
	-- Set ScrollFrame movable.
	JocysCom_DialogueScrollFrame:SetMovable(true);
	JocysCom_DialogueScrollFrame:SetResizable(true);
	--JocysCom_DialogueScrollFrame:EnableMouse(true);
	JocysCom_DialogueScrollFrame:RegisterForDrag("LeftButton");
	JocysCom_DialogueScrollFrame:SetScript("OnDragStart", JocysCom_OptionsFrame.StartMoving);
	JocysCom_DialogueScrollFrame:SetScript("OnDragStop", JocysCom_OptionsFrame.StopMovingOrSizing);
	JocysCom_DialogueScrollFrame:SetScript("OnMouseWheel", JocysCom_DialogueScrollFrame_OnMouseWheel);
	-- Attach DialogueScroll and DialogueMini frames to WoW frame.
	JocysCom_AttachAndShowFrames(GossipFrame);
	-- StopButtonFrame.
	JocysCom_StopButtonFrame:SetMovable(true);
	JocysCom_StopButtonFrame:EnableMouse(true);
	JocysCom_StopButtonFrame:RegisterForDrag("LeftButton");
	JocysCom_StopButtonFrame:SetScript("OnDragStart", JocysCom_DialogueMiniFrame.StartMoving);
	JocysCom_StopButtonFrame:SetScript("OnDragStop", JocysCom_DialogueMiniFrame.StopMovingOrSizing);
	if JocysCom_LockCheckButton:GetChecked() == true then 
		JocysCom_StopButtonFrame:RegisterForDrag();
	else
		JocysCom_StopButtonFrame:RegisterForDrag("LeftButton");
	end
	-- MiniMenuFrame.
	JocysCom_MiniMenuFrame:Hide();
	JocysCom_MiniMenuFrame:EnableMouse(true);
	JocysCom_MiniMenuFrame_FontString:Hide();
	JocysCom_MiniMenuFrame:ClearAllPoints();
	if JocysCom_MenuCheckButton:GetChecked() == true then
	JocysCom_MiniMenuFrame:SetPoint("LEFT", JocysCom_StopButtonFrame, "RIGHT", 0, 0);
	else
	JocysCom_MiniMenuFrame:SetPoint("RIGHT", JocysCom_StopButtonFrame, "LEFT", 0, 0);
	end
	-- Set Save and Options buttons.
	JocysCom_SaveButton:Hide();
	JocysCom_OptionsButton:Hide();
	-- Black Background for message frame.
	JocysCom_MessageFrame.Bg:SetTexture(0, 0, 0, 1.0);
	-- Load descriptions.
	JocysCom_Text_EN();	
	-- CheckBox label position.
	JocysCom_StopOnCloseCheckButton.text:SetPoint("LEFT", 19, 1);
	JocysCom_QuestCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	JocysCom_MonsterCheckButton.text:SetPoint("TOPLEFT", 6, 10);
	JocysCom_WhisperCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	JocysCom_SayCheckButton.text:SetPoint("TOPLEFT", 7, 10);
	JocysCom_PartyCheckButton.text:SetPoint("TOPLEFT", 7, 10);
	JocysCom_GuildCheckButton.text:SetPoint("TOPLEFT", 6, 10);
	JocysCom_BattlegroundCheckButton.text:SetPoint("TOPLEFT", 7, 10);
	JocysCom_BattlegroundLeaderCheckButton.text:SetPoint("TOPLEFT", 4, 10);
	JocysCom_RaidCheckButton.text:SetPoint("TOPLEFT", 6, 10);
	JocysCom_RaidLeaderCheckButton.text:SetPoint("TOPLEFT", 4, 10);
	JocysCom_InstanceCheckButton.text:SetPoint("TOPLEFT", 8, 10);
	JocysCom_InstanceLeaderCheckButton.text:SetPoint("TOPLEFT", 6, 10);
	-- OnEscape scripts.
	JocysCom_OptionsEditBox:SetScript("OnEscapePressed", JocysCom_OptionsButton_OnClick);
	JocysCom_ReplaceNameEditBox:SetScript("OnEscapePressed", JocysCom_OptionsButton_OnClick);
	-- OnShow sripts.
	QuestLogPopupDetailFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	GossipFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	QuestFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	ItemTextFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	QuestMapDetailsScrollFrame:SetScript("OnShow", function()
	JocysCom_AttachAndShowFrames(WorldMapFrame);
	end);
	-- OnHide sripts.
	QuestMapDetailsScrollFrame:SetScript("OnHide", JocysCom_DialogueMiniFrame_Hide);
end

-- Register events (on ADDON_LOADED event load toc file values and set them).
JocysCom_RegisterEvents();

