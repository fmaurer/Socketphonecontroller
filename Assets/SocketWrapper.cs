using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
public class SocketWrapper : MonoBehaviour
{

	//STARTS WEBSOCKET SERVER USING WEBSOCKET-SHARP GITHUB PROJECT

	/* SUBSCRIBE TO CALLBACKS IN SCRIPTS e.g. GameController.cs:
	 * 
	 * OnEnable(){
	 * 		GetWSPacket.OnWebsocketMsg += FUNCTIONNAME;
	 * }
	 * */

	private WebSocketServer wssv;

	void Start()
	{
		wssv = new WebSocketServer (8051);
		wssv.AddWebSocketService<GetWSPacket> ("/webdata");
		wssv.Start();
		print( "WebSocket Server listening on port:" + wssv.Port );
	}

	public void BroadcastData(string str){
		if (wssv.IsListening)
			wssv.WebSocketServices.Broadcast (str);
	}

	void Update()
	{
		//if(Input.GetKeyDown(KeyCode.Space))
		//	wssv.Stop();
	}

	void OnApplicationQuit(){
		wssv.Stop();
	}
}

public class GetWSPacket : WebSocketBehavior
{

	public delegate void WebsocketMessage(string str);
	public static event WebsocketMessage OnWebsocketMsg;
	public static event WebsocketMessage OnNewWebsocketConnection;
	public static event WebsocketMessage OnWebsocketClose;

	protected override void OnMessage(MessageEventArgs e)
	{
		// echo back the data
		//Send(e.Data);

		// print the received data on the screen
		//MonoBehaviour.print(e.Data);	

		// ========================================================
		// Write the received data to a text file. The main program
		// will read the data from this file.

		string msg = e.Data;
		//Debug.Log (msg);

		try{
			OnWebsocketMsg(msg);
		} catch (SystemException err){
			Debug.Log(err.Message);
		}
		// create a writer and open the file
		//TextWriter tw = new StreamWriter("data.txt");
		// write a line of text to the file
		//tw.WriteLine(e.Data);
		// close the stream
		//tw.Close();
	}

	protected override void OnOpen()
	{
		Debug.Log ("new connect, currently connected: "+this.Sessions.Count);
		//ArrayList activeSessions = new ArrayList ();

		string newhash = Guid.NewGuid ().ToString();
		this.Send (newhash);


		/*string activeSessions = "";
		foreach(object item in this.Sessions.ActiveIDs)
		{
			activeSessions += item+ " ";
		}
		*/
		Debug.Log (newhash);


		//OnNewWebsocketConnection (newhash);
	}

	protected override void OnClose(WebSocketSharp.CloseEventArgs e)
	{
		Debug.Log ("socket dropped, currently open sessions: " +this.Sessions.Count);
		Debug.Log (e.Reason);
		string activeids = "";
		foreach (string id in this.Sessions.ActiveIDs) {
			activeids += id + " ";
		}

		//OnWebsocketClose (activeids);

		//Debug.Log (this.Sessions[0]);
	}

}
