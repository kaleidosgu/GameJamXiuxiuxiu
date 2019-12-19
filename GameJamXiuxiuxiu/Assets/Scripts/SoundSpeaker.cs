using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpeaker : MonoBehaviour
{
    public AudioSource xiuSource;
    // Start is called before the first frame update
    void Start()
    {
        xiuSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayClip()
    {
        xiuSource.Play();
    }
}
