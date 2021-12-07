using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public float points;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        points = PlayerPrefs.GetFloat("points");
        score.text = points.ToString();
    }
}
