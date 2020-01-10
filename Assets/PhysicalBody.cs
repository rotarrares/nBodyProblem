using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PhysicalBody : MonoBehaviour
{
    private Vector3 p;
    public Vector3 vDirection;
    public Vector3 v;
    public Vector3 a;
    public Vector3 aDirection;
    public float m;
    private Mutex mutex = new Mutex(); 


    public Vector3 getPos()
    {
        return p;
    }

    public void setPos(Vector3 pos)
    {
        mutex.WaitOne();
        p = pos;
        mutex.ReleaseMutex();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        p = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = p;
    }
}
