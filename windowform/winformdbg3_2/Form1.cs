using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.IO;
using System.Net;
using MySql.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV;
using System.IO.Ports;

namespace winformdbg3
{
    public partial class MainForm : MetroForm
    {

        private bool isStreaming = false;
        private static readonly HttpClient client = new HttpClient(); // 소켓 고갈 방지 목적의 클래스 레벨 선언
        private System.Timers.Timer _timer;

        //아두이노로 모듈을 제어하기 위해 연결 필요
        SerialPort ComPort = new SerialPort();

        String fanTgl, usonicTgl, lampTgl;

        VideoCapture capture;
        Mat frame;

        public MainForm()
        {
            InitializeComponent();
            //LoadData();
            StartTimer();
            frame = new Mat();

        }

        private void InitializeCapture()
        {
            capture = new VideoCapture("http://192.168.0.46:5702/mjpeg/1");

        }


        private void ProcessFrame(object sender, EventArgs e)
        {
            capture.Read(frame);

            if (!frame.IsEmpty)
            {
                pictureBox1.Image = frame.ToImage<Bgr, byte>().ToBitmap<Bgr, byte>();
            }
        }

        void StartTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += async (sender, e) => await LoadData();
            //_timer.Elapsed += async (sender, e) => await LoadTempData();
            _timer.Start();
        }

        public async Task LoadData()
        {
            string url = "http://175.205.128.40:9797/getSensorData.php";

            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            this.Invoke((MethodInvoker)delegate
            { // 다른 스레드에서 라벨에 접근하기 위한 Invoke
                lblTemp.Text = data["Temperature"].ToString();
                lblHumi.Text = data["Humidity"].ToString();
                lblAmmo.Text = data["Ammonium"].ToString();
                lblCO2.Text = data["CO2"].ToString();
            });

            //데이터베이스에 저장된 모듈 값 가져오기
            fanTgl = data["FanOnOff"].ToString();
            usonicTgl= data["UsonicOnOff"].ToString();
            lampTgl = data["LampOnOff"].ToString();
            
        }




        private async void btnStartStream(object sender, EventArgs e)
        {

            if (tglStream.Checked) // 버튼이 켜져있는 경우
            {
                try
                {
                    InitializeCapture();
                    Application.Idle += ProcessFrame;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to initialize video capture. Error: " + ex.Message);
                }
            }
            else // 버튼이 꺼져있는 경우
            {
                Application.Idle -= ProcessFrame; // 이벤트 핸들러 제거
                if (capture != null)
                {
                    capture.Dispose(); // 자원 해제
                    capture = null;
                }

                // 정지 화면 추가하자
            }


        }

        private void btnStopStream(object sender, EventArgs e)
        {


        }



        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

            //포트 설정
            ComPort.PortName = "COM5";
            ComPort.BaudRate = 9600;
            ComPort.DataBits = 8;
            ComPort.Parity = Parity.None;
            ComPort.StopBits = StopBits.One;
            ComPort.Handshake = Handshake.None;
            ComPort.Open();
            ComPort.DiscardInBuffer();

            if (fanTgl == "on") { tglFan.Checked = true; }
            else { tglFan.Checked = false; }
            if (usonicTgl == "on") { tglMoist.Checked = true; }
            else { tglMoist.Checked = false; }
            if (lampTgl == "on") { tglLED.Checked = true; }
            else { tglLED.Checked = false; }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            _timer.Stop();


            _timer.Dispose();

            ComPort.Close();
        }

        //FAN
        private void tglFan_CheckedChanged(object sender, EventArgs e)
        {
            if (tglFan.Checked == true)
            {
                ComPort.Write("1");
                tglFan.Text = "On";
            }
            else
            {
                ComPort.Write("2");
                tglFan.Text = "Off";
            }
        }

        //가습기
        private void tglMoist_CheckedChanged(object sender, EventArgs e)
        {
            if (tglMoist.Checked == true)
            {
                ComPort.Write("3");
                tglMoist.Text = "On";
            }
            else
            {
                ComPort.Write("4");

                tglMoist.Text = "Off";
            }
        }

        //버튼 클릭하면 습도에 의한 자동 조작으로 변환
        private void btnReset_Click(object sender, EventArgs e)
        {
            ComPort.Write("7");
        }

        //LAMP
        private void tglLED_CheckedChanged(object sender, EventArgs e)
        {
            if(tglLED.Checked == true)
            {
                ComPort.Write("5");

                tglLED.Text = "On";
            }
            else
            {
                ComPort.Write("6");

                tglLED.Text = "Off";
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GraphForm graphForm = new GraphForm();
            Debug.WriteLine("그래프폼 열기 시도함");
            graphForm.ShowDialog();

        }
    }
}
