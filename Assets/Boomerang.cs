using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public int THROW_X = 600;
    public int THROW_Y = 200;
    public int LIFT_FORCE = 10;
    public float BACK_FORCE = 4;
    public float LIFT_TIME = 3;
    public float ROTATION_SPEED = 0.1f;
    public float FATAL_SPEED = 10;

    private float liftTimer = 0;
    private int throwDirection = 0;
    private bool hasHit = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (hasHit)
        {
            return;
        }

        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        liftTimer -= Time.deltaTime;
        if (liftTimer > 0)
        {
            float passedTime = 2 + LIFT_TIME - liftTimer;
            rigidBody.AddForce(new Vector2(0, LIFT_FORCE * Time.deltaTime * passedTime * passedTime));
        }
        else
        {
            rigidBody.AddForce(new Vector2(-throwDirection * BACK_FORCE * Time.deltaTime, 0));
        }

        rigidBody.rotation += ROTATION_SPEED * Time.deltaTime;
    }

    public void Throw(int direction)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.simulated = true;
        rigidBody.AddForce(new Vector2(THROW_X * direction, THROW_Y));
        liftTimer = LIFT_TIME;
        throwDirection = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHit = true;
        /*
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody.velocity.magnitude >= FATAL_SPEED)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                var monster = collision.collider.gameObject.GetComponent<MonsterController>();
                monster.Die();
            }
        }
        */
    }
    /*
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("trigger");
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody.velocity.magnitude >= FATAL_SPEED)
        {
            if (collider.CompareTag("Enemy"))
            {
                var monster = collider.gameObject.GetComponent<MonsterController>();
                monster.Die();
            }
        }
    }
    */
}
