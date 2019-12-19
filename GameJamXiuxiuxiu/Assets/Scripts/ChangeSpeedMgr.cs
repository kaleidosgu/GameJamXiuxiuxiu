using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeSpeedMgr : MonoBehaviour
{
    private UnityAction<GlobalDefine.CarMovementDir> m_actChangeSpeed;
    // Start is called before the first frame update
    public void RegisteCar(UnityAction<GlobalDefine.CarMovementDir> _carAc)
    {
        m_actChangeSpeed += _carAc;
    }
    public void UnregisteCar(UnityAction<GlobalDefine.CarMovementDir> _carAc)
    {
        m_actChangeSpeed -= _carAc;
    }
    public void ExecuteSpeed(GlobalDefine.CarMovementDir _carDir)
    {
        m_actChangeSpeed.Invoke(_carDir);
    }
}
