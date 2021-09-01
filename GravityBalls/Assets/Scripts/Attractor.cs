using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float mass;
    public float speed;

    public bool reactivated;

    private Rigidbody rb {get {return GetComponent<Rigidbody>();}}

    void OnEnable()
    {
        Gravity.Register(this);

        if (reactivated)
        {
            AddRandomVelocity(100f);
            StartCoroutine(CollisionSleep(0.5f));
        }
    }

    void Awake()
    {
        reactivated = false;
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
        if(rb.mass != mass)
            mass = rb.mass;

        speed = rb.velocity.magnitude;

        Vector3 f = Vector3.zero;
        f = CalculateForce();
        if(!float.IsNaN(f.x) &&  !float.IsNaN(f.y) && !float.IsNaN(f.z))
            rb.AddForce(f);
    }

    void AddRandomVelocity(float speed)
    {
        Vector3 randVelocity = Random.insideUnitSphere;
        randVelocity = randVelocity.normalized * speed;
        rb.velocity = randVelocity;
    }

    IEnumerator CollisionSleep(float t)
    {
        yield return ToggleCollisions();
        yield return new WaitForSeconds(t);
        yield return ToggleCollisions();
    }
    IEnumerator ToggleCollisions()
    {
        bool dc = rb.detectCollisions;
        rb.detectCollisions = !rb;

        yield return null;
    }


    void OnDisable()
    {
        Gravity.Unregister(this);
    }
}
