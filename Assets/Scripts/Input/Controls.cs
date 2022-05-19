// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""be41cd5e-5a54-4812-a974-1820aaee8f84"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d4311afb-0d85-4c1f-8aa8-222626a5ccfd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""40f02d43-3a37-4b81-bb3f-d2d80711e6f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d97f9dac-cedd-4c2c-904c-71a4a8874565"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleBackEnd"",
                    ""type"": ""Button"",
                    ""id"": ""a33c3932-c53f-4531-b024-06c4b047c385"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lock"",
                    ""type"": ""Button"",
                    ""id"": ""2db1097b-0de6-4760-a2d5-c88da1d70597"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FastForward"",
                    ""type"": ""Button"",
                    ""id"": ""077a8e83-11de-46f0-89dc-776417ea4b47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuickSave"",
                    ""type"": ""Button"",
                    ""id"": ""5a05baff-00ec-4562-a227-019004a808f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LoadQuickSave"",
                    ""type"": ""Button"",
                    ""id"": ""f7894b6f-89d4-4907-a196-beb281d9c5d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""55093553-c953-43da-8979-c0e765a3e782"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""617ebd44-8e38-4cb9-8a85-6a9b58303628"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a130bec8-4db1-4eee-9288-20a933afc7d8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f232aee9-afaa-429b-8e1b-077e8dc743bc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""82dab5af-da60-47e0-b78e-7a09ea19bd38"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e3da4370-b3d6-4ee5-8236-e88292702c57"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6c1661da-4dbd-40d1-873d-87e8c56f2300"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da2c981e-4f96-4577-b39c-317405e906c1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c15a0f8e-fd92-4f67-bf98-d15b013e51d4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e75441c1-1ea9-4863-8fdc-2848e5559f00"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""289fe5b2-f672-44bd-a8ea-bedcf2047dd8"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ToggleBackEnd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""065f465d-a114-48d5-a17c-9aacc17bd465"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ToggleBackEnd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eca620ae-f5fb-4107-bfde-65eb40a83d42"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6b0cc47-f8eb-4336-bff4-6a4b7cd9380a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Lock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d484f47c-13e0-4029-8fe8-ed94259def70"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""FastForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5062bd18-abb5-4e93-8848-ba4f2d15d38b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""FastForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa29a5d9-468e-45a3-9d88-af9b07e7a765"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""QuickSave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89bc769c-de6a-40fc-887b-4b1539985a2f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""QuickSave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84e14be1-a288-4610-b9b3-06e11286ec8d"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LoadQuickSave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22404f55-afee-4203-9c3c-a377cbbff78d"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LoadQuickSave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2D"",
            ""id"": ""1a96e6e3-f53e-4a17-994b-f977c64c21d0"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3fcba5bf-7570-49e2-b9be-e50c618060e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9c82e474-a339-4212-9124-95f5b817c318"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b29e2ff-e7c8-49fd-80b4-61e146ba066f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f6aef1f-93de-4e5a-8e7c-a0439c295db9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e9e3322-62af-4782-a410-0465da76bb64"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d332816-3566-4354-96f4-c743bc372e1b"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7149a3e-e6a9-4cce-a794-44215127eef4"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""dcc14ef5-ee25-41f0-b111-7d32e95698c0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7eac9245-682c-4bf3-98b6-039b794993c5"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dd93ed01-ecfd-458b-8b95-4a01ebb14051"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a44210bb-fb9f-4ae5-bff5-095f70ecc0cb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1dce474d-e933-4eb8-aa70-977258c8df8c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""efc57c93-8c9d-4410-8970-75615d3aaa90"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f0cab649-fbb2-4c0f-948f-5c1d0597459e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""242a2e93-4e5e-4941-a762-e68ad6a8613d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cacf2f46-ee12-49b7-aa73-5267e7ef92d8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3e194f78-4aa2-4891-8d0e-12593ba6a4a7"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b4595fb3-aa43-4e42-8da3-4829bcab3022"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7eebf88c-27ba-4292-bb0a-511d3867ba9e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e52fc9d9-c28c-4035-82d9-50f7d2607660"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e51e5616-44af-49d1-8252-befb54773883"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5706486f-9b7d-42ef-929f-69db55cf4538"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""MinigameUI"",
            ""id"": ""5c6fbafa-f4b2-492e-b171-0774454945cf"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""79e88822-62ae-4e13-8c1e-c3be5a181402"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""d7561bea-7eff-46f2-910d-10df7e2929b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""8a687317-388d-4f9a-928c-8274ff803496"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5f35fefb-b6c8-424f-b681-4dba646c7fe6"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f38a424c-d77a-4be0-bd06-7f55a9fece39"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3e40de2-afd5-41e9-91b0-56e0bfcb3ef3"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9e7b30e-dcf8-42f0-bb88-8bb14e622f50"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4ac569e-d9d2-47ba-9fe0-2c13d1a789f1"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6fa19cc-c873-400b-ba58-968df58074e9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18e7bbc7-1473-4247-b0be-e5adc7972723"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cdd6255-5c4e-4ae2-96d4-d1e9ee34c408"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_ToggleBackEnd = m_Player.FindAction("ToggleBackEnd", throwIfNotFound: true);
        m_Player_Lock = m_Player.FindAction("Lock", throwIfNotFound: true);
        m_Player_FastForward = m_Player.FindAction("FastForward", throwIfNotFound: true);
        m_Player_QuickSave = m_Player.FindAction("QuickSave", throwIfNotFound: true);
        m_Player_LoadQuickSave = m_Player.FindAction("LoadQuickSave", throwIfNotFound: true);
        // Player2D
        m_Player2D = asset.FindActionMap("Player2D", throwIfNotFound: true);
        m_Player2D_Jump = m_Player2D.FindAction("Jump", throwIfNotFound: true);
        m_Player2D_Move = m_Player2D.FindAction("Move", throwIfNotFound: true);
        // MinigameUI
        m_MinigameUI = asset.FindActionMap("MinigameUI", throwIfNotFound: true);
        m_MinigameUI_Up = m_MinigameUI.FindAction("Up", throwIfNotFound: true);
        m_MinigameUI_Down = m_MinigameUI.FindAction("Down", throwIfNotFound: true);
        m_MinigameUI_Select = m_MinigameUI.FindAction("Select", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_ToggleBackEnd;
    private readonly InputAction m_Player_Lock;
    private readonly InputAction m_Player_FastForward;
    private readonly InputAction m_Player_QuickSave;
    private readonly InputAction m_Player_LoadQuickSave;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @ToggleBackEnd => m_Wrapper.m_Player_ToggleBackEnd;
        public InputAction @Lock => m_Wrapper.m_Player_Lock;
        public InputAction @FastForward => m_Wrapper.m_Player_FastForward;
        public InputAction @QuickSave => m_Wrapper.m_Player_QuickSave;
        public InputAction @LoadQuickSave => m_Wrapper.m_Player_LoadQuickSave;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @ToggleBackEnd.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBackEnd;
                @ToggleBackEnd.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBackEnd;
                @ToggleBackEnd.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBackEnd;
                @Lock.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLock;
                @Lock.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLock;
                @Lock.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLock;
                @FastForward.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFastForward;
                @FastForward.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFastForward;
                @FastForward.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFastForward;
                @QuickSave.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSave;
                @QuickSave.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSave;
                @QuickSave.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSave;
                @LoadQuickSave.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLoadQuickSave;
                @LoadQuickSave.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLoadQuickSave;
                @LoadQuickSave.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLoadQuickSave;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @ToggleBackEnd.started += instance.OnToggleBackEnd;
                @ToggleBackEnd.performed += instance.OnToggleBackEnd;
                @ToggleBackEnd.canceled += instance.OnToggleBackEnd;
                @Lock.started += instance.OnLock;
                @Lock.performed += instance.OnLock;
                @Lock.canceled += instance.OnLock;
                @FastForward.started += instance.OnFastForward;
                @FastForward.performed += instance.OnFastForward;
                @FastForward.canceled += instance.OnFastForward;
                @QuickSave.started += instance.OnQuickSave;
                @QuickSave.performed += instance.OnQuickSave;
                @QuickSave.canceled += instance.OnQuickSave;
                @LoadQuickSave.started += instance.OnLoadQuickSave;
                @LoadQuickSave.performed += instance.OnLoadQuickSave;
                @LoadQuickSave.canceled += instance.OnLoadQuickSave;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Player2D
    private readonly InputActionMap m_Player2D;
    private IPlayer2DActions m_Player2DActionsCallbackInterface;
    private readonly InputAction m_Player2D_Jump;
    private readonly InputAction m_Player2D_Move;
    public struct Player2DActions
    {
        private @Controls m_Wrapper;
        public Player2DActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player2D_Jump;
        public InputAction @Move => m_Wrapper.m_Player2D_Move;
        public InputActionMap Get() { return m_Wrapper.m_Player2D; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2DActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2DActions instance)
        {
            if (m_Wrapper.m_Player2DActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_Player2DActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player2DActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player2DActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_Player2DActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player2DActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player2DActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_Player2DActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public Player2DActions @Player2D => new Player2DActions(this);

    // MinigameUI
    private readonly InputActionMap m_MinigameUI;
    private IMinigameUIActions m_MinigameUIActionsCallbackInterface;
    private readonly InputAction m_MinigameUI_Up;
    private readonly InputAction m_MinigameUI_Down;
    private readonly InputAction m_MinigameUI_Select;
    public struct MinigameUIActions
    {
        private @Controls m_Wrapper;
        public MinigameUIActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_MinigameUI_Up;
        public InputAction @Down => m_Wrapper.m_MinigameUI_Down;
        public InputAction @Select => m_Wrapper.m_MinigameUI_Select;
        public InputActionMap Get() { return m_Wrapper.m_MinigameUI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MinigameUIActions set) { return set.Get(); }
        public void SetCallbacks(IMinigameUIActions instance)
        {
            if (m_Wrapper.m_MinigameUIActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnDown;
                @Select.started -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MinigameUIActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_MinigameUIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public MinigameUIActions @MinigameUI => new MinigameUIActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnToggleBackEnd(InputAction.CallbackContext context);
        void OnLock(InputAction.CallbackContext context);
        void OnFastForward(InputAction.CallbackContext context);
        void OnQuickSave(InputAction.CallbackContext context);
        void OnLoadQuickSave(InputAction.CallbackContext context);
    }
    public interface IPlayer2DActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IMinigameUIActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
