namespace R.Common
{
	public static class CommonConstants
	{
		public static readonly string ProductName = "R.Server";

		/// <summary>
		/// Common copyright string.
		/// </summary>
		public static readonly string CopyrightString =
			"Copyright (C) 2006-2007 by RSDN Team (support@rsdn.ru)";

		/// <summary>
		/// Welcome string template for console applications.
		/// </summary>
		public static readonly string WelcomeStringTemplate =
			ProductName + ". {0}. " + CopyrightString + ".";

		/// <summary>
		/// Base XML namespace for all schemas.
		/// </summary>
		public const string BaseXmlNamespace = "http://rsdn.ru/R.Server/Schemas/";

		/// <summary>
		/// Het welcome string for specific application.
		/// </summary>
		/// <param name="appName">application name</param>
		public static string GetWelcomeString(string appName)
		{
			return string.Format(WelcomeStringTemplate, appName);
		}
	}
}