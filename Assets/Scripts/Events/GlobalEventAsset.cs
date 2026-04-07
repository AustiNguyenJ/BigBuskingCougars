using System;
using UnityEngine;
using System.Collections.Generic;

public class GlobalEventAsset : ScriptableObject
{
    #region Singleton
    static GlobalEventAsset instance;
    public static GlobalEventAsset Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GlobalEventAsset>("Core/GlobalEventAsset");

                if (instance == null)
                {
                    Debug.LogError("GlobalEventAsset could not be found! Please ensure it is located exactly at: Resources/SO Assets/Core/GlobalEventAsset.asset");
                }
            }
            return instance;
        }
    }
    #endregion
    
    // Dictionary for listeners with a data payload
    readonly Dictionary<Type, Delegate> eventDictionary = new Dictionary<Type, Delegate>();
    
    // Dictionary for listeners without a data payload
    readonly Dictionary<Type, Action> simpleEventDictionary = new Dictionary<Type, Action>();

    #region Listeners WITH Payload
    public void StartListening<T>(Action<T> listener) where T : struct
    {
        Type eventType = typeof(T);

        if (eventDictionary.TryGetValue(eventType, out Delegate thisEvent))
        {
            eventDictionary[eventType] = Delegate.Combine(thisEvent, listener);
        }
        else
        {
            eventDictionary.Add(eventType, listener);
        }
    }

    public void StopListening<T>(Action<T> listener) where T : struct
    {
        Type eventType = typeof(T);

        if (eventDictionary.TryGetValue(eventType, out Delegate thisEvent))
        {
            Delegate currentEvent = Delegate.Remove(thisEvent, listener);

            if (currentEvent == null)
                eventDictionary.Remove(eventType);
            else
                eventDictionary[eventType] = currentEvent;
        }
    }
    #endregion

    #region Listeners WITHOUT Payload
    // OVERLOAD: Accepts a standard Action with no parameters
    public void StartListening<T>(Action listener) where T : struct
    {
        Type eventType = typeof(T);

        if (simpleEventDictionary.TryGetValue(eventType, out Action thisEvent))
        {
            simpleEventDictionary[eventType] = thisEvent + listener;
        }
        else
        {
            simpleEventDictionary.Add(eventType, listener);
        }
    }

    // OVERLOAD: Accepts a standard Action with no parameters
    public void StopListening<T>(Action listener) where T : struct
    {
        Type eventType = typeof(T);

        if (simpleEventDictionary.TryGetValue(eventType, out Action thisEvent))
        {
            thisEvent -= listener;

            if (thisEvent == null)
                simpleEventDictionary.Remove(eventType);
            else
                simpleEventDictionary[eventType] = thisEvent;
        }
    }
    #endregion

    public void TriggerEvent<T>(T eventData) where T : struct
    {
        Type eventType = typeof(T);

        // 1. Invoke everyone who wants the data
        if (eventDictionary.TryGetValue(eventType, out Delegate thisEvent))
        {
            (thisEvent as Action<T>)?.Invoke(eventData);
        }

        // 2. Invoke everyone who just wants the notification
        if (simpleEventDictionary.TryGetValue(eventType, out Action simpleEvent))
        {
            simpleEvent?.Invoke();
        }
    }

    void OnDisable()
    {
        eventDictionary.Clear();
        simpleEventDictionary.Clear();
    }
    
}