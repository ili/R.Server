using System;
using System.Web.UI;
using System.Web.Security;

public partial class _Default : Page 
{
	protected void Page_Load(object sender, EventArgs e)
	{
		ViewState["list"] = Roles.DeleteRole("TeamMember", true);
	}
}
