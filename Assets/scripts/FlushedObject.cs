using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushedObject : MonoBehaviour
{
    public Player player;
    void Update()
    {
        // all pickup items in the game are translating towards the viewer
        transform.Translate(0, 0, -1 * player.speed * player.speedModifier * Time.deltaTime);
    }
}
