using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CannonPath")]
public class ScrCannonPath : ScriptableObject {
    

    public enum pathEndingType { stop, moveToFirst, shoot };
    public enum rotationDirectionType { clockwise, counterClockwise };
    public enum rotationEndingType { stop, reverse, repeat, jumpToNext };
    public enum movementStyleType { smooth, sharp };
    public enum movementEndingType { stop, reverse, jumpToNext };


    [Header("General Variables")]
    [SerializeField] private pathEndingType pathEnding;
    [SerializeField] private float pause;
    
    [Header("Rotation Variables")]
    [SerializeField] private bool rotationTrue;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationDegrees;
    [SerializeField] private rotationDirectionType rotationDirection;
    [SerializeField] private rotationEndingType rotationEnding;

    [Header("Movement Variables")]
    [SerializeField] private bool movementTrue;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementMatchingMultiplier;
    [SerializeField] private Vector2 movementCoordinates;
    [SerializeField] private Vector2 reverseCoordinates;
    [SerializeField] private movementStyleType movementStyle; 
    [SerializeField] private movementEndingType movementEnding;

    public pathEndingType PathEnding { get => pathEnding; set => pathEnding = value; }
    public float Pause { get => pause; set => pause = value; }
    public bool RotationTrue { get => rotationTrue; set => rotationTrue = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float RotationDegrees { get => rotationDegrees; set => rotationDegrees = value; }
    public rotationDirectionType RotationDirection { get => rotationDirection; set => rotationDirection = value; }
    public rotationEndingType RotationEnding { get => rotationEnding; set => rotationEnding = value; }
    public bool MovementTrue { get => movementTrue; set => movementTrue = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Vector2 MovementCoordinates { get => movementCoordinates; set => movementCoordinates = value; }
    public movementStyleType MovementStyle { get => movementStyle; set => movementStyle = value; }
    public movementEndingType MovementEnding { get => movementEnding; set => movementEnding = value; }
    public float MovementMatchingMultiplier { get => movementMatchingMultiplier; set => movementMatchingMultiplier = value; }
    public Vector2 ReverseCoordinates { get => reverseCoordinates; set => reverseCoordinates = value; }
}
