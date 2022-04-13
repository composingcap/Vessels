using UnityEngine;
using UnityEngine.Events;

[System.Serializable] //Allows the new unity event to be seen in full by other scripts
//OSCEvents extends the UnityEvent class and is based off a floating point number template
public class OSCEvents : UnityEvent<float>
{
}
public class OscEventTrigger : MonoBehaviour
{
    public OSCEvents oscEvent; //The OSC event sends data to another script in unity
    public OSC osc; //This is linked to the main the OSC instance in Unity
    public string messageName; //The message name filters an OSC address
    // Start is called before the first frame update
    void Start()
    {
        osc = GameObject.FindGameObjectWithTag("OSC").GetComponent<OSC>(); //Find the OSC instance on start 
        osc.SetAddressHandler(messageName, triggerOscEvent); //Trigger the OSC event when the address is detected
    }

    // Update is called once per frame
    void triggerOscEvent(OscMessage m)
    {
        oscEvent.Invoke((float)m.values[0]);
        

    }
}
