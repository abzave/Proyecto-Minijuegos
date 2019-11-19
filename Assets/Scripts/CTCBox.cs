using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectColor;

public class CTCBox : MonoBehaviour{

    public int row;
    public int column;
    public GameObject parent;
    private ObjectColor color;
    private bool active = false;

    void Start(){
        color = GetComponent<ObjectColor>();
    }

    void OnMouseDown(){
        if (!active) {
            color.red = 0;
            color.green = 0;
            color.blue = 0;
            color.reload();
            active = true;
            parent.SendMessage("boxClicked");
        }
    }

    public bool isActive(){
        return active;
    }

}
