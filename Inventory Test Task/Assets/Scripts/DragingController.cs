using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragingController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 offset;
    private Plane dragPlane;

    public bool isDragging = false;
    public float dragSpeed = 10f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;

        dragPlane = new Plane(mainCamera.transform.forward, transform.position);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            offset = transform.position - hitPoint;
        }

        rb.useGravity = false;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance) + offset;

                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dragSpeed);
                rb.MovePosition(newPosition);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        rb.useGravity = true;
    }

    public void SetFreezePosition(bool value)
    {
        rb.freezeRotation = value;
       
    }
}
