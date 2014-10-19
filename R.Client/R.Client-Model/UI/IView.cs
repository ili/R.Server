using System;

namespace R.Client.Model
{
	public interface IView
	{
		void Show();
		void Hide();
		bool ShowDialog();
		event EventHandler Close;
	}
}
