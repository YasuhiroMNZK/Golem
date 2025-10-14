﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTrigger : TriggerBase
{
    [SerializeField] private string keyname;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(keyname))
        {
            action.Invoke();
        }
    }
}
