using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCameraZ : CinemachineExtension
{
    public bool LockZ = false;
    public float m_ZPosition = 10;
    public bool LockX = false;
    public float m_XPosition = 10;
    public bool LockY = false;
    public float m_YPosition = 10;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            if (LockX)
            {
                pos.x = m_XPosition;
            }
            if (LockY)
            {
                pos.y = m_YPosition;
            }
            if (LockZ)
            {
                pos.z = m_ZPosition;
            }
            state.RawPosition = pos;
        }
    }
}