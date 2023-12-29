using System;
using System.Collections;
using ScriptableObjects;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Gameplay
{
    public class Piece : MonoBehaviour, IPiece
    {
        [Header("Pieces - Configuration")] [Space(5)] 
        
        [SerializeField] private Direction desiredDirection;
        [SerializeField] private Direction currentDirection;
        [SerializeField] private PieceSo pieceSo;

        [Space(5)] [Header("Pieces - Components")] [Space(5)] 
        
        [SerializeField] private RectTransform rectTransform;

        [Space(5)] [Header("Piece - Rotation Configuration")] [Space(5)] 
        
        [SerializeField] private float rotationSpeed = 0.1f;
        [SerializeField] private bool hasRightRotation;
        [SerializeField] private bool isRotating;

        public void Rotate()
        {
            var currentRotation = rectTransform.eulerAngles.z;
            
            switch (pieceSo.rotationType)
            {
                case RotationType.Four:

                    var target90Rotation = currentRotation + -90.0f;
                    target90Rotation = (target90Rotation + 360.0f) % 360.0f;
                    
                    if (!isRotating)
                    {
                        isRotating = true;
                        StartCoroutine(StepTo(target90Rotation, rotationSpeed));
                    }

                    CheckIfRightRotation();

                    break;
                case RotationType.Eight:
                    
                    var target45Rotation = currentRotation + -45.0f;
                    target45Rotation = (target45Rotation + 360.0f) % 360.0f;
                    
                    if (!isRotating)
                    {
                        isRotating = true;
                        StartCoroutine(StepTo(target45Rotation, rotationSpeed));
                    }

                    CheckIfRightRotation();

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckIfRightRotation()
        {
            var currentRotation = rectTransform.eulerAngles.z;
            var closestRotationDifference = float.MaxValue;
            var closestDirection = Direction.East;
            
            foreach (var piece in pieceSo.pieceDirection.Directions)
            {
                float rotationDifference = Mathf.Abs(piece.rotation - (int)currentRotation);
                
                if (rotationDifference < closestRotationDifference)
                {
                    closestRotationDifference = rotationDifference;
                    closestDirection = piece.direction;
                }
            }
            
            currentDirection = closestDirection;
            hasRightRotation = currentDirection == desiredDirection;
        }
        
        private IEnumerator StepTo(float targetRotation, float duration)
        {
            var startRotation = rectTransform.eulerAngles.z;
            var time = 0.0f;

            while (time < duration)
            {
                var newRotation = Mathf.LerpAngle(startRotation, targetRotation, time / duration);
                rectTransform.rotation = Quaternion.Euler(0, 0, newRotation);

                time += Time.deltaTime;
                yield return null;
            }

            rectTransform.rotation = Quaternion.Euler(0, 0, targetRotation);
            isRotating = false;
        }

    }
}