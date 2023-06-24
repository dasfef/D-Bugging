#define _LF 0x0d
#define _CR 0x0a

//#define LED     D0  // GPIO16
#define Fan1    D1  // GPIO5
#define Fan2    D2  // GPIO4
#define uSonic  D3  // GPIO0
#define Lamp    D4  // GPIO2

void setup() {
  Serial.begin(9600);

  // pinMode(LED,INPUT);
  // digitalWrite(LED,LOW);
  pinMode(Fan1,OUTPUT);
  digitalWrite(Fan1,HIGH);
  pinMode(Fan2,OUTPUT);
  digitalWrite(Fan2,HIGH);
  pinMode(uSonic,OUTPUT);
  digitalWrite(uSonic,HIGH);
  pinMode(Lamp,OUTPUT);
  digitalWrite(Lamp,HIGH);

  Serial.println("Sytem init");

}

void loop() {
  // digitalWrite(LED,LOW);
  // delay(500);
  // digitalWrite(LED,HIGH);
  // delay(500);
}

void serialEvent(){
  String rxd = Serial.readStringUntil(_CR);

  Serial.print("reseived : ");
  Serial.println(rxd);
  
  char cmd[5]={0};
  rxd.toCharArray(cmd,rxd.length());

  if (cmd[0]=='@'){
    switch (cmd[1]){
      case 'O':
        digitalWrite(Lamp,LOW);
        Serial.println("Lamp On");
        break;
      case 'X':
        digitalWrite(Lamp,HIGH);
        Serial.println("Lamp Off");
        break;
      case 'U':
        digitalWrite(uSonic,LOW);
        Serial.println("uSonic On");
        break;
      case 'Z':
        digitalWrite(uSonic,HIGH);
        Serial.println("uSonic Off");
        break;
      case 'A':
        digitalWrite(Fan1,LOW);
        Serial.println("Fan 1 On");
        break;
      case 'a':
        digitalWrite(Fan1,HIGH);
        Serial.println("Fan 1 Off");
        break;
      case 'B':
        digitalWrite(Fan2,LOW);
        Serial.println("Fan 2 On");
        break;
      case 'b':
        digitalWrite(Fan2,HIGH);
        Serial.println("Fan 2 Off");
        break;
      default:
        Serial.println("unknown command");
        break;
    }
  }else{
    Serial.println("??");
  }
}