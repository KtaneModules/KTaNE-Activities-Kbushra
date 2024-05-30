using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System;
using Rnd = UnityEngine.Random;

public class SimpleModuleScript : MonoBehaviour 
{
	public KMAudio audio;
	public KMBombInfo info;
	public KMBombModule module;
	public KMSelectable[] buttons;
	static int ModuleIdCounter = 1;
	int ModuleId;

	bool _isSolved = false;
	bool incorrect = false;

	private string[] activities = new string[20]
	{
	"Coding","Sleeping","Troublemaker","Eating","Playing","Cooking","Building","Drawing","Storymaking","Studying","Watching",
	"Praying","Reading","Messaging","Boredom","Aiding","Teaching","Performing","Housework","Admiring"
	};
	public TextMesh[] activityTexts;
	private string[] names = new string[10]
	{
		"Jake","Emma","Max","Olivia","Betty","Musa","Ryan","Chris","Josie","Kelly"
	};
	public TextMesh nameText;

	public int[] activityages;
	public int age;

	#pragma warning disable 414
	private string TwitchHelpMessage = "The commands that you can use are: !{0} press 'x' (replace 'x' with the labelled button you want to press. This command will press that button).";
	#pragma warning restore 414

	void Awake() 
	{
		ModuleId = ModuleIdCounter++;

		foreach (KMSelectable button in buttons)
		{
			KMSelectable pressedButton = button;
			button.OnInteract += delegate () { press(pressedButton); return false; };
		}
	}

	void Start ()
	{
		int randNum1 = Rnd.Range (0, 20);
		int randNum2 = randNum1;
		int randNum3 = randNum2;
		int randNum4 = Rnd.Range (0, 10);
		activityTexts [0].text = activities [randNum1];

		for( ; randNum2 == randNum1; )
		{
			randNum2 = Rnd.Range(0,20);
		}
		activityTexts [1].text = activities [randNum2];

		for( ; randNum3 == randNum2 || randNum3 == randNum1; )
		{
			randNum3 = Rnd.Range(0,20);
		}
		activityTexts [2].text = activities [randNum3];

		nameText.text = names [randNum4];

		Calculations ();
	}

	void Calculations()
	{
		if ((info.GetBatteryCount () % 10) < 4) 
		{
			Log ("Battery count modulo 10 is less than 4");

			for (int i = 0; i < 3; i++) 
			{
				if (activityTexts [i].text == "Coding") {activityages [i] = 12;}
				if (activityTexts [i].text == "Sleeping") {activityages [i] = 20;}
				if (activityTexts [i].text == "Troublemaker") {activityages [i] = 5;}
				if (activityTexts [i].text == "Eating") {activityages [i] = 9;}
				if (activityTexts [i].text == "Playing") {activityages [i] = 2;}
				if (activityTexts [i].text == "Cooking") {activityages [i] = 11;}
				if (activityTexts [i].text == "Building") {activityages [i] = 3;}
				if (activityTexts [i].text == "Drawing") {activityages [i] = 5;}
				if (activityTexts [i].text == "Storymaking") {activityages [i] = 13;}
				if (activityTexts [i].text == "Studying") {activityages [i] = 15;}
				if (activityTexts [i].text == "Watching") {activityages [i] = 7;}
				if (activityTexts [i].text == "Praying") {activityages [i] = 18;}
				if (activityTexts [i].text == "Reading") {activityages [i] = 8;}
				if (activityTexts [i].text == "Messaging") {activityages [i] = 14;}
				if (activityTexts [i].text == "Boredom") {activityages [i] = 8;}
				if (activityTexts [i].text == "Aiding") {activityages [i] = 10;}
				if (activityTexts [i].text == "Teaching") {activityages [i] = 19;}
				if (activityTexts [i].text == "Performing") {activityages [i] = 9;}
				if (activityTexts [i].text == "Housework") {activityages [i] = 10;}
				if (activityTexts [i].text == "Admiring") {activityages [i] = 13;}
			}
		}

		if ((info.GetBatteryCount () % 10) > 3) 
		{
			Log ("Battery count modulo 10 is more than 3");

			for (int i = 0; i < 3; i++) 
			{
				if (activityTexts [i].text == "Coding") {activityages [i] = 60;}
				if (activityTexts [i].text == "Sleeping") {activityages [i] = 70;}
				if (activityTexts [i].text == "Troublemaker") {activityages [i] = 18;}
				if (activityTexts [i].text == "Eating") {activityages [i] = 13;}
				if (activityTexts [i].text == "Playing") {activityages [i] = 16;}
				if (activityTexts [i].text == "Cooking") {activityages [i] = 50;}
				if (activityTexts [i].text == "Building") {activityages [i] = 45;}
				if (activityTexts [i].text == "Drawing") {activityages [i] = 75;}
				if (activityTexts [i].text == "Storymaking") {activityages [i] = 85;}
				if (activityTexts [i].text == "Studying") {activityages [i] = 20;}
				if (activityTexts [i].text == "Watching") {activityages [i] = 30;}
				if (activityTexts [i].text == "Praying") {activityages [i] = 100;}
				if (activityTexts [i].text == "Reading") {activityages [i] = 70;}
				if (activityTexts [i].text == "Messaging") {activityages [i] = 50;}
				if (activityTexts [i].text == "Boredom") {activityages [i] = 40;}
				if (activityTexts [i].text == "Aiding") {activityages [i] = 60;}
				if (activityTexts [i].text == "Teaching") {activityages [i] = 70;}
				if (activityTexts [i].text == "Performing") {activityages [i] = 55;}
				if (activityTexts [i].text == "Housework") {activityages [i] = 60;}
				if (activityTexts [i].text == "Admiring") {activityages [i] = 90;}
			}
		}

		age = (int)((activityages [0] + activityages [1] + activityages [2]) / 3);

		if (nameText.text == "Jake") {age = age + 1;}
		if (nameText.text == "Emma") {age = age + 2;}
		if (nameText.text == "Max") {age = age - 1;}
		if (nameText.text == "Olivia") {age = age + 3;}
		if (nameText.text == "Betty") {age = age - 1;}
		if (nameText.text == "Musa") {age = age + 5;}
		if (nameText.text == "Ryan") {age = age + 1;}
		if (nameText.text == "Chris") {age = age;}
		if (nameText.text == "Josie") {age = age - 1;}
		if (nameText.text == "Kelly") {age = age + 4;}

		Debug.LogFormat ("[Activities #{0}] Activity ages are {1}, {2}, and {3}", ModuleId, activityages [0], activityages [1], activityages [2]);
		Debug.LogFormat ("[Activities #{0}] Person age is {1}", ModuleId, age);
	}

	void press(KMSelectable pressedButton)
	{
		int buttonPosition = Array.IndexOf(buttons, pressedButton);

		audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, buttons[buttonPosition].transform);
		buttons [buttonPosition].AddInteractionPunch ();
		if (_isSolved == false)
		{
			switch (buttonPosition) 
			{
			case 0:
				if (age < 10)
				{
					module.HandlePass ();
				}
				else
				{
					incorrect = true;
				}
				break;
			case 1:
				if (age > 9 && age < 21)
				{
					module.HandlePass ();
				}
				else
				{
					incorrect = true;
				}
				break;
			case 2:
				if (age > 20 && age < 31)
				{
					module.HandlePass ();
				}
				else
				{
					incorrect = true;
				}
				break;
			case 3:
				if (age > 30 && age < 51)
				{
					module.HandlePass ();
				}
				else
				{
					incorrect = true;
				}
				break;
			case 4:
				if (age > 50 && age < 76)
				{
					module.HandlePass ();
				}
				else
				{
					incorrect = true;
				}
				break;
			case 5:
				if (age > 75)
				{
					module.HandlePass ();
				}
				else
				{
					incorrect = true;
				}
				break;
			}
			if (incorrect) 
			{
				module.HandleStrike ();
				incorrect = false;
			}
		}
	}

	void Log(string message)
	{
		Debug.LogFormat("[Activities #{0}] {1}", ModuleId, message);
	}

	IEnumerator ProcessTwitchCommand(string command)
	{
		command = command.ToLowerInvariant();

		if (command == "press 1") 
		{
			yield return null;
			buttons [0].OnInteract();
			yield break;
		}
		if (command == "press 2") 
		{
			yield return null;
			buttons [1].OnInteract();
			yield break;
		}
		if (command == "press 3") 
		{
			yield return null;
			buttons [2].OnInteract();
			yield break;
		}
		if (command == "press 4") 
		{
			yield return null;
			buttons [3].OnInteract();
			yield break;
		}
		if (command == "press 5") 
		{
			yield return null;
			buttons [4].OnInteract();
			yield break;
		}
		if (command == "press 6") 
		{
			yield return null;
			buttons [5].OnInteract();
			yield break;
		}
	}
}
