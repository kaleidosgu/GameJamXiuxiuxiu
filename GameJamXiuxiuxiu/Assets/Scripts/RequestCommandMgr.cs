﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCommandMgr : MonoBehaviour
{
    public float TimeToExecute;

    public Transform PlayerTrans;

    public SoundSpeaker SndSpeaker;
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
                _changeDir.ExecuteCmd(PlayerTrans);
                m_lstCommand.RemoveAt(0);
                SndSpeaker.PlayClip();
            }
        }
    }

    public void AddCommand(RequestCommand _cmd)
    {
        m_lstCommand.Add( _cmd );
    }
}
