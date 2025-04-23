using UnityEngine;
public enum MovementState
{
    Other,
    Idle,
    Walking,
    Running,
    JumpingUp,
    Falling,
    SitDown
}

public enum SpendingEnergyState
{
    Yes,
    No
}

public enum IsFightingState
{
    Yes,
    No
}

public enum IsCastingSpellState
{
    Yes,
    No
}

public enum IsBlockingState
{
    Yes,
    No
}
public enum PlayerDeadState 
{ 
    Dead,
    Alive
}

public enum CameraState 
{
    Default,
    Combat
}


public static class StateManager
{
    public static MovementState playerCurrentState = MovementState.Idle;
    public static PlayerDeadState playerDeadState = PlayerDeadState.Alive;
    public static CameraState cameraCurrentState = CameraState.Default;
    public static SpendingEnergyState spendingEnergyCurrentState = SpendingEnergyState.No;
    public static IsFightingState isFightingState = IsFightingState.No;
    public static IsCastingSpellState isCastingSpellState = IsCastingSpellState.No;
    public static IsBlockingState isBlocking = IsBlockingState.No;
    public static bool dealDamage = false;
    public static bool getHit = false;
    public static bool safeZone = false;
}



