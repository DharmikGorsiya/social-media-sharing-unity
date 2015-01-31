using UnityEngine;
using System.Collections;

public class MyFB : MonoBehaviour {

	public string gameLink;
	public string gameLinkName;
	public string messageCaption;
	public string messageDescription;
	public string gameIconLink;

	private static string gameLink_;
	private static string gameLinkName_;
	private static string messageCaption_;
//	private static string messageDescription_;
	private static string gameIconLink_;

	private static int score;
	private static bool isAuthenticated = false;

	void Start(){
		SPFacebook.instance.addEventListener(FacebookEvents.FACEBOOK_INITED, 			 OnInit);
		SPFacebook.instance.addEventListener(FacebookEvents.AUTHENTICATION_SUCCEEDED,  	 OnAuth);
		SPFacebook.instance.addEventListener(FacebookEvents.AUTHENTICATION_FAILED,  	 OnAuthFailed);
		SPFacebook.instance.addEventListener(FacebookEvents.POST_FAILED,  			OnPostFailed);
		SPFacebook.instance.addEventListener(FacebookEvents.POST_SUCCEEDED,   		OnPost);

		gameLink_ = gameLink;
		gameLinkName_ =gameLinkName;
		messageCaption_ = messageCaption;
//		messageDescription_ = messageDescription;
		gameIconLink_ = gameIconLink;
	}

	public static void PostScore(int currentScore){
		score = currentScore;
		if(!isAuthenticated){
			SPFacebook.instance.Init();
			SPFacebook.instance.Login("email,publish_actions");
		}
		else
			PostMessage();
	}

	private static void FBLogin(){
		SPFacebook.instance.Login("email,publish_actions");
	}

	private static void PostMessage() {
		//SPFacebook.instance.Init();
		//SPFacebook.instance.Login("email,publish_actions");
		SPFacebook.instance.Post (
			link: gameLink_,
			linkName: gameLinkName_,
			linkCaption: messageCaption_,
			linkDescription: "OMG! I got \"" +score+ "\" points in #CirclePong",
			picture: gameIconLink_
			);
	}

	#region	EVENT LISTENER

	private void OnInit() {

		if(SPFacebook.instance.IsLoggedIn) {
			OnAuth();
		} else {
			FBLogin();
		}
	}
	
	private void OnAuth() {
		isAuthenticated = true;
		PostMessage();
	}
	
	private void OnAuthFailed(CEvent e) {
		FBResult result = e.data as FBResult;
		
	}
	
	private void OnPost(CEvent e) {
		FBResult res = e.data as FBResult;
	}
	
	private void OnPostFailed(CEvent e) {
		
		FBResult res = e.data as FBResult;
		
		//can be null if user wasn't logged in
		if(res != null) {
		}
	}
	#endregion
}
