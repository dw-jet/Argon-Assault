using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    public void Start()
    {
        Destroy(gameObject, 5f);
    }
}
