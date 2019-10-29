
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using SwinGameSDK;
using System.Diagnostics;
static class GameLogic
{
	private static bool _isPaused = false;
	public static void Main ()
	{
		//Opens a new Graphics Window
		SwinGame.OpenGraphicsWindow ("Battle Ships", 800, 600);

		//Load Resources
		GameResources.LoadResources ();

		SwinGame.PlayMusic (GameResources.GameMusic ("Background"));

		//use p to activate pause to stop userinput
		if (SwinGame.KeyDown (KeyCode.vk_p) & _isPaused == false) {
			_isPaused = true;
		} else if (SwinGame.KeyDown (KeyCode.vk_p) & _isPaused == true) {
			_isPaused = false;
		}

		//Game Loop
		do {
			if (_isPaused != true) {
				GameController.HandleUserInput ();
				GameController.DrawScreen ();
			}
		} while (!(SwinGame.WindowCloseRequested () == true | GameController.CurrentState == GameState.Quitting));

		SwinGame.StopMusic ();

		//Free Resources and Close Audio, to end the program.
		GameResources.FreeResources ();
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
