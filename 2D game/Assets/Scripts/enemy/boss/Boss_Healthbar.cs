using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Healthbar : MonoBehaviour
{
	[SerializeField] private Boss_health Bosshealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private Image Blackfill;
    private float intTimer;
    private float timer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
        totalhealthBar.fillAmount = Bosshealth.currentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(Bosshealth.fill &&  Blackfill.fillAmount > 0){
            intTimer += Time.deltaTime;
            if (intTimer > timer) {
                intTimer = 0;
                Blackfill.fillAmount  = Blackfill.fillAmount - 0.1f ;
                if(Blackfill.fillAmount == 0)
                    Bosshealth.fill = false;
            }
            
        }
        currenthealthBar.fillAmount = Bosshealth.currentHealth / 10;
    }
}
