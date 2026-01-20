# Dokumentation loco.xml

## Inhalt

Die `loco.xml` Datei definiert Lokomotiventypen und deren Konfigurationen für den Modellbahnbetrieb. Sie enthält detaillierte Informationen über Lokomotivmodelle, Decodereinstellungen, Funktionszuordnungen, Geschwindigkeitstabelle und Betriebshistorie.

## Dateistruktur

### Root-Element: `<locos>`

Container-Element für die einzelnen Lokomotiven

```xml
<locos>
  <!-- Lokomotiv-Einträge hier -->
</locos>
```

---

## Lokomotivenlement: `<loco>`

### Attribute


| Attribut | Typ  | Beschreibung                                  | Beispiel                               |
| ---------- | ------ | ----------------------------------------------- | ---------------------------------------- |
| `uid`    | UUID | Eindeutige Kennung der Lokomotive             | `f47ac10b-58cc-4372-a567-0e02b2c3d479` |
| `name`   | Text | Anzeigename der Lokomotive                    | `Re460 rot`                            |
| `length` | Zahl | Länge (in global konfigurierter Masseinheit) | `20`                                   |
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
| `catalognumber` | Text | Hersteller Katalognummer | `43757`        |

### Unterelemente

#### `<description>`

Vollständige Beschreibung der Lokomotive

- **Typ:** Text
- **Beispiel:**`SBB Re 460 Werbelok Zürich Relax`

#### `<operator>`

Eisenbahnunternehmen, das das Vorbild betreibt

- **Typ:** Text
- **Beispiel:**`SBB` (Schweizerische Bundesbahn)

#### `<class>`

Lokomotivenklasse/Baureihe

- **Typ:** Text
- **Beispiel:**`Re 460`

#### `<serialnumber>`

Betriebsnummer der Lokomotive

- **Typ:** Text
- **Beispiel:**`460 023-5`

#### `<tractiontype>`

Antriebsart

- **Typ:** Text
- **Werte:**`electric` (elektrisch),`diesel` (Diesel),`steam` (Dampf) usw.
- **Beispiel:**`electric`

#### `<weight>`

Gewicht in Tonnen

- **Typ:** Zahl
- **Beispiel:**`80`

#### `<vmax>`

Höchstgeschwindigkeit (Masseinheit gemäss globaler Konfiguration OpenTrainDrive)

- **Typ:** Zahl
- **Beispiel:**`200`

#### `<image>`

Symbol/Bild für die Lokomotive (relativ zum Projektverzeichnis)

- **Typ:** Text
- **Beispiel:**`locoimages/re460_023_rot.png`

#### `<notes>`

Zusätzliche Notizen oder Bemerkungen

- **Typ:** Text
- **Beispiel:** Anbauteile in Box "Kleinteile" gelagert

---

## 2. Decoder-Konfiguration: `<decoder>`

DCC-Decodereinstellungen für digitale Steuerung

### Unterelemente

#### `<protocol>`

Decoder-Protokoll

- **Typ:** Text
- **Werte:**`DCC128`,`DCC28`,`DCC14`,`MM`,`MFX` etc.
- **Beispiel:**`DCC128` (DDC, 128 Fahrstufen)

#### `<address>`

DCC-Decoder-Adresse

- **Typ:** Zahl
- **Beispiel:**`3`

#### `<addresstype>`

Adress-Typ (nur bei DCC-Dekodern)

- **Typ:** Text
- **Werte:**`short` (kurz),`long` (lang)
- **Beispiel:**`short`

#### `<functiontable>`

Container für Funktionsdefinitionen. Siehe [Funktionstabelle](#funktionstabelle) weiter unten.

#### `<speedtable>`

Container für Geschwindigkeitskurven-Zuordnung. Siehe [Geschwindigkeitstabelle](#geschwindigkeitstabelle) weiter unten.

---

## Funktionstabelle: `<functiontable>`

Ordnet DCC-Funktionstasten Lokomotivenfeatures zu (Beleuchtung, Geräusche, etc.).

### Funktionselement: `<function>`

#### Attribute


| Attribut      | Typ         | Beschreibung                                                            | Beispiel                                 |
| --------------- | ------------- | ------------------------------------------------------------------------- | ------------------------------------------ |
| `no`          | Zahl        | Funktionsnummer (0-28)                                                  | `0`                                      |
| `description` | Text        | Funktionsbeschreibung                                                   | `3x weiss/1x weiss <> 1x weiss/3x weiss` |
| `actuation`   | Text        | Betätigung:`toggle` oder `impulse`                                     | `toggle`                                 |
| `type`        | Text        | Funktionskategorie:`headlight`, `sound`, `interiorlight` oder `driving` | `headlight`                              |
| `visible`     | Wahr/Falsch | Anzeige in Benutzeroberfläche                                          | `true`, `false`                          |
| `image`       | Text        | Symbol-Dateiname für UI                                                | `headlight.svg`                          |

#### Funktionstypen


| Typ             | Beschreibung                  |
| ----------------- | ------------------------------- |
| `headlight`     | Stirnbeleuchtung              |
| `sound`         | Soundeffekte und Audio        |
| `interiorlight` | Innenbeleuchtung              |
| `driving`       | Fahrtmodus (z.B. Rangiergang) |

#### Auslösungsarten


| Art       | Beschreibung                                        |
| ----------- | ----------------------------------------------------- |
| `toggle`  | Ein-/Aus-Schalter (Aktivierung schaltet um)         |
| `impulse` | Einmalige Aktion (eingeschaltet, solange aktiviert) |

#### Beispiel

```xml
<function no="0" description="3x weiss/1x weiss <> 1x weiss/3x weiss" 
          actuation="toggle" type="headlight" visible="true" 
          image="headlight.svg" />
```

---

## Geschwindigkeitstabelle: `<speedtable>`

Ordnet DCC-Fahrstufen tatsächlichen Geschwindigkeitswerten für realistische Geschwindigkeitskurven zu.

### Geschwindigkeit-Element: `<speed>`

#### Attribute


| Attribut | Typ  | Beschreibung                                                                                  | Beispiel |
| ---------- | ------ | ----------------------------------------------------------------------------------------------- | ---------- |
| `step`   | Zahl | Fahrstufennummer (1-27 für DCC128)                                                           | `1`      |
| `v`      | Zahl | Tatsächliche Vorbild-Geschwindigkeit (Einheit gemäss globaler Konfiguration OpenTrainDrive) | `1`      |

#### Beschreibung

Stellt beim Einmessen eine fahrzeugspezifische Geschwindigkeitskurve bereit, die von OpenTrainDrive zur Berechnung von Beschleunigungs- und Bremskurven verwendet wird. Bei mehr als 27 Fahrstufen (z.B. 128) wird interpoliert.

#### Beispiel

```xml
<speed step="1" v="1" />   <!-- Fahrstufe 1 = 1 km/h -->
<speed step="27" v="200" /> <!-- Fahrstufe 27 (max) = 200 km/h -->
```

---

## 3. Betriebsinformationen: `<operation>`

Verfolgt Wartungs- und Betriebshistorie des Lokomotivmodells.

### Unterelemente

#### `<purchasedate>`

Kaufdatum der Lokomotive.

- **Typ:** ISO 8601 Datum (YYYY-MM-DD)
- **Beispiel:**`2022-05-01`

#### `<operatingtime>`

Gesamte Betriebszeit in Stunden.

- **Typ:** Zahl
- **Beispiel:**`20`

#### `<traveldistance>`

Gesamte zurückgelegte Strecke in Kilometern.

- **Typ:** Zahl
- **Beispiel:**`1500`

#### `<serviceinterval>`

Empfohlenes Wartungsintervall in Stunden.

- **Typ:** Zahl
- **Beispiel:**`40`

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
- **Beispiel:**`Radkontakte gereinigt, Getriebe geölt`

---

## Vollständige XML-Struktur

```
<locos>
  └── <loco> (Attribute: uid, name, length, index)
      ├── <model> (Attribute: manufacturer, scale, catalognumber)
      │   ├── <description>
      │   ├── <operator>
      │   ├── <class>
      │   ├── <serialnumber>
      │   ├── <tractiontype>
      │   ├── <weight>
      │   ├── <vmax>
      │   ├── <image>
      │   └── <notes>
      ├── <decoder>
      │   ├── <protocol>
      │   ├── <address>
      │   ├── <addresstype>
      │   ├── <functiontable>
      │   │   └── <function> (mehrere, Attribute: no, description, actuation, type, visible, image)
      │   └── <speedtable>
      │       └── <speed> (mehrere, Attribute: step, v)
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

### Neue Lokomotive hinzufügen

```xml
<loco uid="eindeutige-uuid-hier" name="ICN Zug" length="25" index="2">
  <model manufacturer="Märklin" scale="H0" catalognumber="37521">
    <description>SBB ICN Zug</description>
    <operator>SBB</operator>
    <class>ICN</class>
    <serialnumber>500001</serialnumber>
    <tractiontype>electric</tractiontype>
    <weight>120</weight>
    <vmax>250</vmax>
    <image>locoimages/icn.png</image>
    <notes>Hochgeschwindigkeitszug</notes>
  </model>
  <!-- ... decoder und operation Abschnitte ... -->
</loco>
```

### Wartungseintrag hinzufügen

```xml
<servicetable>
  <service date="2024-06-01">
    <item>Radkontakte gereinigt, Getriebe geölt</item>
  </service>
  <service date="2024-12-15">
    <item>Räder kontrolliert und gereinigt</item>
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

### Antriebsarten

- `electric` - Elektrolokomotiven
- `diesel` - Diesellokomotiven
- `steam` - Dampflokomotiven
- `hybrid` - Hybridantrieb

### Standard-DCC-Funktionsnummern

- **0:** Lichter (immer verfügbar)
- **1-5:** Häufige Geräusche/Funktionen
- **6-8:** Zusätzliche Effekte
- **9+:** Erweiterte Funktionen

---

## Validierungsregeln

- Alle`uid` Werte müssen eindeutig sein
- `address` und`addresstype` müssen kompatibel sein (kurz: 1-127, lang: 128-10239)
- Fahrstufen sollten 1-27 für DCC128 sein
- Datumsformate müssen ISO 8601 sein (YYYY-MM-DD)
- Bildpfade sollten relativ zum Projektverzeichnis existieren
- Funktionsnummern sollten in einer Lokomotive nicht doppelt vorkommen

---

## Verwandte Dateien

- `loco.xml` - Aktive Lokomotivendefinitionen (vereinfachte Version)
- `train_template.xml` - Zugkomposition-Vorlagen
- `railcar_template.xml` - Güterwagen-Vorlagen
- `plan.xml` - Gleisplan-Definitionen
