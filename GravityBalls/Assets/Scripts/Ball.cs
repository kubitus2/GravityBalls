using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    float initialMass;

    int constituentBalls;

    List<GameObject> ballPool = new List<GameObject>();

    void Awake()
    {
        constituentBalls = 1;
        rb = GetComponent<Rigidbody>();
        initialMass = rb.mass;
    }
    void OnCollisionEnter(Collision col)
    {
        if(ShouldAbsorb(col.gameObject))
        {
            Absorb(col.gameObject);
        }
    }

    bool ShouldAbsorb(GameObject toCompare)
    {
        if(this.rb.mass == toCompare.GetComponent<Rigidbody>().mass)
        {
            return this.GetComponent<Attractor>().speed > toCompare.GetComponent<Attractor>().speed;
        }
        else
        {
            return this.rb.mass > toCompare.GetComponent<Rigidbody>().mass;
        }  
    }

    void Absorb(GameObject obj)
    {
        float oldR = this.transform.localScale.x;
        float R = RecalculateRadius(this.transform.localScale.x, obj.transform.localScale.x);
        float change = R - oldR;

        this.transform.localScale += new Vector3(change, change, change);
        rb.mass += obj.GetComponent<Rigidbody>().mass;

        obj.SetActive(false);
        ballPool.Add(obj);
        constituentBalls++;
    }

    void Update()
    {
        if(rb.mass > 50 * initialMass)
        {
            Explode();
        }
    }

    float RecalculateRadius(float a, float b)
    {
        return Mathf.Sqrt(a * a + b * b);
    }

    void Explode()
    {
        foreach(var ball in ballPool)
        {
            ball.GetComponent<Attractor>().reactivated = true;
            ball.transform.position = transform.position;
            ball.SetActive(true);
        }

        gameObject.SetActive(false);
    }


}
