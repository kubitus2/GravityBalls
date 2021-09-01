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
        DisplayText();
        
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit(0);
        }
    }

    void DisplayText()
    {
        counter.text = string.Format("Balls created so far: {0}", spawner.numOfBalls);
    }
}
