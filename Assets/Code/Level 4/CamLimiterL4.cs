using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamLimiterL4 : CinemachineExtension
{
    public float xLeft;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.x = Mathf.Clamp(pos.x, xLeft, Mathf.Infinity);
            state.RawPosition = pos;
        }
    }
}
