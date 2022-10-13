using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AbilityCollectible", order = 2)]
public class AbilityCollectible : Item
{
    public Ability enableAbility;

    public override void Use() {
        base.Use();

        // enable ability
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        switch (enableAbility) {
            case Ability.Dash:
                player.EnableDashing = true;
                break;
            case Ability.WallClimb:
                player.EnableWallClimb = true;
                break;
            case Ability.DoubleJump:
                player.EnableDoubleJump = true;
                break;
        }
    }

    public enum Ability{Dash, WallClimb, DoubleJump};
}
