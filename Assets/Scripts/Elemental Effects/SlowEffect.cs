using UnityEngine;
using System.Collections;

public class SlowEffect : ElementalEffect {

    private int lastStacks;
    private const float slowPerStack = 0.08f;

    override protected void Start() {
        base.Start();

        stats.moveSpeed -= slowPerStack;
        lastStacks = stacks;
    }

    ~SlowEffect() {//when distroyed
        if (Application.isPlaying) {
            stats.moveSpeed += slowPerStack*stacks;
        }
    }

    protected override void Update() {
        isDone();

        Debug.Log("Stacks: " + stacks + " delta: " + (stacks - lastStacks));

        stats.moveSpeed -= slowPerStack * (stacks - lastStacks);

        lastStacks = stacks;
    }

    protected override void setName() {
        effectName = "Slow Down";
    }

    protected override void setLifetime() {//Override this to set the lifetime
        lifetime = 0.1f;
    }

}
