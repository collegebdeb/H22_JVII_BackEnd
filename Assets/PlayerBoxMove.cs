using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using devziie.Inputs;


public class PlayerBoxMove : MonoBehaviour
{
    private Player _player;

    //Get le player
    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void OnEnable()
    {
        //Lit la fonction OnPlayerTryInteract si le joueur cliquer sur le bouton interact
        InputManager.Controls.Player.Interact.started += OnPlayerTryInteract;
    }
    
    public void OnDisable()
    {
        InputManager.Controls.Player.Interact.started -= OnPlayerTryInteract;
    }
    
    private void OnPlayerTryInteract(InputAction.CallbackContext context)
    {
        //Le joueur interagit bien avec une boite; faire code bouge avec boite et tout - Justin
        if (_player.interactState == Player.PlayerInteractState.InteractionWithBox)
        {
            Box box = _player.currentInteraction as Box;
            //Tu peux changer le transform du box et tout simplement avec box.transform
            //Le joueur est connecter a la boite, donc sil recule la boite recule aussi, mais elle doit tomber si il a un trou en dessous delle evidemment
            //Tu peux edit aussi le script Box qui se trouve sur la boite
            //Tu peux peut-etre faire quelque chose comme : la boite a un bool état connecter ou deconnecter; sil elle est connecter (box.currentlyConnectedToPlayer) tu la fait bouger
            //avec le player en desactivant sa physique, si elle est deconnecter, tu arrete de la faire bouger ici avec le joueur et tu reactive sa physique. Et dans le script de la boite
            //tu peux faire quelque chose comme elle lance un raycast en dessous delle ou verifie si elle est vraiment sur le edge de tomber et mettre ton etat a deconnecter du joueur
            //quand tu penses quelle devrais se ceconnecter du joueur
            

        }
        
    }
    
  
}
