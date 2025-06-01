using _Main.Scripts.Interface;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace _Main.Scripts.Props
{
    public class LightSeam : MonoBehaviour
    {


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player") {
                transform.DOJump(other.transform.position, 3f, 1, 0.5f).OnComplete(() =>
                {
                    other.GetComponent<Player>().LightSeams.Add(this.gameObject);
                    gameObject.SetActive(false);
                });
            }
        }


    }
}