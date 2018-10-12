﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

    [SerializeField] Color highlightColor = Color.yellow;

    Color baseColor;
    Renderer renderer;
    Renderer[] childRenderers;
    Queue<Color> childColors;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        childRenderers = GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        Highlight();
    }

    private void OnMouseExit()
    {
        RemoveHighlight();
    }

    public void Highlight()
    {
        if (renderer != null)
        {
            baseColor = renderer.material.color;
            renderer.material.color = highlightColor;
        }
        else if (childRenderers != null)
        {
            childColors = new Queue<Color>();
            foreach (Renderer r in childRenderers)
            {
                childColors.Enqueue(r.material.color);
                r.material.color = highlightColor;
            }
        }
        //Send a message to other scripts on object to begin animation
        SendMessage("StartHighlightAnimation");
    }
    private void RemoveHighlight()
    {
        if (renderer != null)
        {
            renderer.material.color = baseColor;
        }
        else if (childRenderers != null)
        {
            foreach (Renderer r in childRenderers)
            {
                r.material.color = childColors.Dequeue();
            }
        }
        //Send a message to other scripts on object to stop animation
        SendMessage("StopHighlightAnimation");
    }

    //Allows other scripts to send a message indicating the base color has changed
    private void UpdateBaseColor(Color color)
    {
        baseColor = color;
    }

    //Allows other scripts to send a message indicating the base colors of the children has changed
    private void UpdateBaseColorQueue(Queue<Color> colors)
    {
        childColors = colors;
    }
}
