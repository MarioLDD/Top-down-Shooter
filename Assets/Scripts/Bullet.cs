using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[StringInList("Player", "Enemy")] public string target;
    public int damage = 25;
    public GameObject particleSistem;


    void Start()
    {
        //Destroy(gameObject, 2f);
    }



    private void OnTriggerEnter(Collider other)
    {
        //if (collision.gameObject.CompareTag(target))
        {
            if (other.gameObject.GetComponent<IHealthSystem>() != null)
            {
                other.gameObject.GetComponent<IHealthSystem>().TakeDamage(damage);
                Instantiate(particleSistem, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
        Instantiate(particleSistem, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}