using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InformationTransfer;

public class Interaction : MonoBehaviour{

    private bool isIn = false;
    public GameObject messageSender;

    void OnMouseOver(){
        if (!isIn){
            transform.Translate(Vector3.back * 0.1f);
            isIn = true;
        }
    }

    void OnMouseExit(){
        transform.Translate(Vector3.back * -0.1f);
        isIn = false;
    }

    void OnMouseDown(){ 
        InformationTransfer.addPlayer(gameObject);
        messageSender.SendMessage("nextTurn", SendMessageOptions.RequireReceiver);
        transform.Translate(Vector3.forward);
    }

}
