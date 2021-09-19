using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    private bool _dragging = false;

	public BoxCollider2D boxCollider;

    // Update is called once per frame
    void Update()
    {
		Bounds areaBounds = boxCollider.bounds;

		if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
        if (Input.GetMouseButtonUp(0))
        {
            _dragging = false;
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
           
            //Uncomment before build
            
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
            /*
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                return;
            }
            */
            if (Vector2.Distance(touchStart, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.2f)
            {
                _dragging = true;
            }
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_dragging)
            {
                Camera.main.transform.position += direction;
            }
        }
		
		zoom(Input.GetAxis("Mouse ScrollWheel"));

		Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, areaBounds.min.x, areaBounds.max.x),
													 Mathf.Clamp(Camera.main.transform.position.y, areaBounds.min.y, areaBounds.max.y),
													 Camera.main.transform.position.z);
	}

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

}