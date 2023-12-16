using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public float moveSpeed;
    public float hitForce;

    private Rigidbody2D rB;
    private Vector2 direction;
    private Vector2 hitDirection;

    // Start is called before the first frame update
    void Start()
    {
        rB = ball.GetComponent<Rigidbody2D>();
        GetDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rB.velocity = direction * Time.deltaTime * moveSpeed;
        rB.AddForceAtPosition(direction * hitForce, hitDirection, ForceMode2D.Impulse);
        //rB.AddForceAtPosition(direction.normalized, transform.position);
    }

    private void GetDirection(){
        int coinToss = Random.Range(0, 2);
        if (coinToss == 0){
            direction = Vector2.left;
            hitDirection = direction;
        }
        else{
            direction = Vector2.right;
            hitDirection = direction;
        }
    }

    private void /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    OnCollisionEnter2D(Collision2D other)
    {
        float hitPointX = rB.transform.position.x - other.transform.position.x;
        float hitPointY = rB.transform.position.y - other.transform.position.y;
        hitDirection = new Vector2(hitPointX, hitPointY);
        if (other.gameObject.tag == "Walls"){
            direction = new Vector2(direction.x, -direction.normalized.y);
            rB.AddForceAtPosition(direction.normalized, transform.position);
        }
        else {
            float newY = (rB.velocity.normalized.y / 2) + (other.collider.attachedRigidbody.velocity.normalized.y / 3);
            direction = new Vector2(-direction.x, newY);
            //direction = new Vector2(-direction.x, (other.gameObject.GetComponent<Rigidbody2D>().velocity.normalized.y * rB.velocity.normalized.y));
        }
        //direction = Vector2.Reflect(direction, other.contacts[0].normal);
    }
}
