using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    // Start is called before the first frame update
    private Rigidbody2D rb2d;

    [SerializeField]
    private float lerpRate = 0.2f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;

        rb2d.velocity = Vector2.Lerp(movement, rb2d.velocity, lerpRate);
    }
}
