using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour
{
    public float MaxTime;
    public bool Tick;

    private Image _img;
    private float _curretTime;

    void Start()
    {
        _img = GetComponent<Image>();
        _curretTime = MaxTime;
    }

    void Update()
    {
        Tick = false;
        _curretTime -= Time.deltaTime;

        if (_curretTime <= 0)
        {
            Tick = true;
            _curretTime = MaxTime;
        }

        _img.fillAmount = _curretTime / MaxTime;
    }
}
