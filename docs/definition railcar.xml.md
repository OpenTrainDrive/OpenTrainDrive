# Dokumentation railcar.xml

## Inhalt

Die `railcar.xml` Datei definiert Eisenbahnwagen-Typen und deren Konfigurationen für den Modellbahnbetrieb. Sie enthält detaillierte Informationen über Wagenmodelle, Decodereinstellungen für Funktionen (Beleuchtung, Sounds), und Betriebshistorie.

## Dateistruktur

### Root-Element: `<railcars>`

Container-Element für die einzelnen Wagen (Reisezugwagen, Güterwagen, etc.)

```xml
<railcars>
  <!-- Wagen-Einträge hier -->
</railcars>
```

---

## Wagenelement: `<railcar>`

### Attribute


| Attribut | Typ  | Beschreibung                                  | Beispiel                               |
| ---------- | ------ | ----------------------------------------------- | ---------------------------------------- |
| `uid`    | UUID | Eindeutige Kennung des Wagens                 | `70f969ca-c2bb-4b8b-8dff-9f3d76e4e98f` |
| `name`   | Text | Anzeigename des Wagens                        | `IC2000 Bt`                            |
| `length` | Zahl | Länge (in global konfigurierter Masseinheit) | `30`                                   |
| `index`  | Zahl | Anzeigeposition in der Konfiguration          | `1`                                    |

### Unterelemente

---

## 1. Modellinformationen: `<model>`

Beschreibt die Eigenschaften des Vorbilds

### Attribute


| Attribut        | Typ  | Beschreibung             | Beispiel       |
| ----------------- | ------ | -------------------------- | ---------------- |
| `manufacturer`  | Text | Hersteller des Modells   | `Roco`         |
| `scale`         | Text | Modellmassstab           | `H0` (H0-Spur) |
| `catalognumber` | Text | Hersteller Katalognummer | `64855`        |

### Unterelemente

#### `<description>`

Vollständige Beschreibung des Wagens

- **Typ:** Text
- **Beispiel:** `Steuerwagen IC 2000`

#### `<operator>`

Eisenbahnunternehmen, das das Vorbild betreibt

- **Typ:** Text
- **Beispiel:** `SBB` (Schweizerische Bundesbahn)

#### `<class>`

Wagonklasse/Baureihe

- **Typ:** Text
- **Beispiel:** `IC2000 Bt`

#### `<serialnumber>`

Betriebsnummer des Wagens

- **Typ:** Text
- **Beispiel:** `50 85 29-34 256-1`
- **Hinweis:** Kann leer sein, falls Nummer unbekannt

#### `<cartype>`

Wagonstyp/Verwendungszweck

- **Typ:** Text
- **Werte:** `coach` (Personenwagen), `diningcar` (Speisewagen), `sleepingcar` (Schlafwagen), `luggage` (Gepäckwagen), `freightcar` (Güterwagen), `tanker` (Kesselwagen), `container` (Containerträger), `flatcar` (Flachwaggon), `gondola` (Hochbordwagen), etc.
- **Beispiel:** `coach`

#### `<weight>`

Gewicht in Tonnen

- **Typ:** Zahl
- **Beispiel:** `91`

#### `<vmax>`

Höchstgeschwindigkeit (Masseinheit gemäss globaler Konfiguration OpenTrainDrive)

- **Typ:** Zahl
- **Beispiel:** `200`

#### `<image>`

Symbol/Bild für den Wagen (relativ zum Projektverzeichnis)

- **Typ:** Text
- **Beispiel:** `locoimages/IC2000-Bt.png`

#### `<notes>`

Zusätzliche Notizen oder Bemerkungen

- **Typ:** Text
- **Beispiel:** `Neue Innenbeleuchtung 2023 eingebaut`

---

## 2. Decoder-Konfiguration: `<decoder>`

DCC-Decodereinstellungen für digitale Steuerung von Funktionen (Beleuchtung, Sounds, etc.)

### Unterelemente

#### `<protocol>`

Decoder-Protokoll

- **Typ:** Text
- **Werte:** `DCC128`, `DCC28`, `DCC14`, `MM`, `MFX` etc.
- **Beispiel:** `DCC128` (DDC, 128 Fahrstufen)
- **Hinweis:** Bei Wagen ist das Protokoll nicht relevant für die Fahrt, aber für die Funktion

#### `<address>`

DCC-Decoder-Adresse (als Zusatzdecoder im Wagen)

- **Typ:** Zahl
- **Beispiel:** `100`

#### `<addresstype>`

Adress-Typ (nur bei DCC-Dekodern)

- **Typ:** Text
- **Werte:** `short` (kurz), `long` (lang)
- **Beispiel:** `short`

#### `<functiontable>`

Container für Funktionsdefinitionen. Siehe [Funktionstabelle](#funktionstabelle) weiter unten.

---

## Funktionstabelle: `<functiontable>`

Ordnet DCC-Funktionstasten Wagenfeatures zu (Beleuchtung, Geräusche, etc.).

### Funktionselement: `<function>`

#### Attribute


| Attribut      | Typ         | Beschreibung                                                        | Beispiel             |
| --------------- | ------------- | --------------------------------------------------------------------- | ---------------------- |
| `no`          | Zahl        | Funktionsnummer (0-28)                                              | `0`                  |
| `description` | Text        | Funktionsbeschreibung                                               | `3x weiss <> 2x rot` |
| `actuation`   | Text        | Betätigung:`toggle` oder `impulse`                                 | `toggle`             |
| `category`    | Text        | **Veraltete alternative zu `type`** - wird manchmal verwendet       | `headlight`          |
| `type`        | Text        | Funktionskategorie:`headlight`, `sound`, `interiorlight`, `driving` | `interiorlight`      |
| `visible`     | Wahr/Falsch | Anzeige in Benutzeroberfläche                                      | `true`, `false`      |
| `image`       | Text        | Symbol-Dateiname für UI                                            | `light.svg`          |

#### Funktionstypen für Wagen


| Typ             | Beschreibung           |
| ----------------- | ------------------------ |
| `headlight`     | Stirnbeleuchtung       |
| `sound`         | Soundeffekte und Audio |
| `interiorlight` | Innenbeleuchtung       |
| `driving`       | Fahrtmodus-Effekte     |

#### Auslösungsarten


| Art       | Beschreibung                                        |
| ----------- | ----------------------------------------------------- |
| `toggle`  | Ein-/Aus-Schalter (Aktivierung schaltet um)         |
| `impulse` | Einmalige Aktion (eingeschaltet, solange aktiviert) |

#### Beispiel

```xml
<function no="0" description="3x weiss <> 2x rot" 
          actuation="toggle" category="headlight" visible="true" 
          image="headlight.svg" />
<function no="1" description="Innenbeleuchtung Zug" 
          actuation="toggle" type="interiorlight" visible="true" 
          image="light.svg" />
```

---

## 3. Betriebsinformationen: `<operation>`

Verfolgt Wartungs- und Betriebshistorie des Wagenmodells.

### Unterelemente

#### `<purchasedate>`

Kaufdatum des Wagens.

- **Typ:** ISO 8601 Datum (YYYY-MM-DD)
- **Beispiel:** `2022-05-01`

#### `<operatingtime>`

Gesamte Betriebszeit in Stunden.

- **Typ:** Zahl
- **Beispiel:** `20`

#### `<traveldistance>`

Gesamte zurückgelegte Strecke in Kilometern.

- **Typ:** Zahl
- **Beispiel:** `1500`

#### `<serviceinterval>`

Empfohlenes Wartungsintervall in Stunden.

- **Typ:** Zahl (oder leer)
- **Beispiel:** `50`

#### `<servicetable>`

Container für Wartungseinträge.

##### Service-Element: `<issue>`

###### Attribute


| Attribut | Typ            | Beschreibung  | Beispiel     |
| ---------- | ---------------- | --------------- | -------------- |
| `date`   | ISO 8601 Datum | Wartungsdatum | `2024-06-01` |

###### Unterelement: `<item>`

Beschreibung der durchgeführten Arbeiten (mehrere Items möglich).

- **Typ:** Text
- **Beispiel:** `Radkontakte gereinigt, Räder kontrolliert`

---

## Vollständige XML-Struktur

```
<railcars>
  └── <railcar> (Attribute: uid, name, length, index)
      ├── <model> (Attribute: manufacturer, scale, catalognumber)
      │   ├── <description>
      │   ├── <operator>
      │   ├── <class>
      │   ├── <serialnumber>
      │   ├── <cartype>
      │   ├── <weight>
      │   ├── <vmax>
      │   ├── <image>
      │   └── <notes>
      ├── <decoder>
      │   ├── <protocol>
      │   ├── <address>
      │   ├── <addresstype>
      │   └── <functiontable>
      │       └── <function> (mehrere, Attribute: no, description, actuation, type/category, visible, image)
      └── <operation>
          ├── <purchasedate>
          ├── <operatingtime>
          ├── <traveldistance>
          ├── <serviceinterval>
          └── <servicetable>
              └── <service> (mehrere, Attribute: date)
                  └── <item>
```

---

## Anwendungsbeispiele

### Neuer Personenwagen hinzufügen

```xml
<railcar uid="a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c" name="IC2000 Wagen" length="28" index="2">
  <model manufacturer="Roco" scale="H0" catalognumber="64850">
    <description>Reisezugwagen IC 2000</description>
    <operator>SBB</operator>
    <class>IC2000 A</class>
    <serialnumber>50 85 29-30 256-7</serialnumber>
    <cartype>coach</cartype>
    <weight>88</weight>
    <vmax>200</vmax>
    <image>locoimages/IC2000-A.png</image>
    <notes>Komfortabele Ausstattung mit Steckdosen</notes>
  </model>
  <decoder>
    <protocol>DCC128</protocol>
    <address>101</address>
    <addresstype>short</addresstype>
    <functiontable>
      <function no="0" description="Innenbeleuchtung" actuation="toggle" type="interiorlight" visible="true" image="light.svg" />
      <function no="1" description="Signallicht vorne" actuation="toggle" type="headlight" visible="true" image="headlight.svg" />
    </functiontable>
  </decoder>
  <operation>
    <purchasedate>2022-06-15</purchasedate>
    <operatingtime>25</operatingtime>
    <traveldistance>2000</traveldistance>
    <serviceinterval>50</serviceinterval>
    <servicetable>
      <service date="2024-06-01">
        <item>Innenbeleuchtung kontrolliert</item>
      </service>
    </servicetable>
  </operation>
</railcar>
```

### Speisewagen mit Beleuchtung

```xml
<railcar uid="b2c3d4e5-f6a7-48b9-0c1d-2e3f4a5b6c7d" name="Speisewagen" length="27" index="3">
  <model manufacturer="Roco" scale="H0" catalognumber="64864">
    <description>Speisewagen SBB IC 2000</description>
    <operator>SBB</operator>
    <class>IC2000 Gastronomie</class>
    <serialnumber>61 85 29-98 000-3</serialnumber>
    <cartype>diningcar</cartype>
    <weight>92</weight>
    <vmax>200</vmax>
    <image>locoimages/IC2000-Dining.png</image>
    <notes>Hochwertige Ausstattung, seitliche Nummern</notes>
  </model>
  <decoder>
    <protocol>DCC128</protocol>
    <address>102</address>
    <addresstype>short</addresstype>
    <functiontable>
      <function no="0" description="Innenbeleuchtung warm" actuation="toggle" type="interiorlight" visible="true" image="light.svg" />
      <function no="1" description="Stimmungsmusik" actuation="toggle" type="sound" visible="true" image="speaker.svg" />
    </functiontable>
  </decoder>
  <operation>
    <purchasedate>2022-06-15</purchasedate>
    <operatingtime>18</operatingtime>
    <traveldistance>1800</traveldistance>
    <serviceinterval>60</serviceinterval>
    <servicetable>
      <service date="2024-08-10">
        <item>Räder kontrolliert und gereinigt</item>
        <item>Decoder-Batterie überprüft</item>
      </service>
    </servicetable>
  </operation>
</railcar>
```

### Wartungseintrag hinzufügen

```xml
<servicetable>
  <service date="2024-06-01">
    <item>Radkontakte gereinigt</item>
  </service>
  <service date="2024-12-15">
    <item>Räder kontrolliert und gereinigt</item>
    <item>Neue Innenbeleuchtung-LED installiert</item>
  </service>
</servicetable>
```

---

## Referenztabellen

### DCC-Protokolle

- `DCC128` - 128 Fahrstufen (empfohlen)
- `DCC28` - 28 Fahrstufen
- `DCC14` - 14 Fahrstufen
- `Motorola` - Märklin analog
- `mfx` - Märklin mfx digital

### Wagonstypen (cartype)

- `coach` - Personenwagen/Reisezugwagen
- `diningcar` - Speisewagen
- `sleepingcar` - Schlafwagen
- `luggage` - Gepäckwagen
- `freightcar` - Güterwagen (offen)
- `tanker` - Kesselwagen (Flüssigkeiten)
- `container` - Containerträger
- `flatcar` - Flachwaggon
- `gondola` - Hochbordwagen
- `boxcar` - Gedeckter Güterwagen
- `refrigerated` - Kühlwagen
- `autorack` - Autotransportwagen

### Standard-DCC-Funktionsnummern

- **0:** Lichter (immer verfügbar)
- **1-5:** Häufige Funktionen (Innenbeleuchtung, Zusatzlichter)
- **6-8:** Spezielle Effekte (Sounds, Rauch)
- **9+:** Erweiterte Funktionen

---

## Validierungsregeln

- Alle `uid` Werte müssen eindeutig sein
- `address` und `addresstype` müssen kompatibel sein (kurz: 1-127, lang: 128-10239)
- Datumsformate müssen ISO 8601 sein (YYYY-MM-DD)
- Bildpfade sollten relativ zum Projektverzeichnis existieren
- Funktionsnummern sollten in einem Wagen nicht doppelt vorkommen
- `cartype` sollte aus der Referenztabelle gewählt werden
- `vmax` sollte realistisch für die Wagenklasse sein

---

## Unterschiede zwischen loco.xml und railcar.xml


| Aspekt               | Lokomotive (loco.xml)             | Wagen (railcar.xml)                          |
| ---------------------- | ----------------------------------- | ---------------------------------------------- |
| **Geschwindigkeit**  | Eigener Antrieb, volle Leistung   | Nur gezogen, abhängig von Lokomotive        |
| **Decoder**          | Haupt-Antriebsdecoder für Fahrt  | Optional: nur für Funktionen (Licht, Sound) |
| **Funktionstabelle** | Oft: Lichter, Sounds, Rangiergang | Oft: Beleuchtung, Sounds                     |
| **Speedtable**       | Wichtig für Fahrverhalten        | Nicht relevant                               |
| **vmax**             | Maximale Lokomotiven-Leistung     | Maximum des Wagens                           |
| **cartype**          | Nicht vorhanden                   | Spezifische Wagonstypen                      |

---

## Verwandte Dateien

- `railcar.xml` - Aktive Wagondefinitionen
- `railcar_old.xml` - Archiv/Backup alter Wagondefinitionen
- `loco.xml` - Lokomotivendefinitionen
- `train_template.xml` - Zugkomposition-Vorlagen
- `plan.xml` - Gleisplan-Definitionen
- `station.xml` - Bahnhof-Definitionen

---

## Wichtige Hinweise

### Digitale Zusatzfunktionen in Wagen

Wagen können optional mit zusätzlichen DCC-Decodern ausgestattet sein, um Funktionen wie Beleuchtung und Sounds zu steuern. Diese Decoder haben eigene Adressen und können unabhängig von der Lokomotive geschaltet werden.

### Kompatibilität in Zügen

Beim Zusammenstellen von Zügen in `train.xml` müssen die Wagons und Lokomotiven kompatibel sein:

- Gleicher DCC-Standard
- Ähnliche maximale Geschwindigkeiten
- Kompatible Kupplungen (nicht modelliert, aber in der Realität wichtig)

### Wartung und Instandhaltung

Die `<servicetable>` sollte regelmäßig aktualisiert werden:

- Radkontakte reinigen nach jedem Betriebstag mit Verschmutzung
- Getriebe ölen nach 10-20 Betriebsstunden
- Räder kontrollieren auf Verschleiß nach 50 Betriebsstunden
- Decoder-Batterien jährlich überprüfen
