using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    float mass = 0.1f;
    Vector3 force = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < -20f)
        {
            gameObject.SetActive(false);
        }
    }
}
