using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Schemas
{
    public abstract class Initializable
	{
        public virtual Modeller Model { get; internal set; }

		protected virtual void InitializeLogic()
		{
		}
		protected virtual void InitializeView()
		{
		}
	}

    public partial class Modeller
    {
        public Modeller(string filename) : this(null, modeller.Load(filename))
		{
			Model = this;
		}
    }

	public partial class Modeller : Initializable
	{
		public modeller Xml { get; internal set; }

		public Modeller() : this(null, new modeller())
		{
		}
		public Modeller(Modeller model) : this(model, new modeller())
		{
		}
        internal Modeller(Modeller model, modeller xml)
        {
			Xml = xml;
			Model = this;

            if (xml.@entities == null)
                xml.@entities = new modeller.entitiesLocalType();
			m_Entities = new EntitiesLocalType(Model, xml.@entities);

            if (xml.@relationships == null)
                xml.@relationships = new modeller.relationshipsLocalType();
			m_Relationships = new RelationshipsLocalType(Model, xml.@relationships);

            if (xml.@submodels == null)
                xml.@submodels = new modeller.submodelsLocalType();
			m_Submodels = new SubmodelsLocalType(Model, xml.@submodels);

            if (xml.@functionalIds == null)
                xml.@functionalIds = new modeller.functionalIdsLocalType();
			m_FunctionalIds = new FunctionalIdsLocalType(Model, xml.@functionalIds);

			InitializeView();
			InitializeLogic();
        }
		public EntitiesLocalType Entities
		{
			get { return m_Entities; }
			set
            {
				if (m_Entities == value)
                    return;

				if (m_Entities != null)
                    m_Entities.Model = null;

                PropertyChangedEventArgs<EntitiesLocalType> eventArgs = new PropertyChangedEventArgs<EntitiesLocalType>(m_Entities, value);
                m_Entities = value;

				if (m_Entities != null)
                    m_Entities.Model = Model;

                if (OnEntitiesChanged != null)
                    OnEntitiesChanged(this, eventArgs);
            }
		}
		private EntitiesLocalType m_Entities = null;
		public event EventHandler<PropertyChangedEventArgs<EntitiesLocalType>> OnEntitiesChanged;

		public RelationshipsLocalType Relationships
		{
			get { return m_Relationships; }
			set
            {
				if (m_Relationships == value)
                    return;

				if (m_Relationships != null)
                    m_Relationships.Model = null;

                PropertyChangedEventArgs<RelationshipsLocalType> eventArgs = new PropertyChangedEventArgs<RelationshipsLocalType>(m_Relationships, value);
                m_Relationships = value;

				if (m_Relationships != null)
                    m_Relationships.Model = Model;

                if (OnRelationshipsChanged != null)
                    OnRelationshipsChanged(this, eventArgs);
            }
		}
		private RelationshipsLocalType m_Relationships = null;
		public event EventHandler<PropertyChangedEventArgs<RelationshipsLocalType>> OnRelationshipsChanged;

		public SubmodelsLocalType Submodels
		{
			get { return m_Submodels; }
			set
            {
				if (m_Submodels == value)
                    return;

				if (m_Submodels != null)
                    m_Submodels.Model = null;

                PropertyChangedEventArgs<SubmodelsLocalType> eventArgs = new PropertyChangedEventArgs<SubmodelsLocalType>(m_Submodels, value);
                m_Submodels = value;

				if (m_Submodels != null)
                    m_Submodels.Model = Model;

                if (OnSubmodelsChanged != null)
                    OnSubmodelsChanged(this, eventArgs);
            }
		}
		private SubmodelsLocalType m_Submodels = null;
		public event EventHandler<PropertyChangedEventArgs<SubmodelsLocalType>> OnSubmodelsChanged;

		public FunctionalIdsLocalType FunctionalIds
		{
			get { return m_FunctionalIds; }
			set
            {
				if (m_FunctionalIds == value)
                    return;

				if (m_FunctionalIds != null)
                    m_FunctionalIds.Model = null;

                PropertyChangedEventArgs<FunctionalIdsLocalType> eventArgs = new PropertyChangedEventArgs<FunctionalIdsLocalType>(m_FunctionalIds, value);
                m_FunctionalIds = value;

				if (m_FunctionalIds != null)
                    m_FunctionalIds.Model = Model;

                if (OnFunctionalIdsChanged != null)
                    OnFunctionalIdsChanged(this, eventArgs);
            }
		}
		private FunctionalIdsLocalType m_FunctionalIds = null;
		public event EventHandler<PropertyChangedEventArgs<FunctionalIdsLocalType>> OnFunctionalIdsChanged;

		public String Type
		{
			get { return Xml.@type; }
			set
            {
				if (Xml.@type == value)
                    return;

				String oldValue = Xml.@type;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnTypeChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@type = oldValue;
					OnTypeChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@type = value;
	                OnTypeChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanged;

	}
	public partial class Modeller
	{
		public partial class EntitiesLocalType : Initializable
		{
			public modeller.entitiesLocalType Xml { get; internal set; }
	
			public EntitiesLocalType() : this(null, new modeller.entitiesLocalType())
			{
			}
			public EntitiesLocalType(Modeller model) : this(model, new modeller.entitiesLocalType())
			{
			}
	        internal EntitiesLocalType(Modeller model, modeller.entitiesLocalType xml)
	        {
				Xml = xml;
				Model = model;
	
	            if (xml.@entity == null)
	                xml.@entity = new List<entity>(); //normal IList`1
	
				InitializeView();
				InitializeLogic();
	        }
			public Collection<Entity> Entity
			{
				get
				{
					if (m_Entity == null)
					{
						m_Entity = new ObservableCollection<Entity>(Xml.@entity.Select(item => new Entity(Model, item)));
						m_Entity.CollectionChanged += RaiseOnEntityCollectionChanged;
						m_Entity.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
	                    {
	                        switch (e.Action)
	                        {
	                            case NotifyCollectionChangedAction.Add:
	                                {
	                                    int index = e.NewStartingIndex;
	                                    foreach (Entity item in e.NewItems)
										{
											item.Model = Model;
	                                        Xml.@entity.Insert(index++, item.Xml);
										}
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Remove:
	                                {
										int oldItemIndex = 0;
	                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
	                                    {
	                                        Entity item = (Entity)e.OldItems[oldItemIndex++];
	                                        if (Xml.@entity[index] != item.Xml)
	                                            throw new InvalidOperationException();
	
											item.Model = null;
	                                        Xml.@entity.RemoveAt(index);
	                                    }
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Reset:
	                                {
										if(e.OldItems != null)
										{									
											foreach (Entity item in e.OldItems)
												item.Model = null;
										}
	                                    Xml.@entity.Clear();
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Replace:
	                            case NotifyCollectionChangedAction.Move:
	                                throw new NotSupportedException();
	                        }
	                    };
					}
					return m_Entity; 
				}
			}
			private ObservableCollection<Entity> m_Entity = null;
			private event EventHandler<NotifyCollectionChangedEventArgs> EntityCollectionChanged;
			private void RaiseOnEntityCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
			{
				if (EntityCollectionChanged != null)
					EntityCollectionChanged(sender, e);
			}
	
		}
	}
	public partial class Modeller
	{
		public partial class RelationshipsLocalType : Initializable
		{
			public modeller.relationshipsLocalType Xml { get; internal set; }
	
			public RelationshipsLocalType() : this(null, new modeller.relationshipsLocalType())
			{
			}
			public RelationshipsLocalType(Modeller model) : this(model, new modeller.relationshipsLocalType())
			{
			}
	        internal RelationshipsLocalType(Modeller model, modeller.relationshipsLocalType xml)
	        {
				Xml = xml;
				Model = model;
	
	            if (xml.@relationship == null)
	                xml.@relationship = new List<relationship>(); //normal IList`1
	
				InitializeView();
				InitializeLogic();
	        }
			public Collection<Relationship> Relationship
			{
				get
				{
					if (m_Relationship == null)
					{
						m_Relationship = new ObservableCollection<Relationship>(Xml.@relationship.Select(item => new Relationship(Model, item)));
						m_Relationship.CollectionChanged += RaiseOnRelationshipCollectionChanged;
						m_Relationship.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
	                    {
	                        switch (e.Action)
	                        {
	                            case NotifyCollectionChangedAction.Add:
	                                {
	                                    int index = e.NewStartingIndex;
	                                    foreach (Relationship item in e.NewItems)
										{
											item.Model = Model;
	                                        Xml.@relationship.Insert(index++, item.Xml);
										}
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Remove:
	                                {
										int oldItemIndex = 0;
	                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
	                                    {
	                                        Relationship item = (Relationship)e.OldItems[oldItemIndex++];
	                                        if (Xml.@relationship[index] != item.Xml)
	                                            throw new InvalidOperationException();
	
											item.Model = null;
	                                        Xml.@relationship.RemoveAt(index);
	                                    }
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Reset:
	                                {
										if(e.OldItems != null)
										{									
											foreach (Relationship item in e.OldItems)
												item.Model = null;
										}
	                                    Xml.@relationship.Clear();
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Replace:
	                            case NotifyCollectionChangedAction.Move:
	                                throw new NotSupportedException();
	                        }
	                    };
					}
					return m_Relationship; 
				}
			}
			private ObservableCollection<Relationship> m_Relationship = null;
			private event EventHandler<NotifyCollectionChangedEventArgs> RelationshipCollectionChanged;
			private void RaiseOnRelationshipCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
			{
				if (RelationshipCollectionChanged != null)
					RelationshipCollectionChanged(sender, e);
			}
	
		}
	}
	public partial class Modeller
	{
		public partial class SubmodelsLocalType : Initializable
		{
			public modeller.submodelsLocalType Xml { get; internal set; }
	
			public SubmodelsLocalType() : this(null, new modeller.submodelsLocalType())
			{
			}
			public SubmodelsLocalType(Modeller model) : this(model, new modeller.submodelsLocalType())
			{
			}
	        internal SubmodelsLocalType(Modeller model, modeller.submodelsLocalType xml)
	        {
				Xml = xml;
				Model = model;
	
	            if (xml.@submodel == null)
	                xml.@submodel = new List<submodel>(); //normal IList`1
	
				InitializeView();
				InitializeLogic();
	        }
			public Collection<Submodel> Submodel
			{
				get
				{
					if (m_Submodel == null)
					{
						m_Submodel = new ObservableCollection<Submodel>(Xml.@submodel.Select(item => new Submodel(Model, item)));
						m_Submodel.CollectionChanged += RaiseOnSubmodelCollectionChanged;
						m_Submodel.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
	                    {
	                        switch (e.Action)
	                        {
	                            case NotifyCollectionChangedAction.Add:
	                                {
	                                    int index = e.NewStartingIndex;
	                                    foreach (Submodel item in e.NewItems)
										{
											item.Model = Model;
	                                        Xml.@submodel.Insert(index++, item.Xml);
										}
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Remove:
	                                {
										int oldItemIndex = 0;
	                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
	                                    {
	                                        Submodel item = (Submodel)e.OldItems[oldItemIndex++];
	                                        if (Xml.@submodel[index] != item.Xml)
	                                            throw new InvalidOperationException();
	
											item.Model = null;
	                                        Xml.@submodel.RemoveAt(index);
	                                    }
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Reset:
	                                {
										if(e.OldItems != null)
										{									
											foreach (Submodel item in e.OldItems)
												item.Model = null;
										}
	                                    Xml.@submodel.Clear();
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Replace:
	                            case NotifyCollectionChangedAction.Move:
	                                throw new NotSupportedException();
	                        }
	                    };
					}
					return m_Submodel; 
				}
			}
			private ObservableCollection<Submodel> m_Submodel = null;
			private event EventHandler<NotifyCollectionChangedEventArgs> SubmodelCollectionChanged;
			private void RaiseOnSubmodelCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
			{
				if (SubmodelCollectionChanged != null)
					SubmodelCollectionChanged(sender, e);
			}
	
		}
	}
	public partial class Modeller
	{
		public partial class FunctionalIdsLocalType : Initializable
		{
			public modeller.functionalIdsLocalType Xml { get; internal set; }
	
			public FunctionalIdsLocalType() : this(null, new modeller.functionalIdsLocalType())
			{
			}
			public FunctionalIdsLocalType(Modeller model) : this(model, new modeller.functionalIdsLocalType())
			{
			}
	        internal FunctionalIdsLocalType(Modeller model, modeller.functionalIdsLocalType xml)
	        {
				Xml = xml;
				Model = model;
	
	            if (xml.@functionalId == null)
	                xml.@functionalId = new List<functionalId>(); //normal IList`1
	
				InitializeView();
				InitializeLogic();
	        }
			public Collection<FunctionalId> FunctionalId
			{
				get
				{
					if (m_FunctionalId == null)
					{
						m_FunctionalId = new ObservableCollection<FunctionalId>(Xml.@functionalId.Select(item => new FunctionalId(Model, item)));
						m_FunctionalId.CollectionChanged += RaiseOnFunctionalIdCollectionChanged;
						m_FunctionalId.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
	                    {
	                        switch (e.Action)
	                        {
	                            case NotifyCollectionChangedAction.Add:
	                                {
	                                    int index = e.NewStartingIndex;
	                                    foreach (FunctionalId item in e.NewItems)
										{
											item.Model = Model;
	                                        Xml.@functionalId.Insert(index++, item.Xml);
										}
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Remove:
	                                {
										int oldItemIndex = 0;
	                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
	                                    {
	                                        FunctionalId item = (FunctionalId)e.OldItems[oldItemIndex++];
	                                        if (Xml.@functionalId[index] != item.Xml)
	                                            throw new InvalidOperationException();
	
											item.Model = null;
	                                        Xml.@functionalId.RemoveAt(index);
	                                    }
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Reset:
	                                {
										if(e.OldItems != null)
										{									
											foreach (FunctionalId item in e.OldItems)
												item.Model = null;
										}
	                                    Xml.@functionalId.Clear();
	                                }
	                                break;
	                            case NotifyCollectionChangedAction.Replace:
	                            case NotifyCollectionChangedAction.Move:
	                                throw new NotSupportedException();
	                        }
	                    };
					}
					return m_FunctionalId; 
				}
			}
			private ObservableCollection<FunctionalId> m_FunctionalId = null;
			private event EventHandler<NotifyCollectionChangedEventArgs> FunctionalIdCollectionChanged;
			private void RaiseOnFunctionalIdCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
			{
				if (FunctionalIdCollectionChanged != null)
					FunctionalIdCollectionChanged(sender, e);
			}
	
		}
	}
	public partial class Entity : Initializable
	{
		public entity Xml { get; internal set; }

		public Entity() : this(null, new entity())
		{
		}
		public Entity(Modeller model) : this(model, new entity())
		{
		}
        internal Entity(Modeller model, entity xml)
        {
			Xml = xml;
			Model = model;

            if (xml.@primitive == null)
                xml.@primitive = new List<primitive>(); //normal IList`1

            if (xml.@staticData == null)
                xml.@staticData = new staticData();
			m_StaticData = new StaticData(Model, xml.@staticData);

			InitializeView();
			InitializeLogic();
        }
		public Collection<Primitive> Primitive
		{
			get
			{
				if (m_Primitive == null)
				{
					m_Primitive = new ObservableCollection<Primitive>(Xml.@primitive.Select(item => new Primitive(Model, item)));
					m_Primitive.CollectionChanged += RaiseOnPrimitiveCollectionChanged;
					m_Primitive.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                {
                                    int index = e.NewStartingIndex;
                                    foreach (Primitive item in e.NewItems)
									{
										item.Model = Model;
                                        Xml.@primitive.Insert(index++, item.Xml);
									}
                                }
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                {
									int oldItemIndex = 0;
                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
                                    {
                                        Primitive item = (Primitive)e.OldItems[oldItemIndex++];
                                        if (Xml.@primitive[index] != item.Xml)
                                            throw new InvalidOperationException();

										item.Model = null;
                                        Xml.@primitive.RemoveAt(index);
                                    }
                                }
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                {
									if(e.OldItems != null)
									{									
										foreach (Primitive item in e.OldItems)
											item.Model = null;
									}
                                    Xml.@primitive.Clear();
                                }
                                break;
                            case NotifyCollectionChangedAction.Replace:
                            case NotifyCollectionChangedAction.Move:
                                throw new NotSupportedException();
                        }
                    };
				}
				return m_Primitive; 
			}
		}
		private ObservableCollection<Primitive> m_Primitive = null;
		private event EventHandler<NotifyCollectionChangedEventArgs> PrimitiveCollectionChanged;
		private void RaiseOnPrimitiveCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (PrimitiveCollectionChanged != null)
				PrimitiveCollectionChanged(sender, e);
		}

		public StaticData StaticData
		{
			get { return m_StaticData; }
			set
            {
				if (m_StaticData == value)
                    return;

				if (m_StaticData != null)
                    m_StaticData.Model = null;

                PropertyChangedEventArgs<StaticData> eventArgs = new PropertyChangedEventArgs<StaticData>(m_StaticData, value);
                m_StaticData = value;

				if (m_StaticData != null)
                    m_StaticData.Model = Model;

                if (OnStaticDataChanged != null)
                    OnStaticDataChanged(this, eventArgs);
            }
		}
		private StaticData m_StaticData = null;
		public event EventHandler<PropertyChangedEventArgs<StaticData>> OnStaticDataChanged;

		public String Name
		{
			get { return Xml.@name; }
			set
            {
				if (Xml.@name == value)
                    return;

				String oldValue = Xml.@name;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnNameChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@name = oldValue;
					OnNameChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@name = value;
	                OnNameChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanged;

		public String Label
		{
			get { return Xml.@label; }
			set
            {
				if (Xml.@label == value)
                    return;

				String oldValue = Xml.@label;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnLabelChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@label = oldValue;
					OnLabelChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@label = value;
	                OnLabelChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChanged;

		public Boolean Abstract
		{
			get { return Xml.@abstract; }
			set
            {
				if (Xml.@abstract == value)
                    return;

				Boolean oldValue = Xml.@abstract;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnAbstractChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@abstract = oldValue;
					OnAbstractChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@abstract = value;
	                OnAbstractChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnAbstractChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnAbstractChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnAbstractChanged;

		public Boolean Virtual
		{
			get { return Xml.@virtual; }
			set
            {
				if (Xml.@virtual == value)
                    return;

				Boolean oldValue = Xml.@virtual;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnVirtualChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@virtual = oldValue;
					OnVirtualChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@virtual = value;
	                OnVirtualChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnVirtualChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnVirtualChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnVirtualChanged;

		public String Summary
		{
			get { return Xml.@summary; }
			set
            {
				if (Xml.@summary == value)
                    return;

				String oldValue = Xml.@summary;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnSummaryChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@summary = oldValue;
					OnSummaryChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@summary = value;
	                OnSummaryChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnSummaryChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnSummaryChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnSummaryChanged;

		public String Example
		{
			get { return Xml.@example; }
			set
            {
				if (Xml.@example == value)
                    return;

				String oldValue = Xml.@example;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnExampleChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@example = oldValue;
					OnExampleChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@example = value;
	                OnExampleChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnExampleChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnExampleChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnExampleChanged;

		public String Inherits
		{
			get { return Xml.@inherits; }
			set
            {
				if (Xml.@inherits == value)
                    return;

				String oldValue = Xml.@inherits;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnInheritsChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@inherits = oldValue;
					OnInheritsChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@inherits = value;
	                OnInheritsChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnInheritsChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnInheritsChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnInheritsChanged;

		public String Prefix
		{
			get { return Xml.@prefix; }
			set
            {
				if (Xml.@prefix == value)
                    return;

				String oldValue = Xml.@prefix;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnPrefixChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@prefix = oldValue;
					OnPrefixChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@prefix = value;
	                OnPrefixChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnPrefixChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnPrefixChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnPrefixChanged;

		public Boolean IsStaticData
		{
			get { return Xml.@isStaticData; }
			set
            {
				if (Xml.@isStaticData == value)
                    return;

				Boolean oldValue = Xml.@isStaticData;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnIsStaticDataChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@isStaticData = oldValue;
					OnIsStaticDataChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@isStaticData = value;
	                OnIsStaticDataChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsStaticDataChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsStaticDataChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsStaticDataChanged;

		public String FunctionalId
		{
			get { return Xml.@functionalId; }
			set
            {
				if (Xml.@functionalId == value)
                    return;

				String oldValue = Xml.@functionalId;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnFunctionalIdChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@functionalId = oldValue;
					OnFunctionalIdChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@functionalId = value;
	                OnFunctionalIdChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnFunctionalIdChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnFunctionalIdChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnFunctionalIdChanged;

		public String Guid
		{
			get { return Xml.@guid; }
			set
            {
				if (Xml.@guid == value)
                    return;

				String oldValue = Xml.@guid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@guid = oldValue;
					OnGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@guid = value;
	                OnGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanged;

		public String MappingGuid
		{
			get { return Xml.@mappingGuid; }
			set
            {
				if (Xml.@mappingGuid == value)
                    return;

				String oldValue = Xml.@mappingGuid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnMappingGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@mappingGuid = oldValue;
					OnMappingGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@mappingGuid = value;
	                OnMappingGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanged;

	}
	public partial class Primitive : Initializable
	{
		public primitive Xml { get; internal set; }

		public Primitive() : this(null, new primitive())
		{
		}
		public Primitive(Modeller model) : this(model, new primitive())
		{
		}
        internal Primitive(Modeller model, primitive xml)
        {
			Xml = xml;
			Model = model;

			InitializeView();
			InitializeLogic();
        }
		public String Name
		{
			get { return Xml.@name; }
			set
            {
				if (Xml.@name == value)
                    return;

				String oldValue = Xml.@name;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnNameChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@name = oldValue;
					OnNameChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@name = value;
	                OnNameChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanged;

		public Boolean IsKey
		{
			get { return Xml.@isKey; }
			set
            {
				if (Xml.@isKey == value)
                    return;

				Boolean oldValue = Xml.@isKey;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnIsKeyChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@isKey = oldValue;
					OnIsKeyChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@isKey = value;
	                OnIsKeyChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsKeyChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsKeyChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsKeyChanged;

		public String Type
		{
			get { return Xml.@type; }
			set
            {
				if (Xml.@type == value)
                    return;

				String oldValue = Xml.@type;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnTypeChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@type = oldValue;
					OnTypeChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@type = value;
	                OnTypeChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanged;

		public Boolean Nullable
		{
			get { return Xml.@nullable; }
			set
            {
				if (Xml.@nullable == value)
                    return;

				Boolean oldValue = Xml.@nullable;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnNullableChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@nullable = oldValue;
					OnNullableChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@nullable = value;
	                OnNullableChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnNullableChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnNullableChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnNullableChanged;

		public Boolean IsFullTextProperty
		{
			get { return Xml.@isFullTextProperty; }
			set
            {
				if (Xml.@isFullTextProperty == value)
                    return;

				Boolean oldValue = Xml.@isFullTextProperty;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnIsFullTextPropertyChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@isFullTextProperty = oldValue;
					OnIsFullTextPropertyChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@isFullTextProperty = value;
	                OnIsFullTextPropertyChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsFullTextPropertyChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsFullTextPropertyChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsFullTextPropertyChanged;

		public String Guid
		{
			get { return Xml.@guid; }
			set
            {
				if (Xml.@guid == value)
                    return;

				String oldValue = Xml.@guid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@guid = oldValue;
					OnGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@guid = value;
	                OnGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanged;

		public String MappingGuid
		{
			get { return Xml.@mappingGuid; }
			set
            {
				if (Xml.@mappingGuid == value)
                    return;

				String oldValue = Xml.@mappingGuid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnMappingGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@mappingGuid = oldValue;
					OnMappingGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@mappingGuid = value;
	                OnMappingGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanged;

		public String Index
		{
			get { return Xml.@index; }
			set
            {
				if (Xml.@index == value)
                    return;

				String oldValue = Xml.@index;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnIndexChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@index = oldValue;
					OnIndexChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@index = value;
	                OnIndexChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnIndexChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnIndexChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnIndexChanged;

	}
	public partial class StaticData : Initializable
	{
		public staticData Xml { get; internal set; }

		public StaticData() : this(null, new staticData())
		{
		}
		public StaticData(Modeller model) : this(model, new staticData())
		{
		}
        internal StaticData(Modeller model, staticData xml)
        {
			Xml = xml;
			Model = model;

            if (xml.@records == null)
                xml.@records = new records();
			m_Records = new Records(Model, xml.@records);

			InitializeView();
			InitializeLogic();
        }
		public Records Records
		{
			get { return m_Records; }
			set
            {
				if (m_Records == value)
                    return;

				if (m_Records != null)
                    m_Records.Model = null;

                PropertyChangedEventArgs<Records> eventArgs = new PropertyChangedEventArgs<Records>(m_Records, value);
                m_Records = value;

				if (m_Records != null)
                    m_Records.Model = Model;

                if (OnRecordsChanged != null)
                    OnRecordsChanged(this, eventArgs);
            }
		}
		private Records m_Records = null;
		public event EventHandler<PropertyChangedEventArgs<Records>> OnRecordsChanged;

	}
	public partial class Records : Initializable
	{
		public records Xml { get; internal set; }

		public Records() : this(null, new records())
		{
		}
		public Records(Modeller model) : this(model, new records())
		{
		}
        internal Records(Modeller model, records xml)
        {
			Xml = xml;
			Model = model;

            if (xml.@record == null)
                xml.@record = new List<record>(); //normal IList`1

			InitializeView();
			InitializeLogic();
        }
		public Collection<Record> Record
		{
			get
			{
				if (m_Record == null)
				{
					m_Record = new ObservableCollection<Record>(Xml.@record.Select(item => new Record(Model, item)));
					m_Record.CollectionChanged += RaiseOnRecordCollectionChanged;
					m_Record.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                {
                                    int index = e.NewStartingIndex;
                                    foreach (Record item in e.NewItems)
									{
										item.Model = Model;
                                        Xml.@record.Insert(index++, item.Xml);
									}
                                }
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                {
									int oldItemIndex = 0;
                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
                                    {
                                        Record item = (Record)e.OldItems[oldItemIndex++];
                                        if (Xml.@record[index] != item.Xml)
                                            throw new InvalidOperationException();

										item.Model = null;
                                        Xml.@record.RemoveAt(index);
                                    }
                                }
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                {
									if(e.OldItems != null)
									{									
										foreach (Record item in e.OldItems)
											item.Model = null;
									}
                                    Xml.@record.Clear();
                                }
                                break;
                            case NotifyCollectionChangedAction.Replace:
                            case NotifyCollectionChangedAction.Move:
                                throw new NotSupportedException();
                        }
                    };
				}
				return m_Record; 
			}
		}
		private ObservableCollection<Record> m_Record = null;
		private event EventHandler<NotifyCollectionChangedEventArgs> RecordCollectionChanged;
		private void RaiseOnRecordCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (RecordCollectionChanged != null)
				RecordCollectionChanged(sender, e);
		}

	}
	public partial class Record : Initializable
	{
		public record Xml { get; internal set; }

		public Record() : this(null, new record())
		{
		}
		public Record(Modeller model) : this(model, new record())
		{
		}
        internal Record(Modeller model, record xml)
        {
			Xml = xml;
			Model = model;

            if (xml.@property == null)
                xml.@property = new List<property>(); //normal IList`1

			InitializeView();
			InitializeLogic();
        }
		public Collection<Property> Property
		{
			get
			{
				if (m_Property == null)
				{
					m_Property = new ObservableCollection<Property>(Xml.@property.Select(item => new Property(Model, item)));
					m_Property.CollectionChanged += RaiseOnPropertyCollectionChanged;
					m_Property.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                {
                                    int index = e.NewStartingIndex;
                                    foreach (Property item in e.NewItems)
									{
										item.Model = Model;
                                        Xml.@property.Insert(index++, item.Xml);
									}
                                }
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                {
									int oldItemIndex = 0;
                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
                                    {
                                        Property item = (Property)e.OldItems[oldItemIndex++];
                                        if (Xml.@property[index] != item.Xml)
                                            throw new InvalidOperationException();

										item.Model = null;
                                        Xml.@property.RemoveAt(index);
                                    }
                                }
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                {
									if(e.OldItems != null)
									{									
										foreach (Property item in e.OldItems)
											item.Model = null;
									}
                                    Xml.@property.Clear();
                                }
                                break;
                            case NotifyCollectionChangedAction.Replace:
                            case NotifyCollectionChangedAction.Move:
                                throw new NotSupportedException();
                        }
                    };
				}
				return m_Property; 
			}
		}
		private ObservableCollection<Property> m_Property = null;
		private event EventHandler<NotifyCollectionChangedEventArgs> PropertyCollectionChanged;
		private void RaiseOnPropertyCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (PropertyCollectionChanged != null)
				PropertyCollectionChanged(sender, e);
		}

		public String Guid
		{
			get { return Xml.@guid; }
			set
            {
				if (Xml.@guid == value)
                    return;

				String oldValue = Xml.@guid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@guid = oldValue;
					OnGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@guid = value;
	                OnGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanged;

		public String MappingGuid
		{
			get { return Xml.@mappingGuid; }
			set
            {
				if (Xml.@mappingGuid == value)
                    return;

				String oldValue = Xml.@mappingGuid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnMappingGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@mappingGuid = oldValue;
					OnMappingGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@mappingGuid = value;
	                OnMappingGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanged;

	}
	public partial class Property : Initializable
	{
		public property Xml { get; internal set; }

		public Property() : this(null, new property())
		{
		}
		public Property(Modeller model) : this(model, new property())
		{
		}
        internal Property(Modeller model, property xml)
        {
			Xml = xml;
			Model = model;

			InitializeView();
			InitializeLogic();
        }
		public String PropertyGuid
		{
			get { return Xml.@propertyGuid; }
			set
            {
				if (Xml.@propertyGuid == value)
                    return;

				String oldValue = Xml.@propertyGuid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnPropertyGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@propertyGuid = oldValue;
					OnPropertyGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@propertyGuid = value;
	                OnPropertyGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnPropertyGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnPropertyGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnPropertyGuidChanged;

		public String MappingGuid
		{
			get { return Xml.@mappingGuid; }
			set
            {
				if (Xml.@mappingGuid == value)
                    return;

				String oldValue = Xml.@mappingGuid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnMappingGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@mappingGuid = oldValue;
					OnMappingGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@mappingGuid = value;
	                OnMappingGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanged;

		public String Value
		{
			get { return Xml.@value; }
			set
            {
				if (Xml.@value == value)
                    return;

				String oldValue = Xml.@value;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnValueChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@value = oldValue;
					OnValueChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@value = value;
	                OnValueChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnValueChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnValueChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnValueChanged;

	}
	public partial class Relationship : Initializable
	{
		public relationship Xml { get; internal set; }

		public Relationship() : this(null, new relationship())
		{
		}
		public Relationship(Modeller model) : this(model, new relationship())
		{
		}
        internal Relationship(Modeller model, relationship xml)
        {
			Xml = xml;
			Model = model;

            if (xml.@source == null)
                xml.@source = new nodeReference();
			m_Source = new NodeReference(Model, xml.@source);

            if (xml.@target == null)
                xml.@target = new nodeReference();
			m_Target = new NodeReference(Model, xml.@target);

			InitializeView();
			InitializeLogic();
        }
		public NodeReference Source
		{
			get { return m_Source; }
			set
            {
				if (m_Source == value)
                    return;

				if (m_Source != null)
                    m_Source.Model = null;

                PropertyChangedEventArgs<NodeReference> eventArgs = new PropertyChangedEventArgs<NodeReference>(m_Source, value);
                m_Source = value;

				if (m_Source != null)
                    m_Source.Model = Model;

                if (OnSourceChanged != null)
                    OnSourceChanged(this, eventArgs);
            }
		}
		private NodeReference m_Source = null;
		public event EventHandler<PropertyChangedEventArgs<NodeReference>> OnSourceChanged;

		public NodeReference Target
		{
			get { return m_Target; }
			set
            {
				if (m_Target == value)
                    return;

				if (m_Target != null)
                    m_Target.Model = null;

                PropertyChangedEventArgs<NodeReference> eventArgs = new PropertyChangedEventArgs<NodeReference>(m_Target, value);
                m_Target = value;

				if (m_Target != null)
                    m_Target.Model = Model;

                if (OnTargetChanged != null)
                    OnTargetChanged(this, eventArgs);
            }
		}
		private NodeReference m_Target = null;
		public event EventHandler<PropertyChangedEventArgs<NodeReference>> OnTargetChanged;

		public String Name
		{
			get { return Xml.@name; }
			set
            {
				if (Xml.@name == value)
                    return;

				String oldValue = Xml.@name;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnNameChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@name = oldValue;
					OnNameChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@name = value;
	                OnNameChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanged;

		public String Type
		{
			get { return Xml.@type; }
			set
            {
				if (Xml.@type == value)
                    return;

				String oldValue = Xml.@type;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnTypeChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@type = oldValue;
					OnTypeChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@type = value;
	                OnTypeChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanged;

		public String Guid
		{
			get { return Xml.@guid; }
			set
            {
				if (Xml.@guid == value)
                    return;

				String oldValue = Xml.@guid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@guid = oldValue;
					OnGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@guid = value;
	                OnGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanged;

		public String MappingGuid
		{
			get { return Xml.@mappingGuid; }
			set
            {
				if (Xml.@mappingGuid == value)
                    return;

				String oldValue = Xml.@mappingGuid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnMappingGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@mappingGuid = oldValue;
					OnMappingGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@mappingGuid = value;
	                OnMappingGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnMappingGuidChanged;

	}
	public partial class NodeReference : Initializable
	{
		public nodeReference Xml { get; internal set; }

		public NodeReference() : this(null, new nodeReference())
		{
		}
		public NodeReference(Modeller model) : this(model, new nodeReference())
		{
		}
        internal NodeReference(Modeller model, nodeReference xml)
        {
			Xml = xml;
			Model = model;

			InitializeView();
			InitializeLogic();
        }
		public String Label
		{
			get { return Xml.@label; }
			set
            {
				if (Xml.@label == value)
                    return;

				String oldValue = Xml.@label;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnLabelChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@label = oldValue;
					OnLabelChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@label = value;
	                OnLabelChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChanged;

		public String Name
		{
			get { return Xml.@name; }
			set
            {
				if (Xml.@name == value)
                    return;

				String oldValue = Xml.@name;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnNameChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@name = oldValue;
					OnNameChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@name = value;
	                OnNameChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanged;

		public Boolean Nullable
		{
			get { return Xml.@nullable; }
			set
            {
				if (Xml.@nullable == value)
                    return;

				Boolean oldValue = Xml.@nullable;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnNullableChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@nullable = oldValue;
					OnNullableChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@nullable = value;
	                OnNullableChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnNullableChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnNullableChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnNullableChanged;

		public String ReferenceGuid
		{
			get { return Xml.@referenceGuid; }
			set
            {
				if (Xml.@referenceGuid == value)
                    return;

				String oldValue = Xml.@referenceGuid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnReferenceGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@referenceGuid = oldValue;
					OnReferenceGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@referenceGuid = value;
	                OnReferenceGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnReferenceGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnReferenceGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnReferenceGuidChanged;

		public String Type
		{
			get { return Xml.@type; }
			set
            {
				if (Xml.@type == value)
                    return;

				String oldValue = Xml.@type;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnTypeChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@type = oldValue;
					OnTypeChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@type = value;
	                OnTypeChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanged;

	}
	public partial class Submodel : Initializable
	{
		public submodel Xml { get; internal set; }

		public Submodel() : this(null, new submodel())
		{
		}
		public Submodel(Modeller model) : this(model, new submodel())
		{
		}
        internal Submodel(Modeller model, submodel xml)
        {
			Xml = xml;
			Model = model;

            if (xml.@node == null)
                xml.@node = new List<submodel.nodeLocalType>(); //normal IList`1

			InitializeView();
			InitializeLogic();
        }
		public String Explaination
		{
			get { return Xml.@explaination; }
			set
            {
				if (Xml.@explaination == value)
                    return;

				String oldValue = Xml.@explaination;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnExplainationChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@explaination = oldValue;
					OnExplainationChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@explaination = value;
	                OnExplainationChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnExplainationChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnExplainationChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnExplainationChanged;

		public Collection<NodeLocalType> Node
		{
			get
			{
				if (m_Node == null)
				{
					m_Node = new ObservableCollection<NodeLocalType>(Xml.@node.Select(item => new NodeLocalType(Model, item)));
					m_Node.CollectionChanged += RaiseOnNodeCollectionChanged;
					m_Node.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                {
                                    int index = e.NewStartingIndex;
                                    foreach (NodeLocalType item in e.NewItems)
									{
										item.Model = Model;
                                        Xml.@node.Insert(index++, item.Xml);
									}
                                }
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                {
									int oldItemIndex = 0;
                                    foreach (int index in Enumerable.Range(e.OldStartingIndex, e.OldItems.Count).Reverse())
                                    {
                                        NodeLocalType item = (NodeLocalType)e.OldItems[oldItemIndex++];
                                        if (Xml.@node[index] != item.Xml)
                                            throw new InvalidOperationException();

										item.Model = null;
                                        Xml.@node.RemoveAt(index);
                                    }
                                }
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                {
									if(e.OldItems != null)
									{									
										foreach (NodeLocalType item in e.OldItems)
											item.Model = null;
									}
                                    Xml.@node.Clear();
                                }
                                break;
                            case NotifyCollectionChangedAction.Replace:
                            case NotifyCollectionChangedAction.Move:
                                throw new NotSupportedException();
                        }
                    };
				}
				return m_Node; 
			}
		}
		private ObservableCollection<NodeLocalType> m_Node = null;
		private event EventHandler<NotifyCollectionChangedEventArgs> NodeCollectionChanged;
		private void RaiseOnNodeCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (NodeCollectionChanged != null)
				NodeCollectionChanged(sender, e);
		}

		public String Name
		{
			get { return Xml.@name; }
			set
            {
				if (Xml.@name == value)
                    return;

				String oldValue = Xml.@name;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnNameChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@name = oldValue;
					OnNameChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@name = value;
	                OnNameChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanged;

		public Int32? Chapter
		{
			get { return Xml.@chapter; }
			set
            {
				if (Xml.@chapter == value)
                    return;

				Int32? oldValue = Xml.@chapter;
                PropertyChangedEventArgs<Int32?> eventArgs = new PropertyChangedEventArgs<Int32?>(oldValue, value);

				OnChapterChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@chapter = oldValue;
					OnChapterChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@chapter = value;
	                OnChapterChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Int32?>> OnChapterChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Int32?>> OnChapterChanging;
		public event EventHandler<PropertyChangedEventArgs<Int32?>> OnChapterChanged;

		public Boolean IsDraft
		{
			get { return Xml.@isDraft; }
			set
            {
				if (Xml.@isDraft == value)
                    return;

				Boolean oldValue = Xml.@isDraft;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnIsDraftChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@isDraft = oldValue;
					OnIsDraftChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@isDraft = value;
	                OnIsDraftChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsDraftChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsDraftChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsDraftChanged;

		public Boolean IsLaboratory
		{
			get { return Xml.@isLaboratory; }
			set
            {
				if (Xml.@isLaboratory == value)
                    return;

				Boolean oldValue = Xml.@isLaboratory;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnIsLaboratoryChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@isLaboratory = oldValue;
					OnIsLaboratoryChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@isLaboratory = value;
	                OnIsLaboratoryChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsLaboratoryChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsLaboratoryChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsLaboratoryChanged;

	}
	public partial class Submodel
	{
		public partial class NodeLocalType : Initializable
		{
			public submodel.nodeLocalType Xml { get; internal set; }
	
			public NodeLocalType() : this(null, new submodel.nodeLocalType())
			{
			}
			public NodeLocalType(Modeller model) : this(model, new submodel.nodeLocalType())
			{
			}
	        internal NodeLocalType(Modeller model, submodel.nodeLocalType xml)
	        {
				Xml = xml;
				Model = model;
	
				InitializeView();
				InitializeLogic();
	        }
			public String Label
			{
				get { return Xml.@label; }
				set
	            {
					if (Xml.@label == value)
	                    return;
	
					String oldValue = Xml.@label;
	                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);
	
					OnLabelChanging?.Invoke(this, eventArgs);
					if (eventArgs.Cancel)
					{
						Xml.@label = oldValue;
						OnLabelChangeCancelled?.Invoke(this, eventArgs);
					}
					else
					{
						Xml.@label = value;
		                OnLabelChanged?.Invoke(this, eventArgs);
					}
	            }
			}
			public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChangeCancelled;
			public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChanging;
			public event EventHandler<PropertyChangedEventArgs<String>> OnLabelChanged;
	
			public String EntityGuid
			{
				get { return Xml.@entityGuid; }
				set
	            {
					if (Xml.@entityGuid == value)
	                    return;
	
					String oldValue = Xml.@entityGuid;
	                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);
	
					OnEntityGuidChanging?.Invoke(this, eventArgs);
					if (eventArgs.Cancel)
					{
						Xml.@entityGuid = oldValue;
						OnEntityGuidChangeCancelled?.Invoke(this, eventArgs);
					}
					else
					{
						Xml.@entityGuid = value;
		                OnEntityGuidChanged?.Invoke(this, eventArgs);
					}
	            }
			}
			public event EventHandler<PropertyChangedEventArgs<String>> OnEntityGuidChangeCancelled;
			public event EventHandler<PropertyChangedEventArgs<String>> OnEntityGuidChanging;
			public event EventHandler<PropertyChangedEventArgs<String>> OnEntityGuidChanged;
	
			public Double? Xcoordinate
			{
				get { return Xml.@xcoordinate; }
				set
	            {
					if (Xml.@xcoordinate == value)
	                    return;
	
					Double? oldValue = Xml.@xcoordinate;
	                PropertyChangedEventArgs<Double?> eventArgs = new PropertyChangedEventArgs<Double?>(oldValue, value);
	
					OnXcoordinateChanging?.Invoke(this, eventArgs);
					if (eventArgs.Cancel)
					{
						Xml.@xcoordinate = oldValue;
						OnXcoordinateChangeCancelled?.Invoke(this, eventArgs);
					}
					else
					{
						Xml.@xcoordinate = value;
		                OnXcoordinateChanged?.Invoke(this, eventArgs);
					}
	            }
			}
			public event EventHandler<PropertyChangedEventArgs<Double?>> OnXcoordinateChangeCancelled;
			public event EventHandler<PropertyChangedEventArgs<Double?>> OnXcoordinateChanging;
			public event EventHandler<PropertyChangedEventArgs<Double?>> OnXcoordinateChanged;
	
			public Double? Ycoordinate
			{
				get { return Xml.@ycoordinate; }
				set
	            {
					if (Xml.@ycoordinate == value)
	                    return;
	
					Double? oldValue = Xml.@ycoordinate;
	                PropertyChangedEventArgs<Double?> eventArgs = new PropertyChangedEventArgs<Double?>(oldValue, value);
	
					OnYcoordinateChanging?.Invoke(this, eventArgs);
					if (eventArgs.Cancel)
					{
						Xml.@ycoordinate = oldValue;
						OnYcoordinateChangeCancelled?.Invoke(this, eventArgs);
					}
					else
					{
						Xml.@ycoordinate = value;
		                OnYcoordinateChanged?.Invoke(this, eventArgs);
					}
	            }
			}
			public event EventHandler<PropertyChangedEventArgs<Double?>> OnYcoordinateChangeCancelled;
			public event EventHandler<PropertyChangedEventArgs<Double?>> OnYcoordinateChanging;
			public event EventHandler<PropertyChangedEventArgs<Double?>> OnYcoordinateChanged;
	
		}
	}
	public partial class FunctionalId : Initializable
	{
		public functionalId Xml { get; internal set; }

		public FunctionalId() : this(null, new functionalId())
		{
		}
		public FunctionalId(Modeller model) : this(model, new functionalId())
		{
		}
        internal FunctionalId(Modeller model, functionalId xml)
        {
			Xml = xml;
			Model = model;

			InitializeView();
			InitializeLogic();
        }
		public String Value
		{
			get { return Xml.@value; }
			set
            {
				if (Xml.@value == value)
                    return;

				String oldValue = Xml.@value;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnValueChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@value = oldValue;
					OnValueChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@value = value;
	                OnValueChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnValueChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnValueChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnValueChanged;

		public String Name
		{
			get { return Xml.@name; }
			set
            {
				if (Xml.@name == value)
                    return;

				String oldValue = Xml.@name;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnNameChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@name = oldValue;
					OnNameChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@name = value;
	                OnNameChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnNameChanged;

		public String Type
		{
			get { return Xml.@type; }
			set
            {
				if (Xml.@type == value)
                    return;

				String oldValue = Xml.@type;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnTypeChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@type = oldValue;
					OnTypeChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@type = value;
	                OnTypeChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnTypeChanged;

		public String Guid
		{
			get { return Xml.@guid; }
			set
            {
				if (Xml.@guid == value)
                    return;

				String oldValue = Xml.@guid;
                PropertyChangedEventArgs<String> eventArgs = new PropertyChangedEventArgs<String>(oldValue, value);

				OnGuidChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@guid = oldValue;
					OnGuidChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@guid = value;
	                OnGuidChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanging;
		public event EventHandler<PropertyChangedEventArgs<String>> OnGuidChanged;

		public Boolean IsDefault
		{
			get { return Xml.@isDefault; }
			set
            {
				if (Xml.@isDefault == value)
                    return;

				Boolean oldValue = Xml.@isDefault;
                PropertyChangedEventArgs<Boolean> eventArgs = new PropertyChangedEventArgs<Boolean>(oldValue, value);

				OnIsDefaultChanging?.Invoke(this, eventArgs);
				if (eventArgs.Cancel)
				{
					Xml.@isDefault = oldValue;
					OnIsDefaultChangeCancelled?.Invoke(this, eventArgs);
				}
				else
				{
					Xml.@isDefault = value;
	                OnIsDefaultChanged?.Invoke(this, eventArgs);
				}
            }
		}
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsDefaultChangeCancelled;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsDefaultChanging;
		public event EventHandler<PropertyChangedEventArgs<Boolean>> OnIsDefaultChanged;

	}
}
