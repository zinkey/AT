rd /q/s "%APPDATA%\Baidu\browser\DiskCache"
rd /q/s "%APPDATA%\Baidu\browser\SysData\Application Cache\data"
xcopy ApplicationCache.db "%APPDATA%\Baidu\browser\SysData\Application Cache\ApplicationCache.db" /e/r/y/i
