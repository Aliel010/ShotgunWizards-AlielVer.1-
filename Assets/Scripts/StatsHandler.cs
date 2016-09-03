using UnityEngine;
using System.Collections.Generic;

public class StatsHandler : MonoBehaviour {

    public float moveSpeed = 10.0f;

    public float maxHealth = 100.0f;
    public float maxMana = 100.0f;

    private float health;
    private float mana;

    public List<ElementalEffect> effects;

	void Start () {
        health = maxHealth;
        mana   = maxMana;

        effects = new List<ElementalEffect>();
    }
	
	void Update () {
        cleanupEffects();

        moveSpeed = Mathf.Max(0, moveSpeed);
	}

    private void cleanupEffects() {
        List<ElementalEffect> toDelete = new List<ElementalEffect>();

        foreach(ElementalEffect effect in effects) {
            if (effect.isDone()) {
                toDelete.Add(effect);
            }
        }

        foreach(ElementalEffect effect in toDelete) {
            effects.Remove(effect);
            Destroy(effect);
        }
    }

    public float GetHealth() {
        return health;
    }

    public float GetMana() {
        return mana;
    }

    public bool IsAlive() {
        return health > 0;
    }

    public void ChangeHealth(float dHealth) {

        health += dHealth;

        //Bounding Check
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void ChangeMana(float dMana) {

        mana += dMana;

        //Bounding Check
        mana = Mathf.Clamp(mana, 0, maxMana);
    }

    public void AddElementalEffect(ElementalEffect effect) {
        bool hasBeenAdded = false;

        foreach(ElementalEffect focus in effects) {

            if (focus.getName().Equals(effect.getName())) {

                focus.addToStack();
                hasBeenAdded = true;
                Destroy(effect);
                break;
            }
        }

        if (!hasBeenAdded) {//Add if not found
            effects.Add(effect);
        }
    }
}
