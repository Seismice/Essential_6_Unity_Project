using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour
{
    Transform _nPC;
    public Transform NPC
    {
        get { return _nPC; }
        set
        {
            _nPC = value;

            _healthOfNPC = NPC.GetComponent<HealthOfNPC>();
            _slider = GetComponentInChildren<Slider>();
            _slider.maxValue = _healthOfNPC.MaxHealth;
        }
    }

    private HealthOfNPC _healthOfNPC;
    private Slider _slider;

    private TMP_Text _kills;

    void Start()
    {
        _kills = transform.Find("Kills").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!NPC)
            return;

        Vector3 npcPos = new Vector3(NPC.position.x, NPC.position.y + 1.0f, NPC.position.z);
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(npcPos);
        if (_slider)
            _slider.value = _healthOfNPC.Health;

        _kills.text = _healthOfNPC.Kills.ToString();
    }

    public void DisableSlider()
    {
        Destroy(_slider.gameObject);
    }

}
