using System;
using UnityEditor;
using UnityEngine;

public class Pipe : FlushedObject
{
    public Single currentOffset;

    void Awake()
    {
        currentOffset = PipeManager.PipeLength * -0.5f;
    }
}
