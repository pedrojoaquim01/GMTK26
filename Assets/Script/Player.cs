using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Light[] source;
    
    public bool[] verShadow;
    public LayerMask obstacleLayers; // Layers that can cast shadows (e.g., Environment, Buildings)


    // Start is called before the first frame update
    void Start()
    {
        verShadow = new bool[source.Length];
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < source.Length; i++)
        { 
            verShadow[i] = IsInShadow(source[i]);
        }

        if(!HasAnyFalse(verShadow))
        {
            Debug.Log($"{gameObject.name} is hidden in the shadow.");
        }

    }

    public void Movement()
    {
        
    }


    public bool IsInShadow(Light directionalLight)
    {
        if (directionalLight == null) return false;

        // Directional light travels in its forward direction. 
        // We look backward toward the "sun".
        Vector3 rayDirection = -directionalLight.transform.forward;

        // Use a high number for max distance since directional lights are infinite
        float maxDistance = 1000f; 

        // Cast a ray from the center of this object toward the light source
        if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, maxDistance, obstacleLayers))
        {
            // The ray hit an obstacle before reaching the open sky. It's in a shadow!
            Debug.DrawRay(transform.position, rayDirection * hit.distance, Color.red);
            return true;
        }

        // Nothing blocks the path to the sun. It is lit!
        Debug.DrawRay(transform.position, rayDirection * maxDistance, Color.green);
        return false;
    }
    bool HasAnyFalse(bool[] array)
    {
        foreach (bool value in array)
        {
            if (!value) return true; 
        }
        return false;
    }
}
