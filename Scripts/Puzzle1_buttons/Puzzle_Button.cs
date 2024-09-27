﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Button : Interactable
{
    private string ButtonName;
    public GameObject theDoor, theMonitor;
    NetworkScript networkScript;
    bool isAnimating; //animatingstuff
    Vector3 startingPostion, endingPosition; //starting postion, ending position
    float pushDepth = 0.1f, pushSpeed = 0.4f; //how far to push in and out
    
    bool buttonIn; //check which way button is going
    
    //call out to the door that 
    
    //it turns out that listening events can't take information with them as far as i can tell that's why i'm using send message instead
    //public UnityEngine.Events.UnityEvent Buttons1Pressed;

    // Start is called before the first frame update
    void Start()
    {
        ButtonName = this.gameObject.name;
        
        //do we even use networkscript
        networkScript = GameObject.Find("Network").GetComponent<NetworkScript>();
        startingPostion = transform.localPosition;
        endingPosition = startingPostion + transform.forward * pushDepth;

    }


    public override void Interact()
    {
        Dictionary<string, string> buttonPush;

        //move button
        isAnimating = buttonIn = true;

        //play sound here

        //this is the message that needs to be sent
        //MULTI~Name|Door_Puzzle_1~Method|HandleMessage~Value|B1
        
        buttonPush = new Dictionary<string, string>();

        //linking the key of name to the value of door name in the dictionary called buttonpush

        buttonPush["Name"] = theDoor.name;
        buttonPush["Method"] = "HandleMessage";
        buttonPush["Value"] = ButtonName;

        //send network button push
        NetworkHandler.SendTcpMessage("127.0.0.1", 5556, new NetworkHandler.ThreadParams(), new NetworkHandler.MessageContents(buttonPush));

        //tell the door what button you are
        //TheDoor.SendMessage(ButtonName);
        theDoor.SendMessage("HandleMessage", ButtonName);
        //tell the monitor who you are as well
        theMonitor.SendMessage("HandleMessage", ButtonName);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            if (buttonIn == true)
            {

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, endingPosition, Time.deltaTime * pushSpeed);

                if (transform.localPosition == endingPosition)
                {
                    buttonIn = false;
                }

            }
            //else return to starting position
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPostion, Time.deltaTime * pushSpeed);
               
                if (transform.localPosition == startingPostion)
                {
                    isAnimating = false;
                }

            }
        }
    }
}
