using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DialogueManager : MonoBehaviour
{
	[Header("Params")]
	[SerializeField] private float typingSpeed = 0.04f;

	[Header("Dialogue UI")]
	[SerializeField] private TextMeshProUGUI texteDialogueDroite;
	[SerializeField] private TextMeshProUGUI texteDialogueGauche;
	[SerializeField] private TextMeshProUGUI texteDialogueSansPortrait;
	[SerializeField] private TextMeshProUGUI texteNomDroite, texteNomGauche, texteNomSansPortrait;
	[SerializeField] private GameObject panelPortraitDroite, imagePortraitVisageDroite, imagePortraitYeuxDroite, zoneNomDroite;
	[SerializeField] private GameObject panelPortraitGauche, imagePortraitVisageGauche, imagePortraitYeuxGauche, zoneNomGauche;
	[SerializeField] private GameObject panelSansPortrait, zoneNomSansPortrait;
	private TextMeshProUGUI dialogueText;
	private TextMeshProUGUI displayNameText;
	private GameObject dialoguePanel, portraitVisage, portraitYeux, zoneNom;

	[Header("Choices UI")]
	[SerializeField] private GameObject[] choixPortraitDroit;
	[SerializeField] private GameObject[] choixPortraitGauche;
	[SerializeField] private GameObject[] choixSansPortrait;

	[Header("Message UI")]
	[SerializeField] private TextMeshProUGUI zoneTexteMessage;
	[SerializeField] private GameObject panelMessage;
	[SerializeField] private GameObject[] choixMessage;

	private GameObject[] choices;
	private List<TextMeshProUGUI> choicesText;

	private Story currentStory;
	public bool cutsceneIsPlaying { get; private set; }
	public bool characterIsTalking { get; private set; }
	private bool portraitIsDisplayed = false;
	private string nomPortrait, nomSpeaker = "";
	private bool canContinueToNextLine = false;
	private bool estMessage;

	private IInteractible sourceDuDialogue;

	private Coroutine displayLineCoroutine;

	private const string SPEAKER_TAG = "speaker";
	private const string PORTRAIT_TAG = "portrait";
	private const string LAYOUT_TAG = "cotePortrait";

	private static DialogueManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Found more than one Dialogue Manager in the scene");
		}
		instance = this;
	}

	public static DialogueManager GetInstance()
	{
		return instance;
	}

	/*--------------*/
	/*---Dialogue---*/
	/*--------------*/
	private void Start()
	{
		cutsceneIsPlaying = false;
		panelPortraitDroite.SetActive(false);
		panelPortraitGauche.SetActive(false);
		panelSansPortrait.SetActive(false);
		panelMessage.SetActive(false);

		choicesText = new List<TextMeshProUGUI>();
	}

	private void Update()
	{
		// return right away if dialogue isn't playing
		if (!cutsceneIsPlaying)
		{
			return;
		}

		// handle continuing to the next line in the dialogue when submit is pressed
		// NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
		if (currentStory.currentChoices.Count == 0 && Input.GetButtonDown("Submit") && canContinueToNextLine)
		{
			if(estMessage)
			{
				ContinueMessage();
			}
			else
			{
				ContinueStory();
			}
		}

		if (portraitIsDisplayed)
		{
			gereAnimation(nomPortrait);
		}
	}

	public void EnterDialogueMode(TextAsset inkJSON, IInteractible source = null)
	{
		sourceDuDialogue = source;
		currentStory = new Story(inkJSON.text);
		cutsceneIsPlaying = true;
		estMessage = false;

		ContinueStory();
	}

	private IEnumerator ExitDialogueMode()
	{
		yield return new WaitForSeconds(0.2f);

		if (sourceDuDialogue != null)
		{
			sourceDuDialogue.finInteraction();
		}

		cutsceneIsPlaying = false;
		dialoguePanel.SetActive(false);
		displayNameText.text = "";
		dialogueText.text = "";
	}

	private void ContinueStory()
	{
		if (currentStory.canContinue)
		{
			// set text for the current dialogue line
			if (displayLineCoroutine != null)
			{
				StopCoroutine(displayLineCoroutine);
			}

			string line = currentStory.Continue();

			// handle tags
			HandleTags(currentStory.currentTags);

			displayLineCoroutine = StartCoroutine(DisplayLine(line));
		}
		else
		{
			StartCoroutine(ExitDialogueMode());
		}
	}

	private IEnumerator DisplayLine(string line)
	{
		// empty the dialogue text
		dialogueText.text = "";
		// hide items while text is typing
		HideChoices();

		yield return new WaitForSeconds(0.2f);

		if (Regex.IsMatch(line, ".*[a-zA-Z0-9].*") && portraitIsDisplayed)
		{
			characterIsTalking = true;
		}
		else
		{
			characterIsTalking = false;
		}

		canContinueToNextLine = false;

		bool isAddingRichTextTag = false;

		// display each letter one at a time
		foreach (char letter in line.ToCharArray())
		{
			

			// if the submit button is pressed, finish up displaying the line right away
			if (Input.GetButtonDown("Submit"))
			{
				dialogueText.text = line;
				break;
			}

			// check for rich text tag, if found, add it without waiting
			if (letter == '<' || isAddingRichTextTag)
			{
				isAddingRichTextTag = true;
				dialogueText.text += letter;
				if (letter == '>')
				{
					isAddingRichTextTag = false;
				}
			}
			// if not rich text, add the next letter and wait a small time
			else
			{
				dialogueText.text += letter;
				yield return new WaitForSeconds(typingSpeed);
			}

			if(portraitIsDisplayed)
			{
				gereAnimation(nomPortrait);
			}
		}

		characterIsTalking = false;

		// actions to take after the entire line has finished displaying
		DisplayChoices();

		canContinueToNextLine = true;
	}

	private void DisplayChoices()
	{
		List<Choice> currentChoices = currentStory.currentChoices;

		// get all of the choices text 
		foreach (GameObject choice in choices)
		{
			choicesText.Add(choice.GetComponentInChildren<TextMeshProUGUI>());
		}

		// defensive check to make sure our UI can support the number of choices coming in
		if (currentChoices.Count > choices.Length)
		{
			Debug.LogError("More choices were given than the UI can support. Number of choices given: "
				+ currentChoices.Count);
		}

		int index = 0;
		// enable and initialize the choices up to the amount of choices for this line of dialogue
		foreach (Choice choice in currentChoices)
		{
			choices[index].gameObject.SetActive(true);
			choicesText[index].text = choice.text;
			index++;
		}
		// go through the remaining choices the UI supports and make sure they're hidden
		for (int i = index; i < choices.Length; i++)
		{
			choices[i].gameObject.SetActive(false);
		}

		StartCoroutine(SelectFirstChoice());
	}

	private void HideChoices()
	{
		foreach (GameObject choiceButton in choixPortraitDroit)
		{
			choiceButton.SetActive(false);
		}

		foreach (GameObject choiceButton in choixPortraitGauche)
		{
			choiceButton.SetActive(false);
		}

		foreach (GameObject choiceButton in choixSansPortrait)
		{
			choiceButton.SetActive(false);
		}

		foreach (GameObject choiceButton in choixMessage)
		{
			choiceButton.SetActive(false);
		}
	}

	private IEnumerator SelectFirstChoice()
	{
		// Event System requires we clear it first, then wait
		// for at least one frame before we set the current selected object.
		EventSystem.current.SetSelectedGameObject(null);
		yield return new WaitForEndOfFrame();
		EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
	}

	public void MakeChoice(int choiceIndex)
	{
		currentStory.ChooseChoiceIndex(choiceIndex);

		choicesText.Clear();

		Input.GetButtonDown("Submit");
		ContinueStory();
	}

	private void HandleTags(List<string> currentTags)
	{
		string portrait = "", cotePortrait = "", nomTmp = "";
		// loop through each tag and handle it accordingly
		foreach (string tag in currentTags)
		{
			// parse the tag
			string[] splitTag = tag.Split(':');
			if (splitTag.Length != 2)
			{
				Debug.LogError("Tag could not be appropriately parsed: " + tag);
			}
			string tagKey = splitTag[0].Trim();
			string tagValue = splitTag[1].Trim();

			// handle the tag
			switch (tagKey)
			{
				case SPEAKER_TAG:
					nomTmp = tagValue;
					break;
				case PORTRAIT_TAG:
					portrait = tagValue;
					break;
				case LAYOUT_TAG:
					cotePortrait = tagValue;
					break;
				default:
					Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
					break;
			}
		}

		nomSpeaker = nomTmp;
		nomPortrait = portrait;
		gereCotePortrait(cotePortrait);
		if (displayNameText.text == "") 
		{ 
			zoneNom.SetActive(false); 
		}
		else
		{
			zoneNom.SetActive(true);
		}
	}

	private void gereCotePortrait(string cotePortrait)
	{
		cotePortrait = cotePortrait.ToUpper();

		if (dialoguePanel != null) { dialoguePanel.SetActive(false); }
		
		switch (cotePortrait)
		{
			case "DROITE":
				dialoguePanel  = panelPortraitDroite;
				dialogueText = texteDialogueDroite;
				portraitVisage = imagePortraitVisageDroite;
				portraitYeux = imagePortraitYeuxDroite;
				zoneNom = zoneNomDroite;
				displayNameText = texteNomDroite;
				choices = choixPortraitDroit;
				portraitIsDisplayed = true;
				dialoguePanel.SetActive(true);
				break;

			case "GAUCHE":
				dialoguePanel = panelPortraitGauche;
				dialogueText = texteDialogueGauche;
				portraitVisage = imagePortraitVisageGauche;
				portraitYeux = imagePortraitYeuxGauche;
				zoneNom = zoneNomGauche;
				displayNameText = texteNomGauche;
				choices = choixPortraitGauche;
				portraitIsDisplayed = true;
				dialoguePanel.SetActive(true);
				break;

			default:
				dialoguePanel = panelSansPortrait;
				dialogueText = texteDialogueSansPortrait;
				zoneNom = zoneNomSansPortrait;
				displayNameText = texteNomSansPortrait;
				choices = choixSansPortrait;
				portraitIsDisplayed = false;
				dialoguePanel.SetActive(true);
				break;
		}

		displayNameText.text = nomSpeaker;

		dialoguePanel.SetActive(true);
	}

	private void gereAnimation(string nomPortrait)
	{
		PortraitManager.instance.gereAnimations(nomPortrait, portraitVisage, portraitYeux);
	}

	/*---------------*/
	/*----Message----*/
	/*---------------*/

	public void DisplayMessage(TextAsset inkJSON = null, Story story = null, IInteractible source = null)
	{
		sourceDuDialogue = source;
		if(story == null)
		{
			currentStory = new Story(inkJSON.text);
		}
		else
		{
			currentStory = story;
		}
		
		cutsceneIsPlaying = true;
		estMessage = true;
		panelMessage.SetActive(true);
		choices = choixMessage;

		ContinueMessage();
	}

	private void ContinueMessage()
	{
		if(currentStory.canContinue)
		{
			HideChoices();
			zoneTexteMessage.text = currentStory.Continue();
			DisplayChoices();
			canContinueToNextLine = true;
		}
		else
		{
			StartCoroutine(ExitMessageMode());
		}
	}

	private IEnumerator ExitMessageMode()
	{
		yield return new WaitForSeconds(0.1f);

		cutsceneIsPlaying = false;
		panelMessage.SetActive(false);

		yield return new WaitForSeconds(0.1f);

		if (sourceDuDialogue != null)
		{
			sourceDuDialogue.finInteraction();
		}
	}

	/*---- Messages particuliers ----*/

	public void MessageObjetsObtenus(string nomObjet, int quantite, IInteractible source = null)
	{
		Story messageObjet = new Story(Instantiate(Resources.Load<TextAsset>("Dialogue/System/MessageObjetObtenu")).text);

		messageObjet.variablesState.SetGlobal("nomObjetObtenu", Ink.Runtime.StringValue.Create(nomObjet));
		messageObjet.variablesState.SetGlobal("quantite", Ink.Runtime.Value.Create(quantite));

		DisplayMessage(null, messageObjet, source);
	}
}