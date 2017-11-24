namespace restSharpTesting
{
	partial class Form1
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
			this.authenticateButton = new System.Windows.Forms.Button();
			this.returnLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// authenticateButton
			// 
			this.authenticateButton.Location = new System.Drawing.Point(100, 182);
			this.authenticateButton.Name = "authenticateButton";
			this.authenticateButton.Size = new System.Drawing.Size(75, 23);
			this.authenticateButton.TabIndex = 0;
			this.authenticateButton.Text = "Authenticate";
			this.authenticateButton.UseVisualStyleBackColor = true;
			this.authenticateButton.Click += new System.EventHandler(this.authenticateButton_Click);
			// 
			// returnLabel
			// 
			this.returnLabel.AutoSize = true;
			this.returnLabel.Location = new System.Drawing.Point(97, 80);
			this.returnLabel.Name = "returnLabel";
			this.returnLabel.Size = new System.Drawing.Size(0, 13);
			this.returnLabel.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.returnLabel);
			this.Controls.Add(this.authenticateButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button authenticateButton;
		private System.Windows.Forms.Label returnLabel;
	}
}

