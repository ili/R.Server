CREATE FUNCTION Auth_GetPasswordHash
(
	@userName VARCHAR(64)
)
RETURNS BINARY(20)
AS
BEGIN
	DECLARE @passwordHash BINARY(20);
	SELECT @passwordHash = PasswordHash FROM Auth_NativeLogin WHERE UserName = @userName;
	RETURN @passwordHash;
END