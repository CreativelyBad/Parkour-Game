using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    private Canvas canvas;
    public LevelManager levelManager;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        inputField.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            inputField.enabled = !inputField.enabled;
        }
    }

    public void OnEndEdit(string input)
    {
        Submit(input);

        inputField.text = "";
    }

    private void Submit(string command)
    {
        if (command == "test lvl 2")
        {
            
        }
    }
}
