using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TimeManipulator : MonoBehaviour {

    public List<Transform> registeredObjects = new List<Transform>();
    List<Moment> moments;

	void Awake () {
        RegisterManipulator();
        moments = new List<Moment>();
    }

    bool reversedTime = false;
    void Update()
    {
        if (moments.Count > 128000)
            moments.RemoveRange(0, 1000);
        if (!reversedTime)
            moments.Add(Moment.RecordMoment());
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            for (int i = 0; i < registeredObjects.Count(); i++)
                if (registeredObjects[i].GetComponent<Rigidbody>() != null)
                    registeredObjects[i].GetComponent<Rigidbody>().isKinematic = true;
            reversedTime = true;
        }
        if (Input.GetKey(KeyCode.LeftControl))
            Rewind();
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            for (int i = 0; i < registeredObjects.Count(); i++)
                if (registeredObjects[i].GetComponent<Rigidbody>() != null)
                    registeredObjects[i].GetComponent<Rigidbody>().isKinematic = false;
            reversedTime = false;
        }
    }

    void Rewind()
    {
        if (moments.Count() > 0)
        {
            var m = moments.Last();
            m.Inact();
            moments.Remove(m);
            if (moments.Count() > 0) moments.Remove(moments.Last());
        }
    }

    void RegisterManipulator()
    {
        TimedObject._manipulator = this;
    }
}

public class Moment
{
    public List<MomentEntity> Entities = new List<MomentEntity>();

    public static Moment RecordMoment()
    {
        var m = TimedObject._manipulator;
        var moment = new Moment();
        for (int i = 0; i < m.registeredObjects.Count(); i++)
            moment.Entities.Add(new MomentEntity(m.registeredObjects[i], m.registeredObjects[i].GetComponent<Rigidbody>()));
        return moment;
    }

    public void Inact()
    {
        for (int i = 0; i < Entities.Count; i++)
            Entities[i].Retrive();
    }
}

public class MomentEntity
{
    public MomentEntity(Transform t, Rigidbody r=null)
    {
        transform = t;
        rigidbody = r;
        Record();
    }

    public Transform transform;
    public Rigidbody rigidbody;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Vector3 velocity;
    public Vector3 torque;

    void Record()
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
        if (rigidbody != null)
        {
            velocity = rigidbody.velocity;
            torque = rigidbody.angularVelocity;
        }
    }

    public void Retrive()
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
        if (rigidbody != null)
        {
            rigidbody.velocity = velocity;
            rigidbody.angularVelocity = torque;
        }
    }
}
