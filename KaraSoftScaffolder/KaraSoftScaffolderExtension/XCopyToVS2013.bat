set  toPath=C:\Users\Ashraf\AppData\Local\Microsoft\VisualStudio\12.0\Extensions\s2g25zzi.222

set  fromPath=C:\Workspaces\GitHub\KaraSoftScaffolder\KaraSoftScaffolder\KaraSoftScaffolderExtension\bin\Debug

xcopy %fromPath%\*.dll %toPath%\ /s/d/y

xcopy %fromPath%\Templates\*.t4 %toPath%\Templates\ /s/d/y