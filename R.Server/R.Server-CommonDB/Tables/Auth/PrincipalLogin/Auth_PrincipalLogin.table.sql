CREATE TABLE Auth_PrincipalLogin
(
	PrincipalID UNIQUEIDENTIFIER NOT NULL,
	AuthenticationType VARCHAR(64) NOT NULL,
	IdentityName VARCHAR(64) NOT NULL
);
