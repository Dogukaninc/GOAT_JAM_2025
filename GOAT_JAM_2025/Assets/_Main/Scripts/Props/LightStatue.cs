using System;
using System.Collections.Generic;
using _Main.Scripts.Interface;
using _Main.Scripts.Props;
using DG.Tweening;
using UnityEngine;

public class LightStatue : MonoBehaviour, IInteractable
{
    public List<LightSeam> lightSeams;
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
                        AddSeamToStatue(seam);
                        _player.LightSeams.Remove(seam);
                    });

                    Debug.Log("<color=red> Light Seam Taken From Player </color>");
                }
            }
        }

        if (lightSeams.Count >= lightSeamCountToPassLevel)
        {
            Debug.Log("KAPIYI AÇÇÇÇ IŞIKLAR TOPLANDIII");
        }
    }

    public void UnInteract()
    {
        interactionInfoPanel.SetActive(false);
        isNowInteractable = false;
    }

    public void AddSeamToStatue(LightSeam seam)
    {
        if (lightSeams.Count < lightSeamCountToPassLevel)
        {
            lightSeams.Add(seam);
        }
    }
}