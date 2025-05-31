using UnityEngine;

namespace _Template.AudioSystem.Scripts
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Audio/SoundData")]
    public class SoundData : ScriptableObject
    {
        public SoundType type;
        public AudioClip clip;
        public float volume = 1.0f;
        public bool useRandomPitch;
        [Range(-3f, 3f)] public float minPitch;
        [Range(-3f, 3f)] public float maxPitch;
        public float interval;

        private void OnValidate()
        {
            if (useRandomPitch && minPitch > maxPitch)
            {
                minPitch = maxPitch;
            }
        }
    }
}