using UnityEngine;
using System.Collections;

public class ElementalEffect : MonoBehaviour {

    protected string effectName;

    private float startTime;
    protected float lifetime;

    protected StatsHandler stats;

    protected int stacks;

    public ElementalEffect() {
        Start();
    }

	// Use this for initialization
	protected virtual void Start () {
        setName();
        startTime = Time.time;
        stacks = 1;
        setLifetime();
        stats = GetComponent<StatsHandler>();
	}

    protected virtual void setName() {//Override this to set the name
        effectName = "Default Effect";
    }

    protected virtual void setLifetime() {//Override this to set the lifetime
        lifetime = 5.0f;
    }

    protected virtual void OnEffectEnd(){ }

    // Update is called once per frame
    protected virtual void Update () {
        stats.ChangeHealth(-1 * stacks * Time.deltaTime);//generic effect
	}

    public string getName() {
        return effectName;
    }

    public bool isDone() {
        if(Time.time > startTime + lifetime) {
            if (stacks < 1) {
                OnEffectEnd();
                return true;

            } else {
                stacks -= 1;
                startTime += lifetime;

                return isDone();
            }
        } else {
            return false;
        }
    }

    public void addToStack() {
        stacks += 1;
    }

}
