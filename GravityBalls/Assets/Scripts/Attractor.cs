using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 66.7f;
    public Rigidbody rb;
    private static List<Attractor> Attractors;


    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }
    void Attract (Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;
        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        float magnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * magnitude;

        rbToAttract.AddForce(force);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();

        if (attractors.Length > 1)
        {
            foreach (Attractor a in attractors)
            {
                if(a != this)
                Attract(a);
            }
        }
    }
}
