using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextingSystemManager : MonoBehaviour
{
    public static TextingSystemManager Instance;
    public TextMeshProUGUI text;
    public float textSpeed = 0.1f;
    public string actualText;
    public string[] dialogLines;
    public int actualLine;
    public bool continueWhenOver;
    public float waitBetweenLine;
    int debug;
    public void Awake()
    {
        Instance = this;
    }
    public void OnLevelWasLoaded()
    {
        GameObject go = GameObject.FindGameObjectWithTag("TextDialog");
        text = go.GetComponent<TextMeshProUGUI>();
    }
    public void BeginDialogue()
    {
        StartCoroutine(ShowText(dialogLines[0]));
    }
    public void NextLine()
    {
        actualLine++;
        StartCoroutine(ShowText(dialogLines[actualLine]));
    }

    public IEnumerator ShowText(string textToShow)
    {
        text.text = "";
        actualText = "";
        foreach (char c in textToShow)
        {
            if (c != '$')
            {

                if (c != '@')
                {
                    if (c != '°')
                    {
                        if (c != '²')
                        {
                            actualText += c;
                            text.text = actualText;
                            yield return new WaitForSeconds(textSpeed);
                        }
                        else//Si c'est un ² (playercanmove true)
                        {

                            PlayerController.Instance.PlayerMovement.canMove = true;
                        }
                    }
                    else//Si c'est un ° (player can move false)
                    {

                        PlayerController.Instance.PlayerMovement.canMove = false;
                    }
                }
                else//Si c'est un @
                {
                    continueWhenOver = true;
                }
            }
            else//Si c'est un $
            {

                continueWhenOver = false;
            }

        }
        if (continueWhenOver)
        {
            yield return new WaitForSeconds(waitBetweenLine);
            NextLine();
        }
        else
        {
            yield return new WaitForSeconds(waitBetweenLine);
            text.text = "";
            actualText = "";
        }

    }
}
