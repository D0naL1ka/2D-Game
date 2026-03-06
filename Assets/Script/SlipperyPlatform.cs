using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SlipperyPlatform : MonoBehaviour
{
    [Header("Слизькість")]
    [SerializeField] private float friction = 0f;
    [SerializeField] private float bounciness = 0f;

    [SerializeField] private PhysicsMaterialCombine2D frictionCombine = PhysicsMaterialCombine2D.Minimum;
    [SerializeField] private PhysicsMaterialCombine2D bounceCombine = PhysicsMaterialCombine2D.Minimum;

    void Start()
    {
        var col = GetComponent<Collider2D>();

        var mat = new PhysicsMaterial2D("SlipperyMaterial")
        {
            friction = friction,
            bounciness = bounciness,
            frictionCombine = frictionCombine,
            bounceCombine = bounceCombine
        };

        col.sharedMaterial = mat;

        Debug.Log("Слизька платформа налаштована!");
    }
}