using System.Collections;
using UnityEngine;

public class BiteableObject : MonoBehaviour
{
    private int currentStateIndex = 0;
    private int maxStateIndex;
    private int TakeDamageTimer = 1;
    private int currentDamage = 0;
    private bool canTakeDamage = true;
    private ShakeObject shakeObject;
    private ParticleSystem PS;

    [SerializeField] GameObject[] ObjectStates;
    [SerializeField] int damageThreshold;
    [SerializeField] SphereCollider SphereCollider;


    private void OnValidate()
    {
        int childcount = this.transform.childCount;
        ObjectStates = new GameObject[childcount];

        for(int i = 0; i < childcount; i++)
        {
            ObjectStates[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        PS = GetComponentInParent<ParticleSystem>();
        PS.Stop();
        shakeObject = gameObject.GetComponent<ShakeObject>();
        maxStateIndex = ObjectStates.Length - 1;
        UpdateStateObjects();
    }

    public void ReceiveDamage(int damage)
    {
        currentDamage += damage;

        int nextStateIndex = currentDamage / damageThreshold;
        if (nextStateIndex > currentStateIndex && nextStateIndex <= maxStateIndex)
        {
            currentStateIndex = nextStateIndex;
            UpdateStateObjects();
        }else if(currentDamage >= maxStateIndex)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateStateObjects()
    {
        for (int i = 0; i < ObjectStates.Length; i++)
        {
            ObjectStates[i].SetActive(i == currentStateIndex);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == SphereCollider)
        {
            BiteObject biteobject = other.GetComponent<BiteObject>();
            if (biteobject != null)
            {
                if(biteobject.CanDealDamage() == true)
                {
                    if (canTakeDamage)
                    {
                        PS.Play();
                        ReceiveDamage(biteobject.Damage);
                        canTakeDamage = false;
                        ShakeObject();
                        StartCoroutine(Cooldown(TakeDamageTimer));       
                    }
                }
            }
        }
    }

    IEnumerator Cooldown(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        canTakeDamage = true;
        PS.Stop();
    }

    private void ShakeObject()
    {
        shakeObject.StartShake();
    }




}
