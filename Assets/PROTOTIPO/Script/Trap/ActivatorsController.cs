using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatorsController : MonoBehaviour
{
    public List<GameObject> activators;
    public GameObject trap;
    public bool enabledAllActivators;
  
    void Start()
    {      
        foreach (var activator in activators)
        {
            activator.GetComponent<Activators>().myTrap = trap;
        }      
    }

    public void EnabledActivators ()
    {
        foreach (var activator in activators)
        {
            activator.GetComponent<Activators>().active = true;
            activator.GetComponent<Activators>().isEnabled = true;
            enabledAllActivators = false;
        }
    }

    public void DisabledActivators()
    {
        foreach (var activator in activators)
        {
            activator.GetComponent<Activators>().active = false;
            activator.GetComponent<Activators>().isEnabled = false;
            activator.GetComponent<Activators>().mr.material.color = Color.red;
        }
    }

    void Update()
    {
        if (enabledAllActivators)
        {
            EnabledActivators();
        }
    }
}
