using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public float countdownTime;
    public Text countdownDisplay;
    public MonsterMovement mm;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        timerStop();
    }

    public void timerStop()
    {
        countdownDisplay.gameObject.SetActive(false);
        mm.moveSpeed = 0;
    }
}
