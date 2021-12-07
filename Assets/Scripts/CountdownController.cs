using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    private float countdownTime = 11f;
    public Text countdownDisplay;
    public LevelChangeController lcc;
    public PlayerMovement pm;
    public MonsterMovement mm;
    public LifeController lc;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while(countdownTime >= 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        timeRanOut();
    }

    public void timerStop()
    {
        countdownDisplay.gameObject.SetActive(false);
        mm.StopMoving();
    }

    public void timeRanOut()
    {
        pm.cantMove = true;
        mm.StopMoving();
        lc.loseHeart();
        lcc.ChangeLevel();
    }

    public void ResetTimer()
    {
        countdownTime = 11f;
        countdownDisplay.gameObject.SetActive(true);
        StartCoroutine(Countdown());
    }
}
