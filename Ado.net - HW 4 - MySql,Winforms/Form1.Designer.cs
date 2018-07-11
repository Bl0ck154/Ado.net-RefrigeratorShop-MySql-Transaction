namespace Ado.net___HW_4___MySql_Winforms
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.comboBoxTables = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxSeller = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxBuyer = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxProducts = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(12, 287);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(552, 295);
			this.dataGridView1.TabIndex = 0;
			// 
			// comboBoxTables
			// 
			this.comboBoxTables.FormattingEnabled = true;
			this.comboBoxTables.Location = new System.Drawing.Point(66, 257);
			this.comboBoxTables.Name = "comboBoxTables";
			this.comboBoxTables.Size = new System.Drawing.Size(121, 24);
			this.comboBoxTables.TabIndex = 1;
			this.comboBoxTables.SelectedIndexChanged += new System.EventHandler(this.comboBoxTables_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 257);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Table:";
			// 
			// comboBoxSeller
			// 
			this.comboBoxSeller.FormattingEnabled = true;
			this.comboBoxSeller.Location = new System.Drawing.Point(63, 13);
			this.comboBoxSeller.Name = "comboBoxSeller";
			this.comboBoxSeller.Size = new System.Drawing.Size(121, 24);
			this.comboBoxSeller.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "Seller:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(261, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "Buyer:";
			// 
			// comboBoxBuyer
			// 
			this.comboBoxBuyer.FormattingEnabled = true;
			this.comboBoxBuyer.Location = new System.Drawing.Point(315, 13);
			this.comboBoxBuyer.Name = "comboBoxBuyer";
			this.comboBoxBuyer.Size = new System.Drawing.Size(121, 24);
			this.comboBoxBuyer.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(61, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "Product:";
			// 
			// comboBoxProducts
			// 
			this.comboBoxProducts.FormattingEnabled = true;
			this.comboBoxProducts.Location = new System.Drawing.Point(92, 76);
			this.comboBoxProducts.Name = "comboBoxProducts";
			this.comboBoxProducts.Size = new System.Drawing.Size(445, 24);
			this.comboBoxProducts.TabIndex = 8;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(108, 156);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(328, 42);
			this.button1.TabIndex = 9;
			this.button1.Text = "Order";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(576, 594);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBoxProducts);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBoxBuyer);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboBoxSeller);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxTables);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Form1";
			this.Text = "Refregirator Order";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ComboBox comboBoxTables;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxSeller;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxBuyer;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxProducts;
		private System.Windows.Forms.Button button1;
	}
}

