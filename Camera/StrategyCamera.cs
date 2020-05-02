// see https://www.youtube.com/watch?v=rnqF6S7PfFA Matt Gambell "Game Dev Guide"
// Video and presumably the original version of this script released under CC Attribution license

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// rig the camera to a parent object that has this script as a component
public class StrategyCamera : MonoBehaviour
{
public Transform cameraTransform;
  
public float fastSpeed = 3f;
public float slowSpeed = 1f;
public float moment = 5f;
public Vector3 zoomVector = new Vector3( 10, 10, 0 );


float speed;

Vector3 newPosition;
Quaternion newRotation;
Vector3 newZoom;

Vector3 dragStartPosition;
Vector3 dragCurrentPosition;
Vector3 rotateStartPosition;

Transform followTransform;


  public void setFollow( Transform t )
  {
    followTransform = t;
  }


  void Start()
  {
    newPosition = transform.position;
    newRotation = transform.rotation;
    newZoom = cameraTransform.localPosition;
  }

  void Update()
  {
    HandleKeyboard();
    HandleMouse();


    if (null != followTransform)
      transform.position = followTransform.position;
    else
      transform.position = Vector3.Lerp( transform.position, newPosition, Time.deltaTime * moment );

    transform.rotation = Quaternion.Lerp( transform.rotation, newRotation, Time.deltaTime * moment );
    cameraTransform.localPosition = Vector3.Lerp( cameraTransform.localPosition, newZoom, Time.deltaTime * moment );
  }


  
  void HandleKeyboard()
  {
    speed = slowSpeed;

    if (Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift ))
      speed = fastSpeed;

    if (Input.GetKeyDown(KeyCode.Escape)) followTransform = null;

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
      newPosition += transform.forward * speed;

    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
      newPosition -= transform.right * speed;

    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.RightArrow))
      newPosition += transform.right * speed;

    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.DownArrow))
      newPosition -= transform.forward * speed;

    if (Input.GetKey(KeyCode.Q))
      newRotation *= Quaternion.Euler( Vector3.up * speed );

    if (Input.GetKey(KeyCode.E))
      newRotation *= Quaternion.Euler( Vector3.down * speed );
      
    if (Input.GetKey(KeyCode.R))
      newZoom += speed * zoomVector;

    if (Input.GetKey(KeyCode.F))
      newZoom -= speed * zoomVector;
  }


  void HandleMouse()
  {
    newZoom += Input.mouseScrollDelta.y * zoomVector * speed;
    
    if (Input.GetMouseButtonDown( 0 ))
    {
      Plane plane = new Plane( Vector3.up, Vector3.zero );
      Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
      float entry;
    
      if (plane.Raycast( ray, out entry ))
        dragStartPosition = ray.GetPoint( entry );
    }
    
    if (Input.GetMouseButton( 0 ))
    {
      Plane plane = new Plane( Vector3.up, Vector3.zero );
      Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
      float entry;
    
      if (plane.Raycast( ray, out entry ))
        dragCurrentPosition = ray.GetPoint( entry );
        
      newPosition = transform.position + dragStartPosition - dragCurrentPosition;
    }
    
    if (Input.GetMouseButtonDown( 1 ))
      rotateStartPosition = Input.mousePosition;
      
    if (Input.GetMouseButton( 1 ))
    {
      newRotation *= Quaternion.Euler( Vector3.up * (Input.mousePosition.x - newPosition.x) * 0.2f * speed );
      rotateStartPosition = Input.mousePosition;
    }
  }

}

