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
    [SerializeField] private TextMeshProUGUI activeResourceLabel;
    [SerializeField] private TextMeshProUGUI resourcesLabel;
    [SerializeField] private TextMeshProUGUI resourcesValue;
    [SerializeField] private TextMeshProUGUI spiritLabel;
    [SerializeField] private TextMeshProUGUI spiritValue;
    [SerializeField] private TextMeshProUGUI mindsetLabel;
    [SerializeField] private TextMeshProUGUI mindsetValue;
    [SerializeField] private TextMeshProUGUI talentLabel;
    [SerializeField] private TextMeshProUGUI talentValue;

    [Header("Buttons")]
    [SerializeField] private Button activeResourceButton;
    [SerializeField] private Button resourcesButton;
    [SerializeField] private Button spiritButton;
    [SerializeField] private Button mindsetButton;
    [SerializeField] private Button talentButton;

    [Header("Images")]
    [SerializeField] private Image activeResourceBtnImage;
    [SerializeField] private Image resourcesBtnImage;
    [SerializeField] private Image spiritBtnImage;
    [SerializeField] private Image mindsetBtnImage;
    [SerializeField] private Image talentBtnImage;

    private enum ResourceType { Resources, Spirit, Mindset, Talent };

    private ResourceType activeResource = ResourceType.Resources;

    // Use this for initialization
    void Start ()
    {
        resourcesButton.onClick.AddListener(SetActive_Resources);
        spiritButton.onClick.AddListener(SetActive_Spirit);
        mindsetButton.onClick.AddListener(SetActive_Mindset);
        talentButton.onClick.AddListener(SetActive_Talent);
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateDisplay();	
	}

    private void UpdateDisplay()
    {
        if (gameManager.GameTimer != null)
            counterLabel.text = "+" + gameManager.GameTimer.Count.ToString();
    }

    private void SetActive_Resources() //Button onClick event to set Resources as active
    {
        UpdateActiveResourceDisplay(ResourceType.Resources);
    }

    private void SetActive_Spirit() //Button onClick event to set Spirit as active
    {
        UpdateActiveResourceDisplay(ResourceType.Spirit);
    }

    private void SetActive_Mindset() //Button onClick event to set Mindset as active
    {
        UpdateActiveResourceDisplay(ResourceType.Mindset);
    }

    private void SetActive_Talent() //Button onClick event to set Talent as active
    {
        UpdateActiveResourceDisplay(ResourceType.Talent);
    }

    private void UpdateActiveResourceDisplay(ResourceType activeResourceType) //Set the active resource button's visuals
    {
        activeResource = activeResourceType;

        activeResourceLabel.text = activeResource.ToString();

        switch (activeResource)
        {
            case ResourceType.Resources:
                activeResourceBtnImage.color = resourcesBtnImage.color;
                break;
            case ResourceType.Spirit:
                activeResourceBtnImage.color = spiritBtnImage.color;
                break;
            case ResourceType.Mindset:
                activeResourceBtnImage.color = mindsetBtnImage.color;
                break;
            case ResourceType.Talent:
                activeResourceBtnImage.color = talentBtnImage.color;
                break;
            default:
                break;
        }
    }
}
