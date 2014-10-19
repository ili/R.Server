namespace R.Client.Model
{
	/// <summary>
	/// Client application interface.
	/// </summary>
	public interface IClientApplication
	{
		object CreateAppModel();

		AppEntryPoint[] GetEntryPoints();

		void ExecuteEntryPoint(string name);
	}
}
