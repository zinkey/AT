using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using Noesis.Javascript;
using System.Web;


namespace 自动化项目
{


   


    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class From1 : Form
    {



        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hPos,
        int x, int y, int cx, int cy, uint nflags);





        private bool start = false;
        static bool isrun = false;

       
        


        private static string dir;
        private static string scriptdir;
        private static string runninghtml;
        private static string freehtml;
        private static string port;
        private static string casename;
        private static HttpListener listener = new HttpListener();

        public struct PONITAPI
        {
            public int x, y;
        }

        [DllImport("user32.dll")]
        public static extern int GetCursorPos(ref PONITAPI p);

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);


        public class Cmd
        {
            private string reqname;

            public Cmd(string name) {

                reqname = name;
                
            }

            public void sleep(int time)
            {
                Thread.Sleep(time);
            }

            public int getScreenWidth(String obj)
            {

                Console.WriteLine(obj);
                return 123456;
            }

            public void log(string iString)
            {
                Console.WriteLine(iString);
            }

            public void alert(string iString)
            {
                MessageBox.Show(iString);
            }

            public int start(string iString)
            {
                iString = iString.Replace("/", "\\");
                int id = -1;
                if (iString.IndexOf("\\") < 0)
                {
                    iString = scriptdir.Replace("#{name}",reqname) + iString;
                }
                try
                {
                    Process p = Process.Start(iString);
                    id = p.Id;
                    Console.WriteLine("start id=========================" + id);
                    //Process.GetProcessById(id).Close();
                    //p.WaitForExit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error:Cmd.start " + iString + " " + e.Message);
                    return id;
                }
                return id;
            }

            public void stop(int id) {
                try
                {
                    Console.WriteLine("stop id============" + id);
                    Process.GetProcessById(id).Kill();
                }
                catch (Exception e) { 
                }
            }
         

            public void setCursorPos(int x, int y)
            {
                try
                {
                    SetCursorPos(x, y);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error:Cmd.setCursorPos " + x + "  " + y + " " + e.Message);
                }

            }

            public void setCursorEvent(int type)
            {
                mouse_event((int)type, 0, 0, 0, IntPtr.Zero);
            }

            static PONITAPI p;
            public object getCursorPos()
            {
                GetCursorPos(ref p);
                return new JavascriptContext().Run("eval({x:" + p.x + ",y:" + p.y + "})");
            }
        }

        static void run(object obj)
        {
            listener = new HttpListener();
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            listener.Prefixes.Add("http://*:"+port+"/");
            listener.Start();
            Console.WriteLine("Listening");
            try
            {
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    
                    string name = context.Request.QueryString["name"].ToString();
                    
                    

                    if (name != null)
                    {
                        Console.WriteLine(context.Request.ContentEncoding.EncodingName);
                        Console.WriteLine("name！！！！！！！！！！！！！！！" + name);
                        name = HttpUtility.UrlDecode(name, context.Request.ContentEncoding);
                        casename = name;
                        //scriptdir = scriptdir.Replace("#{name}", name);
                        Console.WriteLine(context.Request.HttpMethod);
                        Console.WriteLine("name==============================" + name);
                    }
                    else {
                        name = "";
                    }
                    ProcessHttpClient(context, name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                listener.Stop();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        static void jsrun(Object obj)
        {
            string reqname = obj.ToString();
            isrun = true;
            FileInfo loadfile = new FileInfo(scriptdir.Replace("#{name}",reqname) + "run.js");
            if (loadfile.Exists)
            {
                StreamReader sr = loadfile.OpenText();
                String js = sr.ReadToEnd();
                sr.Dispose();
                Console.WriteLine(js);
                using (JavascriptContext context = new JavascriptContext())
                {
                    context.SetParameter("atdotnetcommand", new Cmd(reqname));
                    js = @"

var at = {
    start:function(str){
        return atdotnetcommand.start(str);
    },
    stop:function(id){
         atdotnetcommand.stop(id);
    },
    getCursorPos:function(){
        return atdotnetcommand.getCursorPos();
    },
    alert:function(str){
        atdotnetcommand.alert(str);
    },
    log:function(str){
        atdotnetcommand.log(str);
    },
    setCursorPos:function(pos){
        atdotnetcommand.setCursorPos(pos.x,pos.y);
    },
    setCursorEvent:function(type){
        var map = {
            LEFTDOWN : 0x2,
            LEFTUP : 0x4,
            MIDDLEDOWN : 0x20,
            MIDDLEUP : 0x40,
            MOVE : 0x1,
            ABSOLUTE : 0x8000,
            RIGHTDOWN : 0x8,
            RIGHTUP : 0x10
        };
        atdotnetcommand.setCursorEvent(map[type.toUpperCase()]);
    },
    sleep:function(time){
        atdotnetcommand.sleep(time);
    }
};
                            " + js;
                    try
                    {
                        context.Run(js);
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            isrun = false;
        }


        static void ProcessHttpClient(HttpListenerContext obj,string reqname)
        {

            HttpListenerContext cxt = obj as HttpListenerContext;
            HttpListenerRequest request = cxt.Request;
            HttpListenerResponse response = cxt.Response;

            string responseString;

            if (isrun)
            {
                responseString = runninghtml;
            }
            else
            {
                responseString = freehtml;
            }
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();

            if (!isrun)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(jsrun, reqname);
            }
        }

        public From1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (start)
            {
                listener.Close();
                label1.Text = "status:stop";
                button1.Text = "start";
                start = false;
                timer1.Enabled = true;
            }
            else
            {
                FileInfo loadfile = new FileInfo("config");
                if (loadfile.Exists)
                {
                    StreamReader sr = loadfile.OpenText();
                    String config = sr.ReadToEnd();
                    sr.Dispose();
                    using (JavascriptContext context = new JavascriptContext())
                    {
                        context.Run("var config = eval(" + config + @");
var dir = config.dir;
var runninghtml  =config.runninghtml;
var freehtml = config.freehtml;
var port = config.port;
                        ");
                        object configdir = context.GetParameter("dir");
                        object configrunninghtml = context.GetParameter("runninghtml");
                        object configfreehtml = context.GetParameter("freehtml");
                        object configport = context.GetParameter("port");
                        dir = configdir.ToString();
                        scriptdir = dir + @"case\#{name}\script\";
                        runninghtml = configrunninghtml.ToString();
                        freehtml = configfreehtml.ToString();
                        port = configport.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("need config file");
                    return;
                }
                System.Threading.ThreadPool.QueueUserWorkItem(run);
                label1.Text = "status:running";
                button1.Text = "stop";
                start = true;
                //timer1.Enabled = false;
            }
        }


        
        private void timer1_Tick(object sender, EventArgs e)
        {
            PONITAPI pos = new PONITAPI();
            GetCursorPos(ref pos);
            label2.Text = pos.y+" "+pos.x;
        }

        private void From1_Load(object sender, EventArgs e)
        {
            IntPtr HWND_TOPMOST = new IntPtr(-1);
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, 0x0001 | 0x0002);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
