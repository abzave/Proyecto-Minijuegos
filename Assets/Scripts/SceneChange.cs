using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static InformationTransfer;

public class SceneChange : MonoBehaviour{
    
    public void change(string name){
        SceneManager.LoadScene(name);
    }

    public void setPlayers(int amount){
        InformationTransfer.amountOfPlayers = amount;
    }

}
