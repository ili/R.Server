CREATE VIEW Log_LogView
AS 
	SELECT
		EventDate, EventType, EventImpotance, SourceName, Message
	FROM
		Log_Log l
			INNER JOIN Log_Source s ON l.SourceID = s.SourceID;
;