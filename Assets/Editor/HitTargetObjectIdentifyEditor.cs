using ObjectIDs;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(HitTargetObjectIdentify))]
    public class HitTargetObjectIdentifyEditor : UnityEditor.Editor
    {
        private const string HitTargetHandlerGameObjectFieldName = "hitTargetHandlerGameObject";

        public override void OnInspectorGUI()
        {
            // 値をロードする
            serializedObject.Update();

            HitTargetObjectIdentify hitTargetObjectIdentify = target as HitTargetObjectIdentify;
            if (hitTargetObjectIdentify != null)
            {
                //hitTargetHandlerGameObject以外表示
                var iterator = serializedObject.GetIterator();
                while (iterator.NextVisible(true))
                {
                    if (iterator.propertyPath != HitTargetHandlerGameObjectFieldName)
                    {
                        EditorGUILayout.PropertyField(iterator);
                    }
                }

                if (hitTargetObjectIdentify.SetTypeforHitTargetType == SetTypeforHitTargetType.SerializeField)
                {
                    //hitTargetHandlerGameObjectを表示
                    var hitTargetHandlerGameObject = serializedObject.FindProperty(HitTargetHandlerGameObjectFieldName);
                    EditorGUILayout.PropertyField(hitTargetHandlerGameObject);

                    EditorGUILayout.HelpBox(
                        $"{HitTargetHandlerGameObjectFieldName}はエディタからIHitTargetHandlerを実装したMonoBefaviour継承クラスを注入する",
                        MessageType.Info,
                        true);
                }
                else if (hitTargetObjectIdentify.SetTypeforHitTargetType == SetTypeforHitTargetType.Script)
                {
                    EditorGUILayout.HelpBox($"{HitTargetHandlerGameObjectFieldName}をスクリプトで設定", MessageType.Info, true);
                }

                // 値を保存する
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}