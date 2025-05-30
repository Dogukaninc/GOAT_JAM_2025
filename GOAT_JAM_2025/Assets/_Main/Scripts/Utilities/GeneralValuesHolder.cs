using Scripts.Utilities;
using UnityEngine;

public class GeneralValuesHolder : SingletonMonoBehaviour<GeneralValuesHolder>
{
    [field: SerializeField] public Player Player { get; private set; }
}