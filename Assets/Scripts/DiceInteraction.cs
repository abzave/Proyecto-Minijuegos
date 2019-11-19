using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DicesController;

public class DiceInteraction : MonoBehaviour{

    public GameObject playerChangeReciever;
    public GameObject dicesController;
    private DicesController script;

    void Start(){
        script = dicesController.GetComponent<DicesController>();
    }

    void OnMouseDown(){
        if (script.canRoll){
            int value = script.roll();
            playerChangeReciever.SendMessage("diceRolled", value, SendMessageOptions.RequireReceiver);
        }
    }
}
