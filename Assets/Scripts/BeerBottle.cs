using UnityEngine;
using System.Collections.Generic;

public class BeerBottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();

    public void Shatter()
    {
        foreach (Rigidbody part in allParts)
        {
        // Controls whether physics forces affect the rigidbody. 
        // If true, the rigidbody will not be affected by physics forces and will not respond to collisions. 
        // If false, the rigidbody will be affected by physics forces and will respond to collisions.
            part.isKinematic = false; 
        }
    }
}
