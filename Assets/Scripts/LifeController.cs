using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private int maxHearts = 3;
    private int currHearts;

    private bool dead = false;

    private void Start()
    {
        currHearts = maxHearts;
    }

    public void loseHeart()
    {
        if(currHearts == 3)
        {
            heart3.gameObject.SetActive(false);
        }
        else if(currHearts == 2)
        {
            heart2.gameObject.SetActive(false);
        }
        else if(currHearts == 1)
        {
            heart1.gameObject.SetActive(false);
            dead = true;
        }

        currHearts--;
    }
}
