using CodeBase.Logic;
using UnityEngine;

public class Hero : MonoBehaviour, IHeroTransform
{
    public Transform Transform => transform;
}