using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RockBehaviour : MonoBehaviour
{
    public bool puedeCaer = false;
    public bool puedeMoverse = false;
    public bool puedeRomperse = false;
    public float fuerzaParaRomper = 6f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AplicarComportamiento();
    }

    void AplicarComportamiento()
    {
        rb.gravityScale = puedeCaer ? 1f : 0f;

        if (!puedeCaer && !puedeMoverse)
            // No puede caer ni moverse
            rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                            RigidbodyConstraints2D.FreezePositionY |
                            RigidbodyConstraints2D.FreezeRotation;
        else if (puedeCaer && !puedeMoverse)
            // Puede caer pero no moverse en X: solo freezar X
            rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                            RigidbodyConstraints2D.FreezeRotation;
        else if (!puedeCaer && puedeMoverse)
            // Puede moverse en X pero no caer: freezar Y
            rb.constraints = RigidbodyConstraints2D.FreezePositionY |
                            RigidbodyConstraints2D.FreezeRotation;
        else
            // Puede caer y moverse libremente
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log($"[Rock] Caer:{puedeCaer} | Moverse:{puedeMoverse} | Romperse:{puedeRomperse}");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impacto = collision.relativeVelocity.magnitude;
        Debug.Log($"[Rock] Impacto: {impacto:F2} de '{collision.gameObject.name}'");

        if (!puedeRomperse) return;

        if (impacto > fuerzaParaRomper)
        {
            Debug.Log("[Rock] ¡Roca destruida!");
            Destroy(gameObject);
        }
    }
}