# Dokumentation train.xml

## Inhalt

Die `train.xml` Datei definiert Zugzusammensetzungen und deren Konfigurationen für den Modellbahnbetrieb. Sie enthält Informationen über die Zugkomposition, bestehend aus Lokomotiven und Wagen, sowie deren physikalische Eigenschaften und Betriebsparameter.

## Dateistruktur

### Root-Element: `<trains>`

Container-Element für die einzelnen Züge

```xml
<trains>
  <!-- Zug-Einträge hier -->
</trains>
```

---

## Zugelement: `<train>`

Definiert einen kompletten Zug mit Lokomotive(n) und Wagen

### Attribute

| Attribut | Typ  | Beschreibung                                  | Beispiel                               |
| ---------- | ------ | ----------------------------------------------- | ---------------------------------------- |
| `uid`    | UUID | Eindeutige Kennung des Zuges                  | `d8487146-7006-4f48-b26c-7e01ac243b03` |
| `name`   | Text | Anzeigename des Zuges                         | `Güterzug 1`                           |
| `length` | Zahl | Gesamtlänge des Zuges (in global konfigurierter Masseinheit) | `110`                                  |
| `index`  | Zahl | Anzeigeposition in der Konfiguration          | `1`                                    |

### Unterelemente

---

## 1. Zug-Basisdaten

### `<description>`

Detaillierte Beschreibung des Zuges

- **Typ:** Text
- **Beispiel:** `Güterzug mit Kieswagen`
- **Hinweis:** Sollte den Zugstyp und besondere Merkmale beschreiben

### `<weight>`

Gesamtgewicht des gesamten Zuges (inklusive aller Wagen) in Tonnen

- **Typ:** Zahl
- **Beispiel:** `1200`
- **Berechnung:** Summe aus Lokomotive(n) + alle Wagen

### `<vmax>`

Maximale Betriebsgeschwindigkeit des Zuges in km/h

- **Typ:** Zahl
- **Beispiel:** `80`
- **Hinweis:** Sollte die Höchstgeschwindigkeit aller beteiligten Fahrzeuge berücksichtigen (meist die langsamste)

### `<image>`

Symbolbild für den Zug (relativ zum Projektverzeichnis)

- **Typ:** Text
- **Beispiel:** `trainimages/gueterzug1.png`
- **Format:** PNG, SVG oder andere gängige Bildformate

### `<notes>`

Zusätzliche Notizen oder Bemerkungen zum Zug

- **Typ:** Text
- **Beispiel:** `Wagen Weihacher Kies`
- **Hinweis:** Kann Besonderheiten, Herkunft oder spezielle Konfigurationen dokumentieren

---

## 2. Fahrzeugliste: `<vehicles>`

Container für alle Fahrzeuge des Zuges in ihrer Reihenfolge

```xml
<vehicles>
  <loco f_uid="..." position="1" />
  <railcar f_uid="..." position="2" />
  <!-- weitere Fahrzeuge -->
</vehicles>
```

### Fahrzeugelemente: `<loco>` und `<railcar>`

#### `<loco>` - Lokomotive

Referenziert eine Lokomotive aus `loco.xml`

##### Attribute

| Attribut    | Typ  | Beschreibung                                     | Beispiel                               |
| ------------- | ------ | -------------------------------------------------- | ---------------------------------------- |
| `f_uid`     | UUID | Eindeutige Kennung der Lokomotive (aus loco.xml) | `f47ac10b-58cc-4372-a567-0e02b2c3d479` |
| `position`  | Zahl | Position im Zug (1 = Zuglok/Spitzenposition)     | `1`                                    |

**Hinweis:** Der Parameter `f_uid` steht für "foreign uid" (fremde/externe ID) und verweist auf die uid einer Lokomotive in der `loco.xml` Datei.

#### `<railcar>` - Wagen

Referenziert einen Wagen aus `railcar.xml`

##### Attribute

| Attribut    | Typ  | Beschreibung                                  | Beispiel                               |
| ------------- | ------ | ---------------------------------------------- | ---------------------------------------- |
| `f_uid`     | UUID | Eindeutige Kennung des Wagens (aus railcar.xml) | `g2b3c4d5-e6f7-8901-bcde-f01234567890` |
| `position`  | Zahl | Position im Zug (nach den Lokomotiven)          | `3`                                    |

**Hinweis:** Der Parameter `f_uid` verweist auf die uid eines Wagens in der `railcar.xml` Datei.

### Positionierung

- **Position 1-N:** Eindeutige Position im Zug von vorne nach hinten
- **Spitzenposition (1):** Normalerweise die Zuglok (erste Lokomotive)
- **Schublokomotive:** Kann auch am Ende des Zuges positioniert sein (für Schubfahrt)
- **Wagen:** Positionen 2 bis zum Ende des Zuges

---

## Vollständige XML-Struktur

```
<trains>
  └── <train> (Attribute: uid, name, length, index)
      ├── <description>
      ├── <weight>
      ├── <vmax>
      ├── <image>
      ├── <notes>
      └── <vehicles>
          ├── <loco> (Attribute: f_uid, position) - mehrere möglich
          └── <railcar> (Attribute: f_uid, position) - mehrere möglich
```

---

## Anwendungsbeispiele

### Reisezug mit einer Lokomotive

```xml
<train uid="a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c" name="IC 1234" length="185" index="1">
  <description>Intercity Zug SBB mit 6 Personenwagen</description>
  <weight>650</weight>
  <vmax>200</vmax>
  <image>trainimages/IC1234.png</image>
  <notes>Tagesverkehr Zürich - Bern</notes>
  <vehicles>
    <loco f_uid="f47ac10b-58cc-4372-a567-0e02b2c3d479" position="1" />
    <railcar f_uid="a1a1a1a1-b2b2-b2b2-c3c3-c3c3c3c3c3c3" position="2" />
    <railcar f_uid="b2b2b2b2-c3c3-c3c3-d4d4-d4d4d4d4d4d4" position="3" />
    <railcar f_uid="c3c3c3c3-d4d4-d4d4-e5e5-e5e5e5e5e5e5" position="4" />
    <railcar f_uid="d4d4d4d4-e5e5-e5e5-f6f6-f6f6f6f6f6f6" position="5" />
    <railcar f_uid="e5e5e5e5-f6f6-f6f6-a7a7-a7a7a7a7a7a7" position="6" />
    <railcar f_uid="f6f6f6f6-a7a7-a7a7-b8b8-b8b8b8b8b8b8" position="7" />
  </vehicles>
</train>
```

### Güterzug mit mehreren Lokomotiven

```xml
<train uid="d8487146-7006-4f48-b26c-7e01ac243b03" name="Güterzug 1" length="110" index="2">
  <description>Güterzug mit Kieswagen (Doppeltraktion)</description>
  <weight>1200</weight>
  <vmax>80</vmax>
  <image>trainimages/gueterzug1.png</image>
  <notes>Schwerer Güterzug, zwei Lokomotiven erforderlich</notes>
  <vehicles>
    <loco f_uid="f47ac10b-58cc-4372-a567-0e02b2c3d479" position="1" />
    <loco f_uid="f1a2b3c4-d5e6-7890-abcd-ef0123456789" position="2" />
    <railcar f_uid="g2b3c4d5-e6f7-8901-bcde-f01234567890" position="3" />
    <railcar f_uid="h3c4d5e6-f7g8-9012-cdef-012345678901" position="4" />
    <railcar f_uid="i4d5e6f7-g8h9-0123-def0-123456789012" position="5" />
    <railcar f_uid="j5e6f7g8-h9i0-1234-ef01-234567890123" position="6" />
    <railcar f_uid="k6f7g8h9-i0j1-2345-f012-345678901234" position="7" />
    <railcar f_uid="l7g8h9i0-j1k2-3456-0123-456789012345" position="8" />
  </vehicles>
</train>
```

### Güterzug mit Schublokomotive

```xml
<train uid="e9f8g7h6-i5j4-k3l2-m1n0-o0p1q2r3s4t5" name="Güterzug mit Schub" length="125" index="3">
  <description>Güterzug mit Schubunterstützung am Zugschluss</description>
  <weight>1400</weight>
  <vmax>60</vmax>
  <image>trainimages/gueterzug-schub.png</image>
  <notes>Steile Strecke, Schublokomotive erforderlich</notes>
  <vehicles>
    <loco f_uid="f47ac10b-58cc-4372-a567-0e02b2c3d479" position="1" />
    <railcar f_uid="g2b3c4d5-e6f7-8901-bcde-f01234567890" position="2" />
    <railcar f_uid="h3c4d5e6-f7g8-9012-cdef-012345678901" position="3" />
    <railcar f_uid="i4d5e6f7-g8h9-0123-def0-123456789012" position="4" />
    <railcar f_uid="j5e6f7g8-h9i0-1234-ef01-234567890123" position="5" />
    <loco f_uid="f1a2b3c4-d5e6-7890-abcd-ef0123456789" position="6" />
  </vehicles>
</train>
```

### Personen-Nahverkehr (S-Bahn)

```xml
<train uid="f0a1b2c3-d4e5-f6a7-b8c9-d0e1f2a3b4c5" name="S5 3402" length="95" index="4">
  <description>S-Bahn Zug, 4-teilig</description>
  <weight>320</weight>
  <vmax>160</vmax>
  <image>trainimages/sbahn.png</image>
  <notes>Mehrteiliger Triebzug, elektrisch angetrieben</notes>
  <vehicles>
    <loco f_uid="e5e5e5e5-f6f6-f6f6-a7a7-a7a7a7a7a7a7" position="1" />
    <railcar f_uid="a1a1a1a1-b2b2-b2b2-c3c3-c3c3c3c3c3c3" position="2" />
    <railcar f_uid="b2b2b2b2-c3c3-c3c3-d4d4-d4d4d4d4d4d4" position="3" />
    <loco f_uid="d4d4d4d4-e5e5-e5e5-f6f6-f6f6f6f6f6f6" position="4" />
  </vehicles>
</train>
```

---

## Referenztabellen

### Zugstypen (nach Verwendung)

| Typ | Beschreibung | Typische Zusammensetzung |
| ----- | -------------- | ----------------------- |
| **Reisezug** | Personenbeförderung Fernverkehr | 1 Lok + 6-8 Personenwagen |
| **S-Bahn** | Nahverkehr, oft mehrteilig | 2-4 Triebköpfe + Mittelwagen |
| **Regional-Zug** | Regionaler Personenverkehr | 1-2 Lok + 3-4 Wagen |
| **Güterzug** | Güterbeförderung | 1-3 Lok + 8-15+ Güterwagen |
| **Mischtransport** | Personen und Güter | 1-2 Lok + gemischte Wagen |
| **Spezialzug** | Besondere Güter/Zwecke | Variabel |

### Typische Geschwindigkeiten

| Zugtyp | vmax (km/h) |
| --------- | ------------- |
| Güterzug | 60-80 |
| Regionalzug | 120-140 |
| S-Bahn | 120-160 |
| Intercity | 160-200 |
| Hochgeschwindigkeit | 200-300+ |

### Typische Zuggewichte

| Zugtyp | Gewicht (Tonnen) |
| --------- | ------------------ |
| S-Bahn 3-teilig | 250-350 |
| Regionalzug | 300-500 |
| IC 6-teilig | 500-700 |
| Güterzug | 800-1500+ |

---

## Validierungsregeln

- Alle `uid` Werte müssen eindeutig sein
- Alle `f_uid` Referenzen müssen auf existierende Fahrzeuge in `loco.xml` oder `railcar.xml` verweisen
- Positionen müssen eindeutig und aufeinanderfolgend (1, 2, 3, ...) sein
- Erste Position sollte normalerweise eine Lokomotive sein
- `vmax` sollte der minimalen Höchstgeschwindigkeit aller Fahrzeuge entsprechen
- `weight` sollte der Summe aller Fahrzeuggewichte entsprechen oder etwas darunter (ohne Kupplungen, etc.)
- `length` sollte realistisch für die Anzahl der Fahrzeuge sein

---

## Wichtige Konzepte

### Zugkomposition

Eine Zugkomposition ist eine Anordnung von Lokomotiven und Wagen. Züge können:

- **Einfache Traktion:** 1 Lokomotive zieht mehrere Wagen
- **Doppeltraktion (Mehrfachtraktion):** 2 oder mehr Lokomotiven ziehen zusammen (z.B. für schwere Lasten)
- **Schubfahrt:** Eine Lokomotive am Zugschluss schiebt (z.B. für Bergstrecken)
- **Triebzüge:** Mehrere Triebköpfe (spezielle Lokomotiven mit Passagierräumen) ziehen Mittelwagen

### Bezug zu anderen Dateien

```
train.xml
  ├─ loco.xml (Referenzen via f_uid)
  ├─ railcar.xml (Referenzen via f_uid)
  └─ timetable.xml (Züge fahren nach Fahrplan)
```

Züge verbinden die Fahrzeugsdefinitionen (`loco.xml`, `railcar.xml`) mit den Fahrplänen (`timetable.xml`).

### Berechnung der Zugparameter

#### Gesamtlänge
```
Gesamtlänge = Summe(Fahrzeuglängen) + Kupplungsabstände
```

Beispiel: 2 Lok à 15m + 5 Wagen à 25m = 30m + 125m = 155m (ohne Kupplungen)

#### Gesamtgewicht
```
Gesamtgewicht = Summe(alle Fahrzeuggewichte)
```

Beispiel: 2 Lok à 80t + 8 Wagen à 85t = 160t + 680t = 840t

#### Maximale Geschwindigkeit
```
vmax = min(vmax_loko, vmax_wagen1, vmax_wagen2, ...)
```

Die vmax des Zuges ist die niedrigste vmax aller beteiligten Fahrzeuge.

---

## Best Practices

### 1. Realistische Zusammensetzung

Züge sollten realistische Konfigurationen widerspiegeln:
- Schwere Güterzüge benötigen starke Lokomotiven
- Leichte Personenzüge können mit schwächeren Lokomotiven fahren
- Steilstrecken erfordern möglicherweise Mehrfachtraktion oder Schublok

### 2. Konsistente Nummern

Die Zugnummern sollten konsistent mit echten Fahrplan-Nummern sein:
- IC-Züge: IC 1000-1999
- Regional-Züge: RE 1000-1999
- S-Bahnen: S1, S2, etc.
- Güterzüge: Beliebig oder nach Betriebsmuster

### 3. Bilddateien

Zugsymbole sollten:
- Im Verzeichnis `trainimages/` gespeichert sein
- Aussagekräftig benannt sein
- Konsistente Größe und Format haben (z.B. 200x100 Pixel)

### 4. Wartung von f_uid Referenzen

Bei Änderungen an `loco.xml` oder `railcar.xml`:
- `f_uid` muss aktualisiert werden, wenn sich die uuid eines Fahrzeugs ändert
- Orphaned Referenzen (nicht vorhandene Fahrzeuge) sollten bereinigt werden
- Validierungstools können helfen, fehlerhafte Referenzen zu erkennen

### 5. Test und Validierung

Vor der Verwendung sollten Züge getestet werden auf:
- Gültige f_uid Referenzen
- Realistische Gewichte und Geschwindigkeiten
- Sinnvolle Zusammensetzung
- Verfügbarkeit der Bilddateien

---

## Verwandte Dateien

- `loco.xml` - Lokomotivendefinitionen (Quelle für f_uid)
- `railcar.xml` - Wagondefinitionen (Quelle für f_uid)
- `train_template.xml` - Zugvorlagen und Konfigurationsbeispiele
- `timetable.xml` - Fahrpläne (referenziert Züge)
- `plan.xml` - Gleisplan und Strecken
- `station.xml` - Bahnhöfe und Haltestellen

---

## Häufig gestellte Fragen

### F: Kann ein Zug ohne Lokomotive fahren?
**A:** Nein. Ein Zug benötigt mindestens eine Lokomotive, um fahren zu können. Triebzüge sind spezielle Lokomotiven mit Passagierräumen.

### F: Wie viele Lokomotiven kann ein Zug haben?
**A:** Theoretisch unbegrenzt, praktisch üblicherweise 1-3. Mehrfachtraktion ist für schwere Güterzüge oder Bergstrecken üblich.

### F: Kann die Lokomotive am Ende des Zuges stehen?
**A:** Ja, bei Schubfahrt (z.B. in Bergstrecken) kann eine Schublok am Ende positioniert sein.

### F: Was passiert, wenn eine f_uid nicht existiert?
**A:** Das ist ein Fehler. Die Referenz sollte auf ein gültiges Fahrzeug in der entsprechenden Datei (loco.xml oder railcar.xml) verweisen.

### F: Wie beeinflusst die Reihenfolge der Fahrzeuge die Simulation?
**A:** Die Position bestimmt die physikalische Anordnung im Zug und ist wichtig für:
- Realistische Darstellung
- Korrekte Gewichtsverteilung (simuliert)
- Fahrdynamik und Beschleunigung
- Kupplung und Bremskräfte

