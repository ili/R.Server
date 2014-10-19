ALTER TABLE Log_Log
	ADD CONSTRAINT CK_Log_Log_EventImpotance
	CHECK  (EventImpotance >= 1 AND EventImpotance <= 3)
