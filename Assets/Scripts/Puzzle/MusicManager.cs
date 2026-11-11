using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicManager : MonoBehaviour
{
    private StudioEventEmitter[] musicEmitters;
    private bool banksAreLoaded;
    private bool musicStarted;

    private IEnumerator Start()
    {
        musicEmitters = GetComponents<StudioEventEmitter>();

        // FMOD bank loading is asynchronous in WebGL.
        while (!RuntimeManager.HaveAllBanksLoaded)
        {
            yield return null;
        }

        banksAreLoaded = true;
    }

    private void Update()
    {
        if (!banksAreLoaded || musicStarted || Mouse.current == null ||
            !Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        // Start on a real click so browsers allow WebGL audio playback.
        foreach (StudioEventEmitter musicEmitter in musicEmitters)
        {
            musicEmitter.Play();
        }

        musicStarted = true;
    }
}
