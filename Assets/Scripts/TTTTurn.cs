using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static InformationTransfer;
using static PlayerController;

public class TTTTurn : MonoBehaviour{

    bool turnX = true;
    string[,] board = new string[3, 3];
    public Text label;
    public GameObject panel;
    private PlayerController script;
    private KeepInformation KeepScript;

    void Start(){
        //KeepScript = InformationTransfer.playerController.transform.parent.gameObject.GetComponent<KeepInformation>();
        //script = InformationTransfer.playerController.GetComponent<PlayerController>();
        for (int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                board[i, j] = "";
            }
        }
    }

    public void changeTurn(int box){
        int row = box / 3;
        int column = box % 3;
        turnX = !turnX;
        if (turnX){
            board[row, column] = "O";
            label.text = "Turno: X";
        }else{
            board[row, column] = "X";
            label.text = "Turno: O";
        }
        if(hasWon(row, column)){
            panel.SetActive(true);
            if (!turnX){
                panel.GetComponentInChildren<Text>().text = "Gana la X!";
            }else{
                panel.GetComponentInChildren<Text>().text = "Gana el O!";
            }
        }else if (isTie()){
            panel.SetActive(true);
            panel.GetComponentInChildren<Text>().text = "Empate!";
        }
    }

    public void returnToBoard(){
        //script.changeContinueState(panel.GetComponentInChildren<Text>().text == "Gana la X!");
        //script.changeTurn();
        //KeepScript.showOrHideObjects(true);
        //SceneManager.LoadScene("Board");
    }

    bool isTie(){
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                if(board[i, j] == ""){
                    return false;
                }
            }
        }
        return true;
    }
    
    bool hasWon(int row, int column){
        if(isHorizontal(row, column)){
            return true;
        }else if(isVertical(row, column)){
            return true;
        }else if(isDiagonal(row, column)){
            return true;
        }else if (isBackDiagonal(row, column)){
            return true;
        }
        return false;
    }

    bool isHorizontal(int row, int column){
        string mark = board[row, column];
        for(int i = 0; i < 3; i++){
            if(!(board[i, column] == mark)){
                return false;
            }
        }
        return true;
    }

    bool isVertical(int row, int column){
        string mark = board[row, column];
        for (int i = 0; i < 3; i++){
            if (!(board[row, i] == mark)){
                return false;
            }
        }
        return true;
    }

    bool isDiagonal(int row, int column){
        string mark = board[row, column];
        if (board[0, 0] == mark && board[1, 1] == mark && board[2, 2] == mark){
            return true;
        }
        return false;
    }

    bool isBackDiagonal(int row, int column){
        string mark = board[row, column];
        if (board[0, 2] == mark && board[1, 1] == mark && board[2, 0] == mark){
            return true;
        }
        return false;
    }

    public bool isXTurn(){
        return turnX;
    }

}
