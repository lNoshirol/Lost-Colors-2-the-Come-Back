using PDollarGestureRecognizer;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PasDinspiPourLeNomDesoLaTeam : MonoBehaviour
{
    [SerializeField] private GameObject closestEnemyDetected;

    private Coroutine runningCoroutine;

    private void Start()
    {
        DrawForDollarP.instance.OnDrawFinish += DrawFinish;
    }


    public void DrawFinish(DrawData result)
    {
        if (EnemyManager.Instance.DoEnemyOnScreenHaveRecognizedGlyph(result.result.GestureClass) && result.result.Score > PlayerMain.Instance.toileInfo.tolerance)
        {
           /* runningCoroutine = */
                StartCoroutine(GoodDraw(result));
            closestEnemyDetected = EnemyManager.Instance.FindClosestEnemyWithGlyph(result.result.GestureClass);
        }
        else if (EnemyManager.Instance.FindAllEnnemyOnScreen().Count > 0) 
        {
          /*  runningCoroutine = */
                StartCoroutine(WrongDraw(result));
        }
    }

    public IEnumerator GoodDraw(DrawData result)
    {
        Time.timeScale = 0;
        Debug.LogWarning($"{result.result.Score}, play good sound");
        yield return new WaitForSecondsRealtime(0.1f);
        closestEnemyDetected.TryGetComponent(out EnemyMain enemyMain);

        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                enemyMain.Armor.activeGlyphs[0].SetActive(false);
                DrawForDollarP.instance.SetActiveLine(false);
            }
            else
            {
                enemyMain.Armor.activeGlyphs[0].SetActive(true);
                DrawForDollarP.instance.SetActiveLine(true);
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }

        Time.timeScale = PlayerMain.Instance.toileInfo.slowMotionScale;
    }

    public IEnumerator WrongDraw(DrawData result)
    {
        Debug.LogWarning("Play sound DrawBad");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);


    }

    public void StopRunningCoroutine()
    {
        StopCoroutine(runningCoroutine);
    }








    #region Debug et Test

    [Header("Debug")]
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