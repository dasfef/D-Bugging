package com.example.d_bugging_and;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.TextView;

import java.util.Map;
import java.util.Timer;
import java.util.TimerTask;

public class MainActivity extends AppCompatActivity {

    WebView cam;
    WebSettings webSettings;
    TextView txtTemp, txtHumi, txtCO2, txtNH4;

    private Timer timer;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        setTitle("D-Bugging");

        cam = findViewById(R.id.WebCam);
        txtTemp=findViewById(R.id.txtTemp);
        txtHumi=findViewById(R.id.txtHumi);
        txtCO2=findViewById(R.id.txtCO2);
        txtNH4=findViewById(R.id.txtNH4);


        //웹 영상 띄우기
        cam.setWebViewClient(new WebViewClient());
        webSettings = cam.getSettings(); //세부 세팅 등록
        webSettings.setJavaScriptEnabled(true); // 웹페이지 자바스클비트 허용 여부 - 동적 컨텐츠 시 필요
        webSettings.setLoadWithOverviewMode(true); // 메타태그 허용 여부 - 웹페이지의 html 허용 여부
        webSettings.setUseWideViewPort(true); // 화면 사이즈 맞추기 허용 여부
        int scalePercent = 160; // 150%로 확대
        cam.setInitialScale(scalePercent);
        webSettings.setLayoutAlgorithm(WebSettings.LayoutAlgorithm.SINGLE_COLUMN); // 컨텐츠 사이즈 맞추기

        //webSettings.setSupportMultipleWindows(false); // 새창 띄우기 허용 여부
        //webSettings.setJavaScriptCanOpenWindowsAutomatically(false); // 자바스크립트 새창 띄우기(멀티뷰) 허용 여부
        //webSettings.setCacheMode(WebSettings.LOAD_NO_CACHE); // 브라우저 캐시 허용 여부
        //webSettings.setDomStorageEnabled(true); // 로컬저장소 허용 여부
        //webSettings.setSupportZoom(false); // 화면 줌 허용 여부

        cam.loadUrl("http://woong.ddns.net:8800/");

    }

    private void startTimer() {
        timer = new Timer();
        timer.schedule(new TimerTask() {
            @Override
            public void run() {
                DataTask loadDataTask = new DataTask(new DataTask.OnDataLoadedListener() {
                    @Override
                    public void onDataLoaded(Map<String, Object> data) {

                        // 선언한 변수에 데이터 넣기
                        String temperature = (String) data.get("Temperature");
                        String humidity = (String) data.get("Humidity");
                        String ammonium = (String) data.get("Ammonium");
                        String co2 = (String) data.get("CO2");

                        //UI 업데이트
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                txtTemp.setText(temperature);
                                txtHumi.setText(humidity);
                                txtNH4.setText(ammonium);
                                txtCO2.setText(co2);
                            }
                        });
                    }
                });
                loadDataTask.execute();
            }
        }, 0, 1000); // 1초마다 업데이트
    }
    protected void onResume() {
        super.onResume();
        startTimer(); // 액티비티가 활성화되면 타이머 시작
    }

    @Override
    protected void onPause() {
        super.onPause();
        if (timer != null) {
            timer.cancel(); // 액티비티가 일시정지되면 타이머 중지
        }
    }

}