using System.Collections.Generic;
using _Main.Scripts.Interface;
using UnityEngine;

public class LightStatue : MonoBehaviour, IInteractable
{
    /// <summary>
    /// 3 tane seam alacak player'dan, player yanına gelip e ye basılı tutunca birer birer seam'leri bırakacak.
    /// Toplam 3 tane seam olunca kapı açılma sekansı başlayacak.
    /// </summary>
   
    public List<GameObject> lightSeams;

    public void Interact()
    {
        
        
    }

    public void UnInteract()
    {
        
        
    }
    
}