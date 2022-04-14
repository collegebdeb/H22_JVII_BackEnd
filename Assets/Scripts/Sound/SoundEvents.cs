using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundEvents
{
    //Emile : Emile a ajouter le code de cette action, donc elle va être trigger au bon moment
    //Alex : Alexandre à ajouter le code qui trigger son audio de cette action, donc s'il a créer l'audio pour ce on son audio va être triggeree
    //Asset : Alexandre à mis l'asset de ce audio dans Unity de cette action, donc il est prêt à être codé par Emile et par Alexandre
    //Il y a deja un asset de son : Il y a déjà un son codé dans le asset, donc pas nécessaire de faire un nouveau son ou un son par dessus

    //Player
    public static Action<AudioList.Sound, GameObject> onPlayerJump; //Emileb //Asset  //Alex
    public static Action<AudioList.Sound, GameObject> onPlayerJumpCanceled; //Emile

    //Input
    public static Action<AudioList.Sound, GameObject> onCannotSwitchToMatrix; //Emile
    public static Action<AudioList.Sound, GameObject> OnMatrixActivated; //Emile
    public static Action<AudioList.Sound, GameObject> OnRealWorldActivated; //Emile
    public static Action<AudioList.Sound, GameObject> OnTransitionActivated; //Emile
    

    //Interaction
    public static Action<AudioList.Sound, GameObject> onCollideLevelExit; //Emile //Il y a deja un asset de son


    public static List<Action<AudioList.Sound, GameObject>> allActions;

    //Action Son quand le joueur clique sur interact  //Asset
 
}
