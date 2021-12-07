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
    public AudioController ac;

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

            if(countdownTime < 5)
            {
                ac.PlayTicking();
            }
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
        ac.PlayTimeOut();
        pm.anim.Play("Hurt");
        mm.StopMoving();
        mm.anim.Play("Attack");
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
