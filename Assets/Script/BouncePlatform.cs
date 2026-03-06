using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BouncePlatform : MonoBehaviour
{
    [Header("Пружність")]
    [SerializeField] private float bounciness = 0.8f;
    [SerializeField] private float friction = 0.3f;

    // Додай ці два поля, якщо хочеш контролювати режим комбінування
    [SerializeField] private PhysicsMaterialCombine2D frictionCombine = PhysicsMaterialCombine2D.Minimum;
    [SerializeField] private PhysicsMaterialCombine2D bounceCombine = PhysicsMaterialCombine2D.Maximum;

    void Start()
    {
        var col = GetComponent<Collider2D>();

        var mat = new PhysicsMaterial2D("BounceMaterial")
        {
            friction = friction,
            bounciness = bounciness,
            frictionCombine = frictionCombine,
            bounceCombine = bounceCombine
        };

        col.sharedMaterial = mat;

        Debug.Log("Пружна платформа налаштована!");
    }
}