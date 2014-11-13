-- /fstack - show frame names

-- Debug.
local DebugMode = 0; -- 1 = debug mode enabled, 0 = debug mode disabled
local debugLine = 0;
local function LogMessage(s)
	if DebugMode == 1 then
		debugLine = debugLine + 1;
		-- SendChatMessage("" .. debugLine .. ": " .. s, "WHISPER", "Common", unitName);	
		-- print(debugLine .. ": " .. s);	
	end
end

-- Set variables.
local lastQuest = nil;
local unitName = UnitName("player");
local messageStop = "<voice command=\"stop\" />";
local DoNotDisturb_Message = "Please wait... NPC dialog window is open and text-to-speech is enabled.";

-- Set text.
local function JocysCom_Text_EN()
	-- Titles.
	OptionsFrame.TitleText:SetText("Jocys.com Text to Speech World of Warcraft Addon 2.1.6 ( 2014-11-13 )");
	-- Frames.
	ScrollFrame.text:SetText("When addon is enabled and quest window is open, you can use your mouse wheel over this hidden frame.\n\nSCROLL UP = START SPEECH\n\nSCROLL DOWN = STOP SPEECH");
	-- Check buttons.
	FilterCheckButton.text:SetText("|cff808080 Hide addon's whisper messages in chat window.|r");
	DndCheckButton.text:SetText("|cff808080 Show|r |cffffffff<Busy>|r|cff808080 over my character for other players, when NPC dialogue window is open and speech is on.|r");
	AutoCheckButton.text:SetText("|cff808080 Auto start speech, when dialog window is open.|r");
	---- Font Strings
	DescriptionFrameFontString:SetText("Text-to-speech voices, pitch, rate, effects, etc. ... you will find all options in |cff77ccffJocys.Com Text to Speech Monitor|r.\n\nHow it works: When you open NPC dialogue window, |cff77ccffJocys.Com Text to Speech WoW Addon|r creates and sends special whisper message to yourself (message includes dialogue text, character name and effect name). Then, |cff77ccffJocys.Com Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.");
	ReplaceNameFontString:SetText("Here you can change your name for text to speech from |cff00ff00" .. unitName .. "|r to something else.");
	MessageFontString:SetText("Whisper message for |cff77ccffJocys.Com Text to Speech Monitor|r ... it must be runninng in background:");	
end

-- Frames Lock or Unlock.
local function UnlockFrames()
	MiniFrame:SetMovable(true);
	MiniFrame:EnableMouse(true);
	MiniFrame:RegisterForDrag("LeftButton");
	MiniFrame:SetScript("OnDragStart", MiniFrame.StartMoving);
	MiniFrame:SetScript("OnDragStop", MiniFrame.StopMovingOrSizing);
	MiniFrame.texture:SetTexture(0, 0, 0, 0.8);
	ScrollFrame:SetMovable(true);
	ScrollFrame:SetResizable(true);
	ScrollFrame:EnableMouse(true);
	ScrollFrame.texture:SetTexture(0, 0, 0, 0.8);
	ScrollFrame.text:Show();
	ScrollFrame.resizeButton:Show();
	ScrollFrame:SetFrameLevel(100);
end

local function LockFrames()
	MiniFrame:SetMovable(nil);
	MiniFrame.texture:SetTexture(0, 0, 0, 0);
	ScrollFrame:SetMovable(nil);
	ScrollFrame:SetResizable(nil);
	ScrollFrame:EnableMouse(nil);
	ScrollFrame.texture:SetTexture(0, 0, 0, 0);
	ScrollFrame.text:Hide();
	ScrollFrame.resizeButton:Hide();
	ScrollFrame:SetFrameLevel(100);
end

-- Assign frames to GossipFrame, QuestFrame or ItemTextFrame.
local function UpdateMiniAndScrollFrame()
	local frame = nil;
	if GossipFrame:IsShown() then frame = GossipFrame end
	if QuestFrame:IsShown() then frame = QuestFrame end
	if ItemTextFrame:IsShown() then frame = ItemTextFrame end 
	if frame == nil then
		-- Hide all frames.
		MiniFrame:Hide();
		ScrollFrame:Hide();
		OptionsFrame:Hide();
	else
		local top = frame:GetTop();
		local width = frame:GetWidth();
		-- Move mini frame.
		MiniFrame:ClearAllPoints();
		MiniFrame:SetParent(frame);
		-- Mini frame position, depending on event.
		if (frame == ItemTextFrame) then
		MiniFrame:SetPoint("TOP", 15, 33);
		else
		MiniFrame:SetPoint("TOPRIGHT", -4, -26);
		end
		MiniFrame:Show();
		-- Move scroll frame.
		ScrollFrame:ClearAllPoints();
		ScrollFrame:SetParent(frame);
		ScrollFrame:SetPoint("TOPLEFT", 12, -67);
		ScrollFrame:SetWidth(width - 50);
		ScrollFrame:Show();
		-- Move options frame.
		OptionsFrame:ClearAllPoints();
		OptionsFrame:SetPoint("TOPLEFT", frame, "TOPRIGHT", 4, 0);
		if OptionsFrame:IsShown() then
			UnlockFrames();
		else
			LockFrames();
		end
	end
end

-- FUNCTION ENABLE or DISABLE TTS
local function EditBox_OnEscapePressed()
	CloseQuest();
	CloseGossip();
	CloseItemText();
	sendChatMessageStop();
	UpdateMiniAndScrollFrame();
end

-- FUNCTION MESSAGE STOP
local function sendChatMessageStop()
		SendChatMessage(messageStop, "WHISPER", "Common", unitName);
		QuestEditBox:SetText("|cff808080" .. messageStop .. "|r");
	    -- Disable DND <Busy> if checked.
		if DndCheckButton:GetChecked() == true then
		if UnitIsDND("player") == true then
			SendChatMessage("", "DND");
		end
		end
end

-- Close Options frame.
local function OptionsFrame_CloseButton_OnClick(self)
	OptionsFrame:Hide();
	UpdateMiniAndScrollFrame();
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

function JocysCom_OptionsFrame_OnLoad(self)
	-- self:Hide();
	self:SetPoint("TOPLEFT", 356, -116);
	self:SetFrameStrata("HIGH");
	self:SetMovable(true);
	self:EnableMouse(true);
	self:RegisterForDrag("LeftButton");
	self:SetScript("OnDragStart", self.StartMoving);
	self:SetScript("OnDragStop", self.StopMovingOrSizing);
	self.CloseButton:SetScript("OnClick", OptionsFrame_CloseButton_OnClick);
	-- Setup title.
	self.TitleText:SetPoint("TOP", 0, -6);
	-- Hide messages.
	JocysCom_MessageEventFilters();
	-- Attach events.
	self:SetScript("OnEvent", JocysCom_OptionsFrame_OnEvent);
	self:RegisterEvent("ADDON_LOADED");
	self:RegisterEvent("QUEST_GREETING");
	self:RegisterEvent("GOSSIP_SHOW");
	self:RegisterEvent("QUEST_DETAIL");
	self:RegisterEvent("QUEST_PROGRESS");
	self:RegisterEvent("QUEST_COMPLETE");
	self:RegisterEvent("QUEST_ACCEPTED");
	self:RegisterEvent("QUEST_FINISHED");
	self:RegisterEvent("GOSSIP_CLOSED");
	self:RegisterEvent("ITEM_TEXT_BEGIN"); -- books, scrolls, saved copies of mail messages, plaques, gravestones.
	self:RegisterEvent("ITEM_TEXT_READY"); 
	self:RegisterEvent("ITEM_TEXT_CLOSED"); 
	self:RegisterEvent("PLAYER_LOGOUT");
	-- Attach OnEscape scripts.
	QuestEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	ReplaceNameEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	LogMessage("Registered");		
end

local JocysCom_event;

function JocysCom_OptionsFrame_OnEvent(self, event)
	if event == "ADDON_LOADED" then
		JocysCom_LoadAllSettings();
		UpdateMiniAndScrollFrame();
	elseif event == "QUEST_GREETING" or event == "GOSSIP_SHOW" or event == "QUEST_DETAIL" or event == "QUEST_PROGRESS" or event == "QUEST_COMPLETE" or event == "ITEM_TEXT_BEGIN" or event == "ITEM_TEXT_READY" then
		UpdateMiniAndScrollFrame();
		if event == "QUEST_GREETING" then
			lastQuest = GetGreetingText();
		elseif event == "GOSSIP_SHOW" then
			lastQuest = GetGossipText();
		elseif event == "QUEST_DETAIL" then
			lastQuest = GetQuestText() .. " Your objective is to " .. GetObjectiveText();
		elseif event == "QUEST_PROGRESS" then
			lastQuest = GetProgressText();
		elseif event == "QUEST_COMPLETE" then
			lastQuest = GetRewardText();
		elseif event == "ITEM_TEXT_BEGIN" then
			lastQuest = ItemTextGetText();
		elseif event == "ITEM_TEXT_READY" then
			lastQuest = ItemTextGetText();
		end
		JocysCom_event = event;	
		if (string.find(lastQuest, "HTML") ~= nil) then
			lastQuest = string.gsub(lastQuest, "%b<>", "")
		end
		JocysCom_SpeakMessage("Auto", lastQuest);
	elseif event == "QUEST_ACCEPTED" or event == "QUEST_FINISHED" or event == "GOSSIP_CLOSED" or event == "ITEM_TEXT_CLOSED" then
		UpdateMiniAndScrollFrame();				
			local isShown = GossipFrame:IsShown() or QuestFrame:IsShown() or ItemTextFrame:IsShown();
			if isShown == false then
			sendChatMessageStop();
			end
	elseif event == "PLAYER_LOGOUT" then
		JocysCom_SaveAllSettings();
	end
end

function JocysCom_SpeakMessage(reason, questText, event)
	if questText == nil then
		LogMessage("No Quest text");
		return;
	end
	-- Get quest message.
	local message = questText;
	-- Get NPC name (Hero call board have wrong target!).
	local NPCName = GetUnitName("npc");
	if NPCName == nil then
	NPCName = "nil"
	else
	NPCName = string.gsub(NPCName, "\"", "");
	end
	-- Get NPC type (Beast, Dragonkin, Demon, Elemental, Giant, Undead, Humanoid, Critter, Mechanical, Not specified, Totem, Non-combat Pet, Gas Cloud).							
	local NPCType = UnitCreatureType("npc");
	if NPCType == nil then NPCType = "Default" end;
	-- Get NPC gender.
	local NPCSex = UnitSex("npc");
	local NPCGender = "Neutral";
	if NPCSex == 3 then
		NPCGender = "Female";
	elseif NPCSex == 2 then
		NPCGender = "Male";
	end
	LogMessage(NPCName .. ", " .. NPCType .. ", " .. NPCGender);
	
	-- Replace name.
	-- LogMessage("Speak 1: Replace name.");
	local newUnitName = ReplaceNameEditBox:GetText();
	if string.len(newUnitName) > 0 and newUnitName ~= unitName then
		message = string.gsub(message, unitName, newUnitName);
	end
	-- Format text.
	-- LogMessage("Speak 1: Format text.");
	local pitchComment = "0";
	-- Replace.
	message = string.gsub(message, "\r", " ");
	message = string.gsub(message, "\n(%u)", ".%1");
	message = string.gsub(message, "\n", " ");
	message = string.gsub(message, "[ ]+", " ");
	message = string.gsub(message, " %.", ".");
	message = string.gsub(message, "[%.]+", ".");
	message = string.gsub(message, "%?%.", "? ");
	message = string.gsub(message, "!%.", "! ");
	message = string.gsub(message, "%?!%?", "?! ");
	message = string.gsub(message, "^%.", "");
	message = string.gsub(message, ">%.", ".>");
	message = string.gsub(message, "[%.]+", ". ");
	message = string.gsub(message, "<", " [comment] ");
	message = string.gsub(message, ">", " [/comment] ");
	message = string.gsub(message, "[ ]+", " ");
	-- Format and send whisper message.
	-- LogMessage("Speak 2, " .. reason .. ", message=" .. string.len(message));
	if (reason == "Scroll") or (reason == "Auto" and AutoCheckButton:GetChecked() == true) then
		--framesClosed = 1;
		local size = 130;
		local startIndex = 1;
		local endIndex = 1;
		local part = "";
		local chatMessageSP = "<voice name=\"" .. NPCName .. "\" gender=\"" .. NPCGender .. "\" effect=\"" .. NPCType .. "\" command=\"play\"><part>";
		
	if (JocysCom_event == "ITEM_TEXT_BEGIN") or (JocysCom_event == "ITEM_TEXT_READY") then
		chatMessageSP = "<voice command=\"play\"><part>";
	else
		if (string.find(NPCName, "Board") ~= nil) then
			chatMessageSP = "<voice command=\"play\"><part>";
		end
	end
		local chatMessageSA = "<voice command=\"add\"><part>";
		local chatMessageE = "</part></voice>";
		local chatMessage;
		while true do
			local command = "";
			local index = string.find(message, " ", endIndex);
			-- print(index);
			-- if nothing found then...
			if index == nil then
				part = string.sub(message, startIndex);
				chatMessage = chatMessageSP .. part .. chatMessageE;
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
				-- print("[" .. startIndex .. "] '" .. part .. "'");
				break;
			elseif (index - startIndex) > size then
				-- if space is out of size then...
				part = string.sub(message, startIndex, endIndex - 1);
				chatMessage = chatMessageSA .. part .. chatMessageE;
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
				-- print("[" .. startIndex .. "-" .. (endIndex - 1) .. "] '" .. part .. "'");
				startIndex = endIndex;
			end
			-- look for next space.
			endIndex = index + 1;
		end
		-- FILL EDITBOXES  .. "<" .. event .. " />" ..
		message = string.gsub(message, "%[comment]", "|cff808080[comment]|r|cfff7e593");
		message = string.gsub(message, "%[/comment]", "|r|cff808080[/comment]|r");
		local outputEditBox = "|cff808080" .. chatMessageSP .. "|r\n" .. message .. "\n|cff808080" .. chatMessageE .. "|r";
		QuestEditBox:SetText(outputEditBox);
		-- QuestEditBox:HighlightText();

		-- Enable DND <Busy> if checked.
		if DndCheckButton:GetChecked() == true then
		if UnitIsDND("player") == false then
		SendChatMessage("<" .. unitName .. ">: " .. DoNotDisturb_Message, "DND");
	    end		
		end
	end
end

function JocysCom_OptionsButton_OnClick(self)
	if OptionsFrame:IsShown() then
		OptionsFrame:Hide();
	else
		OptionsFrame:Show();
	end
	UpdateMiniAndScrollFrame();	
end

-- Auto start.
function JocysCom_AutoCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if self:GetChecked() == true then
	end
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
			SendChatMessage("<" .. unitName .. ">: " .. DoNotDisturb_Message, "DND");
	    end	
	end
end

function JocysCom_FilterCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
end

function JocysCom_ScrollFrame_OnLoad(self)
	self:SetScript("OnMouseWheel", JocysCom_ScrollFrame_OnMouseWheel);
end

function JocysCom_ScrollFrame_OnMouseDown(self)
	self:StartMoving();
end

function JocysCom_ScrollFrame_OnMouseUp(self)
	self:StopMovingOrSizing();
end

function JocysCom_ScrollFrame_OnMouseWheel(self, delta)
		-- Send Message
		if delta == 1 then
			JocysCom_SpeakMessage("Scroll", lastQuest);
		else
			sendChatMessageStop();
		end
end

function JocysCom_PlayButtonButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	JocysCom_SpeakMessage("Scroll", lastQuest);
end

function JocysCom_StopButtonButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOff");
	sendChatMessageStop();
end

function JocysCom_ScrollResizeButton_OnMouseDown(self)
	self:GetParent():StartSizing("BOTTOMRIGHT");
	self:GetParent():SetUserPlaced(true);
end

function JocysCom_ScrollResizeButton_OnMouseUp(self)
	self:GetParent():StopMovingOrSizing();
end

function JocysCom_QuestScrollFrame_OnLoad(self)
	self:SetScrollChild(QuestEditBox);
end

function JocysCom_QuestFrame_OnLoad(self)
	self.Bg:SetTexture(0, 0, 0, 1.0);
end

-- FUNCTION SAVE VALUES (ON OPTIONS CLOSE AND LOGOUT)

function JocysCom_SaveAllSettings()
	-- Save check buttons.
	AutoCheckButtonValue = AutoCheckButton:GetChecked();
	DndCheckButtonValue = DndCheckButton:GetChecked();
	FilterCheckButtonValue = FilterCheckButton:GetChecked();
	-- Save edit boxes.
	ReplaceNameEditBoxValue = ReplaceNameEditBox:GetText();
	LogMessage("Settings Saved");
end

function JocysCom_LoadAllSettings()

	-- Load styles.
	MiniFrame.texture:SetTexture(0, 0, 0, 0);
	QuestEditBox:SetTextInsets(5, 5, 5, 5);
	
	-- Load descriptions.
	JocysCom_Text_EN();
	
	-- Set SetJustifyH
	ReplaceNameEditBox:SetJustifyH("CENTER");
	
	-- If this is first load then...
	if CharacterFirstLoginValue ~= 1 then
		CharacterFirstLoginValue = 1;
		AutoCheckButtonValue = 1;
		DndCheckButtonValue = 1;
		FilterCheckButtonValue = 1;
	end
	AutoCheckButton:SetChecked(AutoCheckButtonValue);
	DndCheckButton:SetChecked(DndCheckButtonValue);
	FilterCheckButton:SetChecked(FilterCheckButtonValue);	
	-- Load edit boxes.
	if ReplaceNameEditBoxValue == "" or ReplaceNameEditBoxValue == nil then ReplaceNameEditBoxValue = unitName end;
	ReplaceNameEditBox:SetText(ReplaceNameEditBoxValue);

end

