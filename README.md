Create Local or URL shortcut in Windows Console.
> 1. Download [exe file](https://github.com/winp/extra-shortcut/releases/download/1.0.0/eshortcut.exe).
> 2. Copy to `C:\Program_Files\Scripts`.
> 3. Add `C:\Program_Files\Scripts` to `PATH` environment variable.


```batch
> eshortcut [options] <target path/url>
:: [-o|--output <output file>]
:: [-s|--window-style <normal (default)/max/min>]
:: [-k|--hot-key <hot key>]
:: [-i|--icon-location <icon path/resource>]
:: [-d|--description <description>]
:: [-w|--working-directory <working directory path>]
:: [-a|--arguments <input arguments>]

:: [] -> optional argument
:: <> -> argument value
```

```batch
:: create shortcut to google
> eshortcut http://www.google.co.in

:: create shortcut to periodic table song
> eshortcut --output PeriodicTableSong.url https://www.youtube.com/watch?v=VgVQKCcfwnU

:: create shortcut to halo
> eshortcut "C:\Program Files (x86)\Halo Combat Evolved\Halo.exe"

:: create shortcut to watch dogs in desktop
> eshortcut -o {{Desktop}}\Watch_Dogs.lnk "C:\Program Files (x86)\R.G. Mechanics\Watch Dogs\bin\watch_dogs.exe"

:: create shortcut for internet test in start menu
> eshortcut -o {{StartMenu}}\Programs\InternetTest.lnk --arguments www.google.com ping

:: details of more special folders here (https://www.google.co.in/#q=WshSpecialFolders)
```


[![Merferry](https://i.imgur.com/GD8BoRC.jpg)](https://merferry.github.io)
