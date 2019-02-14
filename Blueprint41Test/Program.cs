using Blueprint41.Modeller.Schemas;
using System.Threading.Tasks;

namespace Blueprint41Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlPath = @"modeller.xml";
            modeller model = modeller.Load(xmlPath);
            DatastoreModelDocumentGenerator.Instance.ShowAndGenerateDocument(model).Wait();
        }
    }
}
