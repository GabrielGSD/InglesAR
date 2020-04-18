using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
	public void CarregarScene(string nomeScene)
	{
		SceneManager.LoadScene(nomeScene);
	}

	public void openURL() {

		Application.OpenURL("https://inatel.br/home/");

	}

	public void openFB()
	{

		Application.OpenURL("https://www.facebook.com/cdghubinatel/");

	}

	public void openIG()
	{

		Application.OpenURL("https://www.instagram.com/cdghubinatel/");

	}
}
