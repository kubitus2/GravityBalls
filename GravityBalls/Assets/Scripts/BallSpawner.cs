using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float intervalBetweenSpawns;

    bool doInstantiate = true;

    void Start()
    {
        StartCoroutine(SpawnBalls(intervalBetweenSpawns));
    }
    

    IEnumerator SpawnBalls(float interval)
    {
        while(doInstantiate)
        {
            Instantiate(prefab, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }

    }
}
