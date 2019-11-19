using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static InformationTransfer;
using static System.Int32;
using static System.Math;

public class TurnController : MonoBehaviour{

    public Text label;
    public GameObject questionPanel;
    public GameObject resultPanel;
    private int turn = 1;
    private int[] numbers = new int[InformationTransfer.amountOfPlayers];
    private int[] order = new int[InformationTransfer.amountOfPlayers];

    void Start(){
        label.text = "Jugador: 1";
        DontDestroyOnLoad(transform.gameObject);
        InformationTransfer.root = transform.gameObject;
    }

    public void nextTurn(){
        questionPanel.SetActive(true);
    }

    void validateNumber(){
        string number = questionPanel.gameObject.GetComponentInChildren<InputField>().text;
        TryParse(number, out numbers[turn - 1]);
        questionPanel.gameObject.GetComponentInChildren<InputField>().text = "";
        questionPanel.SetActive(false);
        changeTurn();
    }

    void changeTurn(){
        if (!InformationTransfer.isFull){
            turn++;
            label.text = "Jugador: " + turn;
        }else{
            StartCoroutine(lastTurn());
        }
    }

    IEnumerator lastTurn(){
        deteminateTurnOrder();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Board");
    }

    void deteminateTurnOrder(){
        int number = Random.Range(1, 1001);
        resultPanel.SetActive(true);
        resultPanel.gameObject.GetComponentInChildren<Text>().text = "El número era " + number;
        getOrder(number);
        InformationTransfer.turnOrder = order;
    }

    void getOrder(int number){
        for(int i = 0; i < InformationTransfer.amountOfPlayers; i++){
            int difference = Abs(number - numbers[i]);
            numbers[i] = difference;
            order[i] = i;
        }
        for (int i = 0; i < InformationTransfer.amountOfPlayers; i++){
            int swaps = 0;
            for(int j = 1; j < InformationTransfer.amountOfPlayers; j++){
                if(numbers[j - 1] > numbers[j]){
                    int temp = numbers[j - 1];
                    int temp2 = order[j - 1];
                    numbers[j - 1] = numbers[j];
                    order[j - 1] = order[j];
                    numbers[j] = temp;
                    order[j] = temp2;
                    swaps++;
                }
            }
            if(swaps == 0){
                break;
            }
        }
    }

}
