using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCommandMgr : MonoBehaviour
{
    public float TimeToExecute;
    public PlayerController PlayerCtrl;
    public SoundSpeaker SndSpeaker;
    public ChangeSpeedMgr ChangeSpeedMgr;
    public int CountsOfLst;
    public bool ExecuteWestAndEast;

    private float m_fCurrentTimeToExecute;
    private List<RequestCommand> m_lstCommand;
    // Start is called before the first frame update
    void Start()
    {
        m_lstCommand = new List<RequestCommand>();
    }

    // Update is called once per frame
    void Update()
    {
        m_fCurrentTimeToExecute += Time.deltaTime;
        if( m_fCurrentTimeToExecute >= TimeToExecute)
        {
            m_fCurrentTimeToExecute -= TimeToExecute;
            _executeCommand();
        }
    }

    private void _executeCommand()
    {
        if(m_lstCommand.Count > 0)
        {
            if ( m_lstCommand[0].GetCmdTyp() == RequestCommand.RequestCommand_Type.RequestCommand_Type_ChangePlayerDir )
            {
                RequestChangePlayerDir _changeDir = (RequestChangePlayerDir)m_lstCommand[0];
                _changeDir.ExecuteCmd(PlayerCtrl.transform);
            }
            else if (m_lstCommand[0].GetCmdTyp() == RequestCommand.RequestCommand_Type.RequestCommand_Type_ChangeSpeed)
            {
                RequestChangeSpeed _changeSpeed = (RequestChangeSpeed)m_lstCommand[0];
                if(_changeSpeed.GetCarDir() == GlobalDefine.CarMovementDir.CarMovementDir_East || _changeSpeed.GetCarDir() == GlobalDefine.CarMovementDir.CarMovementDir_West)
                {
                    if( ExecuteWestAndEast == true )
                    {
                        ChangeSpeedMgr.ExecuteSpeed(_changeSpeed.GetCarDir());
                    }
                }
                else
                {
                    ChangeSpeedMgr.ExecuteSpeed(_changeSpeed.GetCarDir());
                }
            }
            else
            {
                Debug.Assert(false);
            }
            m_lstCommand.RemoveAt(0);
            SndSpeaker.PlayClip();
			PlayerCtrl.PlayWaveAni();
        }
    }

    public void AddCommand(RequestCommand _cmd)
    {
        int nCounts = m_lstCommand.Count;
        if(nCounts < CountsOfLst )
        {
        }
        else
        {
            m_lstCommand.RemoveAt(nCounts - 1);
        }
        m_lstCommand.Add(_cmd);
    }
}
