# EVE-F-Preview 8.0.2.0

First official release of the **EVE-F-Preview** fork ([upstream: EVE-O Preview](https://bitbucket.org/ulph/eve-o-preview-git)).

## Install

1. Download `Release-v8.0.2.0-Windows.zip` (Windows build).
2. Extract to a folder with write access (not `Program Files`).
3. Run `EVE-F-Preview.exe`.
4. Settings are stored as `EVE-F-Preview.json` next to the exe. An existing `EVE-O-Preview.json` is loaded automatically if the new file is not present.

## What's new in this fork

- **Character portrait thumbnails** when **Do not display previews** is enabled (ESI cache in `thumbs/`).
- **Refresh portraits** button on the Thumbnail tab.
- **Lighter no-preview mode** — unregisters live DWM thumbnails to reduce GPU/CPU use.
- **Active-client highlight** drawn correctly for portrait mode (no transparency artifacts).
- **Cycle hotkey** reliability improvements (UI thread, focus handling).
- **Overwatch mode** — Ctrl+click a preview for an enlarged focused view.
- Rebranded executable and config: `EVE-F-Preview.exe` / `EVE-F-Preview.json`.

## Requirements

- Windows 10/11, .NET 8 runtime (included in self-contained Linux build only; Windows zip uses framework-dependent publish matching upstream).
- EVE clients in **Fixed Window** or **Window Mode** (not fullscreen).

See [README](https://github.com/Chris-Matthewson/eve-f-preview/blob/main/README.md) for full options and EULA notes.
