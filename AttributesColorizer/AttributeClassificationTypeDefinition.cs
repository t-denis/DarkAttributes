﻿using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace AttributesColorizer
{
    internal static class AttributeClassificationTypeDefinition
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.AttributeClassificationTypeName)]
        private static ClassificationTypeDefinition AttributeDefinition;
    }
}