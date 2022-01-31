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
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.ReportTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.DataTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.PreviewPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.PrintButton = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.SearchTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.PreviewPanel);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(1086, 510);
			this.splitContainer1.SplitterDistance = 421;
			this.splitContainer1.SplitterWidth = 8;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.TabStop = false;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.ReportTextBox);
			this.splitContainer2.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.DataTextBox);
			this.splitContainer2.Panel2.Controls.Add(this.label2);
			this.splitContainer2.Size = new System.Drawing.Size(421, 510);
			this.splitContainer2.SplitterDistance = 271;
			this.splitContainer2.TabIndex = 0;
			this.splitContainer2.TabStop = false;
			// 
			// ReportTextBox
			// 
			this.ReportTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReportTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ReportTextBox.Location = new System.Drawing.Point(0, 19);
			this.ReportTextBox.Multiline = true;
			this.ReportTextBox.Name = "ReportTextBox";
			this.ReportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ReportTextBox.Size = new System.Drawing.Size(417, 248);
			this.ReportTextBox.TabIndex = 1;
			this.ReportTextBox.WordWrap = false;
			this.ReportTextBox.TextChanged += new System.EventHandler(this.ReportTextBox_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(5);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(3);
			this.label1.Size = new System.Drawing.Size(45, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "Report";
			// 
			// DataTextBox
			// 
			this.DataTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DataTextBox.Location = new System.Drawing.Point(0, 19);
			this.DataTextBox.Multiline = true;
			this.DataTextBox.Name = "DataTextBox";
			this.DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.DataTextBox.Size = new System.Drawing.Size(417, 212);
			this.DataTextBox.TabIndex = 3;
			this.DataTextBox.WordWrap = false;
			this.DataTextBox.TextChanged += new System.EventHandler(this.ReportTextBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(5);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(3);
			this.label2.Size = new System.Drawing.Size(36, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "Data";
			// 
			// PreviewPanel
			// 
			this.PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PreviewPanel.Location = new System.Drawing.Point(0, 34);
			this.PreviewPanel.Name = "PreviewPanel";
			this.PreviewPanel.Size = new System.Drawing.Size(653, 472);
			this.PreviewPanel.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.SearchTextBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.PrintButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(653, 34);
			this.panel1.TabIndex = 0;
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
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(99, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Search:";
			// 
			// SearchTextBox
			// 
			this.SearchTextBox.Location = new System.Drawing.Point(149, 6);
			this.SearchTextBox.Name = "SearchTextBox";
			this.SearchTextBox.Size = new System.Drawing.Size(163, 20);
			this.SearchTextBox.TabIndex = 2;
			this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1086, 510);
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainForm";
			this.Text = "Report";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TextBox ReportTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox DataTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel PreviewPanel;
		private System.Windows.Forms.Button PrintButton;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.TextBox SearchTextBox;
		private System.Windows.Forms.Label label3;


	}
}

