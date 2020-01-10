using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityToolbag;



public class Gravity : MonoBehaviour
{
    Thread _t1;
    private static PhysicalBody[] sphere;
    static float time = 0.0016F;
    static float gravity = 6F;

    // Start is called before the first frame update
    void Start()
    {
        sphere = FindObjectsOfType<PhysicalBody>();
    }

    // Update is called once per frame
    void Update()
    {
        int i;
        for (i = 0; i < sphere.Length; i++)
        {

            int e = i;
            Thread t = new Thread(() => ComputePosition(e));
            t.Start();
        }

    }


    public static void ComputePosition(int i)
    {
        Vector3 v = ComputeVelocity(i);
        sphere[i].setPos(sphere[i].getPos() + (v * time));
    }


    public static Vector3 ComputeVelocity(int i)
    {

        sphere[i].v = sphere[i].v + (ComputeAcceleration(i) * time);
        return sphere[i].v;

    }

    public static Vector3 ComputeAcceleration(int i)
    {
        Vector3 fAcc = new Vector3(0, 0, 0);
        for(int j = 0; j < sphere.Length;j++)
        {
            if (i != j )
            {
                Vector3 heading = sphere[j].getPos() - sphere[i].getPos();
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                float acc = (gravity * ((sphere[i].m * sphere[j].m) / Mathf.Pow(distance, 2))) / sphere[i].m;
                Vector3 a = acc * direction;
                fAcc += a;
            }
        }
        sphere[i].a = fAcc;
        return fAcc;
    }

   
}
