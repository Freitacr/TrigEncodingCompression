namespace TrigEncodingCompression
{
    partial class BarCharCheck
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
            this.ByteViewingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ByteViewingChart)).BeginInit();
            this.SuspendLayout();
            // 
            // ByteViewingChart
            // 
            chartArea1.Name = "ChartArea1";
            this.ByteViewingChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ByteViewingChart.Legends.Add(legend1);
            this.ByteViewingChart.Location = new System.Drawing.Point(13, 13);
            this.ByteViewingChart.Name = "ByteViewingChart";
            this.ByteViewingChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.ByteViewingChart.Size = new System.Drawing.Size(1159, 836);
            this.ByteViewingChart.TabIndex = 0;
            this.ByteViewingChart.Text = "chart1";
            // 
            // BarCharCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.ByteViewingChart);
            this.Name = "BarCharCheck";
            this.Text = "BarCharCheck";
            ((System.ComponentModel.ISupportInitialize)(this.ByteViewingChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ByteViewingChart;
    }
}