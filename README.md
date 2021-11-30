# OQM10P_HFT_2021221
Haladó fejlesztési technikák beadandó

A projekt témája egy leegyszerűsített ticketing rendszer létrehozása, tehát vannak felhasználók, projektek, és feladatok. Minden projektnek van egy tulajdonos felhasználója, minden feladat egy projekthez tartozik.

Seed adatok generálása
A seed adatokat a mockaroo.com segítségével hoztam létre. Ez egy olyan oldal, amit kifejezetten teszt adatok generálására hoztak létre. Különböző formátumokban lehet generálni az adatokat, pl SQL insertek, csv, xml, json, stb. A projekthez én json-t generáltattam az oldallal, és azt olvastattam fel, majd mentettem le az adatbázisba a programmal. Ez azt eredményezi, hogy viszonylag hosszú json stringek találhatók jelenleg a DbContext osztályban, viszont ez biztosítja azt is, hogy nagyobb mennyiségű adatot tudok egy kattintással készíteni, és ha valami változik a modellben, az adat egyszerűen újragenerálható.

## A különböző modellekhez az alábbi beállításokat használtam:

### User
![image](https://user-images.githubusercontent.com/49789135/137473088-c32a30a9-bbab-4a8a-bd11-6883a88a8d6f.png)
https://mockaroo.com/34fc1ab0

### Project
![image](https://user-images.githubusercontent.com/49789135/137473229-5606e04a-52f8-4ed6-a7e3-c4687acf987f.png)
https://mockaroo.com/92440720

### Issue
![image](https://user-images.githubusercontent.com/49789135/137473337-15b262f5-d7ea-4023-afe0-5b85d8216a47.png)
https://mockaroo.com/5210dec0

## Swagger
A rendszerbe behúztam a swaggert is, hogy azon keresztül egyszerűbben lehessen böngészni és próbálgatni az api-kat
A swagger ui a http://localhost:51332/swagger/index.html url-en érhető el.

## Riportok
- Riport, ami visszaadja, hogy a tervezett ráfordítás alapján a legnagyobb projekten melyik user dolgozott a legtöbbet
- Riport, ami visszaadja azt a top 3 felhasználót, akik a legtöbb feladatot zárták le
- Riport, ami visszaadja, hogy ki a tulajdonosa annak a projektnek, ahol a legtöbb magas prioritású feladat a tervezett időn belül lett lezárva
- Riport, ami visszaadja nemek szerint a határidőn belül lezárt taskok számát
- Riport, ami visszaadja projektek nevét és az egyes projekten a feladatokkal eltöltött és becsült idő arányát
- Riport, ami visszaadja azt a top 3 projektet, ahol a legkevesebb BUG típusú issue található

## Egyebek
A projekt tartalmaz validátor osztályokat, így a service create metódusok helyett a validátorok metódusait teszteltem, illetve természetesen a non-crud lekérdezéseket. A non-crud lekérdezések kikerültek egy külön osztályba, a ReportService-be, mert így tűnt helyesnek elkülöníteni a többi működéstől, ugyanígy a riportok külön controller osztályt is kaptak. A controller osztályok nagy része üres, a crud végpontok egy BaseCrudController osztályban lettek elkészítve, és ezt öröklik a controllerek. 
A projekt esetén elérhető egy Close művelet is, ami egyszerűen lezárja a projektet, de erre egyéb logikát, validálást nem készítettem, pedig lehetne, pl ne lehessen szerkeszteni már a lezárt projekt adatait, vagy ne lehessen új issue-t felvenni hozzá, esetleg azt is meg lehetne vizsgálni lezárás előtt, hogy van-e még nyitott issue-ja a projektnek, és nem engedni lezárni amíg van, stb. 
Ugyanígy rengeteg pontja van még a működésnek, ami szerintem hiányos, vagy lehetne jobb, illetve rengeteg tesztet is lehetne még írni hozzá, de a féléves feladat követelményeit szerintem azért teljesíti így is :)
