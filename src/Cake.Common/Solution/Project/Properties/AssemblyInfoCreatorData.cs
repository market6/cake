﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;

namespace Cake.Common.Solution.Project.Properties
{
    internal sealed class AssemblyInfoCreatorData
    {
        private readonly Dictionary<string, string> _dictionary;
        private readonly Dictionary<string, string> _customAttributes;
        private readonly HashSet<string> _namespaces;
        private readonly HashSet<string> _internalVisibleTo;

        public IDictionary<string, string> Attributes => _dictionary;

        public IDictionary<string, string> CustomAttributes => _customAttributes;

        public ISet<string> Namespaces => _namespaces;

        public ISet<string> InternalVisibleTo => _internalVisibleTo;

        public AssemblyInfoCreatorData(AssemblyInfoSettings settings)
        {
            _dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _customAttributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _namespaces = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _internalVisibleTo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Add attributes.
            AddAttribute("AssemblyTitle", "System.Reflection", settings.Title);
            AddAttribute("AssemblyDescription", "System.Reflection", settings.Description);
            AddAttribute("AssemblyCompany", "System.Reflection", settings.Company);
            AddAttribute("AssemblyProduct", "System.Reflection", settings.Product);
            AddAttribute("AssemblyVersion", "System.Reflection", settings.Version);
            AddAttribute("AssemblyFileVersion", "System.Reflection", settings.FileVersion);
            AddAttribute("AssemblyInformationalVersion", "System.Reflection", settings.InformationalVersion);
            AddAttribute("AssemblyCopyright", "System.Reflection", settings.Copyright);
            AddAttribute("AssemblyTrademark", "System.Reflection", settings.Trademark);
            AddAttribute("AssemblyConfiguration", "System.Reflection", settings.Configuration);
            AddAttribute("Guid", "System.Runtime.InteropServices", settings.Guid);
            AddAttribute("ComVisible", "System.Runtime.InteropServices", settings.ComVisible);
            AddAttribute("CLSCompliant", "System", settings.CLSCompliant);

            // Add assemblies that internals are visible to.
            if (settings.InternalsVisibleTo != null)
            {
                foreach (var item in settings.InternalsVisibleTo.Where(item => item != null))
                {
                    _internalVisibleTo.Add(string.Concat("InternalsVisibleTo(\"", item.UnQuote(), "\")"));
                }
                if (_internalVisibleTo.Count > 0)
                {
                    _namespaces.Add("System.Runtime.CompilerServices");
                }
            }
            if (settings.CustomAttributes != null)
            {
                foreach (var item in settings.CustomAttributes.Where(item => item != null))
                {
                    AddCustomAttribute(item.Name, item.NameSpace, item.Value);
                }
            }
        }

        private void AddAttribute(string name, string @namespace, bool? value)
        {
            if (value != null)
            {
                AddAttributeCore(Attributes, name, @namespace, value.Value ? "true" : "false");
            }
        }

        private void AddAttribute(string name, string @namespace, string value)
        {
            if (value != null)
            {
                AddAttributeCore(Attributes, name, @namespace, string.Concat("\"", value, "\""));
            }
        }

        private void AddCustomAttribute(string name, string @namespace, string value)
        {
            if (value != null)
            {
                AddAttributeCore(CustomAttributes, name, @namespace, string.Concat("\"", value, "\""));
            }
        }

        private void AddAttributeCore(IDictionary<string, string> dict, string name, string @namespace, string value)
        {
            if (dict.ContainsKey(name))
            {
                dict[name] = value;
            }
            else
            {
                dict.Add(name, value);
            }
            Namespaces.Add(@namespace);
        }
    }
}