/*
	Native logins table
*/
CREATE TABLE Auth_NativeLogin
(
	NativeLoginID UNIQUEIDENTIFIER NOT NULL,
	UserName VARCHAR(64) NOT NULL,
	PasswordHash BINARY(20) -- The hash size for the SHA1 algorithm is 160 bits = 20 bytes.
);
