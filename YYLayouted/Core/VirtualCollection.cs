using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYLayouted
{
    public class VirtualCollection : CollectionBase
    {
        #region constructors

        public VirtualCollection()
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

        public event VirtualCollectionChangeHandler CollectionChange
        {
            add { this.Events.AddHandler(Event_CollectionChange, value); }
            remove { this.Events.RemoveHandler(Event_CollectionChange, value); }
        }

        private void RaiseChangeEvent(VirtualCollectionChangeAction action,
            int index, object value, object oldValue, object newValue)
        {
            VirtualCollectionChangeHandler handler = (VirtualCollectionChangeHandler)this.Events[Event_CollectionChange];
            if (handler != null)
            {
                handler(new VirtualCollectionChangeArgs(action, index, value, oldValue, newValue));
            }
        }

        #endregion

        #region override onxx

        protected override void OnClear()
        {
            base.OnClear();
            RaiseChangeEvent(VirtualCollectionChangeAction.BeforeClear, 0, null, null, null);
        }

        protected override void OnClearComplete()
        {
            base.OnClearComplete();
            RaiseChangeEvent(VirtualCollectionChangeAction.AfterClear, 0, null, null, null);
        }

        protected override void OnInsert(int index, object value)
        {
            base.OnInsert(index, value);
            RaiseChangeEvent(VirtualCollectionChangeAction.BeforeInsert, index, value, null, null);
        }

        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            RaiseChangeEvent(VirtualCollectionChangeAction.AfterInsert, index, value, null, null);
        }

        protected override void OnSet(int index, object oldValue, object newValue)
        {
            base.OnSet(index, oldValue, newValue);
            RaiseChangeEvent(VirtualCollectionChangeAction.BeforeSet, index, null, oldValue, newValue);
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            base.OnSetComplete(index, oldValue, newValue);
            RaiseChangeEvent(VirtualCollectionChangeAction.AfterSet, index, null, oldValue, newValue);
        }

        protected override void OnRemove(int index, object value)
        {
            base.OnRemove(index, value);
            RaiseChangeEvent(VirtualCollectionChangeAction.BeforeRemove, index, value, null, null);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            RaiseChangeEvent(VirtualCollectionChangeAction.AfterRemove, index, value, null, null);
        }

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

    #region delegate, eventargs class

    public class VirtualCollectionChangeArgs : EventArgs
    {
        private int _index;
        private object _value;
        private object _oldValue;
        private object _newValue;
        private VirtualCollectionChangeAction _action;

        public VirtualCollectionChangeArgs(VirtualCollectionChangeAction action,
            int index, object value, object oldValue, object newValue)
            : base()
        {
            _index = index;
            _value = value;
            _oldValue = oldValue;
            _newValue = newValue;
            _action = action;
        }

        public VirtualCollectionChangeArgs(VirtualCollectionChangeAction action)
            : this(action, 0, null, null, null)
        {
        }

        public VirtualCollectionChangeArgs(VirtualCollectionChangeAction action, object value)
            : this(action, 0, value, null, null)
        {
        }

        public VirtualCollectionChangeArgs(VirtualCollectionChangeAction action, int index, object value)
            : this(action, index, value, null, null)
        {
        }

        public VirtualCollectionChangeArgs(VirtualCollectionChangeAction action,
            int index, object oldValue, object newValue)
            : this(action, index, null, oldValue, newValue)
        {
        }

        public VirtualCollectionChangeAction Action
        {
            get { return _action; }
        }

        public int Index
        {
            get { return _index; }
        }

        public object Value
        {
            get { return _value; }
        }

        public object OldValue
        {
            get { return _oldValue; }
        }

        public object NewValue
        {
            get { return _newValue; }
        }
    }

    public delegate void VirtualCollectionChangeHandler(VirtualCollectionChangeArgs e);

    public enum VirtualCollectionChangeAction
    {
        BeforeClear,
        AfterClear,
        BeforeInsert,
        AfterInsert,
        BeforeSet,
        AfterSet,
        BeforeRemove,
        AfterRemove
    }

    #endregion
}
