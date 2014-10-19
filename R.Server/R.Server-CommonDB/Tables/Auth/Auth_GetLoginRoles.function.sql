CREATE FUNCTION Auth_GetLoginRoles
(
	@authenType VARCHAR(64),
	@identityName VARCHAR(64)
)
RETURNS TABLE 
RETURN SELECT
		r.RoleID, r.RoleName
	FROM
		Auth_Principal p
			INNER JOIN Auth_PrincipalGroup pg ON p.PrincipalID = pg.PrincipalID
			INNER JOIN Auth_GroupRole gr ON pg.GroupID = gr.GroupID
			INNER JOIN Auth_Role r ON gr.RoleID = r.RoleID
			INNER JOIN Auth_PrincipalLogin pl ON p.PrincipalID = pl.PrincipalID
	WHERE
		pl.AuthenticationType = @authenType AND pl.IdentityName = @identityName