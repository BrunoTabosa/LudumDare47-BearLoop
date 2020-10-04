using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINumpad : MonoBehaviour
{
    public TMP_InputField input;

    public void OnConfirmClick()
    {
        GameController.Instance.EnterCode(input.text);
        OnCancelClick();
    }

    public void OnCancelClick()
    {
        SetEnabled(false);
        input.text = string.Empty;
    }

    public void SetEnabled(bool value)
    {
        gameObject.SetActive(value);
    }
}
