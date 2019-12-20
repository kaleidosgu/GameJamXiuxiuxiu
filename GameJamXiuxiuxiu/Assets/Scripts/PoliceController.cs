using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : MonoBehaviour
{
    public KeyCode KeyCodeRight;
    public KeyCode KeyCodeLeft;
    public KeyCode KeycodeCommand;
    // Start is called before the first frame update

    public Animator currentAnimator;
    public GameObject police;


    private SoundSpeaker m_speaker;

    void Start()
    {
        m_speaker = GetComponent<SoundSpeaker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCodeRight) == true)
        {
            if (currentAnimator.GetBool("changeFace"))
            {
                transform.Rotate(0, -90, 0);
                m_speaker.PlayClip();
            }
        }
        else if (Input.GetKeyDown(KeyCodeLeft) == true)
        {
            if (currentAnimator.GetBool("changeFace"))
            {
                transform.Rotate(0, 90, 0);
                m_speaker.PlayClip();
            }
               
        }
        else if (Input.GetKeyDown(KeycodeCommand) == true)
        {

            
            currentAnimator.Play("wave", 0, 0f);
           
            //transform.Find("police2").Rotate(0, 0, 0);
            if (currentAnimator.GetBool("changeFace"))
            {
                m_speaker.PlayClip();
                //currentAnimator.Play("beginWave");
                currentAnimator.SetBool("changeFace", false);
                //currentAnimator.SetBool("beginWave", true);
            }
                
        }
    }
}
