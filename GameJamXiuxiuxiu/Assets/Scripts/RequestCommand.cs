using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCommand 
{
    public enum RequestCommand_Type
    {
        RequestCommand_Type_ChangeNone = 0,
        RequestCommand_Type_ChangePlayerDir = 1,
        RequestCommand_Type_ChangeSpeed = 2,
    }
    protected RequestCommand_Type m_Typ;
    public RequestCommand()
    {

    }

    public RequestCommand_Type GetCmdTyp()
    {
        return m_Typ;
    }

}

public class RequestChangePlayerDir : RequestCommand
{
    private GlobalDefine.PlayerDir m_playerDir;
    private PlayerController m_ctrl;
    public RequestChangePlayerDir(GlobalDefine.PlayerDir _dir, PlayerController _ctrl)
    {
        m_Typ = RequestCommand_Type.RequestCommand_Type_ChangePlayerDir;
        m_playerDir = _dir;
        m_ctrl = _ctrl;
    }
    public GlobalDefine.PlayerDir GetPlayerDir()
    {
        return m_playerDir;
    }
    public void ExecuteCmd(Transform TransPlayer)
    {
        m_ctrl.SetPlayerDir(m_playerDir);
    }
}

public class RequestChangeSpeed: RequestCommand
{
    private GlobalDefine.CarMovementDir m_dir;
    public RequestChangeSpeed(GlobalDefine.CarMovementDir _dir)
    {
        m_Typ = RequestCommand_Type.RequestCommand_Type_ChangeSpeed;
        m_dir = _dir;
    }
    public GlobalDefine.CarMovementDir GetCarDir()
    {
        return m_dir;
    }

}

