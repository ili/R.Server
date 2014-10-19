CREATE TABLE User_User
(
	UserID UNIQUEIDENTIFIER NOT NULL,
	PrincipalID UNIQUEIDENTIFIER, -- Can be null (robot users, for example)
	Nick VARCHAR(128) NOT NULL
);
