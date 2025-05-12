 using UnityEngine;

public interface IAState
{
    void Enter();
    void Execute();
    void Exit();
}

