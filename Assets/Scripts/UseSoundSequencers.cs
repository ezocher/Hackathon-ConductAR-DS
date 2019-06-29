using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSoundSequencers : MonoBehaviour
{
    public GameObject soundSeq1;
    public GameObject soundSeq2;
    public GameObject soundSeq3;
    public GameObject soundSeq4;

    SoundSequencer sound1;
    SoundSequencer sound2;
    SoundSequencer sound3;
    SoundSequencer sound4;

    void Start()
    {
        sound1 = soundSeq1.GetComponent<SoundSequencer>();
        sound2 = soundSeq2.GetComponent<SoundSequencer>();
        sound3 = soundSeq3.GetComponent<SoundSequencer>();
        sound4 = soundSeq4.GetComponent<SoundSequencer>();

        // SampleOfUsage();
    }

    void SampleOfUsage()
    {
        sound1.StartWithClipNumber(0);
        sound2.StartWithClipNumber(0);

        sound1.SetNextClipNumber(1);
        sound2.SetNextClipNumber(1);
    }

    void StartAll()
    {
        sound1.StartWithClipNumber(0);
        sound2.StartWithClipNumber(0);
        sound3.StartWithClipNumber(0);
        sound4.StartWithClipNumber(0);
    }

    void PauseUnpauseAll()
    {
        sound1.TogglePlayPause();
        sound2.TogglePlayPause();
        sound3.TogglePlayPause();
        sound4.TogglePlayPause();
    }
}
