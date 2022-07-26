namespace IDDC
{
    partial class MainWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnImport = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lblFilename = new System.Windows.Forms.Label();
            this.DepthDoseCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.clsbFiles = new System.Windows.Forms.CheckedListBox();
            this.clsbMeasurements = new System.Windows.Forms.CheckedListBox();
            this.cbEnableAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepthDoseCurve)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 960);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(101, 40);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 38);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(1660, 439);
            this.dataGridView.TabIndex = 1;
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(13, 13);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(0, 13);
            this.lblFilename.TabIndex = 2;
            // 
            // DepthDoseCurve
            // 
            this.DepthDoseCurve.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.DepthDoseCurve.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.DepthDoseCurve.Legends.Add(legend1);
            this.DepthDoseCurve.Location = new System.Drawing.Point(360, 483);
            this.DepthDoseCurve.Name = "DepthDoseCurve";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.DepthDoseCurve.Series.Add(series1);
            this.DepthDoseCurve.Size = new System.Drawing.Size(1312, 517);
            this.DepthDoseCurve.TabIndex = 3;
            this.DepthDoseCurve.Text = "chart1";
            this.DepthDoseCurve.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DepthDoseCurve_MouseMove);
            // 
            // clsbFiles
            // 
            this.clsbFiles.CheckOnClick = true;
            this.clsbFiles.FormattingEnabled = true;
            this.clsbFiles.Location = new System.Drawing.Point(12, 483);
            this.clsbFiles.Name = "clsbFiles";
            this.clsbFiles.Size = new System.Drawing.Size(342, 94);
            this.clsbFiles.TabIndex = 4;
            this.clsbFiles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clsbFiles_ItemCheck);
            this.clsbFiles.SelectedIndexChanged += new System.EventHandler(this.clsbFiles_SelectedIndexChanged);
            // 
            // clsbMeasurements
            // 
            this.clsbMeasurements.CheckOnClick = true;
            this.clsbMeasurements.FormattingEnabled = true;
            this.clsbMeasurements.Location = new System.Drawing.Point(12, 613);
            this.clsbMeasurements.Name = "clsbMeasurements";
            this.clsbMeasurements.Size = new System.Drawing.Size(342, 334);
            this.clsbMeasurements.TabIndex = 5;
            this.clsbMeasurements.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clsbMeasurements_ItemCheck);
            this.clsbMeasurements.SelectedIndexChanged += new System.EventHandler(this.clsbMeasurements_SelectedIndexChanged);
            // 
            // cbEnableAll
            // 
            this.cbEnableAll.AutoSize = true;
            this.cbEnableAll.Location = new System.Drawing.Point(12, 590);
            this.cbEnableAll.Name = "cbEnableAll";
            this.cbEnableAll.Size = new System.Drawing.Size(73, 17);
            this.cbEnableAll.TabIndex = 6;
            this.cbEnableAll.Text = "Enable All";
            this.cbEnableAll.UseVisualStyleBackColor = true;
            this.cbEnableAll.CheckedChanged += new System.EventHandler(this.cbEnableAll_CheckedChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1684, 1012);
            this.Controls.Add(this.cbEnableAll);
            this.Controls.Add(this.clsbMeasurements);
            this.Controls.Add(this.clsbFiles);
            this.Controls.Add(this.DepthDoseCurve);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnImport);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BraggPeakAnalyser (v 1.0 by Erik Fura)";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepthDoseCurve)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.DataVisualization.Charting.Chart DepthDoseCurve;
        private System.Windows.Forms.CheckedListBox clsbFiles;
        private System.Windows.Forms.CheckedListBox clsbMeasurements;
        private System.Windows.Forms.CheckBox cbEnableAll;
    }
}

