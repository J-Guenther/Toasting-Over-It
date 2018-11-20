using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        print("TRIGGERED!");
        triggered = true;
    }
}
