﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.LookAt(Camera.main.transform);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
    }
}