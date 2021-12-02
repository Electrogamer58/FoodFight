using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Health _health { get; private set; }

    private const float ANIMATED_BAR_WIDTH = 500f;

    [SerializeField] Slider _healthBar = null;
    [SerializeField] GameObject _whitePanel = null;

    public Transform damagedBarTemplate;

    private void Awake()
    {
        if (damagedBarTemplate == null)
        {
            damagedBarTemplate = GameObject.Find("damagedBarTemplate").transform;
        }

        _health = GetComponent<Health>();

        _healthBar.maxValue = _health._maxHealth;
        _healthBar.value = _health._maxHealth;

        if (_whitePanel == null)
        {
            _whitePanel = GameObject.FindGameObjectWithTag("Flash");
        }

    }


    private void OnEnable()
    {
        //subscribe to event
        _health.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        //subscribe to event
        _health.Damaged -= OnDamaged;
    }

    void OnDamaged()
    {

        //animated bar
        float beforeAnimatedBarFillAmount = _healthBar.value;
        Debug.Log("should fill at " + beforeAnimatedBarFillAmount);
        //on damage, display new health
        _healthBar.value = _health._currentHealth;
        //continue animating
        //Transform damagedBar = Instantiate(damagedBarTemplate, transform);
        //damagedBar.gameObject.SetActive(true);
        //damagedBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(_healthBar.value * ANIMATED_BAR_WIDTH, damagedBar.GetComponent<RectTransform>().anchoredPosition.y);
        //Image damageBarImage = damagedBar.GetComponent<Image>();
        //Debug.Log(damageBarImage);
        //damageBarImage.fillAmount = beforeAnimatedBarFillAmount - _healthBar.value;



        //show damage vignette
        if (_whitePanel != null)
        {
            DelayHelper.DelayAction(this, EnableWhiteScreen, 0.5f);
            DelayHelper.DelayAction(this, DisableRedScreen, 0.6f); //show for .2 seconds and disable
        }

    }

    private void EnableWhiteScreen()
    {
        _whitePanel.SetActive(true);
    }

    private void DisableRedScreen()
    {
        _whitePanel.SetActive(false);
    }

    //public void UpdateHealthBar()
    //{
    //    if (_healthBar != null)
    //    {
    //        _healthBar.maxValue = _health._maxHealth;
    //        _healthBar.value = _health._currentHealth;
    //    }
    //    
    //}
}
