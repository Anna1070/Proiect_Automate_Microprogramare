#include <SPI.h>
#include <MFRC522.h>

constexpr uint8_t RST_PIN = D3;
constexpr uint8_t SS_PIN = D4;

MFRC522 rfid(SS_PIN,RST_PIN);
MFRC522::MIFARE_Key key;

String tag; //variabila in care stocam serial number-ul

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
  tag = "";

  for (byte i = 0; i < rfid.uid.size; i++) {
    tag += String(rfid.uid.uidByte[i], HEX);
    tag += " ";
  }

  Serial.println(tag);
  rfid.PICC_HaltA();
  rfid.PCD_StopCrypto1();
}
