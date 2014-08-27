--/fstack - show frame names

-- DEFAULT VOICES

Female = "IVONA 2 Amy";
Male = "IVONA 2 Brian";
Unknown = "Microsoft Zira Desktop";

-- TEXT

unitName = UnitName("player");
messageStop = "<voice command=\"stop\" />";

textFrameOptionsTitle = "JOCYS.COM WORLD OF WARCRAFT TEXT TO SPEECH ADDON - VERSION 2014.08.25";
textDescription = "How it works: When you open NPC dialogue window, |cff77ccffJocys.Com WoW Text to Speech Addon|r creates and sends special whisper message to yourself (message includes dialogue text, voice name, pitch value and effect name). Then, |cff77ccffJocys.Com WoW Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.";
textReplace = "CHANGE MY NAME IN TTS\n\nFROM |cff00ff00" .. unitName .. "|r TO";
textDefaults = "RESET and FILL FIELDS with DEFAULT VALUES";
textFrameScroll = "When addon is enabled and quest window is open, you can use your mouse wheel over this frame.\n\nSCROLL UP = START SPEECH\n\nSCROLL DOWN = STOP SPEECH";
textDoNotDisturb = "Please wait... NPC dialog window is open and text-to-speech is enabled.";

-- FUNCTION : CREATE DEFAULT FRAME

local function createFrame(name, parent)
	frame = CreateFrame("Frame", name, parent);
	frame:Hide();
	frame:SetSize(100, 100);
	frame:SetFrameStrata("HIGH");
	frame:SetMovable(true);
	frame:EnableMouse(true);
	frame:RegisterForDrag("LeftButton");
	frame:SetScript("OnDragStart", frame.StartMoving);
	frame:SetScript("OnDragStop", frame.StopMovingOrSizing);
	frame.texture = frame:CreateTexture("ARTWORK");
	frame.texture:SetAllPoints();
	frame.texture:SetTexture(0, 0, 0, 0.8);
end

-- FUNCTION : CREATE DEFAULT EDITBOX

local function createEditBox(name, parent, text)
	frame = CreateFrame("EditBox", name, parent);
	frame:SetSize(150, 15);
	frame:SetTextInsets(2, 2, 0, 0);
	frame:SetFontObject("GameFontHighlightSmall");
	frame:SetText(text);
	frame.texture = frame:CreateTexture("ARTWORK");
	frame.texture:SetAllPoints();
	frame.texture:SetTexture(0, 0, 0, 1.0);
end

-- FUNCTION : CREATE DEFAULT EDITBOX 2

local function createEditBox2(name, parent, text)
	frame = CreateFrame("EditBox", name, parent);
	frame:SetSize(80, 15);
	frame:SetTextInsets(2, 2, 0, 0);
	frame:SetFontObject("GameFontHighlightSmall");
	frame:SetText(text);
	frame.texture = frame:CreateTexture("ARTWORK");
	frame.texture:SetAllPoints();
	frame.texture:SetTexture(0, 0, 0, 1.0);
	frame:SetJustifyH("CENTER");
end

-- MINI FRAME

createFrame("FrameMini", UIParent);
frame:SetFrameStrata("DIALOG");
FrameMini:SetSize(106, 20);
FrameMini:SetPoint("TOPLEFT", 245, -147);
FrameMini.texture:SetTexture(0, 0, 0, 0);

-- MINI FRAME EDITBOX

createEditBox("EditBoxMini", FrameMini, "");
EditBoxMini:SetPoint("TOPLEFT", 0, 0);
EditBoxMini:SetSize(0, 15);

-- MINI FRAME OPTIONS BUTTON

CreateFrame("Button", "ButtonOptions", FrameMini, "UIPanelButtonTemplate");
ButtonOptions:SetPoint("TOPLEFT", 8, 1);
ButtonOptions:SetSize(90, 21);
ButtonOptions:SetText("Options");

-- OPTIONS FRAME

CreateFrame("Frame", "FrameOptions", UIParent, "BasicFrameTemplate");
FrameOptions:Hide();
FrameOptions:SetSize(716, 496);
FrameOptions:SetPoint("TOPLEFT", 356, -116);
FrameOptions:SetFrameStrata("HIGH");
FrameOptions:SetMovable(true);
FrameOptions:EnableMouse(true);
FrameOptions:RegisterForDrag("LeftButton");
FrameOptions:SetScript("OnDragStart", frame.StartMoving);
FrameOptions:SetScript("OnDragStop", frame.StopMovingOrSizing);

-- OPTIONS FRAME TITLE

FrameOptions:CreateFontString("TitleOptions", ARTWORK, "GameFontHighlightSmall");
TitleOptions:SetPoint("TOP", -9, -7);
TitleOptions:SetTextColor(1.0, 0.8, 0.0, 1.0);
TitleOptions:SetText(textFrameOptionsTitle);

-- OPTIONS FRAME EVENTS

FrameOptions:RegisterEvent("ADDON_LOADED");
FrameOptions:RegisterEvent("QUEST_GREETING");
FrameOptions:RegisterEvent("GOSSIP_SHOW");
FrameOptions:RegisterEvent("QUEST_DETAIL");
FrameOptions:RegisterEvent("QUEST_PROGRESS");
FrameOptions:RegisterEvent("QUEST_COMPLETE");
FrameOptions:RegisterEvent("QUEST_ACCEPTED");
FrameOptions:RegisterEvent("QUEST_FINISHED");
FrameOptions:RegisterEvent("GOSSIP_CLOSED");
FrameOptions:RegisterEvent("PLAYER_LOGOUT");

-- ENABLE/DISABLE ADDON CHECKBUTTON

CreateFrame("CheckButton", "CheckButtonEnable", FrameOptions, "UICheckButtonTemplate");
CheckButtonEnable:SetPoint("TOPLEFT", 7, -25);
CheckButtonEnable:SetChecked(1);

-- ENABLE/DISABLE ADDON CHECKBUTTON TITLE

CheckButtonEnable:CreateFontString("CheckButtonEnableTitle", ARTWORK, "GameFontHighlightSmall");
CheckButtonEnableTitle:SetPoint("LEFT", 35, 0);
CheckButtonEnableTitle:SetText("Enable addon");
CheckButtonEnableTitle:SetTextColor(0.5, 0.5, 0.5, 1.0);

-- ENABLE/DISABLE AUTO START SPEECH CHECKBUTTON

CreateFrame("CheckButton", "CheckButtonAuto", FrameOptions, "UICheckButtonTemplate");
CheckButtonAuto:SetPoint("TOPLEFT", 115, -25);
CheckButtonAuto:SetChecked(1);

-- ENABLE/DISABLE AUTO START SPEECH CHECKBUTTON TITLE

CheckButtonAuto:CreateFontString("CheckButtonAutoTitle", ARTWORK, "GameFontHighlightSmall");
CheckButtonAutoTitle:SetPoint("LEFT", 34, 0);
CheckButtonAutoTitle:SetText("Auto start speech when dialog window is open");
CheckButtonAutoTitle:SetTextColor(0.5, 0.5, 0.5, 1.0);

-- ENABLE/DISABLE AUTO DO NOT DISTURB CHECKBUTTON

CreateFrame("CheckButton", "CheckButtonDND", FrameOptions, "UICheckButtonTemplate");
CheckButtonDND:SetPoint("TOPLEFT", 393, -25); --74
CheckButtonDND:SetChecked(1);

-- ENABLE/DISABLE AUTO DO NOT DISTURB CHECKBUTTON TITLE

CheckButtonDND:CreateFontString("CheckButtonDNDTitle", ARTWORK, "GameFontHighlightSmall");
CheckButtonDNDTitle:SetPoint("LEFT", 35, 0);
CheckButtonDNDTitle:SetText("Auto \"Do not disturb\"");
CheckButtonDNDTitle:SetTextColor(0.5, 0.5, 0.5, 1.0);

-- ENABLE/DISABLE MESSAGE FILTER CHECKBUTTON

CreateFrame("CheckButton", "CheckButtonFilter", FrameOptions, "UICheckButtonTemplate");
CheckButtonFilter:SetPoint("TOPLEFT", 546, -25);
CheckButtonFilter:SetChecked(1);

-- ENABLE/DISABLE MESSAGE FILTER CHECKBUTTON TITLE

CheckButtonFilter:CreateFontString("CheckButtonFilterTitle", ARTWORK, "GameFontHighlightSmall");
CheckButtonFilterTitle:SetPoint("LEFT", 35, 0);
CheckButtonFilterTitle:SetText("Hide addon messages");
CheckButtonFilterTitle:SetTextColor(0.5, 0.5, 0.5, 1.0);

-- FUNCTION : ENABLE MESSAGE FILTER : CHAT_MSG_WHISPER_INFORM

local function hideMessageInform(self, event, msg)
if msg:find("<voice") then
return true;
end
end
ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER_INFORM", hideMessageInform);

-- FUNCTION : ENABLE/DISABLE MESSAGE FILTER : CHAT_MSG_WHISPER

local function hideMessage(self, event, msg)
if msg:find("<voice") and CheckButtonFilter:GetChecked() == 1 then
return true;
end
end

-- FUNCTION : ENABLE/DISABLE MESSAGE FILTER : CHAT_MSG_DND

local function hideMessageDND(self, event, msg)
if msg:find("<" .. unitName .. ">: ") and CheckButtonFilter:GetChecked() == 1 then
return true;
end
end

-- FUNCTION : ENABLE/DISABLE MESSAGE FILTER : CHAT_MSG_SYSTEM

local function hideMessageSYSTEM(self, event, msg)
if msg:find("You are no") and CheckButtonFilter:GetChecked() == 1 then
return true;
end
end

-- FUNCTION : ENABLE/DISABLE MESSAGE FILTER

local function functionFilter()
ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER", hideMessage);
ChatFrame_AddMessageEventFilter("CHAT_MSG_DND", hideMessageDND);
ChatFrame_AddMessageEventFilter("CHAT_MSG_SYSTEM", hideMessageSYSTEM);
end
functionFilter();
CheckButtonFilter:SetScript("OnClick", functionFilter);

-- MOUSE SCROLL FRAME

createFrame("FrameScroll", UIParent);
frame:SetFrameStrata("DIALOG");
FrameScroll.texture:SetTexture(0, 0, 0, 0);
FrameScroll:SetPoint("TOPLEFT", 33, -188);
FrameScroll:SetSize(278, 200);

-- MOUSE SCROLL FRAME DESCRIPTION

FrameScroll:CreateFontString("TextScroll", ARTWORK, "GameFontHighlightSmall");
TextScroll:Hide();
TextScroll:SetPoint("CENTER");
TextScroll:SetSize(200, 200);
TextScroll:SetJustifyH("CENTER");
TextScroll:SetJustifyV("CENTER");
TextScroll:SetTextColor(0.5, 0.5, 0.5, 1.0);
TextScroll:SetText(textFrameScroll);

-- MOUSE SCROLL FRAME RESIZE BUTTON

CreateFrame("Button", "ResizeButton", FrameScroll);
ResizeButton:Hide();
ResizeButton:SetPoint("BOTTOMRIGHT");
ResizeButton:SetSize(16, 16);
ResizeButton:SetNormalTexture("Interface\\ChatFrame\\UI-ChatIM-SizeGrabber-Up");
ResizeButton:SetHighlightTexture("Interface\\ChatFrame\\UI-ChatIM-SizeGrabber-Highlight");
ResizeButton:SetPushedTexture("Interface\\ChatFrame\\UI-ChatIM-SizeGrabber-Down");

-- FUNCTIONS : MOUSE SCROLL FRAME RESIZE

local function resizeStart()
	FrameScroll:StartSizing("BOTTOMRIGHT");
	FrameScroll:SetUserPlaced(true);
end
ResizeButton:SetScript("OnMouseDown", resizeStart);

local function resizeStop()
	FrameScroll:StopMovingOrSizing();
end
ResizeButton:SetScript("OnMouseUp", resizeStop);

-- QUEST FRAME

CreateFrame("Frame", "FrameQuest", FrameOptions, "InsetFrameTemplate");
FrameQuest:SetPoint("BOTTOMLEFT", 4, 346);
FrameQuest:SetSize(496, 91);

-- QUEST FRAME BLACK BACKROUND

CreateFrame("Frame", "FrameQuestBackground", FrameQuest);
FrameQuestBackground:SetSize(488, 83);
FrameQuestBackground:SetPoint("TOPLEFT", 4, -4);
FrameQuestBackground.texture = FrameQuestBackground:CreateTexture("ARTWORK");
FrameQuestBackground.texture:SetAllPoints();
FrameQuestBackground.texture:SetTexture(0, 0, 0, 1.0);

-- QUEST EDITBOX

createEditBox("EditBoxQuest", FrameQuest, "");
EditBoxQuest:SetPoint("TOPLEFT", 0, 140);
EditBoxQuest:SetWidth(470);
EditBoxQuest:SetMultiLine();
EditBoxQuest:SetTextInsets(5, 5, 5, 5);
EditBoxQuest:SetTextColor(0.5, 0.5, 0.5, 1.0);
EditBoxQuest.texture:SetTexture(0, 0, 0, 0.0);

-- QUEST SCROLLFRAME

CreateFrame("ScrollFrame", "ScrollFrameQuest", FrameQuest, "UIPanelScrollFrameTemplate");
ScrollFrameQuest:SetPoint("TOPLEFT", 5, -5);
ScrollFrameQuest:SetPoint("BOTTOMRIGHT", -26, 3);
ScrollFrameQuest:SetScrollChild(EditBoxQuest);

-- VOICES FRAME

CreateFrame("Frame", "FrameVoices", FrameOptions, "InsetFrameTemplate");
FrameVoices:SetPoint("BOTTOMLEFT", 4, 108);
FrameVoices:SetSize(496, 237);

-- FEMALE VOICES EDITBOXES AND TITLE 

createEditBox("EditBoxF0", FrameVoices, Female); EditBoxF0:SetPoint("BOTTOMRIGHT", -333, 192);
createEditBox("EditBoxF1", FrameVoices, Female); EditBoxF1:SetPoint("BOTTOMRIGHT", -333, 172);
createEditBox("EditBoxF2", FrameVoices, Female); EditBoxF2:SetPoint("BOTTOMRIGHT", -333, 152);
createEditBox("EditBoxF3", FrameVoices, Female); EditBoxF3:SetPoint("BOTTOMRIGHT", -333, 132);
createEditBox("EditBoxF4", FrameVoices, Female); EditBoxF4:SetPoint("BOTTOMRIGHT", -333, 112);
createEditBox("EditBoxF5", FrameVoices, Female); EditBoxF5:SetPoint("BOTTOMRIGHT", -333, 92);
createEditBox("EditBoxF6", FrameVoices, Female); EditBoxF6:SetPoint("BOTTOMRIGHT", -333, 72);
createEditBox("EditBoxF7", FrameVoices, Female); EditBoxF7:SetPoint("BOTTOMRIGHT", -333, 52);
createEditBox("EditBoxF8", FrameVoices, Female); EditBoxF8:SetPoint("BOTTOMRIGHT", -333, 32); 
createEditBox("EditBoxF9", FrameVoices, Female); EditBoxF9:SetPoint("BOTTOMRIGHT", -333, 12);

EditBoxF0:CreateFontString("TitleFemale", ARTWORK, "GameFontHighlightSmall");
TitleFemale:SetPoint("CENTER", 0, 21);
TitleFemale:SetTextColor(0.5, 0.5, 0.5, 1.0);
TitleFemale:SetText("FEMALE TTS VOICES");

-- MALE VOICES EDITBOXES AND TITLE

createEditBox("EditBoxM0", FrameVoices, Male); EditBoxM0:SetPoint("BOTTOMRIGHT", -173, 192);
createEditBox("EditBoxM1", FrameVoices, Male); EditBoxM1:SetPoint("BOTTOMRIGHT", -173, 172);
createEditBox("EditBoxM2", FrameVoices, Male); EditBoxM2:SetPoint("BOTTOMRIGHT", -173, 152);
createEditBox("EditBoxM3", FrameVoices, Male); EditBoxM3:SetPoint("BOTTOMRIGHT", -173, 132);
createEditBox("EditBoxM4", FrameVoices, Male); EditBoxM4:SetPoint("BOTTOMRIGHT", -173, 112);
createEditBox("EditBoxM5", FrameVoices, Male); EditBoxM5:SetPoint("BOTTOMRIGHT", -173, 92);
createEditBox("EditBoxM6", FrameVoices, Male); EditBoxM6:SetPoint("BOTTOMRIGHT", -173, 72);
createEditBox("EditBoxM7", FrameVoices, Male); EditBoxM7:SetPoint("BOTTOMRIGHT", -173, 52);
createEditBox("EditBoxM8", FrameVoices, Male); EditBoxM8:SetPoint("BOTTOMRIGHT", -173, 32);
createEditBox("EditBoxM9", FrameVoices, Male); EditBoxM9:SetPoint("BOTTOMRIGHT", -173, 12);

EditBoxM0:CreateFontString("TitleMale", ARTWORK, "GameFontHighlightSmall");
TitleMale:SetPoint("CENTER", 0, 21);
TitleMale:SetTextColor(0.5, 0.5, 0.5, 1.0);
TitleMale:SetText("MALE TTS VOICES");

-- UNKNOWN VOICES EDITBOXES AND TITLE 

createEditBox("EditBoxU0", FrameVoices, Unknown); EditBoxU0:SetPoint("BOTTOMRIGHT", -13, 192);
createEditBox("EditBoxU1", FrameVoices, Unknown); EditBoxU1:SetPoint("BOTTOMRIGHT", -13, 172);
createEditBox("EditBoxU2", FrameVoices, Unknown); EditBoxU2:SetPoint("BOTTOMRIGHT", -13, 152);
createEditBox("EditBoxU3", FrameVoices, Unknown); EditBoxU3:SetPoint("BOTTOMRIGHT", -13, 132);
createEditBox("EditBoxU4", FrameVoices, Unknown); EditBoxU4:SetPoint("BOTTOMRIGHT", -13, 112);
createEditBox("EditBoxU5", FrameVoices, Unknown); EditBoxU5:SetPoint("BOTTOMRIGHT", -13, 92);
createEditBox("EditBoxU6", FrameVoices, Unknown); EditBoxU6:SetPoint("BOTTOMRIGHT", -13, 72);
createEditBox("EditBoxU7", FrameVoices, Unknown); EditBoxU7:SetPoint("BOTTOMRIGHT", -13, 52);
createEditBox("EditBoxU8", FrameVoices, Unknown); EditBoxU8:SetPoint("BOTTOMRIGHT", -13, 32);
createEditBox("EditBoxU9", FrameVoices, Unknown); EditBoxU9:SetPoint("BOTTOMRIGHT", -13, 12);

EditBoxU0:CreateFontString("TitleUnknown", ARTWORK, "GameFontHighlightSmall");
TitleUnknown:SetPoint("CENTER", 0, 21);
TitleUnknown:SetTextColor(0.5, 0.5, 0.5, 1.0);
TitleUnknown:SetText("UNKNOWN TTS VOICES");

-- DESCRIPTION FRAME

CreateFrame("Frame", "FrameDescription", FrameOptions, "InsetFrameTemplate");
FrameDescription:SetPoint("BOTTOMLEFT", 4, 27);
FrameDescription:SetSize(708, 80);

-- DESCRIPTION TEXT

FrameDescription:CreateFontString("textDesription", ARTWORK, "GameFontHighlightSmall");
textDesription:SetPoint("TOPLEFT", 10, -9);
textDesription:SetSize(690, 420);
textDesription:SetJustifyH("LEFT");
textDesription:SetJustifyV("TOP");
textDesription:SetTextColor(0.5, 0.5, 0.5, 1.0);
textDesription:SetText(textDescription);

-- NAME FRAME

CreateFrame("Frame", "FrameName", FrameOptions, "InsetFrameTemplate");
FrameName:SetPoint("BOTTOMRIGHT", -4, 346);
FrameName:SetSize(212, 91);

-- NAME TITLE

FrameName:CreateFontString("TitleReplaceName", ARTWORK, "GameFontHighlightSmall");
TitleReplaceName:SetPoint("TOP", 0, -12);
TitleReplaceName:SetTextColor(0.5, 0.5, 0.5, 1.0);
TitleReplaceName:SetText(textReplace);

-- NAME EDITBOX

createEditBox("EditBoxReplaceName", FrameName, unitName);
EditBoxReplaceName:SetPoint("BOTTOM", 0, 12);
EditBoxReplaceName:SetJustifyH("CENTER");
EditBoxReplaceName:SetSize(186, 15);

-- PITCH FRAME

CreateFrame("Frame", "FramePitch", FrameOptions, "InsetFrameTemplate");
FramePitch:SetPoint("BOTTOMRIGHT", -4, 108);
FramePitch:SetSize(212, 237);

-- PITCH TITLE

FramePitch:CreateFontString("TitlepPitch", ARTWORK, "GameFontHighlightSmall");
TitlepPitch:SetPoint("TOP", 9, -12);
TitlepPitch:SetTextColor(0.5, 0.5, 0.5, 1.0);
TitlepPitch:SetText("POSITIVE ... PITCH ... NEGATIVE");

-- PITCH POSITIVE EDITBOXES

createEditBox2("EditBoxP0", FramePitch, "0"); EditBoxP0:SetPoint("BOTTOMRIGHT", -103, 192);
createEditBox2("EditBoxP1", FramePitch, "1"); EditBoxP1:SetPoint("BOTTOMRIGHT", -103, 172);
createEditBox2("EditBoxP2", FramePitch, "2"); EditBoxP2:SetPoint("BOTTOMRIGHT", -103, 152);
createEditBox2("EditBoxP3", FramePitch, "3"); EditBoxP3:SetPoint("BOTTOMRIGHT", -103, 132);
createEditBox2("EditBoxP4", FramePitch, "4"); EditBoxP4:SetPoint("BOTTOMRIGHT", -103, 112);
createEditBox2("EditBoxP5", FramePitch, "5"); EditBoxP5:SetPoint("BOTTOMRIGHT", -103, 92);
createEditBox2("EditBoxP6", FramePitch, "6"); EditBoxP6:SetPoint("BOTTOMRIGHT", -103, 72);
createEditBox2("EditBoxP7", FramePitch, "7"); EditBoxP7:SetPoint("BOTTOMRIGHT", -103, 52);
createEditBox2("EditBoxP8", FramePitch, "8"); EditBoxP8:SetPoint("BOTTOMRIGHT", -103, 32);
createEditBox2("EditBoxP9", FramePitch, "9"); EditBoxP9:SetPoint("BOTTOMRIGHT", -103, 12);

-- PITCH NEGATIVE EDITBOXES

createEditBox2("EditBoxN0", FramePitch, "0");  EditBoxN0:SetPoint("BOTTOMRIGHT", -13, 192);
createEditBox2("EditBoxN1", FramePitch, "-1"); EditBoxN1:SetPoint("BOTTOMRIGHT", -13, 172);
createEditBox2("EditBoxN2", FramePitch, "-2"); EditBoxN2:SetPoint("BOTTOMRIGHT", -13, 152);
createEditBox2("EditBoxN3", FramePitch, "-3"); EditBoxN3:SetPoint("BOTTOMRIGHT", -13, 132);
createEditBox2("EditBoxN4", FramePitch, "-4"); EditBoxN4:SetPoint("BOTTOMRIGHT", -13, 112);
createEditBox2("EditBoxN5", FramePitch, "-5"); EditBoxN5:SetPoint("BOTTOMRIGHT", -13, 92);
createEditBox2("EditBoxN6", FramePitch, "-6"); EditBoxN6:SetPoint("BOTTOMRIGHT", -13, 72);
createEditBox2("EditBoxN7", FramePitch, "-7"); EditBoxN7:SetPoint("BOTTOMRIGHT", -13, 52);
createEditBox2("EditBoxN8", FramePitch, "-8"); EditBoxN8:SetPoint("BOTTOMRIGHT", -13, 32);
createEditBox2("EditBoxN9", FramePitch, "-9"); EditBoxN9:SetPoint("BOTTOMRIGHT", -13, 12);

-- PITCH TITLES

EditBoxP0:CreateFontString("TitlePitch0", ARTWORK, "GameFontHighlightSmall"); TitlePitch0:SetPoint("LEFT", -16, 0); TitlePitch0:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch0:SetText("0");
EditBoxP1:CreateFontString("TitlePitch1", ARTWORK, "GameFontHighlightSmall"); TitlePitch1:SetPoint("LEFT", -16, 0); TitlePitch1:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch1:SetText("1");
EditBoxP2:CreateFontString("TitlePitch2", ARTWORK, "GameFontHighlightSmall"); TitlePitch2:SetPoint("LEFT", -16, 0); TitlePitch2:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch2:SetText("2");
EditBoxP3:CreateFontString("TitlePitch3", ARTWORK, "GameFontHighlightSmall"); TitlePitch3:SetPoint("LEFT", -16, 0); TitlePitch3:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch3:SetText("3");
EditBoxP4:CreateFontString("TitlePitch4", ARTWORK, "GameFontHighlightSmall"); TitlePitch4:SetPoint("LEFT", -16, 0); TitlePitch4:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch4:SetText("4");
EditBoxP5:CreateFontString("TitlePitch5", ARTWORK, "GameFontHighlightSmall"); TitlePitch5:SetPoint("LEFT", -16, 0); TitlePitch5:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch5:SetText("5");
EditBoxP6:CreateFontString("TitlePitch6", ARTWORK, "GameFontHighlightSmall"); TitlePitch6:SetPoint("LEFT", -16, 0); TitlePitch6:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch6:SetText("6");
EditBoxP7:CreateFontString("TitlePitch7", ARTWORK, "GameFontHighlightSmall"); TitlePitch7:SetPoint("LEFT", -16, 0); TitlePitch7:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch7:SetText("7");
EditBoxP8:CreateFontString("TitlePitch8", ARTWORK, "GameFontHighlightSmall"); TitlePitch8:SetPoint("LEFT", -16, 0); TitlePitch8:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch8:SetText("8");
EditBoxP9:CreateFontString("TitlePitch9", ARTWORK, "GameFontHighlightSmall"); TitlePitch9:SetPoint("LEFT", -16, 0); TitlePitch9:SetTextColor(0.5, 0.5, 0.5, 1.0); TitlePitch9:SetText("9");

-- DEFAULTS BUTTON

CreateFrame("Button", "ButtonDefault", FrameOptions, "UIPanelButtonTemplate");
ButtonDefault:SetPoint("BOTTOMRIGHT", -4, 4);
ButtonDefault:SetText(textDefaults);
ButtonDefault:SetWidth(303);

-- FUNCTION : RESET / DEFAULT VALUES

local function defaultFunction()
	EditBoxU0:SetText(Unknown);
	EditBoxU1:SetText(Unknown);
	EditBoxU2:SetText(Unknown);
	EditBoxU3:SetText(Unknown);
	EditBoxU4:SetText(Unknown);
	EditBoxU5:SetText(Unknown);
	EditBoxU6:SetText(Unknown);
	EditBoxU7:SetText(Unknown);
	EditBoxU8:SetText(Unknown);
	EditBoxU9:SetText(Unknown);
	EditBoxM0:SetText(Male);
	EditBoxM1:SetText(Male);
	EditBoxM2:SetText(Male);
	EditBoxM3:SetText(Male);
	EditBoxM4:SetText(Male);
	EditBoxM5:SetText(Male);
	EditBoxM6:SetText(Male);
	EditBoxM7:SetText(Male);
	EditBoxM8:SetText(Male);
	EditBoxM9:SetText(Male);
	EditBoxF0:SetText(Female);
	EditBoxF1:SetText(Female);
	EditBoxF2:SetText(Female);
	EditBoxF3:SetText(Female);
	EditBoxF4:SetText(Female);
	EditBoxF5:SetText(Female);
	EditBoxF6:SetText(Female);
	EditBoxF7:SetText(Female);
	EditBoxF8:SetText(Female);
	EditBoxF9:SetText(Female);
	EditBoxP0:SetText("0");
	EditBoxP1:SetText("1");
	EditBoxP2:SetText("2");
	EditBoxP3:SetText("3");
	EditBoxP4:SetText("4");
	EditBoxP5:SetText("5");
	EditBoxP6:SetText("6");
	EditBoxP7:SetText("7");
	EditBoxP8:SetText("8");
	EditBoxP9:SetText("9");
	EditBoxN0:SetText("0");
	EditBoxN1:SetText("-1");
	EditBoxN2:SetText("-2");
	EditBoxN3:SetText("-3");
	EditBoxN4:SetText("-4");
	EditBoxN5:SetText("-5");
	EditBoxN6:SetText("-6");
	EditBoxN7:SetText("-7");
	EditBoxN8:SetText("-8");
	EditBoxN9:SetText("-9");
	EditBoxReplaceName:SetText(unitName);

end
ButtonDefault:SetScript("OnClick", defaultFunction);

-- FUNCTIONS FOR ALL EVENTS
local function eventFunction(self, event)

		-- FUNCTION ON ADDON_LOADED
	
	if event == "ADDON_LOADED" then
	if CheckButtonEnableValue ~= 1 then CheckButtonEnableValue = nil end; CheckButtonEnable:SetChecked(CheckButtonEnableValue); 
	if CheckButtonAutoValue ~= 1 then CheckButtonAutoValue = nil end; CheckButtonAuto:SetChecked(CheckButtonAutoValue); 
	if CheckButtonDNDValue ~= 1 then CheckButtonDNDValue = nil end; CheckButtonDND:SetChecked(CheckButtonDNDValue);
	if CheckButtonFilterValue ~= 1 then CheckButtonFilterValue = nil end; CheckButtonFilter:SetChecked(CheckButtonFilterValue); 
	if CharacterFirstLoginValue ~= 1 then
		CheckButtonEnable:SetChecked(1); 
		CheckButtonAuto:SetChecked(1);
		CheckButtonDND:SetChecked(1);
		CheckButtonFilter:SetChecked(1);
	end
	if EditBoxReplaceNameValue == "" or EditBoxReplaceNameValue == nil then EditBoxReplaceNameValue = unitName end; EditBoxReplaceName:SetText(EditBoxReplaceNameValue);
	if EditBoxF0Value == "" or EditBoxF0Value == nil then EditBoxF0Value = Female end; EditBoxF0:SetText(EditBoxF0Value);
	if EditBoxF1Value == "" or EditBoxF1Value == nil then EditBoxF1Value = Female end; EditBoxF1:SetText(EditBoxF1Value);
	if EditBoxF2Value == "" or EditBoxF2Value == nil then EditBoxF2Value = Female end; EditBoxF2:SetText(EditBoxF2Value);
	if EditBoxF3Value == "" or EditBoxF3Value == nil then EditBoxF3Value = Female end; EditBoxF3:SetText(EditBoxF3Value);
	if EditBoxF4Value == "" or EditBoxF4Value == nil then EditBoxF4Value = Female end; EditBoxF4:SetText(EditBoxF4Value);
	if EditBoxF5Value == "" or EditBoxF5Value == nil then EditBoxF5Value = Female end; EditBoxF5:SetText(EditBoxF5Value);
	if EditBoxF6Value == "" or EditBoxF6Value == nil then EditBoxF6Value = Female end; EditBoxF6:SetText(EditBoxF6Value);
	if EditBoxF7Value == "" or EditBoxF7Value == nil then EditBoxF7Value = Female end; EditBoxF7:SetText(EditBoxF7Value);
	if EditBoxF8Value == "" or EditBoxF8Value == nil then EditBoxF8Value = Female end; EditBoxF8:SetText(EditBoxF8Value);
	if EditBoxF9Value == "" or EditBoxF9Value == nil then EditBoxF9Value = Female end; EditBoxF9:SetText(EditBoxF9Value);
	if EditBoxM0Value == "" or EditBoxM0Value == nil then EditBoxM0Value = Male end; EditBoxM0:SetText(EditBoxM0Value);
	if EditBoxM1Value == "" or EditBoxM1Value == nil then EditBoxM1Value = Male end; EditBoxM1:SetText(EditBoxM1Value);
	if EditBoxM2Value == "" or EditBoxM2Value == nil then EditBoxM2Value = Male end; EditBoxM2:SetText(EditBoxM2Value);
	if EditBoxM3Value == "" or EditBoxM3Value == nil then EditBoxM3Value = Male end; EditBoxM3:SetText(EditBoxM3Value);
	if EditBoxM4Value == "" or EditBoxM4Value == nil then EditBoxM4Value = Male end; EditBoxM4:SetText(EditBoxM4Value);
	if EditBoxM5Value == "" or EditBoxM5Value == nil then EditBoxM5Value = Male end; EditBoxM5:SetText(EditBoxM5Value);
	if EditBoxM6Value == "" or EditBoxM6Value == nil then EditBoxM6Value = Male end; EditBoxM6:SetText(EditBoxM6Value);
	if EditBoxM7Value == "" or EditBoxM7Value == nil then EditBoxM7Value = Male end; EditBoxM7:SetText(EditBoxM7Value);
	if EditBoxM8Value == "" or EditBoxM8Value == nil then EditBoxM8Value = Male end; EditBoxM8:SetText(EditBoxM8Value);
	if EditBoxM9Value == "" or EditBoxM9Value == nil then EditBoxM9Value = Male end; EditBoxM9:SetText(EditBoxM9Value);
	if EditBoxU0Value == "" or EditBoxU0Value == nil then EditBoxU0Value = Unknown end; EditBoxU0:SetText(EditBoxU0Value);
	if EditBoxU1Value == "" or EditBoxU1Value == nil then EditBoxU1Value = Unknown end; EditBoxU1:SetText(EditBoxU1Value);
	if EditBoxU2Value == "" or EditBoxU2Value == nil then EditBoxU2Value = Unknown end; EditBoxU2:SetText(EditBoxU2Value);
	if EditBoxU3Value == "" or EditBoxU3Value == nil then EditBoxU3Value = Unknown end; EditBoxU3:SetText(EditBoxU3Value);
	if EditBoxU4Value == "" or EditBoxU4Value == nil then EditBoxU4Value = Unknown end; EditBoxU4:SetText(EditBoxU4Value);
	if EditBoxU5Value == "" or EditBoxU5Value == nil then EditBoxU5Value = Unknown end; EditBoxU5:SetText(EditBoxU5Value);
	if EditBoxU6Value == "" or EditBoxF6Value == nil then EditBoxU6Value = Unknown end; EditBoxU6:SetText(EditBoxU6Value);
	if EditBoxU7Value == "" or EditBoxU7Value == nil then EditBoxU7Value = Unknown end; EditBoxU7:SetText(EditBoxU7Value);
	if EditBoxU8Value == "" or EditBoxU8Value == nil then EditBoxU8Value = Unknown end; EditBoxU8:SetText(EditBoxU8Value);
	if EditBoxU9Value == "" or EditBoxU9Value == nil then EditBoxU9Value = Unknown end; EditBoxU9:SetText(EditBoxU9Value);
	if EditBoxP0Value == "" or EditBoxP0Value == nil then EditBoxP0Value = "0" end; EditBoxP0:SetText(EditBoxP0Value);
	if EditBoxP1Value == "" or EditBoxP1Value == nil then EditBoxP1Value = "1" end; EditBoxP1:SetText(EditBoxP1Value);
	if EditBoxP2Value == "" or EditBoxP2Value == nil then EditBoxP2Value = "2" end; EditBoxP2:SetText(EditBoxP2Value);
	if EditBoxP3Value == "" or EditBoxP3Value == nil then EditBoxP3Value = "3" end; EditBoxP3:SetText(EditBoxP3Value);
	if EditBoxP4Value == "" or EditBoxP4Value == nil then EditBoxP4Value = "4" end; EditBoxP4:SetText(EditBoxP4Value);
	if EditBoxP5Value == "" or EditBoxP5Value == nil then EditBoxP5Value = "5" end; EditBoxP5:SetText(EditBoxP5Value);
	if EditBoxP6Value == "" or EditBoxP6Value == nil then EditBoxP6Value = "6" end; EditBoxP6:SetText(EditBoxP6Value);
	if EditBoxP7Value == "" or EditBoxP7Value == nil then EditBoxP7Value = "7" end; EditBoxP7:SetText(EditBoxP7Value);
	if EditBoxP8Value == "" or EditBoxP8Value == nil then EditBoxP8Value = "8" end; EditBoxP8:SetText(EditBoxP8Value);
	if EditBoxP9Value == "" or EditBoxP9Value == nil then EditBoxP9Value = "9" end; EditBoxP9:SetText(EditBoxP9Value);
	if EditBoxN0Value == "" or EditBoxN0Value == nil then EditBoxN0Value = "0" end; EditBoxN0:SetText(EditBoxN0Value);
	if EditBoxN1Value == "" or EditBoxN1Value == nil then EditBoxN1Value = "-1" end; EditBoxN1:SetText(EditBoxN1Value);
	if EditBoxN2Value == "" or EditBoxN2Value == nil then EditBoxN2Value = "-2" end; EditBoxN2:SetText(EditBoxN2Value);
	if EditBoxN3Value == "" or EditBoxN3Value == nil then EditBoxN3Value = "-3" end; EditBoxN3:SetText(EditBoxN3Value);
	if EditBoxN4Value == "" or EditBoxN4Value == nil then EditBoxN4Value = "-4" end; EditBoxN4:SetText(EditBoxN4Value);
	if EditBoxN5Value == "" or EditBoxN5Value == nil then EditBoxN5Value = "-5" end; EditBoxN5:SetText(EditBoxN5Value);
	if EditBoxN6Value == "" or EditBoxN6Value == nil then EditBoxN6Value = "-6" end; EditBoxN6:SetText(EditBoxN6Value);
	if EditBoxN7Value == "" or EditBoxN7Value == nil then EditBoxN7Value = "-7" end; EditBoxN7:SetText(EditBoxN7Value);
	if EditBoxN8Value == "" or EditBoxN8Value == nil then EditBoxN8Value = "-8" end; EditBoxN8:SetText(EditBoxN8Value);
	if EditBoxN9Value == "" or EditBoxN9Value == nil then EditBoxN9Value = "-9" end; EditBoxN9:SetText(EditBoxN9Value);
		FrameMini:SetMovable(true);
		FrameMini:SetUserPlaced(true);
		FrameOptions:SetMovable(true);
		FrameOptions:SetUserPlaced(true);
		FrameScroll:SetMovable(true);
		FrameScroll:SetResizable(true);
		FrameScroll:SetUserPlaced(true);
	end

	
	-- FUNCTION SAVE VALUES (ON OPTIONS CLOSE AND LOGOUT)
	function saveValues()
		CheckButtonEnableValue = CheckButtonEnable:GetChecked();
		CheckButtonAutoValue = CheckButtonAuto:GetChecked();
		CheckButtonDNDValue = CheckButtonDND:GetChecked();
		CheckButtonFilterValue = CheckButtonFilter:GetChecked();
		EditBoxReplaceNameValue = EditBoxReplaceName:GetText();
		EditBoxF0Value = EditBoxF0:GetText(); 
		EditBoxF1Value = EditBoxF1:GetText();
		EditBoxF2Value = EditBoxF2:GetText();
		EditBoxF3Value = EditBoxF3:GetText();
		EditBoxF4Value = EditBoxF4:GetText();
		EditBoxF5Value = EditBoxF5:GetText();
		EditBoxF6Value = EditBoxF6:GetText();
		EditBoxF7Value = EditBoxF7:GetText();
		EditBoxF8Value = EditBoxF8:GetText();
		EditBoxF9Value = EditBoxF9:GetText();
		EditBoxM0Value = EditBoxM0:GetText();
		EditBoxM1Value = EditBoxM1:GetText();
		EditBoxM2Value = EditBoxM2:GetText();
		EditBoxM3Value = EditBoxM3:GetText();
		EditBoxM4Value = EditBoxM4:GetText();
		EditBoxM5Value = EditBoxM5:GetText();
		EditBoxM6Value = EditBoxM6:GetText();
		EditBoxM7Value = EditBoxM7:GetText();
		EditBoxM8Value = EditBoxM8:GetText();
		EditBoxM9Value = EditBoxM9:GetText();
		EditBoxU0Value = EditBoxU0:GetText();
		EditBoxU1Value = EditBoxU1:GetText();
		EditBoxU2Value = EditBoxU2:GetText();
		EditBoxU3Value = EditBoxU3:GetText();
		EditBoxU4Value = EditBoxU4:GetText();
		EditBoxU5Value = EditBoxU5:GetText();
		EditBoxU6Value = EditBoxU6:GetText();
		EditBoxU7Value = EditBoxU7:GetText();
		EditBoxU8Value = EditBoxU8:GetText();
		EditBoxU9Value = EditBoxU9:GetText();
		EditBoxP0Value = EditBoxP0:GetText(); 
		EditBoxP1Value = EditBoxP1:GetText();
		EditBoxP2Value = EditBoxP2:GetText();
		EditBoxP3Value = EditBoxP3:GetText();
		EditBoxP4Value = EditBoxP4:GetText();
		EditBoxP5Value = EditBoxP5:GetText();
		EditBoxP6Value = EditBoxP6:GetText();
		EditBoxP7Value = EditBoxP7:GetText();
		EditBoxP8Value = EditBoxP8:GetText();
		EditBoxP9Value = EditBoxP9:GetText();
		EditBoxN0Value = EditBoxN0:GetText();
		EditBoxN1Value = EditBoxN1:GetText();
		EditBoxN2Value = EditBoxN2:GetText();
		EditBoxN3Value = EditBoxN3:GetText();
		EditBoxN4Value = EditBoxN4:GetText();
		EditBoxN5Value = EditBoxN5:GetText();
		EditBoxN6Value = EditBoxN6:GetText();
		EditBoxN7Value = EditBoxN7:GetText();
		EditBoxN8Value = EditBoxN8:GetText();
		EditBoxN9Value = EditBoxN9:GetText();
		CharacterFirstLoginValue = 1;
		end

	-- FUNCTION MESSAGE STOP

	local function sendChatMessageStop()
		if framesClosed == 1 then	
			-- Disable Do Not Disturb
			if CheckButtonDND:GetChecked() == 1 then
			if UnitIsDND("player") == 1 then
			SendChatMessage("", "DND");
			end
			end
			-- Send messageStop
			SendChatMessage(messageStop, "WHISPER", "Common", unitName);
			EditBoxQuest:SetText(messageStop);
			EditBoxQuest:HighlightText();
			EditBoxMini:SetText(messageStop); 
			EditBoxMini:HighlightText();
		end
		framesClosed = 0;
	end

	-- FUNCTION ENABLE or DISABLE OPTIONS

	local function showFrames()
		FrameOptions:Show();
		ButtonOptions:SetText("Minimize");
		FrameMini:SetMovable(true);
		FrameMini.texture:SetTexture(0, 0, 0, 0.8);
		FrameScroll:SetMovable(true);
		FrameScroll:SetResizable(true);
		FrameScroll:EnableMouse(true);
		FrameScroll.texture:SetTexture(0, 0, 0, 0.8);
		FrameScroll:Show();
		TextScroll:Show();
		ResizeButton:Show();
	end

	local function hideFrames()
		FrameOptions:Hide();
		FrameMini:SetMovable(false);
		FrameMini.texture:SetTexture(0, 0, 0, 0);
		ButtonOptions:SetText("Options");
		FrameScroll:SetMovable(false);
		FrameScroll:SetResizable(false);
		FrameScroll:EnableMouse(false);
		FrameScroll.texture:SetTexture(0, 0, 0, 0);
		if CheckButtonEnable:GetChecked() ~= 1 then
			FrameScroll:Hide();
		end
		TextScroll:Hide();
		ResizeButton:Hide();
	end

	local function functionOptions()
		if FrameOptions:IsShown() then
			saveValues();
			hideFrames();
		else
			showFrames();
		end
	end
	ButtonOptions:SetScript("OnClick", functionOptions);
	FrameOptions:SetScript("OnHide", hideFrames);
	FrameOptions:SetScript("OnShow", showFrames);

	-- FUNCTION ENABLE or DISABLE TTS

	local function functionClose()
		hideFrames();
		CloseQuest();
		CloseGossip();
		sendChatMessageStop();
	end

	-- FUNCTION ON ESCAPE

	EditBoxU0:SetScript("OnEscapePressed", functionClose);
	EditBoxU1:SetScript("OnEscapePressed", functionClose);
	EditBoxU2:SetScript("OnEscapePressed", functionClose);
	EditBoxU3:SetScript("OnEscapePressed", functionClose);
	EditBoxU4:SetScript("OnEscapePressed", functionClose);
	EditBoxU5:SetScript("OnEscapePressed", functionClose);
	EditBoxU6:SetScript("OnEscapePressed", functionClose);
	EditBoxU7:SetScript("OnEscapePressed", functionClose);
	EditBoxU8:SetScript("OnEscapePressed", functionClose);
	EditBoxU9:SetScript("OnEscapePressed", functionClose);
	EditBoxM0:SetScript("OnEscapePressed", functionClose);
	EditBoxM1:SetScript("OnEscapePressed", functionClose);
	EditBoxM2:SetScript("OnEscapePressed", functionClose);
	EditBoxM3:SetScript("OnEscapePressed", functionClose);
	EditBoxM4:SetScript("OnEscapePressed", functionClose);
	EditBoxM5:SetScript("OnEscapePressed", functionClose);
	EditBoxM6:SetScript("OnEscapePressed", functionClose);
	EditBoxM7:SetScript("OnEscapePressed", functionClose);
	EditBoxM8:SetScript("OnEscapePressed", functionClose);
	EditBoxM9:SetScript("OnEscapePressed", functionClose);
	EditBoxF0:SetScript("OnEscapePressed", functionClose);
	EditBoxF1:SetScript("OnEscapePressed", functionClose);
	EditBoxF2:SetScript("OnEscapePressed", functionClose);
	EditBoxF3:SetScript("OnEscapePressed", functionClose);
	EditBoxF4:SetScript("OnEscapePressed", functionClose);
	EditBoxF5:SetScript("OnEscapePressed", functionClose);
	EditBoxF6:SetScript("OnEscapePressed", functionClose);
	EditBoxF7:SetScript("OnEscapePressed", functionClose);
	EditBoxF8:SetScript("OnEscapePressed", functionClose);
	EditBoxF9:SetScript("OnEscapePressed", functionClose);
	EditBoxP0:SetScript("OnEscapePressed", functionClose);
	EditBoxP1:SetScript("OnEscapePressed", functionClose);
	EditBoxP2:SetScript("OnEscapePressed", functionClose);
	EditBoxP3:SetScript("OnEscapePressed", functionClose);
	EditBoxP4:SetScript("OnEscapePressed", functionClose);
	EditBoxP5:SetScript("OnEscapePressed", functionClose);
	EditBoxP6:SetScript("OnEscapePressed", functionClose);
	EditBoxP7:SetScript("OnEscapePressed", functionClose);
	EditBoxP8:SetScript("OnEscapePressed", functionClose);
	EditBoxP9:SetScript("OnEscapePressed", functionClose);
	EditBoxN0:SetScript("OnEscapePressed", functionClose);
	EditBoxN1:SetScript("OnEscapePressed", functionClose);
	EditBoxN2:SetScript("OnEscapePressed", functionClose);
	EditBoxN3:SetScript("OnEscapePressed", functionClose);
	EditBoxN4:SetScript("OnEscapePressed", functionClose);
	EditBoxN5:SetScript("OnEscapePressed", functionClose);
	EditBoxN6:SetScript("OnEscapePressed", functionClose);
	EditBoxN7:SetScript("OnEscapePressed", functionClose);
	EditBoxN8:SetScript("OnEscapePressed", functionClose);
	EditBoxN9:SetScript("OnEscapePressed", functionClose);
	EditBoxMini:SetScript("OnEscapePressed", functionClose);
	EditBoxQuest:SetScript("OnEscapePressed", functionClose);
	EditBoxReplaceName:SetScript("OnEscapePressed", functionClose);

	-- FUNCTION CLEAR

	local function functionReset()
		greeting = "";
		gossip = "";
		quest = "";
		objective = "";
		progress = "";
		reward = "";
		output = ""
		EditBoxQuest:SetText("");
		EditBoxMini:SetText("");
	end

	-- FUNCTION SET COLOR

	local function setColor(parameter, frameName, color)
		local frame = EnumerateFrames(); -- Get the first frame
		while frame do
			if frame:GetName() == frameName then
				if color == 2 then
					frame:SetTextColor(0.0, 1.0, 0.0, 1.0); -- green (2)
				else
					frame:SetTextColor(1.0, 1.0, 1.0, 1.0); -- white (1)
				end
				if parameter == "voice" then
					voiceName = frame:GetText();
					voiceFrameNameDisable =  frameName;
				else
					
					-- SET PITCH AND COMMENT PITCH
					
					pitchName = frame:GetText();
					pitchNameL = string.sub(pitchName, 1, 1);
					if pitchNameL == "-" then
					pitchChange = 10;
					else
					pitchChange = -10;
					end
					pitchComment = pitchName + pitchChange;

					pitchFrameNameDisable = frameName;
				end
				break;
			end
			frame = EnumerateFrames(frame); -- Get the next frame
		end
	end

	-- FUNCTION ENABLE / DISABLE TTS

	local function functionEnable()
		if CheckButtonEnable:GetChecked() == 1 then
			CheckButtonFilter:SetChecked(1);
			CheckButtonAuto:SetChecked(1);
			CheckButtonDND:SetChecked(1);
			functionClose();
			functionReset();
		else
			CheckButtonAuto:SetChecked(nil);
			-- Disable Do Not Disturb
			if CheckButtonDND:GetChecked() == 1 then
			if UnitIsDND("player") == 1 then
			SendChatMessage("", "DND");
			end
			end
			CheckButtonDND:SetChecked(nil);
			setColor("voice", voiceFrameNameDisable, 1); -- green (1)
			setColor("pitch", pitchFrameNameDisable, 1); -- green (1)			
			sendChatMessageStop();
			functionReset();
		end
	end
	CheckButtonEnable:SetScript("OnClick", functionEnable);

	-- FUNCTION ENABLE / DISABLE AUTO SPEECH

	local function functionAuto()
		if CheckButtonAuto:GetChecked() == 1 then
			if CheckButtonEnable:GetChecked() ~= 1 then
				CheckButtonEnable:SetChecked(1);
				functionClose();
				functionReset();
			end
		end
	end
	CheckButtonAuto:SetScript("OnClick", functionAuto);

		-- FUNCTION ENABLE / DISABLE AUTO DO NOT DISTURB

	local function functionDND()
		if CheckButtonDND:GetChecked() == 1 then
			if UnitIsDND("player") == nil then
			if framesClosed == 1 then
			SendChatMessage("<" .. unitName .. ">: " .. textDoNotDisturb, "DND");
			end
			end
		else
			if UnitIsDND("player") == 1 then
			SendChatMessage("", "DND");
			end
		end
	end
	CheckButtonDND:SetScript("OnClick", functionDND);

	-- SET VALUE IF FRAMES ARE VISIBLE (1) OR NOT (0)

	visibleGossip = GossipFrame:IsShown();
	visibleQuest = QuestFrame:IsShown();
	if visibleGossip == 0 or visibleGossip == "" or visibleGossip == nil then
		visibleGossip = 0;
	else
		visibleGossip = 1;
	end
	if visibleQuest == 0 or visibleQuest == "" or visibleQuest == nil then
		visibleQuest = 0;
	else
		visibleQuest = 1;
	end
	visibleFrames = visibleGossip + visibleQuest;

	-- FRAMES HIDE

	if event == "QUEST_ACCEPTED" or event == "QUEST_FINISHED" or event == "GOSSIP_CLOSED" then
	
		if visibleFrames == 0 then
			FrameMini:Hide();
			FrameScroll:Hide();
			hideFrames();
			functionReset();
			if CheckButtonEnable:GetChecked() == 1 then
				sendChatMessageStop();
			end
		end

	end

	-- FRAMES SHOW

	if event == "QUEST_GREETING" or event == "GOSSIP_SHOW" or event == "QUEST_DETAIL" or event == "QUEST_PROGRESS" or event == "QUEST_COMPLETE" then
	
		FrameMini:Show();	

		if CheckButtonEnable:GetChecked() == 1 then

			FrameScroll:Show();
			functionReset();

			-- FUNCTON SET VOICE AND PITCH FRAME

			local function setVoice()
				unitGUID = UnitGUID("target");
				if unitGUID == nil then unitGUID = "12345678901234567890" end;
				targetGUIDNumber = tonumber((unitGUID):sub(-12, -9), 16);

				targetClass = UnitClass("target");
				if targetClass == nil then targetClass = "nil" end;

				targetClassification = UnitClassification("target"); -- worldboss, rareelite, elite, rare, normal, trivial or minus
				if targetClassification == nil then targetClassification = "nil" end;

				targetName = GetUnitName("target");
				if targetName == nil then targetName = "nil" end;

				targetFaction = UnitFactionGroup("target"); -- Horde, Alliance, Neutral
				if targetFaction == nil then targetFaction = "nil" end;

				targetCreature = UnitCreatureType("target"); -- Beast, Dragonkin, Demon, Elemental, Giant, Undead, Humanoid, Critter, Mechanical, Not specified, Totem, Non-combat Pet, Gas Cloud							
				if targetCreature == nil then targetCreature = "Default" end;

				targetSex = UnitSex("target");
				if targetSex == nil then targetSex = 1 end;
				
				if targetSex == 3 then 
					sex = "Female";
					sexFrame = "EditBoxF"; 
				elseif targetSex == 2 then
					sex = "Male";
					sexFrame = "EditBoxM"; 
				else
					sex = "Unknown";
					sexFrame = "EditBoxU"; 
				end
	
				targetInfo = "GUID: " .. unitGUID .. " (" .. targetGUIDNumber .. "); \nFACTION: " .. targetFaction .. "; \nCLASSIFICATION: " .. targetClassification .. "; \nCREATURE: " .. targetCreature ..  " (effect); \nNAME: " .. targetName .. "; \nGENDER: " .. sex .. " (" .. targetSex .. "); \nCLASS: " .. targetClass .. "; \n\n";
								
				--print(targetInfo);
				
				targetInfoLen = string.len(targetInfo);
				targetInfoLenR = string.sub(targetInfoLen, -1); -- Voice number

			    targetNumber = string.sub(targetGUIDNumber + targetInfoLen, -2); 
				targetNumberL = tonumber(string.sub(targetNumber, 1, 1));
				targetNumberR = tonumber(string.sub(targetNumber, -1));
				targetPitch = string.sub(targetNumberL + targetNumberR, -1); -- Pitch number

				pitchFrameName = "EditBoxP";
				if (targetGUIDNumber % 2 ~= 0) and targetPitch ~= 0 then -- Pitch is negative if targetGUIDNumber is add number and not 0
					pitchFrameName = "EditBoxN";
				end

				voiceFrameNameNew = sexFrame .. targetInfoLenR; -- Voice frame name and number
				pitchFrameNameNew = pitchFrameName .. targetPitch;  -- Pitch frame name and number

				--voiceAndPitch = targetInfo .. "INFO_LEN: " .. targetInfoLen .. " (" .. targetInfoLenR.. ") - last number is voice number; \nGUID_NUMBER: " .. targetGUIDNumber .. " - even is positive, add is negative PITCH; \nGUID_NUM + INFO_LEN = (SUM OF LAST 2 NUMBERS) = (R) = PITCH_NUMBER: " .. targetPitch .. ";\n\n";			
			
			end
			setVoice();		

			-- RESET / SET VOICE AND PITCH COLOR

			local function resetSetColor()
				if voiceFrameNameOld == nil then
					voiceFrameNameOld = voiceFrameNameNew;
				end
				if pitchFrameNameOld == nil then
					pitchFrameNameOld = pitchFrameNameNew;
				end
				setColor("voice", voiceFrameNameOld, 1); -- white
				setColor("pitch", pitchFrameNameOld, 1); -- white
				setColor("voice", voiceFrameNameNew, 2); -- green
				setColor("pitch", pitchFrameNameNew, 2); -- green
				voiceFrameNameOld = voiceFrameNameNew;
				pitchFrameNameOld = pitchFrameNameNew;
			end
			resetSetColor();

			-- GET QUEST TEXT BY EVENT

			local function outputFromEvent()
				if event == "QUEST_GREETING" then
					output = GetGreetingText();
				end
				if event == "GOSSIP_SHOW" then
					output = GetGossipText();
				end
				if event == "QUEST_DETAIL" then
					output = GetQuestText() .. " Your objective is to " .. GetObjectiveText();
				end
				if event == "QUEST_PROGRESS" then
					output = GetProgressText();
				end
				if event == "QUEST_COMPLETE" then
					output = GetRewardText();
				end
				
				-- REPLACE NAME
				
				EditBoxReplaceNameTo = EditBoxReplaceName:GetText();
				if string.len(EditBoxReplaceNameTo) > 0 and EditBoxReplaceNameTo ~= unitName then
					output = string.gsub(output, unitName, EditBoxReplaceNameTo);
				end		
				
				-- FORMAT AND REPLACE TEXT

				output = string.gsub(output, "\r", " ");
				output = string.gsub(output, "\n", " ");
				output = string.gsub(output, "[%.]+", ". ");
				output = string.gsub(output, "<", " &lt;pitch absmiddle=\"" .. pitchComment .. "\"&gt;");
				output = string.gsub(output, ">", "&lt;/pitch&gt; ");
				output = string.gsub(output, "[ ]+", " ");

				outputMessage = output;

			end
			outputFromEvent();

			-- FUNCTION : FORMAT AND SEND WHISPERS
			
			local function sendPart(part, command)
				chatMessageS = "<voice name=\"" .. voiceName .. "\" pitch=\"" .. pitchName .. "\" effect=\"" .. targetCreature .. "\" command=\"" .. command .. "\"><part>";
				chatMessageE = "</part></voice>";
				local chatMessage = chatMessageS .. part .. chatMessageE;
				SendChatMessage(chatMessage, "WHISPER", "Common", unitName);
			end

			local function messageSpeak(sender)
				if (sender == "Scroll") or (sender == "Auto" and CheckButtonAuto:GetChecked() == 1) then

					-- Disable Do Not Disturb
					if CheckButtonDND:GetChecked() == 1 then
					if UnitIsDND("player") == 1 then
					SendChatMessage("", "DND");
					end
					end

					framesClosed = 1;
					local size = 150;
					local startIndex = 1;
					local endIndex = 1;
					local part = "";
					while true do
						local command = "";
						local index = string.find(outputMessage, " ", endIndex);
						--print(index);
						-- if nothing found then...
						if index == nil then
							part = string.sub(outputMessage, startIndex);
							sendPart(part, "play");
							--print("[" .. startIndex .. "] '" .. part .. "'");
							break;
						elseif (index - startIndex) > size then
							-- if space is out of size then...
							part = string.sub(outputMessage, startIndex, endIndex - 1);
							sendPart(part, "add");
							--print("[" .. startIndex .. "-" .. (endIndex - 1) .. "] '" .. part .. "'");
							startIndex = endIndex;
						end
						-- look for next space.
						endIndex = index + 1;
					end

				-- Enable Do Not Disturb
				if CheckButtonDND:GetChecked() == 1 then
				--if UnitIsDND("player") == nil then
				SendChatMessage("<" .. unitName .. ">: " .. textDoNotDisturb, "DND");
				--end
				end

				-- FILL EDITBOXES		
				outputEditBox = chatMessageS .. "\n|cffffffff" .. outputMessage .. "|r\n" .. chatMessageE; -- "<" .. event .. " />" .. 
				EditBoxQuest:SetText(outputEditBox);
				EditBoxQuest:HighlightText();
				EditBoxMini:SetText(outputEditBox); 
				EditBoxMini:HighlightText();

				end
			end
			messageSpeak("Auto");

			-- FUNCTION ON SCROLL

			local function onMouseWheel(self, delta)
				functionReset();
				if CheckButtonEnable:GetChecked() == 1 then
									
					-- Send Message
					if delta == 1 then
						setVoice();
						resetSetColor();
						outputFromEvent();
						messageSpeak("Scroll");
					else
						sendChatMessageStop();
					end

				end
			end
			FrameScroll:SetScript("OnMouseWheel", onMouseWheel);

		end -- if Disable TTS end

	end -- if QUEST EVENTS end

	-- SAVING VALUES ON PLAYER_LOGOUT

	if event == "PLAYER_LOGOUT" then
		saveValues();
		FrameMini:SetMovable(true);
		FrameMini:SetUserPlaced(true); 
		FrameOptions:SetMovable(true);
		FrameOptions:SetUserPlaced(true); 
		FrameScroll:SetMovable(true);
		FrameScroll:SetResizable(true);
		FrameScroll:SetUserPlaced(true);
	end

end
FrameOptions:SetScript("OnEvent", eventFunction);

