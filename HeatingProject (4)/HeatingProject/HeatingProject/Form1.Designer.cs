namespace HeatingProject
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
            this.showAllButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.shortestSeasonButton = new System.Windows.Forms.Button();
            this.longestSeasonButton = new System.Windows.Forms.Button();
            this.sortButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // showAllButton
            // 
            this.showAllButton.Location = new System.Drawing.Point(29, 12);
            this.showAllButton.Name = "showAllButton";
            this.showAllButton.Size = new System.Drawing.Size(258, 34);
            this.showAllButton.TabIndex = 0;
            this.showAllButton.Text = "Всі котельні";
            this.showAllButton.UseVisualStyleBackColor = true;
            this.showAllButton.Click += new System.EventHandler(this.showAllButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(322, 12);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(432, 34);
            this.filterButton.TabIndex = 1;
            this.filterButton.Text = "Початок сезону після 15 жовтня";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // shortestSeasonButton
            // 
            this.shortestSeasonButton.Location = new System.Drawing.Point(29, 64);
            this.shortestSeasonButton.Name = "shortestSeasonButton";
            this.shortestSeasonButton.Size = new System.Drawing.Size(258, 34);
            this.shortestSeasonButton.TabIndex = 2;
            this.shortestSeasonButton.Text = "Найкоротший сезон";
            this.shortestSeasonButton.UseVisualStyleBackColor = true;
            this.shortestSeasonButton.Click += new System.EventHandler(this.shortestSeasonButton_Click);
            // 
            // longestSeasonButton
            // 
            this.longestSeasonButton.Location = new System.Drawing.Point(322, 64);
            this.longestSeasonButton.Name = "longestSeasonButton";
            this.longestSeasonButton.Size = new System.Drawing.Size(432, 34);
            this.longestSeasonButton.TabIndex = 3;
            this.longestSeasonButton.Text = "Тривалість сезону більше 6 місяців";
            this.longestSeasonButton.UseVisualStyleBackColor = true;
            this.longestSeasonButton.Click += new System.EventHandler(this.longestSeasonButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.Location = new System.Drawing.Point(29, 507);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(258, 34);
            this.sortButton.TabIndex = 4;
            this.sortButton.Text = "Сортувати і зберегти";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(29, 147);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(725, 347);
            this.dataGridView.TabIndex = 5;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(322, 507);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(258, 34);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Додати новий запис";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(618, 507);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(136, 34);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "Видалити";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(115, 110);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(172, 31);
            this.tbSearch.TabIndex = 8;
            this.tbSearch.Text = "";
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Пошук";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.longestSeasonButton);
            this.Controls.Add(this.shortestSeasonButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.showAllButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Журнал обліку роботи опалювальної системи";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button showAllButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button shortestSeasonButton;
        private System.Windows.Forms.Button longestSeasonButton;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.RichTextBox tbSearch;
        private System.Windows.Forms.Label label1;
    }
}

