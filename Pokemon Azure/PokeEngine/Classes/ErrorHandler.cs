    /*
	 * A basic error handler
	 */
	
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Microsoft.Xna.Framework.Input;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Content;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Audio;
	
	namespace PokeEngine.Error
	{
	    static class ErrorHandler : PokeEngine.Classes.Screens.DialogBox
	    {
	
	        public struct errdescriptions
	        {
				//example errors. We need to fill these out fully
				//at some point.

				string err10 = "10. Method or variable doesn't exist. Check your code!";
				string err20 = "20. Unable to read data. Check your permissions.";
						string err21= "21. Unable to write data. Check your permissions.";
				string err30 = "30. General Error.";
			}

	        /// <summary>
	        /// Drawerror pops up a dialog box (from DialogBox.cs) containing info about the error that happened.
	        /// </summary>
	        /// <param name="isrecoverable">A boolean representing whether or not the error is recoverable</param>
	        /// <param name="errordescription">A string describing said error</param>
	        /// </summary>
	
	        static public void drawerror(string errordescription, bool isrecoverable)
	        {
	            string wecanrecover = "this error is recoverable. The game will now continue.\n";
				showDialog("Error : " + errordescription + "\n");
	
	            //loop the infamous 'printer error' sound when there's an error
                   
                //realized that this could get REALLY annoying, so I disabled it for now.
	            /*
                SoundEffect soundEffect;
	            soundEffect = base.content.Load<SoundEffect>("SoundEffects/error.wav"); //load the sound
	            SoundEffectInstance instance = soundEffect.CreateInstance();
	            instance.IsLooped = true; //set to loop
	            soundEffect.Play(); //play it
                 */
	
	            if (isrecoverable == true)
	            {
					//Error is recoverable! Tell the player
					//and continue.
					showDialog(wecanrecover);
	                return;
	            }
	
	            else if (isrecoverable == false)
	            {
					//Throw an exception. End the game.
	               System.Application.Exception woops = new System.Application.Exception ("Error: " + errordescription + "\n");
					throw woops;
	            }
	        }
	
	    }
	
	
	}