using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraUtilities
{
    private static Camera mainCamera = Camera.main;
    private static float maxDistFromCamera = 30f;

    public static Vector3 RandomPointInFrustum()
    {
        float randZ = Random.Range(mainCamera.nearClipPlane * 5f, maxDistFromCamera);
        
        return new Vector3(0f, 0f, randZ) + RandomPointOnFrustumPlane(randZ);
    }

    public static Vector3 RandomPointOnFrustumPlane(float dist)
    {
        float h = FrustumHeight(dist);
        float w = FrustumWidth(h);

        float x = Random.Range(-w/2, w/2);
        float y = Random.Range(-h/2, h/2);

        return new Vector3 (x, y, 0f);
    }

    public static float FrustumHeight(float dist)
    {
        return 2.0f * dist * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    public static float FrustumWidth(float height)
    {
        return height * mainCamera.aspect;
    }
}
