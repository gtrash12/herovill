using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Admob : MonoBehaviour {
	BannerView bannerView;
	// Use this for initialization
	void Start () {
		bannerView = new BannerView("ca-app-pub-8988512880714262/5740239834", AdSize.Banner, AdPosition.BottomRight);
		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd(request);
	}
	public void Hide(){
		bannerView.Hide();
	}

	public void Show(){
		bannerView.Show();
	}
}
