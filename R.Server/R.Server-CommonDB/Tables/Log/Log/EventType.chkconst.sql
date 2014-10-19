ALTER TABLE Log_Log
	ADD CONSTRAINT CK_Log_Log_EventType
	CHECK  (EventType >= 1 AND EventType <= 4)
