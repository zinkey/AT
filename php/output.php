<?php

include("config.php");


@$name = $_GET["name"];
@$key = $_GET["key"];



@$fp=fopen($dir."case\\".$name."\\output\\output.html",'r');
try{
	if (!$fp)
	{
		@fclose($fp);
	    echo'error';
	}
	else{
		@fclose($fp);
		exec("xcopy ".$dir."case\\".$name."\\output ".$dir."case\\".$name."\\result\\".$key." /e/r/y/i");
		exec("rd ".$dir."case\\".$name."\\output /s/q");
		exec("mkdir ".$dir."case\\".$name."\\output");
		echo'ok';
	}
}
catch(Exception $e){
	echo "error";
}
?>