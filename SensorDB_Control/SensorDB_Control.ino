#include <MQUnifiedsensor.h>  //MQUnifiedsensor by Miguel
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include "DHT.h"  //DHT sensor library by Adafruit

//핀 번호
#define FAN1 D1 
#define FAN2 D2
#define USONIC D3
#define LAMP D4
#define DHTPIN D5
#define pin A0

//Definitions
#define placa "ESP8266"
#define Voltage_Resolution 5
#define type "MQ-135" //MQ135
#define ADC_Bit_Resolution 10 // For arduino UNO/MEGA/NANO
#define RatioMQ135CleanAir 3.6//RS / R0 = 3.6 ppm  
//#define calibration_button 13 //Pin to calibrate your sensor
#define DHTTYPE DHT22

//모듈 on/off : on - 1 / off - 0
String fanOnOff="off";
String usonicOnOff="off";
String lampOnOff="off";

// WiFi definitions
const char* ssid = "xpi";
const char* password = "smartiot";

// Server URL
const char* serverName = "http://175.205.128.40:9797/postSensorData.php";

//Declare Sensor
MQUnifiedsensor MQ135(placa, Voltage_Resolution, ADC_Bit_Resolution, pin, type);
DHT dht(DHTPIN, DHTTYPE);

// Declare WiFi client
WiFiClient client;
HTTPClient http;

//!C#에서 토글 버튼으로 모듈의 on/off를 바꿔도 delay(2000) 후에 다시 loop 처음으로 돌아가
// 자동으로 조작되는 것을 방지하기 위한 변수!
//수동 조작을 false로 설정해 습도에 의해 모듈을 자동으로 제어
bool manual = false;

void setup() {

  Serial.begin(9600); //Init serial port

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED){
    delay(1000);
    Serial.println("connectin...");
    Serial.println(WiFi.status());
  }
  Serial.println("CONNECTED");


  MQ135.setRegressionMethod(1); //_PPM =  a*ratio^b

  dht.begin();
  
  //제어 모듈 
  pinMode(FAN1,OUTPUT);
  pinMode(FAN2,OUTPUT);
  pinMode(USONIC,OUTPUT);
  pinMode(LAMP,OUTPUT);

  MQ135.init(); 

  Serial.print("Calibrating please wait.");
  float calcR0 = 0;
  for(int i = 1; i<=10; i ++)
  {
    MQ135.update(); // Update data, the arduino will read the voltage from the analog pin
    calcR0 += MQ135.calibrate(RatioMQ135CleanAir);
    Serial.print(".");
  }
  MQ135.setR0(calcR0/10);
  Serial.println("  done!.");
  
  if(isinf(calcR0)) {Serial.println("Warning: Conection issue, R0 is infinite (Open circuit detected) please check your wiring and supply"); while(1);}
  if(calcR0 == 0){Serial.println("Warning: Conection issue found, R0 is zero (Analog pin shorts to ground) please check your wiring and supply"); while(1);}

  //led off로 설정해 놓기
  digitalWrite(LAMP,HIGH);
  lampOnOff="off";
}

void loop() {
  MQ135.update(); // Update data, the arduino will read the voltage from the analog pin

  MQ135.setA(110.47); MQ135.setB(-2.862); // Configure the equation to calculate CO2 concentration value
  float CO2 = MQ135.readSensor(); // 이산화탄소
  
  MQ135.setA(102.2 ); MQ135.setB(-2.473); // Configure the equation to calculate NH4 concentration value
  float NH4 = MQ135.readSensor(); // 암모늄
  
  float h = dht.readHumidity();     //습도
  float t = dht.readTemperature();  //온도
  float f = dht.readTemperature(true);
    // Check if any reads failed and exit early (to try again).

  if (isnan(h) || isnan(t) || isnan(f)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  // 온습도
  //조작이 자동으로 되어있는 상태에서
  //습도가 70%를 넘으면
  if(manual == false) {
    if(h>=70){
    //팬 2개 ON(부논리)
    digitalWrite(FAN1,LOW);
    digitalWrite(FAN2,LOW);
    //가습기 OFF
    digitalWrite(USONIC,HIGH);

    //데이터베이스에 넣을 모듈 on/off 값
    fanOnOff="on";
    usonicOnOff="off";

    //적정 습도되면 fan off
    if(h<=61){
      digitalWrite(FAN1,HIGH);
      digitalWrite(FAN2,HIGH);

      fanOnOff="off";
    }
  }
  //습도가 50% 이하면
  else if(h<=60){
    //팬 2개 OFF
    digitalWrite(FAN1,HIGH);
    digitalWrite(FAN2,HIGH);
    //가습기 ON
    digitalWrite(USONIC,LOW);

    //데이터베이스에 넣을 모듈 on/off 값
    fanOnOff="off";
    usonicOnOff="on";
    
    //적정 습도가 되면 가습기 off
    if(h>60){
      digitalWrite(USONIC,HIGH);

      usonicOnOff="off";
    }
  }else{
    digitalWrite(FAN1,HIGH);
    digitalWrite(FAN2,HIGH);
    digitalWrite(USONIC,HIGH);

    fanOnOff="off";
    usonicOnOff="off";
    }
  }
  
  //C#에서 토글 버튼을 바꾸면 수동 조작으로 바꿈
  if(Serial.available())
  {
    manual = true;
    if(manual == true){
      int c = Serial.read();
    
      if(c=='1'){
        digitalWrite(FAN1,LOW);
        digitalWrite(FAN2,LOW);
        fanOnOff="on";
      }else if(c=='2'){
        digitalWrite(FAN1,HIGH);
        digitalWrite(FAN2,HIGH);
        fanOnOff="off";
      }else if(c=='3'){
        digitalWrite(USONIC,LOW);
        usonicOnOff="on";
      }else if(c=='4'){
        digitalWrite(USONIC,HIGH);
        usonicOnOff="off";
      }else if(c=='5'){
        digitalWrite(LAMP,LOW);
        lampOnOff="on";
      }else if(c=='6'){
        digitalWrite(LAMP,HIGH);
        lampOnOff="off";
      }else if(c=='7'){
        manual=false;
      }
    }
    
  }

  

  //php로 값 보내기
  String httpRequestData = "Temperature=" + String(t) + "&Humidity=" + String(h) + "&Ammonium=" + String(NH4) + "&CO2=" + String(CO2)+"&fanOnOff="+String(fanOnOff)+"&usonicOnOff="+String(usonicOnOff)+"&lampOnOff="+String(lampOnOff);

  // Your Domain name with URL path or IP address with path
  http.begin(client, serverName);

  // Specify content-type header
  http.addHeader("Content-Type", "application/x-www-form-urlencoded");

  // Send HTTP POST request
  int httpResponseCode = http.POST(httpRequestData);
   
  if (httpResponseCode>0) {
    String response = http.getString();
    Serial.println(httpResponseCode);  
    Serial.println(response);           
  }
  else {
    Serial.print("Error on sending POST: ");
    Serial.println(httpResponseCode);
  }

  http.end();
  Serial.print(t);
  Serial.print(",");
  Serial.print(h);
  Serial.print(",");
  Serial.print(NH4);
  Serial.print(",");
  Serial.print(CO2);
  Serial.print(",");
  Serial.print(fanOnOff);
  Serial.print(",");
  Serial.print(usonicOnOff);
  Serial.print(",");
  Serial.print(lampOnOff);
  Serial.println("");


  delay(2000); //Sampling frequency
}