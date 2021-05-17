using System.Collections;
using UnityEngine;


public class Skill : MonoBehaviour
{
    /*Salut, browww
    Jvais faire du commentaire interactif
    Tu vois les livres o� tu peux faire des choix et on te dis "va a la page 4"? Bah l� mon fr�rot c'est pareil, mais avec mes commentaires.
    Donc, ici, t'es dans Skill.cs. C'est la classe parent de tout les spells, alli�s comme ennemis (� priori). Doooooonc, toutes les variables qui sont ici, sont aussi dans chaque skill, ok?
    Je pense que les variables sont assez explicites, on va passer sur �a. 
    Le plus important, c'est les enfants. J'ai tout bien tri�, il faut Aller dans:
    Scripts>SerializedObjects>CharacterStats>Player>Skills
    Et l�, t'as les Skills de earth, void et wind! Avec dedans des sous dossiers qui contiennent les sorts de claviers ou de souris.
    Vous devrez coder la fonction ActivatedSkill(). Attention! pas celle dans ce script, mais celle dans les autres skills.
    Pour lancer le coolDown? Trop simple! Il suffit de faire "canLaunch = false"; pile quand tu veux lancer le cooldown :)
    Attention, commencez pas � coder tant que vous ne trouvez pas mon commentaire qui dit "Allez, codez maintenant!", parce que sinon vous manquerez d'infos.
    (ne le cherchez pas, ce commentaire, il viendra a vous facilement)
    Donc � partir de l�, Killian, va dans WindArrow.cs, et toi enzo va dans SwordSwing.cs.
    ATTENTION: on ne peux pas mettre de coroutines sur les spells car on ne les instancie pas. Normalement, ce n'est pas un probl�me.
    Or, si avec votre spell vous instanciez quelque chose, (ex: ma boule magique avec le void), c'est tout a fait possible de mettre une coroutine l� dessus!

    */
    /*public enum ESkillType
    {
        Movement = 0,
        Defense = 1,
        Damage = 2
    }
    public enum ESkillRangeType
    {
        Melee = 0,
        Ranged = 1,
    }*/

    public bool CanLaunch = true;
    public float CooldownTimer;

    [UnityEngine.Header("Common:")]
    [UnityEngine.Tooltip("Skill cooldown.")] public float Cooldown;
    [UnityEngine.Header("To Implement:")]
    [UnityEngine.Tooltip("Skill damage based on THIS % of base strength.(not implemented)")]public int dmg = 5;
    [UnityEngine.Tooltip("The skill's areaOfEffect radius.")] public float AOERadius;
    [UnityEngine.Tooltip("Duration the skill has to be casted in order to be released.(not implemented)")] public float castTime;

    private void Awake()
    {
        CooldownTimer = Cooldown;
        CanLaunch = true;
    }
    public virtual void ActivatedSkill()
    {
        if(Cooldown != 0f)
            StartCoroutine(OnCooldown());
    }

    public IEnumerator OnCooldown()
    {
        CanLaunch = false;
        CooldownTimer = Cooldown;
        while(CooldownTimer >= 0f)
        {
            CooldownTimer -= Time.deltaTime;
            yield return null;
        }
        CanLaunch = true;
        

        yield return null;
    }
    
}
