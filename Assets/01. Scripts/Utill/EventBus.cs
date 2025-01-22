using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventBus
{
    private static readonly Dictionary<EventType, UnityEvent> events = new();

    public static void Subscribe(EventType eventType, UnityAction action)
    {
        UnityEvent unityEvent;

        if (events.TryGetValue(eventType, out unityEvent))
        {
            unityEvent.AddListener(action);
        }
        else
        {
            unityEvent = new UnityEvent();
            unityEvent.AddListener(action);
            events.Add(eventType, unityEvent);
        }
    }

    public static void Unsubscribe(EventType eventType, UnityAction action)
    {
        if (events.TryGetValue(eventType, out UnityEvent unityEvent))
        {
            unityEvent.RemoveListener(action);
        }
    }

    public static void Publish(EventType eventType)
    {
        if (events.TryGetValue(eventType, out var unityEvent))
        {
            unityEvent?.Invoke();
        }
    }
}
