using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStage : MonoBehaviour
    {
        [SerializeField] private CanvasScaler scaler;
        [SerializeField] private int referenceResolutionX = 2930;
        private void Start()
        {
            if (UIUtility.IsThisDeviceTablet())
            {
                scaler.referenceResolution = new Vector2(referenceResolutionX, scaler.referenceResolution.y);
            }
        }
    }
}