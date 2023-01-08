using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSprintBar : MonoBehaviour
{
    [SerializeField] Slider mySlider;
    [SerializeField] Transform mySprintPos;
    public Golem myGol;

    // Start is called before the first frame update
    void Start()
    {
        mySlider.value = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mySlider.transform.position = Camera.main.WorldToScreenPoint(mySprintPos.position);
        mySlider.value = myGol.myStat.HP / myGol.myStat.MaxHP;
    }
}
