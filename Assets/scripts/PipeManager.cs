using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PipeManager : MonoBehaviour
{
    public static Single PipeLength = 80;

    [SerializeField] private Player player;
    [SerializeField] private GameObject pipeObject;

    [SerializeField] private Int32 numPipes;

    [SerializeField] public Int32 topOfPipe = 0;
    [SerializeField] public Int32 bottomOfPipe = 4;
    [SerializeField] public GameObject[] pipeStack;

    void Awake()
    {
        topOfPipe = 0;
        bottomOfPipe = numPipes - 1;

        // stack consists of 5 pipes
        pipeStack = new GameObject[numPipes];

        for (Int32 p = 0; p < numPipes; p++)
        {
            // instantiate the original pipe objects and keep references to them in an array
            pipeStack[p] = Instantiate(pipeObject, new Vector3(0, 0, PipeLength * p), Quaternion.identity, transform);
            pipeStack[p].GetComponent<Pipe>().player = player;
        }
    }

    public void Reset()
    {
        // reset indices
        topOfPipe = 0;
        bottomOfPipe = numPipes - 1;

        for (Int32 p = 0; p < numPipes; p++)
        {
            Destroy(pipeStack[p]);
            // instantiate the original pipe objects and keep references to them in an array
            pipeStack[p] = Instantiate(pipeObject, new Vector3(0, 0, PipeLength * p), Quaternion.identity, transform);
            pipeStack[p].GetComponent<Pipe>().player = player;
        }
    }

    public Pipe TopOfPipe()
    {
        return pipeStack[topOfPipe].GetComponent<Pipe>();
    }

    public Pipe BottomOfPipe()
    {
        return pipeStack[bottomOfPipe].GetComponent<Pipe>();
    }

    public void PopTopOfPipe()
    {
        // destroy pipe at the bottom (destroys all children)
        Destroy(pipeStack[topOfPipe]);

        // instantiate a new pipe object at the top
        pipeStack[topOfPipe] = Instantiate(pipeObject, transform);
        pipeStack[topOfPipe].GetComponent<Pipe>().player = player;

        // translate the old top of pipe to the bottom of pipe's current position
        pipeStack[topOfPipe].transform.Translate(pipeStack[bottomOfPipe].transform.position + new Vector3(0.0f, 0.0f, PipeLength));

        // update indices to the top and bottom of the pipe
        bottomOfPipe = (bottomOfPipe + 1) % numPipes;
        topOfPipe = (topOfPipe + 1) % numPipes;
    }
}
