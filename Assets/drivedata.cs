using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drivedata : MonoBehaviour
{
    public Text speedometer;
	public Text nitroavailable;

    public Rigidbody kart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedometer.text = Mathf.Round(kart.GetComponent<kartscript>().speedometer) +" KM/h";
		nitroavailable.text = "Nitro: " + kart.GetComponent<kartscript>().nitrocap.ToString("F2") +" sec";
    }
}
