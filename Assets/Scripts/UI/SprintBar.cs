using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : CharacterMovement
{
    public GameObject mySprintBar;
    public GameObject mySprintBackGroundBar;
    public Slider mySlider;
    public Transform mySprintPos;
    float MaxSpr = 100.0f;
    [SerializeField] float myStatusSpr = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        myStatusSpr = MaxSpr;
    }

    // Update is called once per frame
    void Update()
    {
        mySprintBar.transform.position = Camera.main.WorldToScreenPoint(mySprintPos.position);
        mySprintBackGroundBar.GetComponent<Slider>().value = mySprintBar.GetComponent<Slider>().value;
        mySprintBackGroundBar.transform.position = mySprintBar.transform.position;
        ConsumeBar();
    }

    Coroutine coRecover = null;
    public void ConsumeBar()
    {
        if (myAnim.GetFloat("Speed") > 0.5f)
        {
            mySprintBar.SetActive(true);
            myStatusSpr -= 20.0f * Time.deltaTime;
            if (coRecover != null)
            {
                StopCoroutine(coRecover);
                coRecover = null;
            }
        }
        else
        {
            if(coRecover == null && myStatusSpr < MaxSpr)
            {
                coRecover = StartCoroutine(Recover());
            }
        }
        mySlider.value = myStatusSpr / MaxSpr;
        if(mySlider.value >= 0.999f)
        {
            mySprintBar.SetActive(false);
        }

        if(myStatusSpr < 20.0f)
        {
            mySprintBackGroundBar.SetActive(true);
        }
        else
        {
            mySprintBackGroundBar.SetActive(false);
        }
        if (myStatusSpr <= 0.0f)
        {
            myStatusSpr = 0.0f;
        }
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(1.6f);
        while(myStatusSpr < MaxSpr)
        {
            myStatusSpr = Mathf.Clamp(myStatusSpr + 20.0f * Time.deltaTime, 0.0f, MaxSpr);
            
            yield return null;
        }
        coRecover = null;
    }
}
