using System.Collections.Generic;
using _Main.Scripts.Interface;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightStatue : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject cardHolder;
    [SerializeField] private GameObject MainCanvas;
    [SerializeField] private GameEvent StageEnd;
    [SerializeField] private Level levelOfStatue;
    
    public List<GameObject> lightSeams;
    public int lightSeamCountToPassLevel;
    public GameObject interactionInfoPanel;
    public bool isNowInteractable;
    private Player _player;

    private float takeTime = 0.5f;
    private float defaulttakeTime = 0.5f;

    private void Start()
    {
        _player = GeneralValuesHolder.Instance.Player;
        defaulttakeTime = takeTime;
    }

    public void Interact()
    {
        interactionInfoPanel.SetActive(true);
        isNowInteractable = true;
    }

    private void Update()
    {
        if (!isNowInteractable) return;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_player.LightSeams.Count > 0)
            {
                var seam = _player.LightSeams[^1];
                seam.gameObject.SetActive(true);
                seam.transform.position = _player.transform.position;
                seam.transform.DOJump(transform.position, 1, 1, 1).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    lightSeams.Add(seam);
                    seam.SetActive(false);
                    _player.LightSeams.Remove(seam);
                });
            }
        }

        if (lightSeams.Count >= lightSeamCountToPassLevel)
        {
            Instantiate(cardHolder, MainCanvas.transform);
            StageEnd.Raise(this, null);
            this.enabled = false;
            levelOfStatue.isLevelFinished = true;
        }
    }

    public void UnInteract()
    {
        interactionInfoPanel.SetActive(false);
        isNowInteractable = false;
    }
}