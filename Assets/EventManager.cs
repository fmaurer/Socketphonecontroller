using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoBehaviour {

	///GENERIC EVENT CONTROLLER FOR SCENE OBJECTS TO PERFORM ACTIONS FROM
	/// 
	/// 

	public delegate void GameEvent();
	public static event GameEvent OnPlayerTriggerEnd;

	public static event GameEvent OnSocketOpen;
	public static event GameEvent OnSocketClose;
	public static event GameEvent OnSocketMessage;

	ArrayList unprocessedWSMessages;

	public void TriggerPlayerEnd(GameObject player){
		//all objects subscribed to EventManager.OnPlayerTriggerEnd activate
		OnPlayerTriggerEnd ();
	}

	void Start(){
		unprocessedWSMessages = new ArrayList ();
	}

	void Update(){
		/*
		//update websocket messages from clients:
		if (unprocessedWSMessages.Count > 0) {
			int startLength = unprocessedWSMessages.Count; //remember to check if list changes during processing
			ArrayList copy = new ArrayList (unprocessedWSMessages);
			foreach (string jsonmsg in copy) {
				if (jsonmsg.Substring (0, 1) == "{") {

					CustomWebSocketEvent wse = JsonUtility.FromJson<CustomWebSocketEvent> (jsonmsg);

				}

				if (startLength != unprocessedWSMessages.Count) {
					break;
				}
			}
			unprocessedWSMessages = new ArrayList();
		}*/
	}

	bool firstTime = true;
	void ExampleMethod(string msg){
		if (firstTime) {
			Debug.Log ("***Example callback - Event Manager was told about a new message: " + msg);
			firstTime = false;
		}
	}

	void OnEnable()
	{
		GetWSPacket.OnWebsocketMsg += ExampleMethod;
		//GetWSPacket.OnNewWebsocketConnection += AssignConnectionsToPlayers;
		//GetWSPacket.OnWebsocketClose += ConfirmActivePlayers ();
	}


	void OnDisable()
	{
		GetWSPacket.OnWebsocketMsg -= ExampleMethod;
		//GetWSPacket.OnNewWebsocketConnection -= AssignConnectionsToPlayers;
		//GetWSPacket.OnWebsocketClose -= ConfirmActivePlayers ();
	}


}
