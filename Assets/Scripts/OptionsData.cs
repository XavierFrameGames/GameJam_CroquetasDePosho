using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OptionsData
{
    [SerializeField]
    private float musicVolume;
    [SerializeField]
    private float sfxVolume;

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }
    public float SfxVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = value; }
    }

    public OptionsData()
    {
        musicVolume = 1;
        sfxVolume = 1;
    }
}
