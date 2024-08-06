using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class StatMovementManager
{
    #region MoveSmoothlyInstance
    public static bool MoveSmoothlyInstance(GameObject obj, Vector2 targetPosition, float speed, float matching) {
        float newPositionX, newPositionY;
        float currentPositionX = obj.transform.position.x;
        float currentPositionY = obj.transform.position.y;
        float currentPositionZ = obj.transform.position.z;
        float matchingConstant = matching*3;

        if ((currentPositionX == targetPosition.x) && (currentPositionY == targetPosition.y)) {
            return false;
        }

        if ((Mathf.Abs(currentPositionX - targetPosition.x) > matching)) {
            newPositionX = Mathf.Lerp(currentPositionX, targetPosition.x, speed * Time.deltaTime);
        }
        else {
            newPositionX = StatAnimationManager.ReachForTarget(currentPositionX,matchingConstant * Time.deltaTime,targetPosition.x);
            //newPositionX = targetPosition.x;
        }

        if ((Mathf.Abs(currentPositionY - targetPosition.y) > matching)) {
            newPositionY = Mathf.Lerp(currentPositionY, targetPosition.y, speed * Time.deltaTime);
        }
        else {
            newPositionY = StatAnimationManager.ReachForTarget(currentPositionY, matchingConstant * Time.deltaTime, targetPosition.y);
            //newPositionY = targetPosition.y;
        }
        var newPosition = new Vector3(newPositionX, newPositionY, currentPositionZ);
        obj.transform.position = newPosition;

        return true;
    }
    #endregion

    #region MoveSmoothInstance3D

    public static bool MoveSmoothlyInstance3D(GameObject obj, Vector3 targetPosition, float speed, float matching, bool movementMatchingFix) {
        float newPositionX, newPositionY, newPositionZ;
        float currentPositionX = obj.transform.position.x;
        float currentPositionY = obj.transform.position.y;
        float currentPositionZ = obj.transform.position.z;
        float matchingConstant = matching * 3;
        short matchingCounter = 0;

        if ((currentPositionX == targetPosition.x) && (currentPositionY == targetPosition.y) && (currentPositionZ == targetPosition.z)) {
            return false;
        }

        if ((Mathf.Abs(currentPositionX - targetPosition.x) > matching)) {
            newPositionX = Mathf.Lerp(currentPositionX, targetPosition.x, speed * Time.deltaTime);
        }
        else {
            newPositionX = StatAnimationManager.ReachForTarget(currentPositionX, matchingConstant * Time.deltaTime, targetPosition.x);
            if (!movementMatchingFix) {
                matchingCounter++;
            }
        }

        if ((Mathf.Abs(currentPositionY - targetPosition.y) > matching)) {
            newPositionY = Mathf.Lerp(currentPositionY, targetPosition.y, speed * Time.deltaTime);
        }
        else {
            newPositionY = StatAnimationManager.ReachForTarget(currentPositionY, matchingConstant * Time.deltaTime, targetPosition.y);
                if (!movementMatchingFix) {
                    matchingCounter++;
                }
            }

        if ((Mathf.Abs(currentPositionZ - targetPosition.z) > matching)) {
            newPositionZ = Mathf.Lerp(currentPositionZ, targetPosition.z, speed * Time.deltaTime);
        }
        else {
            newPositionZ = StatAnimationManager.ReachForTarget(currentPositionZ, matchingConstant * Time.deltaTime, targetPosition.z);
                if (!movementMatchingFix) {
                    matchingCounter++;
                }
        }

        if (matchingCounter == 3) {
            return false;
        }

        var newPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
        obj.transform.position = newPosition;

        return true;
    }

    #endregion

    #region MoveSmoothly
    public static Vector3 MoveSmoothly(Vector2 currentPosition, Vector2 targetPosition, float speed, float matching) {
        float newPositionX, newPositionY;
        if ((Mathf.Abs(currentPosition.x - targetPosition.x) > matching)) {
            newPositionX = Mathf.Lerp(currentPosition.x, targetPosition.x, speed * Time.deltaTime);
        }
        else {
            newPositionX = targetPosition.x;
        }

        if ((Mathf.Abs(currentPosition.y - targetPosition.y) > matching)) {
            newPositionY = Mathf.Lerp(currentPosition.y, targetPosition.y, speed * Time.deltaTime);
        }
        else {
            newPositionY = targetPosition.y;
        }
        var newPosition = new Vector3(newPositionX, newPositionY);
        
        return newPosition;
    }
    #endregion

    #region MoveSmoothlyWithStableZCoordinate
    public static Vector3 MoveSmoothlyWithStableZCoordinate(Vector2 currentPosition, Vector2 targetPosition, float speed, float matching, float zStableCoordinate) {
        float newPositionX, newPositionY;
        float matchingConstant = matching * 3;

        if ((Mathf.Abs(currentPosition.x - targetPosition.x) > matching)) {
            newPositionX = Mathf.Lerp(currentPosition.x, targetPosition.x, speed * Time.deltaTime);
        }
        else {
            newPositionX = StatAnimationManager.ReachForTarget(currentPosition.x, matchingConstant * Time.deltaTime, targetPosition.x);
        }

        if ((Mathf.Abs(currentPosition.y - targetPosition.y) > matching)) {
            newPositionY = Mathf.Lerp(currentPosition.y, targetPosition.y, speed * Time.deltaTime);
        }
        else {
            newPositionY = StatAnimationManager.ReachForTarget(currentPosition.y, matchingConstant * Time.deltaTime, targetPosition.y);
        }
        var newPosition = new Vector3(newPositionX, newPositionY, zStableCoordinate);

        return newPosition;
    }

    public static Vector3 MoveSmoothlyWithStableZCoordinate(Vector2 currentPosition, Vector2 targetPosition, float speed, float matching, float zStableCoordinate, float matchingSeconds) {
        float newPositionX, newPositionY;
        float matchingConstant = matching * (1 / matchingSeconds);

        if ((Mathf.Abs(currentPosition.x - targetPosition.x) > matching)) {
            newPositionX = Mathf.Lerp(currentPosition.x, targetPosition.x, speed * Time.deltaTime);
        }
        else {
            newPositionX = StatAnimationManager.ReachForTarget(currentPosition.x, matchingConstant * Time.deltaTime, targetPosition.x);
        }

        if ((Mathf.Abs(currentPosition.y - targetPosition.y) > matching)) {
            newPositionY = Mathf.Lerp(currentPosition.y, targetPosition.y, speed * Time.deltaTime);
        }
        else {
            newPositionY = StatAnimationManager.ReachForTarget(currentPosition.y, matchingConstant * Time.deltaTime, targetPosition.y);
        }
        var newPosition = new Vector3(newPositionX, newPositionY, zStableCoordinate);

        return newPosition;
    }

    public static Vector3 MoveSmoothlyWithStableZCoordinate(Vector2 currentPosition, Vector2 targetPosition, float speed, float matching, float zStableCoordinate, float matchingSeconds, float maxSpeed) {
        float newPositionX, newPositionY;
        float matchingConstant = matching * (1 / matchingSeconds);

        if ((Mathf.Abs(currentPosition.x - targetPosition.x) > matching)) {
            var smoothSpeed = Mathf.Abs(currentPosition.x - Mathf.Lerp(currentPosition.x, targetPosition.x, speed * Time.deltaTime));
            var newSpeed = Mathf.Min(smoothSpeed, maxSpeed * Time.deltaTime);
            newPositionX = StatAnimationManager.ReachForTarget(currentPosition.x, newSpeed, targetPosition.x);
        }
        else {
            newPositionX = StatAnimationManager.ReachForTarget(currentPosition.x, matchingConstant * Time.deltaTime, targetPosition.x);
        }

        if ((Mathf.Abs(currentPosition.y - targetPosition.y) > matching)) {
            var smoothSpeed = Mathf.Abs(currentPosition.y - Mathf.Lerp(currentPosition.y, targetPosition.y, speed * Time.deltaTime));
            var newSpeed = Mathf.Min(smoothSpeed, maxSpeed * Time.deltaTime);
            newPositionY = StatAnimationManager.ReachForTarget(currentPosition.y, newSpeed, targetPosition.y);
        }
        else {
            newPositionY = StatAnimationManager.ReachForTarget(currentPosition.y, matchingConstant * Time.deltaTime, targetPosition.y);
        }
        var newPosition = new Vector3(newPositionX, newPositionY, zStableCoordinate);

        return newPosition;
    }

    public static Vector3 SpecialMoveSmoothlyWithStableZCoordinate(Vector2 currentPosition, Vector2 targetPosition, float speed, float matching, float zStableCoordinate, float matchingSeconds, float maxSpeed) {
        float newPositionX, newPositionY;
        float matchingConstant = matching * (1 / matchingSeconds);

        if ((Mathf.Abs(currentPosition.x - targetPosition.x) > matching)) {
            var smoothSpeed = Mathf.Abs(currentPosition.x - Mathf.Lerp(currentPosition.x, targetPosition.x, speed * Time.deltaTime));
            var newSpeed = Mathf.Min(smoothSpeed, maxSpeed * Time.deltaTime);
            newPositionX = StatAnimationManager.ReachForTarget(currentPosition.x, newSpeed, targetPosition.x);
        }
        else {
            newPositionX = StatAnimationManager.ReachForTarget(currentPosition.x, matchingConstant * Time.deltaTime, targetPosition.x);
        }

        if ((Mathf.Abs(currentPosition.y - targetPosition.y) > matching)) {
            var smoothSpeed = Mathf.Abs(currentPosition.y - Mathf.Lerp(currentPosition.y, targetPosition.y, speed * Time.deltaTime));
            var newSpeed = Mathf.Min(smoothSpeed, maxSpeed * Time.deltaTime);
            newPositionY = StatAnimationManager.ReachForTarget(currentPosition.y, newSpeed, targetPosition.y);
        }
        else {
            newPositionY = StatAnimationManager.ReachForTarget(currentPosition.y, matchingConstant * Time.deltaTime, targetPosition.y);
        }
        var newPosition = new Vector3(newPositionX, newPositionY, zStableCoordinate);

        return newPosition;
    }

    #endregion

    #region MoveUsually
    public static Vector3 MoveUsually(Vector2 currentPosition, Vector2 targetPosition, float depth, float speed) {
        float newPositionX, newPositionY;
        newPositionX = StatAnimationManager.ReachForTarget(currentPosition.x, speed * Time.deltaTime, targetPosition.x);
        newPositionY = StatAnimationManager.ReachForTarget(currentPosition.y, speed * Time.deltaTime, targetPosition.y);
        return new Vector3(newPositionX, newPositionY, depth);
    }
    #endregion

    #region MoveUsuallyInstance
    public static bool MoveUsuallyInstance(GameObject obj, Vector2 targetPosition, float speed) {
        float newPositionX, newPositionY;
        float currentPositionX = obj.transform.position.x;
        float currentPositionY = obj.transform.position.y;
        float currentPositionZ = obj.transform.position.z;

        if ((currentPositionX == targetPosition.x) && (currentPositionY == targetPosition.y)) {
            return false;
        }

        newPositionX = StatAnimationManager.ReachForTarget(currentPositionX, speed * Time.deltaTime, targetPosition.x);
        newPositionY = StatAnimationManager.ReachForTarget(currentPositionY, speed * Time.deltaTime, targetPosition.y);
        obj.transform.position = new Vector3(newPositionX, newPositionY, currentPositionZ);
        return true; 
    }
    #endregion

    #region MoveUsuallyInstance3D

    public static bool MoveUsuallyInstance3D(GameObject obj, Vector3 targetPosition, float speed) {
        float newPositionX, newPositionY, newPositionZ;
        float currentPositionX = obj.transform.position.x;
        float currentPositionY = obj.transform.position.y;
        float currentPositionZ = obj.transform.position.z;

        if ((currentPositionX == targetPosition.x) && (currentPositionY == targetPosition.y) && (currentPositionZ == targetPosition.z)) {
            return false;
        }

        newPositionX = StatAnimationManager.ReachForTarget(currentPositionX, speed * Time.deltaTime, targetPosition.x);
        newPositionY = StatAnimationManager.ReachForTarget(currentPositionY, speed * Time.deltaTime, targetPosition.y);
        newPositionZ = StatAnimationManager.ReachForTarget(currentPositionZ, speed * Time.deltaTime, targetPosition.z);
        obj.transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
        return true;
    }
    #endregion

    #region SyncPosition2D
    public static void SyncPositions2D (GameObject syncable, GameObject syncParent) {
        float targetX = syncParent.transform.position.x;
        float targetY = syncParent.transform.position.y;
        float z = syncable.transform.position.z;
        syncable.transform.position = new Vector3(targetX, targetY, z);
    }
    #endregion

    #region SyncPosition2DOffset
    public static void SyncPositions2DOffset(GameObject syncable, GameObject syncParent, float offsetX, float offsetY) {
        float targetX = syncParent.transform.position.x + offsetX;
        float targetY = syncParent.transform.position.y + offsetY;
        float z = syncable.transform.position.z;
        syncable.transform.position = new Vector3(targetX, targetY, z);
    }
    #endregion

    #region SetX
    public static void SetX(GameObject obj, float x) {
        obj.transform.position = new Vector3(x, obj.transform.position.y, obj.transform.position.z);
    }
    #endregion

    #region SetY
    public static void SetY(GameObject obj, float y) {
        obj.transform.position = new Vector3(obj.transform.position.x, y, obj.transform.position.z);
    }
    #endregion

    #region IncreaseX
    public static void IncreaseX(GameObject obj, float x) {
        obj.transform.position = new Vector3(obj.transform.position.x + x, obj.transform.position.y, obj.transform.position.z);
    }
    #endregion

    #region IncreaseY
    public static void IncreaseY(GameObject obj, float y) {
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + y, obj.transform.position.z);
    }
    #endregion
}
