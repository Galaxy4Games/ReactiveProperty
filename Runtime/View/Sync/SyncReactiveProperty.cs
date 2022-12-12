using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MVVM
{
    [Serializable]
    public class SyncReactiveProperty<T>: ISyncReactive
    {
        [SerializeField] private Object _target;
        [SerializeField] private string _propertyName;

        private IReadOnlyReactiveProperty<T> _property;

        public IReadOnlyReactiveProperty<T> Property =>
            _property ??= (IReadOnlyReactiveProperty<T>)Binders.GetProperty(_target, _propertyName);

        public void Subscribe(Action<T> action)
        {
            Property?.Subscribe(action);
        }

        public void UnSubscribe(Action<T> action)
        {
            Property?.UnSubscribe(action);
        }
    }
}