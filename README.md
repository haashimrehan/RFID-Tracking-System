# RFID-Tracking-System

This project allows the use of many wifi-enabled arduinos along with the MFRC522 RFID scanner board to create a closed tracking system.

Each location is numbered according the the scheme C1-B1 to C1-B10, then C2-B1 to C2-B10, etc. Each location has an arduino and updates the mySQL database each time a device is detected by the reader. The RFID card is placed on whatever needs to be tracked and needs to be scanned when it gets to a new location.
