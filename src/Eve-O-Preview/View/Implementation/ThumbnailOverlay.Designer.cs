namespace EveOPreview.View
{
	partial class ThumbnailOverlay
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
			if (disposing)
			{
				this.ClearPortrait();
				components?.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThumbnailOverlay));
			OverlayAreaPictureBox = new System.Windows.Forms.PictureBox();
			OverlayLabel = new System.Windows.Forms.Label();
			CycleGroupIndicator = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)OverlayAreaPictureBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)CycleGroupIndicator).BeginInit();
			SuspendLayout();
			// 
			// OverlayAreaPictureBox
			// 
			OverlayAreaPictureBox.BackColor = System.Drawing.Color.FromArgb(0, 0, 1);
			OverlayAreaPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			OverlayAreaPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			OverlayAreaPictureBox.Location = new System.Drawing.Point(0, 0);
			OverlayAreaPictureBox.Name = "OverlayAreaPictureBox";
			OverlayAreaPictureBox.Size = new System.Drawing.Size(438, 351);
			OverlayAreaPictureBox.TabIndex = 0;
			OverlayAreaPictureBox.TabStop = false;
			OverlayAreaPictureBox.Paint += OverlayAreaPictureBox_Paint;
			OverlayAreaPictureBox.MouseDown += OverlayArea_MouseDown;
			OverlayAreaPictureBox.MouseEnter += OverlayArea_MouseEnter;
			OverlayAreaPictureBox.MouseLeave += OverlayArea_MouseLeave;
			OverlayAreaPictureBox.MouseMove += OverlayArea_MouseMove;
			OverlayAreaPictureBox.MouseUp += OverlayArea_MouseUp;
			// 
			// OverlayLabel
			// 
			OverlayLabel.AutoSize = true;
			OverlayLabel.BackColor = System.Drawing.Color.FromArgb(0, 0, 1);
			OverlayLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			OverlayLabel.ForeColor = System.Drawing.Color.DarkGray;
			OverlayLabel.Location = new System.Drawing.Point(8, 8);
			OverlayLabel.Name = "OverlayLabel";
			OverlayLabel.Size = new System.Drawing.Size(25, 13);
			OverlayLabel.TabIndex = 1;
			OverlayLabel.Text = "...";
			OverlayLabel.Visible = false;
			OverlayLabel.MouseDown += OverlayArea_MouseDown;
			OverlayLabel.MouseEnter += OverlayArea_MouseEnter;
			OverlayLabel.MouseLeave += OverlayArea_MouseLeave;
			OverlayLabel.MouseMove += OverlayArea_MouseMove;
			OverlayLabel.MouseUp += OverlayArea_MouseUp;
			// 
			// CycleGroupIndicator
			// 
			CycleGroupIndicator.BackColor = System.Drawing.Color.Transparent;
			CycleGroupIndicator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			CycleGroupIndicator.Image = (System.Drawing.Image)resources.GetObject("CycleGroupIndicator.Image");
			CycleGroupIndicator.Location = new System.Drawing.Point(354, 12);
			CycleGroupIndicator.Name = "CycleGroupIndicator";
			CycleGroupIndicator.Size = new System.Drawing.Size(72, 78);
			CycleGroupIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			CycleGroupIndicator.TabIndex = 3;
			CycleGroupIndicator.TabStop = false;
			CycleGroupIndicator.MouseDown += OverlayArea_MouseDown;
			CycleGroupIndicator.MouseEnter += OverlayArea_MouseEnter;
			CycleGroupIndicator.MouseLeave += OverlayArea_MouseLeave;
			CycleGroupIndicator.MouseMove += OverlayArea_MouseMove;
			CycleGroupIndicator.MouseUp += OverlayArea_MouseUp;
			// 
			// ThumbnailOverlay
			// 
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			BackColor = System.Drawing.Color.FromArgb(0, 0, 1);
			ClientSize = new System.Drawing.Size(438, 351);
			ControlBox = false;
			Controls.Add(OverlayLabel);
			Controls.Add(CycleGroupIndicator);
			Controls.Add(OverlayAreaPictureBox);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ThumbnailOverlay";
			ShowIcon = false;
			ShowInTaskbar = false;
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			Text = "PreviewOverlay";
			TransparencyKey = System.Drawing.Color.FromArgb(0, 0, 1);
			((System.ComponentModel.ISupportInitialize)OverlayAreaPictureBox).EndInit();
			((System.ComponentModel.ISupportInitialize)CycleGroupIndicator).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label OverlayLabel;
		private System.Windows.Forms.PictureBox OverlayAreaPictureBox;
		private System.Windows.Forms.PictureBox CycleGroupIndicator;
	}
}