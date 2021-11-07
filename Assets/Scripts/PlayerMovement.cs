using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator anim;
    private float moveSpeed = 150;

    public CountdownController countdown;
    public LevelChangeController lcc;

    private bool madeSelection = false;
    public bool cantMove = false;

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

        lcc.ChangeLevel();
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

        lcc.ChangeLevel();
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

        lcc.ChangeLevel();
    }

    IEnumerator moveF()
    {
        setAnimXY(0, 1, true);

        yield return new WaitForSeconds(1.25f);

        setAnimXY(1, 0, true);

        yield return new WaitForSeconds(1.5f);

        setAnimXY(0, 0, false);

        StopMoving();

        lcc.ChangeLevel();
    }

    private void Update()
    {
        if(madeSelection == false & cantMove == false)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                movePlayer(0);
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                movePlayer(1);
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                movePlayer(2);
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                movePlayer(3);
            }
        }
    }

    // stop updating the update function by making madeSelection = true, start the correct movement coroutine, stop the timer
    private void movePlayer(int dir)
    {
        madeSelection = true;
        countdown.timerStop();

        if(dir == 0) // A
        {
            StartCoroutine(moveA());
        }
        else if(dir == 1) // S
        {
            StartCoroutine(moveS());
        }
        else if(dir == 2) // D
        {
            StartCoroutine(moveD());
        }
        else if(dir == 3) // F
        {
            StartCoroutine(moveF());
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

    public void ResetPosition()
    {
        rb.transform.localPosition = new Vector3(0, -3.867335f);
        madeSelection = false;
    }
}
