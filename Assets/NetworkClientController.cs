using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class NetworkClientController : MonoBehaviour {


	//USE THIS SCRIPT TO SEND DATA BACK TO WEBSOCKET CLIENT. FOR EXAMPLE, THIS WAS ORIGINALLY USED TO CONVERT THE CAMERA FRAME TO A BYTE STREAM AND SEND THAT TO CLEINT


//	ScreenCapture clientView;
	public Camera clientCam;
	SocketWrapper ws;
	string frameBytes;
	int framesSent = 0;

	float timeBetweenFrames = 0.01667f; // = 1 / FPS
	private float lastFrameUpdate;
	// Use this for initialization
	void Start () {
//		clientView = new ScreenCapture ();
		//clientCam = GetComponent<Camera> ();
	//	ws = GetComponent<SocketWrapper> ();
		lastFrameUpdate = 0f;

	}


	void Update(){

	}
	/*
	public void TriggerPostRender(Camera cam){

		if (cam == clientCam) { // send only the frame from correct camera
			StartCoroutine(MyPostRender ());
		}
	}


	private IEnumerator MyPostRender()
	{
		if (Time.time > lastFrameUpdate +timeBetweenFrames ) {
			
		//Wait for graphics to render
		yield return new WaitForEndOfFrame();

				//Generate byte array:
				Texture2D texture = clientView.GetScreenshot (CaptureMethod.ReadPixels_Synch);
				//byte[] bytes = texture.EncodeToPNG ();

				//byte[] bytes = texture.EncodeToPNG ();
				//string frameformat = "png";

				//Split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
				yield return 0;

				byte[] bytes = texture.EncodeToJPG(80);
				string frameformat = "jpg";
				//Debug.Log (bytes.Length);

				//convert into PNG base64 for web browser client rendering
				frameBytes = System.Convert.ToBase64String (bytes);
				//Broadcast to all clients:
				ws.BroadcastData ("data:image/"+frameformat+";base64," + frameBytes);

				//clean up for next frame
				lastFrameUpdate = Time.time;
				framesSent++;
				Texture.Destroy (texture); //release memory
				texture = null;
				//Debug.Log ("broadcast " + framesSent);
			}
	}*/

	public void OnEnable()
	{
		// register the callback when enabling object
		//Camera.onPostRender += TriggerPostRender;
	}
	public void OnDisable()
	{
		// remove the callback when disabling object
		//Camera.onPostRender -= TriggerPostRender;
	}

}
