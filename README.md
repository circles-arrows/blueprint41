# Blueprint41

### .NET ORM for Neo4j Graph Database

Simplify database operations through generated data access objects. 

### Documentation

To learn more, please visit [Blueprint41 wiki](https://github.com/circles-arrows/blueprint41/wiki).
### Connection

```csharp
PersistenceProvider.CurrentPersistenceProvider = new Neo4JPersistenceProvider($"bolt://localhost:7687", $"neo4j", $"password");
```

### Your DataStore Model

```csharp
Datastore model = new Datastore();
model.Execute(true);
```

### Creating Nodes and Relationships

```csharp
using (Transaction.Begin())
{
    Movie matrix = new Movie()
    {
        Title = "The Matrix",
        Tagline = "Welcome to the Real World",
        Released = new DateTime(1999, 3, 31)
    };

    Person keanu = new Person()
    {
        Name = "Keanu Reeves",
        Born = new DateTime(1964, 9, 2)
    };

    Person laurence = new Person()
    {
        Name = "Laurence Fishburne",
        Born = new DateTime(1961, 7, 30)
    };

    Person lana = new Person()
    {
        Name = "Lana Wachowski",
        Born = new DateTime(1961, 7, 30)
    };

    Person lilly = new Person()
    {
        Name = "Lilly Wachowski",
        Born = new DateTime(1967, 12, 29)
    };

    Person joel = new Person()
    {
        Name = "Joel Silver",
        Born = new DateTime(1952, 7, 14)
    };
    
    // Create relationship via Type-safe generated objects
    movie.Actors.Add(keanu);
    movie.Actors.Add(laurence);
    movie.Directors.Add(lana);
    movie.Directors.Add(lilly);
    movie.ScreenWriters.Add(lana);
    movie.ScreenWriters.Add(lilly);
    movie.Producers.Add(joel);
    movie.Genre.Add(Genre.Lookup(Genre.StaticData.Name.Action));

    // Commits detected changes to database
    Transaction.Commit(); 
}
```


### TypeSafe Query 

```csharp
using (Transaction.Begin())
{
    // Get Movies of Keanu(Direct relationship)
    Person keanu = Person.LoadByName("Keanu Reeves");
    EntityCollection<Movie> movies = keanu.ActedIn; // Movies are retrieve here

    // Get Director of Keanu(Spans multiple relationships)
    var query = Transaction.CompiledQuery
    .Match
    (
        Node.Person.Alias(out var director)
            .In.PERSON_DIRECTED_MOVIE.Out.
        Movie
            .Out.PERSON_ACTED_IN_MOVIE.In.
        Person.Alias(out var actor)
    )
    .Where(actor.Name == "Keanu Reeves")
    .Return(director)
    .Compile();

    List<Person> directors = Person.LoadWhere(query);
}
```
