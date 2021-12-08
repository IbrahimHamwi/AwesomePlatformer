using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;

    public GameObject birdEgg;
    public LayerMask playerLayer;
    private bool attacked, canMove;
    private float speed = 2.5f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x -= 6f;

        canMove = true;
    }
    void Update()
    {
        MoveTheBird();
        DropEgg();
    }

    void MoveTheBird()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * 3f * Time.smoothDeltaTime);
            if (transform.position.x >= originPosition.x)
            {
                moveDirection = Vector3.left;

                ChangeDirection(0.5f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                moveDirection = Vector3.right;

                ChangeDirection(-0.5f);
            }
        }
    }

    private void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
    void DropEgg()
    {
        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                attacked = true;
                anim.Play("BirdFly");
            }
        }
    }
    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {
            anim.Play("BirdDead");

            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;

            canMove = false;

            StartCoroutine(BirdDead());
        }

    }
}
