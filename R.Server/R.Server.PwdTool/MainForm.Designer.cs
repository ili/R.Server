namespace R.Server.PwdTool
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Label pwdLabel;
			System.Windows.Forms.Label hexLabel;
			System.Windows.Forms.Label base64Label;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._pwdBox = new System.Windows.Forms.TextBox();
			this._closeButton = new System.Windows.Forms.Button();
			this._hexBox = new System.Windows.Forms.TextBox();
			this._base64Box = new System.Windows.Forms.TextBox();
			this._timeLabel = new System.Windows.Forms.Label();
			pwdLabel = new System.Windows.Forms.Label();
			hexLabel = new System.Windows.Forms.Label();
			base64Label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _pwdBox
			// 
			this._pwdBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._pwdBox.Location = new System.Drawing.Point(12, 25);
			this._pwdBox.Name = "_pwdBox";
			this._pwdBox.Size = new System.Drawing.Size(378, 20);
			this._pwdBox.TabIndex = 0;
			this._pwdBox.TextChanged += new System.EventHandler(this._pwdBox_TextChanged);
			// 
			// pwdLabel
			// 
			pwdLabel.AutoSize = true;
			pwdLabel.Location = new System.Drawing.Point(12, 9);
			pwdLabel.Name = "pwdLabel";
			pwdLabel.Size = new System.Drawing.Size(56, 13);
			pwdLabel.TabIndex = 1;
			pwdLabel.Text = "Password:";
			// 
			// _closeButton
			// 
			this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._closeButton.Location = new System.Drawing.Point(315, 335);
			this._closeButton.Name = "_closeButton";
			this._closeButton.Size = new System.Drawing.Size(75, 23);
			this._closeButton.TabIndex = 2;
			this._closeButton.Text = "Close";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
			// 
			// hexLabel
			// 
			hexLabel.AutoSize = true;
			hexLabel.Location = new System.Drawing.Point(12, 48);
			hexLabel.Name = "hexLabel";
			hexLabel.Size = new System.Drawing.Size(55, 13);
			hexLabel.TabIndex = 3;
			hexLabel.Text = "Hex hash:";
			// 
			// _hexBox
			// 
			this._hexBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._hexBox.Location = new System.Drawing.Point(12, 64);
			this._hexBox.Name = "_hexBox";
			this._hexBox.ReadOnly = true;
			this._hexBox.Size = new System.Drawing.Size(378, 20);
			this._hexBox.TabIndex = 4;
			// 
			// _base64Box
			// 
			this._base64Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._base64Box.Location = new System.Drawing.Point(12, 103);
			this._base64Box.Name = "_base64Box";
			this._base64Box.ReadOnly = true;
			this._base64Box.Size = new System.Drawing.Size(378, 20);
			this._base64Box.TabIndex = 6;
			// 
			// base64Label
			// 
			base64Label.AutoSize = true;
			base64Label.Location = new System.Drawing.Point(13, 87);
			base64Label.Name = "base64Label";
			base64Label.Size = new System.Drawing.Size(72, 13);
			base64Label.TabIndex = 5;
			base64Label.Text = "Base64 hash:";
			// 
			// _timeLabel
			// 
			this._timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this._timeLabel.Location = new System.Drawing.Point(9, 126);
			this._timeLabel.Name = "_timeLabel";
			this._timeLabel.Size = new System.Drawing.Size(381, 17);
			this._timeLabel.TabIndex = 7;
			// 
			// MainForm
			// 
			this.AcceptButton = this._closeButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._closeButton;
			this.ClientSize = new System.Drawing.Size(402, 370);
			this.Controls.Add(this._timeLabel);
			this.Controls.Add(this._base64Box);
			this.Controls.Add(base64Label);
			this.Controls.Add(this._hexBox);
			this.Controls.Add(hexLabel);
			this.Controls.Add(this._closeButton);
			this.Controls.Add(pwdLabel);
			this.Controls.Add(this._pwdBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "R.Server Password Tool";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _pwdBox;
		private System.Windows.Forms.Button _closeButton;
		private System.Windows.Forms.TextBox _hexBox;
		private System.Windows.Forms.TextBox _base64Box;
		private System.Windows.Forms.Label _timeLabel;
	}
}

