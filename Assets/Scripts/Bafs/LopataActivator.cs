using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LopataActivator : MonoBehaviour
{
    public void ActiveBaf()
    {
        FindObjectOfType<Lopata>().StartWork();
    }
}
