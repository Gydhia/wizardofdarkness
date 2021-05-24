using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EElements { Void = 0, Wind = 1, Earth = 2, None = -1 }
public abstract class Element : MonoBehaviour
{
    public EElements Type;

    public int Def;
    public int Str;
    public float AtkSpeed;
    public float MoveSpeed;
    public CharacterStatus ElementStats;
    public List<Skill> Skills = new List<Skill>();
    public GameObject ElementWeapon;

    public ParticleSystem SwitchingParticle;

    [SerializeField]protected Color BarFillColor;
    [SerializeField]protected Color CenterColor;
    [SerializeField]protected Color BarMinMaxColor;

    public void Init()
    {
        Def = ElementStats.baseDef;
        Str = ElementStats.baseStr;
        AtkSpeed = ElementStats.attackSpeed;
        MoveSpeed = ElementStats.moveSpeed;
    }

    public abstract void ChangementFX(ParticleSystem part);
    /*  Cette fonction, override dans les autres, va g�rer les FX qu'on d�clencheras lors d'un changement. La diff�rence entre cette m�thode et UpdateStats;
    C'est que du coup UpdateStats est commune pour toutes les classes. Dans cette fonction, on met alors les choses que les classes n'ont pas toutes en commun.
    Cette fonction sera appell�e quand on changeras de classe, si c'�tait pas �vident lmao

     */
}
