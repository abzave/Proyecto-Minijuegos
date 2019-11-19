using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NodeGeneration;

public class SelectVertex : MonoBehaviour{

    GameObject[] boxes;
    public int id = 0;
    private int actualVertex;
    private bool[] visited = new bool[26];
    private int initialVertex;
    private NodeGeneration vertexScript;

    void Start(){
        for(int i = 0; i < 26; i++){
            visited[i] = false;
        }
        initialVertex = Random.Range(0, 26);
        boxes = GameObject.FindGameObjectsWithTag("Box");
        restart();
    }

    public int getActualVertex(){
        return actualVertex;
    }

    public bool isVisited(int vertex){
        return visited[vertex];
    }

    public void restart(){
        for (int i = 0; i < 26; i++){
            visited[i] = false;
            vertexScript = (NodeGeneration)boxes[i].GetComponent(typeof(NodeGeneration));
            vertexScript.quitPlayer(id);
        }
        goToVertex(initialVertex, false);
    }

    public void goToVertex(int vertex, bool init = true){
        GameObject box = boxes[vertex];
        transform.position = box.transform.position;
        transform.Translate(Vector3.back * 0.5f);
        vertexScript = (NodeGeneration)box.GetComponent(typeof(NodeGeneration));
        vertexScript.visit(id, init);
        visited[vertex] = true;
        actualVertex = vertex;
    }

    void replay(){
        GameObject box = boxes[actualVertex];
        vertexScript = (NodeGeneration)box.GetComponent(typeof(NodeGeneration));
        vertexScript.visit(id, true);
    }

}
