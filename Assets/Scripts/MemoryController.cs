using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static InformationTransfer;

public class MemoryController : MonoBehaviour{

    private bool isActive = false;
    private int[] pairsCount = new int[2] { 0, 0 };
    private string[] lastCard = new string[2] { "", "" };
    private Text[] lastObject = new Text[2] { null, null };
    private int turn = 0;
    private PlayerController script;
    private KeepInformation KeepScript;
    public Text turnLabel;
    public Text player1Pairs;
    public Text player2Pairs;
    public GameObject winPanel;

    void Start(){
        //KeepScript = InformationTransfer.playerController.transform.parent.gameObject.GetComponent<KeepInformation>();
        //script = InformationTransfer.playerController.GetComponent<PlayerController>();
    }
    
    public bool isOtherCardEnable(){
        return isActive;
    }

    bool hasWon(){
        return (pairsCount[0] + pairsCount[1]) == 9;
    }

    public bool isPair(string value){
        bool pair = lastCard[turn] == value;
        if (pair){
            pairsCount[turn]++;
            if(turn == 0){
                player1Pairs.text = "Jugador 1: " + pairsCount[turn];
            }else{
                player2Pairs.text = "Jugador 2: " + pairsCount[turn];
            }
        }
        return pair;
    }

    public void cardClicked(Text card){
        if (!isActive){
            isActive = true;
            lastCard[turn] = card.text;
            lastObject[turn] = card;
            return;
        }
        isActive = false;
        lastCard[turn] = "";
        lastObject[turn] = null;
        changeTurn();
    }

    public Text getLastObject() {
        return lastObject[turn];
    }

    void win(){
        winPanel.SetActive(true);
        if (canContinue()){
            winPanel.GetComponentInChildren<Text>().text = "Gana el jugador: 1";
        }else{
            winPanel.GetComponentInChildren<Text>().text = "Gana el jugador: 2";
        }
    }

    public void disablePanel(){
        winPanel.SetActive(false);
        returnToBoard(canContinue());
    }

    void returnToBoard(bool won){
        //script.changeContinueState(won);
        //script.changeTurn();
        //KeepScript.showOrHideObjects(true);
        //SceneManager.LoadScene("Board");
    }

    bool canContinue(){
        return pairsCount[0] > pairsCount[1];
    }

    void changeTurn(){
        if (hasWon()){
            win();
        }else if (turn == 0){
            turn = 1;
            turnLabel.text = "Jugador: 2";
        }else{
            turn = 0;
            turnLabel.text = "Jugador: 1";
        }
    }

}
