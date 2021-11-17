using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kartscript : MonoBehaviour
{
    public Rigidbody kart;
    public float speed; //forward speed
    public float revspeed; // reverse speed
    public float speedometer; //KPH speed of car
    public float nitrocap; //Nitro available to use
    public float grip; //Imapcted by wheels, the higher the better
    public float topspeed; //impacted by engine, the higher the better
    public float timetotop=7f; //Impacted by engine, the lower the better
    public float timetozero=5f; //Slightly Impacted by brake power, the lower the better
    public float timetostationary = 2f; //Impacted by Brake Power, the lower the better
    public float nitropower = 1.0f ;//Timed Ammount of nitro bonus, not impacted by parts
    private float acceleration;
    private float deceleration ;
    private float brakerate;
    private float rotationAmount;

    private Vector3 pos;
    private Vector3 forwardDirection;
    private float gravityForce = 10f;
    private bool grounded;
    public LayerMask ground;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;
    public float dragOnGround = 3.0f;

    public Vector3 offset;
    private Quaternion initialRotation;


    void Start()
    {
        nitrocap = 0.0f;
        speed = 0;
        revspeed = 0;
        acceleration =  topspeed / timetotop;
        deceleration = -topspeed / timetozero;
        brakerate = -topspeed / timetostationary;
        initialRotation = kart.transform.rotation;
    }
    
    void OnCollisionStay(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.name == "Cube")
        {
            if (speed > 0)
            {
                speed += brakerate * Time.deltaTime;
                speed = Mathf.Max(speed, 10);
                kart.velocity = transform.forward * speed;
            }
            else
            {
                revspeed += brakerate * Time.deltaTime;
                revspeed = Mathf.Max(revspeed, 10);
                kart.velocity = -transform.forward * revspeed;
            }

        }
        if (collision.gameObject.name == "Cubeline")
        {   

        }
        
    }
    void Update()
    {
        //Steering and grip level
        //Steering only when car is moving
        if (speed > 0)
        {
            rotationAmount = Input.GetAxis("Horizontal") * grip;

        }
        //Inverted steering for reverse
        if (revspeed > 0)
        {
            rotationAmount = -Input.GetAxis("Horizontal") * grip;
            
        }
        rotationAmount *= Time.deltaTime;
        //if (grounded)
        //{
             kart.transform.Rotate (0.0f, rotationAmount, 0.0f);
            //kart.transform.rotation = Quaternion.Euler(kart.transform.rotation.eulerAngles + new Vector3(0f, rotationAmount * Input.GetAxis("Vertical"), 0f));
        //}
        //Accelerate
        if (Input.GetKey("up"))
        {
            if ( revspeed <= 0)
            {
                speed += acceleration * Time.deltaTime;
                speed = Mathf.Min(speed, topspeed);
                kart.velocity = transform.forward * speed;
            }
            else
            {
                revspeed += brakerate * Time.deltaTime;
                revspeed = Mathf.Max(revspeed, 0);
                kart.velocity = -transform.forward * revspeed;
            }
        }
        //Reverse
        if (Input.GetKey("down"))
        {
            if (speed <= 0)
            {
                revspeed += acceleration * Time.deltaTime;
                revspeed = Mathf.Min(revspeed, topspeed);
                kart.velocity = -transform.forward * revspeed;
            }
             else
            {
                 speed += brakerate * Time.deltaTime;
                 speed = Mathf.Max(speed, 0);
                 kart.velocity = transform.forward * speed;
            }
        }
        //Brake
        if (Input.GetKey("space"))
        {
            if (speed > 0)
            {
                speed += brakerate * Time.deltaTime;
                speed = Mathf.Max(speed, 0);
                kart.velocity = transform.forward * speed;
            }
            else
            {
                revspeed += brakerate * Time.deltaTime;
                revspeed = Mathf.Max(revspeed, 0);
                kart.velocity = -transform.forward * revspeed;
            }
        }
        //Deceleration
        if (!Input.GetKey("up") && !Input.GetKey("down"))
        {
            if (speed > 0 && revspeed ==0)
            {
                speed += deceleration * Time.deltaTime;
                speed = Mathf.Max(speed, 0);
                kart.velocity = transform.forward * speed;
            }
            else
            {
                revspeed += deceleration * Time.deltaTime;
                revspeed = Mathf.Max(revspeed, 0);
                kart.velocity = -transform.forward * revspeed;
            }
        }
        //Nitro Powerup
        if(Input.GetKey("s") && Input.GetKey("up")){
            if (nitrocap > 0)
            {
                speed += nitropower;
                nitrocap -= Time.deltaTime;
                nitrocap = Mathf.Max(nitrocap, 0);
            }
        }
        //Flip car
        if(Input.GetKey("f"))
        {
                if (Mathf.Abs(Vector3.Dot(kart.transform.up, Vector3.down)) >0.0f && speed <= 0.1f)
                {
                    kart.transform.position = kart.transform.position + offset;
                    kart.transform.rotation = initialRotation;
                }
        }
        speedometer = Mathf.Max(speed, revspeed);
    }

    
    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, ground))
        {
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded)
        {
            kart.drag = dragOnGround;

            if (Mathf.Abs(speed) > 0)
            {
                kart.AddForce(transform.forward * speed);
            }

        }
        else
        {
            kart.drag = 0.1f;
            kart.AddForce(Vector3.up * -gravityForce * 10000f);
        }
    }
}