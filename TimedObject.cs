using UnityEngine;
using System.Collections;

public class TimedObject : MonoBehaviour {
    public static TimeManipulator _manipulator;
    public bool registerOnStart = true;

    void Start()
    {
        if (registerOnStart)
            _manipulator.registeredObjects.Add(transform);
    }

    public void RegisterManually()
    {
        _manipulator.registeredObjects.Add(transform);
    }
}
