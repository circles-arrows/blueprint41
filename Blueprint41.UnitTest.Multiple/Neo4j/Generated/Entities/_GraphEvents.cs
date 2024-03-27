using System;

using Blueprint41;
using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;

namespace Neo4j.Datastore.Manipulation
{
    public static class GraphEvents
    {
        public static class Nodes
        {
            public static class Movie
            {
                public static readonly Entity Entity = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Movie"];

                #region OnNodeLoading

                private static bool onNodeLoadingIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeLoading;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeLoading
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeLoadingIsRegistered)
                            {
                                Entity.Events.OnNodeLoading += onNodeLoadingProxy;
                                onNodeLoadingIsRegistered = true;
                            }
                            onNodeLoading += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeLoading -= value;
                            if (onNodeLoading is null && onNodeLoadingIsRegistered)
                            {
                                Entity.Events.OnNodeLoading -= onNodeLoadingProxy;
                                onNodeLoadingIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeLoadingProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeLoading;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeLoaded

                private static bool onNodeLoadedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeLoaded;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeLoaded
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeLoadedIsRegistered)
                            {
                                Entity.Events.OnNodeLoaded += onNodeLoadedProxy;
                                onNodeLoadedIsRegistered = true;
                            }
                            onNodeLoaded += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeLoaded -= value;
                            if (onNodeLoaded is null && onNodeLoadedIsRegistered)
                            {
                                Entity.Events.OnNodeLoaded -= onNodeLoadedProxy;
                                onNodeLoadedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeLoadedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeLoaded;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnBatchFinished

                private static bool onBatchFinishedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onBatchFinished;
                public static event EventHandler<Entity, NodeEventArgs> OnBatchFinished
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onBatchFinishedIsRegistered)
                            {
                                Entity.Events.OnBatchFinished += onBatchFinishedProxy;
                                onBatchFinishedIsRegistered = true;
                            }
                            onBatchFinished += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onBatchFinished -= value;
                            if (onBatchFinished is null && onBatchFinishedIsRegistered)
                            {
                                Entity.Events.OnBatchFinished -= onBatchFinishedProxy;
                                onBatchFinishedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onBatchFinishedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onBatchFinished;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeCreate

                private static bool onNodeCreateIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeCreate;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeCreate
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeCreateIsRegistered)
                            {
                                Entity.Events.OnNodeCreate += onNodeCreateProxy;
                                onNodeCreateIsRegistered = true;
                            }
                            onNodeCreate += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeCreate -= value;
                            if (onNodeCreate is null && onNodeCreateIsRegistered)
                            {
                                Entity.Events.OnNodeCreate -= onNodeCreateProxy;
                                onNodeCreateIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeCreateProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeCreate;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeCreated

                private static bool onNodeCreatedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeCreated;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeCreated
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeCreatedIsRegistered)
                            {
                                Entity.Events.OnNodeCreated += onNodeCreatedProxy;
                                onNodeCreatedIsRegistered = true;
                            }
                            onNodeCreated += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeCreated -= value;
                            if (onNodeCreated is null && onNodeCreatedIsRegistered)
                            {
                                Entity.Events.OnNodeCreated -= onNodeCreatedProxy;
                                onNodeCreatedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeCreatedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeCreated;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeUpdate

                private static bool onNodeUpdateIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeUpdate;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeUpdate
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeUpdateIsRegistered)
                            {
                                Entity.Events.OnNodeUpdate += onNodeUpdateProxy;
                                onNodeUpdateIsRegistered = true;
                            }
                            onNodeUpdate += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeUpdate -= value;
                            if (onNodeUpdate is null && onNodeUpdateIsRegistered)
                            {
                                Entity.Events.OnNodeUpdate -= onNodeUpdateProxy;
                                onNodeUpdateIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeUpdateProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeUpdate;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeUpdated

                private static bool onNodeUpdatedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeUpdated;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeUpdated
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeUpdatedIsRegistered)
                            {
                                Entity.Events.OnNodeUpdated += onNodeUpdatedProxy;
                                onNodeUpdatedIsRegistered = true;
                            }
                            onNodeUpdated += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeUpdated -= value;
                            if (onNodeUpdated is null && onNodeUpdatedIsRegistered)
                            {
                                Entity.Events.OnNodeUpdated -= onNodeUpdatedProxy;
                                onNodeUpdatedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeUpdatedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeUpdated;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeDelete

                private static bool onNodeDeleteIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeDelete;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeDelete
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeDeleteIsRegistered)
                            {
                                Entity.Events.OnNodeDelete += onNodeDeleteProxy;
                                onNodeDeleteIsRegistered = true;
                            }
                            onNodeDelete += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeDelete -= value;
                            if (onNodeDelete is null && onNodeDeleteIsRegistered)
                            {
                                Entity.Events.OnNodeDelete -= onNodeDeleteProxy;
                                onNodeDeleteIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeDeleteProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeDelete;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeDeleted

                private static bool onNodeDeletedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeDeleted;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeDeleted
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeDeletedIsRegistered)
                            {
                                Entity.Events.OnNodeDeleted += onNodeDeletedProxy;
                                onNodeDeletedIsRegistered = true;
                            }
                            onNodeDeleted += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeDeleted -= value;
                            if (onNodeDeleted is null && onNodeDeletedIsRegistered)
                            {
                                Entity.Events.OnNodeDeleted -= onNodeDeletedProxy;
                                onNodeDeletedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeDeletedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeDeleted;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
            }
            public static class Person
            {
                public static readonly Entity Entity = Blueprint41.UnitTest.Multiple.Neo4j.DataStore.Neo4jModel.Model.Entities["Person"];

                #region OnNodeLoading

                private static bool onNodeLoadingIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeLoading;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeLoading
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeLoadingIsRegistered)
                            {
                                Entity.Events.OnNodeLoading += onNodeLoadingProxy;
                                onNodeLoadingIsRegistered = true;
                            }
                            onNodeLoading += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeLoading -= value;
                            if (onNodeLoading is null && onNodeLoadingIsRegistered)
                            {
                                Entity.Events.OnNodeLoading -= onNodeLoadingProxy;
                                onNodeLoadingIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeLoadingProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeLoading;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeLoaded

                private static bool onNodeLoadedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeLoaded;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeLoaded
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeLoadedIsRegistered)
                            {
                                Entity.Events.OnNodeLoaded += onNodeLoadedProxy;
                                onNodeLoadedIsRegistered = true;
                            }
                            onNodeLoaded += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeLoaded -= value;
                            if (onNodeLoaded is null && onNodeLoadedIsRegistered)
                            {
                                Entity.Events.OnNodeLoaded -= onNodeLoadedProxy;
                                onNodeLoadedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeLoadedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeLoaded;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnBatchFinished

                private static bool onBatchFinishedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onBatchFinished;
                public static event EventHandler<Entity, NodeEventArgs> OnBatchFinished
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onBatchFinishedIsRegistered)
                            {
                                Entity.Events.OnBatchFinished += onBatchFinishedProxy;
                                onBatchFinishedIsRegistered = true;
                            }
                            onBatchFinished += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onBatchFinished -= value;
                            if (onBatchFinished is null && onBatchFinishedIsRegistered)
                            {
                                Entity.Events.OnBatchFinished -= onBatchFinishedProxy;
                                onBatchFinishedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onBatchFinishedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onBatchFinished;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeCreate

                private static bool onNodeCreateIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeCreate;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeCreate
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeCreateIsRegistered)
                            {
                                Entity.Events.OnNodeCreate += onNodeCreateProxy;
                                onNodeCreateIsRegistered = true;
                            }
                            onNodeCreate += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeCreate -= value;
                            if (onNodeCreate is null && onNodeCreateIsRegistered)
                            {
                                Entity.Events.OnNodeCreate -= onNodeCreateProxy;
                                onNodeCreateIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeCreateProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeCreate;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeCreated

                private static bool onNodeCreatedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeCreated;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeCreated
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeCreatedIsRegistered)
                            {
                                Entity.Events.OnNodeCreated += onNodeCreatedProxy;
                                onNodeCreatedIsRegistered = true;
                            }
                            onNodeCreated += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeCreated -= value;
                            if (onNodeCreated is null && onNodeCreatedIsRegistered)
                            {
                                Entity.Events.OnNodeCreated -= onNodeCreatedProxy;
                                onNodeCreatedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeCreatedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeCreated;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeUpdate

                private static bool onNodeUpdateIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeUpdate;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeUpdate
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeUpdateIsRegistered)
                            {
                                Entity.Events.OnNodeUpdate += onNodeUpdateProxy;
                                onNodeUpdateIsRegistered = true;
                            }
                            onNodeUpdate += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeUpdate -= value;
                            if (onNodeUpdate is null && onNodeUpdateIsRegistered)
                            {
                                Entity.Events.OnNodeUpdate -= onNodeUpdateProxy;
                                onNodeUpdateIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeUpdateProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeUpdate;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeUpdated

                private static bool onNodeUpdatedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeUpdated;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeUpdated
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeUpdatedIsRegistered)
                            {
                                Entity.Events.OnNodeUpdated += onNodeUpdatedProxy;
                                onNodeUpdatedIsRegistered = true;
                            }
                            onNodeUpdated += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeUpdated -= value;
                            if (onNodeUpdated is null && onNodeUpdatedIsRegistered)
                            {
                                Entity.Events.OnNodeUpdated -= onNodeUpdatedProxy;
                                onNodeUpdatedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeUpdatedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeUpdated;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeDelete

                private static bool onNodeDeleteIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeDelete;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeDelete
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeDeleteIsRegistered)
                            {
                                Entity.Events.OnNodeDelete += onNodeDeleteProxy;
                                onNodeDeleteIsRegistered = true;
                            }
                            onNodeDelete += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeDelete -= value;
                            if (onNodeDelete is null && onNodeDeleteIsRegistered)
                            {
                                Entity.Events.OnNodeDelete -= onNodeDeleteProxy;
                                onNodeDeleteIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeDeleteProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeDelete;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
                #region OnNodeDeleted

                private static bool onNodeDeletedIsRegistered = false;

                private static event EventHandler<Entity, NodeEventArgs> onNodeDeleted;
                public static event EventHandler<Entity, NodeEventArgs> OnNodeDeleted
                {
                    add
                    {
                        lock (Entity)
                        {
                            if (!onNodeDeletedIsRegistered)
                            {
                                Entity.Events.OnNodeDeleted += onNodeDeletedProxy;
                                onNodeDeletedIsRegistered = true;
                            }
                            onNodeDeleted += value;
                        }
                    }
                    remove
                    {
                        lock (Entity)
                        {
                            onNodeDeleted -= value;
                            if (onNodeDeleted is null && onNodeDeletedIsRegistered)
                            {
                                Entity.Events.OnNodeDeleted -= onNodeDeletedProxy;
                                onNodeDeletedIsRegistered = false;
                            }
                        }
                    }
                }

                private static void onNodeDeletedProxy(object sender, NodeEventArgs args)
                {
                    EventHandler<Entity, NodeEventArgs> handler = onNodeDeleted;
                    if (handler is not null)
                        handler.Invoke((Entity)sender, args);
                }

                #endregion
            }
        }
        public static class Relationships
        {
        }
    }
}