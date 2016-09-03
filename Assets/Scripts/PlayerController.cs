using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public PlayerInputHandler playerIn;
    public ShotgunController shotgun;

    public SpellController primarySpell;
    public SpellController secondarySpell;

    private CharacterController characterController;

    private StatsHandler myStats;

    bool isPrimaryCasting = false;
    bool isSecondaryCasting = false;


    void Start () {

        characterController = GetComponent<CharacterController>();
        if (characterController == null) {
            Debug.Log("Character Controller not found!");
        }

        myStats = GetComponent<StatsHandler>();
        if(myStats == null) {
            Debug.Log("Stats Handler not found!");
        }
    }

    void Update() {

        //Spell Stuff
        if (!isSecondaryCasting) {
            isPrimaryCasting = playerIn.GetAxisAsButton("Primary Spell") > 0;
        }

        if (!isPrimaryCasting) {
            isSecondaryCasting = playerIn.GetButton("Secondary Spell");
        }

        if (playerIn.GetAxisAsButtonDown("Primary Spell") < 0 && !(isPrimaryCasting || isSecondaryCasting)) {
            shotgun.fire();
        }

        if (playerIn.GetAxisAsButtonDown("Primary Spell") > 0 && !isSecondaryCasting) {
            primarySpell.PressSpell(gameObject);
        }
        if (playerIn.GetAxisAsButtonUp("Primary Spell") > 0   && !isSecondaryCasting) {
            primarySpell.ReleaseSpell(gameObject);
        }

        if (playerIn.GetButtonDown("Secondary Spell") && !isPrimaryCasting) {
            secondarySpell.PressSpell(gameObject);
        }
        if(playerIn.GetButtonUp("Secondary Spell")    && !isPrimaryCasting) {
            secondarySpell.ReleaseSpell(gameObject);
        }

        if (playerIn.GetButtonDown("Disarm")) {

            if(playerIn.GetAxisAsButton("Primary Spell") > 0) {

                primarySpell.DisarmSpell();

            } else if(playerIn.GetButton("Secondary Spell")) {

                secondarySpell.DisarmSpell();
            }
        }

        if (playerIn.GetButtonDown("Fire")) {
            myStats.AddElementalEffect(gameObject.AddComponent<ManaRegenEffect>());
        }

        //Movement
        characterController.Move(playerIn.GetMoveAxis() * myStats.moveSpeed * Time.deltaTime);

        //Look Direction
        Quaternion rotation = new Quaternion();
        Vector3 axisIn = playerIn.GetLookAxis();
        
        if(axisIn.magnitude > 0) {
            //Debug.Log(axisIn.magnitude + ": (" + axisIn.x + ", " + axisIn.y + ", " + axisIn.z + ")" ); //Debug TODO: Remove
            rotation.SetLookRotation(axisIn.normalized);
            transform.rotation = rotation;
        }
        
        if (!myStats.IsAlive()) {
            Destroy(gameObject);
        }
    }
}
