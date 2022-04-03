using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutSceneScriptedEvent : MonoBehaviour {

    public bool eventTriggered;

    public bool GetEventTriggered() {
        return eventTriggered;
    }
}
