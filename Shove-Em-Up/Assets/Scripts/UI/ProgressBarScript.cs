using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    public int player;
    public Color color;
    public Image bar;
    [SerializeField] private float speed = 0.2f;
    private bool runAnimation = true;
    [SerializeField] private float maxvalue = 50;
    [SerializeField] private float maxvalueglobal = 100;
    private float lerpvalue = 0;

    void Start() {
        if(player <= PlayersManager.GetInstance().GetNumberOfPlayers()) {
            maxvalue = ScoreManager.GetInstance().GetPoints(player);
            maxvalueglobal = ScoreManager.GetInstance().GetMaxPoints();
            bar.color = color;
            SetProgress(0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update() {
        if (runAnimation) {
            lerpvalue += speed * Time.deltaTime;
            SetProgress(GetPercent(Mathf.Lerp(0, maxvalueglobal, lerpvalue)));
        }
    }

    private float GetPercent(float _value) {
        if (_value >= maxvalue) {
            _value = maxvalue;
            runAnimation = false;
        }
        return  _value / maxvalueglobal;
    }

    private void SetProgress(float _percent) {
        if (_percent >= 1)   _percent = 1;
        bar.fillAmount = _percent;
    }
}
