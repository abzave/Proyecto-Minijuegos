using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBox : MonoBehaviour{

    public int row = 0;
    public int column = 0;
    public GameObject parent;

    public void OnMouseDown(){
        parent.SendMessage("bombPut", new int[2] {row, column}, SendMessageOptions.RequireReceiver);
    }

}
