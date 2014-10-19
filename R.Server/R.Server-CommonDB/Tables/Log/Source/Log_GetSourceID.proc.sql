CREATE PROCEDURE Log_GetSourceID
	@sourceName Log_SourceName,
	@id UNIQUEIDENTIFIER OUTPUT
AS
	SELECT @id = SourceID FROM Log_Source WHERE SourceName = @sourceName;

	IF @id IS NULL
	BEGIN
		SELECT @id = NEWID();
		INSERT INTO Log_Source VALUES(@id, @sourceName);
	END
RETURN 0;