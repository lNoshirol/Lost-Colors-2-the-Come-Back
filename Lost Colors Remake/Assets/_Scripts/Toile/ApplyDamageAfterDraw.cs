using System;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageAfterDraw : MonoBehaviour
{
    public static ApplyDamageAfterDraw Instance;
    
    [SerializeField] private List<DamageStructModel> _damageToEnemy = new ();
    [SerializeField] private List<string> _enemyArmorToTej = new();
    private List<DrawData> _drawVfxToPlay = new();

    public int damageToEnemyCount = 0;
    public List<EnemyHealth> _tabassedEnemy = new();

    public event Action<List<DrawData>> VfxToPlay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DrawForDollarP.instance.OnDrawFinish += AddVfxToPlay;
    }

    private void Update()
    {
        damageToEnemyCount = _damageToEnemy.Count;
    }

    public void AddEnnemyDamage(EnemyHealth enemy, float damage, DrawData drawData)
    {
        _damageToEnemy.Add(new(enemy, damage, drawData));
    }

    public void AddEnemyGlyphToTej(string glyphName)
    {
        _enemyArmorToTej.Add(glyphName);
    }

    public void AddVfxToPlay(DrawData _drawData)
    {
        if (!_drawVfxToPlay.Contains(_drawData))
        {
            _drawVfxToPlay.Add(_drawData);
        }
    }

    public void ApplyDamage()
    {
        //Debug.Log($"tabasse tout le monde {_damageToEnemy.Count}");
        
        _tabassedEnemy.Clear();

        foreach(DamageStructModel model in _damageToEnemy)
        {
            model.targetedEnemy.EnemyLoseHP(model.damage);
            _tabassedEnemy.Add(model.targetedEnemy);

        }

        _damageToEnemy.Clear();
    }

    public void TejArmor()
    {
        //Debug.Log("Tej armor");

        foreach(string glyphName in _enemyArmorToTej)
        {
            EnemyManager.Instance.ArmorLost(glyphName);
        }

        _enemyArmorToTej.Clear();
    }

    public void TriggerVfxPlay()
    {
        VfxToPlay?.Invoke(_drawVfxToPlay);
    }
}