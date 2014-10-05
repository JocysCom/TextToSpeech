-- /fstack - show frame names

PitchMin = -5;
PitchMax = 5;
RateMin = 0;
RateMax = 2;

local enableSuffix = "EnableCheckBox";
local weightSuffix = "WeightEditBox";
local voiceSuffix = "VoiceEditBox";
local lastQuest = nil;
local prevVoiceInfo = nil;
local DebugMode = 0;
local OptionsFrameIsVisible = 0;
local debugLine = 0;

function LogMessage(s)
	if DebugMode == 1 then
		debugLine = debugLine + 1;
		SendChatMessage("" .. debugLine .. ": " .. s, "WHISPER", "Common", unitName);	
		--print("" .. debugLine .. ": " .. s);	
	end
end

local defaultSettings =
{
	{
		["Gender"] = "Female",
		["ControlPrefix"] = "EditBoxF",
		["TopLeft"] = 8,
		["Voices"] =
		{
			{ true,  100, "IVONA 2 Amy" },
			{ nil, 100, "IVONA 2 Natalie" },
			{ nil, 100, "IVONA 2 Emma" },
			{ nil, 100, "IVONA 2 Justin" },
			{ nil, 100, "IVONA 2 Salli" },
			{ nil, 100, "IVONA 2 Kimberly" },
			{ nil, 100, "IVONA 2 Kendra" },
			{ nil, 100, "IVONA 2 Ivy" },
		},
	},
	{
		["Gender"] = "Male",
		["ControlPrefix"] = "EditBoxM",
		["TopLeft"] = (8 + 160),
		["Voices"] =
		{
			{ true,  100, "IVONA 2 Brian" },
			{ nil, 100, "IVONA 2 Joey" },
			{ nil, 100, "IVONA 2 Eric" },
			{ nil, 100, "IVONA 2 Russel" },
			{ nil, 100, "" },
			{ nil, 100, "" },
			{ nil, 100, "" },
			{ nil, 100, "" },
		},
	},
	{
		["Gender"] = "Neutral",
		["ControlPrefix"] = "EditBoxU",
		["TopLeft"] = (8 + 320),
		["Voices"] =
		{
			{ true,  100, "Microsoft Zira Desktop" },
			{ nil, 100, "" },
			{ nil, 100, "" },
			{ nil, 100, "" },
			{ nil, 100, "" },
			{ nil, 100, "" },
			{ nil, 100, "" },
			{ nil, 100, "" },
		},
	},
};

-- TEXT

unitName = UnitName("player");
messageStop = "<voice command=\"stop\" />";

local Translations_EN = {
	MaleTitleFontString = "MALE TTS VOICES",
	FemaleTitleFontString = "FEMALE TTS VOICES",
	NeutralTitleFontString = "NEUTRAL TTS VOICES",
	OptionsFrameTitle = "Jocys.com World of Warcraft Text to Speech Addon - v2014.10.01",
	Defaults = "Reset all settings to default values",
	FrameScroll = "When addon is enabled and quest window is open, you can use your mouse wheel over this hidden frame.\n\nSCROLL UP = START SPEECH\n\nSCROLL DOWN = STOP SPEECH",
	DoNotDisturb = "Please wait... NPC dialog window is open and text-to-speech is enabled.",
	Description = "How it works: When you open NPC dialogue window, |cff77ccffJocys.Com WoW Text to Speech Addon|r creates and sends special whisper message to yourself (message includes dialogue text, voice name, pitch value and effect name). Then, |cff77ccffJocys.Com WoW Text to Speech Monitor|r (which must be running in background) picks-up this message from your network traffic and reads it with text-to-speech voice. You can use free text-to-speech voices by Microsoft or you can download and install additional and better text-to-speech voices from |cff77ccffIvona.com|r website. Good voices are English-British \"Amy\" and \"Brian\". English-American \"Salli\" and \"Joey\" are not bad too. For more help and to download or update |cff77ccffAddon|r with |cff77ccffMonitor|r, visit \"Software\" section of |cff77ccffJocys.com|r website.",
	-- Help.
	DndCheckButton_Help = "Enables \"Do not disturb\" status and shows |cffffffff<Busy>|r near name (over character) for other players, when NPC dialogue window is open.",
	EnableCheckBox_Help = "|cffffffffEnable|r or |cffffffffdisable|r usage of this text-to-speech voice.",
	WeightEditBox_Help = "Voice popularity. Value from |cffffffff0|r (no usage) or |cffffffff1|r (rare usage) to |cffffffff100|r (often usage). How it works... lets say, you enabled 3 male voices with weight: |cffffffff100 Ivona 2 Brian|r, |cffffffff80 Ivona 2 Joey|r and |cffffffff50 Ivona 2 Eric|r. From name of NPC addon will generate number from |cff77ccff0|r to |cff77ccff230|r (|cffffffff100|r\+|cffffffff80|r\+|cffffffff50|r\=|cff77ccff230|r).\n\nIf this number will be from |cff77ccff1|r to |cff77ccff100|r, then |cffffffff100 Ivona 2 Brian|r voice will be selected.\nIf this number will be from |cff77ccff101|r to |cff77ccff180|r, then |cffffffff80 Ivona 2 Joey|r voice will be selected.\nIf this number will be from |cff77ccff181|r to |cff77ccff230|r, then |cffffffff50 Ivona 2 Eric|r voice will be selected.",
	VoiceEditBox_Help = "You can write in this field |cfffffffftext-to-speech voice name|r installed on your operating system. For example: |cffffffffMicrosoft Zira Desktop|r or |cffffffffIVONA 2 Brian|r.",
	ReplaceNameEditBox_Help = "If text-to-speech voice pronounce your character |cffffffffname|r incorrectly, you can change |cffffffffit|r in this field from |cff00ff00" .. unitName .. "|r to ...something else.",
	RateMinEditBox_Help = "Minimum voice rate (speed). Values from |cffffffff-10|r to |cffffffff10|r. Recommended value |cffffffff" .. RateMin .. "|r.",
	RateMaxEditBox_Help = "Maximum voice rate (speed). Values from |cffffffff-10|r to |cffffffff10|r. Recommended value |cffffffff" .. RateMax .. "|r.",
	PitchMinEditBox_Help = "Minimum voice pitch. Values from |cffffffff-10|r to |cffffffff10|r. Recommended value |cffffffff" .. PitchMin .. "|r.",
	PitchMaxEditBox_Help = "Maximum voice pitch. Values from |cffffffff-10|r to |cffffffff10|r. Recommended value |cffffffff" .. PitchMax .. "|r.",
};

local L = Translations_EN;

local function ShowHelp(control)
	local help = L.Description;
	if control ~= nil then
		local name = control:GetName();
		local s = nil;
		if string.match(name, enableSuffix) then
			s = L.EnableCheckBox_Help;
		elseif string.match(name, weightSuffix) then
			s = L.WeightEditBox_Help;
		elseif string.match(name, voiceSuffix) then
			s = L.VoiceEditBox_Help;
		else
			s = L[name .. "_Help"];
		end		
		if s ~= nil then
			help = s;
		end
	end
	DescriptionFrame.text:SetText(help);
end

local function Control_OnLeave(self)
	ShowHelp(nil);	
end

local function Control_OnEnter(self)
	ShowHelp(self);	
end

local function PitchMinEditBox_OnTextChanged(self)
	LogMessage("Changed");
	local v = tonumber(self:GetText());
	if v < PitchMin then
		self.SetText(PitchMin);
	end
end

local function PitchMaxEditBox_OnTextChanged(self)
end

local function RateMinEditBox_OnTextChanged(self)
end

local function RateMaxEditBox_OnTextChanged(self)
end

-- FUNCTION SET COLOR
local function GetFrame(frameName)
	local frame = EnumerateFrames();
	-- Get the first frame
	while frame do
		if frame:GetName() == frameName then
			return frame;
		end
		frame = EnumerateFrames(frame);
		-- Get the next frame
	end
end

-- FUNCTION : CREATE DEFAULT EDITBOX

local function createEditBox(name, parent, text, width)
	if width == nil then
		width = 100;
	end
	frame = CreateFrame("EditBox", name, parent);
	frame:SetSize(width, 15);
	frame:SetTextInsets(2, 2, 0, 0);
	frame:SetFontObject("GameFontHighlightSmall");
	frame:SetText(text);
	frame.texture = frame:CreateTexture("ARTWORK");
	frame.texture:SetAllPoints();
	frame.texture:SetTexture(0, 0, 0, 1.0);
	return frame;
end

-- FUNCTION : CREATE DEFAULT EDITBOX 2

local crcTable = {
	0x00000000,0x77073096,0xee0e612c,0x990951ba,0x076dc419,0x706af48f,0xe963a535,0x9e6495a3,
	0x0edb8832,0x79dcb8a4,0xe0d5e91e,0x97d2d988,0x09b64c2b,0x7eb17cbd,0xe7b82d07,0x90bf1d91,
	0x1db71064,0x6ab020f2,0xf3b97148,0x84be41de,0x1adad47d,0x6ddde4eb,0xf4d4b551,0x83d385c7,
	0x136c9856,0x646ba8c0,0xfd62f97a,0x8a65c9ec,0x14015c4f,0x63066cd9,0xfa0f3d63,0x8d080df5,
	0x3b6e20c8,0x4c69105e,0xd56041e4,0xa2677172,0x3c03e4d1,0x4b04d447,0xd20d85fd,0xa50ab56b,
	0x35b5a8fa,0x42b2986c,0xdbbbc9d6,0xacbcf940,0x32d86ce3,0x45df5c75,0xdcd60dcf,0xabd13d59,
	0x26d930ac,0x51de003a,0xc8d75180,0xbfd06116,0x21b4f4b5,0x56b3c423,0xcfba9599,0xb8bda50f,
	0x2802b89e,0x5f058808,0xc60cd9b2,0xb10be924,0x2f6f7c87,0x58684c11,0xc1611dab,0xb6662d3d,
	0x76dc4190,0x01db7106,0x98d220bc,0xefd5102a,0x71b18589,0x06b6b51f,0x9fbfe4a5,0xe8b8d433,
	0x7807c9a2,0x0f00f934,0x9609a88e,0xe10e9818,0x7f6a0dbb,0x086d3d2d,0x91646c97,0xe6635c01,
	0x6b6b51f4,0x1c6c6162,0x856530d8,0xf262004e,0x6c0695ed,0x1b01a57b,0x8208f4c1,0xf50fc457,
	0x65b0d9c6,0x12b7e950,0x8bbeb8ea,0xfcb9887c,0x62dd1ddf,0x15da2d49,0x8cd37cf3,0xfbd44c65,
	0x4db26158,0x3ab551ce,0xa3bc0074,0xd4bb30e2,0x4adfa541,0x3dd895d7,0xa4d1c46d,0xd3d6f4fb,
	0x4369e96a,0x346ed9fc,0xad678846,0xda60b8d0,0x44042d73,0x33031de5,0xaa0a4c5f,0xdd0d7cc9,
	0x5005713c,0x270241aa,0xbe0b1010,0xc90c2086,0x5768b525,0x206f85b3,0xb966d409,0xce61e49f,
	0x5edef90e,0x29d9c998,0xb0d09822,0xc7d7a8b4,0x59b33d17,0x2eb40d81,0xb7bd5c3b,0xc0ba6cad,
	0xedb88320,0x9abfb3b6,0x03b6e20c,0x74b1d29a,0xead54739,0x9dd277af,0x04db2615,0x73dc1683,
	0xe3630b12,0x94643b84,0x0d6d6a3e,0x7a6a5aa8,0xe40ecf0b,0x9309ff9d,0x0a00ae27,0x7d079eb1,
	0xf00f9344,0x8708a3d2,0x1e01f268,0x6906c2fe,0xf762575d,0x806567cb,0x196c3671,0x6e6b06e7,
	0xfed41b76,0x89d32be0,0x10da7a5a,0x67dd4acc,0xf9b9df6f,0x8ebeeff9,0x17b7be43,0x60b08ed5,
	0xd6d6a3e8,0xa1d1937e,0x38d8c2c4,0x4fdff252,0xd1bb67f1,0xa6bc5767,0x3fb506dd,0x48b2364b,
	0xd80d2bda,0xaf0a1b4c,0x36034af6,0x41047a60,0xdf60efc3,0xa867df55,0x316e8eef,0x4669be79,
	0xcb61b38c,0xbc66831a,0x256fd2a0,0x5268e236,0xcc0c7795,0xbb0b4703,0x220216b9,0x5505262f,
	0xc5ba3bbe,0xb2bd0b28,0x2bb45a92,0x5cb36a04,0xc2d7ffa7,0xb5d0cf31,0x2cd99e8b,0x5bdeae1d,
	0x9b64c2b0,0xec63f226,0x756aa39c,0x026d930a,0x9c0906a9,0xeb0e363f,0x72076785,0x05005713,
	0x95bf4a82,0xe2b87a14,0x7bb12bae,0x0cb61b38,0x92d28e9b,0xe5d5be0d,0x7cdcefb7,0x0bdbdf21,
	0x86d3d2d4,0xf1d4e242,0x68ddb3f8,0x1fda836e,0x81be16cd,0xf6b9265b,0x6fb077e1,0x18b74777,
	0x88085ae6,0xff0f6a70,0x66063bca,0x11010b5c,0x8f659eff,0xf862ae69,0x616bffd3,0x166ccf45,
	0xa00ae278,0xd70dd2ee,0x4e048354,0x3903b3c2,0xa7672661,0xd06016f7,0x4969474d,0x3e6e77db,
	0xaed16a4a,0xd9d65adc,0x40df0b66,0x37d83bf0,0xa9bcae53,0xdebb9ec5,0x47b2cf7f,0x30b5ffe9,
	0xbdbdf21c,0xcabac28a,0x53b39330,0x24b4a3a6,0xbad03605,0xcdd70693,0x54de5729,0x23d967bf,
	0xb3667a2e,0xc4614ab8,0x5d681b02,0x2a6f2b94,0xb40bbe37,0xc30c8ea1,0x5a05df1b,0x2d02ef8d
};

local function ComputeHashCrc32(crc32, buffer, offset, length)
	crc32 = bit.bxor(crc32, 0xFFFFFFFF);
	while length > 0 do
		length = length - 1;
		crc32 = bit.bxor(crcTable[bit.band(bit.bxor(crc32, buffer[offset]), 0xFF) + 1], bit.rshift(crc32, 8));
		offset = offset + 1;
	end
	crc32 = bit.bxor(crc32, 0xFFFFFFFF);
	return crc32;
end

local function ComputeHash(s)
	local buffer = { };
	local len = string.len(s)
	for i = 1, len do
		buffer[i - 1] = string.byte(s, i);
	end
	return ComputeHashCrc32(0, buffer, 0, len);
end

local function GetNumber(min, max, key, value)
	local s = key .. value;
	local hash = ComputeHash(s);
	local d = bit.band((max - min), 0xFFFFFFFF);
	if d == 0xFFFFFFFF then
		return hash;
	end
	return min +(hash %(d + 1));
end

local function PickVoiceInfo(voices, s)
	local count = 0;
	local len = table.getn(voices);
	local min = 0;
	local max = 0;
	for i = 1, len, 1 do
		max = max + voices[i][2];
	end
	local number = GetNumber(0,(max - 1), "voice", s);
	local weight;
	local currentWeight = 0;
	for i = 1, len, 1 do
		weight = voices[i][2];
		if number < (currentWeight + weight) then
			return voices[i];
		end
		currentWeight = currentWeight + weight;
	end
	return nil;
end

local function UnlockFrames()
	MiniFrame:SetMovable(true);
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
	MiniFrame.texture:SetTexture(0, 0, 0, 0.05);
	ScrollFrame:SetMovable(nil);
	ScrollFrame:SetResizable(nil);
	ScrollFrame:EnableMouse(nil);
	ScrollFrame.texture:SetTexture(0, 0, 0, 0.05);
	ScrollFrame.text:Hide();
	ScrollFrame.resizeButton:Hide();
	ScrollFrame:SetFrameLevel(100);
end

local function UpdateMiniAndScrollFrame()
	local frame = nil;
	if GossipFrame:IsShown() then frame = GossipFrame end
	if QuestFrame:IsShown() then frame = QuestFrame end
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
		MiniFrame:SetPoint("TOPRIGHT", -4, -26);
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
		if OptionsFrameIsVisible == 1 then
			UnlockFrames();
			OptionsFrame:Show();
		else
			OptionsFrame:Hide();
			LockFrames();
		end
	end
end

-- FUNCTION SET COLOR
local function SetFrameTextColor(frameName, enable)
	local frame = GetFrame(frameName);
	if enable then
		-- green
		frame:SetTextColor(0.0, 1.0, 0.0, 1.0);
	else
		-- white (1)		
		frame:SetTextColor(1.0, 1.0, 1.0, 1.0);
	end
end

---- FUNCTION : ENABLE MESSAGE FILTER : CHAT_MSG_WHISPER_INFORM

local function hideMessageWhisperInform(self, event, msg)
	if msg:find("<voice") then
		return true;
	end
end

---- FUNCTION : ENABLE/DISABLE MESSAGE FILTER : CHAT_MSG_WHISPER

local function hideMessageWhisper(self, event, msg)
	if msg:find("<voice") and FilterCheckButton:GetChecked() == 1 then
		return true;
	end
end

---- FUNCTION : ENABLE/DISABLE MESSAGE FILTER : CHAT_MSG_DND

local function hideMessageDnd(self, event, msg)
	if msg:find("<" .. unitName .. ">: ") and FilterCheckButton:GetChecked() == 1 then
		return true;
	end
end

---- FUNCTION : ENABLE/DISABLE MESSAGE FILTER : CHAT_MSG_SYSTEM

local function hideMessageSystem(self, event, msg)
	if msg:find("You are no") and FilterCheckButton:GetChecked() == 1 then
		return true;
	end
end

---- FUNCTION : ENABLE/DISABLE MESSAGE FILTER

local function InitialiseMessageEventFilters()
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER_INFORM", hideMessageWhisperInform);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_WHISPER", hideMessageWhisper);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_DND", hideMessageDnd);
	ChatFrame_AddMessageEventFilter("CHAT_MSG_SYSTEM", hideMessageSystem);
end

-- FUNCTION ENABLE or DISABLE TTS

local function EditBox_OnEscapePressed()
	CloseQuest();
	CloseGossip();
	sendChatMessageStop();
	OptionsFrameIsVisible = 0;
	UpdateMiniAndScrollFrame();
end

-- FUNCTION MESSAGE STOP
local function sendChatMessageStop()
	if framesClosed == 1 then
		-- Send messageStop
		SendChatMessage(messageStop, "WHISPER", "Common", unitName);
		QuestEditBox:SetText(messageStop);
		QuestEditBox:HighlightText();
	end
	framesClosed = 0;
end


-- FUNCTON SET VOICE, PITCH AND RATE

function GetCurrentVoiceInfo()
	-- Get target name.
	-- Hero call board have wrong target!!!!
	local targetName = GetUnitName("target");
	if targetName == nil then targetName = "nil" end;
	-- Get target type
	-- Beast, Dragonkin, Demon, Elemental, Giant, Undead, Humanoid, Critter, Mechanical, Not specified, Totem, Non-combat Pet, Gas Cloud							
	local targetType = UnitCreatureType("target");
	if targetType == nil then targetType = "Default" end;
	-- Get target gender.
	local targetSex = UnitSex("target");
	local targetGender = "Neutral";
	if targetSex == 3 then
		targetGender = "Female";
	elseif targetSex == 2 then
		targetGender = "Male";
	end
	LogMessage(targetName .. ", " .. targetType .. ", " .. targetGender);
	-- ====================================================
	-- Add voices to the list: Name, Weight.
	local voiceList = { };
	local voiceIndex = 0;
	local len = table.getn(defaultSettings);
	for i = 1, len do
		local row = defaultSettings[i];
		local controlPrefix = row["ControlPrefix"];
		local voices = row["Voices"];
		local gender = row["Gender"];
		if gender == targetGender then
			local voicesLen = table.getn(voices);
			for v = 1, voicesLen do
				local prefix = controlPrefix .. (v - 1);
				local check = GetFrame(prefix .. enableSuffix):GetChecked();
				local weight = GetFrame(prefix .. weightSuffix):GetText();
				local voice = GetFrame(prefix .. voiceSuffix):GetText();
				if check == 1 then
					voiceIndex = voiceIndex + 1;
					-- add voice to the list.
					voiceList[voiceIndex] = { voice, tonumber(weight), prefix };
				end
			end
		end
	end
	local voiceListLen = table.getn(voiceList);
	LogMessage("VoiceList[" .. voiceListLen .. "]");
	-- pick voice by target name.
	local pickedVoiceInfo = PickVoiceInfo(voiceList, targetName);
	local voiceInfo = nil
	if pickedVoiceInfo ~= nil then
		local voice = pickedVoiceInfo[1];
		local prefix = pickedVoiceInfo[3];
		local pitchMin = tonumber(PitchMinEditBox:GetText())
		local pitchMax = tonumber(PitchMaxEditBox:GetText())
		local pitch = GetNumber(pitchMin, pitchMax, "pitch", targetName);
		local rateMin = tonumber(RateMinEditBox:GetText())
		local rateMax = tonumber(RateMaxEditBox:GetText())
		local rate = GetNumber(rateMin, rateMax, "rate", targetName);
		voiceInfo = {
			["name"] = voice,
			["gender"] = targetGender,
			["pitch"] = pitch,
			["rate"] = rate,
			["effect"] = targetType,
			["prefix"] = prefix,
		};
		LogMessage("Voice=" .. voice .. ", pitch=" .. pitch .. ", rate=" .. rate);
	end
	return 	voiceInfo;
end

-- VOICE EDITBOXES AND TITLE 

local function CreateFrames(parent, controlPrefix, gender, voices, position, column)
	local voicesLen = table.getn(voices) -1;
	local item;
	local parentWidth = parent:GetWidth();
	local checkBoxSize = 24;
	local weightWidth = 32;
	local padding = 8;
	local space = 4;
	local nameWidth = ((parentWidth - (padding * 2)) - (checkBoxSize * 3) - (weightWidth * 3) - (space * 7)) / 3;
	position = padding + space + ((checkBoxSize + weightWidth + nameWidth + (space * 2)) * (column - 1));
	for i = 0, voicesLen do
		local prefix = controlPrefix .. i;
		local enabled = voices[i + 1][1];
		local weight = voices[i + 1][2];
		local voice = voices[i + 1][3];
		local top = -32 -(i * 24);
		-- Create enable CheckButton
		CreateFrame("CheckButton", prefix .. enableSuffix, parent, "UICheckButtonTemplate");
		item = GetFrame(prefix .. enableSuffix);
		item:SetSize(checkBoxSize,checkBoxSize);
		item:SetPoint("TOPLEFT", position, top);
		item:SetChecked(enabled);
		item:SetScript("OnEnter", Control_OnEnter);
		item:SetScript("OnLeave", Control_OnLeave);
		-- Create weight TextBox.
		createEditBox(prefix .. weightSuffix, parent, weight, weightWidth);
		item = GetFrame(prefix .. weightSuffix);
		item:SetPoint("TOPLEFT", position + checkBoxSize, top - 4);
		item:SetJustifyH("RIGHT");
		item:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
		item:SetScript("OnEnter", Control_OnEnter);
		item:SetScript("OnLeave", Control_OnLeave);
		-- Create voice name TextBox.
		createEditBox(prefix .. voiceSuffix, parent, voice, nameWidth);
		item = GetFrame(prefix .. voiceSuffix);
		item:SetScript("OnEscapePressed", EditBox_OnEscapePressed)
		item:SetPoint("TOPLEFT", position + checkBoxSize + space + weightWidth, top - 4);
		item:SetScript("OnEnter", Control_OnEnter);
		item:SetScript("OnLeave", Control_OnLeave);
		if i == 0 then
			local titleName = gender .. "TitleFontString";
			local titleText = L[titleName];
			local fontString = item:CreateFontString(titleName, ARTWORK, "GameFontHighlightSmall");
			fontString:SetPoint("TOPLEFT", -34, 21);
			fontString:SetTextColor(0.5, 0.5, 0.5, 1.0);
			fontString:SetText(titleText);
		end
	end
end

function VoicesFrame_OnLoad(self)
	local len = table.getn(defaultSettings);
	for i = 1, len do
		local row = defaultSettings[i];
		local gender = row["Gender"];
		local controlPrefix = row["ControlPrefix"];
		local topLeft = row["TopLeft"];
		local voices = row["Voices"];
		local voicesLen = table.getn(voices);
		-- createEditBox("TestEditBox", FrameVoices, voicesLen);
		-- GetFrame("TestEditBox"):SetPoint("BOTTOMRIGHT", -353, 192);
		CreateFrames(self, controlPrefix, gender, voices, topLeft, i);
	end
end

function OptionsFrame_CloseButton_OnClick(self)
	OptionsFrameIsVisible = 0;
	UpdateMiniAndScrollFrame();
end

function OptionsFrame_OnLoad(self)
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
	self.TitleText:SetText(L.OptionsFrameTitle);
	-- Attach events.
	self:SetScript("OnEvent", OptionsFrame_OnEvent);
	self:RegisterEvent("ADDON_LOADED");
	self:RegisterEvent("QUEST_GREETING");
	self:RegisterEvent("GOSSIP_SHOW");
	self:RegisterEvent("QUEST_DETAIL");
	self:RegisterEvent("QUEST_PROGRESS");
	self:RegisterEvent("QUEST_COMPLETE");
	self:RegisterEvent("QUEST_ACCEPTED");
	self:RegisterEvent("QUEST_FINISHED");
	self:RegisterEvent("GOSSIP_CLOSED");
	self:RegisterEvent("PLAYER_LOGOUT");
	InitialiseMessageEventFilters();
	DefaultsFontString:SetText(L.Defaults);	
	-- Attach escape and OnEnter/OnLeave scripts.
	QuestEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	DndCheckButton:SetScript("OnEnter", Control_OnEnter);
	DndCheckButton:SetScript("OnLeave", Control_OnLeave);
	RateMinEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	RateMinEditBox:SetScript("OnEnter", Control_OnEnter);
	RateMinEditBox:SetScript("OnLeave", Control_OnLeave);
	RateMaxEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	RateMaxEditBox:SetScript("OnEnter", Control_OnEnter);
	RateMaxEditBox:SetScript("OnLeave", Control_OnLeave);
	PitchMinEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	PitchMinEditBox:SetScript("OnEnter", Control_OnEnter);
	PitchMinEditBox:SetScript("OnLeave", Control_OnLeave);
	PitchMaxEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	PitchMaxEditBox:SetScript("OnEnter", Control_OnEnter);
	PitchMaxEditBox:SetScript("OnLeave", Control_OnLeave);
	ReplaceNameEditBox:SetScript("OnEscapePressed", EditBox_OnEscapePressed);
	ReplaceNameEditBox:SetScript("OnEnter", Control_OnEnter);
	ReplaceNameEditBox:SetScript("OnLeave", Control_OnLeave);
	if DebugMode == 0 then	
		SaveSettingsButton:Hide();
		LoadSettingsButton:Hide();
	end
	LogMessage("Registered");		
end

function SetDnd(enable)
	local addonEnabled = (EnabledCheckButton:GetChecked() == 1);
	local dndEnabled = (DndCheckButton:GetChecked() == 1);
	local isShown = GossipFrame:IsShown() or QuestFrame:IsShown();
	local force = 0;
	-- If auto then...
	if enable == -1 then
		force = 1;
		if addonEnabled and dndEnabled and isShown then
			enable = 1;
		else
			enable = 0;
		end
	end
	--LogMessage("SetDnd: enable=" .. enable .. ", force=" .. force);
	-- DND control is allowed then...
	if force == 1 or (addonEnabled and dndEnabled) then
		local enabled = (UnitIsDND("player") == 1);
		if enable == 1 and not enabled then
			-- Enable DND.
			SendChatMessage("<" .. unitName .. ">: " .. L.DoNotDisturb, "DND");	
		elseif enable == 0 and enabled then
			-- Disable DND.
			SendChatMessage("", "DND");
		end
	end
end

function OptionsFrame_OnEvent(self, event)
	if event == "ADDON_LOADED" then
		LoadAllSettings();
		UpdateMiniAndScrollFrame();
	elseif event == "QUEST_GREETING" or event == "GOSSIP_SHOW" or event == "QUEST_DETAIL" or event == "QUEST_PROGRESS" or event == "QUEST_COMPLETE" then
		UpdateMiniAndScrollFrame();
		SetDnd(1);
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
		end
		SpeakMessage("Auto", lastQuest);
	elseif event == "QUEST_ACCEPTED" or event == "QUEST_FINISHED" or event == "GOSSIP_CLOSED" then
		UpdateMiniAndScrollFrame();		
		SetDnd(0);
		if EnabledCheckButton:GetChecked() == 1 then
			sendChatMessageStop();
		end
	elseif event == "PLAYER_LOGOUT" then
		SaveAllSettings();
	end
end

function SpeakMessage(reason, questText)
	if questText == nil then
		LogMessage("No Quest text");
		return;
	end
	local vi = GetCurrentVoiceInfo();
	-- If previous voice was selected then...
	if prevVoiceInfo ~= nil then
		-- Unselect previous voice.
		SetFrameTextColor(prevVoiceInfo.prefix .. voiceSuffix, nil);
	end
	-- Store current voice.
	prevVoiceInfo = vi;
	-- Select current voice.
	SetFrameTextColor(prevVoiceInfo.prefix .. voiceSuffix, true);
	local message = questText;
	-- REPLACE NAME
	--LogMessage("Speak 1: Replace name.");
	local newUnitName = ReplaceNameEditBox:GetText();
	if string.len(newUnitName) > 0 and newUnitName ~= unitName then
		message = string.gsub(message, unitName, newUnitName);
	end
	-- FORMAT TEXT
	--LogMessage("Speak 1: Format text.");
	local pitchComment = "0";
	message = string.gsub(message, "\r", " ");
	message = string.gsub(message, "\n", " ");
	message = string.gsub(message, "[%.]+", ". ");
	message = string.gsub(message, "<", " &lt;pitch absmiddle=\"" .. pitchComment .. "\"&gt;");
	message = string.gsub(message, ">", "&lt;/pitch&gt; ");
	message = string.gsub(message, "[ ]+", " ");
	-- Format and speak
	--LogMessage("Speak 2, " .. reason .. ", message=" .. string.len(message));
	if (reason == "Scroll") or(reason == "Auto" and AutoCheckButton:GetChecked() == 1) then
		framesClosed = 1;
		local size = 130;
		local startIndex = 1;
		local endIndex = 1;
		local part = "";
		local chatMessageSP = GetStartElement(vi, "play");
		local chatMessageSA = GetStartElement(vi, "add");
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
		-- FILL EDITBOXES
		local outputEditBox = chatMessageSP .. "\n|cffffffff" .. message .. "|r\n" .. chatMessageE;
		-- "<" .. event .. " />" ..
		QuestEditBox:SetText(outputEditBox);
		QuestEditBox:HighlightText();
	end
end

function OptionsButton_OnClick(self)
	if OptionsFrame:IsShown() then
		OptionsFrameIsVisible = 0;
	else
		OptionsFrameIsVisible = 1;
	end
	UpdateMiniAndScrollFrame();	
end

function EnabledCheckButton_OnLoad(self)
	self.text:SetFontObject(GameFontHighlightSmall);
	self.text:SetTextColor(0.5, 0.5, 0.5, 1.0);
	self.text:SetText("Enable addon");
end

function EnabledCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	-- If addon was disabled.
	if EnabledCheckButton:GetChecked() == 1 then
	else
		sendChatMessageStop();
	end
	UpdateMiniAndScrollFrame();
	SetDnd(-1);
end

function AutoCheckButton_OnLoad(self)
	self.text:SetFontObject(GameFontHighlightSmall);
	self.text:SetTextColor(0.5, 0.5, 0.5, 1.0);
	self.text:SetText("Auto start speech when dialog window is open");
end

function AutoCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	if self:GetChecked() == 1 then
		--if EnabledCheckButton:GetChecked() ~= 1 then
		--	EnabledCheckButton:SetChecked(1);
		--end
	end
end

function DndCheckButton_OnLoad(self)
	self.text:SetFontObject(GameFontHighlightSmall);
	self.text:SetTextColor(0.5, 0.5, 0.5, 1.0);
	self.text:SetText("Auto \"Do not disturb\"");
end

function DndCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	SetDnd(-1);
end

function FilterCheckButton_OnLoad(self)
	self.text:SetFontObject(GameFontHighlightSmall);
	self.text:SetTextColor(0.5, 0.5, 0.5, 1.0);
	self.text:SetText("Hide addon messages in chat window");
end

function FilterCheckButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
end

function MiniFrame_OnLoad(self)
	self.texture:SetTexture(0, 0, 0, 0);
end

function ScrollFrame_OnLoad(self)
	self.text:SetText(L.FrameScroll);
	self:SetScript("OnMouseWheel", ScrollFrame_OnMouseWheel);
end

function ScrollFrame_OnMouseDown(self)
	self:StartMoving();
end

function ScrollFrame_OnMouseUp(self)
	self:StopMovingOrSizing();
end

function ScrollFrame_OnMouseWheel(self, delta)
	if EnabledCheckButton:GetChecked() == 1 then
		-- Send Message
		if delta == 1 then
			SpeakMessage("Scroll", lastQuest);
		else
			sendChatMessageStop();
		end
	end
end

function PlayButtonButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOn");
	SpeakMessage("Scroll", lastQuest);
end

function StopButtonButton_OnClick(self)
	PlaySound("igMainMenuOptionCheckBoxOff");
	sendChatMessageStop();
end

function ScrollResizeButton_OnMouseDown(self)
	self:GetParent():StartSizing("BOTTOMRIGHT");
	self:GetParent():SetUserPlaced(true);
end

function ScrollResizeButton_OnMouseUp(self)
	self:GetParent():StopMovingOrSizing();
end

function QuestFrame_OnLoad(self)
	self.Bg:SetTexture(0, 0, 0, 1.0);
end

function QuestScrollFrame_OnLoad(self)
	self:SetScrollChild(QuestEditBox);
	QuestEditBox.texture:SetTexture(0, 0, 0, 0.0);
end

function QuestEditBox_OnLoad(self)
	self:SetTextInsets(5, 5, 5, 5);
	self:SetTextColor(0.5, 0.5, 0.5, 1.0);
	self.texture:SetTexture(0, 0, 0, 0.0);
end

function DescriptionFrame_OnLoad(self)
	self.text:SetText(L.Description);
end

function DefaultsButton_OnClick(self)
	ResetAllSettingsToDefault();
end

function LoadSettingsButton_OnClick(self)
	LoadAllSettings();
	LogMessage("TTS: Load All Settings");
end

function SaveSettingsButton_OnClick(self)
	SaveAllSettings();
	LogMessage("TTS: Save All Settings");
end

---- FUNCTION : RESET / DEFAULT VALUES

function ResetAllSettingsToDefault()
	local len = table.getn(defaultSettings);
	for i = 1, len do
		local row = defaultSettings[i];
		local controlPrefix = row["ControlPrefix"];
		local voices = row["Voices"];
		local voicesLen = table.getn(voices);
		for v = 1, voicesLen do
			-- Get default value.
			local check = voices[v][1];
			local weight = voices[v][2];
			local voice = voices[v][3];
			local prefix = controlPrefix .. (v - 1);
			GetFrame(prefix .. enableSuffix):SetChecked(check);
			GetFrame(prefix .. weightSuffix):SetText(weight);
			GetFrame(prefix .. voiceSuffix):SetText(voice);
		end
	end
	RateMinEditBox:SetText(RateMin);
	RateMaxEditBox:SetText(RateMax);
	PitchMinEditBox:SetText(PitchMin);
	PitchMaxEditBox:SetText(PitchMax);
	ReplaceNameEditBox:SetText(unitName);
end

-- FUNCTION SAVE VALUES (ON OPTIONS CLOSE AND LOGOUT)

function SaveAllSettings()
	-- Save check buttons.
	EnabledCheckButtonValue = EnabledCheckButton:GetChecked();
	AutoCheckButtonValue = AutoCheckButton:GetChecked();
	DndCheckButtonValue = DndCheckButton:GetChecked();
	FilterCheckButtonValue = FilterCheckButton:GetChecked();
	-- Save edit boxes.
	ReplaceNameEditBoxValue = ReplaceNameEditBox:GetText();
	RateMinEditBoxValue = RateMinEditBox:GetText();
	RateMaxEditBoxValue = RateMaxEditBox:GetText();
	PitchMinEditBoxValue = PitchMinEditBox:GetText();
	PitchMaxEditBoxValue = PitchMaxEditBox:GetText();
	local len = table.getn(defaultSettings);
	for i = 1, len do
		local row = defaultSettings[i];
		local controlPrefix = row["ControlPrefix"];
		local voices = row["Voices"];
		local voicesLen = table.getn(voices);
		AllSavedValues[i] = { };
		AllSavedValues[i]["Voices"] = { };
		for v = 1, voicesLen do
			local prefix = controlPrefix .. (v - 1);
			AllSavedValues[i]["Voices"][v] = { };
			AllSavedValues[i]["Voices"][v][1] = GetFrame(prefix .. enableSuffix):GetChecked();
			AllSavedValues[i]["Voices"][v][2] = GetFrame(prefix .. weightSuffix):GetText();
			AllSavedValues[i]["Voices"][v][3] = GetFrame(prefix .. voiceSuffix):GetText();
		end
	end
	LogMessage("Settings Saved");
end

function LoadAllSettings()
	-- If this is first load then...
	if CharacterFirstLoginValue ~= 1 then
		CharacterFirstLoginValue = 1;
		EnabledCheckButtonValue = 1;
		AutoCheckButtonValue = 1;
		DndCheckButtonValue = 1;
		FilterCheckButtonValue = 1;
	end
	EnabledCheckButton:SetChecked(EnabledCheckButtonValue);
	AutoCheckButton:SetChecked(AutoCheckButtonValue);
	DndCheckButton:SetChecked(DndCheckButtonValue);
	FilterCheckButton:SetChecked(FilterCheckButtonValue);	
	-- Load edit boxes.
	if ReplaceNameEditBoxValue == "" or ReplaceNameEditBoxValue == nil then ReplaceNameEditBoxValue = unitName end;
	ReplaceNameEditBox:SetText(ReplaceNameEditBoxValue);
	if RateMinEditBoxValue == "" or RateMinEditBoxValue == nil then RateMinEditBoxValue = RateMin end;
	RateMinEditBox:SetText(RateMinEditBoxValue);
	if RateMaxEditBoxValue == "" or RateMaxEditBoxValue == nil then RateMaxEditBoxValue = RateMax end;
	RateMaxEditBox:SetText(RateMaxEditBoxValue);
	if PitchMinEditBoxValue == "" or PitchMinEditBoxValue == nil then PitchMinEditBoxValue = PitchMin end;
	PitchMinEditBox:SetText(PitchMinEditBoxValue);
	if PitchMaxEditBoxValue == "" or PitchMaxEditBoxValue == nil then PitchMaxEditBoxValue = PitchMax end;
	PitchMaxEditBox:SetText(PitchMaxEditBoxValue);
		local len = table.getn(defaultSettings);
		for i = 1, len do
			local row = defaultSettings[i];
			local controlPrefix = row["ControlPrefix"];
			local voices = row["Voices"];
			local voicesLen = table.getn(voices);
			for v = 1, voicesLen do
				-- Get default value
				local check = voices[v][1];
				local weight = voices[v][2];
				local voice = voices[v][3];
				if AllSavedValues ~= nil then
					if AllSavedValues[i] ~= nil then
						if AllSavedValues[i]["Voices"] ~= nil then
							if AllSavedValues[i]["Voices"][v] ~= nil then
								if AllSavedValues[i]["Voices"][v][3] ~= nil then
									check = AllSavedValues[i]["Voices"][v][1];
								end
								if AllSavedValues[i]["Voices"][v][3] ~= nil then
									weight = AllSavedValues[i]["Voices"][v][2];
								end
								if AllSavedValues[i]["Voices"][v][3] ~= nil then
									voice = AllSavedValues[i]["Voices"][v][3];
								end
							end
						end
					end
				end
				local prefix = controlPrefix .. (v - 1);
				GetFrame(prefix .. enableSuffix):SetChecked(check);
				GetFrame(prefix .. weightSuffix):SetText(weight);
				GetFrame(prefix .. voiceSuffix):SetText(voice);
			end
		end
end

function GetStartElement(vi, command)
	return "<voice name=\"" .. vi.name .. "\" gender=\"" .. vi.gender .. "\" pitch=\"" .. vi.pitch .. "\" rate=\"" .. vi.rate .. "\" effect=\"" .. vi.effect .. "\" command=\"" .. command .. "\"><part>";
end
