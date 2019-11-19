using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Math;
using static CTCBox;
using static InformationTransfer;
using static PlayerController;

public class CatchTheCat : MonoBehaviour{

    private GameObject[,] board = new GameObject[11, 11];
    private int[] catPos = new int[2] {5, 5};
    private PlayerController script;
    private KeepInformation KeepScript;
    public GameObject box;
    public GameObject cat;
    public GameObject panel;

    void Start(){
        //KeepScript = InformationTransfer.playerController.transform.parent.gameObject.GetComponent<KeepInformation>();
        //script = InformationTransfer.playerController.GetComponent<PlayerController>();
        createBoxes();
        moveCat();
    }

    void createBoxes(){
        Vector3 position = box.transform.position;
        float x = position.x;
        for (int i = 0; i < 11; i++){
            position.x = x;
            for (int j = 0; j < 11; j++){
                GameObject newBox = Instantiate(box, position, box.transform.rotation);
                CTCBox script = newBox.GetComponent<CTCBox>();
                script.row = i;
                script.column = j;
                newBox.transform.SetParent(gameObject.transform);
                board[i, j] = newBox;
                position.x += 12;
            }
            position.y -= 12;
        }
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

    void boxClicked(){
        if(verifyLoose()){
            loose();
        }else if(verifyWin()){
            win();
        }else if (canMoveRight()){
            moveRight();
        }else if (canMoveDiagonalDown()){
            moveDiagonalDown();
        }else if (canMoveDown()){
            moveDown();
        }else if (canMoveBackDiagonalDown()){
            moveBackDiagonalDown();
        }else if (canMoveLeft()){
            moveLeft();
        }else if (canMoveBackDiagonalUp()){
            moveBackDiagonalUp();
        }else if (canMoveUp()){
            moveUp();
        }else if (canMoveDiagonalUp()){
            moveDiagonalUp();
        } else{
            moveRandom();
        }
    }

    void win(){
        panel.GetComponentInChildren<Text>().text = "Has ganado!";
        panel.SetActive(true);
    }

    void loose(){
        panel.GetComponentInChildren<Text>().text = "Has perdido :(";
        panel.SetActive(true);
    }

    void moveCat(){
        cat.transform.position = board[catPos[0], catPos[1]].transform.position;
        cat.transform.Translate(Vector3.back * 0.5f);
    }

    void moveRandom(){
        int val = Random.Range(0, 8);
        if (val == 0){
            moveRight();
        }else if (val == 1){
            moveDiagonalDown();
        }else if (val == 2){
            moveDown();
        }else if (val == 3){
            moveBackDiagonalDown();
        }else if (val == 4){
            moveLeft();
        }else if (val == 5){
            moveBackDiagonalUp();
        }else if (val == 6){
            moveUp();
        }else{
            moveDiagonalUp();
        }
    }

    bool verifyWin(){
        if(!board[catPos[0], catPos[1] + 1].GetComponent<CTCBox>().isActive()){
            return false;
        }else if (!board[catPos[0] + 1, catPos[1] + 1].GetComponent<CTCBox>().isActive()){
            return false;
        }else if (!board[catPos[0] + 1, catPos[1]].GetComponent<CTCBox>().isActive()){
            return false;
        }else if (!board[catPos[0] + 1, catPos[1] - 1].GetComponent<CTCBox>().isActive()){
            return false;
        }else if (!board[catPos[0], catPos[1] - 1].GetComponent<CTCBox>().isActive()){
            return false;
        }else if (!board[catPos[0], catPos[1] - 1].GetComponent<CTCBox>().isActive()){
            return false;
        }else if (!board[catPos[0] - 1, catPos[1] - 1].GetComponent<CTCBox>().isActive()){
            return false;
        }else if (!board[catPos[0] - 1, catPos[1]].GetComponent<CTCBox>().isActive()){
            return false;
        }
        return true;
    }

    bool verifyLoose(){
        if (catPos[0] == 0 || catPos[0] == 10){
            return true;
        }else if(catPos[1] == 0 || catPos[1] == 10){
            return true;
        }
        return false;
    }

    void moveRight(){
        catPos[1]++;
        moveCat();
    }

    void moveDiagonalDown(){
        catPos[0]++;
        catPos[1]++;
        moveCat();
    }

    void moveDown(){
        catPos[0]++;
        moveCat();
    }

    void moveBackDiagonalDown(){
        catPos[0]++;
        catPos[1]--;
        moveCat();
    }

    void moveLeft(){
        catPos[1]--;
        moveCat();
    }

    void moveBackDiagonalUp(){
        catPos[0]--;
        catPos[1]--;
        moveCat();
    }

    void moveUp(){
        catPos[0]--;
        moveCat();
    }

    void moveDiagonalUp(){
        catPos[0]--;
        catPos[1]++;
        moveCat();
    }

    bool canMoveRight(){
        if(catPos[1] >= 10){
            return false;
        }else{
            int y = catPos[0];
            for (int i = catPos[1]; i < 11; i++){
                if(board[y, i].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

    bool canMoveDiagonalDown(){
        if(catPos[1] >= 10 || catPos[0] >= 10){
            return false;
        }else{
            int min = Min(11 - catPos[0], 11 - catPos[1]);
            for (int i = 0; i < min; i++){
                if(board[catPos[0] + i, catPos[1] + i].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

    bool canMoveDown(){
        if(catPos[0] >= 10){
            return false;
        }else{
            int x = catPos[1];
            for (int i = catPos[0]; i < 11; i++){
                if(board[i, x].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

    bool canMoveBackDiagonalDown(){
        if(catPos[1] == 0 || catPos[0] == 0 || catPos[1] == 10 || catPos[1] == 10){
            return false;
        }else{
            int min = Min(11 - catPos[0], 11 - catPos[1]);
            for (int i = 0; i < min; i++){
                if(board[catPos[0] + i, catPos[1] - i].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

    bool canMoveLeft(){
        if(catPos[1] == 10 || catPos[1] == 0){
            return false;
        }else{
            int y = catPos[0];
            for (int i = 0; i < catPos[1]; i++){
                if(board[y, catPos[1] - i].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

    bool canMoveBackDiagonalUp(){
        if(catPos[1] == 0 || catPos[0] == 0 || catPos[1] == 10 || catPos[1] == 10){
            return false;
        }else{
            int min = Min(11 - catPos[0], 11 - catPos[1]);
            for (int i = 0; i < min; i++){
                if(board[catPos[0] - i, catPos[1] - i].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

    bool canMoveUp(){
        if(catPos[0] == 0 || catPos[1] == 10){
            return false;
        }else{
            int x = catPos[1];
            for (int i = catPos[0]; i < 11; i++){
                if(board[x, i].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

    bool canMoveDiagonalUp(){
        if(catPos[1] == 0 || catPos[0] == 0 || catPos[1] == 10 || catPos[1] == 10){
            return false;
        }else{
            int min = Min(11 - catPos[0], 11 - catPos[1]);
            for (int i = 0; i < min; i++){
                if(board[catPos[0] - i, catPos[1] + i].GetComponent<CTCBox>().isActive()){
                    return false;
                }
            }
            return true;
        }
    }

}
