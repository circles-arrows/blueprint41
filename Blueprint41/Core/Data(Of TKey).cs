using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Core
{
    public abstract class Data<TKey> : Data
    {
        public abstract TKey GetKey();
        public bool HasKey
        {
            get
            {
                TKey key = GetKey();
                if (key == null)
                    return false;

                return !key.Equals(default(TKey));
            }
        }

        sealed internal protected override void SetKey(object key)
        {
            SetKey((TKey)key);
        }
        protected virtual void SetKey(TKey key)
        {
            PersistenceState = PersistenceState.HasUid;
            Transaction.Current.Register(Wrapper.GetEntity().Name, Wrapper);
        }
    }
}
