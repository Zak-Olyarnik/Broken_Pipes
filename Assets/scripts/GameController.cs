using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
	static public GameController instance;      // instance of the GameController
	static public int score;					// score during play, equal to clock time
	public GameObject[] holes;					// array of pipe break positions
	public GameObject waterTank;				// sprite showing remaining "life" 
	public Tool selectedTool;					// currently selected tool
	public Tool noTool;							// "No Tool" selected, starting condition
	public Text clockText;						// displays seconds since start / score

	private float subTime = 0;					// value subtracted off to increase pipe break time

	// initialization
	void Start ()
	{
		instance = this;					// set instance
		score = 0;							// reset score
		Invoke("BreakPipe", 4);				// start pipes breaking
		InvokeRepeating("Clock", 1, 1);		// start updating score display
	}

	// gets current instance of the gameController
	public static GameController getInstance()
	{ return instance; }
	
	// update is called once per frame
	void Update ()
	{
		int holeCount = 0;
		float drainRatePerHole = .0001F;
		
		// check for loss condition and reset for replay
		if (waterTank.gameObject.transform.localScale.y <= 0)
		{
			//foreach (GameObject h in holes)
			//{ h.SetActive(false); }
			selectedTool = noTool;
			Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
			CancelInvoke();
			SceneManager.LoadScene("menu");		// score is read from Menu.cs
		}
		else
		{
			// calculate subtraction rate based on number of broken pipes
			foreach (GameObject h in holes)
			{
				if (h.activeSelf)
				{ holeCount += 1; }
			}
			// "remove" water from tank accordingly
			waterTank.gameObject.transform.localScale -= new Vector3(0, holeCount * drainRatePerHole, 0);
		}
	}
	
	// selects a random pipe to break, then sets up a slowly-decreasing random time for the next one
	void BreakPipe() 
	{
		float randomTime;

		GameObject hole = holes[Random.Range(0, (holes.Length))];	// Random.Range integer is max-exclusive
		hole.GetComponent<HoleController>().BreakPipe();

		if (subTime > 4)
		{ randomTime = 1f; }
		else
		{ randomTime = Random.Range(1, 5 - subTime); }				// Random.Range float is max-inclusive
		Invoke("BreakPipe", randomTime);
		subTime += .005f;
	}

	// updates the simple seconds counter, which also serves as a score display
	void Clock()
	{
		score += 1;
		clockText.text = score.ToString();
	}
}
