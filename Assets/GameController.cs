using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	//SUBSCRIBES TO SOCKET PACKET SERVICE (i.e. GetWsPacket) AND PROCESS THE PACKETS SOMEHOW

	//public GameObject playerPrefab;
	public GameObject messageReceiver;
	Quaternion targetRotation;
	GameObject startzone;
	ArrayList newplayers;
	GameObject ActivePlayers;
	int numPlayers;
	public Text showConnectInfo;
	//public Camera gamecamera;

	public Color[] playerColors;

	string lastWSMessage = "";
	ArrayList unprocessedWSMessages;
	string UpdateActiveIds = ""; //Websocket updates this with still active connections
	// Use this for initialization

	SocketWrapper ws;
	void Start () {
		newplayers = new ArrayList();
		numPlayers = 0;//0
		//startzone = transform.FindChild ("START").gameObject;
		//ActivePlayers = GameObject.Find ("Players");
		//Debug.Log (Network.player.ipAddress);
		showConnectInfo.text = Network.player.ipAddress;

		unprocessedWSMessages = new ArrayList ();
		//AddNewPlayer ("asdfasfdsa");
		targetRotation = Quaternion.identity;
		//ws = GetComponent<SocketWrapper> ();
	}
		
	public void ResetLevel(){
		/*
		foreach (var t in ActivePlayers.transform){
			Destroy(((Transform)t).gameObject);
		}
		numPlayers = 0;
		newplayers = new ArrayList();
		*/
	}

	// Update is called once per frame
	void Update () {




		if (Input.GetKeyDown (KeyCode.R)) {
			ResetLevel ();
		}

		/*
		GenerateFrameObjectPositionsJSON ();

		//create new players
		if (newplayers.Count > 0) {
			ProcessConnections ();
		}
*/
		


		//Debug.Log ("Begin processing of packet buffer.");

		//update websocket messages from clients:
		if (unprocessedWSMessages.Count > 0) {
			int startLength = unprocessedWSMessages.Count; //remember to check if list changes during processing
			//ArrayList copy = new ArrayList(unprocessedWSMessages);
			foreach (string jsonmsg in unprocessedWSMessages) {
				if (jsonmsg.Substring (0, 1) == "{") {
					//Debug.Log ("do something with packet");
					CustomWebSocketEvent wse = JsonUtility.FromJson<CustomWebSocketEvent> (jsonmsg);

					//DO SOMETHING WITH THE PACKET...
					//Debug.Log(wse.yaw);
					//messageReceiver.transform.rotation = Quaternion.Euler( new Vector3(0,wse.yaw,0));
					targetRotation = Quaternion.Euler( new Vector3(0,wse.yaw,0));
				}

				if (startLength != unprocessedWSMessages.Count) {
					break;
				}

			}
			lastWSMessage = "";
			unprocessedWSMessages = new ArrayList();
		}

		if (targetRotation != null && targetRotation != messageReceiver.transform.rotation) {
			messageReceiver.transform.rotation = Quaternion.Lerp (messageReceiver.transform.rotation, targetRotation, Time.deltaTime * 25);
		}
	}

	public Color NextAvailablePlayerColor(){
		return playerColors [numPlayers];
	}

	/*
	public void AddNewPlayer(string wsID){
		GameObject newPlayer = Instantiate (playerPrefab, startzone.transform.position, Quaternion.identity);
		newPlayer.name = wsID; //"Player " + (numPlayers+1);
		newPlayer.transform.parent = ActivePlayers.transform;

		newPlayer.transform.GetComponent<MeshRenderer> ().material.color = NextAvailablePlayerColor ();
		//players.Add (newPlayer);
		numPlayers++;

		Debug.Log ("Added Player "+numPlayers+"("+newPlayer.name+") to the game");
	}

	void ProcessConnections(){

		//foreach connection,

		foreach (string id in newplayers) {
			//check if exists
			Transform result = ActivePlayers.transform.FindChild(id);
			if (result == null) {
				// if not, then create it
				AddNewPlayer(id);
			}
		}
		newplayers = new ArrayList ();
		//clean up old connections somehow
		//Debug.Log (conns [0]);
	}
	*/

	public void AssignConnectionsToPlayers(string newconnection){
		//newplayers.Add (newconnection);

		Debug.Log ("Assigning new connection to player: **** NOT IMPLEMENTED IN GAMECONTROLLER.cs");
	}

	void OnEnable()
	{
		EventManager.OnPlayerTriggerEnd += RestartLevel;
		GetWSPacket.OnWebsocketMsg += ProcessWSMessage;
		//GetWSPacket.OnNewWebsocketConnection += AssignConnectionsToPlayers;
		//GetWSPacket.OnWebsocketClose += ConfirmActivePlayers ();
	}


	void OnDisable()
	{
		EventManager.OnPlayerTriggerEnd -= RestartLevel;
		GetWSPacket.OnWebsocketMsg -= ProcessWSMessage;
		//GetWSPacket.OnNewWebsocketConnection += AssignConnectionsToPlayers;
	}

	public void ProcessWSMessage(string jsonmsg){
		lastWSMessage = (jsonmsg);
		unprocessedWSMessages.Add (jsonmsg);
	}

	void RestartLevel(){

		//Application.LoadLevel(Application.loadedLevel);
	}
}


[System.Serializable]
public class CustomWebSocketEvent {

	public string wsevent; //tap, press, pressup, pan, etc
	public string inputname; //input's reference
	public string serverToken;

	public int deltaX;
	public int deltaY;

	public float yaw;
	public float pitch;
	public float roll;


}