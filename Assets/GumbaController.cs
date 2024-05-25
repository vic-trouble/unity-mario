using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumbaController : MonoBehaviour
{
    public int SPEED = 200;
    public int DIRECTION = 1;
    public float AIRBORNE_SPEED_FACTOR = 0.2f;
    public float TERMINAL_SPEED = 15;

    public bool isAirborne;

    // Start is called before the first frame update
    void Start()
    {

    }

    private bool IsAirborne()
    {
        var Gumba = gameObject.GetComponent<Rigidbody2D>();

        // слева
        {
            var hitDirection = new Vector2(-0.3f, -0.5f);
            RaycastHit2D[] vhits = Physics2D.RaycastAll(Gumba.transform.position, hitDirection, hitDirection.magnitude);
            if (vhits.Length > 1 || vhits.Length == 1 && vhits[0] != Gumba)
            {
                return false;
            }
        }

        // справа
        {
            var hitDirection = new Vector2(0.3f, -0.5f);
            RaycastHit2D[] vhits = Physics2D.RaycastAll(Gumba.transform.position, hitDirection, hitDirection.magnitude);
            if (vhits.Length > 1 || vhits.Length == 1 && vhits[0] != Gumba)
            {
                return false;
            }
        }

        return true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var Gumba = gameObject.GetComponent<Rigidbody2D>();

        var ray = new Vector2(DIRECTION * 0.5f, 0);
        Debug.DrawRay(Gumba.transform.position, ray, Color.yellow, 0.1f);

        RaycastHit2D[] hits = Physics2D.RaycastAll(Gumba.transform.position, ray, ray.magnitude);
        if (hits.Length > 1 || hits.Length == 1 && hits[0].collider.gameObject.GetInstanceID() != gameObject.GetInstanceID())
        {
            DIRECTION = -DIRECTION;
            Debug.Log("hit " + hits[0].rigidbody.gameObject.GetInstanceID() + " my " + gameObject.GetInstanceID());
        }

        isAirborne = IsAirborne();
        //Debug.Log(isAirborne ? "in air" : "grounded");

        float speedFactor = isAirborne ? AIRBORNE_SPEED_FACTOR : 1;
        Gumba.AddForce(new Vector2(SPEED * speedFactor * DIRECTION * Time.deltaTime, 0));

        if (Mathf.Abs(Gumba.velocity[1]) > TERMINAL_SPEED)
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
    }

}
