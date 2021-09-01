using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float mass;
    public float speed;

    private Rigidbody rb 
    {
        get 
        {
            return GetComponent<Rigidbody>();
        }
    }

    private float forceMultiplier;

    void OnEnable()
    {
        BallSpawner.OnGravityRevert += RevertGravity;

        Gravity.Register(this);
    }

    void Awake()
    {
        mass = rb.mass;
        forceMultiplier = 1.0f;
    }

    Vector3 CalculateForce()
    {
        Vector3 g = Vector3.zero;

        for (int i = 0; i < Gravity.attractors.Count; i++)
        {
            if(Gravity.attractors[i] != this)
            {
                g += Gravity.GravityForce(this, Gravity.attractors[i]) * forceMultiplier;
            }
        }

        return g;
    }

    void FixedUpdate()
    {
        UpdateSpeed();
        UpdateMass();
        UpdateForce();
    }

    void UpdateForce()
    {
        Vector3 f = Vector3.zero;
        f = CalculateForce();
        if(CheckIfNotNaN(f))
            rb.AddForce(f);
    }

    void UpdateMass()
    {
        if(rb.mass != mass)
            mass = rb.mass;
    }
    void UpdateSpeed()
    {
        speed = rb.velocity.magnitude;
    }

    bool CheckIfNotNaN(Vector3 v)
    {
        return !float.IsNaN(v.x) &&  !float.IsNaN(v.y) && !float.IsNaN(v.z);
    }

    void RevertGravity()
    {
        forceMultiplier = -1.0f;
        AudioManager.PlaySound(AudioManager.Sound.GravityReversion);
    }

    void OnDisable()
    {
        Gravity.Unregister(this);
        BallSpawner.OnGravityRevert -= RevertGravity;
    }
}
