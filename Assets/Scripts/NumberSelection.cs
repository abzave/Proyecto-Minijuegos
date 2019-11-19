using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelection : MonoBehaviour{

    public GameObject messageSender;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            messageSender.SendMessage("validateNumber", SendMessageOptions.RequireReceiver);
        }
    }
}
