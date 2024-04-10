using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMeterManager : MonoBehaviour
{

    [Tooltip("Fill image to show")]
    public Image fillImage;

    [Tooltip("Relation between the speed and the angle it should display")]
    [SerializeField] public AngleSpeed[] angleSpeeds;


    private float minAngle = 0;
    private float maxAngle = 0;

    public 
    // Start is called before the first frame update
    void Start()
    {
        //Check if the aray is not empty
        if (angleSpeeds.Length > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angleSpeeds[0].angle));

            minAngle = angleSpeeds[0].angle;
            maxAngle = angleSpeeds[angleSpeeds.Length - 1].angle;
        }
        else
        {
            Debug.Log("Angle speed relations are not set");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Check if the array is not empty
        if (angleSpeeds.Length == 0)
        {
            return;
        }


        //Calculate the rotation of the needle
        float angle = CalculateAngle();

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));



        //Calculate the proportion of the fill image
        float angleProprtion = (angle - minAngle) / (maxAngle - minAngle);

        if (fillImage != null) 
        {
            if (fillImage.fillMethod == Image.FillMethod.Radial360)
                fillImage.fillAmount = angleProprtion * ((minAngle - maxAngle) / 360);
            else
                fillImage.fillAmount = SpeedManager.Instance.speed / SpeedManager.Instance.maxSpeed;
        }
    }


    float CalculateAngle()
    {
        //Function to calculate the angle depending on the different array entries

        int lastSpeed = 0;

        //Check current and next angles to calculate the new angle
        for (int i = 0; i< angleSpeeds.Length; i++)
        {
            if (SpeedManager.Instance.speed <= angleSpeeds[i].speed)
            {
                break;
            }

            lastSpeed = i;
        }

        //Check if the last speed is not the last in the list
        if (lastSpeed == angleSpeeds.Length -1)
        {
            return angleSpeeds[lastSpeed].angle;
        }

        //Calculate the angle 
        float curranlge = angleSpeeds[lastSpeed].angle;
        float nextangle = angleSpeeds[lastSpeed + 1].angle;

        float spdProportion = (SpeedManager.Instance.speed - angleSpeeds[lastSpeed].speed) / (angleSpeeds[lastSpeed + 1].speed - angleSpeeds[lastSpeed].speed);


        return (spdProportion * (nextangle - curranlge)) + curranlge;
    }


}

[Serializable]
public class AngleSpeed
{
    [SerializeField] public float angle;
    [SerializeField] public float speed;
}
