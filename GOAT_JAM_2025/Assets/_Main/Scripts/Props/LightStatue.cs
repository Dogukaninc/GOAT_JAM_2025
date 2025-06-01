using System.Collections.Generic;
using _Main.Scripts.Interface;
using DG.Tweening;
using UnityEngine;

public class LightStatue : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject cardHolder;
    [SerializeField] private GameEvent StageEnd;
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
        if (Input.GetKey(KeyCode.E))
        {
            takeTime -= Time.deltaTime;
            if (takeTime <= 0)
            {
                if (_player.LightSeams.Count > 0)
                {
                    var seam = _player.LightSeams[^1];
                    seam.gameObject.SetActive(true);
                    seam.transform.DOJump(transform.position, 1, 1, 1).SetEase(Ease.InQuad).OnComplete(() =>
                    {
                        _player.LightSeams.Remove(seam);
                        lightSeams.Add(seam);
                        seam.SetActive(false);
                    });

                    Debug.Log("<color=red> Light Seam Taken From Player </color>");
                }
            }
        }

        if (lightSeams.Count >= lightSeamCountToPassLevel)
        {
            Instantiate(cardHolder);
            StageEnd.Raise(this,null);
        }
    }

    public void UnInteract()
    {
        interactionInfoPanel.SetActive(false);
        isNowInteractable = false;
    }
}