using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    public Material Material;   //  회전시킬 재질
    public float RotateSpeed;   //  회전 파워
    private float m_RotateValue = 0; // 현재 회전 값

    private void Update()
    {
        m_RotateValue = m_RotateValue + (RotateSpeed * Time.deltaTime);
        if (m_RotateValue >= 360f) m_RotateValue -= 360f;

        //  Shader 안에 있는 파라메터를 접근하는 방법
        Material.SetFloat("_Rotation", m_RotateValue);
    }
}
