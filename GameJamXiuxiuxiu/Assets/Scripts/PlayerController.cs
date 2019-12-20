using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirSets
{
    public GlobalDefine.CarMovementDir CheckDir;
    public GlobalDefine.CarMovementDir RightDir;
    public GlobalDefine.CarMovementDir LeftDir;

    public DirSets(GlobalDefine.CarMovementDir _checkDir, GlobalDefine.CarMovementDir _rightDir, GlobalDefine.CarMovementDir _leftDir)
    {
        CheckDir = _checkDir;
        RightDir = _rightDir;
        LeftDir = _leftDir;
    }
}
public class PlayerController : MonoBehaviour
{
    public KeyCode KeyCodeRight;
    public KeyCode KeyCodeLeft;
    public KeyCode KeycodeCommand;
    public RequestCommandMgr requestMgr;

    public Animator currentAnimator;

    private List<DirSets> m_lstDirSets;

    private GlobalDefine.PlayerDir m_playerDir;
    private GlobalDefine.CarMovementDir m_defaultDir;
    // Start is called before the first frame update
    void Start()
    {
        m_defaultDir = GlobalDefine.CarMovementDir.CarMovementDir_East;
        m_lstDirSets = new List<DirSets>();
        m_lstDirSets.Add(new DirSets(GlobalDefine.CarMovementDir.CarMovementDir_North, GlobalDefine.CarMovementDir.CarMovementDir_East, GlobalDefine.CarMovementDir.CarMovementDir_West));
        m_lstDirSets.Add(new DirSets(GlobalDefine.CarMovementDir.CarMovementDir_South, GlobalDefine.CarMovementDir.CarMovementDir_West, GlobalDefine.CarMovementDir.CarMovementDir_East));
        m_lstDirSets.Add(new DirSets(GlobalDefine.CarMovementDir.CarMovementDir_East, GlobalDefine.CarMovementDir.CarMovementDir_South, GlobalDefine.CarMovementDir.CarMovementDir_North));
        m_lstDirSets.Add(new DirSets(GlobalDefine.CarMovementDir.CarMovementDir_West, GlobalDefine.CarMovementDir.CarMovementDir_North, GlobalDefine.CarMovementDir.CarMovementDir_South));
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
            requestMgr.AddCommand(new RequestChangeSpeed(m_defaultDir));
        }
    }

    public void SetPlayerDir(GlobalDefine.PlayerDir _dir)
    {
        if (_dir == GlobalDefine.PlayerDir.PlayerDir_Right)
        {
            transform.rotation *= Quaternion.Euler(0, 90, 0);
        }
        else if (_dir == GlobalDefine.PlayerDir.PlayerDir_Left)
        {
            transform.rotation *= Quaternion.Euler(0, -90, 0);
        }
        else
        {
            Debug.Assert(false);
        }
        _changeSelfDir(_dir);
    }
	public void PlayWaveAni()
	{
		currentAnimator.Play("wave", 0, 0f);
	}
    private void _changeSelfDir(GlobalDefine.PlayerDir _dir)
    {
        foreach(DirSets _set in m_lstDirSets)
        {
            if(m_defaultDir == _set.CheckDir)
            {
                if(_dir == GlobalDefine.PlayerDir.PlayerDir_Right)
                {
                    m_defaultDir = _set.RightDir;
                    break;
                }
                else if( _dir == GlobalDefine.PlayerDir.PlayerDir_Left)
                {
                    m_defaultDir = _set.LeftDir;
                    break;
                }
            }
        }
    }
}
