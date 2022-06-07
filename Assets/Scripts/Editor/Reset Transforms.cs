#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ResetAllTransforms
{
    [MenuItem("GameObject/ResetAllTransforms %#q")]

    static public void ResetGameObjectTransforms()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            Transform trx = selectedObject.transform;
            Undo.RegisterCompleteObjectUndo(trx, "Reset game object transforms to default");

            trx.localPosition = Vector3.zero;
            trx.localRotation = Quaternion.identity;
            trx.localScale = Vector3.one;
        }
    }
}

public class ResetPosition
{
    [MenuItem("GameObject/ResetPosition %#w")]

    static public void ResetDameObjectPosition()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            Transform trx = selectedObject.transform;
            Undo.RegisterCompleteObjectUndo(trx, "Reset game object postion to zero");

            trx.localPosition = Vector3.zero;
        }
    }
}

public class ResetRotation
{
    [MenuItem("GameObject/ResetRotation %#e")]

    static public void ResetGameObjectRotation()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            Transform trx = selectedObject.transform;
            Undo.RegisterCompleteObjectUndo(trx, "Reset game object rotation to zero");

            trx.localRotation = Quaternion.identity;
        }
    }
}

public class ResetScale
{
    [MenuItem("GameObject/ResetScale %#r")]

    static public void ResetGameObjectScale()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            Transform trx = selectedObject.transform;
            Undo.RegisterCompleteObjectUndo(trx, "Reset game object scale to one");

            trx.localScale = Vector3.one;
        }
    }
}
#endif