using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text counter;
    [SerializeField]
    private BallSpawner spawner;

    void Update()
    {
        counter.text = string.Format("Balls created so far: {0}", spawner.numOfBalls);
    }

}
