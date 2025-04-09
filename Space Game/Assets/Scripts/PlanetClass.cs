using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetSizeEnum
{
    Small,
    Medium,
    Large
}

public class PlanetClass : MonoBehaviour
{
private
    PlanetSizeEnum planetSize;
    float gravityRadius;
    float gravityStrenght;
    bool isInGrav;

    public PlanetSizeEnum PlanetSize
    {
        get { return planetSize; }
        private set { planetSize = value; }
    }
    public float GravityRadius
    {
        get { return gravityRadius; }
        private set { gravityRadius = value; }
    }
    public float GravityStrenght
    {
        get { return gravityStrenght; }
        private set { gravityStrenght = value; }
    }
    public bool IsInGrav
    {
        get { return isInGrav; }
        set { isInGrav = value; }
    }

    void Awake()
    {
        if (this.name == "Planet S(Clone)")
        {
            GravityRadius = 2.5f;
            GravityStrenght = 1.5f;
            IsInGrav = false;
            PlanetSize = PlanetSizeEnum.Small;
        }
        else if (this.name == "Planet M(Clone)")
        {
            GravityRadius = 3.5f;
            GravityStrenght = 1.7f;
            IsInGrav = false;
            PlanetSize = PlanetSizeEnum.Medium;
        }
        else if (this.name == "Planet L(Clone)")
        {
            GravityRadius = 5f;
            GravityStrenght = 2f;
            IsInGrav = false;
            PlanetSize = PlanetSizeEnum.Large;
        }
    }

}
