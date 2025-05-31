using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Template.AudioSystem.Scripts
{
    public enum SoundType
    {
        ButtonGeneric,
        ButtonUpgrade,
        ButtonClose, //or Cancel, negative events
        SpendMoney,
        CollectMoney,
        TractorIdle,
        Excavator,
        TakeAsset,
        GiveAsset,
        UnlockArea,
        OpenPanel,
        PlantCrop,
        WaterCrop,
        HarvestCrop
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private List<SoundData> soundDataList;

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;
        [SerializeField] private AudioSource truckIdleSource;
        [SerializeField] private AudioSource truckMoveSource;


        private readonly Dictionary<SoundType, SoundData> _soundDictionary = new();
        private readonly Dictionary<SoundType, float> _lastPlayTimes = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            foreach (var soundData in soundDataList)
            {
                _soundDictionary[soundData.type] = soundData;
                _lastPlayTimes[soundData.type] = 0f;
            }

            InitializeSettings();
            PlayMusic();
        }

        private void InitializeSettings()
        {
        }

        public void PlayMusic()
        {
            //if (musicSource.mute) return;

            musicSource.Play();
        }

        public void PlaySound(SoundType soundType)
        {
            if (soundSource.mute) return;

            if (!_soundDictionary.TryGetValue(soundType, out var sound))
            {
                Debug.LogError($"Sound {soundType} not found!");
                return;
            }

            var currentTime = Time.time;
            if (currentTime - _lastPlayTimes[soundType] < sound.interval)
            {
                return;
            }

            _lastPlayTimes[soundType] = currentTime;
            soundSource.pitch = sound.useRandomPitch ? Random.Range(sound.minPitch, sound.maxPitch) : 1.0f;
            soundSource.PlayOneShot(sound.clip, sound.volume);
        }

        public void SetMusic(bool value)
        {
            musicSource.mute = !value;
        }

        public void SetSound(bool value)
        {
            soundSource.mute = !value;
        }

        // public void OnEnteredTruck(FieldConfigSO _)
        // {
        //     if (soundSource.mute) return;
        //     
        //     truckIdleSource.volume = 0.2f;
        //     truckIdleSource.Play();
        // }
    }
}