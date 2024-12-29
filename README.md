# _KappiMod_ pack for MiSide

## Оглавление (Table of contents)

- [_KappiMod_ pack for MiSide](#kappimod-pack-for-miside)
  - [Оглавление (Table of contents)](#оглавление-table-of-contents)
  - [Информация о модификациях (English version below)](#информация-о-модификациях-english-version-below)
    - [Поддерживаемые загрузчики модов](#поддерживаемые-загрузчики-модов)
    - [Список модификаций](#список-модификаций)
    - [Список патчей](#список-патчей)
  - [Установка мод пака](#установка-мод-пака)
  - [Назначение клавиш](#назначение-клавиш)
  - [Information about modifications (English)](#information-about-modifications-english)
    - [Supported mod loaders](#supported-mod-loaders)
    - [Mod list](#mod-list)
    - [Patch list](#patch-list)
  - [Mod pack installation](#mod-pack-installation)
  - [Key bindings](#key-bindings)
  - [For developers](#for-developers)

## Информация о модификациях (English version below)

Последняя версия мод пака доступна [здесь](https://github.com/MrSago/MiSide-KappiMod/releases/latest).

### Поддерживаемые загрузчики модов

|  Загрузчик  | Поддержка |
| :---------: | :-------: |
| MelonLoader |    ✅     |
|   BepInEx   |    ✅     |

### Список модификаций

Названия кликабельны, если есть необходимость поставить моды или патчи из списков отдельно.

|                                          Мод                                          | Статус |
| :-----------------------------------------------------------------------------------: | :----: |
|                                       GUI мода                                        |   ✅   |
|                                 Разблокировка консоли                                 |   ✅   |
| [Увеличение мощности фонарика](https://github.com/MrSago/MiSide-Flashlight-Increaser) |   ✅   |
|                                   Возможность лечь                                    |   ⭕   |
|                              Установка FPS лимита в GUI                               |   ✅   |
|       [Разблокировка приседания](https://github.com/MrSago/MiSide-Sit-Unlocker)       |   ✅   |
|       [Разблокировка спринта](https://github.com/MrSago/MiSide-Sprint-Unlocker)       |   ✅   |
|                                  Скроллер time scale                                  |   ✅   |

> _Примечание: ставить отдельно модификации можно, но ставить их с мод паком не имеет смысла, т.к. они замещают функционал друг друга._

Установленные настройки в GUI мода сохраняются!

### Список патчей

|       Патч       |             Статус              |
| :--------------: | :-----------------------------: |
|  Пропуск интро   |               ✅                |
| Пропуск диалогов | **⚠️ МОЖЕТ ВЫЗВАТЬ СОФТЛОК ⚠️** |

Пропуск диалогов включается в GUI (клавиша F1).

## Установка мод пака

- Установить один из поддерживаемых загрузчиков

  - [MelonLoader](https://github.com/LavaGang/MelonLoader/releases)
    - Просто используйте **установщик** последней версии
  - [BepInEx](https://github.com/BepInEx/BepInEx/releases)
    - Нам нужен **BepInEx-Unity.IL2CPP-win-x64-6.x.x** (6-ая версия)
    - Распакуйте содержимое архива в папку с игрой

- Скачать [последнюю версию мода](https://github.com/MrSago/MiSide-KappiMod/releases/latest) **под нужный загрузчик:**

  - KappiMod.MelonLoader.zip - для MelonLoader
  - KappiMod.BepInEx.zip - для BepInEx

- Извлечь файлы из архива в папку с игрой

- Веселитесь

## Назначение клавиш

|           Клавиша            |               Действие                |
| :--------------------------: | :-----------------------------------: |
|              F1              |           Открыть GUI мода            |
|     **~** (удерживание)      |     Открыть внутриигровую консоль     |
|              F               | Увеличить дальность и радиус фонарика |
|            LShift            |                  Бег                  |
|            LCtrl             |              Приседание               |
| LShift + Колесико мыши вверх |      Увеличить time scale на 0.1      |
| LShift + Колесико мыши вниз  |      Уменьшить time scale на 0.1      |
| LShift + Клик колесика мыши  |  Переключить time scale между 0 и 1   |

---

## Information about modifications (English)

Latest version of the mod pack is available [here](https://github.com/MrSago/MiSide-KappiMod/releases/latest).

### Supported mod loaders

| Mod loader  | Supported |
| :---------: | :-------: |
| MelonLoader |    ✅     |
|   BepInEx   |    ✅     |

### Mod list

The names are clickable if there is a need to install mods or patches from the lists separately.

|                                      Mod                                      | Status |
| :---------------------------------------------------------------------------: | :----: |
|                                    Mod GUI                                    |   ✅   |
|                               Console unlocker                                |   ✅   |
| [Flashlight increaser](https://github.com/MrSago/MiSide-Flashlight-Increaser) |   ✅   |
|                                  Lying down                                   |   ⭕   |
|                             Set FPS limit in GUI                              |   ✅   |
|         [Sit unlocker](https://github.com/MrSago/MiSide-Sit-Unlocker)         |   ✅   |
|      [Sprint unlocker](https://github.com/MrSago/MiSide-Sprint-Unlocker)      |   ✅   |
|                              Time scale scroller                              |   ✅   |

> _Note: you can install modifications separately, but installing them with a mod pack does not make sense, because they replace each other's functionality._

The installed settings in the GUI of the mod are saved!

### Patch list

|      Patch       |            Status            |
| :--------------: | :--------------------------: |
|  Intro skipper   |              ✅              |
| Dialogue skipper | **⚠️ MAY CAUSE SOFTLOCK ⚠️** |

Skipping dialogues is enabled in the GUI (F1 key).

## Mod pack installation

- Install one of the supported mod loaders:

  - [MelonLoader](https://github.com/LavaGang/MelonLoader/releases)
    - Just use the latest version **installer**
  - [BepInEx](https://github.com/BepInEx/BepInEx/releases)
    - We need **BepInEx-Unity.IL2CPP-win-x64-6.x.x** (6th version)
    - Unzip the contents of the archive into the game folder

- Download [the latest version of the mod](https://github.com/MrSago/MiSide-KappiMod/releases/latest) for **the required loader**:

  - KappiMod.MelonLoader.zip - for the MelonLoader
  - KappiMod.BepInEx.zip - for the BepInEx

- Extract files from the archive into the game folder

- Have fun

## Key bindings

|            Key             |                 Action                  |
| :------------------------: | :-------------------------------------: |
|             F1             |              Open mod GUI               |
|        **~** (hold)        |          Open in game console           |
|             F              | Increase flashlight distance and radius |
|           LShift           |                 Sprint                  |
|           LCtrl            |                Sit down                 |
|  LShift + Mouse wheel up   |       Increase time scale by 0.1        |
| LShift + Mouse wheel down  |       Decrease time scale by 0.1        |
| LShift + Mouse wheel click |    Switch time scale between 0 and 1    |

## For developers

_Soon..._
