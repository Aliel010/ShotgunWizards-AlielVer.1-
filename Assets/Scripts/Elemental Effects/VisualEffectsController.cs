using UnityEngine;
using System.Collections;

public class VisualEffectsController : MonoBehaviour {

    public ParticleSystem flames;

    public void setFlames(bool flamesOn){

        if (flamesOn){
            flames.Play();

        } else {
            flames.Stop();
        }
         
    }
}
