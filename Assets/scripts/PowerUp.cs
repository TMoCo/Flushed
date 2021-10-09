using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PowerUp : PickUp
{
    public enum Type : Int32
    {
        Time,
        DoubleScore
    };

    public Type type;

    void Start()
    {
        GetComponent<BoxCollider>().tag = "PowerUp";
    }

    public IEnumerator ApplyEffect(Player player, Type type)
    {
        switch(type)
        {
            case Type.Time:
                {
                    player.speed *= 0.5f;
                    yield return new WaitForSeconds(30.0f);
                    player.speed *= 2.0f;
                    break;
                }
            case Type.DoubleScore:
                {
                    // save score modifier
                    Int32 oldModfier = player.scoreModifier;
                    player.scoreModifier = oldModfier * 2;
                    yield return new WaitForSeconds(30.0f);
                    player.scoreModifier -= oldModfier; // because we doubled modifier, just subtract to keep any previous score modifier
                    break;
                }
        }
    }
}
