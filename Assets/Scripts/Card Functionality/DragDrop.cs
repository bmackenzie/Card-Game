using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{

    private GameObject canvas;
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;


    private void Awake()
    {
        canvas = GameObject.Find("Main Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        //check if the object is being dragged, and moves the object to the mouse position
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //set object to a child of canvas so that appears over other elements visually
            transform.SetParent(canvas.transform, true);
        }    
    }

    //checks if object is colliding with something, if so make that collision the drop area
    //needs to check for a specific collider if the game has multiple things with collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DropZone")
        {
            isOverDropZone = true;
            dropZone = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }

    //set start position in case player drops the object somewhere they can't
    public void StartDrag()
    {
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    //set object transform to the dropzone if its over a collider, send it back to the start position if not
    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
        } 
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
        

    }
}
