using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWho : MonoBehaviour{

    public GameObject box;
    private GameObject[,] board = new GameObject[10, 10];

    void Start(){
        createBoxes();
    }

    void createBoxes(){
        Vector3 position = box.transform.position;
        float x = position.x;
        for (int i = 0; i < 10; i++){
            position.x = x;
            for (int j = 0; j < 10; j++){
                GameObject newBox = Instantiate(box, position, box.transform.rotation);
                newBox.transform.SetParent(gameObject.transform);
                board[i, j] = newBox;
                position.x += 0.6f;
            }
            position.y -= 0.6f;
        }
    }

}
