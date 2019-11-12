
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
static class DiscoveryController
{

	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput()
	{
		
		//use class name call the function and correct keycode name
		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.AddNewState(GameState.ViewingGameMenu);
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			DoAttack();
		}

		//check if player click surrender
		if (UtilityFunctions.IsMouseInRectangle (650, 70, 51, 46) & SwinGame.MouseClicked (MouseButton.LeftButton)) {
			GameController.HumanPlayer.IsSurrender = true;
			GameController.EndCurrentState ();
			GameController.AddNewState (GameState.EndingGame);
		}

		if (UtilityFunctions.IsMouseInRectangle (550, 70, 51, 46) & SwinGame.MouseClicked (MouseButton.LeftButton)) {
			KillAllAiShip ();
		}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack()
	{
		//use class name call the function and variable-- remark
		Point2D mouse = default (Point2D);

		mouse = SwinGame.MousePosition ();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32 (Math.Floor ((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
		col = Convert.ToInt32 (Math.Floor ((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

		if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) {
			if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) {
				GameController.Attack (row, col);
			}
		}
	}

	/// <summary>
	/// Kills all ai ship.
	/// </summary>
	public static void KillAllAiShip ()
	{
		//get the all ai ship size for the loop
		int Bssize = GameController.ComputerPlayer.Ship (ShipName.Battleship).Size;
		int Tugsize = GameController.ComputerPlayer.Ship (ShipName.Tug).Size;
		int Desize = GameController.ComputerPlayer.Ship (ShipName.Destroyer).Size;
		int Subize = GameController.ComputerPlayer.Ship (ShipName.Submarine).Size;
		int Airize = GameController.ComputerPlayer.Ship (ShipName.AircraftCarrier).Size;

		//check the ship direction then chose the atk position
		if (GameController.ComputerPlayer.Ship (ShipName.Tug).Direction == Direction.LeftRight) {
			GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Tug).Row, GameController.ComputerPlayer.Ship (ShipName.Tug).Column);
		} else {
			GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Tug).Row, GameController.ComputerPlayer.Ship (ShipName.Tug).Column);
		}

		for (int i = 0; i < Desize; i++) {
			if (GameController.ComputerPlayer.Ship (ShipName.Destroyer).Direction == Direction.LeftRight) {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Destroyer).Row, GameController.ComputerPlayer.Ship (ShipName.Destroyer).Column + i);
			} else {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Destroyer).Row + i, GameController.ComputerPlayer.Ship (ShipName.Destroyer).Column);
			}
		}

		for (int i = 0; i < Bssize; i++) {
			if (GameController.ComputerPlayer.Ship (ShipName.Battleship).Direction == Direction.LeftRight) {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Battleship).Row, GameController.ComputerPlayer.Ship (ShipName.Battleship).Column + i);
			} else {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Battleship).Row + i, GameController.ComputerPlayer.Ship (ShipName.Battleship).Column);
			}
		}

		for (int i = 0; i < Airize; i++) {
			if (GameController.ComputerPlayer.Ship (ShipName.AircraftCarrier).Direction == Direction.LeftRight) {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.AircraftCarrier).Row, GameController.ComputerPlayer.Ship (ShipName.AircraftCarrier).Column + i);
			} else {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.AircraftCarrier).Row + i, GameController.ComputerPlayer.Ship (ShipName.AircraftCarrier).Column);
			}
		}

		for (int i = 0; i < Subize; i++) {
			if (GameController.ComputerPlayer.Ship (ShipName.Submarine).Direction == Direction.LeftRight) {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Submarine).Row, GameController.ComputerPlayer.Ship (ShipName.Submarine).Column + i);
			} else {
				GameController.Attack (GameController.ComputerPlayer.Ship (ShipName.Submarine).Row + i, GameController.ComputerPlayer.Ship (ShipName.Submarine).Column);
			}
		}
	}

	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;

		//Draw whiteflag
		SwinGame.DrawBitmap (GameResources.GameImage ("Whiteflag"), 650, 70);
		//Draw whiteflag outline
		SwinGame.DrawRectangle (Color.Yellow, 650, 70, 51, 46);

		//Draw whiteflag
		SwinGame.DrawBitmap (GameResources.GameImage ("KillButton"), 550, 70);
		//Draw whiteflag outline
		SwinGame.DrawRectangle (Color.Yellow, 550, 70, 51, 46);
		
		//use class name call the function and keycode name
		if ((SwinGame.KeyDown (KeyCode.vk_LSHIFT) | SwinGame.KeyDown (KeyCode.vk_RSHIFT)) & SwinGame.KeyDown (KeyCode.vk_c)) {
			UtilityFunctions.DrawField (GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
		} else {
			UtilityFunctions.DrawField (GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}

		UtilityFunctions.DrawSmallField (GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
		UtilityFunctions.DrawMessage ();

		SwinGame.DrawText (GameController.HumanPlayer.Shots.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SHOTS_TOP);
		SwinGame.DrawText (GameController.HumanPlayer.Hits.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, HITS_TOP);
		SwinGame.DrawText (GameController.HumanPlayer.Missed.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SPLASH_TOP);
	}
}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
