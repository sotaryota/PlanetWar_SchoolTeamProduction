using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_Attack : MonoBehaviour
{
    [SerializeField] AilianTurret_Status ailianTurret_Status;

    [SerializeField] ParticleSystem attack_Beem;

    [SerializeField] float attackInterval;

    [SerializeField] PlayerStatus_Solo playerStatus_Solo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        attack_Beem.Play();
        
    }

    private void OnParticleCollision(GameObject other)
    {
        
    } 

}
