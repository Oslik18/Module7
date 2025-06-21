using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimer HarvestImageTimer;
    public ImageTimer EatingImageTimer;
    public Image RaidImageTimer;
    public Image PeasantImageTimer;
    public Image WarriorImageTimer;

    public Button peasantButton;
    public Button warriorButton;
    public TMP_Text peasantsCountText;
    public TMP_Text warriorsCountText;
    public TMP_Text foodCountText;
    public TMP_Text enemyCountText;
    public TMP_Text raidCountText;

    public AudioSource eatingSound;
    public AudioSource enemySound;
    public AudioSource harvestSound;
    public AudioSource peasantSound;
    public AudioSource warriorSound;
    public AudioSource winLoseSound;

    public TMP_Text totalPeasantsText;
    public TMP_Text totalWarriorsText;
    public TMP_Text totalFoodText;
    public TMP_Text eatingFoodText;
    public TMP_Text totalKilledWarriorsText;
    public TMP_Text totalRaidsText;
    public TMP_Text headText;

    public GameObject winLosePanel;
    public AudioClip winMusic;
    public AudioClip gameOverMusic;

    public int peasantsCount;
    public int warriorsCount;
    public int foodCount;

    public int foodPerPeasant;
    public int foodToWarrior;

    public int peasantCost;
    public int warriorCost;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;
    public int countFoodForWin;

    private float _peasantTimer = -2;
    private float _warriorTimer = -2;
    private float _raidTimer;
    private int _raidCount;
    private int _totalFood;
    private int _totalWarriors;
    private int _totalKilled;

    void Start()
    {
        _raidTimer = raidMaxTime;
        _raidCount = 1;
        _peasantTimer = peasantCreateTime;
        _warriorTimer = warriorCreateTime;
        peasantButton.interactable = false;
        warriorButton.interactable = false;
        UpdateText();
        winLosePanel.SetActive(false);
        _totalFood = Convert.ToInt32(foodCount);
        _totalWarriors = Convert.ToInt32(warriorsCount);
        _totalKilled = 0;
    }

    void Update()
    {
        RaidTimer();
        HarvestTimer();
        EatingTimer();

        if (!warriorButton.interactable)
        {
            WarriorTimer();
        }

        if (!peasantButton.interactable)
        {
            PeasantTimer();
        }

        UpdateText();

        if (foodCount > Convert.ToInt32(countFoodForWin))
        {
            ShowWinLosePanel(1);
        }
    }

    private void UpdateText()
    {
        peasantsCountText.text = peasantsCount.ToString();
        warriorsCountText.text = warriorsCount.ToString();
        foodCountText.text = foodCount.ToString();
        enemyCountText.text = nextRaid.ToString();
        raidCountText.text = _raidCount.ToString();
    }

    public void CreatePeasant()
    {
        foodCount -= peasantCost;
        peasantsCount += 1;
        _peasantTimer = peasantCreateTime;
        peasantButton.interactable = false;
        peasantSound.Play();
    }

    public void CreateWarrior()
    {
        foodCount -= warriorCost;
        warriorsCount += 1;
        _totalWarriors += 1;
        _warriorTimer = warriorCreateTime;
        warriorButton.interactable = false;
        warriorSound.Play();
    }

    private void PeasantTimer()
    {
        if (_peasantTimer > 0)
        {
            _peasantTimer -= Time.deltaTime;
            PeasantImageTimer.fillAmount = _peasantTimer / peasantCreateTime;
        }
        else if (_peasantTimer > -1)
        {
            PeasantImageTimer.fillAmount = 1;

            if (foodCount > peasantCost)
                peasantButton.interactable = true;
        }
    }

    private void WarriorTimer()
    {
        if (_warriorTimer > 0)
        {
            _warriorTimer -= Time.deltaTime;
            WarriorImageTimer.fillAmount = _warriorTimer / warriorCreateTime;
        }
        else if (_warriorTimer > -1)
        {
            WarriorImageTimer.fillAmount = 1;

            if (foodCount > warriorCost)
                warriorButton.interactable = true;
        }
    }

    private void RaidTimer()
    {
        _raidTimer -= Time.deltaTime;
        RaidImageTimer.fillAmount = _raidTimer / raidMaxTime;

        if (_raidTimer <= 0)
        {
            _raidTimer = raidMaxTime;
            _raidCount++;

            if (_raidCount > 2)
            {
                if (warriorsCount > nextRaid)
                    _totalKilled += nextRaid;
                else
                    _totalKilled += warriorsCount;

                warriorsCount -= nextRaid;
                nextRaid += raidIncrease;
            }

            if (_raidCount > 3)
            {
                if (warriorsCount < 0)
                {
                    ShowWinLosePanel(0);
                }
                else
                {
                    enemySound.Play();
                }
            }
        }
    }

    private void EatingTimer()
    {
        if (EatingImageTimer.Tick)
        {
            foodCount -= warriorsCount * foodToWarrior;
            eatingSound.Play();
        }
    }

    private void HarvestTimer()
    {
        if (HarvestImageTimer.Tick)
        {
            foodCount += peasantsCount * foodPerPeasant;
            _totalFood += peasantsCount * foodPerPeasant;
            harvestSound.Play();
        }
    }

    public void CloseWinLosePanel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        winLosePanel.SetActive(false);
    }

    private void ShowWinLosePanel(int result)
    {
        totalPeasantsText.text = peasantsCount.ToString();
        totalWarriorsText.text = _totalWarriors.ToString();
        totalFoodText.text = _totalFood.ToString();
        eatingFoodText.text = (_totalFood - foodCount).ToString();
        totalKilledWarriorsText.text = _totalKilled.ToString();
        totalRaidsText.text = _raidCount.ToString();

        if (result == 0)
        {
            headText.text = "Вы проиграли :(";
            winLoseSound.clip = gameOverMusic;
        }
        else
        {
            headText.text = "Вы выйграли!!! :)";
            winLoseSound.clip = winMusic;
        }

        winLosePanel.SetActive(true);
        winLoseSound.Play();
    }
}
