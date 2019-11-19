using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InformationTransfer;
using static DicesController;

public class PlayerController : MonoBehaviour{

    public Text label;
    public GameObject diceController;
    private int turn = 0;
    private bool[] canContinue = new bool[6];
    private DicesController script;

    void Start(){
        for(int i = 0; i < InformationTransfer.amountOfPlayers; i++){
            GameObject player = InformationTransfer.getPlayer(i);
            player.AddComponent<SelectVertex>();
            SelectVertex vertex = (SelectVertex)player.GetComponent(typeof(SelectVertex));
            vertex.id = i;
            Destroy(player.GetComponent("Interaction"));
            InformationTransfer.overwritePlayer(player, i);
            canContinue[i] = true;
        }
        label.text = "Jugador: " + (InformationTransfer.turnOrder[0] + 1);
        InformationTransfer.quitUnusedPlayer();
        InformationTransfer.playerController = gameObject;
        script = (DicesController)diceController.GetComponent(typeof(DicesController));
    }

    void diceRolled(int diceValue){
        diceController.SendMessage("changeRollState", false, SendMessageOptions.RequireReceiver);
        gameObject.SendMessage("selectNewBox", diceValue, SendMessageOptions.RequireReceiver);
    }

    public void changeTurn(){
        turn++;
        if(turn == InformationTransfer.amountOfPlayers){
            turn = 0;
        }
        label.text = "Jugador: " + (InformationTransfer.turnOrder[turn] + 1);
        script.changeRollState(canContinue[turn]);
        if (!canContinue[turn]){
            InformationTransfer.getPlayer(turn).SendMessage("replay", SendMessageOptions.RequireReceiver);
        }
    }

    public void changeContinueState(bool state){
        canContinue[turn] = state;
    }

    public int getTurn(){
        return turn;
    }

}
