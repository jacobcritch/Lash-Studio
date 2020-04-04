using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientState
{
    WaitingForSpawn,
    WaitingForService,
    BeingServiced,
    WantsToMove,
    MovingToTarget,
    WantsToExit
}

