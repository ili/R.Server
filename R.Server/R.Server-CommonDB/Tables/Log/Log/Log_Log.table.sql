CREATE TABLE Log_Log
(
	EventDate DATETIME NOT NULL,
	EventType INT NOT NULL,
		-- 1 - Information,
		-- 2 - Warning,
		-- 3 - Error,
		-- 4 - CriticalError
	EventImpotance INT NOT NULL,
		-- 1 - Unimportant,
		-- 2 - Important,
		-- 3 - VeryImportant
	SourceID UNIQUEIDENTIFIER NOT NULL,
	Message Log_Message
);
