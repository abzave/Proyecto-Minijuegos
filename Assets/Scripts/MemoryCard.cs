using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MemoryController;

public class MemoryCard : MonoBehaviour{

    private static List<string> pairs = new List<string> { "1", "1", "2", "2", "3", "3", "4", "4", "5", "5", "6", "6", "7", "7", "8", "8", "9", "9" };
    private string figure;
    private MemoryController controller;
    private static bool canClick = true;
    public Text figureLabel;

    void Start(){
        int value = Random.Range(0, pairs.Count);
        figure = pairs[value];
        pairs.RemoveAt(value);
        figureLabel.text = figure;
        controller = transform.parent.gameObject.GetComponent<MemoryController>();
    }

    void OnMouseDown(){
        if (canClick) {
            canClick = false;
            figureLabel.gameObject.SetActive(true);
            int time = 0;
            if (controller.isOtherCardEnable()){
                time = 1;
                StartCoroutine(wait());
            }
            StartCoroutine(informClick(time));
        }
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(1);
        if (!controller.isPair(figureLabel.text)){
            figureLabel.gameObject.SetActive(false);
            controller.getLastObject().gameObject.SetActive(false);
        }
    }

    IEnumerator informClick(int time){
        yield return new WaitForSeconds(time + 0.1f);
        controller.cardClicked(figureLabel);
        canClick = true;
    }

}
