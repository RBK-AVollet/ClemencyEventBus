using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clemency.EventBus {
    public static class EventBus<T> where T : IEvent {
        static readonly HashSet<IEventBinding<T>> _bindings = new();
    
        public static void Register(EventBinding<T> binding) => _bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => _bindings.Remove(binding);

        public static void Raise(T @event) {
            foreach (var binding in _bindings) {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }

        static void Clear() {
            Debug.Log($"Clearing {typeof(T).Name} bindings...");
            _bindings.Clear();
        }
    }
}