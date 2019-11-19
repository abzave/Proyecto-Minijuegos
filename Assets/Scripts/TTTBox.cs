using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TTTTurn;

public class TTTBox : MonoBehaviour{
    public int id;
    public Text label;
    public GameObject controller;
    private bool ocupated = false;
    private TTTTurn script;

    void Start(){
        script = (TTTTurn)controller.GetComponent(typeof(TTTTurn));
    }

    public void OnMouseDown(){
        if (!ocupated){
            ocupated = true;
            if (script.isXTurn()){
                label.text = "X";
            }else{
                label.text = "O";
            }
            script.changeTurn(id);
        }
    }

}
