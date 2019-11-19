using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static KeepInformation;
using static PlayerController;

public class NodeGeneration : MonoBehaviour{

    private static List<string> boxes = new List<string>() { "TicTacToe", "MemoryPath",
        "Memory", "LetterSoup", "GuessWho", "CatchTheCat", "6", "7", "8", "9", "10", "11", "12", 
        "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"};
    private bool[] visitedBy = new bool[6];
    private Text label = null;
    private string game;
    private KeepInformation KeepScript;
    private PlayerController playerScript;
    public GameObject playerController;

    void Start(){
        KeepScript = transform.parent.transform.parent.gameObject.GetComponent<KeepInformation>();
        playerScript = playerController.GetComponent<PlayerController>();
        for (int i = 0; i < 6; i++){
            visitedBy[i] = false;
        }
        label = gameObject.GetComponentInChildren<Text>();
        int content = Random.Range(0, boxes.Count);
        game = boxes[content];
        boxes.RemoveAt(content);
    }

    public void visit(int player, bool game){
        if (!visitedBy[player]){
            visitedBy[player] = true;
            label.text += (player + 1) + " ";
        }
        if (game){
            loadGame();
        }
    }

    public void quitPlayer(int player){
        visitedBy[player] = false;
        string newText = "";
        for(int i = 0; i < label.text.Length; i++){
            if(label.text[i] == (player + 1).ToString()[0]){
                i += 2;
                continue;
            }
            newText += label.text[i];
        }
        label.text = newText;
    }

    public bool playerHasVisited(int player){
        return visitedBy[player];
    }

    public void loadGame(){
        if (game == "TicTacToe" || game == "LetterSoup" || game == "MemoryPath" || game == "Memory" || game == "CatchTheCat" || game == "Bombermario" || game == "GuessWho"){
            SceneManager.LoadScene(game);
            KeepScript.showOrHideObjects(false);
        }else{
            playerScript.changeTurn();
        }
    }

}
