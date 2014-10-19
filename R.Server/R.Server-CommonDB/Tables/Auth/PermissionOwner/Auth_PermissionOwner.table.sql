CREATE TABLE Auth_PermissionOwner
(
	PermissionOwnerID UNIQUEIDENTIFIER NOT NULL,
	PermissionOwnerType INT NOT NULL -- 1 - Principal, 2 - Role
);
