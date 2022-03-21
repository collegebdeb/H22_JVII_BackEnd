using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvents
{
    //Emile : Emile a ajouter le code de cette action, donc elle va être trigger au bon moment
    //Alex : Alexandre à ajouter le code qui trigger son audio de cette action, donc s'il a créer l'audio pour ce on son audio va être triggeree
    //Asset : Alexandre à mis l'asset de ce audio dans Unity de cette action, donc il est prêt à être codé par Emile et par Alexandre
    //AssetSound : Il y a déjà un son codé dans le asset, donc pas nécessaire de faire un nouveau son ou un son par dessus
    
    //Player
    public static Action onPlayerJump; //Emile
    public static Action onPlayerJumpCanceled; //Emile

    //Input
    public static Action onCannotSwitchToMatrix; //Emile
    
    //Interaction
    public static Action onCollideLevelExit; //Emile //AssetSound
}
