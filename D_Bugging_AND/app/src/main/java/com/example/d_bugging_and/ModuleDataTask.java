package com.example.d_bugging_and;

//모듈 on/off 값 가져오는 코드

import android.os.AsyncTask;
import org.json.JSONException;
import org.json.JSONObject;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

public class ModuleDataTask extends AsyncTask<Void, Void,String> {
    private static final String URL = "http://175.205.128.40:9797/getModeState.php";

    private ModuleDataTask.OnDataLoadedListener listener; //데이터가 성공적으로 로드되었을 때 호출되는 onDataLoaded 메서드

    public ModuleDataTask(ModuleDataTask.OnDataLoadedListener listener) {
        this.listener = listener;
    }

    //백그라운드에서 실행되는 작업 정의
    @Override
    protected String doInBackground(Void... params) {
        HttpURLConnection connection = null;
        BufferedReader reader = null;
        try {
            URL url = new URL(URL);
            connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("GET");

            StringBuilder response = new StringBuilder();
            reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            String line;
            while ((line = reader.readLine()) != null) {
                response.append(line);
            }

            return response.toString();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (connection != null) {
                connection.disconnect();
            }
            if (reader != null) {
                try {
                    reader.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        return null;
    }

    //가져온 데이터를 JSON 객체로 변환하고, Map<String, Object>에 저장한 후에 onDataLoaded() 콜백을 호출
    @Override
    protected void onPostExecute(String response) {
        if (response != null) {
            try {
                JSONObject jsonObject = new JSONObject(response);
                Map<String, Object> data = new HashMap<>();
                data.put("fan", jsonObject.getString("fan"));
                data.put("usonic", jsonObject.getString("usonic"));
                data.put("lamp", jsonObject.getString("lamp"));

                if (listener != null) {
                    listener.onDataLoaded(data);
                }
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
    }

    public interface OnDataLoadedListener {
        void onDataLoaded(Map<String, Object> data);
    }
}