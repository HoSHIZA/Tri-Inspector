using TriInspector;
using TriInspector.Drawers;
using UnityEditor;
using UnityEngine;

[assembly: RegisterTriAttributeDrawer(typeof(LayerDrawer), TriDrawerOrder.Decorator)]

namespace TriInspector.Drawers
{
    public class LayerDrawer : TriAttributeDrawer<LayerAttribute>
    {
        private int _layer;
        
        public override TriExtensionInitializationResult Initialize(TriPropertyDefinition propertyDefinition)
        {
            var type = propertyDefinition.FieldType;
            if (type != typeof(LayerMask) && type != typeof(int))
            {
                return "Layer attribute can only be used on field of type int or LayerMask";
            }
            
            return TriExtensionInitializationResult.Ok;
        }
        
        public override void OnGUI(Rect position, TriProperty property, TriElement next)
        {
            if (property.ValueType == typeof(int))
            {
                var layer = EditorGUI.LayerField(position, property.DisplayNameContent, (int) property.Value);
                
                if (_layer == layer)
                {
                    return;
                }
                
                property.SetValue(layer);

                _layer = layer;
            }
            else if (property.ValueType == typeof(LayerMask))
            {
                var layer = EditorGUI.LayerField(position, property.DisplayNameContent, (LayerMask) property.Value);
                
                if (_layer == layer)
                {
                    return;
                }
                
                property.SetValue((LayerMask) layer);

                _layer = layer;
            }
        }
    }
}