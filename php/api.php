<?php
@$name = $_GET["name"];
try{
	$james=fopen("info.html","w");
	fwrite($james,"case\\".$name."\\script\\run.js");
	fclose($james);
 	echo "ok";
 }
 catch(Exception $e){
	echo "error";
 }
?>