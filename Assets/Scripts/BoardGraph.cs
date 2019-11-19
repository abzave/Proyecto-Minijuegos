using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGraph : MonoBehaviour{

    public static BoardGraph info;
    static bool[,] edges = new bool[26, 26];
    static int[,] weights = new int[26, 26];

    void Awake(){
        if (info == null){
            BoardGraph.info = this;
        }else if (info != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        int amount = Random.Range(423, 541);
        fillEdges(amount);
        ponderate();
    }

    void ponderate(){
        for (int i = 0; i < 26; i++){
            for (int j = i; j < 26; j++){
                if (edges[i, j]){
                    int weight = Random.Range(1, 11);
                    weights[i, j] = weight;
                    weights[j, i] = weight;
                }
                else{
                    weights[i, j] = -1;
                    weights[j, i] = -1;
                }
            }
        }
    }

    void fillEdges(int amount){
        int row = 0;
        while (amount > 0){
            int edge = Random.Range(0, 26);
            if (row != edge){
                edges[row, edge] = true;
                edges[edge, row] = true;
            }else{
                continue;
            }
            amount--;
            if (row < 25){
                row++;
            }else{
                row = 0;
            }
        }
    }

    public static int getWeight(int i, int j){
        return weights[i, j];
    }

}
