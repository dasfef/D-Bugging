using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json;

namespace winformdbg3
{
    public partial class GraphForm : MetroForm
    {

        private static readonly HttpClient client = new HttpClient();
        private System.Timers.Timer _timer;
        CancellationTokenSource cts = new CancellationTokenSource();

        public GraphForm()
        {
            InitializeComponent();
            this.Load += Form2_Load; // 이벤트 핸들러 등록
        }


        private void Form2_Load(object sender, EventArgs e)
        {


        }

        private void startTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += async (sender, e) => await LoadSensorData(cts.Token);
            _timer.Start();
        }

        public async Task LoadSensorData(CancellationToken ct)
        {
            try
            {
                while (true)
                {

                    ct.ThrowIfCancellationRequested();
                    Debug.WriteLine("센서값 받아오기 시작");
                    string url = "http://175.205.128.40:9797/getGraphData.php";
                    string response = await client.GetStringAsync(url);
                    var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(response);

                    if (this.IsDisposed || !this.IsHandleCreated)
                    {
                        return;
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        // 데이터가 있는 경우, 차트를 업데이트합니다.
                        if (data.Count > 0)
                        {
                            // 차트를 초기화합니다.
                            chartTemp.Series.Clear();
                            chartTemp.Series.Add("온도");
                            chartTemp.Series["온도"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                            chartHumi.Series.Clear();
                            chartHumi.Series.Add("습도");
                            chartHumi.Series["습도"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                            chartAmmo.Series.Clear();
                            chartAmmo.Series.Add("암모늄");
                            chartAmmo.Series["암모늄"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                            chartCO2.Series.Clear();
                            chartCO2.Series.Add("CO2");
                            chartCO2.Series["CO2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                            // 각 데이터 포인트를 차트에 추가합니다.
                            foreach (var point in data)
                            {
                                chartTemp.Series["온도"].Points.AddXY(point["timestamp"], point["temperature"]);
                                chartHumi.Series["습도"].Points.AddXY(point["timestamp"], point["Humidity"]);
                                chartAmmo.Series["암모늄"].Points.AddXY(point["timestamp"], point["Ammonium"]);
                                chartCO2.Series["CO2"].Points.AddXY(point["timestamp"], point["CO2"]);
                            }
                        }
                    });// Your logic here
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("작업 취소됨");
            }
            finally
            {
                _timer.Stop();
                _timer.Dispose();
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GraphForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            cts.Cancel();

        }

        private void GraphForm_Shown(object sender, EventArgs e)
        {
            Debug.WriteLine("폼2로드됨");
            startTimer();
        }
    }
}
