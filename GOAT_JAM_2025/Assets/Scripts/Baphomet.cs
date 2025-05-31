using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Baphomet : MonoBehaviour
{
    [SerializeField] private string[,] speech = new string[3,7] { { "Use WASD to move, rotate mouse to look", "Use your Right Mouse Button to open your lantern." , "Enemies becomes vulnerable after exposed to light for a while but be careful there is a limit on your lantern that regens over time","You can Use Right Mouse Button to shoot bolts","afer killing certain amount of enemies you will get enough light energy to fill the statuse and bring the light to the room","It is your holy mission to bring the light to the every single corner of this lightless world","" },
        {"eskici geldieeeaaahhh","","","","","",""},{"asdasdasd","","","","","","" } };
    private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI speechBubble;
    private int levelCount = 0;
    private int speechCount = 0;
    private bool speaking = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if(speaking && Mouse.current.leftButton.wasPressedThisFrame) { 
            nextSpeech();
        }


    }
    private void Start()
    {
        OnEnterNextStage();
    }
    public void OnEnterNextStage()
    {
        //stop character movement
        speechBubble.text = speech[levelCount, speechCount];
        rectTransform.DOMoveX(rectTransform.position.x + Screen.width / 2, 0.5f).OnComplete(() =>Time.timeScale = 0);
        speaking = true;
        speechCount = 0;
    }
    private void nextSpeech()
    {
        speechCount++;
        if(speech[levelCount,speechCount] != "")
        {
        speechBubble.text = speech[levelCount,speechCount];
        }
        else
        {
            //start character movement
            levelCount++;
            speaking = false;
            Time.timeScale = 1;
            rectTransform.DOMoveX(rectTransform.position.x - Screen.width / 2, 0.5f);

        }
    }
    
}
