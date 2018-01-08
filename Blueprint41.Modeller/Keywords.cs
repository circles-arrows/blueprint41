using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller
{
    public sealed class Keywords
    {
        private static volatile Keywords instance;
        private static object syncRoot = new Object();
        private List<string> reservedWords = null;

        private Keywords()
        {
            this.reservedWords = File.ReadAllLines(@"keywords.txt", Encoding.UTF8).ToList();
        }

        public static Keywords Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Keywords();
                    }
                }

                return instance;
            }
        }

        public bool Contains(string word)
        {
            if (reservedWords.Where(x => x == word).Count() > 0) return true;
            return false;
        }
    }
}
