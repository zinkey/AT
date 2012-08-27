<?php

$dir = dirname(__FILE__);


@$fp=opendir($dir);

while(false!=$file=@readdir($fp))
{
    if($file!='.' &&$file!='..')
    {
    	$file = "$file";
		$file = iconv('gb2312','utf-8',$file);
		echo $file;
		exit;
    }
}
echo "";
?>