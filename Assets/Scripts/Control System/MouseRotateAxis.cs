using UnityEngine;
using System.Collections;

public class MouseRotateAxis : Axis
{

    [SerializeField]
    float tolerance = 25;
    Vector2 previousMousePosition;
    Vector2 deltaMousePosition
    {
        get
        {
            return (Vector2)Input.mousePosition - previousMousePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _axisValue = 0;
        if (focus)
        {
			_axisValue = Mathf.Clamp(deltaMousePosition.x, (-1 * tolerance), tolerance)/tolerance;
        }
        previousMousePosition = Input.mousePosition;
    }
}
