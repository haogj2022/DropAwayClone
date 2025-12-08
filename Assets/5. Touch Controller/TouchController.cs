using System;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public static TouchController Instance;

    public Action<Vector2> OnStartTouching;
    public Action<Vector2> OnTouching;
    public Action<Vector2> OnStopTouching;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);

        if (Input.GetMouseButtonDown(0))
        {
            OnStartTouching(mousePos);
        }

        if (Input.GetMouseButton(0))
        {
            OnTouching(mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnStopTouching(mousePos);
        }
    }
}
