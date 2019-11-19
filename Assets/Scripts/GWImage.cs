using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GWImage : MonoBehaviour{

    string[] images = new string[15] {"Bowser.jpg", "BowserJr.jpg", "Dirdo.jpg", "DonkeyKong.jpg",
     "Goomba.jpg", "KingBoo.jpg", "Luigi.jpg", "Mario.png", "PeteyPiranha.jpg", "PomPom.jpg", 
     "ShyGuy.jpg", "Toad.png", "Toadette.jpg", "Wario.jpg", "Yoshi.jpg"}; 

    void Start(){
        string image = images[Random.Range(0, 15)];
        string filePath = Path.Combine(Application.streamingAssetsPath, image);
        var bytes = System.IO.File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        Material material = new Material(Shader.Find("Diffuse"));
        material.mainTexture = texture;
        GetComponent<Renderer>().material = material;
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }
}
