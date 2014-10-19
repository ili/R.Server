CREATE PROCEDURE Log_AddEvent
	@eventDate DATETIME,
	@eventType INT,
	@eventImpotance INT,
	@sourceName Log_SourceName,
	@message Log_Message
AS
	SET XACT_ABORT ON
	BEGIN TRANSACTION

	DECLARE @sourceID Global_PK;
	EXECUTE Log_GetSourceID @sourceName, @sourceID OUTPUT;
	INSERT INTO Log_Log VALUES(@eventDate, @eventType, @eventImpotance, @sourceID, @message);

	COMMIT TRANSACTION
RETURN 0;