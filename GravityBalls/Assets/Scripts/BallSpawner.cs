using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public delegate void RevertGravity();
    public static event RevertGravity OnGravityRevert;

    public int numOfBalls;

    [SerializeField]
    [Range(2, 250)]
    private int maxNumberOfBalls = 50;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float intervalBetweenSpawns;

    [SerializeField]
    private float maxDistFromCamera = 20;

    private Camera mainCamera;

    bool doInstantiate = true;

    void Awake()
    {
        mainCamera = Camera.main;
        mainCamera.farClipPlane = 4f * maxDistFromCamera;
    }

    void Start()
    {
        numOfBalls = 0;
        StartCoroutine(SpawnBalls(intervalBetweenSpawns));
    }
    
    Vector3 RandomPointInFrustum()
    {
        float randZ = Random.Range(mainCamera.nearClipPlane * 4f, maxDistFromCamera);
        
        return new Vector3(0f, 0f, randZ) + RandomPointOnFrustumPlane(randZ);
    }

    Vector3 RandomPointOnFrustumPlane(float dist)
    {
        float h = FrustumHeight(dist);
        float w = FrustumWidth(h);

        float x = Random.Range(-w/2, w/2);
        float y = Random.Range(-h/2, h/2);

        return new Vector3 (x, y, 0f);
    }

    float FrustumHeight(float dist)
    {
        return 2.0f * dist * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FrustumWidth(float height)
    {
        return height * mainCamera.aspect;
    }


    IEnumerator SpawnBalls(float interval)
    {
        while(doInstantiate)
        {
            numOfBalls++;
            Debug.Log(numOfBalls);
            Instantiate(prefab, RandomPointInFrustum(), Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }

    void Update()
    {
        if (numOfBalls >= maxNumberOfBalls)
        {
            doInstantiate = false;
            OnGravityRevert();
        }
            
    }
}
