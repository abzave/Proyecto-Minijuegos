using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InformationTransfer;

public class KeepInformation : MonoBehaviour{

    public static KeepInformation info;

    void Awake(){
        if (info == null){
            KeepInformation.info = this;
            DontDestroyOnLoad(gameObject);
            for (int i = 0; i < InformationTransfer.amountOfPlayers; i++){
                DontDestroyOnLoad(InformationTransfer.root);
            }
        }else if (info != this){
            Destroy(gameObject);
        }
    }

    public void showOrHideObjects(bool state){
        gameObject.SetActive(state);
        for (int i = 0; i < InformationTransfer.amountOfPlayers; i++){
            InformationTransfer.getPlayer(i).SetActive(state);
        }
    }

}
