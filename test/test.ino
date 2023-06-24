#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ESP8266WebServer.h>

#define PAN1 1
#define PAN2 2
#define USONIC 3
#define LEMP 4

// const char* ssid = "xpi";
// const char* password = "smartiot";

// IPAddress staticIP(192, 168, 100, 33);

// IPAddress gateway(192, 168, 100, 1);
// IPAddress subnet(255, 255, 255, 0);

void setup() {

  Serial.begin(9600);

  // if (WiFi.config(staticIP, gateway, subnet) == false) {
  //  Serial.println("Configuration failed.");
  // }

  //pinMode(LED_BUILTIN, OUTPUT);
  pinMode(PAN1,OUTPUT);
  digitalWrite(PAN1,HIGH);
  pinMode(PAN2,OUTPUT);
  digitalWrite(PAN2,HIGH);
  pinMode(USONIC,OUTPUT);
  digitalWrite(USONIC,HIGH);
  pinMode(LEMP,OUTPUT);
  digitalWrite(LEMP,HIGH);

  Serial.println("System Init");

}

void loop() {  
  digitalWrite(LED_BUILTIN, LOW);  // Turn the LED on (Note that LOW is the voltage level
  // but actually the LED is on; this is because
  // it is active low on the ESP-01)
  delay(1000);                      // Wait for a second
  digitalWrite(LED_BUILTIN, HIGH);  // Turn the LED off by making the voltage HIGH
  delay(2000);   
}

#define _CR 0x0D
#define _LF 0x0A

void serialEvent(){


  String str = Serial.readStringUntil(_LF);
  String head = str.substring(0,1);
  char command[5]={0};
  str.toCharArray(command,str.length());

  Serial.println(command);

  switch(command[1]){
    case 'O':
      digitalWrite(LEMP,LOW);
      Serial.println("LAMP ON");
      break;
    case 'X':
      digitalWrite(LEMP,HIGH);
      Serial.println("LAMP OFF");
      break;
    case 'A':
      digitalWrite(PAN1,LOW);
      Serial.println("FAN1 ON");
      break;
    case 'B':
      digitalWrite(PAN1,HIGH);
      Serial.println("FAN1 OFF");
      break;
    case 'C':
      digitalWrite(PAN2,LOW);
      break;
    case 'D':
      digitalWrite(PAN2,HIGH);
      break;
    case 'U':
      digitalWrite(USONIC,LOW);
      break;
    case 'Z':
      digitalWrite(USONIC,HIGH);
      break;
    default:
      Serial.println("default");
      break;
  }
}
