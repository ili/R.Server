namespace R.Client.Model
{
	public interface IUIManagerService
	{
		IUIController CreateControllerForModel(object model);

		C CreateView<C>() where C : IView;
	}
}
