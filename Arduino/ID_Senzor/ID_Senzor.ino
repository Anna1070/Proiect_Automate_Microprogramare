#include <SPI.h>
#include <MFRC522.h>

#define  RST_PIN D3
#define SS_PIN D4

MFRC522 rfid(SS_PIN,RST_PIN);
MFRC522::MIFARE_Key key;

byte readCard[4];
String tag = ""; //variabila in care stocam serial number-ul

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  SPI.begin();
  rfid.PCD_Init();
}

void loop() {
  // put your main code here, to run repeatedly:
  
  Serial.println("Waiting for RFID card...");

  if (!rfid.PICC_IsNewCardPresent()) {
    delay(500);
    return;
  }
  Serial.println("Card detected!");

  if (!rfid.PICC_ReadCardSerial()) {
    Serial.println("Failed to read card.");
    return;
  }

  Serial.print("Card UID: ");
  Serial.println();

  tag = "";
  for (byte i = 0; i < rfid.uid.size; i++) {
    tag += String(rfid.uid.uidByte[i] < 0x10 ? "0" : "");
    tag += String(rfid.uid.uidByte[i], HEX);
  }
  Serial.println(tag);
  rfid.PICC_HaltA();
  return;
}
