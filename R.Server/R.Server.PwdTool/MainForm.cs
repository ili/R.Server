using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace R.Server.PwdTool
{
	public partial class MainForm : Form
	{
		private readonly SHA1Managed _sha1 = new SHA1Managed();

		public MainForm()
		{
			InitializeComponent();
		}

		private void _closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void _pwdBox_TextChanged(object sender, EventArgs e)
		{
			var src = Encoding.UTF8.GetBytes(_pwdBox.Text);
			var sw = new Stopwatch();
			sw.Start();
			byte[] res = null;
			const int pwds = 10000;
			for (var i = 0; i < pwds; i++)
				res = _sha1.ComputeHash(src);
			sw.Stop();
			_timeLabel.Text = string.Format("Compute time: {0} ms per {1} passwords",
				sw.ElapsedMilliseconds, pwds);

			var sb = new StringBuilder("0x");
			foreach (var b in res)
				sb.AppendFormat("{0:X}", b);
			_hexBox.Text = sb.ToString();

			_base64Box.Text = Convert.ToBase64String(res);
		}
	}
}