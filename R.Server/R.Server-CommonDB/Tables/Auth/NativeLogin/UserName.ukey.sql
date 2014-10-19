ALTER TABLE Auth_NativeLogin
ADD CONSTRAINT [UK_Auth_NativeLogin_UserName]
UNIQUE (UserName)