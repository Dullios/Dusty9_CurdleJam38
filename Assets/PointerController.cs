using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public float lagValue;

    [Header("Mouse Values")]
    Vector3 mousePos;
    Vector3 prevMousePos;
    Vector3 mouseDelta;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDelta = mousePos - prevMousePos;

        transform.position += mouseDelta * lagValue;
        transform.position = new Vector3(transform.position.x, transform.position.y, 10);

        if(lagValue <= 1 && lagValue >= 0.1)
            lagValue += (float)(Input.GetAxis("Mouse ScrollWheel"));

        prevMousePos = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.visible)
                Cursor.visible = false;
        }
    }
}
