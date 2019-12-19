using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode KeyCodeRight;
    public KeyCode KeyCodeLeft;
    public KeyCode KeycodeCommand;

    private SoundSpeaker m_speaker;
    // Start is called before the first frame update
    void Start()
    {
        m_speaker = GetComponent<SoundSpeaker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCodeRight) == true)
        {
            transform.Rotate(0, -90, 0);
            m_speaker.PlayClip();
        }
        else if (Input.GetKeyDown(KeyCodeLeft) == true)
        {
            transform.Rotate(0, 90, 0);
            m_speaker.PlayClip();
        }
        else if( Input.GetKeyDown(KeycodeCommand) == true)
        {
            m_speaker.PlayClip();
        }
    }
}
