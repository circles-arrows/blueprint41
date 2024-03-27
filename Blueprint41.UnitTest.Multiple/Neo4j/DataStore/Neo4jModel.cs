namespace Blueprint41.UnitTest.Multiple.Neo4j.DataStore
{
    public class Neo4jModel : DatastoreModel<Neo4jModel>
    {
        protected override void SubscribeEventHandlers()
        {

        }

        [Version(0, 0, 0)]
        public void Script_0_0_0()
        {
            FunctionalIds.Default = FunctionalIds.UUID;

            Entities.New("Movie")
                    .AddProperty("title", typeof(string), IndexType.Unique)
                    .AddProperty("tagline", typeof(string))
                    .AddProperty("released", typeof(int))
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true);

            Entities.New("Person")
                    .AddProperty("name", typeof(string), IndexType.Unique)
                    .AddProperty("born", typeof(int))
                    .AddProperty("Uid", typeof(string), false, IndexType.Unique)
                    .SetKey("Uid", true);


            Relations.New(Entities["Person"], Entities["Movie"], "ACTED_IN", "ACTED_IN")
                     .SetInProperty("ActedMovies", PropertyType.Collection)
                     .SetOutProperty("Actors", PropertyType.Collection)
                     .AddProperty("roles", typeof(string[]), IndexType.Unique);
        }
    }
}
