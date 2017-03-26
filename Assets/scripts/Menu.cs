using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public Text highScoreText;				// displays high score
	public Text lastScoreText;				// displays last earned score
	static private bool firstTime = true;   // initial play flag
	static public int score = 0;			// high score

	// displays high score only after first and subsequent plays
	void Start ()
	{
		if (!firstTime)
		{
			if (GameController.score > score)
			{ score = GameController.score; }
			highScoreText.text = "High Score: " + score;
			lastScoreText.text = "Last Score: " + GameController.score;
		}
	}

	// initializes and loads main level
	public void StartClick()
	{
		firstTime = false;
		SceneManager.LoadScene("main");
	}

	// exits
	public void QuitClick()
	{ Application.Quit(); }
}
