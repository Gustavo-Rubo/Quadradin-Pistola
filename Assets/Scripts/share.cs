using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Facebook.Unity;

public class share : MonoBehaviour {

    string AppID = "1874170446029947";
    string Link = "https://www.facebook.com/events/1928001570597653/";
    string Picture = "https://i.imgur.com/A4cZ26i.png";
    string Caption = "Fiz esse jogo em uma game jam da TFG e consegui essa pontuação: ";
    string Description = "Confira o evento da Hackathon";

    private void Awake()
    {
        /*
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }*/
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }


    public void shareScoreOnFacebook () {
        Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&link=" + Link + "&picture=" + Picture + "&caption=" + Caption + GameControl.instance.score + "&description=" + Description);
        //Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&caption=" + Caption + GameControl.instance.score);
        /*FB.ShareLink(
    new System.Uri("https://www.facebook.com/events/1928001570597653/"), "Fiz " + GameControl.instance.score + " pontos!",
    "Jogo desenvolvido na Hackathon da TFG",
    new System.Uri(Picture));*/

    }
}
