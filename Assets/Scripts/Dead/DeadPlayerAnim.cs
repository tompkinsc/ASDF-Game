using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerAnim : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator anim;

    private Vector3 moveDir = new Vector3(0, 0).normalized;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        moveDir = new Vector3(0, 0).normalized;
    }
    private void setAnimXY(float x, float y, bool isMoving)
    {
        anim.SetFloat("X", x);
        anim.SetFloat("Y", y);
        anim.SetBool("isMoving", isMoving);

        moveDir = new Vector3(x, y).normalized;
    }
}
