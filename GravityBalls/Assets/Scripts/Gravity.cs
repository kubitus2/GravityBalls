using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gravity
{
    const float G = 66.7f;

    public static List<Attractor> attractors = new List<Attractor>();

    public static Vector3 GravityForce(Attractor a, Attractor b)
    {
        Vector3 direction = DirectionAtoB(a.gameObject.transform.position, b.gameObject.transform.position);
        float dist = direction.magnitude;

        float force = (G * a.mass * b.mass) / (dist * dist);

        return force * direction.normalized;
    }

    private static Vector3 DirectionAtoB(Vector3 a, Vector3 b)
    {
        return b - a;
    }

    public static void Register (Attractor a)
    {
        attractors.Add(a);
    }

    public static void Unregister (Attractor a)
    {
        attractors.Remove(a);
    }   
}
