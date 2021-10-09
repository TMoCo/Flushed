using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Single speed;
    public Single speedModifier;
    public Int32 score;
    public Int32 scoreModifier;
    public Single angle;
    public Single angleSpeed;

    void Awake()
    {
        Reset();
    }

    void Update()
    {
        // increase speed based on score (every 1000 points)
        speedModifier = 1.0f + (score / 1000) * 0.25f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle = angle > -50.0f ? angle - angleSpeed * Time.deltaTime : angle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle = angle < 50.0f ? angle + angleSpeed * Time.deltaTime : angle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return;
        }
    }

    public void Reset()
    {
        score = 0;
        scoreModifier = 1;
        
        speed = 80.0f;
        speedModifier = 1.0f;
        
        angle = 0.0f;
        angleSpeed = 200.0f;
        transform.rotation = Quaternion.identity;
    }
        
}
