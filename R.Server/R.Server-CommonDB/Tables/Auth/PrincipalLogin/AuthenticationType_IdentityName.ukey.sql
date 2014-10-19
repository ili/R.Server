ALTER TABLE Auth_PrincipalLogin
ADD CONSTRAINT UK_Auth_PrincipalLogin_AuthenticationType_IdentityName
UNIQUE (AuthenticationType, IdentityName);


