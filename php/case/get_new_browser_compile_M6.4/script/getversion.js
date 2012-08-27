(function () {
	var console = {
		log: function (info) {
			WScript.StdOut.WriteLine(info);
		}
	};

	var fso = new ActiveXObject("Scripting.FileSystemObject");

	function readFileText(filename) {
		if (!fso.FileExists(filename)) return "";
		var adostream = new ActiveXObject("ADODB.Stream");
		adostream.Type = 2; //TextStreamType;
		adostream.Charset = "utf-8";
		adostream.Open();
		adostream.LoadFromFile(filename);
		var contents = adostream.ReadText();
		adostream.Close();
		adostream = null;
		return contents;
	}

	function writeFileText(filename, text) {
		//console.log(["writeFileText:", filename].join(" "));
		var adostream = new ActiveXObject("ADODB.Stream");
		adostream.Type = 2; // TextStreamType;
		adostream.Charset = "utf-8";
		adostream.Open();
		adostream.WriteText(text);
		var adostream2 = new ActiveXObject("ADODB.Stream");
		adostream2.Type = 1;
		adostream2.Open();
		adostream.Position = 3; // Remove BOM
		adostream.CopyTo(adostream2);
		adostream2.Position = 0;
		adostream2.SaveToFile(filename, 2);
		adostream2.Close();
		adostream2 = null;

		adostream.Close();
		adostream = null;
	}
	

	function process() {
		

		

		var template = readFileText("output.template.html");

		var content = readFileText("html.txt");


		var reg = /tooltip="Success" \/>[^#]*#(\d*)/;


		content.replace(reg,function(a,b){

			console.log(template);


			eval(readFileText("template.js"));

			var outputcontent = YayaTemplate(template).render({
				version:b
			});

			console.log(outputcontent);

			writeFileText("output.html",outputcontent);

		});

	}

	process();
})();	