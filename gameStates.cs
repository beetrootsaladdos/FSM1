using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Debug = UnityEngine.Debug;


// patrole, alert, chase, retreat
public class GuardState : MonoBehaviour

{
    // common base class for sharing stuff (e.g. static counter variables)
    // also forces people to implement minimal functionality
    public virtual void handleInput(gameStates thisObject) { }
    public virtual void report(gameStates thisObject) { }
};

public class PatrolState : GuardState
{
    public override void handleInput(gameStates thisObject)
    {
        if (Input.GetKeyUp(thisObject.shoutKey))
        {
            Debug.Log(thisObject.playerName + " shouts, \"" + thisObject.shoutText + "\"");
        }
        else if (Input.GetKeyUp(thisObject.swapStateKey)) //chnage of state
        {
            thisObject.currentState = new AlertState();
            Debug.Log(thisObject.playerName + " : no point hiding now!\n");
        }
    }

    public override void report(gameStates thisObject) //if nothing pressed continue
    {
        Debug.Log(thisObject.playerName + " patrols as normal\n");
    }
}


public class AlertState : GuardState
{
    public override void handleInput(gameStates thisObject)
    {
        if (Input.GetKeyUp(thisObject.shoutKey))
        {
            Debug.Log(thisObject.playerName + " shouts, \"" + thisObject.shoutText + "\"");
        }
        else if (Input.GetKeyUp(thisObject.swapStateKey)) //come back here to add options
        {
            thisObject.currentState = new ChaseState();
            Debug.Log(thisObject.playerName + " : there you are. better get running\n");
        }
    }

    public override void report(gameStates thisObject)
    {
       Debug.Log(thisObject.playerName + " : come out little rat\n");
    }
}

public class ChaseState : GuardState
{
    public override void handleInput(gameStates thisObject)
    {
        if (Input.GetKeyUp(thisObject.shoutKey))
        {
            Debug.Log(thisObject.playerName + " shouts, \"" + thisObject.shoutText + "\"");
        }
        else if (Input.GetKeyUp(thisObject.swapStateKey)) //change conditions
        {
            thisObject.currentState = new RetreatState();
            Debug.Log(thisObject.playerName + " : your tricks won't hold me off for long!\n");
        }
    }

    public override void report(gameStates thisObject)
    {
        Debug.Log(thisObject.playerName + " : you may be fast but I don't tire so easy\n");
    }
}

public class RetreatState : GuardState
{
    public override void handleInput(gameStates thisObject)
    {
        if (Input.GetKeyUp(thisObject.shoutKey))
        {
            Debug.Log(thisObject.playerName + " shouts, \"" + thisObject.shoutText + "\"");
        }
        else if (Input.GetKeyUp(thisObject.swapStateKey))
        {
            thisObject.currentState = new AlertState();
            Debug.Log(thisObject.playerName + " : I'll find you again, don't worry\n");
        }
    }

    public override void report(gameStates thisObject)
    {
        Debug.Log(thisObject.playerName + " : damn it all, I'll give you credit\n");
    }
}


public class gameStates : MonoBehaviour 
{

    public string playerName;
    public KeyCode swapStateKey;
    public KeyCode shoutKey;
    public string shoutText;
    [SerializedField] private string[] states;
    [SerializedField] private float moveSpeed;

    GuardState new currentState;
    public string[4] arrayState = { PatrolState, AlertState, ChaseState, RetreatState};

    // Use this for initialization
    void Start()
    {
        currentState = 0;
        InvokeRepeating("Report", 0.0f, 3.0f);
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        currentState.handleInput(this);
        if (states[currentState] == "Patrol")
    }

    void Report()
    {
        currentState.report(this);
    }
}
