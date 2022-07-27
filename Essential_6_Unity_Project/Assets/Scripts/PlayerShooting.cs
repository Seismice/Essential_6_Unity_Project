using UnityEngine;
using System.Collections;


public class PlayerShooting : MonoBehaviour
{
    public GameObject Body;                         ///Ссылка на оруж

    public int damagePerShot = 20;                  // Уорона за выстрел
    public float timeBetweenBullets = 0.15f;        // Время между каждым выстрелом.
    public float range = 100f;                      // Расстояние выстрела.

    float timer;                                    // Таймер для определения, когда стрелять.
    Ray shootRay;                                   // Луч из оружия, который выпущен вперед.
    RaycastHit shootHit;                            // Луч поал, получаем информацию
    int shootableMask;                              // Индекс слоя в котором попадаем во врагов
    ParticleSystem gunParticles;                    // Эффект выстрела
    LineRenderer gunLine;                           // Линия выстрела
    AudioSource gunAudio;                           // Звук выстрела
    Light gunLight;                                 // Свет выстрела
    public Light faceLight;
    float effectsDisplayTime = 0.2f;

    private HealthOfNPC _healthOfNPC;

    void Awake()
    {
        // Берем индекс слоя Shootable
        shootableMask = LayerMask.GetMask("Shootable");

        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();


        //faceLight = GetComponentInChildren<Light> ();

        _healthOfNPC = GetComponentInParent<HealthOfNPC>();
    }


    void Update()
    {
        // Добавьте время, так как обновление было в прошлом Update.
        timer += Time.deltaTime;



        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        faceLight.enabled = false;
        gunLight.enabled = false;
    }

    public void StartShoot()
    {
        if (timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        timer = 0f;

        //gunAudio.Play();

        // Включаем всвет
        gunLight.enabled = true;
        faceLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        // Линия выстрела
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        // Настройка луча
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            if(shootHit.collider.GetComponentInParent<HealthOfNPC>())
            {
                shootHit.collider.GetComponentInParent<HealthOfNPC>().GetDamage(10, _healthOfNPC);
            }

            else if (shootHit.collider.GetComponent<Rigidbody>())
            {
                shootHit.collider.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
            }

            // Рисуем линию
            gunLine.SetPosition(1, shootHit.point);
        }
        // Если не попали
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }


    public void Drop()
    {
        StartCoroutine(WaitDrop());
    }

    IEnumerator WaitDrop()
    {
        yield return new WaitForSeconds(0.3f);

        Body.transform.SetParent(null);
        Body.GetComponent<Collider>().enabled = true;
        Body.GetComponent<Rigidbody>().isKinematic = false;
        Body.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 200);
    }
}