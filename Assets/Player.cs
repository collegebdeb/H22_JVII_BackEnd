using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public enum PlayerInteractState {None, InteractionWithBox}

    //Etat d'interaction du joueur
    public PlayerInteractState interactState;

    //Avec quoi le joueur est t'il en train d'interagir? Peut être un pickable comme une fleur ou quelque chose comme une boite
    public Interactable currentInteraction;

    private void OnTriggerEnter(Collider other)
    {
        //Est-ce que rentre en collision avec une zone interagissable
        if (other.CompareTag("Interactable"))
        {
            //La zone d'interaction est enfant de l'objet qui peut être intéractable (exemple Boite parent de zone de detection de la boite)
            currentInteraction = other.GetComponentInParent<Interactable>();

            if (currentInteraction is Box) //Est-ce que l'objet interactif est une boite?
            {
                interactState = PlayerInteractState.InteractionWithBox; //Nouvelle État du Player
            }
        }
    }
    
    //Quitte la zone interactable
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactState = PlayerInteractState.None;
            currentInteraction = null;
        }
    }
}
