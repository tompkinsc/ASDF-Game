using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator anim;
    private float moveSpeed = 150;

    public CountdownController countdown;

    private bool madeSelection = false;

    private Vector3 moveDir = new Vector3(0, 0).normalized;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator moveA()
    {
        setAnimXY(0, 1, true);

        yield return new WaitForSeconds(1.25f);

        setAnimXY(-1, 0, true);

        yield return new WaitForSeconds(1.5f);

        setAnimXY(0, 0, false);

        StopMoving();
    }

    IEnumerator moveS()
    {
        setAnimXY(0, 1, true);

        yield return new WaitForSeconds(1.25f);

        setAnimXY(-1, 0, true);

        yield return new WaitForSeconds(0.32f);

        setAnimXY(0, 1, true);

        yield return new WaitForSeconds(1.5f);

        setAnimXY(0, 0, false);

        StopMoving();
    }

    IEnumerator moveD()
    {
        setAnimXY(0, 1, true);

        yield return new WaitForSeconds(1.25f);

        setAnimXY(1, 0, true);

        yield return new WaitForSeconds(0.32f);

        setAnimXY(0, 1, true);

        yield return new WaitForSeconds(1.5f);

        setAnimXY(0, 0, false);

        StopMoving();
    }

    IEnumerator moveF()
    {
        setAnimXY(0, 1, true);

        yield return new WaitForSeconds(1.25f);

        setAnimXY(1, 0, true);

        yield return new WaitForSeconds(1.5f);

        setAnimXY(0, 0, false);

        StopMoving();
    }

    private void Update()
    {
        if(madeSelection == false)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                madeSelection = true;
                StartCoroutine(moveA());
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                madeSelection = true;
                StartCoroutine(moveS());
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                madeSelection = true;
                StartCoroutine(moveD());
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                madeSelection = true;
                StartCoroutine(moveF());
            }
        }
        else
        {
            // player made selection, disable timer
            countdown.timerStop();
        }
    }

    private void setAnimXY(float x, float y, bool isMoving)
    {
        anim.SetFloat("X", x);
        anim.SetFloat("Y", y);
        anim.SetBool("isMoving", isMoving);

        moveDir = new Vector3(x, y).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}
