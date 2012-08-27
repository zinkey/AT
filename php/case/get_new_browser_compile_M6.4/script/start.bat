set version=http://tangram.baidu.com/js/conf.js
%cd%\wget\wget -t 0 %version% -O %cd%/version.ini

cscript.exe version.js

set /p trueVersion=<version.ini

set url=http://img.baidu.com/js/tangram-base-%trueVersion%.js
%cd%\wget\wget -t 0 %url% -O %cd%/tangram-all.js 

del /q version.ini

::set /p from=<from.txt
cscript.exe reader.js from.txt

::set /p str=<tangram.txt

setlocal enabledelayedexpansion
for %%i in (*.ini) do ( 
	set name=%%i
	set /p content=<%%i
	set url=http://tangram.baidu.com/import.php?f=!content!
	%cd%\wget\wget -t 0 !url! -O %cd%/!name!.js  
)

cscript.exe update.js  from.txt


del /q tangram-all.js
pause