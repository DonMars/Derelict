using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(ShopUpgrade))]
public class UpdateEditor : Editor
{
    ShopUpgrade _shopUpgrade;

    private void OnEnable()
    {
        _shopUpgrade = (ShopUpgrade)target;
    }

    public override void OnInspectorGUI()
    {

        _shopUpgrade.name = EditorGUILayout.TextField("Name", _shopUpgrade.name);
        _shopUpgrade.description = EditorGUILayout.TextField("Description", _shopUpgrade.description);
        _shopUpgrade.icon = (Sprite) EditorGUILayout.ObjectField("Icon", _shopUpgrade.icon, typeof(Sprite), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        _shopUpgrade.cost = EditorGUILayout.IntField("Cost", _shopUpgrade.cost);

        _shopUpgrade.type = (UpgradeEnum)EditorGUILayout.EnumPopup("Update Type", _shopUpgrade.type);

        Space(3);

        switch (_shopUpgrade.type)
        {
            case UpgradeEnum.AttackType:
                {
                    _shopUpgrade.normalDamageIncreaseMin = EditorGUILayout.IntField("Normal Damage Increase Min", _shopUpgrade.normalDamageIncreaseMin);
                    _shopUpgrade.normalDamageIncreaseMax = EditorGUILayout.IntField("Normal Damage Increase Max", _shopUpgrade.normalDamageIncreaseMax);
                    _shopUpgrade.randomMinIncrease = EditorGUILayout.IntField("Random Damage Increase Min", _shopUpgrade.randomMinIncrease);
                    _shopUpgrade.randomMaxIncrease = EditorGUILayout.IntField("Random Damage Increase Max", _shopUpgrade.randomMaxIncrease);
                    _shopUpgrade.fireRateIncrease = EditorGUILayout.IntField("Fire Rate Increase", _shopUpgrade.fireRateIncrease);
                    break;
                }

            case UpgradeEnum.DefenseType:
                {
                    break;
                }

            case UpgradeEnum.MovementType:
                {
                    break;
                }

            case UpgradeEnum.SpecialType:
                {
                    break;
                }
        }
    }

    private void Space(int space)
    {
        for (int i = 0; i < space; i++)
        {
            EditorGUILayout.Space();
        }
    }
}

#endif