using UnityEngine;


public abstract class EnemiesState : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;

    public float time => Time.time - startTime;

    public virtual void OnEnter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void OnExit() { }

    protected EnemyMain EnemiesMain;



    public void Setup(EnemyMain _enemiesMain)
    {
        EnemiesMain = _enemiesMain;
    }
}
