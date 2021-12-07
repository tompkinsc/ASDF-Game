using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public float points = 0f; 
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = points.ToString();
    }
    
    public void addPoint()
    {
        points += 1;
        score.text = points.ToString();
    }
}
