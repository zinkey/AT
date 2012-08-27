<?php

include("config.php");


 $folder=$dir."case\\";


@$fp=opendir($folder);




while(false!=$file=readdir($fp))
{

    if($file!='.' &&$file!='..')
    {


		


        //$file="$file";

		$file = iconv('gb2312','utf-8',$file);


		$readme = $folder.$file."\\readme.txt";
		@$handle = fopen($readme, "r");
		$contents = "";
		if ($handle){
			@$contents = file_get_contents($readme);
		}
		@fclose($handle);

        $arr_file[] = array(
			"name"=>"$file",
			"readme"=>iconv('gb2312','utf-8',$contents)
		);

    }
}


if(is_array($arr_file))
{
    
    
    echo json_encode($arr_file);

}


closedir($fp);

?>