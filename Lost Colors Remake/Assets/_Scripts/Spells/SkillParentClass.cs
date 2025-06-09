using NavMeshPlus.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public abstract class SkillParentClass
{
    protected string Name;
    protected delegate void Delegate();

    public SkillParentClass()
    {

    }

    public abstract void Activate(SkillContext context);

    protected void PlayVFX(VisualEffect vfx)
    {
        // jouer le VFX
    }

    protected GameObject SearchVFX(string vfxName)
    {
       GameObject newVFX = EnemyManager.Instance.SearchInPool(vfxName, EnemyManager.Instance.vfxPool);
       return newVFX;
    }

    protected void PlaySFX(AudioClip sfx)
    {
        // jouer le son dans le sound manager
    }

    protected void PlayAnimation(AnimationClip animation)
    {
        // jouer l'animation
    }

    #region Utilites
    protected async void DelayedFunction(Delegate function, float time)
    {
        await Task.Delay((int)(1000 * time));
        function();
    }
    protected async void DelayedFunction(Delegate[] functions, float time)
    {
        await Task.Delay((int)(1000 * time));
        foreach (Delegate function in functions)
        {
            function();
        }
    }
    #endregion

    #region Subskills
    protected void Dash(Rigidbody2D rb, Vector2 direction, float force)
    {
        Debug.Log("Skill : Dash");
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        //Delegate[] functions = { PrintRandomTest, PrintRandomTest, PrintRandomTest };
        //DelayedFunction(functions, 1f);
    }

    protected void EnemyDash(NavMeshAgent agent, float force, float dashDuration)
    {
        agent.isStopped = false;
        float originalSpeed = agent.speed;

        agent.acceleration = 100;
        agent.angularSpeed = 720;
        agent.autoBraking = false;

        agent.speed = originalSpeed + force;
        Vector3 direction = (PlayerMain.Instance.PlayerGameObject.transform.position - PlayerMain.Instance.PlayerGameObject.transform.position).normalized;
        Vector3 overshootTarget = PlayerMain.Instance.PlayerGameObject.transform.position + direction * 5f;
        agent.SetDestination(overshootTarget);

        DOTween.To(() => agent.speed, x => agent.speed = x, originalSpeed, dashDuration)
               .SetEase(Ease.OutCubic)
               .OnComplete(() => {
                   agent.speed = originalSpeed;
               });
    }

    protected void StopRigidBody(Rigidbody2D rb)
    {
        rb.angularVelocity = 0;
        rb.linearVelocity = new Vector2(0,0);

    }

    protected void PrintRandomTest()
    {
        string[] textDatas = { "zarma", "tout roule", "bigre", "zebi", "corneguidouille", "fichtre" };
        string text = textDatas[Random.Range(0, textDatas.Length)];
        Debug.Log(text);
    }

    protected GameObject GetProjectile(string projectileName)
    {
        return ProjectileManager.Instance.ProjectilePools[projectileName].GetObject();
    }

    protected void AlignToPlayerTransform(Transform transform)
    {
        Transform targetTransform = PlayerMain.Instance.ProjectileSocket.transform;
        transform.localPosition = targetTransform.position;
        transform.rotation = targetTransform.parent.rotation;
    }

    protected void AlignToCameraTransform(Transform transform)
    {
        AlignToTransform(transform, Camera.main.transform);
    }

    protected void AlignToTransform(Transform transform, Transform target)
    {
        transform.localPosition = target.position;
        transform.rotation = target.rotation;
    }

    protected List<Vector2> Vector2AroundPlayer(Vector2 playerPos, float maxDistanceWithPlayer, float numberOfTime)
    {
        List<Vector2> vector2List = new List<Vector2>();
        for (int i = 0; i < numberOfTime; i++)
        {
            vector2List.Add(new Vector2(playerPos.x + Random.Range(-maxDistanceWithPlayer, maxDistanceWithPlayer), playerPos.y + Random.Range(-maxDistanceWithPlayer, maxDistanceWithPlayer)));
        }
        return vector2List;
    }

    protected void /*Vector3*/ GetNearestEnemyPosition()
    {
        // Récupérer la positon de l'ennemi le plus proche
    }
    #endregion
}

/// <summary>
/// The fourre-tout class that transmit to skills all the stuff they need.
/// </summary>
public class SkillContext
{
    public Rigidbody2D Rigidbody2D;
    public GameObject Caster;
    public Vector3 Direction;
    public float Strength;
    public float MaxDistance;
    public NavMeshAgent Agent;

    // Constructeur qui permet d'injecter que les données dont on a besoin
    public SkillContext(Rigidbody2D _rigidbody2D = null, GameObject caster = null, Vector2? direction = null, float strength = 0f, float maxDistance = 0, NavMeshAgent agent = null) // Vector3? est un Nullable, d'où le cast en vector3 après (évite les problèmes de Vector3 inconstant)
    {
        Rigidbody2D = _rigidbody2D;
        Caster = caster;
        Direction = (Vector2)direction;
        Strength = strength;
        MaxDistance = maxDistance;
        Agent = agent;
    }
}