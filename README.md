# OQM10P_HFT_2021221
Haladó fejlesztési technikák beadandó

A projekt témája egy leegyszerűsített ticketing rendszer létrehozása, tehát vannak felhasználók, projektek, és feladatok. Minden projektnek van egy tulajdonos felhasználója, minden feladat egy projekthez tartozik.

Seed adatok generálása
A seed adatokat a mockaroo.com segítségével hoztam létre. Ez egy olyan oldal, amit kifejezetten teszt adatok generálására hoztak létre. Különböző formátumokban lehet generálni az adatokat, pl SQL insertek, csv, xml, json, stb. A projekthez én json-t generáltattam az oldallal, és azt olvastattam fel, majd mentettem le az adatbázisba a programmal. Ez azt eredményezi, hogy viszonylag hosszú json stringek találhatók jelenleg a DbContext osztályban, viszont ez biztosítja azt is, hogy nagyobb mennyiségű adatot tudok egy kattintással készíteni, és ha valami változik a modellben, az adat egyszerűen újragenerálható.

A különböző modellekhez az alábbi beállításokat használtam:

## User
![image](https://user-images.githubusercontent.com/49789135/137473088-c32a30a9-bbab-4a8a-bd11-6883a88a8d6f.png)
https://mockaroo.com/34fc1ab0

## Project
![image](https://user-images.githubusercontent.com/49789135/137473229-5606e04a-52f8-4ed6-a7e3-c4687acf987f.png)
https://mockaroo.com/92440720

## Issue
![image](https://user-images.githubusercontent.com/49789135/137473337-15b262f5-d7ea-4023-afe0-5b85d8216a47.png)
https://mockaroo.com/5210dec0

