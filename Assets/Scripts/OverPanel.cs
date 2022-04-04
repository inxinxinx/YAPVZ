using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverPanel : MonoBehaviour
{
    private Image image;
    private Image panel;

    void Start()
    {
        image = transform.Find("Image").GetComponent<Image>();
        panel = transform.Find("Panel").GetComponent<Image>();
        image.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        panel.color = new Color(0, 0, 0, 0);
    }
    public void Over()
    {
        // ��ʾͼƬ
        image.gameObject.SetActive(true);
        // ��Panle����ɺ�ɫ
        StartCoroutine(PanelColorEF());
    }

    IEnumerator PanelColorEF()
    {
        panel.gameObject.SetActive(true);
        float a = 0;
        while (a < 1)
        {
            a += 0.02f;
            panel.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.05f);
        }
        // �����λ����ζ���Ѿ�����
        yield return new WaitForSeconds(2f);
        // �ص�������
        //DoBackMainScene();
    }
    /*
    private void DoBackMainScene()
    {
        SceneManager.LoadScene("Start");
    }
    */
}
