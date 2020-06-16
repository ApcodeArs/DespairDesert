using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DespairDesertController : MonoBehaviour
{
    [BoxGroup("Borders")] public GameObject LeftBorder;
    [BoxGroup("Borders")] public GameObject RightBorder;
    [BoxGroup("Borders")] public GameObject TopBorder;
    [BoxGroup("Borders")] public GameObject BottomBorder;

    [SerializeField, BoxGroup("Enemies")]
    public List<GameObject> Enemies = new List<GameObject>();
    [SerializeField, BoxGroup("Enemies")]
    public List<GameObject> EnemiesPrefabs = new List<GameObject>();
    private Coroutine _enemiesCoroutine;

    public GameObject Tank;

    [BoxGroup("Points")]
    public Text PointsUi;
    private float _points;

    [BoxGroup("Points")]
    public Text BestScoreUi;
    private GameData _gameData;

    [BoxGroup("Audio")] public AudioSource MissionAudioSource;
    [BoxGroup("Audio")] public AudioClip StartGame;
    [BoxGroup("Audio")] public AudioClip EndGame;

    void Awake()
    {
        _gameData = FileManager.Load();
        SetBestScore(_gameData.Score);

        PlaySound(StartGame);

        SetBorders(Screen.width, Screen.height);

        ScreenResolutionController.Instance.WindowResolutionChanged += (width, height) =>
        {
            SetBorders(width, height);
        };

        #region Enemies сoroutine
        if (_enemiesCoroutine != null)
            StopCoroutine(_enemiesCoroutine);

        _enemiesCoroutine = StartCoroutine(EnemiesFactory());
        #endregion
    }

    public void SetPoints(float points)
    {
        _points += points;
        PointsUi.text = _points.ToString();

        if (_gameData.Score < _points)
            SetBestScore(_points);
    }

    public void SetBestScore(float bestPoints)
    {
        _gameData.Score = bestPoints;
        BestScoreUi.text = _gameData.Score.ToString();
    }

    public void RestartLevel()
    {
        FileManager.Save(_gameData);

        PlaySound(EndGame);

        Time.timeScale = 0;
        DOVirtual.DelayedCall(EndGame.length + 0.5f, () =>
          {
              SceneManager.LoadScene(SceneManager.GetActiveScene().name);
              Time.timeScale = 1;
          });
    }

    private IEnumerator EnemiesFactory()
    {
        while (true)
        {
            if (Enemies.Count < EnemyBase.EnemiesCount)
            {
                var enemy = Instantiate(EnemiesPrefabs[Random.Range(0, EnemiesPrefabs.Count)]);
                var pos = Camera.main.ViewportToWorldPoint(EnemiesSpawnHelper.GetRandomSpawnPosition());
                enemy.transform.position = new Vector3(pos.x, pos.y, 0.0f) * 1.5f; //scale factor

                Enemies.Add(enemy);
            }

            yield return new WaitForSeconds(EnemyBase.SpawnDelay);
        }
    }

    private void SetBorders(int width, int height, float borderThickness = 5f, float borderScaleFactor = 2f)
    {
        var cam = Camera.main;
        var screenSize = cam.ScreenToWorldPoint(new Vector3(width, height));

        #region Set Borders Size
        LeftBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(borderThickness, screenSize.y * 2 * borderScaleFactor);
        RightBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(borderThickness, screenSize.y * 2 * borderScaleFactor);
        TopBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(screenSize.x * 2 * borderScaleFactor, borderThickness);
        BottomBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(screenSize.x * 2 * borderScaleFactor, borderThickness);
        #endregion

        #region Set Colliders Size
        LeftBorder.GetComponent<BoxCollider2D>().size = LeftBorder.GetComponent<RectTransform>().sizeDelta;
        RightBorder.GetComponent<BoxCollider2D>().size = RightBorder.GetComponent<RectTransform>().sizeDelta;
        TopBorder.GetComponent<BoxCollider2D>().size = TopBorder.GetComponent<RectTransform>().sizeDelta;
        BottomBorder.GetComponent<BoxCollider2D>().size = BottomBorder.GetComponent<RectTransform>().sizeDelta;
        #endregion

        #region Set Borders Positions
        LeftBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(0, 0.5f))
                                        + new Vector3(-LeftBorder.GetComponent<RectTransform>().rect.width / 2,
                                            0.0f);

        RightBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(1, 0.5f))
                                         + new Vector3(RightBorder.GetComponent<RectTransform>().rect.width / 2,
                                             0.0f);

        TopBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f))
                                       + new Vector3(0.0f, TopBorder.GetComponent<RectTransform>().rect.height / 2);

        BottomBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0))
                                          + new Vector3(0.0f,
                                              -BottomBorder.GetComponent<RectTransform>().rect.height / 2);
        #endregion
    }

    private void PlaySound(AudioClip clip)
    {
        MissionAudioSource.clip = clip;
        MissionAudioSource.Play();
    }
}