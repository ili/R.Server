CREATE TABLE User_PublicBaseProfile
(
	PublicBaseProfileID UNIQUEIDENTIFIER NOT NULL,
	ContentType INT NOT NULL,
	RealName NVARCHAR(255),
	Origin NVARCHAR(255)
);
