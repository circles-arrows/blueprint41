using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Gremlin;
using org.opencypher.gremlin.translation;
using org.opencypher.gremlin.translation.groovy;
using org.opencypher.gremlin.translation.translator;
using System;
using System.IO;
using System.Windows.Forms;
using Datastore.Manipulation;
using System.Collections.Generic;
using Blueprint41.DatastoreTemplates;

namespace Blueprint41Test
{
    public class MovieModel : DatastoreModel<MovieModel>
    {

        public override GraphFeatures TargetFeatures => GraphFeatures.Gremlin;

        protected override void SubscribeEventHandlers()
        {

        }

        [Version(0, 0, 0)]
        public void Initialize()
        {
            FunctionalIds.Default = FunctionalIds.New("Org", "O_", IdFormat.Numeric);

            Entities.New("Film")
                .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true)
                .AddProperty("title", typeof(string), false)
                .AddProperty("tagline", typeof(string))
                .AddProperty("release", typeof(int), false);

            Entities.New("Actor")
                 .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                .SetKey("Uid", true)
                .AddProperty("fullname", typeof(string), false)
                .AddProperty("born", typeof(int), false);

            Relations.New(Entities["Actor"], Entities["Film"], "PERSON_ACTED_IN_FILM", "ACTED_IN")
                .SetInProperty("ActedFilms", PropertyType.Collection)
                .SetOutProperty("Actor", PropertyType.Collection);

            Relations.New(Entities["Actor"], Entities["Film"], "PERSON_DIRECTED_FILM", "DIRECTED")
                .SetInProperty("DirectedFilms", PropertyType.Collection)
                .SetOutProperty("Directors", PropertyType.Collection);

            Relations.New(Entities["Actor"], Entities["Film"], "PERSON_PRODUCED_FILM", "PRODUCED")
                .SetInProperty("ProducedFilms", PropertyType.Collection)
                .SetOutProperty("Producers", PropertyType.Collection);

            Relations.New(Entities["Actor"], Entities["Film"], "PERSON_WROTE_FILM", "WROTE")
                .SetInProperty("FilmsWrited", PropertyType.Collection)
                .SetOutProperty("Writers", PropertyType.Collection);
        }

        [Version(0, 0, 1)]
        public void RefactorModel()
        {
            // TODO: Cosmos DB not supported property refactor rename
            // https://github.com/Azure/azure-cosmos-dotnet-v2/issues/566
            Entities["Actor"].Properties["fullname"].Refactor.Rename("name");

            // TODO: Supports Property Refactor Deprecate
            //Entities["Actor"].Properties["born"].Refactor.Deprecate();
            //          Entities["Film"].Properties["release"].Refactor.Deprecate();

            //            Entities["Actor"].Refactor.Rename("Actor");

            // TODO: Not Supported Relationship Refactor Rename
            //Relations["PERSON_DIRECTED_FILM"].Refactor.Rename("ACTOR_DIRECTED_FILM");
        }

        [Version(0, 0, 2)]
        public void RefactorModel2()
        {
            Entities["Actor"].Properties["born"].Refactor.Convert(typeof(List<string>), true);
            Entities["Actor"].Properties["born"].Refactor.MakeMandatory();
        }

            //[Version(0, 0, 2)]
            //public void RefactorModel2()
            //{
            //    //TODO: Static Data Supported add

            //    // Static Data
            //    Entities.New("AccountType")
            //        .HasStaticData(true)
            //        .AddProperty("Uid", typeof(string), false, IndexType.Unique)
            //        .SetKey("Uid", true)
            //        .AddProperty("Name", typeof(string))
            //        .AddProperty("Description", typeof(string));

            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "6", Name = "Account" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "81", Name = "MTMSAccount" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "89", Name = "FinancialAccount" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "90", Name = "BillingAccount" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "91", Name = "AxaptaAccount" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "349", Name = "Aircraft" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "350", Name = "Installation" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "351", Name = "SiteTrackingObject" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "352", Name = "StaticSite" });
            //    Entities["AccountType"].Refactor.CreateNode(new { Uid = "353", Name = "Vessel" });
            //}

            //[Version(0, 0, 3)]
            //public void RefactorModel3()
            //{
            //    Entities["Actor"].Properties["fullname"].Refactor.Rename("name");
            //}

            //[Version(0, 0, 4)]
            //public void RefactorModel4()
            //{

            //    Entities["Actor"].Properties["born"].Refactor.Deprecate();
            //    Entities["Film"].Properties["release"].Refactor.Deprecate();
            //}

            //[Version(0, 0, 5)]
            //public void RefactorModel5()
            //{            
            //    Entities["Actor"].Refactor.Deprecate();            
            //}

            //[Version(0, 0, 6)]
            //public void RefactorModel6()
            //{
            //    // TODO: NOt supported
            //    Entities["Film"].Refactor.Rename("Movie");
            //}
        }

    class Program
    {
        //[STAThread]
        static void Main(string[] args)
        {
            string hostname = "9caa0e3e-0ee0-4-231-b9ee.gremlin.cosmosdb.azure.com";
            int port = 443;
            string authKey = "8NTSGVQfWB5LWhPNutO5040rhZv8kene3pTS1dHHOG9xWWQ0oCYatdYcA6Z6S81RoCYnCjzWSqYye7bGAqvgQQ==";
            string database = "sample-database";
            string collection = "sample-graph";

            string uri = "bolt://localhost:7687";
            string username = "neo4j";
            string pass = "neo";
            //"bolt://localhost:7687", "neo4j", "neo"


            hostname = "localhost";
            port = 8182;
            authKey = database = collection = null;

            PersistenceProvider.Initialize(typeof(MovieModel), hostname, port, authKey, database, collection);
            //PersistenceProvider.Initialize(typeof(MovieModel), uri, username, pass);

            string projectFolder = Environment.CurrentDirectory + "\\..\\..\\";
            GeneratorSettings settings = new GeneratorSettings(projectFolder);
            GeneratorResult result = Generator.Execute<MovieModel>(settings);


            //MovieModel model = new MovieModel();
            //model.Execute(true);
            
            ////return;
            //using (Transaction.Begin())
            //{
            //    //for(int i = 0; i < 10; i++)
            //    //{

            //    Film matrix = new Film()
            //    {
            //        Uid = Guid.NewGuid().ToString(),
            //        title = "Film Relationship",
            //        release = 1999,
            //        tagline = "Welcome to the Real World"
            //    };

            //    Actor keanu = new Actor()
            //    {
            //        Uid = Guid.NewGuid().ToString(),
            //        fullname = "Actor Relationship"
            //    };

            //    keanu.ActedFilms.Add(matrix);

            //    //Film f = Film.Load("0a5e7812-cc1f-443d-b305-90a04c7f9d10");

            //    //Actor a = f.Actor[0];

            //    //Actor a = Actor.Load("2fd16c7b-463c-4a3c-b11b-b18fad1dc034");

            //    // List<Film> films = Film.GetAll();

            //    //Film matrix = new Film
            //    //{
            //    //    Uid = "matrix",
            //    //    title = "The Matrix",
            //    //    release = 1999,
            //    //    tagline = "Welcome to the Real World"
            //    //};


            //    Transaction.Commit();
            //}

            //string cypherText = File.ReadAllText(Environment.CurrentDirectory + "/moviegraph.txt");

            //string[] createcyphers = cypherText.Split(new string[] { "CREATE" }, StringSplitOptions.RemoveEmptyEntries);
            //string c = "";
            //foreach (string cy in createcyphers)
            //{
            //    c += "CREATE " + cy.Trim() + " ";
            //}

            // Console.ReadLine();

            return;

            string cypher = null;
            do
            {
                Console.WriteLine("Enter new cyper string:");
                cypher = Console.ReadLine();

                if (cypher == "q")
                    break;

                if (string.IsNullOrEmpty(cypher.Trim()))
                {
                    Console.WriteLine("Enter new cyper string:");
                    continue;
                }

                Console.WriteLine("========Cosmos=========");
                Console.WriteLine(Translate.ToCosmos(cypher));
                Console.WriteLine("=================");

                Console.WriteLine();
                Console.WriteLine();

                //CypherAst ast;
                //try
                //{
                //    ast = CypherAst.parse(cypher);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    continue;
                //}


                //Translator groovyTranlator = Translator.builder().gremlinGroovy().build();
                //Translator cosmosTranlator = Translator.builder().gremlinGroovy().build(TranslatorFlavor.cosmosDb());
                //Translator neptuneTranlator = Translator.builder().gremlinGroovy().build(TranslatorFlavor.neptune());

                //string g, c, n;

                //Console.WriteLine("========Groovy=========");
                //Console.WriteLine((g = ast.buildTranslation(groovyTranlator).ToString()));
                //Console.WriteLine("=================");

                //Console.WriteLine("========Neptune=========");
                //Console.WriteLine((n = ast.buildTranslation(neptuneTranlator).ToString()));
                //Console.WriteLine("=================");

                //Console.WriteLine("========Cosmos=========");
                //Console.WriteLine((c = ast.buildTranslation(cosmosTranlator).ToString()));
                //Console.WriteLine("=================");

                //Console.WriteLine();
                //Console.WriteLine();

                //Console.WriteLine("Type g (groovy), n (neptune) or c (cosmos) to copy to clipboard. Or antyhing to cancel");

                //string toCopy = Console.ReadLine();

                //if (toCopy == "g")
                //{
                //    Clipboard.SetText(g);
                //    Console.WriteLine("Copied to clipboard");
                //}

                //else if (toCopy == "c")
                //{
                //    Clipboard.SetText(c);
                //    Console.WriteLine("Copied to clipboard");
                //}

                //else if (toCopy == "n")
                //{
                //    Clipboard.SetText(n);
                //    Console.WriteLine("Copied to clipboard");
                //}

            } while (cypher != "q");


            //TranslationFacade cfog = new TranslationFacade();
            //string gremlin = cfog.toGremlinGroovy(cypherText);
            //Console.WriteLine(gremlin);

            //TranslationFacade cfog = new TranslationFacade();
            //string gremlin = cfog.toGremlinGroovy("MATCH (cloudAtlas {title: \"Cloud Atlas\"})<-[:DIRECTED]-(directors) RETURN directors.name");
            //Console.WriteLine(gremlin);

            // Console.ReadLine();
        }
    }
}
