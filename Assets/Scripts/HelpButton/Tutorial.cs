using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private HelpButton helpButton;
    private bool helpButtonPressed = false;

    [SerializeField] private int instructionNum = 0;
    [SerializeField] private GameObject[] instructions;


    [SerializeField] private PickaxeTutorial pickaxeTutorial;
    void Start()
    {
        UpdateInstructions();
    }
    IEnumerator ResetButton()
    {
        //Debug.Log("Start button reset");
        yield return new WaitForSeconds(1f);
        helpButton.ActivateButton();
        helpButtonPressed = false;
        //Debug.Log("End button reset");
    }


    private void UpdateInstructions()
    {
        foreach (GameObject instruction in instructions)
        {
            instruction.SetActive(false);
        }
        instructions[instructionNum].SetActive(true);
    }
    private void NextInstruction()
    {
        instructionNum++;
        UpdateInstructions();
    }


    public void StartTutorial()
    {
        if (!helpButtonPressed)
        {
            if (instructionNum > 0)
            {
                Debug.Log("Reset tutorial");
                instructionNum = 0;
                UpdateInstructions();
                helpButtonPressed = true;
                StartCoroutine(ResetButton());
            }
            else
            {
                Debug.Log("Begin tutorial");
                instructionNum = 0;
                NextInstruction();
                helpButtonPressed = true;
                StartCoroutine(ResetButton());
            }
        }
    }

    public void AttemptPickaxeGrab()
    {
        if (instructionNum == 1)
        {
            NextInstruction();
        }
        if (instructionNum == 6)
        {
            AttemptEndTutorial();
        }
    }

    public void AttemptDamageGrid()
    {
        if (instructionNum == 2)
        {
            NextInstruction();
        }
    }

    public void AttemptGrabItem()
    {
        if (instructionNum == 3)
        {
            NextInstruction();
        }
    }

    public void AttemptPlaceItemDown()
    {
        if (instructionNum == 4)
        {
            NextInstruction();
        }
    }

    public void AttemptSellItem()
    {
        if (instructionNum == 5)
        {
            NextInstruction();
        }
    }

    public void AttemptEndTutorial()
    {
        if (instructionNum == 6)
        {
            pickaxeTutorial.ActivateTutorial();
            instructionNum = 0;
            UpdateInstructions();
        }
    }
}
