using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public GameObject canvas;
    private GameObject zoomCard;

    public void Awake()
    {
        canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        //gameobject refers to the object this script is attached to
        zoomCard = Instantiate(gameObject, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 250), Quaternion.identity);
        zoomCard.transform.SetParent(canvas.transform, false);
        //sets the layer of the zoom card to something else so it can't collide with anything on the game layer
        zoomCard.layer = LayerMask.NameToLayer("Zoom");

        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(240, 344);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}


