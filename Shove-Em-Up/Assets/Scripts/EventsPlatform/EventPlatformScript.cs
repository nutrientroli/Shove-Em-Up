using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlatformScript : MonoBehaviour
{
    public enum TypeEvent { TIME, STEP };
    public TypeEvent type;
    public delegate float MethodEvent();
    public List<MethodEvent> listEvent = new List<MethodEvent>();

    private int step = 0;
    private float currentTime;
    private float actualMaxTime;
    public bool execute = false;

    public virtual void Init() {
        List<MethodEvent> listEvent = new List<MethodEvent>();
        execute = true;
    }

    public void Finnish() {
        execute = false;
    }

    public void NextStep() { 
        step++;
        if (step <= listEvent.Count) listEvent[step]();
        else Finnish();
    }

    public void Run(float _time) {
        currentTime += _time;
        if (currentTime >= actualMaxTime) {
            step++;
            if (step <= listEvent.Count) {
                actualMaxTime = listEvent[step-1]();
                currentTime = 0;
                if (actualMaxTime == -1) Finnish();
            }
            else Finnish();
        }
    }

    /*
     * float MethodEvent()
     * {
     *      Se devuelve valor para el siguente evento. Tiempo hasta siguiente evento.
     *      return 0; Para Eventos de tipo Call por Step.
     *      return -1; Para indicar que es el último evento.
     *      return value; Para indicar el tiempo que hay que esperar para ejecutar el siguiente evento.
     * }
    */
}
