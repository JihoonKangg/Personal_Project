using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : MonoBehaviour
{
    [SerializeField] Slider mySlider;
    [SerializeField] Slider mySlider_BG;
    [SerializeField] Transform myHpPos;
    [SerializeField] GameObject myHPBar;
    public Golem myGol;

    // Start is called before the first frame update
    void Start()
    {
        mySlider.value = 1.0f;
        mySlider_BG.value = mySlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        mySlider.transform.position = Camera.main.WorldToScreenPoint(myHpPos.position);
        mySlider_BG.transform.position = mySlider.transform.position;
        mySlider.value = myGol.myStat.HP / myGol.myStat.MaxHP;
        mySlider_BG.value = Mathf.Lerp(mySlider_BG.value, myGol.myStat.HP / myGol.myStat.MaxHP, 5.0f * Time.deltaTime);

        if(mySlider.value == 0.0f)
        {
            myHPBar.SetActive(false);
        }
    }
}
