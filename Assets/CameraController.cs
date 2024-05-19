using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            var heroPos =target.transform.position;
            var t = Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, heroPos.x, t),
                Mathf.Lerp(transform.position.y, heroPos.y, t), transform.position.z);
        }
    }
}
