namespace IBTrader
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.date2 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ıdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tickerIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.askDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hourDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntraDayDatabds = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntraDayDatabds)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(280, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // date1
            // 
            this.date1.CustomFormat = "dd.MM.yyyy HH:mm";
            this.date1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date1.Location = new System.Drawing.Point(12, 12);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(132, 20);
            this.date1.TabIndex = 1;
            this.date1.Value = new System.DateTime(2016, 6, 1, 9, 30, 0, 0);
            // 
            // date2
            // 
            this.date2.CustomFormat = "dd.MM.yyyy HH:mm";
            this.date2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date2.Location = new System.Drawing.Point(150, 12);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(124, 20);
            this.date2.TabIndex = 2;
            this.date2.Value = new System.DateTime(2016, 6, 1, 10, 30, 0, 0);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ıdDataGridViewTextBoxColumn,
            this.tickerIdDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.lastDataGridViewTextBoxColumn,
            this.askDataGridViewTextBoxColumn,
            this.bidDataGridViewTextBoxColumn,
            this.volDataGridViewTextBoxColumn,
            this.hourDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.IntraDayDatabds;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(861, 383);
            this.dataGridView1.TabIndex = 3;
            // 
            // ıdDataGridViewTextBoxColumn
            // 
            this.ıdDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.ıdDataGridViewTextBoxColumn.HeaderText = "Id";
            this.ıdDataGridViewTextBoxColumn.Name = "ıdDataGridViewTextBoxColumn";
            // 
            // tickerIdDataGridViewTextBoxColumn
            // 
            this.tickerIdDataGridViewTextBoxColumn.DataPropertyName = "TickerId";
            this.tickerIdDataGridViewTextBoxColumn.HeaderText = "TickerId";
            this.tickerIdDataGridViewTextBoxColumn.Name = "tickerIdDataGridViewTextBoxColumn";
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // lastDataGridViewTextBoxColumn
            // 
            this.lastDataGridViewTextBoxColumn.DataPropertyName = "Last";
            this.lastDataGridViewTextBoxColumn.HeaderText = "Last";
            this.lastDataGridViewTextBoxColumn.Name = "lastDataGridViewTextBoxColumn";
            // 
            // askDataGridViewTextBoxColumn
            // 
            this.askDataGridViewTextBoxColumn.DataPropertyName = "Ask";
            this.askDataGridViewTextBoxColumn.HeaderText = "Ask";
            this.askDataGridViewTextBoxColumn.Name = "askDataGridViewTextBoxColumn";
            // 
            // bidDataGridViewTextBoxColumn
            // 
            this.bidDataGridViewTextBoxColumn.DataPropertyName = "Bid";
            this.bidDataGridViewTextBoxColumn.HeaderText = "Bid";
            this.bidDataGridViewTextBoxColumn.Name = "bidDataGridViewTextBoxColumn";
            // 
            // volDataGridViewTextBoxColumn
            // 
            this.volDataGridViewTextBoxColumn.DataPropertyName = "Vol";
            this.volDataGridViewTextBoxColumn.HeaderText = "Vol";
            this.volDataGridViewTextBoxColumn.Name = "volDataGridViewTextBoxColumn";
            // 
            // hourDataGridViewTextBoxColumn
            // 
            this.hourDataGridViewTextBoxColumn.DataPropertyName = "Hour";
            this.hourDataGridViewTextBoxColumn.HeaderText = "Hour";
            this.hourDataGridViewTextBoxColumn.Name = "hourDataGridViewTextBoxColumn";
            // 
            // IntraDayDatabds
            // 
            this.IntraDayDatabds.DataSource = typeof(IB.Db.Model.MdlIntraDayData);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 433);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.date2);
            this.Controls.Add(this.date1);
            this.Controls.Add(this.button1);
            this.Name = "FrmMain";
            this.Text = "IBTrader";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntraDayDatabds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.DateTimePicker date2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource IntraDayDatabds;
        private System.Windows.Forms.DataGridViewTextBoxColumn ıdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tickerIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn askDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn volDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hourDataGridViewTextBoxColumn;
    }
}

