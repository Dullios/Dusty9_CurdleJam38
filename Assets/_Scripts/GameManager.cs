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
    string[] itemTargetList = {"Box", "Can", "Red", "Green", "Blue", "Yellow"};
    public string currentTarget;
    string previousTarget;
    public float targetTimer;

    bool targetChanged;

    [Header("Score Variables")]
    public TextMeshProUGUI scoreLabel;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeTarget(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(targetChanged)
        {
            StartCoroutine(ChangeTarget(targetTimer));
            targetChanged = false;
        }
    }

    IEnumerator ChangeTarget(float _targetTime)
    {
        yield return new WaitForSeconds(_targetTime);

        string tempTarget;
        do
        {
            tempTarget = itemTargetList[Random.Range(0, itemTargetList.Length)];
        } while (tempTarget == previousTarget);

        previousTarget = currentTarget;
        currentTarget = tempTarget;

        targetLabel.text = currentTarget;

        targetChanged = true;
    }
}
