using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasketBigger : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private int startTime;
    [SerializeField] private GameManager _GameManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerStart());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator TimerStart()
    {
        time.text = startTime.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1);
            startTime--;
            time.text = startTime.ToString();

            if (startTime == 0)
            {
                gameObject.SetActive(false);
                break;
            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        _GameManager.MakeBasketBigger(transform.position);
    }
}
