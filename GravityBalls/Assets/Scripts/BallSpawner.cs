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
    private int maxNumberOfBalls = 250;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float intervalBetweenSpawns;

    bool doInstantiate;

    void Awake()
    {
        numOfBalls = 0;
        doInstantiate = true;
        Camera.main.farClipPlane = 1000f;
    }

    void Start()
    {
        StartCoroutine(SpawnBalls(intervalBetweenSpawns));
    }

    IEnumerator SpawnBalls(float interval)
    {
        while(doInstantiate)
        {
            numOfBalls++;
            Instantiate(prefab, CameraUtilities.RandomPointInFrustum(), Quaternion.identity);
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
