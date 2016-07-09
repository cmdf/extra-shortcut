# oshortcut

Create Local or URL shortcut in Windows Console.


## usage

```batch
> oshortcut [options] <target path/url>
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
> oshortcut http://www.google.co.in

:: create shortcut to periodic table song
> oshortcut --output PeriodicTableSong.url https://www.youtube.com/watch?v=VgVQKCcfwnU

:: create shortcut to halo
> oshortcut "C:\Program Files (x86)\Halo Combat Evolved\Halo.exe"

:: create shortcut to watch dogs in desktop
> oshortcut -o {{Desktop}}\Watch_Dogs.lnk "C:\Program Files (x86)\R.G. Mechanics\Watch Dogs\bin\watch_dogs.exe"

:: create shortcut for internet test in start menu
> oshortcut -o {{StartMenu}}\Programs\InternetTest.lnk --arguments www.google.com ping

:: details of more special folders here (https://www.google.co.in/#q=WshSpecialFolders)
```


:: license

Do you have a Poké ball? That's enough!
