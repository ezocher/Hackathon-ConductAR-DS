using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundSequencer : MonoBehaviour
{
    public AudioClip[] clips;
    int currentClipNumber;  // Clips are numbered 0, 1, 2, ... 9
    int nextClipNumber;
    bool musicPlaying = false;

    const bool KEYBOARD_ENABLED = true;

    public void Awake()
    {
        // Preload all clips
        AudioSource sound = this.GetComponent<AudioSource>();
        for (int clipNumber = 0; clipNumber < clips.Length; clipNumber++)
            sound.clip = clips[clipNumber];
        Debug.Log("All clips loaded");
    }

    public void Start()
    {
        if (KEYBOARD_ENABLED)
            StartWithClipNumber(0);
    }

    public void StartWithClipNumber(int i)
    {
        AudioSource sound;

        nextClipNumber = currentClipNumber = i;
        sound = this.GetComponent<AudioSource>();
        sound.clip = clips[currentClipNumber];
        sound.loop = false;

        sound.Play();
        musicPlaying = true;
        Debug.Log("> Playing clip " + currentClipNumber);
    }

    public void TogglePlayPause()
    {
        Debug.Log("= Play/Pause");
        if (musicPlaying)
            this.GetComponent<AudioSource>().Pause();
        else
            this.GetComponent<AudioSource>().UnPause();

        musicPlaying = !musicPlaying;
    }

    public void SetNextClipNumber(int i)
    {
        nextClipNumber = i;
        Debug.Log("* Next clip set to: " + nextClipNumber);
    }

    private void CheckAndStartNextClip()
    {
        AudioSource sound = this.GetComponent<AudioSource>();

        if (musicPlaying && !sound.isPlaying)
        {
            Debug.Log("> Clip " + currentClipNumber + " finished, playing clip " + nextClipNumber);
            currentClipNumber = nextClipNumber;


            sound.clip = clips[currentClipNumber];
            sound.loop = false;
            sound.Play();

        }
    }

    void FixedUpdate()
    {
        CheckAndStartNextClip();
    }

    void Update()
    {
        if (KEYBOARD_ENABLED)
        {
            updateCounter++;
            CheckForKeyboardInput();
        }
    }

/*
    void ChangeVolume(float amount)
    {
        this.GetComponent<AudioSource>().volume += amount;

        Debug.Log("Volume change: " + amount + "(" + updateCounter + ")");
    }


    void ChangePitch(float amount)
    {
        float change = amount * pitchDelta;
        this.GetComponent<AudioSource>().pitch += change;

        Debug.Log("Pitch change: " + change + "(" + updateCounter + ")");
    }
*/

    // -------------- Keyboard input -------------- 
    int updateCounter = 0;  // For keyboard input
    int lastKeyCounter = 0;
    KeyCode[] numericKeyCodes = { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
                                  KeyCode.Alpha8, KeyCode.Alpha9 };


    private void CheckForKeyboardInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (DebounceKeys())
                TogglePlayPause();
        }

        for (int keyNumber = 0; keyNumber < clips.Length; keyNumber++)
        {
            if (Input.GetKey(numericKeyCodes[keyNumber]))
            {
                if (DebounceKeys())
                    SetNextClipNumber(keyNumber);
            }
        }
    }

    private bool DebounceKeys()
    {
        if (updateCounter == (lastKeyCounter + 1))
        {
            lastKeyCounter = updateCounter;
            return false;
        }
        else
        {
            lastKeyCounter = updateCounter;
            return true;
        }
    }
}
