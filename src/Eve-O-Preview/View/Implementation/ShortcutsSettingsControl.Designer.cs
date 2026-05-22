namespace EveOPreview.View
{
	partial class ShortcutsSettingsControl
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.ScrollPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			//
			// ScrollPanel
			//
			this.ScrollPanel.AutoScroll = true;
			this.ScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScrollPanel.Location = new System.Drawing.Point(0, 0);
			this.ScrollPanel.Name = "ScrollPanel";
			this.ScrollPanel.Size = new System.Drawing.Size(400, 300);
			this.ScrollPanel.TabIndex = 0;
			//
			// ShortcutsSettingsControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ScrollPanel);
			this.Name = "ShortcutsSettingsControl";
			this.Size = new System.Drawing.Size(400, 300);
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Panel ScrollPanel;
	}
}
