using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static InformationTransfer;

public class MemoryPath : MonoBehaviour{

    private bool[,] board = new bool[8, 3];
    private int attemps = 5;
    private bool[] valids = new bool[8];
    private PlayerController script;
    private KeepInformation KeepScript;
    public GameObject panel;
    public Text label;

    void Start(){
        KeepScript = InformationTransfer.playerController.transform.parent.gameObject.GetComponent<KeepInformation>();
        script = InformationTransfer.playerController.GetComponent<PlayerController>();
        for (int i = 0; i < 8; i++){
            board[i, Random.Range(0, 3)] = true;
        }
    }

    public bool isCorrect(int row, int column){
        return board[row, column];
    }

    public void reset(){
        attemps--;
        label.text = attemps.ToString();
        if (attemps == 0){
            panel.GetComponentInChildren<Text>().text = "Has perdido :(";
            panel.SetActive(true);
        }
        for (int i = 0; i < 8; i++){
            valids[i] = false;
        }
        StartCoroutine(wait());
    }

    public void verifyWin(int row){
        valids[row] = true;
        for (int i = 0; i < 8; i++){
            if (!valids[i]){
                return;
            }
        }
        panel.GetComponentInChildren<Text>().text = "Has ganado!";
        panel.SetActive(true);
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(1);
        foreach(Transform child in transform){
            child.gameObject.SendMessage("resetColor", SendMessageOptions.RequireReceiver);
        }
    }

    public void disablePanel(){
        panel.SetActive(false);
        returnToBoard(panel.GetComponentInChildren<Text>().text == "Has ganado!");
    }

    public void returnToBoard(bool won){
        script.changeContinueState(won);
        script.changeTurn();
        KeepScript.showOrHideObjects(true);
        SceneManager.LoadScene("Board");
    }

}
