-- /fstack - show frame names

-- Debug.
local DebugEnabled = false; -- true = debug mode enabled, false = debug mode disabled

-- Set variables.
local unitName = UnitName("player");
local messageStop = "<voice command=\"stop\" />";
local messageDoNotDisturb = "Please wait... NPC dialog window is open and text-to-speech is enabled.";

-- Set text.
local function JocysCom_Text_EN()
	-- Titles.
	JocysCom_OptionsFrame.TitleText:SetText("Jocys.com Text to Speech World of Warcraft Addon 2.2.7 ( 2014-11-19 )");
	-- Frames.
	JocysCom_ScrollFrame.text:SetText("When mouse pointer is over this frame...\n\nSCROLL UP will START SPEECH\n\nSCROLL DOWN will STOP SPEECH");
	-- Check buttons.
	FilterCheckButton.text:SetText("|cff808080 Hide addon's whisper messages in chat window.|r");
	DndCheckButton.text:SetText("|cff808080 Show|r |cffffffff<Busy>|r|cff808080 over my character for other players, when NPC dialogue window is open and speech is on.|r");
	AutoCheckButton.text:SetText("|cff808080 Auto start speech, when dialog window is open (except for quest log windows).|r");
	---- Font Strings
	DescriptionFrameFontString:SetText("Text-to-speech voices, pitch, rate, effects, etc. ... you will find all options in |cff77ccffJocys.Com Text to Speech Monitor|r.\n\nHow it works: When you open NPC dialogue window, |cff77ccffJocys.Com Text to Speech WoW Addon|r creates and sends special whisper message to yourself (message includes dialogue text, character name and effect name). Then, |cff77ccffJocys.Com Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.");
	ReplaceNameFontString:SetText("Here you can change your name for text to speech from |cff00ff00" .. unitName .. "|r to something else.");
	MessageFontString:SetText("Whisper message for |cff77ccffJocys.Com Text to Speech Monitor|r ... it must be runninng in background:");	
end

local lastQuest = nil;
local debugLine = 0;
local debugLineOld = 0;
local questId = nil;
local questIdOld = nil;

-- Lock frames.
local function JocysCom_UnLockFrames()
	JocysCom_MiniFrame:SetMovable(true);
	JocysCom_MiniFrame:EnableMouse(true);
	JocysCom_MiniFrame:RegisterForDrag("LeftButton");
	JocysCom_MiniFrame:SetScript("OnDragStart", JocysCom_MiniFrame.StartMoving);
	JocysCom_MiniFrame:SetScript("OnDragStop", JocysCom_MiniFrame.StopMovingOrSizing);
	JocysCom_ScrollFrame:SetMovable(true);
	JocysCom_ScrollFrame:SetResizable(true);
	JocysCom_ScrollFrame:EnableMouse(true);
	JocysCom_ScrollFrame.texture:SetTexture(0, 0, 0, 0.8);
	JocysCom_ScrollFrame.text:Show();
	JocysCom_ScrollFrame.resizeButton:Show();
	JocysCom_ScrollFrame:SetFrameLevel(100);
end

-- Unlock frames.
local function JocysCom_LockFrames()
	JocysCom_MiniFrame:SetMovable(nil);
	JocysCom_ScrollFrame:SetMovable(nil);
	JocysCom_ScrollFrame:SetResizable(nil);
	JocysCom_ScrollFrame:EnableMouse(nil);
	JocysCom_ScrollFrame.texture:SetTexture(0, 0, 0, 0);
	JocysCom_ScrollFrame.text:Hide();
	JocysCom_ScrollFrame.resizeButton:Hide();
	JocysCom_ScrollFrame:SetFrameLevel(100);
end

-- FUNCTION MESSAGE STOP
local function sendChatMessageStop()
		-- Disable DND <Busy> if checked.
		if DndCheckButton:GetChecked() == true then
		if UnitIsDND("player") == true then
			SendChatMessage("", "DND");
		end
		end
		SendChatMessage(messageStop, "WHISPER", "Common", unitName);
		QuestEditBox:SetText("|cff808080" .. messageStop .. "|r");
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
	self:SetFrameStrata("HIGH");
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
	-- Logout event.
	self:RegisterEvent("PLAYER_LOGOUT");
end

function JocysCom_OptionsFrame_OnEvent(self, event)
	-- Debug.
	if DebugEnabled then print(event) end
	-- Events.
	if event == "ADDON_LOADED" then
		JocysCom_LoadAllSettings();
	elseif event == "PLAYER_LOGOUT" then
		JocysCom_SaveAllSettings();
	elseif JocysCom_MiniFrame:IsShown() and event ~= "ADDON_LOADED" and event ~= "PLAYER_LOGOUT" and event ~= "QUEST_ACCEPTED" and event ~= "QUEST_FINISHED" and event ~= "GOSSIP_CLOSED" and event ~= "ITEM_TEXT_CLOSED" then
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
		if string.find(lastQuest, "HTML") ~= nil then
			lastQuest = string.gsub(lastQuest, "%b<>", "");
		end
		lastQuest = string.gsub(lastQuest, "\"", "");
		lastQuest = string.gsub(lastQuest, "&", " and ");
		JocysCom_SpeakMessage("Auto", lastQuest);
	elseif event == "GOSSIP_CLOSED" then
			JocysCom_MiniFrame_Hide();
	end


end

function JocysCom_SpeakMessage(reason, questText, event)
	-- if no quest text.
	if questText == nil then return end
	-- Set NPC name.
	local NPCName = GetUnitName("npc");
	if NPCName == nil then
		NPCName = "";
	else
	NPCName = string.gsub(NPCName, "&", " and ");
	NPCName = string.gsub(NPCName, "\"", "");
		NPCName = " name=\"" .. NPCName .. "\"";
	end
	-- Set NPC gender (1 = Neutrum / Unknown, 2 = Male, 3 = Female). 
	local NPCSex = UnitSex("npc");
	local NPCGender = nil;
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
	questText = string.gsub(questText, "!%.", "! ");
	questText = string.gsub(questText, "%?%.", "? ");
	questText = string.gsub(questText, "%?!%?", "?! ");
	questText = string.gsub(questText, "^%.", "");
	questText = string.gsub(questText, ">%.", ".>");
	questText = string.gsub(questText, "[%.]+", ". ");
	questText = string.gsub(questText, "<", " [comment] ");
	questText = string.gsub(questText, ">", " [/comment] ");
	questText = string.gsub(questText, "[ ]+", " ");
	-- Format and send whisper message.
	if (reason == "Scroll") or (reason == "Auto" and AutoCheckButton:GetChecked() == true) then
		local size = 130;
		local startIndex = 1;
		local endIndex = 1;
		local part = "";
		local chatMessageSP = "<voice" .. NPCName .. NPCGender .. NPCType .. " command=\"play\"><part>";
		if NPCGender == "" then
		chatMessageSP = "<voice" .. NPCType .. " command=\"play\"><part>";
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
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
				-- print("[" .. startIndex .. "] '" .. part .. "'");
				break;
			elseif (index - startIndex) > size then
				-- if space is out of size then...
				part = string.sub(questText, startIndex, endIndex - 1);
				chatMessage = chatMessageSA .. part .. chatMessageE;
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
		if DndCheckButton:GetChecked() == true then
		if UnitIsDND("player") == false then
		SendChatMessage("<" .. unitName .. ">: " .. messageDoNotDisturb, "DND");
	    end		
		end
	end
end

-- Auto start speech.
function JocysCom_AutoCheckButton_OnClick(self) PlaySound("igMainMenuOptionCheckBoxOn") end

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
			sendChatMessageStop(); -- Send stop message.
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
	JocysCom_ScrollFrame:ClearAllPoints();
	
	if frame == WorldMapFrame then
	JocysCom_ScrollFrame:SetParent(QuestMapDetailsScrollFrame);
	else
	JocysCom_ScrollFrame:SetParent(frame);
	end

	JocysCom_ScrollFrame:SetFrameLevel(100);
	-- Move Mini frame.		
	JocysCom_MiniFrame:ClearAllPoints();
	JocysCom_MiniFrame:SetParent(frame);
	-- Move Options frame.	
	JocysCom_OptionsFrame:ClearAllPoints();	
	JocysCom_OptionsFrame:SetParent(frame);
	-- Frames position, depending on event.
	JocysCom_MiniFrame:SetPoint("TOPRIGHT", frame, "BOTTOMRIGHT", 0, -4);
	JocysCom_OptionsFrame:SetPoint("TOPLEFT", frame, "TOPRIGHT", 4, 0);
	
	if frame == WorldMapFrame then
	JocysCom_ScrollFrame:SetPoint("TOPLEFT", 0, -4);
	local width2 = QuestMapDetailsScrollFrame:GetWidth();
	JocysCom_ScrollFrame:SetWidth(width2 - 4);
	else
	JocysCom_ScrollFrame:SetWidth(width - 50);
	JocysCom_ScrollFrame:SetPoint("TOPLEFT", 12, -67);
	end

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
	sendChatMessageStop();
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
	JocysCom_SpeakMessage("Scroll", lastQuest);
end

-- [ Stop ] button
function JocysCom_StopButtonButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOff");
	sendChatMessageStop();
end

-- Close frames on "Escape" key press.
local function EditBox_OnEscapePressed()
	JocysCom_OptionsButton_OnClick();
	CloseQuest();
	CloseGossip();
	CloseItemText();
	sendChatMessageStop();
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
	-- Save edit boxes.
	ReplaceNameEditBoxValue = ReplaceNameEditBox:GetText();
end

function JocysCom_LoadAllSettings()
	JocysCom_ShowFrames(GossipFrame);
	-- Load styles.
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
end

