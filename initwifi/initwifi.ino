#include <WiFi.h>

// Replace with your network credentials
const char* ssid = "xpi"; 
const char* password = "smartiot";

void initWiFi() {
 WiFi.mode(WIFI_STA);    //Set Wi-Fi Mode as station
 WiFi.begin(ssid, password);   
 
 Serial.println("Connecting to WiFi ..");
 while (WiFi.status() != WL_CONNECTED) {
   Serial.print('.');
   delay(1000);
 }
 
 Serial.print("Local IP: ");
 Serial.println(WiFi.localIP());
 Serial.print("Subnet Mask: " );
 Serial.println(WiFi.subnetMask());
 Serial.print("Gateway IP: ");
 Serial.println(WiFi.gatewayIP());
 Serial.print("DNS 1: ");
 Serial.println(WiFi.dnsIP(0));
 Serial.print("DNS 2: ");
 Serial.println(WiFi.dnsIP(1));
}

void setup() {
 Serial.begin(115200);
 initWiFi();
}

void loop() {
 // put your main code here, to run repeatedly:
}