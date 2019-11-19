using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using static InformationTransfer;
using static PlayerController;

public class LetterSoapController : MonoBehaviour{

    public Text label;
    public Text counterLabel;
    public InputField word;
    public GameObject verificationPanel;
    private string gameDataFileName = "Words.dat";
    private string[] words;
    private string[] selectedWords = new string[4];
    private bool[] correctWord = new bool[4] { false, false, false, false };
    private char[,] board;
    private const string letters = "abcdefghijklmnñpopqrstuvwxyz";
    private PlayerController script;
    private KeepInformation KeepScript;

    void Start(){
        KeepScript = InformationTransfer.playerController.transform.parent.gameObject.GetComponent<KeepInformation>();
        script = InformationTransfer.playerController.GetComponent<PlayerController>();
        readFile();
        selectWords();
        createBoard();
        fillBoard();
        positionateWords();
        fillLabel();
        StartCoroutine(counter());
    }

    void selectWords(){
        for (int i = 0; i < 4; i++){
            selectedWords[i] = words[Random.Range(0, 100)];
            selectedWords[i] = selectedWords[i].Substring(0, selectedWords[i].Length - 1);
            Debug.Log(selectedWords[i]);
        }
    }

    void readFile(){
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        StreamReader reader = new StreamReader(filePath);
        words = reader.ReadToEnd().Split('\n');
        reader.Close();
    }

    void createBoard(){
        int[] sizes = new int[3] {10, 15, 20};
        int size = sizes[Random.Range(0, 3)];
        board = new char[size, size];
    }

    void fillBoard(){
        for(int i = 0; i < board.GetLength(0); i++){
            for(int j = 0; j < board.GetLength(0); j++){
                board[i, j] = letters[Random.Range(0, letters.Length)];
            }
        }
    }

    void positionateWords(){
        positionateHorizontalWord(selectedWords[0]);
        positionateVerticalWord(selectedWords[1]);
        positionateDiagonalWord(selectedWords[2]);
        positionateBackDiagonalWord(selectedWords[3]);
    }

    void positionateHorizontalWord(string word){
        int row = Random.Range(0, board.GetLength(0));
        int column = Random.Range(0, board.GetLength(0) - word.Length - 1);
        for(int i = 0; i < word.Length; i++){
            board[row, column + i] = word[i];
        }
    }

    void positionateVerticalWord(string word){
        int row = Random.Range(0, board.GetLength(0) - word.Length - 1);
        int column = Random.Range(0, board.GetLength(0));
        for (int i = 0; i < word.Length; i++){
            board[row + i, column] = word[i];
        }
    }

    void positionateDiagonalWord(string word){
        int row = Random.Range(0, board.GetLength(0) - word.Length - 1);
        int column = Random.Range(0, board.GetLength(0) - word.Length - 1);
        for (int i = 0; i < word.Length; i++){
            board[row + i, column + i] = word[i];
        }
    }

    void positionateBackDiagonalWord(string word){
        int row = Random.Range(0, board.GetLength(0) - word.Length);
        int column = Random.Range(word.Length - 1, board.GetLength(0));
        for (int i = 0; i < word.Length; i++){
            board[row + i, column - i] = word[i];
        }
    }

    void fillLabel(){
        string message = "";
        for(int i = 0; i < board.GetLength(0); i++){
            for(int j = 0; j < board.GetLength(0); j++){
                message += board[i, j] + " ";
            }
            message += "\n";
        }
        label.text = message;
    }

    public void verifyWord(){
        bool correct = false;
        for(int i = 0; i < 4; i++){
            if(!correctWord[i] && word.text == selectedWords[i]){
                correct = true;
                correctWord[i] = true;
                break;
            }
        }
        word.text = "";
        if (verifyWin()){
            win();
        }else if (correct){
            verificationPanel.GetComponentInChildren<Text>().text = "Palabra correcta!";
        }else{
            verificationPanel.GetComponentInChildren<Text>().text = "Palabra incorrecta";
        }
        verificationPanel.SetActive(true);
    }

    public void disablePanel(){
        verificationPanel.SetActive(false);
    }

    IEnumerator counter(){
        int time = 120;
        while (time > 0){
            yield return new WaitForSeconds(1);
            time--;
            int seconds = time % 60;
            if(seconds >= 10){
                counterLabel.text = (time / 60) + ":" + seconds;
            }else{
                counterLabel.text = (time / 60) + ":0" + seconds;
            }
        }
        loose();
    }

    void loose(){
        verificationPanel.GetComponentInChildren<Text>().text = "Has perdido :(";
        verificationPanel.SetActive(true);
        StartCoroutine(wait(false));
    }

    bool verifyWin(){
        for(int i = 0; i < 4; i++){
            if (!correctWord[i]){
                return false;
            }
        }
        return true;
    }

    void win(){
        verificationPanel.GetComponentInChildren<Text>().text = "Has ganado :D";
        verificationPanel.SetActive(true);
        StartCoroutine(wait(true));
    }

    IEnumerator wait(bool state){
        yield return new WaitForSeconds(1);
        returnToBoard(state);
    }

    public void returnToBoard(bool won){
        script.changeContinueState(won);
        script.changeTurn();
        KeepScript.showOrHideObjects(true);
        SceneManager.LoadScene("Board");
    }

}
