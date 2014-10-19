ALTER TABLE Log_Log
	ADD CONSTRAINT FK_Log_Log_Log_Source
	FOREIGN KEY (SourceID)
	REFERENCES Log_Source (SourceID)	

