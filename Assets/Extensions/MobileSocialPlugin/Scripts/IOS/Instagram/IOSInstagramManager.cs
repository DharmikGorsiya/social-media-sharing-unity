////////////////////////////////////////////////////////////////////////////////
//  
// @module Mobile Social Plugin 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IOSInstagramManager : Singletone<IOSInstagramManager> {



	[DllImport ("__Internal")]
	private static extern void _instaShare(string encodedMedia, string message);

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public void Share(Texture2D texture) {
		Share(texture, "");
	}


	public void Share(Texture2D texture, string message) {
		byte[] val = texture.EncodeToPNG();
		string bytesString = System.Convert.ToBase64String (val);
		
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_instaShare(bytesString, message);
		}
	}




	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------


	private void OnPostSuccess() {
		dispatch(InstagramEvents.POST_SUCCEEDED);
	}
	
	
	private void OnPostFailed(string data) {

		int code = System.Convert.ToInt32(data);
		InstaErrorCode error = InstaErrorCode.NO_APPLIACTON_INSTALED;

		switch(code) {
		case 1:
			error = InstaErrorCode.NO_APPLIACTON_INSTALED;
			break;
		case 2:
			error = InstaErrorCode.USER_CANSLED;
			break;
		case 3:
			error = InstaErrorCode.SYSTEM_VERSION_ERROR;
			break;
		}


		dispatch(InstagramEvents.POST_FAILED, error);
	}

	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
