using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    // tranform
    [SerializeField]
    private Transform pointerTransform;

    [SerializeField]
    private float angle_offset = 0.0f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rot = (Mathf.Rad2Deg * Mathf.Atan(rb2d.velocity.y/rb2d.velocity.x) + 
            angle_offset);
        if((rb2d.velocity.x) >= 0){
            rot+=180;
        }
        pointerTransform.rotation = Quaternion.Lerp(pointerTransform.rotation, Quaternion.Euler(Vector3.forward * rot), 0.1f);
    }
}
