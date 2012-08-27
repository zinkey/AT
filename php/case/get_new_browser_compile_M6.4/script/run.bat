::copy %~dp0output.html %~dp0..\output\output.html

del "%cd%\html.txt" /q
del "%cd%\output.html" /q

set url=http://tc-bar-safe03.tc.baidu.com:8235/hudson/view/Browser_RB/job/browser_compile_2.64.2/
%cd%\wget\wget -t 0 %url% -O %cd%/html.txt


cscript.exe getversion.js

del "%cd%\html.txt" /q

::set /p version=<num.txt

::set downloadurl=http://tc-bar-safe03.tc.baidu.com:8235/hudson/view/Browser_Trunk/job/browser_compile/%version%/artifact/baidubrowser/output/bdbrowser_setup.exe
::%cd%\wget\wget -t 0 %downloadurl% -O %cd%/browser_%version%.exe
