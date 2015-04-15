-- /fstack - show frame names.
-- /run local m=MinimapCluster if m:IsShown()then m:Hide()else m:Show()end - Show / Hide MinimapCluster.

-- Debug.
local DebugEnabled = false; -- true = debug mode enabled, false = debug mode disabled

-- Set variables.
local unitName = UnitName("player");
local realmName = GetRealmName();
local messageStop = "<voice command=\"stop\" />";
local messageDoNotDisturb = "Please wait... NPC dialog window is open and text-to-speech is enabled.";

-- Set text.
local function JocysCom_Text_EN()
	-- Titles.
	JocysCom_OptionsFrame.TitleText:SetText("Jocys.com Text to Speech World of Warcraft Addon 2.2.23 ( 2015-04-15 )");
	-- Frames.
	JocysCom_ScrollFrame.text:SetText("When mouse pointer is over this frame...\n\nSCROLL UP will START SPEECH\n\nSCROLL DOWN will STOP SPEECH");
	-- Check buttons.
	FilterCheckButton.text:SetText("|cff808080 Hide addon's whisper messages in chat window.|r");
	DndCheckButton.text:SetText("|cff808080 Show|r |cffffffff<Busy>|r|cff808080 over my character for other players, when NPC dialogue window is open and speech is on.|r");
	AutoCheckButton.text:SetText("|cff808080 Auto start speech, when dialog window is open (except for quest log windows).|r");
	LockCheckButton.text:SetText("|cff808080 Lock mini frame with|r |cffffffff[Stop]|r |cff808080button and|r |cffffffffNPC|r|cff808080,|r |cffffffffWhisper|r|cff808080,|r |cffffffffSay|r|cff808080,|r |cffffffffParty|r|cff808080,|r |cffffffff...|r |cff808080check-boxes.|r");
	StopOnCloseCheckButton.text:SetText(" |cff808080Play after closing|r");
	MonsterCheckButton.text:SetText("|cfffffb9f N|r");
	WhisperCheckButton.text:SetText("|cffffb2eb W|r");
	SayCheckButton.text:SetText("|cffffffff S|r");
	PartyCheckButton.text:SetText("|cffaaa7ff P|r");
	CheckButtonTooltipFontString:SetText("");
	GuildCheckButton.text:SetText("|cff40fb40 G|r");	
	RaidCheckButton.text:SetText("|cffff7d00 R|r");
	RaidLeaderCheckButton.text:SetText("|cffff4709 RL|r");
	BattlegroundCheckButton.text:SetText("|cffff7d00 B|r");
	BattlegroundLeaderCheckButton.text:SetText("|cffff4709 BL|r");
	InstanceCheckButton.text:SetText("|cffff7d00 I|r");
	InstanceLeaderCheckButton.text:SetText("|cffff4709 IL|r");
	---- Font Strings
	DescriptionFrameFontString:SetText("Text-to-speech voices, pitch, rate, effects, etc. ... you will find all options in |cff77ccffJocys.Com Text to Speech Monitor|r.\n\nHow it works: When you open NPC dialogue window, |cff77ccffJocys.Com Text to Speech WoW Addon|r creates and sends special whisper message to yourself (message includes dialogue text, character name and effect name). Then, |cff77ccffJocys.Com Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.");
	ReplaceNameFontString:SetText("Here you can change your name for text to speech from |cff00ff00" .. unitName .. "|r to something else.");
	MessageFontString:SetText("Whisper message for |cff77ccffJocys.Com Text to Speech Monitor|r ... it must be runninng in background:");	
end

local lastArg = nil;
local lastQuest = nil;
local lastMessage = nil;
local debugLine = 0;
local debugLineOld = 0;
local questId = nil;
local questIdOld = nil;
local MessageIsFromChat = false;
local NPCSex = nil;
local NPCGender = nil;
local stopWhenClosing = 1;
local dashIndex = nil;

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
	JocysCom_ScrollFrame.text:Show();
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
	JocysCom_ScrollFrame.text:Hide();
	JocysCom_ScrollFrame.resizeButton:Hide();
	JocysCom_ScrollFrame:SetFrameLevel(100);
	JocysCom_OptionsFrame:SetFrameLevel(11);
	JocysCom_StopFrame:SetFrameLevel(10);
end

-- FUNCTION MESSAGE STOP
local function sendChatMessageStop(CloseOrButton)
		-- Disable DND <Busy> if checked.
		if DndCheckButton:GetChecked() == true then
			if UnitIsDND("player") == true then
				SendChatMessage("", "DND");
			end
		end
		if stopWhenClosing == 1 then
			if StopOnCloseCheckButton:GetChecked() ~= true or CloseOrButton == 1 then
				SendChatMessage(messageStop, "WHISPER", "Common", unitName);
				stopWhenClosing = 0;
				QuestEditBox:SetText("|cff808080" .. messageStop .. "|r");
			end
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
	if msg:find("<voice") and FilterCheckButton:GetChecked() == true then
		return true;
	end
end
-- Enable/Disable message filter
function JocysCom_CHAT_MSG_DND(self, event, msg)
	if msg:find("<" .. unitName .. ">: ") and FilterCheckButton:GetChecked() == true then
		return true;
	end
end
-- Enable/Disable message filter
function JocysCom_CHAT_MSG_SYSTEM(self, event, msg)
	if msg:find("You are no") and FilterCheckButton:GetChecked() == true then
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
function JocysCom_OptionsFrame_OnLoad(self)
	self:SetPoint("TOPLEFT", 356, -116);
	self:SetMovable(true);
	self:EnableMouse(true);
	self:RegisterForDrag("LeftButton");
	self:SetScript("OnDragStart", self.StartMoving);
	self:SetScript("OnDragStop", self.StopMovingOrSizing);
	-- Setup title.
	self.TitleText:SetPoint("TOP", 0, -6);
	-- Hide messages.
	JocysCom_MessageEventFilters();
	-- Attach events.
	self:SetScript("OnEvent", JocysCom_OptionsFrame_OnEvent);
	-- Addon loaded event.
	self:RegisterEvent("ADDON_LOADED");
	-- Dialogue events (open).
	self:RegisterEvent("QUEST_GREETING");
	self:RegisterEvent("GOSSIP_SHOW");
	self:RegisterEvent("QUEST_DETAIL");
	self:RegisterEvent("QUEST_PROGRESS");
	self:RegisterEvent("QUEST_COMPLETE");
	-- books, scrolls, saved copies of mail messages, plaques, gravestones events.
	self:RegisterEvent("ITEM_TEXT_READY"); 
	-- Close frames.
	self:RegisterEvent("GOSSIP_CLOSED"); --"QUEST_FINISHED", "QUEST_ACCEPTED"
	-- Chat MONSTER events.
	self:RegisterEvent("CHAT_MSG_MONSTER_EMOTE");
	self:RegisterEvent("CHAT_MSG_MONSTER_WHISPER");
	self:RegisterEvent("CHAT_MSG_MONSTER_SAY");
	self:RegisterEvent("CHAT_MSG_MONSTER_PARTY");
	self:RegisterEvent("CHAT_MSG_MONSTER_YELL");
	-- Chat WHISPER events.
	self:RegisterEvent("CHAT_MSG_WHISPER");
	self:RegisterEvent("CHAT_MSG_WHISPER_INFORM");
	self:RegisterEvent("CHAT_MSG_BN_WHISPER");
	self:RegisterEvent("CHAT_MSG_BN_WHISPER_INFORM");
	-- Chat SAY events.
	self:RegisterEvent("CHAT_MSG_SAY");
	-- Chat PARTY events.
	self:RegisterEvent("CHAT_MSG_PARTY");
	self:RegisterEvent("CHAT_MSG_PARTY_LEADER");
	self:RegisterEvent("CHAT_MSG_GUILD");
	self:RegisterEvent("CHAT_MSG_GUILD_ACHIEVEMENT");
	-- Chat RAID events.
	self:RegisterEvent("CHAT_MSG_RAID");
	self:RegisterEvent("CHAT_MSG_RAID_LEADER");
	self:RegisterEvent("CHAT_MSG_RAID_WARNING");
	-- Chat BATTLEGROUND events.
	self:RegisterEvent("CHAT_MSG_BATTLEGROUND");
	self:RegisterEvent("CHAT_MSG_BATTLEGROUND_LEADER");
	-- Chat INSTANCE events.
	self:RegisterEvent("CHAT_MSG_INSTANCE_CHAT");
	self:RegisterEvent("CHAT_MSG_INSTANCE_CHAT_LEADER");
	-- Logout event.
	self:RegisterEvent("PLAYER_LOGOUT");
end

function JocysCom_OptionsFrame_OnEvent(self, event, arg1, arg2)
	-- Debug.
	if DebugEnabled then print(event) end
	-- Events.
	if event == "ADDON_LOADED" then
		JocysCom_LoadAllSettings();
	elseif event == "PLAYER_LOGOUT" then
		JocysCom_SaveAllSettings();
	elseif JocysCom_MiniFrame:IsShown() and (event == "QUEST_GREETING" or event == "GOSSIP_SHOW" or event == "QUEST_DETAIL" or event == "QUEST_PROGRESS" or event == "QUEST_COMPLETE" or event == "ITEM_TEXT_READY") then
		lastQuest = nil;	
		if event == "QUEST_GREETING" then 
			lastQuest = GetGreetingText();
		elseif GossipFrame:IsShown() and event == "GOSSIP_SHOW" then
			lastQuest = GetGossipText();
		elseif event == "QUEST_DETAIL" then
			lastQuest = GetQuestText() .. " Your objective is to " .. GetObjectiveText();
		elseif event == "QUEST_PROGRESS" then
			lastQuest = GetProgressText();
		elseif event == "QUEST_COMPLETE" then
			lastQuest = GetRewardText();
		elseif event == "ITEM_TEXT_READY" then
			lastQuest = ItemTextGetText();
		end	
		-- Remove HTML <> tags and text between them.
		if lastQuest ~= nil then
		 if string.find(lastQuest, "HTML") ~= nil then
			 lastQuest = string.gsub(lastQuest, "%b<>", "");
		 end
		 lastQuest = string.gsub(lastQuest, "\"", "");
		 lastQuest = string.gsub(lastQuest, "&", " and ");
		end
		JocysCom_SpeakMessage("Auto", lastQuest, event);
	elseif event == "GOSSIP_CLOSED" then
		JocysCom_MiniFrame_Hide();
	-- Chat events.
	elseif MonsterCheckButton:GetChecked() == true and (event == "CHAT_MSG_MONSTER_EMOTE" or event == "CHAT_MSG_MONSTER_WHISPER" or event == "CHAT_MSG_MONSTER_SAY" or event == "CHAT_MSG_MONSTER_PARTY" or event == "CHAT_MSG_MONSTER_YELL") then
		if arg1:find("<voice") or (lastArg == arg2 .. arg1) then
		else
		lastArg = arg2 .. arg1;
			local SpeechEmotion = " says. ";
			if (event == "CHAT_MSG_MONSTER_WHISPER") then SpeechEmotion = " whispers. " end
			if (event == "CHAT_MSG_MONSTER_YELL") then SpeechEmotion = " yells. " end
			if (event == "CHAT_MSG_MONSTER_EMOTE") then
				lastMessage = arg2 .. " " .. arg1;
			else				
				lastMessage = arg2 .. SpeechEmotion .. arg1;
			end
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif WhisperCheckButton:GetChecked() == true and (event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_WHISPER") then
		if arg1:find("<voice") or (event == "CHAT_MSG_WHISPER" and arg2:find(unitName)) then
		else
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
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			lastMessage = arg2 .. " whispers. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif WhisperCheckButton:GetChecked() == true and (event == "CHAT_MSG_WHISPER_INFORM" or event == "CHAT_MSG_BN_WHISPER_INFORM") then
		if arg1:find("<voice") then
		else
			arg2 = unitName;
			lastMessage = arg2 .. " whispers. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif SayCheckButton:GetChecked() == true and (event == "CHAT_MSG_SAY") then
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			lastMessage = arg2 .. " says. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif PartyCheckButton:GetChecked() == true and (event == "CHAT_MSG_PARTY" or event == "CHAT_MSG_PARTY_LEADER") then
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			lastMessage = arg2 .. " says. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif GuildCheckButton:GetChecked() == true and (event == "CHAT_MSG_GUILD") then --  or event == "CHAT_MSG_GUILD_ACHIEVEMENT"
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			lastMessage = arg2 .. " says. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif RaidCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID" or event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING") then
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			if event == "CHAT_MSG_RAID" then
				lastMessage = arg2 .. " says. " .. arg1;
			else
				lastMessage = "Raid leader " .. arg2 .. " says. " .. arg1;
			end
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif RaidLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING") then
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			lastMessage = "Raid leader " .. arg2 .. " says. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif BattlegroundCheckButton:GetChecked() == true and (event == "CHAT_MSG_BATTLEGROUND" or event == "CHAT_MSG_BATTLEGROUND_LEADER") then 
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			if event == "CHAT_MSG_BATTLEGROUND_LEADER" then
				lastMessage = "Battleground leader " .. arg2 .. " says. " .. arg1;
			else
				lastMessage = arg2 .. " says. " .. arg1;
			end
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif BattlegroundLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_BATTLEGROUND_LEADER") then
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			lastMessage = "Battleground leader " .. arg2 .. " says. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif InstanceCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT" or event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then 
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			if event == "CHAT_MSG_INSTANCE_CHAT_LEADER" then
				lastMessage = "Instance leader " .. arg2 .. " says. " .. arg1;
			else
				lastMessage = arg2 .. " says. " .. arg1;
			end
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	elseif InstanceLeaderCheckButton:GetChecked() == true and (event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then
		if arg1:find("<voice") then
		else
			dashIndex = string.find(arg2, "-");
			if dashIndex ~= nil then
				dashIndex = dashIndex - 1;
				arg2 = string.sub(arg2, 1, dashIndex);
			end
			lastMessage = "Instance leader " .. arg2 .. " says. " .. arg1;
			JocysCom_SpeakMessage("Auto", lastMessage, event, arg2);
		end
	end
end

function JocysCom_SpeakMessage(reason, questText, event, name)
	if questText == nil then return end
	if (event == "CHAT_MSG_GUILD_ACHIEVEMENT" or event == "CHAT_MSG_MONSTER_EMOTE" or event == "CHAT_MSG_MONSTER_WHISPER" or event == "CHAT_MSG_MONSTER_SAY" or event == "CHAT_MSG_MONSTER_PARTY" or event == "CHAT_MSG_MONSTER_YELL" or event == "CHAT_MSG_WHISPER" or event == "CHAT_MSG_WHISPER_INFORM" or event == "CHAT_MSG_BN_WHISPER" or event == "CHAT_MSG_BN_WHISPER_INFORM" or event == "CHAT_MSG_SAY" or event == "CHAT_MSG_PARTY" or event == "CHAT_MSG_PARTY_LEADER" or event == "CHAT_MSG_GUILD" or event == "CHAT_MSG_RAID" or event == "CHAT_MSG_RAID_LEADER" or event == "CHAT_MSG_RAID_WARNING" or event == "CHAT_MSG_BATTLEGROUND" or event == "CHAT_MSG_BATTLEGROUND_LEADER" or event == "CHAT_MSG_INSTANCE_CHAT" or event == "CHAT_MSG_INSTANCE_CHAT_LEADER") then
		MessageIsFromChat = true;
		dashIndex = string.find(questText, "|");
		if dashIndex ~= nil then
			questText = questText:gsub("%|cff(.-)%|h", "");
			questText = questText:gsub("%|h%|r", "");
			questText = questText:gsub("%[", " \"");
			questText = questText:gsub("%]", "\" ");
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
	-- Replace player name.
	local newUnitName = ReplaceNameEditBox:GetText();
	if string.len(newUnitName) > 1 and newUnitName ~= unitName then
		questText = string.gsub(questText, unitName, newUnitName);
	end
	-- Replace in questText. %c %p
	questText = string.gsub(questText, "%c(%u)", ".%1");
	questText = string.gsub(questText, "%c", " ");
	questText = string.gsub(questText, "[ ]+", " ");
	questText = string.gsub(questText, " %.", ".");
	questText = string.gsub(questText, "[(%p)]+", "%1");
	questText = string.gsub(questText, "[%!]+", "!");
	questText = string.gsub(questText, "[%?]+", "?");
	questText = string.gsub(questText, "%!%.", "!");
	questText = string.gsub(questText, "%?%.", "?");
	questText = string.gsub(questText, "%?%-", "?");
	questText = string.gsub(questText, "%?%!%?", "?!");
	questText = string.gsub(questText, "%!%?%!", "?!");
	questText = string.gsub(questText, "%!", "! ");
	questText = string.gsub(questText, "%?", "? ");
	questText = string.gsub(questText, " %!", "!");
	questText = string.gsub(questText, "^%.", "");
	questText = string.gsub(questText, ">%.", ".>");
	questText = string.gsub(questText, "[%.]+", ". ");
	questText = string.gsub(questText, "<", " [comment] ");
	questText = string.gsub(questText, ">", " [/comment] ");
	questText = string.gsub(questText, "[ ]+", " ");
	questText = string.gsub(questText, "%%s", "");
	questText = string.gsub(questText, "lvl", "level");
	-- Format and send whisper message.
	if (reason == "Scroll") or (reason == "Auto" and AutoCheckButton:GetChecked() == true) then
		local size = 130;
		local startIndex = 1;
		local endIndex = 1;
		local part = "";
		local chatMessageSP = "<voice" .. NPCName .. NPCGender .. NPCType .. " command=\"play\"><part>";
		if NPCGender == "" then
		chatMessageSP = "<voice" .. NPCName .. NPCType .. " command=\"play\"><part>";
		end
		local chatMessageSA = "<voice command=\"add\"><part>";
		local chatMessageE = "</part></voice>";
		local chatMessage;
		while true do
			local command = "";
			local index = string.find(questText, " ", endIndex);
			-- print(index);
			-- if nothing found then...
			if index == nil then
				part = string.sub(questText, startIndex);
				chatMessage = chatMessageSP .. part .. chatMessageE;
				stopWhenClosing = 1;
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
				-- print("[" .. startIndex .. "] '" .. part .. "'");
				break;
			elseif (index - startIndex) > size then
				-- if space is out of size then...
				part = string.sub(questText, startIndex, endIndex - 1);
				chatMessage = chatMessageSA .. part .. chatMessageE;
				stopWhenClosing = 1;
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
				-- print("[" .. startIndex .. "-" .. (endIndex - 1) .. "] '" .. part .. "'");
				startIndex = endIndex;
			end
			-- look for next space.
			endIndex = index + 1;
		end
		-- FILL EDITBOXES  .. "<" .. event .. " />" ..
		local questEditBox = string.gsub(questText, "%[comment]", "|cff808080[comment]|r|cfff7e593");
		questEditBox = string.gsub(questEditBox, "%[/comment]", "|r|cff808080[/comment]|r");
		questEditBox = "|cff808080" .. chatMessageSP .. "|r\n" .. questEditBox .. "\n|cff808080" .. chatMessageE .. "|r";
		QuestEditBox:SetText(questEditBox);
		-- Enable DND <Busy> if checked.
		if DndCheckButton:GetChecked() == true and MessageIsFromChat == false then
		--if UnitIsDND("player") == false then
		SendChatMessage("<" .. unitName .. ">: " .. messageDoNotDisturb, "DND");
	    --end		
		end
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
	if DndCheckButton:GetChecked() == false then
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
	if LockCheckButton:GetChecked() == true then
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

-- Clear and Hide MonsterCheckButtonFontString.
function JocysCom_CheckButtonTooltipFontString_OnLeave(self)
	CheckButtonTooltipFontString:Hide();
	CheckButtonTooltipFontString:SetText("");
	JocysCom_SaveAllSettings();
end

-- MonsterCheckButton functions.
function JocysCom_MonsterCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if MonsterCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_MonsterCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cfffffb9f Play NPC messages|r");
end

-- WhisperCheckButton functions.
function JocysCom_WhisperCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if WhisperCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_WhisperCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffffb2eb Play WHISPER messages|r");
end

-- SayCheckButton functions.
function JocysCom_SayCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if SayCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_SayCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffffffff Play SAY messages|r");
end

-- PartyCheckButton functions.
function JocysCom_PartyCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if PartyCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_PartyCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffaaa7ff Play PARTY messages|r");
end

-- GuildCheckButton functions.
function JocysCom_GuildCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if GuildCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_GuildCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cff40fb40 Play GUILD messages|r");
end

-- RaidCheckButton functions.
function JocysCom_RaidCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if RaidCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	else
		RaidLeaderCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_RaidCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffff7d00 Play all RAID messages|r");
end

-- RaidLeaderCheckButton functions.
function JocysCom_RaidLeaderCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if RaidLeaderCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	else
		RaidCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_RaidLeaderCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffff4709 Play RAID leader messages only|r");
end

-- BattlegroundCheckButton functions.
function JocysCom_BattlegroundCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if BattlegroundCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	else
		BattlegroundLeaderCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_BattlegroundCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffff7d00 Play all BATTLEGROUND messages|r");
end

-- BattlegroundLeaderCheckButton functions.
function JocysCom_BattlegroundLeaderCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if BattlegroundLeaderCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	else
		BattlegroundCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_BattlegroundLeaderCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffff4709 Play BATTLEGROUND leader messages only|r");
end


-- InstanceCheckButton functions.
function JocysCom_InstanceCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if InstanceCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	else
		InstanceLeaderCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_InstanceCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffff7d00 Play all INSTANCE messages|r");
end

-- InstanceLeaderCheckButton functions.
function JocysCom_InstanceLeaderCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if InstanceLeaderCheckButton:GetChecked() == false then
		sendChatMessageStop(1);
	else
		InstanceCheckButton:SetChecked(false);
	end
	JocysCom_SaveAllSettings();
end
function JocysCom_InstanceLeaderCheckButton_OnEnter(self)
	CheckButtonTooltipFontString:Show();
	CheckButtonTooltipFontString:SetText("|cffff4709 Play INSTANCE leader messages only|r");
end


-- Hide whisper messages.
function JocysCom_FilterCheckButton_OnClick(self) PlaySound("igMainMenuOptionCheckBoxOn") end

-- JocysCom_ScrollFrame OnLoad.
function JocysCom_ScrollFrame_OnLoad(self) self:SetScript("OnMouseWheel", JocysCom_ScrollFrame_OnMouseWheel) end
-- JocysCom_ScrollFrame's mouse wheel functions.
function JocysCom_ScrollFrame_OnMouseDown(self) self:StartMoving() end
function JocysCom_ScrollFrame_OnMouseUp(self) self:StopMovingOrSizing() end
function JocysCom_ScrollFrame_OnMouseWheel(self, delta)
	if delta == 1 then
		if QuestLogPopupDetailFrame:IsShown() or WorldMapFrame:IsShown() then
		local questDescription, questObjectives = GetQuestLogQuestText();
		lastQuest = questObjectives .. " Description. " .. questDescription;
		end
		JocysCom_SpeakMessage("Scroll", lastQuest); -- Send quest message.
		else
			sendChatMessageStop(1); -- Send stop message.
		end
end

-- Set QuestEditBox child of JocysCom_QuestScrollFrame.
function JocysCom_QuestScrollFrame_OnLoad(self) self:SetScrollChild(QuestEditBox) end

-- Start resize JocysCom_ScrollFrame.
function JocysCom_ScrollResizeButton_OnMouseDown(self) 
	self:GetParent():StartSizing("BOTTOMRIGHT");
	self:GetParent():SetUserPlaced(true);
end
-- Stop resizing JocysCom_ScrollFrame.
function JocysCom_ScrollResizeButton_OnMouseUp(self) self:GetParent():StopMovingOrSizing() end

-- Show or Hide JocysCom frames.
local function JocysCom_ShowFrames(frame)
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
end

function JocysCom_MiniFrame_OnShow()
	if DebugEnabled then print("MiniFrame Show") end
end

-- Close all JocysCom frames.
function JocysCom_MiniFrame_OnHide()
	if DebugEnabled then print("MiniFrame Hide") end
	JocysCom_OptionsFrame:Hide();
	sendChatMessageStop(0);
	QuestEditBox:SetText("");
end

function JocysCom_MiniFrame_Hide()
	JocysCom_MiniFrame:Hide();
end

-- Open JocysCom frames function.
local function JocysCom_GossipFrame_OnShow() JocysCom_ShowFrames(GossipFrame) end
local function JocysCom_QuestFrame_OnShow() JocysCom_ShowFrames(QuestFrame) end
local function JocysCom_ItemTextFrame_OnShow() JocysCom_ShowFrames(ItemTextFrame) end
local function JocysCom_QuestLogPopupDetailFrame_OnShow() JocysCom_ShowFrames(QuestLogPopupDetailFrame) end
local function JocysCom_QuestMapDetailsScrollFrame_OnShow() JocysCom_ShowFrames(WorldMapFrame) end

-- [ Close ] Options button.
local function JocysCom_OptionsFrame_CloseButton_OnClick(self)
	JocysCom_OptionsFrame:Hide();
end

---- [ Close & Play ] Options button.
--local function JocysCom_OptionsFrame_CloseAndPlayButton_OnClick(self)
	--stopWhenClosing = 0;
    ---- close  windows here.
--end

-- [ TTS... ] button.
function JocysCom_OptionsButton_OnClick(self)
	if JocysCom_OptionsFrame:IsShown() then
		JocysCom_OptionsFrame:Hide();
	else
		JocysCom_OptionsFrame:Show();
	end
end

function JocysCom_OptionsFrame_OnShow()
	JocysCom_UnLockFrames();
end

function JocysCom_OptionsFrame_OnHide()
	if string.len(ReplaceNameEditBox:GetText()) < 2 then
		ReplaceNameEditBox:SetText(unitName);
	end
	JocysCom_LockFrames();
end

-- [ Play ] button.
function JocysCom_PlayButtonButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if QuestLogPopupDetailFrame:IsShown() or WorldMapFrame:IsShown() then
		local questDescription, questObjectives = GetQuestLogQuestText();
		lastQuest = questObjectives .. " Description. " .. questDescription;
	end
	JocysCom_SpeakMessage("Scroll", lastQuest);
end

-- [ Stop ] button
function JocysCom_StopButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOff");
	sendChatMessageStop(1);
end

-- Close frames on "Escape" key press.
local function EditBox_OnEscapePressed()
	JocysCom_OptionsButton_OnClick();
	CloseQuest();
	CloseGossip();
	CloseItemText();
	sendChatMessageStop(0);
end

-- Black Background for Quest frame.
function JocysCom_QuestFrame_OnLoad(self)
	self.Bg:SetTexture(0, 0, 0, 1.0);
end

-- Save values on options close or logout.
function JocysCom_SaveAllSettings()
	-- Save check buttons.
	AutoCheckButtonValue = AutoCheckButton:GetChecked();
	DndCheckButtonValue = DndCheckButton:GetChecked();
	FilterCheckButtonValue = FilterCheckButton:GetChecked();
	LockCheckButtonValue = LockCheckButton:GetChecked();
	StopOnCloseCheckButtonValue = StopOnCloseCheckButton:GetChecked();
	MonsterCheckButtonValue = MonsterCheckButton:GetChecked();
	WhisperCheckButtonValue = WhisperCheckButton:GetChecked();
	SayCheckButtonValue = SayCheckButton:GetChecked();
	PartyCheckButtonValue = PartyCheckButton:GetChecked();
	GuildCheckButtonValue = GuildCheckButton:GetChecked();
	RaidCheckButtonValue = RaidCheckButton:GetChecked();
	RaidLeaderCheckButtonValue = RaidLeaderCheckButton:GetChecked();
	BattlegroundLeaderCheckButtonValue = BattlegroundLeaderCheckButton:GetChecked();
	BattlegroundCheckButtonValue = BattlegroundCheckButton:GetChecked();
	InstanceLeaderCheckButtonValue = InstanceLeaderCheckButton:GetChecked();
	InstanceCheckButtonValue = InstanceCheckButton:GetChecked();
	-- Save edit boxes.
	ReplaceNameEditBoxValue = ReplaceNameEditBox:GetText();

	JocysCom_StopFrame:SetMovable(true);
	JocysCom_StopFrame:EnableMouse(true);
	JocysCom_StopFrame:SetUserPlaced(true);
end

function JocysCom_LoadAllSettings()
	JocysCom_ShowFrames(GossipFrame);
	-- Load styles.
	QuestEditBox:SetTextInsets(5, 5, 5, 5);	
	-- Load descriptions.
	JocysCom_Text_EN();	
	-- Set SetJustifyH
	ReplaceNameEditBox:SetJustifyH("CENTER");	
	-- If -------------------- value --- was set then set this --- value --------- else --------- default value.
	if LockCheckButtonValue == true then LockCheckButton:SetChecked(true) else LockCheckButton:SetChecked(false) end;
	if AutoCheckButtonValue == false then AutoCheckButton:SetChecked(false) else AutoCheckButton:SetChecked(true) end;
	if DndCheckButtonValue == false then DndCheckButton:SetChecked(false) else DndCheckButton:SetChecked(true) end;
	if FilterCheckButtonValue == false then FilterCheckButton:SetChecked(false) else FilterCheckButton:SetChecked(true) end;
	if StopOnCloseCheckButtonValue == true then StopOnCloseCheckButton:SetChecked(true) else StopOnCloseCheckButton:SetChecked(false) end;
	if MonsterCheckButtonValue == false then MonsterCheckButton:SetChecked(false) else MonsterCheckButton:SetChecked(true) end;
	if WhisperCheckButtonValue == false then WhisperCheckButton:SetChecked(false) else WhisperCheckButton:SetChecked(true) end;
	if SayCheckButtonValue == true then SayCheckButton:SetChecked(true) else SayCheckButton:SetChecked(false) end;
	if PartyCheckButtonValue == false then PartyCheckButton:SetChecked(false) else PartyCheckButton:SetChecked(true) end;
	if GuildCheckButtonValue == false then GuildCheckButton:SetChecked(false) else GuildCheckButton:SetChecked(true) end;
	if RaidCheckButtonValue == false then RaidCheckButton:SetChecked(false) else RaidCheckButton:SetChecked(true) end;
	if RaidLeaderCheckButtonValue == true then RaidLeaderCheckButton:SetChecked(true) else RaidLeaderCheckButton:SetChecked(false) end;
	if BattlegroundCheckButtonValue == false then BattlegroundCheckButton:SetChecked(false) else BattlegroundCheckButton:SetChecked(true) end;
	if BattlegroundLeaderCheckButtonValue == true then BattlegroundLeaderCheckButton:SetChecked(true) else BattlegroundLeaderCheckButton:SetChecked(false) end;
	if InstanceCheckButtonValue == false then InstanceCheckButton:SetChecked(false) else InstanceCheckButton:SetChecked(true) end;
	if InstanceLeaderCheckButtonValue == true then InstanceLeaderCheckButton:SetChecked(true) else InstanceLeaderCheckButton:SetChecked(false) end;

	StopOnCloseCheckButton.text:SetPoint("LEFT", 19, 1);
	MonsterCheckButton.text:SetPoint("TOPLEFT", 4, 10);
	WhisperCheckButton.text:SetPoint("TOPLEFT", 4, 10);
	SayCheckButton.text:SetPoint("TOPLEFT", 6, 10);
	PartyCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	GuildCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	BattlegroundCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	BattlegroundLeaderCheckButton.text:SetPoint("TOPLEFT", 2, 10);
	RaidCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	RaidLeaderCheckButton.text:SetPoint("TOPLEFT", 2, 10);
	InstanceCheckButton.text:SetPoint("TOPLEFT", 5, 10);
	InstanceLeaderCheckButton.text:SetPoint("TOPLEFT", 2, 10);
	
	-- Load edit boxes.
	if ReplaceNameEditBoxValue == "" or ReplaceNameEditBoxValue == nil then ReplaceNameEditBoxValue = unitName end
	ReplaceNameEditBox:SetText(ReplaceNameEditBoxValue);	
	-- Attach OnEscape scripts.
	QuestEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	ReplaceNameEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	-- Open JocysCom frames script.
	QuestMapDetailsScrollFrame:SetScript("OnHide", JocysCom_MiniFrame_Hide);
	-- Open JocysCom frames script.
	GossipFrame:SetScript("OnShow", JocysCom_GossipFrame_OnShow);
	QuestFrame:SetScript("OnShow", JocysCom_QuestFrame_OnShow);
	ItemTextFrame:SetScript("OnShow", JocysCom_ItemTextFrame_OnShow);
	QuestLogPopupDetailFrame:SetScript("OnShow", JocysCom_QuestLogPopupDetailFrame_OnShow);
	QuestMapDetailsScrollFrame:SetScript("OnShow", JocysCom_QuestMapDetailsScrollFrame_OnShow);
	JocysCom_OptionsFrame:SetScript("OnShow", JocysCom_OptionsFrame_OnShow);
	-- LockCheckButton Enable / Disable.
	if LockCheckButton:GetChecked() == true then
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

