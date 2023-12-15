using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Player 1 variables
    public GameObject p1;
    public float p1MoveSpeed;
    private Rigidbody2D p1Rb;

    // Player 2 variables
    public GameObject p2;
    public float p2MoveSpeed;
    private Rigidbody2D p2Rb;

    // Ball Variables


    // Start is called before the first frame update
    void Start()
    {
        GetRbs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            if (Input.GetKey(KeyCode.W)){
                p1Rb.velocity += Vector2.up * Time.deltaTime * p1MoveSpeed;
            }
            else if (Input.GetKey(KeyCode.S)){
                p1Rb.velocity -= Vector2.up * Time.deltaTime * p1MoveSpeed;
            }
        }
    }

    private void GetRbs(){
        p1Rb = p1.GetComponent<Rigidbody2D>();
        p2Rb = p2.GetComponent<Rigidbody2D>();
    }
}
