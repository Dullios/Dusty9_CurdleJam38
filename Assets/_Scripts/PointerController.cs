using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public float lagValue;

    [Header("Mouse Values")]
    public float mouseXBound;
    public float mouseYBound;

    float mouseXVel;
    float mouseYVel;

    [Header("Raycast Values")]
    public int raycastRange;
    public LayerMask groceryMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseXVel = Input.GetAxis("Mouse X");
        mouseYVel = Input.GetAxis("Mouse Y");

        transform.Translate(mouseXVel * lagValue, mouseYVel * lagValue, 0);

        if(lagValue <= 1 && lagValue >= 0.1)
            lagValue += (float)(Input.GetAxis("Mouse ScrollWheel"));
        if (lagValue < 0.1)
            lagValue = 0.1f;
        if (lagValue > 1)
            lagValue = 1;

        ScreenBounds();

        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.visible)
                Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            CastRay();
        }
    }

    void CastRay()
    {
        Vector3 mouseViewport = Camera.main.WorldToViewportPoint(transform.position);
        Ray cameraRay = Camera.main.ViewportPointToRay(mouseViewport);

        RaycastHit cameraHit;

        Debug.DrawRay(cameraRay.origin, cameraRay.direction * raycastRange, Color.green, 1f);

        if (Physics.Raycast(cameraRay, out cameraHit, raycastRange, groceryMask))
        {
            Destroy(cameraHit.transform.gameObject);
        }
    }

    private void ScreenBounds()
    {
        if(transform.position.x < -mouseXBound)
            transform.position = new Vector3(-mouseXBound, transform.position.y, transform.position.z);
        if (transform.position.x > mouseXBound)
            transform.position = new Vector3(mouseXBound, transform.position.y, transform.position.z);
        if (transform.position.y < -mouseYBound)
            transform.position = new Vector3(transform.position.x, -mouseYBound, transform.position.z);
        if (transform.position.y > mouseYBound)
            transform.position = new Vector3(transform.position.x, mouseYBound, transform.position.z);
    }
}
