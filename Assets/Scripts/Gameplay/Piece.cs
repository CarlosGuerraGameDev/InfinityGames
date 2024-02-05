using System;
using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay
{
    public class Piece : MonoBehaviour, IPiece
    {
        [Header("Pieces - Configuration")] [Space(5)] [SerializeField]
        private List<Direction> desiredDirections;

        [SerializeField] private Direction currentDirection;
        [SerializeField] private PieceSo pieceSo;

        [Space(5)] [Header("Pieces - Components")] [Space(5)] [SerializeField]
        private RectTransform rectTransform;

        [SerializeField] private Image imageColor;

        [Space(5)] [Header("Piece - Rotation Configuration")] [Space(5)] [SerializeField]
        private float rotationDuration = 1f;

        [field: SerializeField] public bool HasRightRotation { get; private set; }

        [SerializeField] private bool isRotating;

        [SerializeField] private bool canRotate = true;

        [Space(5)] [Header("Piece - Animation")] [Space(5)]
        public UnityEvent whenPieceRotates;

        [Space(5)] [Header("Piece - Animation")] [Space(5)] [SerializeField]
        private Ease rotationAnim;



        public delegate void CheckPuzzleConditionIsMet();

        public static event CheckPuzzleConditionIsMet OnCheckPuzzleConditionIsMet;

        public bool CanRotate
        {
            get => canRotate;
            set => canRotate = value;
        }

        private void Start()
        {
            imageColor = gameObject.GetComponent<Image>();
        }

        public void Rotate()
        {
            if (!CanRotate) return;

            var currentRotation = rectTransform.eulerAngles.z;

            switch (pieceSo.rotationType)
            {
                case RotationType.Four:

                    var target90Rotation = currentRotation + -90.0f;
                    target90Rotation = (target90Rotation + 360.0f) % 360.0f;
                    
                    if (!isRotating)
                    {
                        whenPieceRotates.Invoke();
                        isRotating = true;
                        rectTransform.DOLocalRotate(new Vector3(0, 0, target90Rotation), rotationDuration)
                            .SetEase(rotationAnim)
                            .OnComplete(() =>
                            {
                                isRotating = false;
                                CheckIfRightRotation();
                                OnCheckPuzzleConditionIsMet?.Invoke();
                            });
                    }

                    break;
                case RotationType.Eight:

                    var target45Rotation = currentRotation + -45.0f;
                    target45Rotation = (target45Rotation + 360.0f) % 360.0f;

                    if (!isRotating)
                    {
                        whenPieceRotates.Invoke();
                        isRotating = true;
                        transform.DORotate(new Vector3(0, 0, target45Rotation), rotationDuration)
                            .SetEase(Ease.Linear)
                            .SetEase(rotationAnim)
                            .OnComplete(() =>
                            {
                                isRotating = false;
                                CheckIfRightRotation();
                                OnCheckPuzzleConditionIsMet?.Invoke();
                            });
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckIfRightRotation()
        {
            var pieceSoPieceDirection = pieceSo.pieceDirection;
            var currentAnglesZ = rectTransform.eulerAngles.z;

            for (int i = 0; i < pieceSoPieceDirection.Directions.Count; i++)
            {
                if (Math.Abs(pieceSoPieceDirection.Directions[i].rotation - currentAnglesZ) < float.Epsilon)
                {
                    Debug.Log(pieceSoPieceDirection.Directions[i].direction);
                    currentDirection = pieceSoPieceDirection.Directions[i].direction;
                }
            }
            
            foreach (var desiredDir in desiredDirections)
            {
                if (currentDirection == desiredDir)
                {
                    HasRightRotation = true;
                    return;
                }                
            }

            HasRightRotation = false;
        }

        public void SetWinningColor()
        {
            imageColor.color = Color.white;
        }
    }
}