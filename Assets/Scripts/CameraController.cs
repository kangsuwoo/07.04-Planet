using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotateSpeed = 20.0f;       //  회전 속도
    private Vector3 m_StartMousePosition = Vector3.zero; //  회전을 위한 마우스 위치 저장 값

    public Transform PlanetTransform = null;    //  선택된 행성

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //  GetKey 는 마우스가 Press 상태 (꾹 누른 상태) 일 때 계속 ture 이지만
            //  GetKeyDown 은 사용자가 마우스를 눌렀을 때 단 한 번만 ture 를 반환하므로
            //  처음 클릭할 때 위치를 가져올 수 있다!
            if (Input.GetKeyDown(KeyCode.Mouse0))
                m_StartMousePosition = Input.mousePosition;
            else
            {
                //  normalized ? 벡터의 방향을 구한다 (0 ~ 1)
                Vector3 newAxis = (m_StartMousePosition - Input.mousePosition).normalized;

                //  마우스 위치는 축이 다르기 때문에 반드시 축을 바꿔 주어야 한다!
                transform.RotateAround(
                    PlanetTransform != null ? PlanetTransform.position : Vector3.zero,
                    new Vector3(newAxis.y, -newAxis.x),
                    RotateSpeed * Time.deltaTime);

                m_StartMousePosition = Input.mousePosition;
            }
        }
    }

}
