using System.Collections;
using UnityEngine;

public class TransformSender : MonoBehaviour
{
    public OSC sender; //The main OSC instance in the Unity project 
    private OscMessage message; //Contains an address and values
    private Vector3 currentPosition;
    private Vector3 currentRotation;
    public bool messageOrderSwitch;

    // Start is called before the first frame update
    private void Start()
    {
        message = new OscMessage(); //Initialize the OSC message
        sender = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>(); //Find the OSC instance
    }

    // Update is called once per frame
    private void Update()
    {
        //Null check the OSC object so it does not crash the game if not found
        if (sender == null)
        {
            //Find the OSC instance
            sender = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>();  
            return;
        }
        //Construct OSC address versions are for two different uses in Max
        if (messageOrderSwitch) message.address = "/" + name + "/position" + "/xyz/";
        else message.address = "/position/" + gameObject.name + "/xyz/";
        currentPosition = gameObject.transform.position; //Get the position
        message.values = new ArrayList(); //Clears the values of the OSC message
        message.values.Add(currentPosition.x);
        message.values.Add(currentPosition.y);
        message.values.Add(currentPosition.z);
        sender.Send(message); //Send the message to Max

        
        if (messageOrderSwitch) message.address = "/" + name + "/rotation" + "/xyz/";
        else message.address = "/rotation/" + gameObject.name + "/xyz/";
        currentRotation = gameObject.transform.rotation.eulerAngles;
        message.values = new ArrayList();
        message.values.Add(currentRotation.x);
        message.values.Add(currentRotation.y);
        message.values.Add(currentRotation.z);
        sender.Send(message);
    }
}