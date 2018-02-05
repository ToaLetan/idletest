using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Included for Text components
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    [Header("TextMeshProUGUI Text")]
    [SerializeField] private TextMeshProUGUI counterLabel;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateDisplay();	
	}

    private void UpdateDisplay()
    {
        if (gameManager.GameTimer != null)
            counterLabel.text = gameManager.GameTimer.Count.ToString();
    }
}
