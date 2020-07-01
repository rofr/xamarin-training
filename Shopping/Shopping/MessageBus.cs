using System;
using System.Collections.Generic;

namespace Shopping
{

    /// <summary>
    /// Custom message bus implementation, use with caution as the referenced actions might prevent garbage collection.
    /// If view models are singletons it will probably not matter but beware!
    /// Consider using MessagingCenter class of Xamarin Forms or modify this class to use WeakReference
    /// </summary>
    public static class MessageBus
    {
        private static readonly Dictionary<Type, List<Action<object>>>
            Subscribers = new Dictionary<Type, List<Action<object>>>();

        public static void Subscribe<T>(Action<T> handler)
        {
            var exists = Subscribers.TryGetValue(typeof(T), out var actions);
            if (!exists)
            {
                actions = new List<Action<object>>();
                Subscribers.Add(typeof(T), actions);
            }
            actions.Add(o => handler((T)o));
        }

        public static void Publish<T>(T @event)
        {
            if (Subscribers.TryGetValue(typeof(T), out var actions))
            {
                actions.ForEach(a => a.Invoke(@event));
            }
        }
    }
}