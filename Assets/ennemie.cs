using UnityEngine;

public class DeplacementEnnemi : MonoBehaviour
{
    public Transform[] pointsDeDeplacement; // Les points que l'ennemi doit suivre
    public float vitesseDeDeplacement = 5f; // Vitesse de déplacement de l'ennemi
    private int indexPointActuel = 0; // Index du point de déplacement actuel

    void Update()
    {
        if (pointsDeDeplacement.Length == 0)
            return;

        // Déplacement vers le prochain point
        Transform pointCible = pointsDeDeplacement[indexPointActuel];
        transform.position = Vector3.MoveTowards(transform.position, pointCible.position, vitesseDeDeplacement * Time.deltaTime);

        // Si l'ennemi atteint le point cible, passer au point suivant
        if (Vector3.Distance(transform.position, pointCible.position) < 0.1f)
        {
            indexPointActuel = (indexPointActuel + 1) % pointsDeDeplacement.Length;
        }
    }
}
