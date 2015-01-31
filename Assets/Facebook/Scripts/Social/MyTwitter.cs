////////////////////////////////////////////////////////////////////////////////
//  
// @module Mobile Social Plugin 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using System.Collections;

public class MyTwitter: MonoBehaviour {

	//REPLACE WITH YOUR KEY AND SECRET
	public string TWITTER_CONSUMER_KEY;// = "8YdNGF3fFiiwoIPpjjdleGnOM";
	public string TWITTER_CONSUMER_SECRET;// = "oJeyuTxlm41lGfoFHuRRLZ0ZKlJxZ6DcflnQkIHrLc8yI9TaER";

	public Texture2D adScreenShot;

	static Texture2D adScreen;

	private static bool IsAuntifivated = false;
	
	void Awake() {
		SPTwitter.Init(TWITTER_CONSUMER_KEY, TWITTER_CONSUMER_SECRET);
		
		SPTwitter.twitter.addEventListener(TwitterEvents.AUTHENTICATION_SUCCEEDED,  OnAuth);
		SPTwitter.twitter.addEventListener(TwitterEvents.TWITTER_INITED,  OnInit);
		
		SPTwitter.twitter.addEventListener(TwitterEvents.POST_SUCCEEDED,  OnPost);
		SPTwitter.twitter.addEventListener(TwitterEvents.POST_FAILED,  OnPostFailed);
		
		SPTwitter.twitter.addEventListener(TwitterEvents.USER_DATA_LOADED,  OnUserDataLoaded);
		SPTwitter.twitter.addEventListener(TwitterEvents.USER_DATA_FAILED_TO_LOAD,  OnUserDataLoadFailed);
	}

	void Start(){
		adScreen = adScreenShot;
	}

	public static void postScore(int score){
		Debug.Log("Posting score int Twitter");
		if(!IsAuntifivated) {
			SPTwitter.AuthificateUser();
		}
		else{
			/////////////////////////////////////////////
			// SET POSTING MESSAGE HEAR FOR TWITTER...
			/////////////////////////////////////////////

//			SPTwitter.Post("I just Scored " +score+ " On #CirclePong iOS game , can you beat my score ?  \n Game Link : bit.ly/1v2KpyG",adScreen);
			SPTwitter.Post("OMG! I got \"" +score+ "\" points in #CirclePong http://bit.ly/1v2KpyG",adScreen);
		}
	}

	#region EVENT LISTENER

	// --------------------------------------
	// EVENT LISTENER
	// --------------------------------------

	private void OnUserDataLoadFailed() {
		Debug.Log("Opps, user data load failed, something was wrong");
	}
	
	
	private void OnUserDataLoaded() {
		SPTwitter.twitter.userInfo.LoadProfileImage();
		SPTwitter.twitter.userInfo.LoadBackgroundImage();
		Camera.main.GetComponent<Camera>().backgroundColor  = SPTwitter.twitter.userInfo.profile_background_color;
	}
	
	
	private void OnPost() {
		Debug.Log("Congrats, you just postet something to twitter");
	}
	
	private void OnPostFailed() {
		Debug.Log("Opps, post failed, something was wrong");
	}
	
	
	private void OnInit() {
		if(SPTwitter.twitter.IsAuthed) {
			OnAuth();
		}
	}
	
	
	private void OnAuth() {
		IsAuntifivated = true;
	}
	#endregion
	// --------------------------------------
	// PRIVATE METHODS
	// --------------------------------------
	
	private IEnumerator PostScreenshot() {
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();
		
		SPTwitter.Post("My app ScreehShot", tex);
		
		Destroy(tex);
		
	}
	
	private void LogOut() {
		IsAuntifivated = false;
		
		SPTwitter.LogOut();
	}
}
