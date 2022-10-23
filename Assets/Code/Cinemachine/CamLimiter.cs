using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamLimiter : CinemachineExtension
{
    public int xLeft;
    public int xRight;
    public int y;
    public int width;

    public bool forL1 = false;
    // Update is called once per frame

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            if (forL1) {
                pos.x = xLeft;
                pos.y = Mathf.Clamp(pos.y, y, y+200);
            } else {
                pos.y = y;
                pos.x = Mathf.Clamp(pos.x, xLeft + width / 2, xRight - width / 2);
            }
            state.RawPosition = pos;
        }
    }
}
