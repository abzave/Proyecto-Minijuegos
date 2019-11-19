using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour{

    public float red;
    public float green;
    public float blue;
    private Renderer rendererColor;

    // Start is called before the first frame update
    void Start(){
        rendererColor = GetComponent<Renderer>();
        rendererColor.material.color = new Color(red, green, blue);
    }

    public void reload(){
        rendererColor.material.color = new Color(red, green, blue);
    }

}
