using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHUD : MonoBehaviour
{
    public Health _health { get; private set; }

    private const float ANIMATED_BAR_WIDTH = 500f;

    [SerializeField] Slider _healthBar = null;
    [SerializeField] Slider _sugarBar = null;
    [SerializeField] GameObject _redPanel = null;

    public Transform damagedBarTemplate;

    private void Awake()
    {
        if (damagedBarTemplate == null)
        {
            damagedBarTemplate = GameObject.Find("damagedBarTemplate").transform;
        }

        _health = GetComponent<Health>();

        _healthBar.maxValue = _health._maxHealth;
        _healthBar.value = _health._currentHealth;

        _sugarBar.maxValue = _health._maxSugar;
        _sugarBar.value = _health._currentSugar;

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
        Transform damagedBar = Instantiate(damagedBarTemplate, transform);
        damagedBar.gameObject.SetActive(true);
        damagedBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(_healthBar.value * ANIMATED_BAR_WIDTH, damagedBar.GetComponent<RectTransform>().anchoredPosition.y);
        Image damageBarImage = damagedBar.GetComponent<Image>();
        //Debug.Log(damageBarImage);
        damageBarImage.fillAmount = beforeAnimatedBarFillAmount - _healthBar.value;



        //show damage vignette
        if (_redPanel != null)
        {
            _redPanel.SetActive(true);
            DelayHelper.DelayAction(this, DisableRedScreen, 0.2f); //show for .2 seconds and disable

        }



    }

    void OnHealed()
    {
        float beforeAnimatedBarFillAmount = _healthBar.value;
        Debug.Log("should fill at " + beforeAnimatedBarFillAmount);
        //on damage, display new health
        _healthBar.value = _health._currentHealth;
        //continue animating
        Transform damagedBar = Instantiate(damagedBarTemplate, transform);
        damagedBar.gameObject.SetActive(true);
        damagedBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(_healthBar.value * ANIMATED_BAR_WIDTH, damagedBar.GetComponent<RectTransform>().anchoredPosition.y);
        Image damageBarImage = damagedBar.GetComponent<Image>();
        //Debug.Log(damageBarImage);
        damageBarImage.fillAmount = beforeAnimatedBarFillAmount - _healthBar.value;
    }

    private void DisableRedScreen()
    {
        if (_redPanel != null)
        _redPanel.SetActive(false);
    }

}