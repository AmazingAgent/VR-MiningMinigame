using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSounds : MonoBehaviour
{
    [SerializeField] private AudioSource slamAudio;
    [SerializeField] private AudioSource moveAudio;
    //[SerializeField] private WallVisibility wallVisibility;
    //private bool isAtTop = false;
    //private bool isClosed = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlaySlam()
    {
        slamAudio.Play();
    }

    public void PlayMove()
    {
        moveAudio.Play();
    }

    public void StopMove()
    {
        moveAudio.Stop();
    }
}
