using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedometer : MonoBehaviour
{
    public Rigidbody target;
    public float maxSpeed = 0.0f;
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;
    public Text speedometertext; 
    public RectTransform arrow;
    private float speed = 0.0f;
    
    void Start()
    {
        maxSpeed = PlayerPrefs.GetFloat("topSpeed")+20;
        minSpeedArrowAngle = 13.26f;
        maxSpeedArrowAngle = (maxSpeed * -166f)/220;
    }

    
    void Update()
    {
        speed = Mathf.Round(target.GetComponent<kartscript>().speedometer);
        
        if (speedometertext != null)
        {
            speedometertext.text = ((int) speed) + " km/h";
        }

        if (arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
        }
    
    }
}
