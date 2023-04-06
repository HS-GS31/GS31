using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    private bool isLeft;
    private Vector3 position;
    private Vector3 rotation;

    public Route(bool isLeft, Vector3 position, Vector3 rotation)
    {
        this.isLeft = isLeft;
        this.position = position;
        this.rotation = rotation;
    }
}
