using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixer audioMixer;           // 北 Mixer 跑秖                                     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(float volume)       // 北秖ㄧ计
    {
        audioMixer.SetFloat("MasterVolume", volume);
        // MasterVolume 忌臩ㄓ Master 把计
    }

    public void SetGameSoundVolume(float volume)        // 北璉春贾秖ㄧ计
    {
        audioMixer.SetFloat("GameSoundVolume", volume);
        // MusicVolume 忌臩ㄓ Music 把计
    }

    public void SetSoundEffectVolume(float volume)  // 北秖ㄧ计
    {
        audioMixer.SetFloat("SoundEffectVolume", volume);
        // SoundEffectVolume 忌臩ㄓ SoundEffect 把计
    }
}
