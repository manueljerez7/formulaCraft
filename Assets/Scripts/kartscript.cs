using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kartscript : MonoBehaviour
{
    public Rigidbody kart;
    public float speed; //forward speed
    public float revspeed; // reverse speed
    public float speedometer; //KPH speed of car
    public float nitrocap; //Nitro available to use
    public float grip; //Imapcted by wheels, the higher the better
    public float topspeed; //impacted by engine, the higher the better
    public float timetotop; //Impacted by engine, the lower the better
    public float timetozero; //Slightly Impacted by brake power, the lower the better
    public float timetostationary; //Impacted by Brake Power, the lower the better
    public float nitropower = 2.0f ;//Timed Ammount of nitro bonus, not impacted by parts
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
		grip = PlayerPrefs.GetFloat("grip"); //Imapcted by wheels, the higher the better
    	topspeed = PlayerPrefs.GetFloat("topSpeed"); //impacted by engine, the higher the better
    	timetotop=PlayerPrefs.GetFloat("timeToMaxSpeed"); //Impacted by engine, the lower the better
    	timetozero=PlayerPrefs.GetFloat("timeToZero"); //Slightly Impacted by brake power, the lower the better
    	timetostationary = PlayerPrefs.GetFloat("timeToStationary"); 
        nitrocap = 0.0f;
        nitropower = 2.0f;
        speed = 0;
        revspeed = 0;
        acceleration =  topspeed / timetotop;
        deceleration = -topspeed / timetozero;
        brakerate = -topspeed / timetostationary;
        initialRotation = kart.transform.rotation;
        soundManager.PlaySound(soundManager.Sound.KartStartup);
    }
    private void OnCollisionEnter(Collision collision)
    {
        kart.freezeRotation = true;
        ContactPoint contact = collision.contacts[0];
        Vector3 pos = contact.point;
        
        if(collision.gameObject.name == "Road.002" || collision.gameObject.name == "Landscape.001")
        {
            kart.AddForce(pos * -gravityForce * 20f);
            if (speed > 0)
            {
                speed += -0.35f*speed;
            }
            else
            {
                revspeed += -0.33f*revspeed;
            }
            soundManager.PlaySound(soundManager.Sound.KartHitsObstacle);
        }
        if(collision.gameObject.tag == "Obstacle")
        {
            soundManager.PlaySound(soundManager.Sound.KartHitsObstacle);
        }
    }
    
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Road.002" || collision.gameObject.name == "Landscape.001")
        {
            if (speed > 0)
            {
                speed += -350.0f * Time.deltaTime;
                speed = Mathf.Max(speed, 2);
                kart.velocity = transform.forward * speed;
            }
            else
            {
                revspeed += -350.0f * Time.deltaTime;
                revspeed = Mathf.Max(revspeed, 2);
                kart.velocity = -transform.forward * revspeed;
            }
        
        }

    }
    void Update()
    {
        //Steering and grip level
        //Steering only when car is moving
        //degradeble grip by speed increase
        if (speed > 0)
        {
            rotationAmount = Input.GetAxis("Horizontal") * grip *((topspeed-speed*0.35f)/topspeed);
        }
        //Inverted steering for reverse
        if (revspeed > 0)
        {
            rotationAmount = -Input.GetAxis("Horizontal") * grip;
            
        }
        rotationAmount *= Time.deltaTime;
        kart.transform.Rotate (0.0f, rotationAmount, 0.0f);

        //Accelerate
        if (Input.GetAxis("Vertical") > 0)
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

            soundManager.PlaySound(soundManager.Sound.KartAccelerate);
        }
        //Reverse
        if (Input.GetAxis("Vertical") < 0)
        {
            if (speed <= 0)
            {
                revspeed += acceleration * Time.deltaTime;
                revspeed = Mathf.Min(revspeed, topspeed/4);
                kart.velocity = -transform.forward * revspeed;
            }
             else
            {
                 speed += brakerate * Time.deltaTime;
                 speed = Mathf.Max(speed, 0);
                 kart.velocity = transform.forward * speed;
            }

            soundManager.PlaySound(soundManager.Sound.KartAccelerate);
        }
        //Brake
        if (Input.GetKey("space"))
        {
            if (speed > 0)
            {
                speed += brakerate * Time.deltaTime;
                speed = Mathf.Max(speed, 0);
                kart.velocity = transform.forward * speed;
                soundManager.PlaySound(soundManager.Sound.KartBreaks);
            }
            else
            {
                revspeed += brakerate * Time.deltaTime;
                revspeed = Mathf.Max(revspeed, 0);
                kart.velocity = -transform.forward * revspeed;
            }
            
        }
        //Deceleration
        if (Input.GetAxis("Vertical") == 0)
        {
            if(speed > 0)
            {
                soundManager.PlaySound(soundManager.Sound.KartDecelerate);
            }
            else
            {
                soundManager.PlaySound(soundManager.Sound.KartIdle);
            }
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
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical")>0){
            if (nitrocap > 0)
            {
                if (speed == topspeed)
                {
                    speed += 20;
                }
                else
                {
                    speed += nitropower;
                }
                
                nitrocap -= Time.deltaTime;
                nitrocap = Mathf.Max(nitrocap, 0);
                soundManager.PlaySound(soundManager.Sound.KartNitro);
            }
        }

        //Flip car
        if(Input.GetKeyUp("f"))
        {
                if (Mathf.Abs(Vector3.Dot(kart.transform.up, Vector3.down)) >0.0f && speed <= 0.1f)
                {
                    kart.transform.position = kart.transform.position + offset;
                    kart.transform.rotation = initialRotation;
                    soundManager.PlaySound(soundManager.Sound.KartFlip);
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