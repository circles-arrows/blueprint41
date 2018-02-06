using Blueprint41;
using Blueprint41.Core;
using Blueprint41.DatastoreTemplates;

namespace Domain.Data.Manipulation
{
	public static class GraphEvents
    {
		public static class Nodes
        {
			public static class Address
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Address"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class AddressType
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["AddressType"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class BillOfMaterials
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["BillOfMaterials"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ContactType
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ContactType"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class CountryRegion
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["CountryRegion"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class CreditCard
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["CreditCard"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Culture
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Culture"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Currency
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Currency"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class CurrencyRate
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["CurrencyRate"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Customer
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Customer"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Department
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Department"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Document
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Document"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class EmailAddress
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["EmailAddress"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Employee
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Employee"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class EmployeeDepartmentHistory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["EmployeeDepartmentHistory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class EmployeePayHistory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["EmployeePayHistory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Illustration
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Illustration"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class JobCandidate
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["JobCandidate"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Location
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Location"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Password
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Password"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Person
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Person"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class PhoneNumberType
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["PhoneNumberType"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Product
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Product"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductCategory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductCategory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductCostHistory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductCostHistory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductDescription
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductDescription"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductInventory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductInventory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductListPriceHistory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductListPriceHistory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductModel
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductModel"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductPhoto
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductPhoto"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductProductPhoto
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductProductPhoto"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductReview
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductReview"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductSubcategory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductSubcategory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ProductVendor
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ProductVendor"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class PurchaseOrderDetail
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["PurchaseOrderDetail"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class PurchaseOrderHeader
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["PurchaseOrderHeader"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesOrderDetail
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesOrderDetail"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesOrderHeader
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesOrderHeader"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesPerson
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesPerson"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesPersonQuotaHistory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesPersonQuotaHistory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesReason
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesReason"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesTaxRate
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesTaxRate"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesTerritory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesTerritory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SalesTerritoryHistory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SalesTerritoryHistory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ScrapReason
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ScrapReason"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Shift
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Shift"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ShipMethod
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ShipMethod"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class ShoppingCartItem
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["ShoppingCartItem"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class SpecialOffer
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["SpecialOffer"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class StateProvince
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["StateProvince"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Store
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Store"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class TransactionHistory
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["TransactionHistory"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class TransactionHistoryArchive
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["TransactionHistoryArchive"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class UnitMeasure
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["UnitMeasure"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class Vendor
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["Vendor"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class WorkOrder
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["WorkOrder"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
			public static class WorkOrderRouting
            {
                private static readonly Entity Entity = Datastore.AdventureWorks.Model.Entities["WorkOrderRouting"];

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
							if (onNodeLoading == null && onNodeLoadingIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeLoaded == null && onNodeLoadedIsRegistered)
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
					if ((object)handler != null)
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
							if (onBatchFinished == null && onBatchFinishedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreate == null && onNodeCreateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeCreated == null && onNodeCreatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdate == null && onNodeUpdateIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeUpdated == null && onNodeUpdatedIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDelete == null && onNodeDeleteIsRegistered)
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
					if ((object)handler != null)
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
							if (onNodeDeleted == null && onNodeDeletedIsRegistered)
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
					if ((object)handler != null)
						handler.Invoke((Entity)sender, args);
				}

				#endregion
            }
		}
		public static class Relationships
        {
			public static class ADDRESS_HAS_ADDRESSTYPE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["ADDRESS_HAS_ADDRESSTYPE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class ADDRESS_HAS_STATEPROVINCE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["ADDRESS_HAS_STATEPROVINCE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class BILLOFMATERIALS_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["BILLOFMATERIALS_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class BILLOFMATERIALS_HAS_UNITMEASURE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["BILLOFMATERIALS_HAS_UNITMEASURE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class CURRENCYRATE_HAS_CURRENCY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["CURRENCYRATE_HAS_CURRENCY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class CUSTOMER_HAS_PERSON
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["CUSTOMER_HAS_PERSON"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class CUSTOMER_HAS_SALESTERRITORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["CUSTOMER_HAS_SALESTERRITORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class CUSTOMER_HAS_STORE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["CUSTOMER_HAS_STORE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class DEPARTMENT_CONTAINS_EMPLOYEE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["DEPARTMENT_CONTAINS_EMPLOYEE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class EMPLOYEE_BECOMES_SALESPERSON
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["EMPLOYEE_BECOMES_SALESPERSON"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["EMPLOYEE_HAS_EMPLOYEEDEPARTMENTHISTORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class EMPLOYEE_HAS_EMPLOYEEPAYHISTORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["EMPLOYEE_HAS_EMPLOYEEPAYHISTORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class EMPLOYEE_HAS_SHIFT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["EMPLOYEE_HAS_SHIFT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class EMPLOYEE_IS_JOBCANDIDATE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["EMPLOYEE_IS_JOBCANDIDATE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["EMPLOYEEDEPARTMENTHISTORY_HAS_SHIFT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["EMPLOYEEDEPARTMENTHISTORY_VALID_FOR_DEPARTMENT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_BECOMES_EMPLOYEE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_BECOMES_EMPLOYEE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_HAS_ADDRESS
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_HAS_ADDRESS"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_HAS_CONTACTTYPE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_HAS_CONTACTTYPE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_HAS_EMAILADDRESS
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_HAS_EMAILADDRESS"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_HAS_PASSWORD
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_HAS_PASSWORD"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_HAS_PHONENUMBERTYPE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_HAS_PHONENUMBERTYPE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_VALID_FOR_CREDITCARD
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_VALID_FOR_CREDITCARD"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PERSON_VALID_FOR_DOCUMENT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PERSON_VALID_FOR_DOCUMENT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCT_HAS_DOCUMENT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCT_HAS_DOCUMENT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCT_HAS_PRODUCTMODEL
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCT_HAS_PRODUCTMODEL"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCT_HAS_PRODUCTPRODUCTPHOTO
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCT_HAS_PRODUCTPRODUCTPHOTO"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCT_HAS_TRANSACTIONHISTORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCT_HAS_TRANSACTIONHISTORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCT_IN_PRODUCTSUBCATEGORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCT_IN_PRODUCTSUBCATEGORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCT_VALID_FOR_PRODUCTREVIEW
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCT_VALID_FOR_PRODUCTREVIEW"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTCOSTHISTORY_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTCOSTHISTORY_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTINVENTORY_HAS_LOCATION
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTINVENTORY_HAS_LOCATION"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTINVENTORY_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTINVENTORY_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTLISTPRICEHISTORY_VALID_FOR_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTMODEL_HAS_CULTURE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTMODEL_HAS_CULTURE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTMODEL_HAS_ILLUSTRATION
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTMODEL_HAS_ILLUSTRATION"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTMODEL_HAS_PRODUCTDESCRIPTION
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTMODEL_HAS_PRODUCTDESCRIPTION"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTPRODUCTPHOTO_HAS_PRODUCTPHOTO"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTSUBCATEGORY_IN_PRODUCTCATEGORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTVENDOR_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTVENDOR_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PRODUCTVENDOR_HAS_UNITMEASURE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PRODUCTVENDOR_HAS_UNITMEASURE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PURCHASEORDERDETAIL_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PURCHASEORDERDETAIL_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PURCHASEORDERDETAIL_HAS_PURCHASEORDERHEADER"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PURCHASEORDERHEADER_HAS_SHIPMETHOD
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PURCHASEORDERHEADER_HAS_SHIPMETHOD"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class PURCHASEORDERHEADER_HAS_VENDOR
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["PURCHASEORDERHEADER_HAS_VENDOR"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERDETAIL_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERDETAIL_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERDETAIL_HAS_SALESORDERHEADER
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERDETAIL_HAS_SALESORDERHEADER"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERDETAIL_HAS_SPECIALOFFER
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERDETAIL_HAS_SPECIALOFFER"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERHEADER_CONTAINS_SALESTERRITORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERHEADER_CONTAINS_SALESTERRITORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERHEADER_HAS_ADDRESS
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERHEADER_HAS_ADDRESS"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERHEADER_HAS_CREDITCARD
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERHEADER_HAS_CREDITCARD"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERHEADER_HAS_CURRENCYRATE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERHEADER_HAS_CURRENCYRATE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERHEADER_HAS_SALESREASON
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERHEADER_HAS_SALESREASON"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESORDERHEADER_HAS_SHIPMETHOD
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESORDERHEADER_HAS_SHIPMETHOD"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESPERSON_HAS_SALESPERSONQUOTAHISTORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESPERSON_HAS_SALESPERSONQUOTAHISTORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESPERSON_HAS_SALESTERRITORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESPERSON_HAS_SALESTERRITORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESPERSON_IS_PERSON
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESPERSON_IS_PERSON"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESTAXRATE_HAS_STATEPROVINCE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESTAXRATE_HAS_STATEPROVINCE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SALESTERRITORY_HAS_SALESTERRITORYHISTORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SALESTERRITORY_HAS_SALESTERRITORYHISTORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class SHOPPINGCARTITEM_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["SHOPPINGCARTITEM_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class STATEPROVINCE_HAS_COUNTRYREGION
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["STATEPROVINCE_HAS_COUNTRYREGION"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class STATEPROVINCE_HAS_SALESTERRITORY
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["STATEPROVINCE_HAS_SALESTERRITORY"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class STORE_HAS_ADDRESS
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["STORE_HAS_ADDRESS"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class STORE_VALID_FOR_SALESPERSON
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["STORE_VALID_FOR_SALESPERSON"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["TRANSACTIONHISTORYARCHIVE_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class VENDOR_BECOMES_PRODUCTVENDOR
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["VENDOR_BECOMES_PRODUCTVENDOR"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class VENDOR_VALID_FOR_EMPLOYEE
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["VENDOR_VALID_FOR_EMPLOYEE"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class WORKORDER_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["WORKORDER_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class WORKORDER_HAS_SCRAPREASON
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["WORKORDER_HAS_SCRAPREASON"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class WORKORDERROUTING_HAS_LOCATION
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["WORKORDERROUTING_HAS_LOCATION"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class WORKORDERROUTING_HAS_PRODUCT
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["WORKORDERROUTING_HAS_PRODUCT"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
			public static class WORKORDERROUTING_HAS_WORKORDER
            {
				private static readonly Relationship Relationship = Datastore.AdventureWorks.Model.Relations["WORKORDERROUTING_HAS_WORKORDER"];

				#region OnRelationCreate

				private static bool onRelationCreateIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreate;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreate
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate += onRelationCreateProxy;
								onRelationCreateIsRegistered = true;
							}
							onRelationCreate += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreate -= value;
							if (onRelationCreate == null && onRelationCreateIsRegistered)
							{
								Relationship.Events.OnRelationCreate -= onRelationCreateProxy;
								onRelationCreateIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreateProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreate;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationCreated

				private static bool onRelationCreatedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationCreated;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationCreated
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated += onRelationCreatedProxy;
								onRelationCreatedIsRegistered = true;
							}
							onRelationCreated += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationCreated -= value;
							if (onRelationCreated == null && onRelationCreatedIsRegistered)
							{
								Relationship.Events.OnRelationCreated -= onRelationCreatedProxy;
								onRelationCreatedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationCreatedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationCreated;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDelete

				private static bool onRelationDeleteIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDelete;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDelete
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete += onRelationDeleteProxy;
								onRelationDeleteIsRegistered = true;
							}
							onRelationDelete += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDelete -= value;
							if (onRelationDelete == null && onRelationDeleteIsRegistered)
							{
								Relationship.Events.OnRelationDelete -= onRelationDeleteProxy;
								onRelationDeleteIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeleteProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDelete;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
				#region OnRelationDeleted

				private static bool onRelationDeletedIsRegistered = false;

				private static event EventHandler<Relationship, RelationshipEventArgs> onRelationDeleted;
				public static event EventHandler<Relationship, RelationshipEventArgs> OnRelationDeleted
				{
					add
					{
						lock (Relationship)
						{
							if (!onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted += onRelationDeletedProxy;
								onRelationDeletedIsRegistered = true;
							}
							onRelationDeleted += value;
						}
					}
					remove
					{
						lock (Relationship)
						{
							onRelationDeleted -= value;
							if (onRelationDeleted == null && onRelationDeletedIsRegistered)
							{
								Relationship.Events.OnRelationDeleted -= onRelationDeletedProxy;
								onRelationDeletedIsRegistered = false;
							}
						}
					}
				}

				private static void onRelationDeletedProxy(object sender, RelationshipEventArgs args)
				{
					EventHandler<Relationship, RelationshipEventArgs> handler = onRelationDeleted;
					if ((object)handler != null)
						handler.Invoke((Relationship)sender, args);
				}

				#endregion
            }
        }
	}
}