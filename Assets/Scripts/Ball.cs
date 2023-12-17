using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public float moveSpeed;
    public float hitForce;
    public float maxSpeed;

    public float TimeOfSimulation;

    private Rigidbody2D rB;
    private LineRenderer lR;
    private Vector2 direction;
    private Vector2 hitDirection;
    private Vector2 maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rB = ball.GetComponent<Rigidbody2D>();
        lR = ball.GetComponent<LineRenderer>();
        maxVelocity = new Vector2(maxSpeed, 0.0f);
        GetDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // working velocity implement
        //rB.velocity = direction * Time.deltaTime * moveSpeed;

        /*lR.positionCount = SimulateTrajectory().Count;
        for (int i = 0; i < lR.positionCount; i++){
            lR.SetPosition(i, SimulateTrajectory()[i]);
        }*/
        
        rB.AddForceAtPosition((direction*Time.deltaTime*moveSpeed), transform.position);
        rB.velocity = Vector2.ClampMagnitude(rB.velocity, maxSpeed);
        //Debug.Log(rB.velocity);
        Debug.Log(direction);

        // lets try positions based


        //rB.AddForce( hitForce * Vector2.Cross(rB.velocity,rB.angularVelocity), ForceMode.Force);
        //rB.AddForceAtPosition(direction, hitDirection, ForceMode2D.Impulse);
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
        //hitDirection = (rB.velocity.normalized * other.collider.attachedRigidbody.velocity.normalized);
        if (other.gameObject.tag == "Walls"){
            //intereseting
            //float newY = (rB.angularVelocity - other.contacts[0].normal.y);
            //direction = Vector2.ClampMagnitude(new Vector2(direction.x, newY), 0.7f);
            
            //direction = Vector2.Reflect(direction, other.contacts[0].normal);
            //direction = new Vector2(direction.x, -rB.velocity.normalized.y);
        }
        else if (other.gameObject.tag == "Goals"){
            Reset();
        }
        else {
            float newY = (rB.velocity.normalized.y - other.collider.attachedRigidbody.velocity.normalized.y);
            //float newY = (rB.velocity.normalized.y / 2) + (other.collider.attachedRigidbody.velocity.normalized.y / 3);
            direction = Vector2.ClampMagnitude(new Vector2(-direction.x, newY), 0.7f);
            //direction = new Vector2(-direction.x, (other.gameObject.GetComponent<Rigidbody2D>().velocity.normalized.y * rB.velocity.normalized.y));
        }
        hitDirection = new Vector2(direction.x, (other.collider.attachedRigidbody.velocity.normalized.y - rB.velocity.normalized.y));
        //direction = Vector2.Reflect(direction, other.contacts[0].normal);
    }

    private void Reset(){
        transform.position = Vector2.zero;
        direction = Vector2.zero;
        rB.velocity = Vector2.zero;
        GetDirection();
    }

    private List<Vector2> SimulateTrajectory(){
        float simulateForDuration = TimeOfSimulation;
        float simulationStep = 0.1f;//Will add a point every 0.1 secs.

        int steps = (int)(simulateForDuration / simulationStep);
        List<Vector2> lineRendererPoints = new List<Vector2>();
        Vector2 calculatedPosition;
        Vector2 directionVector = rB.velocity;// The direction it should go
        Vector2 launchPosition = transform.position;//Position where you launch from
        //float launchSpeed = 5f;//The initial power applied on the player

        for (int i = 0; i < steps; ++i)
        {
            calculatedPosition = launchPosition + (directionVector * (Time.deltaTime *  moveSpeed * i * simulationStep));
            //Calculate gravity
            //calculatedPosition.y += Physics2D.gravity.y * (i * simulationStep);
            lineRendererPoints.Add(calculatedPosition);
            /*if (CheckForCollision(calculatedPosition))//if you hit something
            {
                break;//stop adding positions
            }*/

        }

        return lineRendererPoints;
    }
}
