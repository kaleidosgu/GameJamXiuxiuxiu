using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode KeyCodeRight;
    public KeyCode KeyCodeLeft;
    public KeyCode KeycodeCommand;
    public RequestCommandMgr requestMgr;

    private GlobalDefine.PlayerDir m_playerDir;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCodeRight) == true)
        {
            requestMgr.AddCommand(new RequestChangePlayerDir(GlobalDefine.PlayerDir.PlayerDir_Right,this));
        }
        else if (Input.GetKeyDown(KeyCodeLeft) == true)
        {
            requestMgr.AddCommand(new RequestChangePlayerDir(GlobalDefine.PlayerDir.PlayerDir_Left, this));
        }
        else if( Input.GetKeyDown(KeycodeCommand) == true)
        {
            //requestMgr.AddCommand(new RequestChangeSpeed(GlobalDefine.CarMovementDir.)
        }
    }

    public void SetPlayerDir(GlobalDefine.PlayerDir _dir)
    {
        m_playerDir = _dir;
    }
}
