﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warshipmiddle : MonoBehaviour
{
    public GameObject badguyPrefab;
    private Rigidbody rb;
    public int badguynumber = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (badguynumber < 10)
        {

            GameObject badguy = Instantiate(badguyPrefab, transform.position, transform.rotation);
            badguynumber = badguynumber + 250;
        }
        else
        {
            badguynumber = badguynumber - 1;
        }
    }
}