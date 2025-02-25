using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    // Private variables
    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    int direction = 1;
    bool aggressive = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;

    }


    // Update is called every frame
    void Update()
    {
        if (!aggressive)
        {
            return;
        }

    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if (!aggressive)
        {
            return;
        }


        timer -= Time.deltaTime;


        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }


        rigidbody2d.MovePosition(position);
    }



    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();


        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        aggressive = false;
        GetComponent<Rigidbody2D>().simulated = false;
        animator.SetTrigger("Fixed");
    }


}



