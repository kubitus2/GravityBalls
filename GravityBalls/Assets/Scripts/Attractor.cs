using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float mass;
    public float speed;

    private Rigidbody rb {get {return GetComponent<Rigidbody>();}}

    void OnEnable()
    {
        Gravity.Register(this);
    }

    void Awake()
    {
        mass = rb.mass;
    }

    Vector3 CalculateForce()
    {
        Vector3 g = Vector3.zero;

        for (int i = 0; i < Gravity.attractors.Count; i++)
        {
            if(Gravity.attractors[i] != this)
            {
                g += Gravity.GravityForce(this, Gravity.attractors[i]);
            }
        }

        return g;
    }

    void FixedUpdate()
    {
        speed = rb.velocity.magnitude;
        rb.AddForce(CalculateForce());
    }

    void OnDisable()
    {
        Gravity.Unregister(this);
    }
}
