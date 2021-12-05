using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
        }
    }
}
