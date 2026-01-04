using UnityEngine;
using UnityEngine.UI;
public class InputFocus : MonoBehaviour
{

    public InputField nameInputField;

    void Start()
    {
        // InputFieldを自動でフォーカスする
        nameInputField.Select();
        nameInputField.ActivateInputField();

    }
}
