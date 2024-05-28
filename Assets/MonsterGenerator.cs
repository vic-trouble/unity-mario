using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float SPAWN_DELAY = 1;
    public float spawnCooldown = 0;
    public Vector2 spawnForce = new Vector2(0, -1);

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
        spawnCooldown -= Time.deltaTime;
        if (spawnCooldown < 0)
        {
            spawnCooldown = SPAWN_DELAY;
            var newMonster = Instantiate(monsterPrefab/*, gameObject.transform, true*/);
            newMonster.transform.position = gameObject.transform.position;
            newMonster.GetComponent<Rigidbody2D>().AddForce(spawnForce);
        }
    }
}
