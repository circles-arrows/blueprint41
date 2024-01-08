#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.Linq;
using System.Collections.Generic;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.Query;
using Blueprint41.DatastoreTemplates;
using q = Datastore.Query;

namespace Datastore.Manipulation
{
    /// <summary>
    /// Relationship: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
    /// </summary>
    public partial class MOVIE_CERTIFICATION
    {
        private string _elementId { get; set; }

        /// <summary>
        /// Person (In Node)
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// Restaurant (Out Node)
        /// </summary>
        public Restaurant Restaurant { get; private set; }

        public System.DateTime CreationDate { get; private set; }
        public Blueprint41.UnitTest.DataStore.RatingComponent? FrighteningIntense { get; private set; }
        public Blueprint41.UnitTest.DataStore.RatingComponent? ViolenceGore { get; private set; }
        public Blueprint41.UnitTest.DataStore.RatingComponent? Profanity { get; private set; }
        public Blueprint41.UnitTest.DataStore.RatingComponent? Substances { get; private set; }
        public Blueprint41.UnitTest.DataStore.RatingComponent? SexAndNudity { get; private set; }

        public void Assign(JsNotation<System.DateTime> CreationDate = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> ViolenceGore = default)
        {
            throw new NotImplementedException();
        }
        public static List<MOVIE_CERTIFICATION> Where(Func<MOVIE_CERTIFICATION_ALIAS, QueryCondition> alias)
        {
            throw new NotImplementedException();
        }
        public static List<MOVIE_CERTIFICATION> Where(Func<MOVIE_CERTIFICATION_ALIAS, QueryCondition[]> alias)
        {
            throw new NotImplementedException();
        }
        public static List<MOVIE_CERTIFICATION> Where(JsNotation<System.DateTime> CreationDate = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> ViolenceGore = default, JsNotation<Person> InNode = default, JsNotation<Restaurant> OutNode = default)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Alias for relationship: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
    /// </summary>
    public partial class MOVIE_CERTIFICATION_ALIAS
    {
        internal MOVIE_CERTIFICATION_ALIAS(OGM entity, DirectionEnum direction)
        {
        }
        internal MOVIE_CERTIFICATION_ALIAS(IEnumerable<OGM> entity, DirectionEnum direction)
        {
        }

        public DateTimeResult CreationDate
        {
            get
            {
                if (_creationDate is null)
                    _creationDate = _alias.CreationDate;

                return _creationDate;
            }
        }
        private DateTimeResult _creationDate = null;
        public MiscResult FrighteningIntense
        {
            get
            {
                if (_frighteningIntense is null)
                    _frighteningIntense = _alias.FrighteningIntense;

                return _frighteningIntense;
            }
        }
        private MiscResult _frighteningIntense = null;
        public MiscResult ViolenceGore
        {
            get
            {
                if (_violenceGore is null)
                    _violenceGore = _alias.ViolenceGore;

                return _violenceGore;
            }
        }
        private MiscResult _violenceGore = null;
        public MiscResult Profanity
        {
            get
            {
                if (_profanity is null)
                    _profanity = _alias.Profanity;

                return _profanity;
            }
        }
        private MiscResult _profanity = null;
        public MiscResult Substances
        {
            get
            {
                if (_substances is null)
                    _substances = _alias.Substances;

                return _substances;
            }
        }
        private MiscResult _substances = null;
        public MiscResult SexAndNudity
        {
            get
            {
                if (_sexAndNudity is null)
                    _sexAndNudity = _alias.SexAndNudity;

                return _sexAndNudity;
            }
        }
        private MiscResult _sexAndNudity = null;

        /// <summary>
        /// Movie in-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
        /// </summary>
        /// <returns>
        /// Condition where in-node is the given movie
        /// </returns>
        public QueryCondition Movie(Movie movie)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Movie in-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
        /// </summary>
        /// <returns>
        /// Condition where in-node is in the given set of movies
        /// </returns>
        public QueryCondition Movies(IEnumerable<Movie> movie)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rating out-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
        /// </summary>
        /// <returns>
        /// Condition where out-node is the given rating
        /// </returns>
        public QueryCondition Rating(Rating rating)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Rating out-node: (Movie)-[MOVIE_CERTIFICATION]->(Rating)
        /// </summary>
        /// <returns>
        /// Condition where out-node is in the given set of ratings
        /// </returns>
        public QueryCondition Ratings(IEnumerable<Rating> rating)
        {
            throw new NotImplementedException();
        }

        private static readonly q.MOVIE_CERTIFICATION_ALIAS _alias = new q.MOVIE_CERTIFICATION_ALIAS(new q.MOVIE_CERTIFICATION_REL(null, DirectionEnum.None));
    }

    public static partial class RelationshipAssignmentExtensions
    {
        public static void Assign(this IEnumerable<MOVIE_CERTIFICATION> @this, JsNotation<System.DateTime> CreationDate = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> FrighteningIntense = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Profanity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> SexAndNudity = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> Substances = default, JsNotation<Blueprint41.UnitTest.DataStore.RatingComponent?> ViolenceGore = default)
        {
            throw new NotImplementedException();
        }
    }
}