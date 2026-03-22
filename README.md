## Overview

The purpose of this application is to provide a simple way to keep an eye on several simultaneously running EVE Online clients and to easily switch between them. While running it shows a set of live thumbnails for each of the active EVE Online clients. These thumbnails allow fast switch to the corresponding EVE Online client either using mouse or configurable hotkeys.

It's essentially a task switcher, it does not relay any keyboard/mouse events and suchlike. The application works with EVE, EVE through Steam, or any combination thereof.

If you have questions come to the Discord and chat.

Discord: https://discord.gg/xYt8R9AFXB

The program does NOT (and will NOT ever) do the following things:

* modify EVE Online interface
* display modified EVE Online interface
* broadcast any keyboard or mouse events
* anyhow interact with EVE Online except of bringing its main window to foreground or resizing/minimizing it

<div style="page-break-after: always;"></div>

**Under any conditions you should NOT use EVE-O Preview for any actions that break EULA or ToS of EVE Online.**

If you have find out that some of the features or their combination of EVE-O Preview might cause actions that can be considered as breaking EULA or ToS of EVE Online you should consider them as a bug and immediately notify the Developer ( Devilen ) via in-game mail or contact via Discord Server: 

<div style="page-break-after: always;"></div>

## How To Install & Use

1. Download and extract the contents of the .zip archive to a location of your choice (ie: Desktop, CCP folder, etc)
..* **Note**: Please do not install the application into the *Program Files* or *Program files (x86)* folders. These folders in general do not allow applications to write anything there while EVE-O Preview now stores its configuration file next to its executable, thus requiring the write access to the folder it is installed into.
2. Start up both EVE-O Preview and your EVE Clients (the order does not matter)
3. Adjust settings as you see fit. Program options are described below

Video Guides:

* [Eve online , How To : EVE-O Preview (multiboxing; legal)](https://youtu.be/2r0NMKbogXU)

Support:
* Discord: https://discord.gg/xYt8R9AFXB

## Development Details
We have migrated to a unified source code where Windows and Linux share the same code base. When doing do we also implemented the ability to build the source using Github Actions. If you would like to make changed to the code for yourself, fork this repo, make changes to the code as you see fit and actions will build the .exe files. If you would like to share your code changes with the community open a PR for approval.

## System Requirements

* Windows 7, Windows 8/8.1, Windows 10, Windows 11, Linux Wine
* Microsoft .NET Framework 4.8+
* EVE clients Display Mode should be set to **Fixed Window** or **Window Mode**. **Fullscreen** mode is not supported.

<div style="page-break-after: always;"></div>

## EVE Online EULA/ToS

This application is legal under the EULA/ToS:

CCP FoxFour wrote:
> Please keep the discussion on topic. The legitimacy of this software has already been discussed
> and doesn't need to be again. Assuming the functionality of the software doesn't change, it is
> allowed in its current state.

CCP Grimmi wrote:
> Overlays which contain a full, unchanged, EVE Client instance in a view only mode, no matter
> how large or small they are scaled, like it is done by EVE-O Preview as of today, are fine
> with us. These overlays do not allow any direct interaction with the EVE Client and you have
> to bring the respective EVE Client to the front/put the window focus on it, in order to
> interact with it.

<div style="page-break-after: always;"></div>

## Application Options

### Application Options Available Via GUI

#### **General** Tab
| Option | Description |
| --- | --- |
| Minimize to System Tray | Determines whether the main window form be minimized to windows tray when it is closed |
| Track client locations | Determines whether the client's window position should be restored when it is activated or started |
| Hide preview of active EVE client | Determines whether the thumbnail corresponding to the active EVE client is not displayed |
| Propagate single left-click | If enabled, a single left-click on a preview is forwarded to the EVE client after activation |
| Propagate single right-click | If enabled, a single right-click on a preview is forwarded to the EVE client after activation |
| Minimize inactive EVE clients | Allows to auto-minimize inactive EVE clients to save CPU and GPU |
| Animation Style | Use original animation style (0) or No Animation style (1). You may find Original is cleaner with fixed window mode and No Animation is cleaner with windowed mode. Especially when using minimize inactive clients.
| Hide caption bar on clients | Hides (or shows) caption bar on eve clients |
| Previews always on top | Determines whether EVE client thumbnails should stay on top of all other windows |
| Hide previews when EVE client is not active | Determines whether all thumbnails should be visible only when an EVE client is active |
| Unique layout for each EVE client | Determines whether thumbnails positions are different depending on the EVE client being active |
| Use preview crop region | Enables cropping the preview source to a configured rectangle (see `PreviewCropRegion` below) |

When `Use preview crop region` is enabled:
* Mouse wheel over a thumbnail zooms the crop region in/out (toward cursor position)
* Left-click + drag on a thumbnail pans the crop region
* Right-click drag keeps moving/resizing thumbnail windows as usual

#### **Thumbnail** Tab
| Option | Description |
| --- | --- |
| Opacity | Determines the inactive EVE thumbnails opacity (from almost invisible 20% to 100% solid) |
| Thumbnail Width | Thumbnails width. Can be set to any value from **100** to **640** points |
| Thumbnail Height | Thumbnails Height. Can be set to any value from **80** to **400** points |
| Lock Thumbnail Location | Lock position of thumbnails, preventing misclicks moving your thumbnails |
| Thumbnail Snap to Grid | Force Thumbnails to snap to defined grid when moved |
| Snap X / Snap Y | X/Y grid Pixels |
| Do not display previews | Prevent previews to show of clients - unless overridden by PerClient settings |
| Do not display previews background color | Background colour to use for preview windows if not showing preview (and not overridden by PerClient settings). Do NOT select #000001 else you will be clashing with Transparency settings. |

#### **Zoom** Tab
| Option | Description |
| --- | --- |
| Zoom on hover | Determines whether a thumbnail should be zoomed when the mouse pointer is over it  |
| Zoom factor | Thumbnail zoom factor. Can be set to any value from **2** to **10** |
| Zoom anchor | Sets the starting point of the thumbnail zoom |

#### **Overlay** Tab
| Option | Description |
| --- | --- |
| Show overlay | Determines whether a name of the corresponding EVE client should be displayed on the thumbnail |
| Show frames | Determines whether thumbnails should be displays with window caption and borders |
| Highlight active client | Determines whether the thumbnail of the active EVE client should be highlighted with a bright border |
| Color | Color used to highlight the active client's thumbnail in case the corresponding option is set |
| Cycle Group Indicator Position | The position of Exclude from Cycle Group Indicator |
| Label Font | The font name, style and size of the overlay label (Character Name) |
| Label Color | The color of the Font for the Overlay Label |
| Position | The position of the overlay label in the thumbnail |

#### **Active Clients** Tab
| Option | Description |
| --- | --- |
| Thumbnails list | List of currently active EVE client thumbnails. Checking an element in this list will hide the corresponding thumbnail. However these checks are not persisted and on the next EVE client or EVE-O Preview run the thumbnail will be visible again |

<div style="page-break-after: always;"></div>

### Mouse Gestures and Actions

Mouse gestures are applied to the thumbnail window currently being hovered over.

| Action | Gesture |
| --- | --- |
| Activate the EVE Online client and bring it to front  | Click the thumbnail |
| Minimize the EVE Online client | Hold Control key and click the thumbnail |
| Toggle the EVE Online client inclusion in the configured cycle groups[| Hold Shift key and click the thumbnail |
| Switch to the last used application that is not an EVE Online client | Hold Control + Shift keys and click any thumbnail |
| Move thumbnail to a new position | Press right mouse button and move the mouse |
| Adjust thumbnail height | Press both left and right mouse buttons and move the mouse up or down |
| Adjust thumbnail width | Press both left and right mouse buttons and move the mouse left or right |

<div style="page-break-after: always;"></div>

### Configuration File-Only Options

Some of the application options are not exposed in the GUI. They can be adjusted directly in the configuration file.

**Note:** Do any changes to the configuration file only while the EVE-O Preview itself is closed. Otherwise the changes you made might be lost.

| Option | Description |
| --- | --- |
| **ActiveClientHighlightThickness** | <div style="font-size: small">Thickness of the border used to highlight the active client's thumbnail.<br />Allowed values are **1**...**6**.<br />The default value is **3**<br />For example: **"ActiveClientHighlightThickness": 3**</div> |
| **CompatibilityMode** | <div style="font-size: small">Enables the alternative render mode (see below)<br />The default value is **false**<br />For example: **"CompatibilityMode": true**</div> |
| **EnablePreviewCrop** | <div style="font-size: small">Enable cropping of the source area shown in thumbnails.<br />The default value is **false**.<br />For example: **"EnablePreviewCrop": true**</div> |
| **PreviewCropRegion** | <div style="font-size: small">Rectangle inside each client window to capture, using pixels and format `{ "X": 0, "Y": 0, "Width": 800, "Height": 450 }`.<br />If Width/Height are invalid, full client area is used.<br />For example: **"PreviewCropRegion": { "X": 0, "Y": 0, "Width": 800, "Height": 450 }**</div> |
| **EnableThumbnailSnap** | <div style="font-size: small">Allows to disable thumbnails snap feature by setting its value to **false**<br />The default value is **true**<br />For example: **"EnableThumbnailSnap": true**</div> |
| **HideThumbnailsDelay** | <div style="font-size: small">Delay before thumbnails are hidden if the **General** -> **Hide previews when EVE client is not active** option is enabled<br />The delay is measured in thumbnail refresh periods<br />The default value is **2** (corresponds to 1 second delay)<br />For example: **"HideThumbnailsDelay": 2**</div> |
| **HideLoginClientThumbnail** | <div style="font-size: small">Hide EVE login window clients. If an Eve online client is sat at character selection screen - hide the preview window for this client<br />The default value is **false**<br />For example: **"HideLoginClientThumbnail": false**</div> |
| **PropagateSingleClick** | <div style="font-size: small">Enable forwarding of single left-click on a preview to the target client after activation.<br />The default value is **false**.<br />For example: **"PropagateSingleClick": true**</div> |
| **PropagateSingleRightClick** | <div style="font-size: small">Enable forwarding of single right-click on a preview to the target client after activation.<br />The default value is **false**.<br />For example: **"PropagateSingleRightClick": true**</div> |
| **PriorityClients** | <div style="font-size: small">Allows to set a list of clients that are not auto-minimized on inactivity even if the **Minimize inactive EVE clients** option is enabled. Listed clients still can be minimized using Windows hotkeys or via _Ctrl+Click_ on the corresponding thumbnail<br />The default value is empty list **[]**<br />For example: **"PriorityClients": [ "EVE - Phrynohyas Tig-Rah", "EVE - Ondatra Patrouette" ]**</div> |
| **ThumbnailMinimumSize** | <div style="font-size: small">Minimum thumbnail size that can be set either via GUI or by resizing a thumbnail window. Value is written in the form "width, height"<br />The default value is **"100, 80"**.<br />For example: **"ThumbnailMinimumSize": "100, 80"**</div> |
| **ThumbnailMaximumSize** | <div style="font-size: small">Maximum thumbnail size that can be set either via GUI or by resizing a thumbnail window. Value is written in the form "width, height"<br />The default value is **"640, 400"**.<br />For example: **"ThumbnailMaximumSize": "640, 400"**</div> |
| **ThumbnailRefreshPeriod** | <div style="font-size: small">Thumbnail refresh period in milliseconds. This option accepts values between **300** and **1000** only.<br />The default value is **500** milliseconds. For LINUX build this can go down to **10**<br />For example: **"ThumbnailRefreshPeriod": 500**</div> |
| **ThumbnailResizeTimeoutPeriod** | <div style="font-size: small">Thumbnail Resize Timeout period in milliseconds. This option accepts values between **200** and **5000** only.<br />The default value is **500** milliseconds.<br />For example: **"ThumbnailResizeTimeoutPeriod": 500**. If you are having the preview windows resize incorrectly on startup increase this value.</div> |
| **ExecutablesToPreview** | <div style="font-size: small">List of executables to display preview windows for. List of strings.<br />The default value is **"exefile"**.<br />For example: **"ExecutablesToPreview": ["exefile","wow","Diablo IV"]**. If you are having the preview windows resize incorrectly on startup increase this value.</div> |
| **IconName** | <div style="font-size: small">The icon you wish to use for Eve-O-Preview.<br />The default value is **""** which would equate to **IconOriginal**.<br />If an invalid or empty value is used, **IconOriginal** will be used.<br />Valid values are : **IconOriginal**, **IconDefault**, **IconAmber**, **IconBlue**, **IconCherry**, **IconDal**, **IconDark**, **IconMint**, **IconPurple** and **IconUrns**</div> |

<div style="page-break-after: always;"></div>

### Hotkey Setup

It is possible to set a key combinations to immediately jump to certain EVE window. However currently EVE-O Preview doesn't provide any GUI to set the these hotkeys. It should be done via editing the configuration file directly. Don't forget to make a backup copy of the file before editing it.

**Note**: Don't forget to make a backup copy of the file before editing it.

Open the file using any text editor. find the entry **ClientHotkey**. Most probably it will look like

    "ClientHotkey": {},

This means that no hotkeys are defined. Edit it to be like

    "ClientHotkey": {
      "EVE - Phrynohyas Tig-Rah": "F1",
      "EVE - Ondatra Patrouette": "F2"
    }

This simple edit will assign **F1** as a hotkey for Phrynohyas Tig-Rah and **F2** as a hotkey for Ondatra Patrouette, so pressing F1 anywhere in Windows will immediately open EVE client for Phrynohyas Tig-Rah if he is logged on.

The following hotkey is described as `modifier+key` where `modifier` can be **Control**, **Alt**, **Shift**, or their combination. F.e. it is possible to setup the hotkey as

    "ClientHotkey": {
      "EVE - Phrynohyas Tig-Rah": "F1",
      "EVE - Ondatra Patrouette": "Control+Shift+F4"
    }

**Note:** Do not set hotkeys to use the key combinations already used by EVE. It won't work as "_I set hotkey for my DPS char to F1 and when I'll press F1 it will automatically open the DPS char's window and activate guns_". Key combination will be swallowed by EVE-O Preview and NOT retranslated to EVE window. So it will be only "_it will automatically open the DPS char's window_".

<div style="page-break-after: always;"></div>

### Cycle Clients with Hotkey Setup

In a similar pattern to the per client Hotkey Setup, It is possible to set a key combinations to cycle through select Eve Windows. EVE-O Preview doesn't provide any GUI to set the these hotkeys. It should be done via editing the configuration file directly. Don't forget to make a backup copy of the file before editing it.

If you have not run EVE-O Preview before, or since this feature was added then it is recommended to quickly open and close EVE-O Preview once to trigger the config to update with some sample values. 

**Note**: Don't forget to make a backup copy of the file before editing it.

Open the file using any text editor. find the entries **CycleGroup1ForwardHotkeys** and **CycleGroup1BackwardHotkeys**. Most probably it will look like

    "CycleGroup1ForwardHotkeys": [
      "F14",
      "Control+F14"
    ],
    "CycleGroup1BackwardHotkeys": [
      "F13",
      "Control+F13"
    ]

**Note**: It is highly recommended to leave the Hotkey values as default and bind them with a gaming device if you can support it.

Next find the entry **CycleGroup1ForwardHotkeys**. Most probably it will look like

    "CycleGroup1ClientsOrder": {
      "EVE - Example DPS Toon 1": 1,
      "EVE - Example DPS Toon 2": 2,
      "EVE - Example DPS Toon 3": 3
    }

You should modify this entry with a list of each of your clients replacing "Example DPS Toon 1", etc with the name of your character. The numbers on the right are used to force the order in which they cycle.
If a character appears in the list but is not currently logged in, then it will simply be skipped.
If a character does not appear in the list, then they will never become active when cycling clients.
If "EVE" is used instead of a character name - then this is taken to mean active clients with no active character (ie on character selection screen). This would allow you to setup a cycle group to go through clients at character selection screen. EVE-O Preview will cycle through all clients in this state.
If no clients defined here but Hotkeys are defined then ALL active clients will be cycled through with those hotkeys.

By now you may have noticed that there are multiple groups. The above configuration can be followed for a second group by using the values **CycleGroup2ForwardHotkeys**, **CycleGroup2BackwardHotkeys**, and **CycleGroup2ForwardHotkeys**
This may provide useful if you want to have one HotKey to cycle through a group of DPS characters, while another HotKey cycles through support roles such as gate scouts, or a group of logi. Cyclegroups are numbered 1 through 5.

Alternatively you may not want to use any of these HotKeys. Please note that deleting the values in their entirety will simply result in them being automatically re-generated.
Should you wish to remove these HotKeys completely, Simply set the values to empty, such as the example below:

    "CycleGroup1ForwardHotkeys": [],
	  "CycleGroup1BackwardHotkeys": [],
	  "CycleGroup1ClientsOrder": {},
	  "CycleGroup2ForwardHotkeys": [],
	  "CycleGroup2BackwardHotkeys": [],
	  "CycleGroup2ClientsOrder": {},
	  "CycleGroup3ForwardHotkeys": [],
	  "CycleGroup3BackwardHotkeys": [],
	  "CycleGroup3ClientsOrder": {},
	  "CycleGroup4ForwardHotkeys": [],
	  "CycleGroup4BackwardHotkeys": [],
	  "CycleGroup4ClientsOrder": {},
	  "CycleGroup5ForwardHotkeys": [],
	  "CycleGroup5BackwardHotkeys": [],
	  "CycleGroup5ClientsOrder": {}

### Minimize All Clients with Hotkey Setup

In a similar pattern to the per client Hotkey Setup, It is possible to set a key combinations to Minimize all the Eve Windows. EVE-O Preview doesn't provide any GUI to set the these hotkeys. It should be done via editing the configuration file directly. Don't forget to make a backup copy of the file before editing it.

  "MinimizeAllClientsHotkeys": [
    "F22"
  ],


**Hints** 
* Minimise the use of modifiers or standard keys to minimise issues with the client playing up. In the default example unusual Function keys (e.g. F14) are used which are then bound to a game pad or gaming mouse.
* The Eve client can be somewhat less than stable, often getting confused as client focus switches. It is near certain that you will experience issues such as keys sticking or even in some cases D-Scan running each time the client swaps. So far I have found no perfect solution and opt for the most stable solution instead, of sticking to the F14+ keys.
* For the best experience try to use the Control modifier. In the default example F14 is used to cycle to the next client, but if pressed mid locking a target (Control + Clicking) then the client will not cycle. By registering Control+F4 as an additional hotkey, the client will cycle.
* For a list of supported keys, see: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys

### Per Client Border Color
Have you ever wanted your main client to show up in a different color so that it more easily catches your eye? Or maybe your Logi to stand out?

EVE-O Preview doesn't provide any GUI to set the these per client overrides as yet. Though, It can be done via editing the configuration file directly. 
**Note** Don't forget to make a backup copy of the file before editing it.

Open the file using any text editor. find the entry **PerClientActiveClientHighlightColor**. Most probably it will look like

    "PerClientActiveClientHighlightColor": {
      "EVE - Example Toon 1": "Red",
      "EVE - Example Toon 2": "Green"
    }

You should modify this entry with a list of each of your clients replacing "Example Toon 1", etc with the name of your character. The names on the right represent which highligh color to use for that clients border.

If a client does not appear in this list, then it will use the global highlight color by default.

**Hint** For a list of supported colors see: https://docs.microsoft.com/en-us/dotnet/api/system.drawing.color#properties

### Per Client Thumbnail Size
Would you like to have different clients with different thumbnail sizes ?

EVE-O Preview doesn't provide any GUI to set the these per client overrides as yet. Though, It can be done via editing the configuration file directly. 
**Note** Don't forget to make a backup copy of the file before editing it.

Open the file using any text editor. find the entry **PerClientThumbnailSize**. Most probably it will look like

    "PerClientThumbnailSize": {
      "EVE - Example Toon 1": "240, 180",
      "EVE - Example Toon 2": "200, 100",
      "EVE": "320, 240"
    }

You should modify this entry with a list of each of your clients replacing "Example Toon 1", etc with the name of your character. The values on the right represent the size of the thumbnail.

If a client does not appear in this list, then it will use the global thumbnail size by default.

### Per Client Zoom Anchor
Would you like to have different clients with different ZoomAnchor for each thumbnail ?

EVE-O Preview doesn't provide any GUI to set the these per client overrides as yet. Though, It can be done via editing the configuration file directly. 
**Note** Don't forget to make a backup copy of the file before editing it.

Open the file using any text editor. find the entry **PerClientZoomAnchor**. Most probably it will look like

    "PerClientZoomAnchor": {
      "EVE - Example Toon 1": 1,
      "EVE - Example Toon 2": 2,
      "EVE": 3
    }

You should modify this entry with a list of each of your clients replacing "Example Toon 1", etc with the name of your character. The values on the right represent the Zoom Anchor for the thumbnail (which sets the starting point of the thumbnail zoom).
Valid values are 0-8 : 0-NW, 1-North, 2-NE, 3-West, 4-Center, 5-East, 6-SW, 7-South, 8-SE.

If a client does not appear in this list, then it will use the global Zoom Anchor by default.

### Per Client Prevent Preview
Would you like to prevent previews of some clients, or enforce preview of some clients ?

EVE-O Preview doesn't provide any GUI to set the these per client overrides as yet. Though, It can be done via editing the configuration file directly. 
**Note** Don't forget to make a backup copy of the file before editing it.

Open the file using any text editor. find the entry **PerClientZoomAnchor**. Most probably it will look like

  "PerClientPreventPreviews": {
      "EVE - Example Toon 1": false,
      "EVE - Example Toon 2": true
    }

You should modify this entry with a list of each of your clients replacing "Example Toon 1", etc with the name of your character. The values on the right allow you to enforce always showing preview (if not minimized) or never showing preview.

If a client does not appear in this list, then it will use the global Prevent Preview by default.

### Per Client Prevent Preview Color
Would you like different background colors for specific clients when you prevent preview on those clients ?

EVE-O Preview doesn't provide any GUI to set the these per client overrides as yet. Though, It can be done via editing the configuration file directly. 
**Note** Don't forget to make a backup copy of the file before editing it.

Open the file using any text editor. find the entry **PerClientPreventPreviewColor**. Most probably it will look like

"PerClientPreventPreviewColor": {
      "EVE - Example Toon 1": "Red",
      "EVE - Example Toon 2": "Blue"
    }

You should modify this entry with a list of each of your clients replacing "Example Toon 1", etc with the name of your character. The values on the right allow you to set a background color for when that client has prevent preview true (either Per Client or global). Do NOT select #000001 else you will be clashing with Transparency settings.

If a client does not appear in this list, then it will use the global Prevent Preview Color by default.

### Compatibility Mode

This setting allows to enable an alternate thumbnail render. This render doesn't use advanced DWM API to create live previews. Instead it is a screenshot-based render with the following pros and cons:
* `+`  Should work even in remote desktop environments
* `-`  Consumes significantly more memory. In the testing environment EVE-O Preview did consume around 180 MB to manage 3 thumbnails using this render. At the same time the primary render did consume around 50 MB when run in the same environment.
* `-`  Thumbnail images are refreshed at 1 FPS rate
* `-`  Possible short mouse cursor freezes

### Release build

Release builds are generated using github actions when a release is created from tag. If you wish to build locally the variable EVEOTARGET controls Linux or Windows source inclusion.

You can build using : 

* dotnet build src\\Eve-O-Preview\\Eve-O-Preview.csproj --configuration Release -p:EVEOTARGET="Linux" -p:AssemblyVersion="8.0.2.0"
* dotnet build src\\Eve-O-Preview\\Eve-O-Preview.csproj --configuration Release   -p:EVEOTARGET="Windows" -p:AssemblyVersion="8.0.2.0"

<div style="page-break-after: always;"></div>

## Credits

### Maintained by

* Devilen
  
* Dal Shooth

* Izakbar


### Created by

* StinkRay


### Previous maintainers

* Aura Asuna

* Phrynohyas Tig-Rah
 
* Makari Aeron

* StinkRay


### With contributions from

* CCP FoxFour


### Forum thread

https://forums.eveonline.com/t/eve-o-preview-v8-0-2-0/463600


### Original repository

https://bitbucket.org/ulph/eve-o-preview-git

<div style="page-break-after: always;"></div>

## CCP Copyright Notice

EVE Online, the EVE logo, EVE and all associated logos and designs are the intellectual property of CCP hf. All artwork, screenshots, characters, vehicles, storylines, world facts or other recognizable features of the intellectual property relating to these trademarks are likewise the intellectual property of CCP hf. EVE Online and the EVE logo are the registered trademarks of CCP hf. All rights are reserved worldwide. All other trademarks are the property of their respective owners. CCP hf. has granted permission to Eve-O-Preview to use EVE Online and all associated logos and designs for promotional and information purposes on its website but does not endorse, and is not in any way affiliated with, Eve-O-Preview. CCP is in no way responsible for the content on or functioning of this program, nor can it be liable for any damage arising from the use of this program. 


