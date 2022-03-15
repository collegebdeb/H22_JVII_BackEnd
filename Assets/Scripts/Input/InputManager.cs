using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace devziie.Inputs {

    public class InputManager : MonoBehaviour
    {

        #region Controls
        private static Controls _controls;
        public static Controls Controls
        {
            get
            {
                if (_controls != null) { return _controls; }
                return _controls = new Controls();
            }
        }

        private void Awake()
        {
            if (_controls != null) { return; }
            _controls = new Controls();
        }
        
        private void OnEnable()
        {
            Controls.Enable();
        }
        
        private void OnDisable()
        {
            Controls.Disable();
        }

        private void OnDestroy()
        {
            _controls = null;
        }

        #endregion


    }
}
