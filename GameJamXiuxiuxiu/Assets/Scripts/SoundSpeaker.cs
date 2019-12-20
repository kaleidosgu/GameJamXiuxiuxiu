using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpeaker : MonoBehaviour
{
    private AudioSource[] m_lstSwishSound;
    private int m_nLstCounts;
    // Start is called before the first frame update
    void Start()
    {
        m_lstSwishSound = GetComponents<AudioSource>();
        m_nLstCounts = m_lstSwishSound.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayClip()
    {
        int nIdx = Random.Range(0, m_nLstCounts);
        m_lstSwishSound[nIdx].Play();
    }
}
