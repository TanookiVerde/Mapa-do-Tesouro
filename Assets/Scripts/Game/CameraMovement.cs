using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
    public static int canBeMoved;
    private static bool startedDrag;

    private Vector3 lastMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 delta;

    public float[] yBounds = new float[2];
    public float[] xBounds = new float[2];

    private void Update()
    {
        if(canBeMoved == 0)
            DragCamera();
        ClampPosition();
    }
    private void DragCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            startedDrag = true;
            return;
        }
        if (Input.GetMouseButton(0) && startedDrag)
        {
            currentMousePosition = Input.mousePosition;
            delta = Camera.main.ScreenToWorldPoint(currentMousePosition) - Camera.main.ScreenToWorldPoint(lastMousePosition);
            delta.z = 0;
            transform.position -= delta;
        }
        else
        {
            startedDrag = false;
        }
        lastMousePosition = currentMousePosition;
    }
    public static void MoveTo(Vector2 position)
    {
        var pos = (Vector3)position - Vector3.forward * 10;
        Camera.main.transform.DOMove(pos, Globals.TIME_BETWEEN_TURNS);
    }
    private void ClampPosition()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, xBounds[0], xBounds[1]);
        pos.y = Mathf.Clamp(pos.y, yBounds[0], yBounds[1]);
        transform.position = pos;
    }
}
