using PDollarGestureRecognizer;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PasDinspiPourLeNomDesoLaTeam : MonoBehaviour
{
    [SerializeField] private GameObject closestEnemyDetected;

    private void Start()
    {
        DrawForDollarP.instance.OnDrawFinish += DrawFinish;
    }


    public void DrawFinish(DrawData result)
    {
        if (EnemyManager.Instance.DoEnemyOnScreenHaveRecognizedGlyph(result.result.GestureClass) && result.result.Score > PlayerMain.Instance.toileInfo.tolerance)
        {
            StartCoroutine(GoodDraw(result));
            closestEnemyDetected = EnemyManager.Instance.FindClosestEnemyWithGlyph(result.result.GestureClass);
        }
        else if (EnemyManager.Instance.FindAllEnnemyOnScreen().Count > 0) 
        {
            StartCoroutine(WrongDraw(result));
        }
    }

    public IEnumerator GoodDraw(DrawData result)
    {
        Time.timeScale = 0;
        Debug.LogWarning($"{result.result.Score}");
        yield return new WaitForSecondsRealtime(0.1f);
        closestEnemyDetected.TryGetComponent(out EnemyMain enemyMain);

        enemyMain.Armor.activeGlyphs[0].SetActive(false);
        DrawForDollarP.instance.SetActiveLine(false);
        yield return new WaitForSecondsRealtime(0.1f);

        enemyMain.Armor.activeGlyphs[0].SetActive(true);
        DrawForDollarP.instance.SetActiveLine(true);
        yield return new WaitForSecondsRealtime(0.1f);

        enemyMain.Armor.activeGlyphs[0].SetActive(false);
        DrawForDollarP.instance.SetActiveLine(false);
        yield return new WaitForSecondsRealtime(0.1f);

        enemyMain.Armor.activeGlyphs[0].SetActive(true);
        DrawForDollarP.instance.SetActiveLine(true);
        yield return new WaitForSecondsRealtime(0.1f);

        enemyMain.Armor.activeGlyphs[0].SetActive(false);
        DrawForDollarP.instance.SetActiveLine(false);
        yield return new WaitForSecondsRealtime(0.1f);

        enemyMain.Armor.activeGlyphs[0].SetActive(true);
        DrawForDollarP.instance.SetActiveLine(true);
        Time.timeScale = PlayerMain.Instance.toileInfo.slowMotionScale;
    }

    public IEnumerator WrongDraw(DrawData result)
    {
        Debug.LogWarning("Play sound DrawBad");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);


    }










    #region Debug et Test

    public Slider slider;
    public float time;
    public float unscaledTime;


    private void Update()
    {
        time = Time.time;
        unscaledTime = Time.unscaledTime;
    }

    public void ChangeScale()
    {
        Time.timeScale = slider.value;
    }

    #endregion
}