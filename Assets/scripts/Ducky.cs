using System;
using System.Collections;
using UnityEngine;

public class Ducky : MonoBehaviour
{
    public PipeManager manager;
    public Player player;
    public UIManager ui;
    public ObjectSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        ui.UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // reached the end of a pipe
    void OnTriggerEnter(Collider collider)
    {
        // most likely to return true
        if (collider.tag == "PickUp")
        {
            PickUp pickUp = collider.gameObject.GetComponent<PickUp>();

            player.score += pickUp.value * player.scoreModifier;

            ui.UpdateScore();

            Destroy(collider.gameObject);
            return;
        }

        if (collider.tag == "PowerUp")
        {
            // get the power up and start the coroutine with acceptable parameters
            PowerUp powerUp = collider.gameObject.GetComponent<PowerUp>();

            player.score += powerUp.value * player.scoreModifier;

            IEnumerator coroutine = powerUp.ApplyEffect(player, powerUp.type);
            StartCoroutine(coroutine);

            Destroy(collider.gameObject);
            return;
        }

        if (collider.tag == "Obstacle")
        {
            Obstacle obstacle = collider.gameObject.GetComponent<Obstacle>();

            player.speed = 0;
            player.angleSpeed = 0;
            spawner.StopAllCoroutines();

            ui.DisplayLoss();

            // you lose!
            return;
        }

        manager.PopTopOfPipe();
    }

}
