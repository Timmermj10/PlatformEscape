using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEvent
{
    // The room we should be manipulating
    public Vector2 room;

    // The location of which to rotate around
    public Vector3 point_of_rotation;

    // The axis we will rotate about
    public Vector3 axis_of_rotation;



    public RotateEvent(Vector2 _new_room, Vector3 _new_point, Vector3 _new_axis)
    {
        // Set all the values needed

        // Room
        room = _new_room;

        // Point
        point_of_rotation = _new_point;

        // Axis
        axis_of_rotation = _new_axis;
    }
}
