using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellController : MonoBehaviour {

    public GameObject precastEffect;
    public GameObject spellEffect;

    public Vector3 spellOffset;
    public bool preMovesWithCaster = false;
    public bool postMovesWithCaster = false;

    public float cooldownTime;
    private float nextCast;

    public bool isBoodmagic;

    public float manaCost;

    //Determines where the mana cost is applied
    public bool isPreCostly = false;
    public bool isPostCostly = true;

    private GameObject castSpell;//spell effects in play that can be looked up by caster
    private bool isDisarmed = false;

    void Start() {
        nextCast = 0;
    }

    private void InstatiateSpell(GameObject caster, GameObject spell, bool movesWithCaster) {

        if(spell != null) {
            castSpell = (GameObject)Instantiate(spell, caster.transform.position, caster.transform.rotation);
            //set as parent if nessisary
            if (movesWithCaster) {
                castSpell.transform.parent = caster.transform;
            }

            //offset the spell relitive to caster rotation
            castSpell.transform.position += caster.transform.right   * spellOffset.x;
            castSpell.transform.position += caster.transform.up      * spellOffset.y;
            castSpell.transform.position += caster.transform.forward * spellOffset.z;
        }
    }

    private void CastWithCost(GameObject caster, GameObject spell, bool movesWithCaster) {
        StatsHandler casterStats = caster.GetComponent<StatsHandler>();

        //check for cooldown
        if (Time.time >= nextCast) {
            nextCast = Time.time + cooldownTime;//set nup next time for next cast

            //check if caster has enough mana and cast if they do, cast the spell
            if (casterStats.GetMana() >= manaCost) {

                //remove mana then cast
                casterStats.ChangeMana(-manaCost);
                InstatiateSpell(caster, spell, movesWithCaster);

            }
            else if (isBoodmagic && casterStats.GetHealth() + casterStats.GetMana() >= manaCost) {

                //remove mana and health then cast
                casterStats.ChangeHealth(-(manaCost - casterStats.GetMana()));//minus the extra needed
                casterStats.ChangeMana(-casterStats.GetMana());
                InstatiateSpell(caster, spell, movesWithCaster);
            }
        }
    }

    public void PressSpell(GameObject caster) {

        if (isPreCostly) {
            CastWithCost(caster, precastEffect, preMovesWithCaster);

        } else {
            InstatiateSpell(caster, precastEffect, preMovesWithCaster);
        }
    }

    public void ReleaseSpell(GameObject caster) {
        
        if(castSpell != null) {
            Destroy(castSpell);
            castSpell = null;
        }

        if (isDisarmed) {
            isDisarmed = false;

        } else {
            //cast release effects
            if (isPostCostly) {
                CastWithCost(caster, spellEffect, postMovesWithCaster);

            } else {
                InstatiateSpell(caster, spellEffect, postMovesWithCaster);
            }
        }
    }

    public void DisarmSpell() {
        
        if (castSpell != null) {
            Destroy(castSpell);
            castSpell = null;
        }

        isDisarmed = true;
    }
}
