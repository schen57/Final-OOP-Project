using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerupController : MonoBehaviour
{
    float scaleFactor;
    float timeFactor;
    public TextMeshProUGUI powerUpText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeFactor += Time.deltaTime;
        scaleFactor = Mathf.Sin(timeFactor*4)/3+2f;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
    private IEnumerator HidePowerUpText(TextMeshProUGUI powerUpText)
    {
        yield return new WaitForSeconds(.2f);
        powerUpText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerUpText.gameObject.SetActive(true);
            Vector3 closestPoint = other.ClosestPoint(transform.position);
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(closestPoint);
            powerUpText.transform.position = screenPoint;
            powerUpText.text = "+1 Projectile";
            StartCoroutine(HidePowerUpText(powerUpText));
            
        }
    }
}
