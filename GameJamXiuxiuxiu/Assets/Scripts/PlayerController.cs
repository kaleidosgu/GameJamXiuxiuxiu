using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode KeyCodeRight;
    public KeyCode KeyCodeLeft;
    public KeyCode KeycodeCommand;
    public RequestCommandMgr requestMgr;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCodeRight) == true)
        {
            requestMgr.AddCommand(new RequestChangePlayerDir(GlobalDefine.PlayerDir.PlayerDir_Right));
            //transform.Rotate(0, -90, 0);
        }
        else if (Input.GetKeyDown(KeyCodeLeft) == true)
        {
            requestMgr.AddCommand(new RequestChangePlayerDir(GlobalDefine.PlayerDir.PlayerDir_Left));
            //transform.Rotate(0, 90, 0);
        }
        else if( Input.GetKeyDown(KeycodeCommand) == true)
        {
            //m_speaker.PlayClip();
        }
    }
}
