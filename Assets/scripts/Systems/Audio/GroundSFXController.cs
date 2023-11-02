using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSFXController : MonoBehaviour
{
    public GroundType groundType; 
    public enum GroundType { 
    Grass,
    Stone,
    Wood,
    Water
    }
}
