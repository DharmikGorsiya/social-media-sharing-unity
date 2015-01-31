using UnityEngine;
using System.Collections;

public class MyInstagram : MonoBehaviour {

	public static MyInstagram instance;

	void Start(){
		instance = this;
	}

	public Texture2D instaAd;

	static Texture2D screenShot;

	public void TakeInstagramScreenShot()
	{
		StartCoroutine("TakeSS");
	}

	IEnumerator TakeSS(){
		yield return new WaitForEndOfFrame();
		screenShot = new Texture2D( Screen.width,Screen.height, TextureFormat.RGB24, false );
		if (screenShot != null)
		{
			screenShot.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0, false );
			screenShot.Apply();
			var bytes = screenShot.EncodeToPNG();
			if (bytes.Length <= 0)
			{
				screenShot = null;
			}
		}
	}

	public void PostScreenShotWithScore(int score)
	{
//		SPInstagram.Share(instaAd, "I just Scored "+score+" on #CirclePong iOS game , can you beat my score ? \n Game Link : bit.ly/1v2KpyG");
		SPInstagram.Share(instaAd, "OMG! I got \"" +score+ "\" points in #CirclePong http://bit.ly/1v2KpyG");
	}
}
