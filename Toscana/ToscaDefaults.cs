﻿using System.Collections.Generic;

namespace Toscana
{
    /// <summary>
    /// TOSCA built-in node types
    /// </summary>
    public static class ToscaDefaults
    {
        /// <summary>
        /// Name of the tosca.nodes.Root node type
        /// </summary>
        public const string ToscaNodesRoot = "tosca.nodes.Root";

        /// <summary>
        /// Returns an instance of ToscaNodeType, 
        /// which represents Node Type all other TOSCA base Node Types derive from
        /// </summary>
        /// <returns>The TOSCA Node Type all other TOSCA base Node Types derive from</returns>
        public static ToscaNodeType GetRootNodeType()
        {
            var rootNode = new ToscaNodeType
            {
                Description = "The TOSCA Node Type all other TOSCA base Node Types derive from"
            };
            rootNode.Attributes.Add("tosca_id", new ToscaAttributeDefinition { Type = "string" });
            rootNode.Attributes.Add("tosca_name", new ToscaAttributeDefinition { Type = "string" });
            rootNode.Attributes.Add("state", new ToscaAttributeDefinition { Type = "string" });
            rootNode.Capabilities.Add("feature", new ToscaCapability { Type = "tosca.capabilities.Node" });
            rootNode.Requirements.Add(new Dictionary<string, ToscaRequirement>
            {
                {
                    "dependency",
                    new ToscaRequirement
                    {
                        Capability = "tosca.capabilities.Node",
                        Node = "tosca.nodes.Root",
                        Relationship = "tosca.relationships.DependsOn",
                        Occurrences = new object[] {"0", "UNBOUNDED"}
                    }
                }
            });

            return rootNode;
        }

        public const string ToscaCapabilitiesRoot = "tosca.capabilities.Root";

        public static ToscaCapabilityType GetRootCapabilityType()
        {
            return new ToscaCapabilityType();
        }
    }
}