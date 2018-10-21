﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
    public int HitPoints { get; protected set; }
    public int Mass { get; protected set; }
    public Target ModuleTarget { get; protected set; }
    public ModuleType ModuleType { get; protected set; }
    public Action Action { get; protected set; }

    //virtual GetAction
    public virtual Action GetAction()
    {
        return null;
    }

    //set action of module
    public void SetAction(Action action)
    {
        this.Action = action;
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

//used to name module
public enum ModuleType
{
    shortRange, longRange, heal, slow,
}

//used for choosing target
public enum Target
{
    ally, 
    enemy,
    self,
    everyone,
}