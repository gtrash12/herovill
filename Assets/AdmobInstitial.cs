using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobInstitial : MonoBehaviour {

		// Initialize an InterstitialAd.
	public InterstitialAd interstitial;
	// Use this for initialization
	void Start () {
		 interstitial = new InterstitialAd("ca-app-pub-8988512880714262/1111563838");
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}
	public void Show(){
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		}
	}
}
