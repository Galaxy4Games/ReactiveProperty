using System;
using System.Runtime.CompilerServices;

namespace MVVM
{
    [Serializable]
    public class ReactiveEvent : IReactiveEvent
    {
        private event Action OnEvent;

        public void Subscribe(Action action)
        {
            OnEvent += action;
        }

        public void UnSubscribe(Action action)
        {
            OnEvent -= action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Notify()
        {
            OnEvent?.Invoke();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Notify();
        }
#endif
    }
}