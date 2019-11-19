using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static BomberBox;
using static InformationTransfer;
using static PlayerController;

public class Bombermario : MonoBehaviour{

    private GameObject[,] board;
    private int[] sizes = new int[3] { 10, 15, 20 };
    private int bombs = 10;
    private int[,] treasureCoordinates = new int[4, 2];
    private PlayerController script;
    private KeepInformation KeepScript;
    public GameObject box;
    public GameObject panel;
    public GameObject treasure00;
    public GameObject treasure01;
    public GameObject treasure10;
    public GameObject treasure11;
    public Text bombsLabel;

    void Start(){
        //KeepScript = InformationTransfer.playerController.transform.parent.gameObject.GetComponent<KeepInformation>();
        //script = InformationTransfer.playerController.GetComponent<PlayerController>();
        int size = sizes[Random.Range(0, 3)];
        board = new GameObject[size, size];
        createBoxes(size);
        positionateTreasure(size);
        updateBombs();
    }

    public void disablePanel(){
        panel.SetActive(false);
        returnToBoard();
    }

    void returnToBoard(){
        //script.changeContinueState(panel.GetComponentInChildren<Text>().text == "Has ganado!");
        //script.changeTurn();
        //KeepScript.showOrHideObjects(true);
        //SceneManager.LoadScene("Board");
    }

    void bombPut(int[] coordinates){
        bombs--;
        updateBombs();
        destroyBoxes(coordinates[0], coordinates[1]);
        if (verifyWin()){
            win();
        }else if (verifyLoose()){
            loose();
        }
    }

    void win(){
        panel.GetComponentInChildren<Text>().text = "Has ganado!";
        panel.SetActive(true);
    }

    void loose(){
        panel.GetComponentInChildren<Text>().text = "Has Perdido :(";
        panel.SetActive(true);
    }

    bool verifyWin(){
        if(board[treasureCoordinates[0, 0], treasureCoordinates[0, 1]].activeSelf){
            return false;
        }else if(board[treasureCoordinates[1, 0], treasureCoordinates[1, 1]].activeSelf){
            return false;
        }else if(board[treasureCoordinates[2, 0], treasureCoordinates[2, 1]].activeSelf){
            return false;
        }else if(board[treasureCoordinates[3, 0], treasureCoordinates[3, 1]].activeSelf){
            return false;
        }
        return true;
    }

    bool verifyLoose(){
        return bombs == 0;
    }

    void destroyBoxes(int row, int column){
        board[row, column].SetActive(false);
        board[row + 1, column].SetActive(false);
        board[row, column +  1].SetActive(false);
        board[row + 1, column + 1].SetActive(false);
        showTreasure();
    }

    void showTreasure(){
        GameObject box00 = board[treasureCoordinates[0, 0], treasureCoordinates[0, 1]];
        GameObject box01 = board[treasureCoordinates[1, 0], treasureCoordinates[1, 1]];
        GameObject box10 = board[treasureCoordinates[2, 0], treasureCoordinates[2, 1]];
        GameObject box11 = board[treasureCoordinates[3, 0], treasureCoordinates[3, 1]];
        if (!box00.activeSelf){
            treasure00.SetActive(true);
            treasure00.transform.position = box00.transform.position;
        }
        if (!box01.activeSelf){
            treasure01.SetActive(true);
            treasure01.transform.position = box01.transform.position;
        }
        if (!box10.activeSelf){
            treasure10.SetActive(true);
            treasure10.transform.position = box10.transform.position;
        }
        if (!box11.activeSelf){
            treasure11.SetActive(true);
            treasure11.transform.position = box11.transform.position;
        }
    }

    void positionateTreasure(int size){
        int row = Random.Range(0, size - 1);
        int column = Random.Range(0, size - 1);
        treasureCoordinates[0, 0] = row;
        treasureCoordinates[0, 1] = column;
        treasureCoordinates[1, 0] = row;
        treasureCoordinates[1, 1] = column + 1;
        treasureCoordinates[2, 0] = row + 1;
        treasureCoordinates[2, 1] = column;
        treasureCoordinates[3, 0] = row + 1;
        treasureCoordinates[3, 1] = column + 1;
    }

    void createBoxes(int size){
        Vector3 position = box.transform.position;
        float x = position.x;
        for (int i = 0; i < size; i++){
            position.x = x;
            for (int j = 0; j < size; j++){
                GameObject newBox = Instantiate(box, position, box.transform.rotation);
                newBox.transform.SetParent(gameObject.transform);
                BomberBox script = newBox.GetComponent<BomberBox>();
                script.row = i;
                script.column = j;
                board[i, j] = newBox;
                position.x += 12;
            }
            position.y -= 12;
        }
        Destroy(box);
    }

    void updateBombs(){
        bombsLabel.text = "Bombas: " + bombs;
    }

}
