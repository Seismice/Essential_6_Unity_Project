using System.Collections;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class SeachEnemy : MonoBehaviour
{
    public float FireRange = 10;

    private HealthOfNPC _target;

    public HealthOfNPC Target
    {
        get { return _target; }
        set { _target = value; }
    }

    private HealthOfNPC _healthOfNPC;
    private PlayerShooting _gun;
    void Start()
    {
        _healthOfNPC = GetComponent<HealthOfNPC>();
        _gun = GetComponentInChildren<PlayerShooting>();
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        HealthOfNPC[] targets = FindObjectsOfType<HealthOfNPC>()
            .Where(p => p.Group != _healthOfNPC.Group && !p.IsDead).ToArray();

        if (targets.Length == 0)
        {
            GetComponent<Animator>().SetTrigger("Winner");
        }
        else
        {
            _target = targets[Random.Range(0, targets.Length)];

            if (!_healthOfNPC.IsDead)
            {
                yield return new WaitForSeconds(5);

                StartCoroutine(Timer());
            }
        }
    }

    void Update()
    {
        if (!_target || _healthOfNPC.IsDead || _target.IsDead)
        {
            return;
        }
        
        if(FireRange > Vector3.Distance(transform.position, _target.transform.position))
        {
            Vector3 targetPos = new Vector3(_target.transform.position.x, 5.0f, _target.transform.position.z);
            transform.LookAt(targetPos);
            float height = _target.GetComponent<CapsuleCollider>().height + 4.1f;

            Vector3 firePosition = new Vector3(_target.transform.position.x, height, _target.transform.position.z);

            Vector3 randomeSph = Random.insideUnitSphere * 1.5f;

            _gun.Body.transform.LookAt(firePosition + randomeSph);
            _gun.StartShoot();
        }
    }
}
