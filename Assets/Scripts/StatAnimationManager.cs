using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


static public class StatAnimationManager
{
    #region ZoomCamera
    static public bool ZoomCamera (Camera camera, float cameraSpeed, float cameraTarget) {
        float cameraHeight = camera.orthographicSize;
        if (cameraHeight == cameraTarget) { return false; }
        camera.orthographicSize = ReachForTarget(cameraHeight, cameraSpeed*Time.deltaTime, cameraTarget);
        return true;
    }
    #endregion

    #region ZoomCameraForWidthHeight

    static public float GetCameraZoomWithWidthHeight (Camera camera, bool horizontal, float widthHeight) {
        if (horizontal) {
            float hFOV = 2 * Mathf.Atan(camera.aspect * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad));
            float cameraDistance = widthHeight * 0.5f / Mathf.Tan(hFOV * 0.5f);
            return cameraDistance;
        }
        else {
            float cameraDistance = widthHeight * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            return cameraDistance;
        }
    }

    static public float GetCameraWidthHeightWithZoom (Camera camera, bool horizontal, float cameraDistance) {
        if (horizontal) {
            float hFOV = 2 * Mathf.Atan(camera.aspect * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad));
            float widthHeight = cameraDistance * Mathf.Tan(hFOV * 0.5f) * 2;
            return widthHeight;
        }
        else {
            float widthHeight = cameraDistance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * 2;
            return widthHeight;
        }
    }

    #endregion

    #region AnimateAlpha
    static public bool AnimateAlpha (GameObject obj, float alphaSpeed, float alphaTarget) {
        float alpha = 0;
        if (obj.GetComponent<SpriteRenderer>()) {
            Color color = obj.GetComponent<SpriteRenderer>().color;
            alpha = color.a;
            if (alpha == alphaTarget) { return false; }
            alpha = ReachForTarget(alpha, alphaSpeed * Time.deltaTime, alphaTarget);
            color.a = alpha;
            obj.GetComponent<SpriteRenderer>().color = color;
        }
        else if (obj.GetComponent<TextMeshPro>()) {
            Color color = obj.GetComponent<TextMeshPro>().color;
            alpha = color.a;
            if (alpha == alphaTarget) { return false; }
            alpha = ReachForTarget(alpha, alphaSpeed * Time.deltaTime, alphaTarget);
            color.a = alpha;
            obj.GetComponent<TextMeshPro>().color = color;
        }
        return true; 
    }
    #endregion

    #region AnimateAbstractColor
    static public Color AnimateAbstractColor (Color currentColor, float speed, Color targetColor) {
        float currentR = currentColor.r;
        float currentG = currentColor.g;
        float currentB = currentColor.b;

        
        float targetR = targetColor.r;
        float targetG = targetColor.g;
        float targetB = targetColor.b;

        float newSpeed = speed * Time.deltaTime;

        currentR = ReachForTarget(currentR, newSpeed, targetR);
        currentG = ReachForTarget(currentG, newSpeed, targetG);
        currentB = ReachForTarget(currentB, newSpeed, targetB);

        Color color = new Color(currentR, currentG, currentB);

        return color;
    }
    #endregion

    #region AnimateSpriteColor
    static public bool AnimateSpriteColor(GameObject obj, float speed, Color targetColor) {
        
        if (obj.GetComponent<SpriteRenderer>()) {
            Color currentColor = obj.GetComponent<SpriteRenderer>().color;
            if (currentColor == targetColor) { return false; }
            currentColor = AnimateAbstractColor(currentColor, speed, targetColor);
            obj.GetComponent<SpriteRenderer>().color = currentColor;
            return true;
        }
        
        return false;
    }
    #endregion

    #region AnimateTextColor
    static public bool AnimateTextColor(GameObject obj, float speed, Color targetColor) {

        if (obj.GetComponent<TextMeshPro>()) {
            Color currentColor = obj.GetComponent<TextMeshPro>().color;
            if (currentColor == targetColor) { return false; }
            currentColor = AnimateAbstractColor(currentColor, speed, targetColor);
            obj.GetComponent<TextMeshPro>().color = currentColor;
            return true;
        }

        return false;
    }
    #endregion

    #region AnimateScale
    static public bool AnimateScale (GameObject obj, float speed, float targetX, float targetY) {
        Vector3 currentScale = obj.transform.localScale;
        float currentX = currentScale.x;
        float currentY = currentScale.y;
        float currentZ = currentScale.z;
        if ((currentX == targetX) && (currentY == targetY)) { return false; }
        float newSpeed = speed*Time.deltaTime;
        currentX = ReachForTarget(currentX, newSpeed, targetX);
        currentY = ReachForTarget(currentY, newSpeed, targetY);
        obj.transform.localScale = new Vector3(currentX, currentY, currentZ);

        return true;
    }
    #endregion

    #region AnimateScale3D
    static public bool AnimateScale3D(GameObject obj, float speed, float targetX, float targetY, float targetZ) {
        Vector3 currentScale = obj.transform.localScale;
        float currentX = currentScale.x;
        float currentY = currentScale.y;
        float currentZ = currentScale.z;
        if ((currentX == targetX) && (currentY == targetY) && (currentZ == targetZ)) { return false; }
        float newSpeed = speed * Time.deltaTime;
        currentX = ReachForTarget(currentX, newSpeed, targetX);
        currentY = ReachForTarget(currentY, newSpeed, targetY);
        currentZ = ReachForTarget(currentZ, newSpeed, targetZ);
        obj.transform.localScale = new Vector3(currentX, currentY, currentZ);

        return true;
    }
    #endregion

    #region AnimateTextSize
    static public bool AnimateTextSize (GameObject obj, float speed, float targetSize) {
        float currentSize = obj.GetComponent<TextMeshPro>().fontSize;
        if (currentSize == targetSize) { return false; }
        currentSize = ReachForTarget(currentSize, speed*Time.deltaTime, targetSize);
        obj.GetComponent<TextMeshPro>().fontSize = currentSize;

        return true;
    }
    #endregion

    #region AnimateRotationZ
    static public bool AnimateRotationZ (GameObject obj, float degreesSpeed, float targetDegreesZ) {
        float currentDegreesZ = obj.transform.localEulerAngles.z;
        if (currentDegreesZ == targetDegreesZ) { return false; }
        float newDegreesZ = ReachForTarget(currentDegreesZ, degreesSpeed * Time.deltaTime, targetDegreesZ);
        obj.transform.Rotate(0, 0, newDegreesZ-currentDegreesZ);
        return true;
    }
    #endregion

    #region AnimateRotationZOneCircle
    static public bool AnimateRotationZOneCircle(GameObject obj, float degreesSpeed, float targetDegreesZ, bool clockwise) {
        float currentDegreesZ = Mathf.Round(obj.transform.eulerAngles.z * 100f)/100f;
        if (clockwise) {
            float newDegreesZ = ReachForTargetDown(currentDegreesZ, degreesSpeed * Time.deltaTime, targetDegreesZ);
            obj.transform.Rotate(0, 0, newDegreesZ - currentDegreesZ);
        }
        else {
            float newDegreesZ = ReachForTargetUp(currentDegreesZ, degreesSpeed * Time.deltaTime, targetDegreesZ);
            obj.transform.Rotate(0, 0, newDegreesZ - currentDegreesZ);
        }
        if (Mathf.Abs(currentDegreesZ - targetDegreesZ) < (degreesSpeed * Time.deltaTime)) { return false; }

        return true;
    }
    #endregion

    static public void AnimateRotationIncreaseWithSpeed3D (GameObject obj, float degreesSpeedX, float degreesSpeedY, float  degreesSpeedZ) {
        float newDegreesX = obj.transform.localEulerAngles.x + degreesSpeedX * Time.deltaTime;
        float newDegreesY = obj.transform.localEulerAngles.y + degreesSpeedY * Time.deltaTime;
        float newDegreesZ = obj.transform.localEulerAngles.z + degreesSpeedZ * Time.deltaTime;
        obj.transform.Rotate(newDegreesX, newDegreesY, newDegreesZ);
    }

    static public void AnimateRotationIncrease(GameObject obj, float degreesSpeedX, float degreesSpeedY, float degreesSpeedZ) {
        float newDegreesX = obj.transform.localEulerAngles.x + degreesSpeedX;
        float newDegreesY = obj.transform.localEulerAngles.y + degreesSpeedY;
        float newDegreesZ = obj.transform.localEulerAngles.z + degreesSpeedZ;
        obj.transform.Rotate(newDegreesX, newDegreesY, newDegreesZ);
    }

    #region AnimateRotation3D
    static public bool AnimateRotation3D(GameObject obj, float degreesSpeed, float targetDegreesX, float targetDegreesY, float targetDegreesZ) {
        float currentDegreesX = obj.transform.localEulerAngles.x;
        float currentDegreesY = obj.transform.localEulerAngles.y;
        float currentDegreesZ = obj.transform.localEulerAngles.z;
        if ((currentDegreesX == targetDegreesX) && (currentDegreesY == targetDegreesY) && (currentDegreesX == targetDegreesZ)) { return false; }
        float newDegreesX = ReachForTarget(currentDegreesX, degreesSpeed * Time.deltaTime, targetDegreesX);
        float newDegreesY = ReachForTarget(currentDegreesY, degreesSpeed * Time.deltaTime, targetDegreesY);
        float newDegreesZ = ReachForTarget(currentDegreesZ, degreesSpeed * Time.deltaTime, targetDegreesZ);
        obj.transform.Rotate(newDegreesX - currentDegreesX, 0, 0);
        obj.transform.Rotate(0, newDegreesY - currentDegreesY, 0);
        obj.transform.Rotate(0, 0, newDegreesZ - currentDegreesZ);
        return true;
    }
    #endregion

    #region PanCamera
    static public bool PanCamera(Camera camera, float sizeSpeed, float targetSize) {
        if (camera.orthographicSize == targetSize) {
            return false;
        }
        var currentSize = camera.orthographicSize;
        camera.orthographicSize = ReachForTarget(currentSize, sizeSpeed * Time.deltaTime, targetSize);
        return true;
    }
    #endregion

    #region ReachForTarget
    static public float ReachForTarget (float current, float speed, float target) {
        if (current > target) {
            current -= Mathf.Min(speed, Mathf.Abs(current-target));
        }
        else if (current < target) {
            current += Mathf.Min(speed, Mathf.Abs(target- current));
        }
        return current;
    }
    #endregion

    #region Reach For Target With Excess
    static public float ReachForTargetWithExcess(float current, float speed, float target) {
        if (current > target) {
            current -= speed;
        }
        else if (current < target) {
            current += speed;
        }
        return current;
    }
    #endregion

    #region ReachForTargetDown 
    static public float ReachForTargetDown (float current, float speed, float target) {
        if (current != target) {
            current -= Mathf.Min(speed, Mathf.Abs(current - target));
        }
        return current;
    }
    #endregion

    #region ReachForTargetUp 
    static public float ReachForTargetUp(float current, float speed, float target) {
        if (current != target) {
            current += Mathf.Min(speed, Mathf.Abs(target - current));
        }
        return current;
    }
    #endregion

    #region Wobble
    
    /*
    static public int Wobble(GameObject obj, ScrWobbleSettings wobbleSettings, int wobbleCurrent) {
        if (wobbleCurrent<wobbleSettings.GetMaxElement()+1) {
            float scaleToReachX, scaleToReachY, scaleToReachZ;
            if (!wobbleSettings.GetProportionalIncrease()) {
                scaleToReachX = wobbleSettings.GetXScaleIncrese(wobbleCurrent);
                scaleToReachY = wobbleSettings.GetYScaleIncrese(wobbleCurrent);
                scaleToReachZ = wobbleSettings.GetZScaleIncrese(wobbleCurrent);
            }
            else {
                scaleToReachX = wobbleSettings.GetStartingProportion() * wobbleSettings.GetXScaleIncrese(wobbleCurrent);
                scaleToReachY =  wobbleSettings.GetStartingProportion() * wobbleSettings.GetYScaleIncrese(wobbleCurrent);
                scaleToReachZ =  wobbleSettings.GetStartingProportion() * wobbleSettings.GetZScaleIncrese(wobbleCurrent);
            }
            float scaleSpeed = wobbleSettings.GetSpeed(wobbleCurrent);
            if (!StatAnimationManager.AnimateScale3D(obj, scaleSpeed, scaleToReachX, scaleToReachY, scaleToReachZ)) {
                return wobbleCurrent + 1;
            }
            else {
                return wobbleCurrent;
            }
        }
        return -1;
    }*/
    #endregion
    
}
