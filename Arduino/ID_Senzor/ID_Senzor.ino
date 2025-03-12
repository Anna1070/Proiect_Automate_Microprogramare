#include <SPI.h>
#include <MFRC522.h>
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <time.h>

#define  RST_PIN D3
#define SS_PIN D4

#define SERVER_IP "10.14.10.39:5154"
#ifndef STASSID
#define STASSID "OMiLAB"
#define STAPSK "digifofulbs"
#endif

MFRC522 rfid(SS_PIN,RST_PIN);
MFRC522::MIFARE_Key key;

byte readCard[4];
String tag = ""; //variabila in care stocam serial number-ul

const char* ntpServer = "pool.ntp.org";
const long gmtOffset_sec = 0;  // Adjust this according to your timezone
const int daylightOffset_sec = 3600; // Adjust for daylight saving time if needed


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  SPI.begin();
  rfid.PCD_Init();

  Serial.println();
  Serial.println();
  Serial.println();

  WiFi.begin(STASSID, STAPSK);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());

   // Initialize NTP for time synchronization
  configTime(gmtOffset_sec, daylightOffset_sec, ntpServer);
  
  // Wait for time to be set
  Serial.print("Waiting for NTP time sync");
  while (time(nullptr) < 100000) { // Wait for valid time
    Serial.print(".");
    delay(500);
  }
  Serial.println();
  Serial.println("Time synchronized!");
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
  time_t now = time(nullptr);
  char timeStr[25];
  strftime(timeStr, sizeof(timeStr), "%Y-%m-%d_T%H:%M:%S", localtime(&now));
  String collectionTime = String(timeStr);

  Serial.print("Collection Time: ");
  Serial.println(collectionTime);

  // wait for WiFi connection
  if ((WiFi.status() == WL_CONNECTED)) {

    WiFiClient client;
    HTTPClient http;

    Serial.print("[HTTP] begin...\n");
    // configure traged server and url
    http.begin(client, "http://" SERVER_IP "/api/data");  // HTTP
    http.addHeader("Content-Type", "application/json");

    Serial.print("[HTTP] POST...\n");
    // start connection and send HTTP header and body
    int httpCode = http.POST("{\"TagId\":\"" + tag + "\", \"CollectionTime\":\"" + collectionTime + "\"}");

    // httpCode will be negative on error
    if (httpCode > 0) {
      // HTTP header has been send and Server response header has been handled
      Serial.printf("[HTTP] POST... code: %d\n", httpCode);

      // file found at server
      if (httpCode == HTTP_CODE_OK) {
        const String& payload = http.getString();
        Serial.println("received payload:\n<<");
        Serial.println(payload);
        Serial.println(">>");
      }
    } else {
      Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }

    http.end();
  }

  return;
}
