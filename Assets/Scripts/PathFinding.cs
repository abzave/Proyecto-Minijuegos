using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerController;
using static InformationTransfer;
using static BoardGraph;
using static System.Int32;

public class PathFinding : MonoBehaviour{

    private SelectVertex playerScript;
    private Dropdown combobox;
    private Text label;
    public GameObject panel;

    void Start(){
        updatePlayer();
        combobox = panel.GetComponentsInChildren<Dropdown>()[0];
        label = panel.GetComponentsInChildren<Text>()[1];
    }
    
    public List<string> find(int value){
        int vertex = getActualVertex();
        List<string> path = new List<string>();
        for(int i = 0; i < 26; i++){
            int weight = BoardGraph.getWeight(vertex, i);
            if(weight != -1 && weight == value && !isVisited(i)){
                path.Add(i.ToString());
            }
        }
        return path;
    }

    void updatePlayer(){
        PlayerController controllerScript = gameObject.GetComponent<PlayerController>();
        int turn = controllerScript.getTurn();
        GameObject player = InformationTransfer.getPlayer(turn);
        playerScript = player.GetComponent<SelectVertex>();
    }

    void selectNewBox(int value){
        updatePlayer();
        showPanel(find(value));
    }

    void showPanel(List<string> boxes){
        panel.SetActive(true);
        int elements = boxes.Capacity;
        if (elements != 0){
            label.text = "";
            combobox.gameObject.SetActive(true);
            combobox.ClearOptions();
            combobox.AddOptions(boxes);
        }else if (hasPosibility()){
            combobox.gameObject.SetActive(false);
            label.text = "El valor no es suficiente para avanzar";
        }else{
            combobox.gameObject.SetActive(false);
            label.text = "No hay posbilidades de avanzar :(";
            playerScript.restart();
        }
    }

    int getActualVertex(){
        return playerScript.getActualVertex();
    }

    bool isVisited(int vertex){
        return playerScript.isVisited(vertex);
    }

    bool hasPosibility(){
        int vertex = getActualVertex();
        for (int i = 0; i < 26; i++){
            int weight = BoardGraph.getWeight(vertex, i);
            if (weight != -1 && !isVisited(i)){
                return true;
            }
        }
        return false;
    }

    public void disablePanel(){
        if (combobox.gameObject.activeSelf){
            int vertex = 0;
            TryParse(combobox.options[combobox.value].text, out vertex);
            playerScript.goToVertex(vertex);
        }
        panel.SetActive(false);
        if(label.text == "El valor no es suficiente para avanzar"){
            gameObject.SendMessage("changeTurn", SendMessageOptions.RequireReceiver);
        }
    }

}
