using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicesController : MonoBehaviour{

    public bool canRoll;
    public GameObject dice1;
    public GameObject dice2;

    void Start(){
        canRoll = true;
        roll();
    }

    public int roll(){
        if (canRoll){
            int dice1Value = generateDiceValue(dice1);
            int dice2Value = generateDiceValue(dice2);
            return dice1Value + dice2Value;
        }else{
            return 0;
        }
    }

    int generateDiceValue(GameObject dice){
        int diceValue = Random.Range(0, 8);
        if (diceValue == 7){
            dice.gameObject.GetComponentInChildren<Text>().text = ":(";
            diceValue = -10;
        }else{
            dice.gameObject.GetComponentInChildren<Text>().text = diceValue.ToString();
        }
        return diceValue;
    }

    public void changeRollState(bool state){
        canRoll = state;
    }

}
