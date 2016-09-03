using UnityEngine;
using UnityEngine.UI;

public class StatsMonitor : MonoBehaviour {

    public int playerNumber;
    private StatsHandler target;

    private Text text;

    void Start() {
        text = GetComponent<Text>();

        FetchPlayer();
    }

	void OnGUI () {
        if(target != null) {//if not dead

            text.text  = "Health: " + target.GetHealth() + "\n";
            text.text += "Mana: " + target.GetMana();

        } else { //if dead

            //try and find the player
            FetchPlayer();

            text.text  = "Health: Dead\n";
            text.text += "Mana: Dead";
        } 
	}

    void FetchPlayer() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players){
            PlayerInputHandler playerInput = player.GetComponent<PlayerInputHandler>();

            if(playerInput != null) { //players should always have a player input components

                if(playerInput.controllerNumber == playerNumber) {
                    target = player.GetComponent<StatsHandler>();

                    if(target == null) {
                        Debug.Log("ERROR - Player " + playerNumber + " does not have a StatsHandler!");
                    }
                }

            } else {
                Debug.Log("ERROR - Player does not have an input component!");
            }
        }
    }
}
