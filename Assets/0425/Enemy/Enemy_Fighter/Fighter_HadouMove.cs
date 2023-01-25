using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_HadouMove : MonoBehaviour
{
    [Header("”ò‚Ô‘¬“x"), SerializeField]
    private float flySpeed;

    public Fighter_Status fighter_Status;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, flySpeed * Time.deltaTime);
    }

    private void OnParticleCollision(GameObject other)
    {

        if (other.tag == "Player")
        {
            PlayerStatus_Solo pss;
            if (pss = other.GetComponent<PlayerStatus_Solo>())
            {
                pss.Damage(fighter_Status.GetPower());
            }
        Destroy(this.gameObject);
        }
    }
}
