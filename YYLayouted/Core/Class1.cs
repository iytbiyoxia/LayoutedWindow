using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYLayouted  
{
    /// <summary>
    /// 带事件的实现IList接口的集合
    /// </summary>
    public class GMEventsCollection : CollectionBase
    {
        #region constructors

        public GMEventsCollection()
        {

        }

        #endregion

        #region private var, proterties

        private EventHandlerList _events;
        protected EventHandlerList Events
        {
            get
            {
                if (_events == null)
                    _events = new EventHandlerList();
                return _events;
            }
        }

        #endregion

        #region Events

        private static readonly object Event_CollectionChange = new object();

        //public event GMCollectionChangeHandler CollectionChange
        //{
        //    add { this.Events.AddHandler(Event_CollectionChange, value); }
        //    remove { this.Events.RemoveHandler(Event_CollectionChange, value); }
        //}

        //private void RaiseChangeEvent(GMCollectionChangeAction action,
        //    int index, object value, object oldValue, object newValue)
        //{
        //    GMCollectionChangeHandler handler = (GMCollectionChangeHandler)this.Events[Event_CollectionChange];
        //    if (handler != null)
        //    {
        //        handler(new GMCollectionChangeArgs(action, index, value, oldValue, newValue));
        //    }
        //}

        #endregion

        #region override onxx

        //protected override void OnClear()
        //{
        //    base.OnClear();
        //    RaiseChangeEvent(GMCollectionChangeAction.BeforeClear, 0, null, null, null);
        //}

        //protected override void OnClearComplete()
        //{
        //    base.OnClearComplete();
        //    RaiseChangeEvent(GMCollectionChangeAction.AfterClear, 0, null, null, null);
        //}

        //protected override void OnInsert(int index, object value)
        //{
        //    base.OnInsert(index, value);
        //    RaiseChangeEvent(GMCollectionChangeAction.BeforeInsert, index, value, null, null);
        //}

        //protected override void OnInsertComplete(int index, object value)
        //{
        //    base.OnInsertComplete(index, value);
        //    RaiseChangeEvent(GMCollectionChangeAction.AfterInsert, index, value, null, null);
        //}

        //protected override void OnSet(int index, object oldValue, object newValue)
        //{
        //    base.OnSet(index, oldValue, newValue);
        //    RaiseChangeEvent(GMCollectionChangeAction.BeforeSet, index, null, oldValue, newValue);
        //}

        //protected override void OnSetComplete(int index, object oldValue, object newValue)
        //{
        //    base.OnSetComplete(index, oldValue, newValue);
        //    RaiseChangeEvent(GMCollectionChangeAction.AfterSet, index, null, oldValue, newValue);
        //}

        //protected override void OnRemove(int index, object value)
        //{
        //    base.OnRemove(index, value);
        //    RaiseChangeEvent(GMCollectionChangeAction.BeforeRemove, index, value, null, null);
        //}

        //protected override void OnRemoveComplete(int index, object value)
        //{
        //    base.OnRemoveComplete(index, value);
        //    RaiseChangeEvent(GMCollectionChangeAction.AfterRemove, index, value, null, null);
        //}

        #endregion

        #region new public methods

        public bool Contains(object value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(object value)
        {
            return base.List.IndexOf(value);
        }

        public int Add(object value)
        {
            return base.List.Add(value);
        }

        public void Remove(object value)
        {
            base.List.Remove(value);
        }

        public void Insert(int index, object value)
        {
            base.List.Insert(index, value);
        }

        public void Insert(object value)
        {
            this.Insert(base.Count, value);
        }

        #endregion

        #region new public properties

        public object this[int index]
        {
            get
            {
                if (index < 0 || index > base.Count - 1)
                    return null;

                return base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

        #endregion
    }
}
