using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    _instance = obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    [Header("Target Variables")]
    public TextMeshProUGUI targetLabel;
    string[] categoryList = {"Box", "Can", "Red", "Green", "Blue", "Yellow"};
    public string currentCategory;
    [SerializeField] float categoryTimer;
    public float categoryTimerMin;
    public float categoryTimerMax;

    bool categoryChanged;

    [Header("Score Variables")]
    public TextMeshProUGUI scoreLabel;
    [SerializeField] float score;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeCategory(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(categoryChanged)
        {
            StartCoroutine(ChangeCategory(categoryTimer));
            categoryChanged = false;
        }
    }

    public void UpdateScore(float _score)
    {
        score += _score;
        scoreLabel.text = "Cost: " + score.ToString("C");
    }

    IEnumerator ChangeCategory(float _targetTime)
    {
        yield return new WaitForSeconds(_targetTime);

        string tempCategory;
        do
        {
            tempCategory = categoryList[Random.Range(0, categoryList.Length)];
        } while (tempCategory == currentCategory);

        currentCategory = tempCategory;

        targetLabel.text = currentCategory;

        categoryTimer = Random.Range(categoryTimerMin, categoryTimerMax);
        categoryChanged = true;
    }
}
