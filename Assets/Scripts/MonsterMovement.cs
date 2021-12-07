using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    private float moveSpeed = 0f;

    private Vector3 moveDir = new Vector3(0, 1).normalized;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    public void StopMoving()
    {
        moveSpeed = 0f;
    }

    public void StartMoving()
    {
        moveSpeed = 3.6f;
    }

    public void ResetPosition()
    {
        rb.transform.localPosition = new Vector3(0, -5.4f);
    }
}
