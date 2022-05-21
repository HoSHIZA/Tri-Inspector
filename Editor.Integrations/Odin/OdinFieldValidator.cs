﻿using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Validation;
using TriInspector.Editor.Integrations.Odin;

[assembly: RegisterValidator(typeof(OdinFieldValidator<>))]

namespace TriInspector.Editor.Integrations.Odin
{
    public class OdinFieldValidator<T> : AttributeValidator<DrawWithTriInspectorAttribute, T>, IDisposable
    {
        private TriPropertyTreeForOdin<T> _propertyTree;

        public override RevalidationCriteria RevalidationCriteria { get; }
            = RevalidationCriteria.OnValueChangeOrChildValueChange;

        public override bool CanValidateProperty(InspectorProperty property)
        {
            if (typeof(UnityEngine.Object).IsAssignableFrom(property.Info.TypeOfValue))
            {
                return false;
            }

            if (property.IsTreeRoot)
            {
                return false;
            }

            return true;
        }

        protected override void Initialize()
        {
            _propertyTree = new TriPropertyTreeForOdin<T>(ValueEntry);
            _propertyTree.Initialize(TriEditorMode.None);
        }

        public void Dispose()
        {
            _propertyTree.Dispose();
        }

        protected override void Validate(ValidationResult result)
        {
            _propertyTree.Update();
            _propertyTree.RunValidation();
            _propertyTree.CopyValidationResultsTo(result);
        }
    }
}