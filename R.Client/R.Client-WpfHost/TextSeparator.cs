using System.Windows;
using System.Windows.Controls;

namespace R.Client.WpfHost
{
	public class TextSeparator : Separator
	{
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof (string), typeof (TextSeparator));


		public TextSeparator()
		{
			Style = (Style)FindResource(typeof (TextSeparator));
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value);}
		}
	}
}
