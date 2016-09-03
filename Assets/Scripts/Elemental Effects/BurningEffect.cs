using UnityEngine;
using System.Collections;

public class BurningEffect : ElementalEffect {

    private const float dps = 10.0f;

    protected override void Start() {
        base.Start();

        gameObject.GetComponent<VisualEffectsController>().setFlames(true);
    }

    protected override void OnEffectEnd() {
        gameObject.GetComponent<VisualEffectsController>().setFlames(false);
    }

    protected override void setName() {
        effectName = "Burning Effect";
    }

    protected override void setLifetime() {
        lifetime = 1.0f;
    }

    protected override void Update () {

        stats.ChangeHealth(-dps * Time.deltaTime);
    }
}
