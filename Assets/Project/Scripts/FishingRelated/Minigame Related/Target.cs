using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour, IBreakable
{
    public void BreakObject()
    {
       Destroy(gameObject);
    }
}
