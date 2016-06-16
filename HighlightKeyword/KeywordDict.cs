using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighlightKeyword
{
    class KeywordDict
    {
        #region 私有变量
        //存放关键词的最大长度
        private const int MAX_WORD_LENGTH = 15;

        //存放所有关键词
        private Dictionary<string, string>[] words = new Dictionary<string, string>[ char.MaxValue + 1 ];

        private int maxStoreWordLength = 0;
        private int minStoreWordLength = int.MaxValue;

        //存放每个字符在对应位数上是否存在敏感词，以及哪几位是敏感词
        private short[] fastPositionCheck = new short[char.MaxValue + 1];
        
        private short[] fastLengthCheck = new short[char.MaxValue + 1];

        #endregion


        public Dictionary<string, string>[] Words
        {
            get { return words; }
        }

        public int MaxWordLength
        {
            get { return maxStoreWordLength; }
        }

        public int MinWordLength
        {
            get { return minStoreWordLength; }
        }

        public short[] FastCheck
        {
            get { return fastPositionCheck; }
        }

        public short[] FastLength
        {
            get { return fastLengthCheck; }
        }


        public bool AddWord( string word)
        {
            bool result = false;
            try
            {
                maxStoreWordLength = Math.Max(maxStoreWordLength, word.Length);
                minStoreWordLength = Math.Min(minStoreWordLength, word.Length);

                //这些运算符的运用简直了，神来之笔
                for (int i = 0; i < word.Length; i++)
                {
                    fastPositionCheck[word[i]] |= (short)(1 << i);
                }

                //记录以某个字开头的关键字的长度信息，左移位数长度为该字符串长度减一
                fastLengthCheck[word[0]] |= (short)(1 << (word.Length - 1));

                if (words[word[0]] == null)
                {
                    words[word[0]] = new Dictionary<string, string>();
                }

                //字典
                if (!words[word[0]].ContainsKey(word))
                {
                    words[word[0]].Add(word, word);
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }
    }
}
