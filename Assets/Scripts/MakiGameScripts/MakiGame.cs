﻿using UnityEngine;
using System.Collections;

public class MakiGame : MonoBehaviour {
    private GameObject[] arrows;
    public SushiMenu menu;
    // Use this for initialization
    void Start () {
        menu = GameObject.FindGameObjectsWithTag("sushimenu")[0].GetComponent<SushiMenu>();
       arrows = GameObject.FindGameObjectsWithTag("makiarrow");
       foreach(GameObject arrow in arrows){
            arrow.GetComponent<Arrow>().setMakiGame(this);
       }
    }

    // Update is called once per frame
    void Update () {

    }

    public void buildGame(){
        //randomly change orientation of arrows
        foreach(GameObject arrow in arrows){
            /*if(Random.Range(-1,1) <= 0){
                Debug.Log("Swapping orientation of this arrow.");
                arrow.GetComponent<Transform>().localScale = Vector3.Scale(arrow.GetComponent<Transform>().localScale , new Vector3(1.0F, -1.0F, 1.0F)); //scale in y
                arrow.GetComponent<Arrow>().toggleRightToLeft();
            }*/
            //show arrows
            arrow.GetComponent<Renderer>().enabled = true;
            //enable colliders of all children of arrow
            foreach(Transform child in arrow.transform){
                child.gameObject.GetComponent<Collider>().enabled = true;
            }
            //arrow.GetComponent<Collider>().enabled = true;
        }

    }
    public void failGame(){ //called by an arrow if the game is failed
        Debug.Log("Failed the game! Try Again!");
        cleanup();
    }
    public void winGame(){
        Debug.Log("Beat the Maki game!");
        cleanup();
    }
    void cleanup(){
        //hide arrows and other objects, rebuild menu
        foreach(GameObject arrow in arrows){
            arrow.GetComponent<Renderer>().enabled = false;
            foreach(Transform child in arrow.transform){
                child.gameObject.GetComponent<Collider>().enabled = false;
            }
            //arrow.GetComponent<Collider>().enabled = false;
            arrow.GetComponent<Arrow>().beaten = false;
            arrow.GetComponent<Arrow>().clicked = false;
        }
        menu.buildMenu();

    }
    public void beatArrow(){
        //Called when `arrow` is beaten. This checks for win condition.
        foreach(GameObject arrow in arrows){
            if(!arrow.GetComponent<Arrow>().beaten)
                return;
        }
        winGame();
    }
}
