using UnityEngine;
using DG.Tweening;

//namespace _Main.Scripts.Props
//{
public class LightSeam : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            transform.DOJump(other.transform.position, 2f, 1, 0.5f).OnComplete(() =>
            {
                other.GetComponent<Player>().LightSeams.Add(this.gameObject);
                gameObject.SetActive(false);
            });
        }
    }
}
//}