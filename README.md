AT平台是一个自动化完成平台


## 介绍

AT内置javascript解析器，暴露了Native功能，可完成自动化测试等功能。

## Native方法

1.创建进程

	var pid = at.start("notepad");

2.结束进程

	at.stop(pid);

3.获取当前鼠标位置

	var pos = at.getCursorPos();//pos.x,pos.y

4.设置当前鼠标位置

	at.setCursorPos({
		x:100,
		y:100
	});

5.设置鼠标事件

	at.setCursorEvent("LEFTDOWN");//鼠标左键按下

6.休眠

	at.sleep(1000);//休眠1秒

## 使用方法

1.部署启动
	
	配置config文件，设置at服务的端口号等。配置php环境，设置对应路径。启动at.exe。

2.添加case

	run.js为每个case的启动脚本，output.html为运行结束生成页面，readme.txt为说明文件。添加了case后，刷新at项目页面会自动添加新的case，点击可直接运行，运行成功后会展示output.html页面。

© uloveit.com.cn 