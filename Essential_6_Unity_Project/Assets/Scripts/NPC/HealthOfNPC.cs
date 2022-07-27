using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOfNPC : MonoBehaviour
{
    public int MaxHealth = 100;
    public int Health = 100;
    public int Group = 0;

    [SerializeField] private bool isDynamicHealthBarCreat = true;

    [SerializeField] private Canvas _canvas;

    private UIHealthBar _uIHealthBar;

    private bool _isDead;

    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }

    public int Kills { get; private set; }

    void Start()
    {
        GameObject uIHealthBar = Instantiate(Resources.Load("HealthBarSlider"), Vector3.zero, Quaternion.identity) as GameObject;
        uIHealthBar.transform.SetParent(_canvas.transform);


        _uIHealthBar = uIHealthBar.GetComponent<UIHealthBar>();

        _uIHealthBar.NPC = transform;
    }

    void Update()
    {
        
    }

    public void GetDamage(int damage, HealthOfNPC killer)
    {
        if (IsDead)
            return;
        Health -= damage;

        if (Health <= 0)
        {
            IsDead = true;
            killer.Kills += 1;
            GetComponentInChildren<PlayerShooting>().Drop();
            GetComponent<Animator>().SetBool("Dead", true);
            Destroy(_uIHealthBar.gameObject);
        }
    }
}
