//author:yaya,jihu
//uloveit.com.cn/template
//how to use?  YayaTemplate("xxx").render({});
var YayaTemplate = YayaTemplate || function(str){
			//核心分析方法
			var  _analyze=function(text){
					return text.replace(/{\$(\s|\S)*?\$}/g,function(s){	
								return s.replace(/("|\\)/g,"\\$1")
												.replace("{$",'_s.push("')
												.replace("$}",'");')
												.replace(/{\%([\s\S]*?)\%}/g, '",$1,"').replace(/\r\n|\n/g,"\\r\\n");
					});
			};
			//中间代码
			var _temp = "var _s=[];"+_analyze(str)+" return _s;";
			//返回生成器render方法
			return {
					render : function(mapping){
							var _a = [],_v = [],i;
							for (i in mapping){
									_a.push(i);
									_v.push(mapping[i]);
							}
							return (new Function(_a,_temp)).apply(null,_v).join("");
					}
			}
};