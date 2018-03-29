using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
	public GameObject healthBar;			// display of pipe's fix progress
	private int pipeHealth = 100;			// all pipes start at full health
	private int strength = 0;				// strength will cause pipes to withstand further breaks
	private float healthBarWidth = .4F;		// dimension for UI calculation and display

	// rapidly clicking with a tool fixes the pipe
	void OnMouseDown()
	{
		Tool selectedTool = GameController.getInstance().selectedTool;

		if (pipeHealth < 100)	// fix pipe (and update UI) with tool-specific fix rate
		{
			int fixRate = selectedTool.fixRate;
			pipeHealth += fixRate;
			healthBar.gameObject.transform.localScale = new Vector3(pipeHealth * healthBarWidth / 100F, .1F, 1F);
		}
		if (pipeHealth >= 100)	// once pipe is completely fixed, hide hole sprite and increase strength based on tool
		{
			int strengthValue = selectedTool.strengthValue;
			strength += strengthValue;
			AudioSource source = GetComponent<AudioSource>();
			source.Stop();
			gameObject.SetActive(false);
		}
	}

	// calculates the pipe's strength left from the last fix, then breaks it again if necessary
	public void BreakPipe()
	{
		if (strength > 0)
		{ strength -= 1; }
		else
		{
			gameObject.SetActive(true);
			AudioSource source = GetComponent<AudioSource>();
			source.Play();
			pipeHealth = 0;
			healthBar.transform.localScale = new Vector3(0, 0, 0);
		}
	}
}
