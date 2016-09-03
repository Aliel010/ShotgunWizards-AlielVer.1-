using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    private GameObject shooter;

    void Start() {
        //null intilizse shooter
        shooter = null;
    }

    public void setShooter(GameObject shooter) {
        this.shooter = shooter;
    }

    public GameObject getShooter() {
        return shooter;
    }
}
