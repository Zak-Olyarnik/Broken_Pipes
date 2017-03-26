using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
	public Texture2D cursorTexture;		// custom tool sprite
	public int fixRate;					// rate of pipe fixing with this tool
	public int strengthValue;			// amount of strength added by using this tool
	private Vector2 hotSpot;			// center of custom cursor icon
	
	// changes cursor to match selected tool
	void OnMouseDown()
	{
		hotSpot = new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
		Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
		GameController.getInstance().selectedTool = this;
	}
}
