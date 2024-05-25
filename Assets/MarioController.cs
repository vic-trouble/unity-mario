using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public int SPEED = 300;
    public int JUMP_HEIGHT = 500;
    public float AIRBORNE_SPEED_FACTOR = 0.5f;
    public GameObject boomerangObject;

    private int direction = 1;

    public bool isAirborne = false;
    private bool jumpingAction = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var Mario = GameObject.Find("Марио").GetComponent<Rigidbody2D>();
        //bool isAirborne = Mathf.Abs(Mario.velocity.y) > 0.1;  // TODO исправить прыжок в воздухе!

        var hitDirection = new Vector2(0, -0.5f);
        Debug.DrawRay(Mario.transform.position, hitDirection, Color.green, 0.1f);

        isAirborne = true;
        RaycastHit2D[] hits = Physics2D.RaycastAll(Mario.transform.position, hitDirection, hitDirection.magnitude);
        if (hits.Length > 1 || hits.Length == 1 && hits[0] != Mario)
        {
            isAirborne = false;
        }


        float speedFactor = isAirborne ? AIRBORNE_SPEED_FACTOR : 1;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Mario.AddForce(new Vector2(SPEED * speedFactor * Time.deltaTime, 0));
            direction = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Mario.AddForce(new Vector2(-SPEED * speedFactor * Time.deltaTime, 0));
            direction = -1;
        }

        Mario.transform.localScale = new Vector3(direction, 1, 1);
    }

    void Update()
    {
        var Mario = GameObject.Find("Марио").GetComponent<Rigidbody2D>();

        if (Input.GetKey/*Down*/(KeyCode.UpArrow))
        {
            //Debug.Log("Key Up");
            if (!isAirborne && !jumpingAction)
            {
                jumpingAction = true;
                Mario.AddForce(new Vector2(0, JUMP_HEIGHT));
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //Debug.Log("Key Down");
            jumpingAction = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
            var boomerang = Instantiate(boomerangObject).GetComponent<Boomerang>();
            var displacement = new Vector3(direction, 0, 0);
            boomerang.transform.position = Mario.transform.position + displacement;
            boomerang.Throw(direction);
            */
        }
    }

    private void CastRayOfDeath(Vector2 position, Vector2 direction)
    {
        Debug.DrawRay(position, direction, Color.green, 1.0f);

        var Mario = GameObject.Find("Марио").GetComponent<Rigidbody2D>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction, direction.magnitude);
        foreach (var hit in hits)
        {
            Debug.DrawRay(hit.collider.transform.position, new Vector2(0, 0.5f), Color.red, 2.0f);
            if (hit.collider.CompareTag("Enemy"))
            {
                var monster = hit.collider.gameObject.GetComponent<GumbaController>();
                monster.Die();
                Mario.AddForce(new Vector2(0, JUMP_HEIGHT * 0.25f));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var Mario = GameObject.Find("Марио").GetComponent<Rigidbody2D>();
        var displacement = new Vector3(0.2f, 0, 0);
        var hitDirection = new Vector2(0, -0.5f);
        CastRayOfDeath(Mario.transform.position - displacement, hitDirection);
        CastRayOfDeath(Mario.transform.position + displacement, hitDirection);
    }
}
