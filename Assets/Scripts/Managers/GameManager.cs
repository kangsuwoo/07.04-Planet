using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Speed Control")]
    public Text SpeedButtonText;

    [Header("Planet Text Description")]
    public GameObject PlanetPanel;
    public Text PlanetHeaderText;
    public Text PlanetBodyText;

    public void OnClickSpeedButton()
    {
        //  Time.timeScale ?
        //  게임의 배속을 조절하는 변수 (기본 값 = 1)
        switch (Time.timeScale)
        {
            case 0:
                {
                    Time.timeScale = 1;
                    SpeedButtonText.text = "X 1";
                }
                break;

            case 1:
                {
                    Time.timeScale = 2;
                    SpeedButtonText.text = "X 2";
                }
                break;

            case 2:
                {
                    Time.timeScale = 4;
                    SpeedButtonText.text = "X 4";
                }
                break;

            case 4:
                {
                    Time.timeScale = 0;
                    SpeedButtonText.text = "||";
                }
                break;
        }
    }

    private void ChangeTimeScale(int minScale, int maxScale)
    {
        int value = minScale;
        SpeedButtonText.text = $"X {value}";
    }

    public void ReadTextSample()
    {
        // 유니티 폴더 (에셋 폴더) 안에 있는 파일 읽는 방법
        // Resources ? 
        // Assets 폴더 안에 있는 모든 Resources 폴더에서 파일을 찾아 읽는다
        // 확장자는 적지 않는다!
        TextAsset textAsset = Resources.Load<TextAsset>("Sample");
        PlanetHeaderText.text = textAsset.text;
    }

    public void ReadPlanetDescription(GameObject planet)
    {
        // 선택한 행성이 비어있으면 설명문 패널을 비활성화 해준다 

        if (planet == null)
        {
            PlanetPanel.SetActive(false);
            PlanetHeaderText.text = null;
            PlanetBodyText.text = null;
            return;
        }
        TextAsset textAsset = Resources.Load<TextAsset>(planet.name);

        // MemoryStream?
        // TextAsset 은 유니티에서 만든것 이고, 로드할때 이미 읽혀서 나오므로
        // StreamReader 랑 호환되어지지 않는다. 그래서 MemoryStream을 사용하여
        // StreamReader 와 호환할 수 있도록 하용한다
        using (MemoryStream ms = new MemoryStream(textAsset.bytes))
        using (StreamReader reader = new StreamReader(ms, System.Text.Encoding.UTF8))
        {
            // StreamReader ?
            // 파일을 읽는다. txt 뿐만 아니라 다른 파일도 읽을 수 있다!
            while(!reader.EndOfStream)
            {
                string str = reader.ReadLine();
                if(str[0] == '#')
                {
                    if (str.Contains("Header")) // 문자에 "Header" 가 포함되어 있는지? 
                    {
                        // Substring ? 문자열을 잘라준다
                        string header = str.Substring(9);
                        PlanetHeaderText.text = header;
                    }
                    else if (str.Contains("Characteristic"))
                    {
                        string body = str.Substring(17);

                        while (!reader.EndOfStream)
                        {
                            body += reader.ReadLine().Replace("\\n", "\n");
                        }

                        PlanetBodyText.text = body;

                    }

                }
            }
        }


        PlanetPanel.SetActive(true);
    }
}
