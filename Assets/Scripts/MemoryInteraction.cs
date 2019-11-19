using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MemoryPath;
using static ObjectColor;

public class MemoryInteraction : MonoBehaviour{

    private MemoryPath script;
    private ObjectColor color;
    public int row;
    public int column;

    void Start(){
        script = gameObject.transform.parent.GetComponent<MemoryPath>();
        color = gameObject.GetComponent<ObjectColor>();
    }

    void OnMouseDown(){
        if(script.isCorrect(row, column)){
            color.red = 0;
            color.green = 5;
            color.blue = 0;
            color.reload();
            script.verifyWin(row);
        }else{
            color.red = 5;
            color.green = 0;
            color.blue = 0;
            color.reload();
            script.reset();
        }
    } 

    public void resetColor(){
        color.red = 5;
        color.green = 3;
        color.blue = 0;
        color.reload();
    }

}
