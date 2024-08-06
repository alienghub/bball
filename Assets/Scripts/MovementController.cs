using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    #region Paths and Controls
    public enum pathControlType { controllable, locked };
    public enum pathEventType { onConstant, onBeforeLoad, onLoad, onUnload };
    public enum cameraXYFollowType { lockedBetweenTwoCannons, lockedBetweenOneCannonPaths, lockedToRelativeCoordinates, lockedToAbsoluteCoordinates, stableCamera, followCannon };
    public enum cameraZPointType { desiredHeightWidthSize, oneCannon, twoCannons, depth, stable };
    public enum stateType { loaded, unloaded, empty };
    [SerializeField] public int id;
    [Header("Path Properties")]
    [SerializeField] public ScrCannonPath[] cannonPathsConstant;
    bool cannonPathsConstantExist = false;
   
    [SerializeField] public ScrCannonPath[] cannonPathsOnBeforeLoad;
    bool cannonPathsOnBeforeLoadExist = false;
    [SerializeField] public ScrCannonPath[] cannonPathsOnLoad;
    bool cannonPathsOnLoadExist = false;
    [SerializeField] public ScrCannonPath[] cannonPathsOnUnload;
    bool cannonPathsOnUnloadExist = false;
    [SerializeField] pathControlType pathControl;
    stateType state;
    #endregion

    #region Event Variables
    private bool[] rotationTrue;
    private float[] rotationDegrees;
    private float[] rotationSpeed;
    private ScrCannonPath.rotationDirectionType[] rotationDirection;
    private ScrCannonPath.rotationEndingType[] rotationEnding;

    private bool[] movementTrue;
    private float[] movementSpeed;
    private float[] movementMatchingMultiplier;
    private Vector2[] movementCoordinates;
    private Vector2[] reverseCoordinates;
    private ScrCannonPath.movementStyleType[] movementStyle;
    private ScrCannonPath.movementEndingType[] movementEnding;
    #endregion

    #region Other Variables
    private int currentCannonPathConstant = 0;
    private int currentCannonPathOnBeforeLoad = 0;
    private int currentCannonPathOnLoad = 0;
    private int currentCannonPathOnUnload = 0;
    private bool broughtToMoventHalt = false;
    private bool broughtToRotationHalt = false;
    private bool ableToCatch = true;

    private bool isReverseMoving = false;
    private bool isReverseRotating = false;

    private float zPosition, zTarget;
    private Vector3 anchorPosition;
    private float anchorDegrees;
    [Header("Other Properties")]
    [SerializeField] private bool destroyOnUnload;
    [SerializeField] private float pauseBeforeDestruction; 
    #endregion

    #region Camera Controls
    //private ObjCameraController cameraController;
    //private ObjParticleController particleController;
    [Header("Camera Properties")]
    //[SerializeField] ScrCameraMovement cameraMovement;
    /*[SerializeField]*/ private cameraXYFollowType horizontalFollowType;
    /*[SerializeField]*/ private cameraXYFollowType verticalFollowType;
    /*[SerializeField]*/ private float verticalParameter;
    /*[SerializeField]*/ private float horizontalParameter;
    [Space]
    /*[SerializeField]*/ private cameraZPointType cameraDepthType;
    /*[SerializeField]*/ private bool horizontalZAlignment;
    /*[SerializeField]*/ private float cameraZParameter;
    /*[SerializeField]*/ private float minCameraHeight, maxCameraHeight, cameraAmplitude;

    #endregion

    #region Wobble Animation Properties
    //[Header("Wobble Properties")]
    //[SerializeField] private ScrWobbleSettings wobbleSettings;
    //[SerializeField] private float inactivityPauseAfterWobbling;
    //private int currentWobble = -1;
    //private bool wobbleInactivity = false;

    //public bool AbleToCatch { get => ableToCatch; set => ableToCatch = value; }
    //public bool WobbleInactivity { get => wobbleInactivity; set => wobbleInactivity = value; }
    #endregion

    private void Awake() {
        //cameraController = FindObjectOfType<ObjCameraController>();
        //cameraController.RegisterCannon(id, gameObject);
        //particleController = FindObjectOfType<ObjParticleController>();
        anchorPosition = transform.position;
    }
    void Start() {

        anchorDegrees = transform.localEulerAngles.z % 360;
        zPosition = anchorPosition.z;
        state = stateType.empty;

        #region Event Variables Initiation;
        rotationTrue = new bool[4];
        rotationDegrees = new float[4];
        rotationSpeed = new float[4];
        rotationDirection = new ScrCannonPath.rotationDirectionType[4];
        rotationEnding = new ScrCannonPath.rotationEndingType[4];

        movementTrue = new bool[4];
        movementSpeed = new float[4];
        movementMatchingMultiplier = new float[4];
        movementCoordinates = new Vector2[4];
        reverseCoordinates = new Vector2[4];
        movementStyle = new ScrCannonPath.movementStyleType[4];
        movementEnding = new ScrCannonPath.movementEndingType[4];
        #endregion

        #region Camera Movement Settings
        /*
        horizontalFollowType = cameraMovement.HorizontalFollowType;
        verticalFollowType = cameraMovement.VerticalFollowType;
        verticalParameter = cameraMovement.VerticalParameter;
        horizontalParameter = cameraMovement.HorizontalParameter;
        cameraDepthType = cameraMovement.CameraDepthType;
        horizontalZAlignment = cameraMovement.HorizontalZAlignment;
        cameraZParameter = cameraMovement.CameraZParameter;
        minCameraHeight = cameraMovement.MinCameraHeight;
        maxCameraHeight = cameraMovement.MaxCameraHeight;
        cameraAmplitude = cameraMovement.CameraAmplitude;
        */
        #endregion

        #region Read Paths
        if (cannonPathsConstant.Length>0) {
            cannonPathsConstantExist = true;
            ReadMovementData(pathEventType.onConstant);
        }
        /*
        if (cannonPathsOnBeforeLoad.Length > 0) {
            cannonPathsOnBeforeLoadExist = true;
            ReadMovementData(pathEventType.onBeforeLoad);
        }
        if (cannonPathsOnLoad.Length > 0) {
            cannonPathsOnLoadExist = true;
            ReadMovementData(pathEventType.onLoad);
        }
        if (cannonPathsOnUnload.Length > 0) {
            cannonPathsOnUnloadExist = true;
            ReadMovementData(pathEventType.onUnload);
        }*/
        #endregion

        //TESTABLE
        /*
        if (id == 0) {
            cameraController.MoveCameraInstantly(GetPointForCameraMovement(true, horizontalFollowType),
                GetPointForCameraMovement(false, verticalFollowType),
                SetCameraDepthTarget(cameraDepthType, horizontalZAlignment, cameraZParameter, minCameraHeight, maxCameraHeight, cameraAmplitude));
            PassDefaultCameraMovementValues();
        }*/
    }

    void Update() {
        if (cannonPathsConstantExist) {
            MoveAndRotateCannon(0);
        }/*
        switch(state) {
            case stateType.empty:
                if (cannonPathsOnBeforeLoadExist) {
                    MoveAndRotateCannon(1);
                }
                break;
            case stateType.loaded:
                if (cannonPathsOnLoadExist) {
                    MoveAndRotateCannon(2);
                }
                break;
            case stateType.unloaded:
                if (cannonPathsOnUnloadExist) {
                    MoveAndRotateCannon(3);
                }
                break;
        }*/

        /*
        if (currentWobble != -1) {
            currentWobble = StatAnimationManager.Wobble(gameObject, wobbleSettings, currentWobble);
        }*/
    }

    private void RotateCannon(int pathEvent) {

        float rotationDegreesHere;
        if (isReverseRotating) {
            rotationDegreesHere = anchorDegrees;
        }
        else {
            rotationDegreesHere = rotationDegrees[pathEvent];
        }


        if (rotationTrue[pathEvent]) {

            bool rotationDirectionHere;
            if (rotationDirection[pathEvent] == ScrCannonPath.rotationDirectionType.clockwise) {
                if (isReverseRotating) {
                    rotationDirectionHere = false;
                }
                else {
                    rotationDirectionHere = true;
                }
            }
            else {
                if (isReverseRotating) {
                    rotationDirectionHere = true;
                }
                else {
                    rotationDirectionHere = false;
                }
            }
            if (!StatAnimationManager.AnimateRotationZOneCircle(gameObject, rotationSpeed[pathEvent], rotationDegreesHere, rotationDirectionHere)) {
                switch (rotationEnding[pathEvent]) {
                    case ScrCannonPath.rotationEndingType.jumpToNext:
                        anchorDegrees = transform.localEulerAngles.z % 360;
                        JumpToNextPath(pathEvent);
                        break;
                    case ScrCannonPath.rotationEndingType.reverse:
                        if (isReverseRotating) { isReverseRotating = false; } else { isReverseRotating = true; }
                        break;
                    case ScrCannonPath.rotationEndingType.stop:
                        broughtToRotationHalt = true;
                        break;
                    }
             }
        
        }
    }

    /*
    public void ChangeState(stateType state) {
        this.state = state;
        if (state == stateType.unloaded) {
            StartCoroutine(BecomeUntouchableAfterWobble());
        }
    }*/

    private void MoveCannon(int pathEvent) {

        float movementMatching;
        Vector3 movementTargetPosition;
        movementMatching = movementMatchingMultiplier[pathEvent] * movementSpeed[pathEvent];

        if (isReverseMoving) {
            movementTargetPosition = new Vector3(anchorPosition.x + reverseCoordinates[pathEvent].x,
                anchorPosition.y + reverseCoordinates[pathEvent].y,
                zPosition);
        }
        else {
            movementTargetPosition = new Vector3(anchorPosition.x + movementCoordinates[pathEvent].x,
                anchorPosition.y + movementCoordinates[pathEvent].y,
                zPosition);
        }

        if (movementTrue[pathEvent]) {
            if (movementStyle[pathEvent] == ScrCannonPath.movementStyleType.smooth) {
                if (!StatMovementManager.MoveSmoothlyInstance3D(gameObject, movementTargetPosition, movementSpeed[pathEvent], movementMatching, false)) {
                    //Debug.Log(movementTargetPosition.y - transform.position.y);
                    switch (movementEnding[pathEvent]) {
                        case ScrCannonPath.movementEndingType.jumpToNext:
                            JumpToNextPath(pathEvent);
                            break;
                        case ScrCannonPath.movementEndingType.reverse:
                            if (isReverseMoving) { isReverseMoving = false; } else { isReverseMoving = true; }
                            break;
                        case ScrCannonPath.movementEndingType.stop:
                            broughtToMoventHalt = true;
                            break;
                    }
                }
            }
            else {
                if (!StatMovementManager.MoveUsuallyInstance3D(gameObject, movementTargetPosition, movementSpeed[pathEvent])) {
                    switch (movementEnding[pathEvent]) {
                        case ScrCannonPath.movementEndingType.jumpToNext:
                            JumpToNextPath(pathEvent);
                            break;
                        case ScrCannonPath.movementEndingType.reverse:
                            if (isReverseMoving) { isReverseMoving = false; } else { isReverseMoving = true; }
                            break;
                        case ScrCannonPath.movementEndingType.stop:
                            broughtToMoventHalt = true;
                            break;
                    }
                }
            }
        }
    }

    private void MoveAndRotateCannon(int pathEvent) {
        if (!broughtToMoventHalt) {
            MoveCannon(pathEvent);
        }
        if (!broughtToRotationHalt) {
            RotateCannon(pathEvent);
        }
    }

    private void ReadMovementData(pathEventType pathEvent) {
        int eventNumber;
        switch (pathEvent) {
            case pathEventType.onConstant: eventNumber = 0;  break;
            case pathEventType.onBeforeLoad: eventNumber = 1; break;
            case pathEventType.onLoad: eventNumber = 2; break;
            case pathEventType.onUnload: eventNumber = 3;  break;
            default: eventNumber = 0; break;
        }

        switch(eventNumber) {
            case 0:
                rotationTrue[eventNumber] = cannonPathsConstant[currentCannonPathConstant].RotationTrue;
                rotationEnding[eventNumber] = cannonPathsConstant[currentCannonPathConstant].RotationEnding;
                if (rotationEnding[eventNumber] == ScrCannonPath.rotationEndingType.repeat) {
                    rotationDegrees[eventNumber] = 700;
                }
                else {
                    rotationDegrees[eventNumber] = cannonPathsConstant[currentCannonPathConstant].RotationDegrees;
                }
                rotationSpeed[eventNumber] = cannonPathsConstant[currentCannonPathConstant].RotationSpeed;
                rotationDirection[eventNumber] = cannonPathsConstant[currentCannonPathConstant].RotationDirection;

                reverseCoordinates[eventNumber] = cannonPathsConstant[currentCannonPathConstant].ReverseCoordinates;
                movementTrue[eventNumber] = cannonPathsConstant[currentCannonPathConstant].MovementTrue;
                movementCoordinates[eventNumber] = cannonPathsConstant[currentCannonPathConstant].MovementCoordinates;
                movementSpeed[eventNumber] = cannonPathsConstant[currentCannonPathConstant].MovementSpeed;
                movementMatchingMultiplier[eventNumber] = cannonPathsConstant[currentCannonPathConstant].MovementMatchingMultiplier;
                movementStyle[eventNumber] = cannonPathsConstant[currentCannonPathConstant].MovementStyle;
                movementEnding[eventNumber] = cannonPathsConstant[currentCannonPathConstant].MovementEnding;
               break;

            case 1:
                rotationTrue[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].RotationTrue;
                rotationEnding[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].RotationEnding;
                if (rotationEnding[eventNumber] == ScrCannonPath.rotationEndingType.repeat) {
                    rotationDegrees[eventNumber] = 700;
                }
                else {
                    rotationDegrees[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].RotationDegrees;
                }
                rotationSpeed[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].RotationSpeed;
                rotationDirection[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].RotationDirection;

                reverseCoordinates[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].ReverseCoordinates;
                movementTrue[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].MovementTrue;
                movementCoordinates[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].MovementCoordinates;
                movementSpeed[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].MovementSpeed;
                movementMatchingMultiplier[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].MovementMatchingMultiplier;
                movementStyle[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].MovementStyle;
                movementEnding[eventNumber] = cannonPathsOnBeforeLoad[currentCannonPathOnBeforeLoad].MovementEnding;
                break;
            case 2:
                rotationTrue[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].RotationTrue;
                rotationEnding[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].RotationEnding;
                if (rotationEnding[eventNumber] == ScrCannonPath.rotationEndingType.repeat) {
                    rotationDegrees[eventNumber] = 700;
                }
                else {
                    rotationDegrees[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].RotationDegrees;
                }
                rotationSpeed[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].RotationSpeed;
                rotationDirection[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].RotationDirection;

                reverseCoordinates[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].ReverseCoordinates;
                movementTrue[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].MovementTrue;
                movementCoordinates[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].MovementCoordinates;
                movementSpeed[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].MovementSpeed;
                movementMatchingMultiplier[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].MovementMatchingMultiplier;
                movementStyle[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].MovementStyle;
                movementEnding[eventNumber] = cannonPathsOnLoad[currentCannonPathOnLoad].MovementEnding;
                break;
            case 3:
                rotationTrue[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].RotationTrue;
                rotationEnding[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].RotationEnding;
                if (rotationEnding[eventNumber] == ScrCannonPath.rotationEndingType.repeat) {
                    rotationDegrees[eventNumber] = 500;
                }
                else {
                    rotationDegrees[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].RotationDegrees;
                }
                rotationSpeed[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].RotationSpeed;
                rotationDirection[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].RotationDirection;

                reverseCoordinates[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].ReverseCoordinates;
                movementTrue[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].MovementTrue;
                movementCoordinates[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].MovementCoordinates;
                movementSpeed[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].MovementSpeed;
                movementMatchingMultiplier[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].MovementMatchingMultiplier;
                movementStyle[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].MovementStyle;
                movementEnding[eventNumber] = cannonPathsOnUnload[currentCannonPathOnUnload].MovementEnding;
                break;
        }

    }

    private void JumpToNextPath(int pathEvent) {
        int presentCannonPathNumber;
        int presentPathsLength;
        ScrCannonPath presentCannonPath;

        anchorPosition = transform.position;

        switch (pathEvent) {
            case 0:
                presentCannonPathNumber = currentCannonPathConstant;
                presentCannonPath = cannonPathsConstant[presentCannonPathNumber];
                presentPathsLength = cannonPathsConstant.Length;
                break;
            case 1:
                presentCannonPathNumber = currentCannonPathOnBeforeLoad;
                presentCannonPath = cannonPathsOnBeforeLoad[presentCannonPathNumber];
                presentPathsLength = cannonPathsOnBeforeLoad.Length;
                break;
            case 2:
                presentCannonPathNumber = currentCannonPathOnLoad;
                presentCannonPath = cannonPathsOnLoad[presentCannonPathNumber];
                presentPathsLength = cannonPathsOnLoad.Length;
                break;
            case 3:
                presentCannonPathNumber = currentCannonPathOnUnload;
                presentCannonPath = cannonPathsOnUnload[presentCannonPathNumber];
                presentPathsLength = cannonPathsOnUnload.Length;
                break;
            default:
                presentCannonPathNumber = 0;
                presentCannonPath = null;
                presentPathsLength = 0;
                break;
        }

        if (presentCannonPathNumber >= presentPathsLength-1) {
            switch (presentCannonPath.PathEnding) {
                case ScrCannonPath.pathEndingType.moveToFirst:
                    presentCannonPathNumber = 0;
                break;
                case ScrCannonPath.pathEndingType.shoot:

                break;
                case ScrCannonPath.pathEndingType.stop:
                    broughtToMoventHalt = true;
                    broughtToRotationHalt = true;
                break;
            }
        }
        else {
            presentCannonPathNumber++;
        }

        switch (pathEvent) {
            case 0:
                currentCannonPathConstant = presentCannonPathNumber;
                ReadMovementData(pathEventType.onConstant);
            break;
            case 1:
                currentCannonPathOnBeforeLoad = presentCannonPathNumber;
                ReadMovementData(pathEventType.onBeforeLoad);
                break;
            case 2:
                currentCannonPathOnLoad = presentCannonPathNumber;
                ReadMovementData(pathEventType.onLoad);
                break;
            case 3:
                currentCannonPathOnUnload = presentCannonPathNumber;
                ReadMovementData(pathEventType.onUnload);
                break;
            default:
            break;
        }
    }

    #region Lowest and Highest Values
    public float GetHighestCoordinateValue(bool horizontal, bool beginning) {
        float highestValue;
        if (horizontal) {
            #region Horizontal Checking
            highestValue = anchorPosition.x;
            if (cannonPathsConstantExist) {
                for (var i = 0; i < cannonPathsConstant.Length; i++) {
                    if (cannonPathsConstant[i].MovementCoordinates.x + anchorPosition.x > highestValue) {
                        highestValue = cannonPathsConstant[i].MovementCoordinates.x + anchorPosition.x;
                    }
                    if (cannonPathsConstant[i].ReverseCoordinates.x + anchorPosition.x > highestValue) {
                        highestValue = cannonPathsConstant[i].ReverseCoordinates.x + anchorPosition.x;
                    }
                }

                if (beginning) {
                    for (var i = 0; i < cannonPathsOnLoad.Length; i++) {
                        if (cannonPathsOnLoad[i].MovementCoordinates.x + anchorPosition.x > highestValue) {
                            highestValue = cannonPathsOnLoad[i].MovementCoordinates.x + anchorPosition.x;
                        }
                        if (cannonPathsOnLoad[i].ReverseCoordinates.x + anchorPosition.x > highestValue) {
                            highestValue = cannonPathsOnLoad[i].ReverseCoordinates.x + anchorPosition.x;
                        }
                    }
                }
                else {
                    for (var i = 0; i < cannonPathsOnBeforeLoad.Length; i++) {
                        if (cannonPathsOnBeforeLoad[i].MovementCoordinates.x + anchorPosition.x > highestValue) {
                            highestValue = cannonPathsOnBeforeLoad[i].MovementCoordinates.x + anchorPosition.x;
                        }
                        if (cannonPathsOnBeforeLoad[i].ReverseCoordinates.x + anchorPosition.x > highestValue) {
                            highestValue = cannonPathsOnBeforeLoad[i].ReverseCoordinates.x + anchorPosition.x;
                        }
                    }
                }
            }
            #endregion
        }
        else {
            #region Vertical Checking
            highestValue = anchorPosition.y;
            if (cannonPathsConstantExist) {
                for (var i = 0; i < cannonPathsConstant.Length; i++) {
                    if (cannonPathsConstant[i].MovementCoordinates.y + anchorPosition.y > highestValue) {
                        highestValue = cannonPathsConstant[i].MovementCoordinates.y + anchorPosition.y;
                    }
                    if (cannonPathsConstant[i].ReverseCoordinates.y + anchorPosition.y > highestValue) {
                        highestValue = cannonPathsConstant[i].ReverseCoordinates.y + anchorPosition.y;
                    }
                }

                if (beginning) {
                    for (var i = 0; i < cannonPathsOnLoad.Length; i++) {
                        if (cannonPathsOnLoad[i].MovementCoordinates.y + anchorPosition.y > highestValue) {
                            highestValue = cannonPathsOnLoad[i].MovementCoordinates.y + anchorPosition.y;
                        }
                        if (cannonPathsOnLoad[i].ReverseCoordinates.y + anchorPosition.y > highestValue) {
                            highestValue = cannonPathsOnLoad[i].ReverseCoordinates.y + anchorPosition.y;
                        }
                    }
                }
                else {
                    for (var i = 0; i < cannonPathsOnBeforeLoad.Length; i++) {
                        if (cannonPathsOnBeforeLoad[i].MovementCoordinates.y + anchorPosition.y > highestValue) {
                            highestValue = cannonPathsOnBeforeLoad[i].MovementCoordinates.y + anchorPosition.y;
                        }
                        if (cannonPathsOnBeforeLoad[i].ReverseCoordinates.y + anchorPosition.y > highestValue) {
                            highestValue = cannonPathsOnBeforeLoad[i].ReverseCoordinates.y + anchorPosition.y;
                        }
                    }
                }
            }
            #endregion
        }
        return highestValue;
    }

    public float GetLowestCoordinateValue(bool horizontal, bool beginning) {
        float lowestValue = 0;
        if (horizontal) {
            #region Horizontal Checking
            if (cannonPathsConstantExist) {
                lowestValue = anchorPosition.x;
                for (var i = 0; i < cannonPathsConstant.Length; i++) {
                    if (cannonPathsConstant[i].MovementCoordinates.x + anchorPosition.x < lowestValue) {
                        lowestValue = cannonPathsConstant[i].MovementCoordinates.x + anchorPosition.x;
                    }
                    if (cannonPathsConstant[i].ReverseCoordinates.x + anchorPosition.x < lowestValue) {
                        lowestValue = cannonPathsConstant[i].ReverseCoordinates.x + anchorPosition.x;
                    }
                }

                if (beginning) {
                    for (var i = 0; i < cannonPathsOnLoad.Length; i++) {
                        if (cannonPathsOnLoad[i].MovementCoordinates.x + anchorPosition.x < lowestValue) {
                            lowestValue = cannonPathsOnLoad[i].MovementCoordinates.x + anchorPosition.x;
                        }
                        if (cannonPathsOnLoad[i].ReverseCoordinates.x + anchorPosition.x < lowestValue) {
                            lowestValue = cannonPathsOnLoad[i].ReverseCoordinates.x + anchorPosition.x;
                        }
                    }
                }
                else {
                    for (var i = 0; i < cannonPathsOnBeforeLoad.Length; i++) {
                        if (cannonPathsOnBeforeLoad[i].MovementCoordinates.x + anchorPosition.x < lowestValue) {
                            lowestValue = cannonPathsOnBeforeLoad[i].MovementCoordinates.x + anchorPosition.x;
                        }
                        if (cannonPathsOnBeforeLoad[i].ReverseCoordinates.x + anchorPosition.x < lowestValue) {
                            lowestValue = cannonPathsOnBeforeLoad[i].ReverseCoordinates.x + anchorPosition.x;
                        }
                    }
                }
            }
            #endregion
        }
        else {
            #region Vertical Checking
            lowestValue = anchorPosition.y;
            if (cannonPathsConstantExist) {
                for (var i = 0; i < cannonPathsConstant.Length; i++) {
                    if (cannonPathsConstant[i].MovementCoordinates.y + anchorPosition.y < lowestValue) {
                        lowestValue = cannonPathsConstant[i].MovementCoordinates.y + anchorPosition.y;
                    }
                    if (cannonPathsConstant[i].ReverseCoordinates.y + anchorPosition.y < lowestValue) {
                        lowestValue = cannonPathsConstant[i].ReverseCoordinates.y + anchorPosition.y;
                    }
                }

                if (beginning) {
                    for (var i = 0; i < cannonPathsOnLoad.Length; i++) {
                        if (cannonPathsOnLoad[i].MovementCoordinates.y + anchorPosition.y < lowestValue) {
                            lowestValue = cannonPathsOnLoad[i].MovementCoordinates.y + anchorPosition.y;
                        }
                        if (cannonPathsOnLoad[i].ReverseCoordinates.y + anchorPosition.y < lowestValue) {
                            lowestValue = cannonPathsOnLoad[i].ReverseCoordinates.y + anchorPosition.y;
                        }
                    }
                }
                else {
                    for (var i = 0; i < cannonPathsOnBeforeLoad.Length; i++) {
                        if (cannonPathsOnBeforeLoad[i].MovementCoordinates.y + anchorPosition.y < lowestValue) {
                            lowestValue = cannonPathsOnBeforeLoad[i].MovementCoordinates.y + anchorPosition.y;
                        }
                        if (cannonPathsOnBeforeLoad[i].ReverseCoordinates.y + anchorPosition.y < lowestValue) {
                            lowestValue = cannonPathsOnBeforeLoad[i].ReverseCoordinates.y + anchorPosition.y;
                        }
                    }
                }
            }
            #endregion
        }
        return lowestValue;
    }
    #endregion

    #region Camera Movement Scripts

    /*
    public void PassDefaultCameraMovementValues() {
        SetCameraMovement(true, horizontalFollowType);
        SetCameraMovement(false, verticalFollowType);
        PassCameraDepthTargetToController(); 
    }

    public float GetPointForCameraMovement(bool horizontal, cameraXYFollowType pointType) {
        
        switch(pointType) {

            case cameraXYFollowType.lockedBetweenOneCannonPaths:
                if (horizontal) { StatCompareArrays.ReturnMean(GetHighestCoordinateValue(true, true), GetLowestCoordinateValue(true, true)); }
                else { StatCompareArrays.ReturnMean(GetHighestCoordinateValue(false, true), GetLowestCoordinateValue(false, true)); }
                break;
            case cameraXYFollowType.lockedBetweenTwoCannons:
                if (horizontal) {
                    Vector2 numbers = StatCompareArrays.ReturnMeanFurthestPair(GetHighestCoordinateValue(true, true), GetLowestCoordinateValue(true, true),
                        cameraController.GetHighestCoordinateValue(id+1, true, false), cameraController.GetLowestCoordinateValue(id+1, true, false));
                    return StatCompareArrays.ReturnMean(numbers.x, numbers.y);
                }
                else {
                    Vector2 numbers = StatCompareArrays.ReturnMeanFurthestPair(GetHighestCoordinateValue(false, true), GetLowestCoordinateValue(false, true),
                    cameraController.GetHighestCoordinateValue(id + 1, false, false), cameraController.GetLowestCoordinateValue(id + 1, false, false));
                    return StatCompareArrays.ReturnMean(numbers.x, numbers.y);
                }
                break;
            case cameraXYFollowType.stableCamera:
                if (horizontal) { return cameraController.GetCameraCoordinates().x; }
                else { return cameraController.GetCameraCoordinates().y; }
                break;
            case cameraXYFollowType.followCannon:
                if (horizontal) { return transform.position.x; }
                else { return transform.position.y; }
                break;
            case cameraXYFollowType.lockedToAbsoluteCoordinates:
                if (horizontal) { Debug.Log("Horizontal parameter is " + horizontalParameter); return horizontalParameter; }
                else { Debug.Log("Vertical parameter is " + verticalParameter); return verticalParameter; }
                break;
            case cameraXYFollowType.lockedToRelativeCoordinates:
                if (horizontal) { return anchorPosition.x + horizontalParameter; }
                else { return anchorPosition.y + verticalParameter; }
                break;
        }
        return 0;
    }*/

    /*
    public void SetCameraMovement(bool horizontalIsTrue,cameraXYFollowType cameraFollowType) {
        float pointToFollow = GetPointForCameraMovement(horizontalIsTrue, cameraFollowType);
        switch(cameraFollowType) {
            case cameraXYFollowType.followCannon:
                cameraController.SetFollowingAlignment(horizontalIsTrue, ObjCameraController.cameraFollowingType.followingCannon, pointToFollow, gameObject);
                break;
            case cameraXYFollowType.lockedBetweenOneCannonPaths:
            case cameraXYFollowType.lockedBetweenTwoCannons:
            case cameraXYFollowType.lockedToRelativeCoordinates:
            case cameraXYFollowType.lockedToAbsoluteCoordinates:
                cameraController.SetFollowingAlignment(horizontalIsTrue, ObjCameraController.cameraFollowingType.followingPoint, pointToFollow, gameObject);
                break;
            case cameraXYFollowType.stableCamera:
                cameraController.SetFollowingAlignment(horizontalIsTrue, ObjCameraController.cameraFollowingType.stable, pointToFollow, gameObject);
                break;
        }
    }*/

    /*
    public void PassCameraDepthTargetToController() {
        float cameraHeightToPass = SetCameraDepthTarget(cameraDepthType, horizontalZAlignment, cameraZParameter, minCameraHeight, maxCameraHeight, cameraAmplitude);
        cameraController.SetZFollowing(cameraHeightToPass);
    }*/

    /*
    public float SetCameraDepthTarget(cameraZPointType zPointType, bool horizontal, float parameter, float minHeight, float maxHeight, float amplitude) {
        float cameraHeight;
        float currentCameraHeight = cameraController.GetCamera().transform.position.z;
        switch (zPointType) {
            case cameraZPointType.depth:
                cameraHeight = cameraZParameter;
                break;
            case cameraZPointType.desiredHeightWidthSize:
                if (horizontal) {
                    cameraHeight = -StatAnimationManager.GetCameraZoomWithWidthHeight(cameraController.GetCamera(), true, parameter);
                }
                else {
                    cameraHeight = -StatAnimationManager.GetCameraZoomWithWidthHeight(cameraController.GetCamera(), false, parameter);
                }
                break;
            case cameraZPointType.oneCannon:
                if (horizontal) {
                    float distance = Mathf.Abs(GetHighestCoordinateValue(true, true) - GetLowestCoordinateValue(true, true));
                    Debug.Log("Distance should be this " + distance);
                    cameraHeight = -StatAnimationManager.GetCameraZoomWithWidthHeight(cameraController.GetCamera(), true, distance);
                }
                else {
                    float distance = Mathf.Abs(GetHighestCoordinateValue(false, true) - GetLowestCoordinateValue(false, true));
                    cameraHeight = -StatAnimationManager.GetCameraZoomWithWidthHeight(cameraController.GetCamera(), false, distance);
                }
                break;
            case cameraZPointType.twoCannons:
                if (horizontal) {
                    Vector2 numbers = StatCompareArrays.ReturnMeanFurthestPair(GetHighestCoordinateValue(true, true), GetLowestCoordinateValue(true, true),
                    cameraController.GetHighestCoordinateValue(id + 1, true, false), cameraController.GetLowestCoordinateValue(id + 1, true, false));
                    float distance = Mathf.Abs(numbers.x - numbers.y) + parameter * 2;
                    cameraHeight = -StatAnimationManager.GetCameraZoomWithWidthHeight(cameraController.GetCamera(), true, distance);
                }
                else {
                    Vector2 numbers = StatCompareArrays.ReturnMeanFurthestPair(GetHighestCoordinateValue(false, true), GetLowestCoordinateValue(false, true),
                    cameraController.GetHighestCoordinateValue(id + 1, false, false), cameraController.GetLowestCoordinateValue(id + 1, false, false));
                    float distance = Mathf.Abs(numbers.x - numbers.y) + parameter * 2; ;
                    cameraHeight = -StatAnimationManager.GetCameraZoomWithWidthHeight(cameraController.GetCamera(), false, distance);
                }
                break;
            default:
                cameraHeight = currentCameraHeight;
                break;
        }

        if (Mathf.Abs(currentCameraHeight - cameraHeight) < amplitude) { return currentCameraHeight; }
        else if (cameraHeight < minHeight) { return minHeight; }
        else if (cameraHeight > maxHeight) { return maxHeight;  }
        else { return cameraHeight;  }
    }
    */

    #endregion


    /*
    #region Wobble Properties

    public void StartWobbleAnimation() {
        currentWobble = 0;
    }
    #endregion

    #region IEnumerator Become untouchable
    
     IEnumerator BecomeUntouchableAfterWobble() {
        WobbleInactivity = true;
        yield return new WaitForSeconds(inactivityPauseAfterWobbling);
        WobbleInactivity = false;
    }

    #endregion
    */

    #region IEnumerator Explode


    #endregion
}
