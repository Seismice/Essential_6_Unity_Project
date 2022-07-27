using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    Transform _nPC;
    public Transform NPC { get { return _nPC; } 
        set 
        {
            _nPC = value;

            _healthOfNPC = NPC.GetComponent<HealthOfNPC>();
            _slider = GetComponent<Slider>();
            _slider.maxValue = _healthOfNPC.MaxHealth;
        } 
    }

    private HealthOfNPC _healthOfNPC;
    private Slider _slider;

    void Start()
    {
        
    }

    void Update()
    {
        if (!NPC)
            return;

        Vector3 npcPos = new Vector3(NPC.position.x, NPC.position.y + 1.0f, NPC.position.z);
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(npcPos);

        _slider.value = _healthOfNPC.Health;
    }

}
