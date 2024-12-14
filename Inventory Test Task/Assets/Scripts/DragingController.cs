using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragingController : MonoBehaviour
{
    public bool isDragging = false;
    public float dragSpeed = 10f;

    [SerializeField] private Outline outline;

    private Rigidbody _rb;
    private Camera _mainCamera;
    private Vector3 _offset;
    private Plane _dragPlane;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        isDragging = true;

        _dragPlane = new Plane(_mainCamera.transform.forward, transform.position);

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (_dragPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            _offset = transform.position - hitPoint;
        }

        _rb.useGravity = false;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (_dragPlane.Raycast(ray, out float distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance) + _offset;

                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dragSpeed);
                _rb.MovePosition(newPosition);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        _rb.useGravity = true;
    }

    public void SetFreezePosition(bool value)
    {
        _rb.freezeRotation = value;
       
    }
}
