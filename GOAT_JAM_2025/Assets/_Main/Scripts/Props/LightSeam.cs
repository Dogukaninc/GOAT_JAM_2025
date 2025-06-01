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
                StartCoroutine(collect(other));
            }
        }

        private IEnumerator collect(Collider other)
        {
            transform.DOJump(other.transform.position, 3f, 1, 0.5f).OnComplete(() => other.GetComponent<Player>().LightSeams.Add(this));
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
}