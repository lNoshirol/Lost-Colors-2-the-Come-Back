using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomSwitcher", menuName = "Scriptable Objects/RoomSwitcher", order =1)]
public class RoomSwitcherDATA : ScriptableObject
{
    public string currentSceneName;         // "Forest_Main_01" "FR_M_01"
    public string switcherID;               // "Forest_Main_01_Right01" "FR_M_01_R01"

    public string targetSceneName;          // "Forest_Main_02"
    public string targetSwitcherID;         // "Left01"
}