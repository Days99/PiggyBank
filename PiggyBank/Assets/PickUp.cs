using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool canpickup;
    bool pickedup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire2") && canpickup == true)
        {
           
            if(pickedup==false)
            {
                GameObject.Find("Stick").transform.SetParent(this.gameObject.transform);

                Debug.Log("WOWZERS");
                pickedup = true;
            }
            else if(pickedup==true)
            {
                GameObject.Find("Stick").transform.SetParent(null);
                Debug.Log("SKRRT");
                pickedup = false;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "Stick" && other.gameObject != this)
        {
            canpickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.name == "Stick" && other.gameObject != this)
            canpickup = false;
    }
}
