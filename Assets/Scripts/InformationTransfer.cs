using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InformationTransfer{
    public static int amountOfPlayers = 0;
    public static bool isFull = false;
    public static GameObject root = null;
    public static int[] turnOrder = new int[6];
    public static GameObject playerController;
    private static GameObject[] players = new GameObject[6];
    private static int lastIndex = 0;

    public static void addPlayer(GameObject player){
        players[lastIndex] = player;
        lastIndex++;
        if(lastIndex == amountOfPlayers){
            isFull = true;
        }
    }

    public static void overwritePlayer(GameObject player, int index){
        players[index] = player;
    }

    public static GameObject getPlayer(int index){
        return players[index];
    }

    public static void quitUnusedPlayer(){
        foreach (Transform child in root.transform){
            bool delete = true;
            for (int i = 0; i < amountOfPlayers; i++){
                if(child.gameObject == players[i]){
                    delete = false;
                    break;
                }
            }
            if (delete){
                GameObject.Destroy(child.gameObject);
            }
        }
    }

}
