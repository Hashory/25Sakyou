using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D rb_left;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce((Vector3.right + Vector3.up) * 500);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            rb_left.AddForce((Vector3.left + Vector3.up) * 500);
        }
    }
}