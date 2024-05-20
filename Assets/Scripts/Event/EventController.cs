using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventController<T>
{
    public Action<T> baseEvent;
    public void AddListener(Action<T> listener)
    {
        baseEvent += listener;
    }
    public void RemoveListener(Action<T> listener)
    {
        baseEvent -= listener;
    }

    public void RaiseEvent(T param)
    {
        baseEvent?.Invoke(param);
    }

}

public class EventController
{
    public Action baseEvent;
    public void AddListener(Action listener)
    {
        baseEvent += listener;
    }
    public void RemoveListener(Action listener)
    {
        baseEvent -= listener;
    }

    public void RaiseEvent()
    {
        baseEvent?.Invoke();
    }

}