using UnityEngine;
using UnityEngine.UI; 

public class TextScript : MonoBehaviour
{
    public Text scoreText;

    public void UpdateScore(int Score)
    {
        scoreText.text = Score + "";
    }
}