using UnityEngine;
using System.Collections.Generic;

public abstract class Targeter : MonoBehaviour {
    
    public abstract List<GameObject> GetTargets();
}
