using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlsPauseScript : MonoBehaviour
{
    #region Variables
    [SerializeField]
    InputField walkUp;
    [SerializeField]
    InputField walkDown;
    [SerializeField]
    InputField walkRight;
    [SerializeField]
    InputField walkLeft;
    [SerializeField]
    InputField dash;
    [SerializeField]
    InputField basicAttack;
    [SerializeField]
    InputField specialAbility;


    InputField currentInputField;
    InputField[] inputFieldArray;
    #endregion

    private void Start()
    {
        inputFieldArray =  new InputField[7] { walkUp, walkDown, walkRight, walkLeft, dash, basicAttack, specialAbility };
    }

    void Update ()
    {
        if (currentInputField != null)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    foreach (InputField i in inputFieldArray)
                    {
                        if (i.text.Equals(kcode.ToString()))
                        {
                            i.text = currentInputField.text;
                        }
                    }
                    currentInputField.text = kcode.ToString();
                }
            }
        }
    }

    public void SetCurrentInpuFieldNull()
    {
        currentInputField = null;
    }

    public void WalkUpButtonSelected()
    {
        currentInputField = walkUp;
    }
    
    public void WalkDownButtonSelected()
    {
        currentInputField = walkDown;
    }

    public void WalkRightButtonSelected()
    {
        currentInputField = walkRight;
    }

    public void WalkLeftButtonSelected()
    {
        currentInputField = walkLeft;
    }

    public void DashButtonSelected()
    {
        currentInputField = dash;
    }

    public void BasicAttackButtonSelected()
    {
        currentInputField = basicAttack;
    }

    public void SpecialAbilityButtonSelected()
    {
        currentInputField = specialAbility;
    }
}
