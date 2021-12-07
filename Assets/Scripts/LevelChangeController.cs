using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeController : MonoBehaviour
{
    public SpriteRenderer black;
    public PlayerMovement pm;
    public MonsterMovement mm;
    public CountdownController cc;
    public ChangeQuestion cQ;
    public LifeController lc;
    
    public float fadeTime = 1f;

    // on game start, fade the black out
    void Start()
    {
        StartCoroutine(Fade(false));
    }

    public void ChangeLevel()
    {
        
        StartCoroutine(Fade(true));
    }

    IEnumerator BehindTheSceneStuff()
    {
        // teleport player & monster back to original position
        pm.anim.Play("Idle Tree");
        pm.ResetPosition();
        mm.ResetPosition();
        cc.ResetTimer();
        cQ.changeQ();
        yield return new WaitForSeconds(1f);
        if(!lc.dead)
        {
            StartCoroutine(Fade(false));
            pm.cantMove = false;
        }
    }

    // if fadeIn == true, black will fade in instead of out when coroutine is started
    IEnumerator Fade(bool fadeIn)
    {
        if (fadeIn == true)
        {
            Color tmpColor = black.color;

            while (tmpColor.a < 1f)
            {
                tmpColor.a += Time.deltaTime / fadeTime;
                black.color = tmpColor;

                if (tmpColor.a >= 1f)
                    tmpColor.a = 1.0f;

                yield return null;
            }

            black.color = tmpColor;

            StartCoroutine(BehindTheSceneStuff()); // reset player position and monster position
            
        }
        else // fade black out
        {
            Color tmpColor = black.color;

            while (tmpColor.a > 0f)
            {
                tmpColor.a -= Time.deltaTime / fadeTime;
                black.color = tmpColor;

                if (tmpColor.a <= 0f)
                    tmpColor.a = 0.0f;

                yield return null;
            }

            black.color = tmpColor;

            mm.StartMoving(); // make monster start moving
        }
    }

}