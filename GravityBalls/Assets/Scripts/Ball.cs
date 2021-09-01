using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector]
    public bool reactivated;

    [SerializeField]
    [Range(25, 120)]
    private float explosionSpeed = 50f;

    private Rigidbody rb 
    {
        get 
        {
            return GetComponent<Rigidbody>();
        }
    }

    private Attractor a
    {
        get 
        {
            return GetComponent<Attractor>();
        }
    }

    private float initialMass;

    List<GameObject> ballPool = new List<GameObject>();

    void OnEnable()
    {
        if (reactivated)
        {
            AddRandomVelocity(explosionSpeed);
            StartCoroutine(CollisionSleep(0.5f));
        }
    }

    void Awake()
    {
        reactivated = false;
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
        Rigidbody rbC = toCompare.GetComponent<Rigidbody>();
        Attractor aC = toCompare.GetComponent<Attractor>();

        if(this.rb.mass == rbC.mass)
        {
            return a.speed > aC.speed;
        }
        else
        {
            return rb.mass > rbC.mass;
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
    }

    float RecalculateRadius(float a, float b)
    {
        return Mathf.Sqrt(a * a + b * b);
    }

    void Explode()
    {
        foreach(var ball in ballPool)
        {
            ball.GetComponent<Ball>().reactivated = true;
            ball.transform.position = transform.position;
            ball.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    void AddRandomVelocity(float speed)
    {
        Vector3 randVelocity = Random.insideUnitSphere;
        randVelocity = randVelocity.normalized * speed;
        rb.velocity = randVelocity;
    }

    void FixedUpdate()
    {
        if(rb.mass > 50 * initialMass)
        {
            Explode();
        }
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
        rb.detectCollisions = !dc;

        yield return null;
    }
}
