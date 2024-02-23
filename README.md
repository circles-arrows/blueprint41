 
<div>
 <div><img align="left" src="https://github.com/circles-arrows/blueprint41/assets/121929116/88c219d4-403b-4e98-81a6-f1f3055a0a33" alt="Girl in a jacket" width="180" height="180"></div> 
   <div align="right">
     <div align="left"><b> ✔️ Supports for Neo4j and Memgraph</b></div>  
     <div align="left"><b> ✔️ Works Visual Studio, Visual Studio Code and Rider</b></div>   
     <div align="left"><b> ✔️ Develop on Windows, macOS or Linux</b></div>     
   </div>  
</div>  

# Blueprint41

[![NuGet](https://img.shields.io/nuget/v/Blueprint41.svg)](https://www.nuget.org/packages/Blueprint41)
[![downloads](https://img.shields.io/nuget/dt/Blueprint41)](https://www.nuget.org/packages/Blueprint41)
![repo size](https://img.shields.io/github/repo-size/circles-arrows/blueprint41)
![license](https://img.shields.io/github/license/circles-arrows/Blueprint41)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=circles-arrows_blueprint41&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=circles-arrows_blueprint41)

&nbsp;
### .NET ORM for Neo4j Graph Database

Simplify database operations through generated data access objects. 

### Frictionless Development with Intellisense 

![Intellisense](https://raw.githubusercontent.com/circles-arrows/blueprint41/master/Documentation/Blueprint41_Intellisense.gif)

### Neo4j Plugins (Optional)
* Download Blueprint41 plugin for [Neo4j v5](https://github.com/circles-arrows/functionalid/blob/master/blueprint41-5.0.0.jar?raw=true), [Neo4j v4](https://github.com/circles-arrows/functionalid/blob/master/blueprint41-4.0.3.jar?raw=true) or [Neo4j v3](https://github.com/circles-arrows/functionalid/blob/master/blueprint41-1.0.3.jar?raw=true).
* Download APOC plugin [here](https://neo4j-contrib.github.io/neo4j-apoc-procedures/#_download_latest_release).

To learn more, please visit [Extension and Plugins](https://github.com/circles-arrows/blueprint41/wiki/Extension-and-Plugins).

### Documentation

To learn more, please visit [Blueprint41 wiki](https://github.com/circles-arrows/blueprint41/wiki).
### Connection

```csharp
PersistenceProvider.CurrentPersistenceProvider = new Neo4JPersistenceProvider($"bolt://localhost:7687", $"neo4j", $"password");
```

### Automated Deployment of Schema Upgrades

```csharp
// Datastore defines the latest schema definition
Datastore model = new Datastore();

// Sync database schema with the latest upgrade scripts
model.Execute(true);
```

### Type Safe Creation of Nodes and Relationships

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
    
    // Creates relationship via Type-safe generated objects
    movie.Actors.Add(keanu);
    movie.Directors.Add(lana);
    movie.Directors.Add(lilly);
    movie.Genre.Add(Genre.Load(Genre.StaticData.Name.Action));
    movie.Genre.Add(Genre.Load(Genre.StaticData.Name.Sci_Fi));

    // Commits detected changes to database
    Transaction.Commit(); 
}
```


### Type Safe Querying of the Graph

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

