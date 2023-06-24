namespace winformdbg3
{
    partial class GraphForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartTemp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartHumi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAmmo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCO2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAct = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnClose = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHumi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAmmo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCO2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAct)).BeginInit();
            this.SuspendLayout();
            // 
            // chartTemp
            // 
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.chartTemp.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartTemp.Legends.Add(legend1);
            this.chartTemp.Location = new System.Drawing.Point(23, 63);
            this.chartTemp.Name = "chartTemp";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTemp.Series.Add(series1);
            this.chartTemp.Size = new System.Drawing.Size(1272, 138);
            this.chartTemp.TabIndex = 21;
            this.chartTemp.Text = "chart1";
            // 
            // chartHumi
            // 
            chartArea2.AxisY.Maximum = 100D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "ChartArea1";
            this.chartHumi.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartHumi.Legends.Add(legend2);
            this.chartHumi.Location = new System.Drawing.Point(23, 207);
            this.chartHumi.Name = "chartHumi";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartHumi.Series.Add(series2);
            this.chartHumi.Size = new System.Drawing.Size(1272, 138);
            this.chartHumi.TabIndex = 22;
            this.chartHumi.Text = "chart2";
            // 
            // chartAmmo
            // 
            chartArea3.AxisY.Maximum = 100D;
            chartArea3.AxisY.Minimum = 0D;
            chartArea3.Name = "ChartArea1";
            this.chartAmmo.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartAmmo.Legends.Add(legend3);
            this.chartAmmo.Location = new System.Drawing.Point(23, 351);
            this.chartAmmo.Name = "chartAmmo";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartAmmo.Series.Add(series3);
            this.chartAmmo.Size = new System.Drawing.Size(1272, 138);
            this.chartAmmo.TabIndex = 23;
            this.chartAmmo.Text = "chart3";
            // 
            // chartCO2
            // 
            chartArea4.AxisY.Maximum = 100D;
            chartArea4.AxisY.Minimum = 0D;
            chartArea4.Name = "ChartArea1";
            this.chartCO2.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartCO2.Legends.Add(legend4);
            this.chartCO2.Location = new System.Drawing.Point(23, 495);
            this.chartCO2.Name = "chartCO2";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartCO2.Series.Add(series4);
            this.chartCO2.Size = new System.Drawing.Size(1272, 138);
            this.chartCO2.TabIndex = 24;
            this.chartCO2.Text = "chart4";
            // 
            // chartAct
            // 
            chartArea5.AxisY.Maximum = 100D;
            chartArea5.AxisY.Minimum = 0D;
            chartArea5.Name = "ChartArea1";
            this.chartAct.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartAct.Legends.Add(legend5);
            this.chartAct.Location = new System.Drawing.Point(23, 639);
            this.chartAct.Name = "chartAct";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartAct.Series.Add(series5);
            this.chartAct.Size = new System.Drawing.Size(1272, 138);
            this.chartAct.TabIndex = 25;
            this.chartAct.Text = "chart5";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1179, 783);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 829);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chartAct);
            this.Controls.Add(this.chartCO2);
            this.Controls.Add(this.chartAmmo);
            this.Controls.Add(this.chartHumi);
            this.Controls.Add(this.chartTemp);
            this.Name = "GraphForm";
            this.Text = "그래프";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GraphForm_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.GraphForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHumi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAmmo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCO2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemp;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHumi;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAmmo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCO2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAct;
        private MetroFramework.Controls.MetroButton btnClose;
    }
}