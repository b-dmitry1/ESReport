namespace ESReport
{
	partial class MainForm
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.PreviewPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SearchTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.PrintButton = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.Color.Gray;
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.PreviewPanel);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(1008, 510);
			this.splitContainer1.SplitterDistance = 390;
			this.splitContainer1.SplitterWidth = 8;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.TabStop = false;
			// 
			// PreviewPanel
			// 
			this.PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PreviewPanel.Location = new System.Drawing.Point(0, 34);
			this.PreviewPanel.Name = "PreviewPanel";
			this.PreviewPanel.Size = new System.Drawing.Size(608, 474);
			this.PreviewPanel.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.SearchTextBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.PrintButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(608, 34);
			this.panel1.TabIndex = 0;
			// 
			// SearchTextBox
			// 
			this.SearchTextBox.Location = new System.Drawing.Point(149, 6);
			this.SearchTextBox.Name = "SearchTextBox";
			this.SearchTextBox.Size = new System.Drawing.Size(163, 20);
			this.SearchTextBox.TabIndex = 2;
			this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(99, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Search:";
			// 
			// PrintButton
			// 
			this.PrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.PrintButton.Location = new System.Drawing.Point(3, 3);
			this.PrintButton.Name = "PrintButton";
			this.PrintButton.Size = new System.Drawing.Size(75, 26);
			this.PrintButton.TabIndex = 0;
			this.PrintButton.TabStop = false;
			this.PrintButton.Text = "PRINT";
			this.PrintButton.UseVisualStyleBackColor = true;
			this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// printDialog1
			// 
			this.printDialog1.UseEXDialog = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 510);
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainForm";
			this.Text = "Report";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel PreviewPanel;
		private System.Windows.Forms.Button PrintButton;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.TextBox SearchTextBox;
		private System.Windows.Forms.Label label3;


	}
}

