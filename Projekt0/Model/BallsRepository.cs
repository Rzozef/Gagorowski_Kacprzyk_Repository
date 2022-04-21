using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace Prezentacja
{
    namespace Model
    {
        internal class BallsRepository<BallAbstract> : AsyncObservableCollection<BallAbstract>
        {

            public void RegisterPropertyChanged(INotifyCollectionChanged item)
            {
                if (item != null)
                {
                    item.CollectionChanged += new NotifyCollectionChangedEventHandler(ItemPropertyChanged);
                }
            }

            public void UnRegisterPropertyChanged(INotifyCollectionChanged item)
            {
                if (item != null)
                {
                    item.CollectionChanged -= new NotifyCollectionChangedEventHandler(ItemPropertyChanged);
                }
            }

            private void ItemPropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }
}