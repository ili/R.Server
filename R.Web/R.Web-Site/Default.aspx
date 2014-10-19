﻿<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:LoginName ID="LoginName1" runat="server" />
		<asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8"
			BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
			DisableCreatedUser="false" DisplayCancelButton="True" DisplaySideBar="True">
			<WizardSteps>
				<asp:CreateUserWizardStep runat="server">
				</asp:CreateUserWizardStep>
				<asp:CompleteWizardStep runat="server">
				</asp:CompleteWizardStep>
			</WizardSteps>
			<SideBarStyle BackColor="#5D7B9D" BorderWidth="0px" Font-Size="0.9em" VerticalAlign="Top" />
			<TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
			<SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
			<NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
				BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
			<HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" Font-Size="0.9em"
				ForeColor="White" HorizontalAlign="Center" />
			<CreateUserButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
				BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
			<ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
				BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
			<StepStyle BorderWidth="0px" />
		</asp:CreateUserWizard>

	</div>
	</form>
</body>
</html>
