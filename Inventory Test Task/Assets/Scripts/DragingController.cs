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

    // Enabeling outline
    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    // Disable outline
    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    // Begin drag an item
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

    // On draging an item
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

    // End drag an item
    private void OnMouseUp()
    {
        isDragging = false;

        _rb.useGravity = true;
    }

    /// <summary>
    /// Sets freeze position on rigitbody of an item
    /// </summary>
    /// <param name="value">True to freeze, false to unfreeze</param>
    public void SetFreezePosition(bool value)
    {
        _rb.freezeRotation = value;
       
    }
}
