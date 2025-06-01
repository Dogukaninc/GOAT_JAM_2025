using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Level : MonoBehaviour
    {
        public List<GameObject> roomLights;
        public GameEvent onLevelEnd;
        public GameObject doorLock;
        public ParticleSystem doorEffect;
        public bool isLevelFinished = false;

        public void OpenDoorLock(Component sender,object args)
        {
            doorEffect.Play();
            doorLock.SetActive(false);
            StartCoroutine(LightSeq());
            Debug.Log("IŞIKLARGELDİİİ");
        }

        public IEnumerator LightSeq()
        {
            for (int i = 0; i < roomLights.Count; i++)
            {
                roomLights[i].SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}