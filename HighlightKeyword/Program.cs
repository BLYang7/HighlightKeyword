using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HighlightKeyword
{
    class Program
    {
        static void Main(string[] args)
        {
            Common common = new Common();
            KeywordDict keywordDict = new KeywordDict();
            common.readFile(ref keywordDict);
            
            KeywordManager keywordManager = new KeywordManager(keywordDict);

            string pageData = common.readString("test.txt");

            //记录程序运行时间
            Stopwatch myWatch = Stopwatch.StartNew();

            ///对pageData的处理
            keywordManager.CheckNormalWord(pageData);

            //记录程序运行时间并显示
            myWatch.Stop();
            Console.WriteLine("time cost: {0} ms", myWatch.ElapsedMilliseconds.ToString());

            Console.ReadKey();
        }
    }
}
