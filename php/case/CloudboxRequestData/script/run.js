/*
for(var i=0;i<2;i++){
	
}
at.setCursorPos({x:10,y:80});
at.sleep(1000);
at.setCursorEvent("leftdown");
at.setCursorEvent("leftup");
at.sleep(1000);
at.setCursorPos({x:10,y:95});
at.sleep(1000);
at.setCursorEvent("leftdown");
at.setCursorEvent("leftup");
*/

at.start("FEcache-yw.bat");

at.sleep(1000);


var pid = at.start("D:\\Fiddler2\\Fiddler.exe");
at.sleep(3000);
var pid2 = at.start("D:\\baidu\\BaiduBrowser\\baidubrowser.exe");


at.sleep(6000);

//点击网盘
at.setCursorPos({x:1712,y:78});
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");
at.sleep(5000);

//点击fiddler
at.sleep(100);
at.setCursorPos({x:108,y:1060});
at.sleep(100);
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");

at.sleep(5000);


//点击Edit
at.setCursorPos({x:53,y:33});

at.sleep(2000);
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");

at.sleep(1000);

//点击Select All
at.sleep(100);
at.setCursorPos({x:71,y:101});
at.sleep(100);
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");

at.sleep(1000);

//点击右键
at.sleep(100);
at.setCursorPos({x:1700,y:500});
at.sleep(100);
at.setCursorEvent("rightdown");
at.sleep(100);
at.setCursorEvent("rightup");


//点击autoscale chart
at.sleep(1000);

at.setCursorPos({x:1750,y:510});
at.sleep(100);
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");

at.sleep(1000);

//点击右键
at.setCursorPos({x:1700,y:500});
at.sleep(100);
at.setCursorEvent("rightdown");
at.sleep(100);
at.setCursorEvent("rightup");

at.sleep(1000);


//点击copy
at.setCursorPos({x:1750,y:540});
at.sleep(100);
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");

at.sleep(1000);

at.start("run.bat");

at.sleep(5000);




//粘贴图片

at.setCursorPos({x:886,y:500});
at.sleep(100);
at.setCursorEvent("rightdown");
at.sleep(100);
at.setCursorEvent("rightup");

at.sleep(1000);

at.setCursorPos({x:958,y:681});
at.sleep(100);
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");

at.sleep(3000);

at.setCursorPos({x:1895,y:11});
at.sleep(100);
at.setCursorEvent("leftdown");
at.sleep(100);
at.setCursorEvent("leftup");

at.sleep(100);

at.stop(pid);

at.sleep(500);

at.stop(pid2);

at.sleep(1000);

at.start("copy.bat");