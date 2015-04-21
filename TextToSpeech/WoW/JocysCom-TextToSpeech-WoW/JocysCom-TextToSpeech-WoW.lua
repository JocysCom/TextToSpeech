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
local MessageIsFromChat = false;
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
	JocysCom_OptionsFrame.TitleText:SetText("Jocys.com Text to Speech World of Warcraft Addon 2.2.23 ( 2015-04-15 )");
	-- CheckButtons.
	JocysCom_FilterCheckButton.text:SetText("|cff808080 Hide addon's whisper messages in chat window.|r");
	JocysCom_DndCheckButton.text:SetText("|cff808080 Show|r |cffffffff<Busy>|r|cff808080 over my character for other players, when NPC dialogue window is open and speech is on.|r");
	JocysCom_AutoCheckButton.text:SetText("|cff808080 Auto start speech, when dialog window is open (except for quest log windows).|r");
	JocysCom_LockCheckButton.text:SetText("|cff808080 Lock mini frame with|r |cffffffff[Stop]|r |cff808080button and|r |cffffffffNPC|r|cff808080,|r |cffffffffWhisper|r|cff808080,|r |cffffffffSay|r|cff808080,|r |cffffffffParty|r|cff808080,|r |cffffffff...|r |cff808080check-boxes.|r");
	JocysCom_StopOnCloseCheckButton.text:SetText(" |cff808080Play after closing|r");
	JocysCom_MonsterCheckButton.text:SetText("|cfffffb9f N|r");
	JocysCom_WhisperCheckButton.text:SetText("|cffffb2eb W|r");
	JocysCom_SayCheckButton.text:SetText("|cffffffff S|r");
	JocysCom_PartyCheckButton.text:SetText("|cffaaa7ff P|r");
	JocysCom_CheckButtonTooltipFontString:SetText("");
	JocysCom_GuildCheckButton.text:SetText("|cff40fb40 G|r");	
	JocysCom_RaidCheckButton.text:SetText("|cffff7d00 R|r");
	JocysCom_RaidLeaderCheckButton.text:SetText("|cffff4709 RL|r");
	JocysCom_BattlegroundCheckButton.text:SetText("|cffff7d00 B|r");
	JocysCom_BattlegroundLeaderCheckButton.text:SetText("|cffff4709 BL|r");
	JocysCom_InstanceCheckButton.text:SetText("|cffff7d00 I|r");
	JocysCom_InstanceLeaderCheckButton.text:SetText("|cffff4709 IL|r");
	-- Font Strings.
	JocysCom_ScrollFrameFontString:SetText("When mouse pointer is over this frame...\n\nSCROLL UP will START SPEECH\n\nSCROLL DOWN will STOP SPEECH");
	JocysCom_DescriptionFrameFontString:SetText("Text-to-speech voices, pitch, rate, effects, etc. ... you will find all options in |cff77ccffJocys.Com Text to Speech Monitor|r.\n\nHow it works: When you open NPC dialogue window, |cff77ccffJocys.Com Text to Speech WoW Addon|r creates and sends special whisper message to yourself (message includes dialogue text, character name and effect name). Then, |cff77ccffJocys.Com Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.");
	JocysCom_ReplaceNameFontString:SetText("Here you can change your name for text to speech from |cff00ff00" .. unitName .. "|r to something else.");
	JocysCom_MessageForMonitorFrameFontString:SetText("Whisper message for |cff77ccffJocys.Com Text to Speech Monitor|r ... it must be runninng in background:");	
end

-- Unlock frames.
local function JocysCom_UnLockFrames()
	JocysCom_MiniFrame:SetMovable(true);
	JocysCom_MiniFrame:EnableMouse(true);
	JocysCom_MiniFrame:RegisterForDrag("LeftButton");
	JocysCom_MiniFrame:SetScript("OnDragStart", JocysCom_MiniFrame.StartMoving);
	JocysCom_MiniFrame:SetScript("OnDragStop", JocysCom_MiniFrame.StopMovingOrSizing);
	JocysCom_StopFrame.texture:SetTexture(0, 0, 0, 0.8);
	JocysCom_ScrollFrame:SetMovable(true);
	JocysCom_ScrollFrame:SetResizable(true);
	JocysCom_ScrollFrame:EnableMouse(true);
	JocysCom_ScrollFrame.texture:SetTexture(0, 0, 0, 0.8);
	JocysCom_ScrollFrameFontString:Show();
	JocysCom_ScrollFrame.resizeButton:Show();
	JocysCom_ScrollFrame:SetFrameLevel(100);
	JocysCom_OptionsFrame:SetFrameLevel(11);
	JocysCom_StopFrame:SetFrameLevel(10);
end

-- Lock frames.
local function JocysCom_LockFrames()
	JocysCom_MiniFrame:SetMovable(nil);
	JocysCom_StopFrame.texture:SetTexture(0, 0, 0, 0);
	JocysCom_ScrollFrame:SetMovable(nil);
	JocysCom_ScrollFrame:SetResizable(nil);
	JocysCom_ScrollFrame:EnableMouse(nil);
	JocysCom_ScrollFrame.texture:SetTexture(0, 0, 0, 0);
	JocysCom_ScrollFrameFontString:Hide();
	JocysCom_ScrollFrame.resizeButton:Hide();
	JocysCom_ScrollFrame:SetFrameLevel(100);
	JocysCom_OptionsFrame:SetFrameLevel(11);
	JocysCom_StopFrame:SetFrameLevel(10);
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
		JocysCom_QuestEditBox:SetText("|cff808080" .. messageStop .. "|r");
	end
end

-- Enable message filter.
function JocysCom_CHAT_MSG_WHISPER_INFORM(self, event, msg)
	if msg:find("<voice") then
		return true;
	end
end
-- Enable/Disable message filter.
function JocysCom_CHAT_MSG_WHISPER(self, event, msg)
	if msg:find("<voice") and JocysCom_FilterCheckButton:GetChecked() == true then
		return true;
	end
end
-- Enable/Disable message filter
function JocysCom_CHAT_MSG_DND(self, event, msg)
	if msg:find("<" .. unitName .. ">: ") and JocysCom_FilterCheckButton:GetChecked() == true then
		return true;
	end
end
-- Enable/Disable message filter
function JocysCom_CHAT_MSG_SYSTEM(self, event, msg)
	if msg:find("You are no") and JocysCom_FilterCheckButton:GetChecked() == true then
		return true;
	end
end

-- Filter messages.
function JocysCom_MessageEventFilters()
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER_INFORM", JocysCom_CHAT_MSG_WHISPER_INFORM);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER", JocysCom_CHAT_MSG_WHISPER);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_DND", JocysCom_CHAT_MSG_DND);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_SYSTEM", JocysCom_CHAT_MSG_SYSTEM);
end

-- Options frame on load.
function JocysCom_OptionsFrame_OnLoad()
	JocysCom_OptionsFrame:SetPoint("TOPLEFT", 356, -116);
	JocysCom_OptionsFrame:SetMovable(true);
	JocysCom_OptionsFrame:EnableMouse(true);
	JocysCom_OptionsFrame:RegisterForDrag("LeftButton");
	JocysCom_OptionsFrame:SetScript("OnDragStart", JocysCom_OptionsFrame.StartMoving);
	JocysCom_OptionsFrame:SetScript("OnDragStop", JocysCom_OptionsFrame.StopMovingOrSizing);
	-- Setup title.
	JocysCom_OptionsFrame.TitleText:SetPoint("TOP", 0, -6);
	-- Hide messages.
	JocysCom_MessageEventFilters();
	-- Attach events.
	JocysCom_OptionsFrame:SetScript("OnEvent", JocysCom_OptionsFrame_OnEvent);
	-- Addon loaded event.
	JocysCom_OptionsFrame:RegisterEvent("ADDON_LOADED");
	-- Dialogue events (open).
	JocysCom_OptionsFrame:RegisterEvent("QUEST_GREETING");
	JocysCom_OptionsFrame:RegisterEvent("GOSSIP_SHOW");
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
		JocysCom_LoadAllSettings();
		return;
	elseif event == "PLAYER_LOGOUT" then
		JocysCom_SaveAllSettings();
		return;
	elseif JocysCom_MiniFrame:IsShown() and (event == "QUEST_GREETING" or event == "GOSSIP_SHOW" or event == "QUEST_DETAIL" or event == "QUEST_PROGRESS" or event == "QUEST_COMPLETE" or event == "ITEM_TEXT_READY") then
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
		if JocysCom_AutoCheckButton:GetChecked() ~= true then return end -- Don't proceed if "auto-start" speech check-box is disabled.	
	elseif event == "GOSSIP_CLOSED" then
		JocysCom_MiniFrame_Hide();
		return;
	-- Chat events.
	elseif JocysCom_MonsterCheckButton:GetChecked() == true and (event == "CHAT_MSG_MONSTER_EMOTE" or event == "CHAT_MSG_MONSTER_WHISPER" or event == "CHAT_MSG_MONSTER_SAY" or event == "CHAT_MSG_MONSTER_PARTY" or event == "CHAT_MSG_MONSTER_YELL") then
		-- don't proceed repetitive NPC messages by the same NPC.
		if (lastArg == arg2 .. arg1) then return else lastArg = arg2 .. arg1 end
		group = "NPC";
		speakMessage = arg1;
	elseif JocysCom_WhisperCheckButton:GetChecked() == true and (event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_WHISPER") then
		group = "Whisper";
		speakMessage = arg1;
		-- replace friend's real name with character name.
		if event == "CHAT_MSG_BN_WHISPER" then
			-- totalBNet, numBNetOnline = BNGetNumFriends();
			-- presenceID, presenceName, battleTag, isBattleTagPresence, toonName, toonID, client, isOnline, lastOnline, isAFK, isDND, messageText, noteText, isRIDFriend, broadcastTime, canSoR = BNGetFriendInfoByID(7);		
			arg2 = string.gsub(arg2, "|", " ");
			for i in string.gmatch(arg2, "%S+") do
				if string.find(i, "Kf") then
					arg2 = string.gsub(i, "Kf", "");
				end
			end	
			presenceID, presenceName, battleTag, isBattleTagPresence, toonName = BNGetFriendInfoByID(arg2);
			arg2 = toonName;
		end	
	elseif JocysCom_WhisperCheckButton:GetChecked() == true and (event == "CHAT_MSG_WHISPER_INFORM" or event == "CHAT_MSG_BN_WHISPER_INFORM") then
		group = "Whisper";
		arg2 = unitName;
		speakMessage = arg1;
	elseif JocysCom_SayCheckButton:GetChecked() == true and (event == "CHAT_MSG_SAY") then
		group = "Say";
		speakMessage = arg1;
	elseif JocysCom_PartyCheckButton:GetChecked() == true and (event == "CHAT_MSG_PARTY" or event == "CHAT_MSG_PARTY_LEADER") then
		group = "Party";
		speakMessage = arg1;
	elseif JocysCom_GuildCheckButton:GetChecked() == true and (event == "CHAT_MSG_GUILD") then
		group = "Guild";
		speakMessage = arg1;
	elseif JocysCom_RaidCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID" or event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING") then
		group = "Raid";
		speakMessage = arg1;
	elseif JocysCom_RaidLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING") then
		group="RaidLeader";
		speakMessage = arg1;
	elseif JocysCom_BattlegroundCheckButton:GetChecked() == true and (event == "CHAT_MSG_BATTLEGROUND" or event == "CHAT_MSG_BATTLEGROUND_LEADER") then 
		group="BG";
		speakMessage = arg1;	
	elseif JocysCom_BattlegroundLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_BATTLEGROUND_LEADER") then
		group="BGLeader";
		speakMessage = arg1;
	elseif JocysCom_InstanceCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT" or event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then 
		group="Instance";
		speakMessage = arg1;
	elseif JocysCom_InstanceLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then
		group="InstanceLeader";
		speakMessage = arg1;
	end
	 -- don't proceed messages with <voice> tags and your own incoming whispers.
	print("start");
	print(event);
	print(speakMessage);

	if speakMessage:find("<voice") or (event == "CHAT_MSG_WHISPER" and arg2:find(unitName)) then return end
	print("end");
	-- Remove realm name from name.
	if string.find(arg2, "-") ~= nil then arg2 = string.sub(arg2, 1, dashIndex - 1)	end
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
	if (event == "CHAT_MSG_GUILD_ACHIEVEMENT" or event == "CHAT_MSG_MONSTER_EMOTE" or event == "CHAT_MSG_MONSTER_WHISPER" or event == "CHAT_MSG_MONSTER_SAY" or event == "CHAT_MSG_MONSTER_PARTY" or event == "CHAT_MSG_MONSTER_YELL" or event == "CHAT_MSG_WHISPER" or event == "CHAT_MSG_WHISPER_INFORM" or event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_BN_WHISPER_INFORM" or event == "CHAT_MSG_SAY" or event == "CHAT_MSG_PARTY" or event == "CHAT_MSG_PARTY_LEADER" or event == "CHAT_MSG_GUILD" or event == "CHAT_MSG_RAID" or event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING" or event == "CHAT_MSG_BATTLEGROUND" or event == "CHAT_MSG_BATTLEGROUND_LEADER" or event == "CHAT_MSG_INSTANCE_CHAT" or event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then
		MessageIsFromChat = true;
		dashIndex = string.find(speakMessage, "|");
		if dashIndex ~= nil then
			speakMessage = string.gsub(speakMessage, "%|cff(.-)%|h", "");
			speakMessage = string.gsub(speakMessage, "%|h%|r", "");
			speakMessage = string.gsub(speakMessage, "%[", " \"");
			speakMessage = string.gsub(speakMessage, "%]", "\" ");
		end
	else
		MessageIsFromChat = false;
		sendChatMessageStop(1); -- Send stop message.
	end
	-- Set NPC name.
	local NPCName = nil;
	if MessageIsFromChat == true then
		NPCName = name;
	else
		NPCName = GetUnitName("npc");
	end

	if NPCName == nil then
		NPCName = "";
	else
	NPCName = string.gsub(NPCName, "&", " and ");
	NPCName = string.gsub(NPCName, "\"", "");
	NPCName = " name=\"" .. NPCName .. "\"";
	end
	-- Set NPC gender (1 = Neutrum / Unknown, 2 = Male, 3 = Female). 
	if MessageIsFromChat == true then
		-- NPCSex = nil;
		NPCSex = UnitSex(name);
		-- print(NPCSex);
		-- print(name);
		if (event == "CHAT_MSG_GUILD_ACHIEVEMENT" or event == "CHAT_MSG_WHISPER" or event == "CHAT_MSG_WHISPER_INFORM" or event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_BN_WHISPER_INFORM" or event == "CHAT_MSG_SAY" or event == "CHAT_MSG_PARTY" or event == "CHAT_MSG_GUILD" or event == "CHAT_MSG_PARTY_LEADER"  or event == "CHAT_MSG_RAID" or event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING" or event == "CHAT_MSG_BATTLEGROUND" or event == "CHAT_MSG_BATTLEGROUND_LEADER" or event == "CHAT_MSG_INSTANCE_CHAT" or event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then
			NPCSex = UnitSex(name);
		end
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
	NPCGender = " gender=\"" .. NPCGender .. "\"";
	end
	-- Get NPC type (Beast, Dragonkin, Demon, Elemental, Giant, Undead, Humanoid, Critter, Mechanical, Not specified, Totem, Non-combat Pet, Gas Cloud).							
	local NPCType = UnitCreatureType("npc");
	if NPCType == nil then
		NPCType = "";
	else
		NPCType = " effect=\"" .. NPCType .. "\"";
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
	speakMessage = string.gsub(speakMessage, "%!", "! ");
	speakMessage = string.gsub(speakMessage, "%?", "? ");
	speakMessage = string.gsub(speakMessage, " %!", "!");
	speakMessage = string.gsub(speakMessage, "^%.", "");
	speakMessage = string.gsub(speakMessage, ">%.", ".>");
	speakMessage = string.gsub(speakMessage, "[%.]+", ". ");
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
		JocysCom_QuestEditBox:SetText(questEditBox);
		-- Enable DND <Busy> if checked.
		if JocysCom_DndCheckButton:GetChecked() == true and MessageIsFromChat == false then
		--if UnitIsDND("player") == false then
		SendChatMessage("<" .. unitName .. ">: " .. messageDoNotDisturb, "DND");
	    --end		
		end
end

-- Auto start speech.
function JocysCom_AutoCheckButton_OnClick(self)
 PlaySound("igMainMenuOptionCheckBoxOn");
 JocysCom_SaveAllSettings();
end

-- DND.
function JocysCom_DndCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	 -- Disable DND.
	if JocysCom_DndCheckButton:GetChecked() == false then
		if UnitIsDND("player") == true then
			SendChatMessage("", "DND");
		end
	else
		if UnitIsDND("player") == false then
			SendChatMessage("<" .. unitName .. ">: " .. messageDoNotDisturb, "DND");
	    end	
	end
	JocysCom_SaveAllSettings();
end

-- Lock Enable / Disable.
function JocysCom_LockCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_LockCheckButton:GetChecked() == true then
		JocysCom_SaveAllSettings();
		JocysCom_StopFrame:SetMovable(false);
		JocysCom_StopFrame:EnableMouse(false);
	else
		JocysCom_SaveAllSettings();
		JocysCom_StopFrame:SetMovable(true);
		JocysCom_StopFrame:EnableMouse(true);
	end
end

-- Continue play Enable / Disable.
function JocysCom_StopOnCloseCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	JocysCom_SaveAllSettings();
end

-- Clear and Hide JocysCom_MonsterCheckButtonFontString.
function JocysCom_CheckButtonTooltipFontString_OnLeave(self)
	JocysCom_CheckButtonTooltipFontString:Hide();
	JocysCom_CheckButtonTooltipFontString:SetText("");
	JocysCom_SaveAllSettings();
end

-- JocysCom_MonsterCheckButton functions.
function JocysCom_MonsterCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_MonsterCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "NPC");
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_MonsterCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cfffffb9f Play NPC messages|r");
end

-- JocysCom_WhisperCheckButton functions.
function JocysCom_WhisperCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_WhisperCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "Whisper");
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_WhisperCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffffb2eb Play WHISPER messages|r");
end

-- JocysCom_SayCheckButton functions.
function JocysCom_SayCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_SayCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "Say");
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_SayCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffffffff Play SAY messages|r");
end

-- JocysCom_PartyCheckButton functions.
function JocysCom_PartyCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_PartyCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "Party");
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_PartyCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffaaa7ff Play PARTY messages|r");
end

-- JocysCom_GuildCheckButton functions.
function JocysCom_GuildCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_GuildCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "Guild");
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_GuildCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cff40fb40 Play GUILD messages|r");
end

-- JocysCom_RaidCheckButton functions.
function JocysCom_RaidCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_RaidCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "Raid");
	else
		JocysCom_RaidLeaderCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_RaidCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffff7d00 Play all RAID messages|r");
end

-- JocysCom_RaidLeaderCheckButton functions.
function JocysCom_RaidLeaderCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_RaidLeaderCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "RaidLeader");
	else
		JocysCom_RaidCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_RaidLeaderCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffff4709 Play RAID leader messages only|r");
end

-- JocysCom_BattlegroundCheckButton functions.
function JocysCom_BattlegroundCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_BattlegroundCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "BG");
	else
		JocysCom_BattlegroundLeaderCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_BattlegroundCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffff7d00 Play all BATTLEGROUND messages|r");
end

-- JocysCom_BattlegroundLeaderCheckButton functions.
function JocysCom_BattlegroundLeaderCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_BattlegroundLeaderCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "BGLeader");
	else
		JocysCom_BattlegroundCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_BattlegroundLeaderCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffff4709 Play BATTLEGROUND leader messages only|r");
end


-- JocysCom_InstanceCheckButton functions.
function JocysCom_InstanceCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_InstanceCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "Instance");
	else
		JocysCom_InstanceLeaderCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_InstanceCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffff7d00 Play all INSTANCE messages|r");
end

-- JocysCom_InstanceLeaderCheckButton functions.
function JocysCom_InstanceLeaderCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if JocysCom_InstanceLeaderCheckButton:GetChecked() == false then
		sendChatMessageStop(1, "InstanceLeader");
	else
		JocysCom_InstanceCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_InstanceLeaderCheckButton_OnEnter(self)
	JocysCom_CheckButtonTooltipFontString:Show();
	JocysCom_CheckButtonTooltipFontString:SetText("|cffff4709 Play INSTANCE leader messages only|r");
end


-- Hide whisper messages.
function JocysCom_FilterCheckButton_OnClick(self) PlaySound("igMainMenuOptionCheckBoxOn") end

-- JocysCom_ScrollFrame's mouse click functions.
function JocysCom_ScrollFrame_OnMouseDown(self) self:StartMoving() end
function JocysCom_ScrollFrame_OnMouseUp(self) self:StopMovingOrSizing() end
-- JocysCom_ScrollFrame's mouse wheel functions.
function JocysCom_ScrollFrame_OnMouseWheel(self, delta)
	if delta == 1 then
			JocysCom_PlayButtonButton_OnClick();
		else
			sendChatMessageStop(1, "Quest"); -- Send stop message.
		end
end

-- Start resize JocysCom_ScrollFrame.
function JocysCom_ScrollResizeButton_OnMouseDown(self) 
	self:GetParent():StartSizing("BOTTOMRIGHT");
	self:GetParent():SetUserPlaced(true);
end
-- Stop resizing JocysCom_ScrollFrame.
function JocysCom_ScrollResizeButton_OnMouseUp(self) self:GetParent():StopMovingOrSizing() end

-- Show or Hide JocysCom frames.
local function JocysCom_AttachAndShowFrames(frame)
	-- Set frame values.
	local top = frame:GetTop();
	local width = frame:GetWidth();
	-- Move Scroll frame.
	JocysCom_MiniFrame:ClearAllPoints();
	JocysCom_ScrollFrame:ClearAllPoints();
	JocysCom_OptionsFrame:ClearAllPoints();	
	-- Different scrollFrame position and size... if frame is WorldMapFrame.	
	if frame == WorldMapFrame then
	JocysCom_ScrollFrame:SetParent(QuestMapDetailsScrollFrame);
	JocysCom_ScrollFrame:SetPoint("TOPLEFT", 0, -4);
	local width2 = QuestMapDetailsScrollFrame:GetWidth();
	JocysCom_ScrollFrame:SetWidth(width2 - 4);
	else
	JocysCom_ScrollFrame:SetParent(frame);
	JocysCom_ScrollFrame:SetWidth(width - 50);
	JocysCom_ScrollFrame:SetPoint("TOPLEFT", 12, -67);
	end
	-- Set parent frame.		
	JocysCom_MiniFrame:SetParent(frame);
	JocysCom_OptionsFrame:SetParent(frame);
	-- Frames position, depending on event.
	JocysCom_MiniFrame:SetPoint("TOPRIGHT", frame, "BOTTOMRIGHT", 0, 0);
	JocysCom_OptionsFrame:SetPoint("TOPLEFT", frame, "TOPRIGHT", 2, 0);
	-- Set FrameLevel frame.
	JocysCom_ScrollFrame:SetFrameLevel(100);
	JocysCom_OptionsFrame:SetFrameLevel(11);
	JocysCom_StopFrame:SetFrameLevel(10);
		-- Show Frames.
	JocysCom_ScrollFrame:Show();
	JocysCom_MiniFrame:Show();
	JocysCom_MiniFrame:SetBackdrop({bgFile="Interface/AddOns/JocysCom-TextToSpeech-WoW/Images/JocysCom-MiniFrame-Background"});
end

function JocysCom_MiniFrame_OnShow()
	if DebugEnabled then print("MiniFrame Show") end
end

-- Close all JocysCom frames.
function JocysCom_MiniFrame_OnHide()
	if DebugEnabled then print("MiniFrame Hide") end
	JocysCom_OptionsFrame:Hide();
	sendChatMessageStop(0, "Quest");
	JocysCom_QuestEditBox:SetText("");
end

function JocysCom_MiniFrame_Hide()
	JocysCom_MiniFrame:Hide();
end

-- [ Close ] Options button.
local function JocysCom_OptionsFrame_CloseButton_OnClick(self)
	JocysCom_OptionsFrame:Hide();
end

-- [ TTS... ] button.
function JocysCom_OptionsButton_OnClick(self)
	if JocysCom_OptionsFrame:IsShown() then
		JocysCom_OptionsFrame:Hide();
	else
		JocysCom_OptionsFrame:Show();
	end
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
	end
	JocysCom_SpeakMessage(questMessage, "SROLL_UP_OR_PLAY_BUTTON", "", "Quest");
end

-- [ Stop ] button
function JocysCom_StopButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOff");
	sendChatMessageStop(1);
end

function JocysCom_StopButtonQuest_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOff");
	sendChatMessageStop(1, "Quest");
end

-- Close frames on "Escape" key press.
local function EditBox_OnEscapePressed()
	JocysCom_OptionsButton_OnClick();
	CloseQuest();
	CloseGossip();
	CloseItemText();
	sendChatMessageStop(0, "Quest");
end

-- Save values on options close or logout.
function JocysCom_SaveAllSettings()
	-- Save check buttons.
	JocysCom_AutoCheckButtonValue = JocysCom_AutoCheckButton:GetChecked();
	JocysCom_DndCheckButtonValue = JocysCom_DndCheckButton:GetChecked();
	JocysCom_FilterCheckButtonValue = JocysCom_FilterCheckButton:GetChecked();
	JocysCom_LockCheckButtonValue = JocysCom_LockCheckButton:GetChecked();
	JocysCom_StopOnCloseCheckButtonValue = JocysCom_StopOnCloseCheckButton:GetChecked();
	JocysCom_MonsterCheckButtonValue = JocysCom_MonsterCheckButton:GetChecked();
	JocysCom_WhisperCheckButtonValue = JocysCom_WhisperCheckButton:GetChecked();
	JocysCom_SayCheckButtonValue = JocysCom_SayCheckButton:GetChecked();
	JocysCom_PartyCheckButtonValue = JocysCom_PartyCheckButton:GetChecked();
	JocysCom_GuildCheckButtonValue = JocysCom_GuildCheckButton:GetChecked();
	JocysCom_RaidCheckButtonValue = JocysCom_RaidCheckButton:GetChecked();
	JocysCom_RaidLeaderCheckButtonValue = JocysCom_RaidLeaderCheckButton:GetChecked();
	JocysCom_BattlegroundLeaderCheckButtonValue = JocysCom_BattlegroundLeaderCheckButton:GetChecked();
	JocysCom_BattlegroundCheckButtonValue = JocysCom_BattlegroundCheckButton:GetChecked();
	JocysCom_InstanceLeaderCheckButtonValue = JocysCom_InstanceLeaderCheckButton:GetChecked();
	JocysCom_InstanceCheckButtonValue = JocysCom_InstanceCheckButton:GetChecked();
	-- Save edit boxes.
	JocysCom_ReplaceNameEditBoxValue = JocysCom_ReplaceNameEditBox:GetText();

	JocysCom_StopFrame:SetMovable(true);
	JocysCom_StopFrame:EnableMouse(true);
	JocysCom_StopFrame:SetUserPlaced(true);
end

function JocysCom_LoadAllSettings()
	JocysCom_AttachAndShowFrames(GossipFrame);
	-- Load styles.
	JocysCom_QuestEditBox:SetTextInsets(5, 5, 5, 5);
	-- Black Background for Quest frame.
	JocysCom_MessageFrame.Bg:SetTexture(0, 0, 0, 1.0);
	-- Load descriptions.
	JocysCom_Text_EN();	
	-- Set SetJustifyH
	JocysCom_ReplaceNameEditBox:SetJustifyH("CENTER");	
	-- If -------------------- value --- was set then set this --- value --------- else --------- default value.
	if JocysCom_LockCheckButtonValue == true then JocysCom_LockCheckButton:SetChecked(true) else JocysCom_LockCheckButton:SetChecked(false) end
	if JocysCom_AutoCheckButtonValue == false then JocysCom_AutoCheckButton:SetChecked(false) else JocysCom_AutoCheckButton:SetChecked(true) end
	if JocysCom_DndCheckButtonValue == false then JocysCom_DndCheckButton:SetChecked(false) else JocysCom_DndCheckButton:SetChecked(true) end
	if JocysCom_FilterCheckButtonValue == false then JocysCom_FilterCheckButton:SetChecked(false) else JocysCom_FilterCheckButton:SetChecked(true) end
	if JocysCom_StopOnCloseCheckButtonValue == true then JocysCom_StopOnCloseCheckButton:SetChecked(true) else JocysCom_StopOnCloseCheckButton:SetChecked(false) end
	if JocysCom_MonsterCheckButtonValue == false then JocysCom_MonsterCheckButton:SetChecked(false) else JocysCom_MonsterCheckButton:SetChecked(true) end
	if JocysCom_WhisperCheckButtonValue == false then JocysCom_WhisperCheckButton:SetChecked(false) else JocysCom_WhisperCheckButton:SetChecked(true) end
	if JocysCom_SayCheckButtonValue == true then JocysCom_SayCheckButton:SetChecked(true) else JocysCom_SayCheckButton:SetChecked(false) end
	if JocysCom_PartyCheckButtonValue == false then JocysCom_PartyCheckButton:SetChecked(false) else JocysCom_PartyCheckButton:SetChecked(true) end
	if JocysCom_GuildCheckButtonValue == false then JocysCom_GuildCheckButton:SetChecked(false) else JocysCom_GuildCheckButton:SetChecked(true) end
	if JocysCom_RaidCheckButtonValue == false then JocysCom_RaidCheckButton:SetChecked(false) else JocysCom_RaidCheckButton:SetChecked(true) end
	if JocysCom_RaidLeaderCheckButtonValue == true then JocysCom_RaidLeaderCheckButton:SetChecked(true) else JocysCom_RaidLeaderCheckButton:SetChecked(false) end
	if JocysCom_BattlegroundCheckButtonValue == false then JocysCom_BattlegroundCheckButton:SetChecked(false) else JocysCom_BattlegroundCheckButton:SetChecked(true) end
	if JocysCom_BattlegroundLeaderCheckButtonValue == true then JocysCom_BattlegroundLeaderCheckButton:SetChecked(true) else JocysCom_BattlegroundLeaderCheckButton:SetChecked(false) end
	if JocysCom_InstanceCheckButtonValue == false then JocysCom_InstanceCheckButton:SetChecked(false) else JocysCom_InstanceCheckButton:SetChecked(true) end
	if JocysCom_InstanceLeaderCheckButtonValue == true then JocysCom_InstanceLeaderCheckButton:SetChecked(true) else JocysCom_InstanceLeaderCheckButton:SetChecked(false) end

	JocysCom_StopOnCloseCheckButton.text:SetPoint("LEFT", 19, 1);
	JocysCom_MonsterCheckButton.text:SetPoint("TOPLEFT", 4, 10);
	JocysCom_WhisperCheckButton.text:SetPoint("TOPLEFT", 4, 10);
	JocysCom_SayCheckButton.text:SetPoint("TOPLEFT", 6, 10);
	JocysCom_PartyCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	JocysCom_GuildCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	JocysCom_BattlegroundCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	JocysCom_BattlegroundLeaderCheckButton.text:SetPoint("TOPLEFT", 2, 10);
	JocysCom_RaidCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	JocysCom_RaidLeaderCheckButton.text:SetPoint("TOPLEFT", 2, 10);
	JocysCom_InstanceCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	JocysCom_InstanceLeaderCheckButton.text:SetPoint("TOPLEFT", 2, 10);
	
	-- Load edit boxes.
	if JocysCom_ReplaceNameEditBoxValue == "" or JocysCom_ReplaceNameEditBoxValue == nil then JocysCom_ReplaceNameEditBoxValue = unitName end
	JocysCom_ReplaceNameEditBox:SetText(JocysCom_ReplaceNameEditBoxValue);	
	-- Attach OnEscape scripts.
	JocysCom_QuestEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	JocysCom_ReplaceNameEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	-- Open JocysCom frames script.
	QuestMapDetailsScrollFrame:SetScript("OnHide", JocysCom_MiniFrame_Hide);
	-- Open JocysCom frames functions.
	GossipFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	QuestFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	ItemTextFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	QuestLogPopupDetailFrame:SetScript("OnShow", JocysCom_AttachAndShowFrames);
	QuestMapDetailsScrollFrame:SetScript("OnShow", function()
	JocysCom_AttachAndShowFrames(WorldMapFrame);
	end);
	JocysCom_OptionsFrame:SetScript("OnShow", JocysCom_UnLockFrames);

	-- JocysCom_ScrollFrame.
	JocysCom_ScrollFrame:SetScript("OnMouseWheel", JocysCom_ScrollFrame_OnMouseWheel);

	-- Set JocysCom_QuestEditBox child of JocysCom_QuestScrollFrame.
	JocysCom_QuestScrollFrame:SetScrollChild(JocysCom_QuestEditBox);

	-- JocysCom_LockCheckButton Enable / Disable.
	if JocysCom_LockCheckButton:GetChecked() == true then
		JocysCom_StopFrame:SetMovable(false);
		JocysCom_StopFrame:EnableMouse(false);
	else
		JocysCom_StopFrame:SetMovable(true);
		JocysCom_StopFrame:EnableMouse(true);
	end
	-- Set custom position of StopFrame.
	JocysCom_StopFrame:RegisterForDrag("LeftButton");
	JocysCom_StopFrame:SetScript("OnDragStart", JocysCom_StopFrame.StartMoving);
	JocysCom_StopFrame:SetScript("OnDragStop", JocysCom_StopFrame.StopMovingOrSizing);
	JocysCom_StopFrame:SetUserPlaced(true);
	JocysCom_StopFrame:Show();
end

JocysCom_OptionsFrame_OnLoad();

