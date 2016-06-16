using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace HighlightKeyword
{
    class Common
    {

        //捕获网页数据
        public string getPageData(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "text/html";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)";

            try
            {
                string html = null;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                html = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(html))
                {
                    Console.WriteLine("Download OK.");
                }
                return html;
            }
            catch (IOException e)
            {
                Console.WriteLine( "error" );
                return "error";
            }
        }


        //将字符串保存到本地
        public void saveDataLocal( string pageData )
        {
            FileStream fs = null;
            try 
            {
                fs = new FileStream("E:\\page.html", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter( fs, Encoding.UTF8 );
                sw.WriteLine(pageData);
                sw.Close();
                fs.Close();
            }
            catch( IOException e )
            {
                Console.WriteLine( e.Message );
            }
        }

        // 从本地读取文件，并将文件保存到数据结构中
        public void readFile(ref KeywordDict keywordDict ) 
        {
            string strLine;
            try
            {
                FileStream fileStream = new FileStream("keyWord.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fileStream, System.Text.Encoding.Default);
                strLine = sr.ReadLine();
                while (strLine != null)
                {
                    keywordDict.AddWord(strLine);
                    strLine = sr.ReadLine();
                }
                sr.Close();
                fileStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        // 从本地读取文件，并将文件保存到数据结构中
        public string readString(string fileName)
        {
            string strLine;
            StringBuilder sb = new StringBuilder();
            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open);
                StreamReader sr = new StreamReader(fileStream, System.Text.Encoding.Default);
                strLine = sr.ReadLine();
                while (strLine != null)
                {
                    sb.Append(strLine);
                    strLine = sr.ReadLine();
                }
                sr.Close();
                fileStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return sb.ToString();
        }


    }
}
