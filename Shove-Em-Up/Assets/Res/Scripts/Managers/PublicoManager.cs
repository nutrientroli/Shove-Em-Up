using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicoManager
{
    #region Singleton Pattern
    private static PublicoManager instance;
    private PublicoManager() { }
    public static PublicoManager GetInstance()
    {
        if (instance == null) instance = new PublicoManager();
        return instance;
    }
    #endregion

    private List<SpectatorScript> spectators = new List<SpectatorScript>();

    public void AddToList(SpectatorScript _spectator)
    {
        spectators.Add(_spectator);
    }

    public void ChangeAnimations()
    {
        foreach(SpectatorScript sp in spectators)
            sp.ChangeAnimation();
        
    }
}
