using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDefine 
{
    [System.Serializable]
    public enum CarMovementDir
    {
        CarMovementDir_North = 1,
        CarMovementDir_South = 2,
        CarMovementDir_East = 3,
        CarMovementDir_West = 4,
    }
    [System.Serializable]
    public enum PlayerDir
    {
        PlayerDir_Right = 1,
        PlayerDir_Left = 2,
    }
}
