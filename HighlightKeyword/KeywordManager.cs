using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighlightKeyword
{
    class KeywordManager
    {
        private KeywordDict keywordDict;
        public KeywordManager( KeywordDict keywordDict)
        {
            this.keywordDict = keywordDict;
        }

        public void CheckNormalWord(string text )
        {
            
            int index = 0;
            while (index < text.Length)
            {

                //首先判断当前字符是否是某关键字的第一个字符，不是时就继续向下遍历
                if ((keywordDict.FastCheck[text[index]] & 1) == 0)
                {
                    do
                    {
                        index++;
                    }
                    while ( (index < text.Length) && ((keywordDict.FastCheck[text[index]] & 1) == 0) );

                    if (index >= text.Length)
                    {
                        break;
                    }
                }

                //此时已经判定，当前的这个字符会出现在关键词的第一位上
                //在判断
                char begin = text[index];
                int jump = 1;
                for (int j = 0; j <= Math.Min(keywordDict.MaxWordLength, text.Length - index - 1); j++)
                {
                    char current = text[index + j];
                    
                    //判断当前字符是否会出现在关键字的对应位上，实现快速判断
                    if ((keywordDict.FastCheck[current] & (1 << Math.Min(j, keywordDict.MaxWordLength))) == 0)
                    {
                        break;
                    }
                    
                    //当判决的长度大于关键字的最小长度时，当前的截取字符串有可能会是关键字，要做详细判定
                    if (j + 1 >= keywordDict.MinWordLength)
                    {
                        if ((keywordDict.FastLength[begin] & (1 << Math.Min(j, keywordDict.MaxWordLength))) > 0)
                        {
                            string sub = text.Substring(index, j + 1);

                            //在字典中搜索判断，得出结论。同时给出跳转位数，供下一次跳转用
                            if (keywordDict.Words[begin] != null && keywordDict.Words[begin].ContainsKey(sub))
                            {
                                jump = sub.Length;
                                Console.WriteLine(sub);
                            }
                        }
                    }
                }

                index += jump;
            }
        }
    }
}



