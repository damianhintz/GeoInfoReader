GeoInfoReader v1.0-beta, 21 października 2016
---
Czytnik plików GEO-INFO V i TANGO.

# Cechy

* prostota
* odczyt geometrii obiektów
* odczyt numeru operatu

# Pomoc
```c#
var map = new MapaGeoInfo();
var reader = new TangoReader(mapa);
reader.Wczytaj("fileName.giv");
```

# Kontekst projektu

[Zwinny samuraj](https://docs.google.com/document/d/1KzQ8RNV6Y0hWGIU8NrvJ5ZppsC9On_1ciJ9LptcwHBM/edit?usp=sharing)

## Po co tu jesteśmy (myśl przewodnia)?

Potrzebuję biblioteki do odczytu formatu plików danych systemu GEO-INFO, która będzie wykorzystana w innych projektach.

## Krótkie podsumowanie

Czym jest produkt i dla kogo?

* dla mnie i innych
* którzy potrzebują czytnika plików GEO-INFO
* produkt GeoInfoReader
* jest biblioteką
* która pozwala w prosty sposób odczytać dane
* w odróżnieniu od braku takiego rozwiązania
* nasz produkt pozwala na odczyt plików systemu GEO-INFO

## Opakowanie produktu

Jak będzie wyglądał ten produkt:

**GeoInfoReader**
	
![Dobre zdjęcie](GeoInfoReader/Map.ico)

Prosta, szybka i precyzyjna biblioteka
	
* prostota
* szybkość
* precyzja
	
## Lista "NIE"

Czego nie robimy w tym projekcie.

w zakresie | poza zakresem
--------------- | -----------------------------------
odczyt obiektów | to nie będzie edytor plików GEO-INFO
operaty przypisane do obiektu | nie będzie grupowania obiektów według operatu
geometria obiektów | nie będzie generowania zakresów

nieokreślone | 
--------------
budowa zakresów | 
scalenie z plikiem Tango | 

## Otoczenie projektu

## Szkic rozwiązania

## Oszacowania

Jest to projekt na tydzień.

## Ogólny szkic architektury technicznej

# Historia

Do zrobienia:

- [ ] podręcznik użytkownika

2016-10-21 v1.0-beta

* nowość: importer mapy z GEO-INFO V
* nowość: eksporter mapy do Geomedia
* nowość: eksporter mapy do TANGO
* nowość: ekstraktor zasięgów obrębów

2016-10-01 v1.0-alfa

* [propozycja projektu](https://docs.google.com/document/d/1O7EHPSBacFY5yFfxNs8UU7O_whekDUPvDwXJXS3iZh0/edit?usp=sharing)
