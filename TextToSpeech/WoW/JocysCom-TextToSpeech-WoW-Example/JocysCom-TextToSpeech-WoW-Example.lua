-- Message prefix for TTS Monitor to find pixel line on display. 6 coloured characters-pixels.
-- (|c) color starts, (##) alpha, (##) red, (##) green, (##) blue, (·) character-pixel, (|r) color ends.
local messagePrefixPixels = "|cff220000·|r|cff002200·|r|cff000022·|r|cff220000·|r|cff002200·|r|cff000022·|r"
-- Table (list) for adding and removing messages.
local messageTable = {}
-- Message change color value. For Monitor to know, when message was changed.
local messageChanged = math.random(5, 80)
-- TTS Monitor checks display with 100 millisecond intervals.
-- Send and remove message(s) from table with 500 millisecond intervals.
local messageInterval = 0.5

function JocysCom_PlayButton_OnClick()
local sendMessage = "Hello World!"
if #JocysCom_MessageEditBox:GetText() > 0 then
sendMessage = JocysCom_MessageEditBox:GetText()
end

JocysCom_MessageEditBox:SetText(sendMessage)
JocysCom_AddMessageToTable("<message command=\"play\"><part>" .. sendMessage .. "</part></message>")
end

function JocysCom_StopButton_OnClick()
JocysCom_AddMessageToTable("<message command=\"stop\" />")
end
function JocysCom_AddMessageToTable(messageText)
table.insert(messageTable, messageText)
end
function JocysCom_SendMessageFromTable()
-- Convert message characters to bytes (UTF-8). a ш B Ш C • 61 D188 42 D0A8 43 • 61D18842D0A843
local messageBytes = messageTable[1]:gsub(".", function(v) return string.format("%02x", string.byte(v)) end)

-- Count bytes-pairs (7). 61 D1 88 42 D0 A8 43
local messageLen = #messageBytes / 2
-- Create message length value-color (3 bytes). Add missing bytes.
local messageLenBytes = string.format("%06x", messageLen)
-- Create message lenght character-pixel.
local messageLenPixel = "|cff" .. messageLenBytes .. "·|r"

-- Divide message (7) into 3 byte groups and get ungrouped byte(s) • (7)-3-3=1 left • (61D188) (42D0A8) 43
local ungroupedBytes = math.fmod(messageLen, 3)
-- If ungrouped bytes left, add missing bytes. "00" or "0000".
if ungroupedBytes > 0 then
messageBytes = messageBytes .. string.rep("00", 3 - ungroupedBytes)
end
-- Get (3 byte) groups. Switch red color with blue (RGB > BGR). Create coloured pixels. |cff88D161·|r
local messagePixels = messageBytes:gsub("(..)(..)(..)", "|cff" .. "%3%2%1" .. "·|r")

-- Update message change indicator color for Monitor: add 5 to red, green and blue.
messageChanged = messageChanged + 5
-- Do not exceed color brightness #808080.
if messageChanged > 80 then messageChanged = 10 end
-- Create message change character-pixel. |cff101010·|r
local messageChangedPixel = "|cff" .. string.rep(messageChanged, 3) .. "·|r"

-- Create final message with prefixes: prefix(6px) + change(1px) + size(1px) + message(#px)
local message = messagePrefixPixels .. messageChangedPixel .. messageLenPixel .. messagePixels
-- Show message on display.
JocysCom_ColorMessageEditBox:SetText(message)
-- Remove message from table.
table.remove(messageTable, 1)
end

function JocysCom_SendMessageTimer()
if #messageTable > 0 then JocysCom_SendMessageFromTable() end
-- Wait #.# second and check messageTable.
C_Timer.After(messageInterval, JocysCom_SendMessageTimer)
end
JocysCom_SendMessageTimer()