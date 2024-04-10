using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{

    private static SpeedManager _instance;

    public static SpeedManager Instance { get { return _instance; } }


    [Tooltip("Car's acceleration")]
    public float accereration = 0;

    [Tooltip("Car's deceleration")]
    public float deceleration = 0;

    [Tooltip("Car's max speed")]
    public float maxSpeed = 0;


    public Text speedText;

    [Tooltip("Current speed, do not edit")]
    public float speed = 0;

    private void Awake()
    {

        //Set up the speed manager as a Singleton simulating that its needed in more scripts, here I didn't needed but if the program scales up it would be helpful
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //Update the speed depending on the input
        if (Input.GetKey(KeyCode.A) && speed < maxSpeed) 
        {
            speed += accereration * Time.deltaTime;
        }
        else if (!Input.GetKey(KeyCode.A) && speed > 0)
        {
            speed -= deceleration * Time.deltaTime;
        }

        speedText.text = ((int)speed).ToString();
    }

}
