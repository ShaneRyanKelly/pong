using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle1 : MonoBehaviour
{
    // Player variables
    public GameObject paddle;
    public float moveSpeed;
    public float friction;
    private Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        GetRb();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)){
            if (Input.GetKey(KeyCode.UpArrow)){
                rB.velocity += Vector2.up * Time.deltaTime * moveSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow)){
                rB.velocity -= Vector2.up * Time.deltaTime * moveSpeed;
            }
        }
        else{
            rB.velocity -=  rB.velocity * Time.deltaTime * friction;
        }
    }

    private void GetRb(){
        rB = paddle.GetComponent<Rigidbody2D>();
    }
}
