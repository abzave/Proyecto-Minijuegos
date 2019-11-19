using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Array;
using static BoardGraph;
using static InformationTransfer;
using static PlayerController;
using static SelectVertex;

public class Dijkstra : MonoBehaviour{

    private int[] distance = new int[26];
    private int[][] cost = new int[26][];
    private bool[] visited = new bool[26];
    public GameObject dijkstraPanel;
    public GameObject floydPanel;

    void Start(){
        for(int i = 0; i < 26; i++){
            cost[i] = new int[26];
        }
    }

    public void run(){
        int startNode = getActualVertex();
        resetDistance();
        dijkstra(startNode);
        quitVisited();
        fillDijkstraPanel();
    }

    void dijkstra(int startNode){
        distance[startNode] = 0;
        for (int i = 0; i < 26; i++){
            int lower = getLowerNotVisited();
            visited[lower] = true;
            for (int j = 0; j < 26; j++){
                int weight = BoardGraph.getWeight(lower, j);
                if (weight == -1){
                    continue;
                }
                int newDistance = distance[lower] + weight;
                if (newDistance < distance[j]){
                    distance[j] = newDistance;
                }
            }
        }
    }

    public void runFloyd(){
        resetCost();
        for(int i = 0; i < 26; i++){
            resetDistance();
            dijkstra(i);
            Copy(distance, cost[i], 26);
        }
        quitOutOfRange();
        fillFloydPanel();
    }

    void resetDistance(){
        for (int i = 0; i < 26; i++){
            distance[i] = int.MaxValue;
            visited[i] = false;
        }
    }

    void quitOutOfRange(){
        for(int i = 0; i < 26; i++){
            for(int j = 0; j < 26; j++){
                if(cost[i][j] > 12){
                    cost[i][j] = -1;
                }
            }
        }
    }

    void resetCost(){
        for(int i = 0; i < 26; i++){
            visited[i] = false;
            distance[i] = int.MaxValue;
            for (int j = 0; j < 26; j++){
                cost[i][j] = 0;
            }
        }
    }

    int getLowerNotVisited(){
        int min = int.MaxValue;
        int index = -1;
        for (int i = 0; i < 26; i++){
            if(!visited[i] && min > distance[i]){
                min = distance[i];
                index = i;
            }
        }
        return index;
    }

    int getActualTurn(){
        PlayerController script = gameObject.GetComponent<PlayerController>();
        return script.getTurn();
    }

    int getActualVertex(){
        int playerIndex = getActualTurn();
        GameObject player = InformationTransfer.getPlayer(playerIndex);
        SelectVertex script = player.GetComponent<SelectVertex>();
        return script.getActualVertex();
    }

    void quitVisited(){
        int playerIndex = getActualTurn();
        GameObject player = InformationTransfer.getPlayer(playerIndex);
        SelectVertex script = player.GetComponent<SelectVertex>();
        for (int i = 0; i < 26; i++){
            if (script.isVisited(i)){
                distance[i] = -1;
            }
        }
    }

    void fillDijkstraPanel(){
        string message = " ";
        for (int i = 0; i < 26; i++){
            if(i == 13){
                message += "\n\n";
            }
            int value = distance[i];
            if(value < 10){
                message += "  ";
            }
            if(value >= 0) {
                message += value + "  ";
            }else{
                message += "v  ";
            }
        }
        dijkstraPanel.GetComponentsInChildren<Text>()[2].text = message;
        dijkstraPanel.SetActive(true);
    }

    void fillFloydPanel(){
        string message = "    1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26\n";
        for (int i = 0; i < 26; i++){
            if(i < 9){
                message += " ";
            }
            message += (i + 1);
            for(int j = 0; j < 26; j++){
                int value = cost[i][j];
                if(value < 10){
                    message += " ";
                }
                if (value >= 0){
                    message += " " + value;
                }else{
                    message += " -";
                }
            }
            message += "\n";
        }
        floydPanel.GetComponentsInChildren<Text>()[1].text = message;
        floydPanel.SetActive(true);
    }

    public void disableDijkstraPanel(){
        dijkstraPanel.SetActive(false);
    }

    public void disableFloydPanel(){
        floydPanel.SetActive(false);
    }

}
