using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Blueprint41.Modeller.keywords.txt";

            this.reservedWords = new List<string>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (reader.Peek() >= 0)
                {
                    this.reservedWords.Add(reader.ReadLine());
                }                
            }
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
