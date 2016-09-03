using UnityEngine;
using System.Collections;

public class ManaRegenEffect : ElementalEffect {

	protected override void setName() {
        effectName = "Mana Regen Full";
    }

    protected override void setLifetime() {//Override this to set the lifetime
        lifetime = 10.0f;
    }

    protected override void Update() {
        stats.ChangeMana(10 * stacks * Time.deltaTime);
    }
}
