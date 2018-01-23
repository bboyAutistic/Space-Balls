using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour {

    public AudioMixer audioMixer;

	public NetworkManagerHUD multiplayerHUD;

    
    
	public void PlayGame()
    {
        SceneManager.LoadScene("Game");
	}
	public void PlayMultiplayer()
	{
		multiplayerHUD.OnHostPress();
	}

	public void JoinGame(){
		//multiplayerHUD.OnJoinPress ();
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetNomberOfEnemis(int br)
    {

        NumberOfSpawners.numberOfEnemis = br;
    }
    
}
