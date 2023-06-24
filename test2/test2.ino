#define FAN1 D1 
#define FAN2 D2
#define USONIC D3
#define LAMP D4

void setup() {
  Serial.begin(9600);

  pinMode(FAN1,OUTPUT);
  pinMode(FAN2,OUTPUT);
  pinMode(USONIC,OUTPUT);
  pinMode(LAMP,OUTPUT);
}

// the loop function runs over and over again forever
void loop() {
  digitalWrite(FAN1,LOW);
  digitalWrite(FAN2,LOW);
  digitalWrite(LAMP,HIGH);
  digitalWrite(USONIC,HIGH);

  delay(10000);

  digitalWrite(FAN1,HIGH);
  digitalWrite(FAN2,HIGH);
  digitalWrite(LAMP,LOW);
  digitalWrite(USONIC,LOW);
  delay(10000);

}
