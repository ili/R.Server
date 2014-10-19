using System;

namespace R.Client.Model
{
	public interface IUIController
	{
		void Show();
		void Hide();
		bool ShowDialog();
		event EventHandler Close;
	}
}
