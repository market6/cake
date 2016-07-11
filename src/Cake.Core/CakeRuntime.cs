// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.Runtime.Versioning;

namespace Cake.Core
{
    /// <summary>
    /// Represents the runtime that Cake is running in.
    /// </summary>
    public sealed class CakeRuntime : ICakeRuntime
    {
        /// <summary>
        /// Gets the target .NET framework version that the current AppDomain is targeting.
        /// </summary>
        public FrameworkName TargetFramework { get; private set; }

        /// <summary>
        /// Gets a value indicating whether we're running on CoreClr.
        /// </summary>
        /// <value>
        /// <c>true</c> if we're runnning on CoreClr; otherwise, <c>false</c>.
        /// </value>
        public bool IsCoreClr
        {
            get
            {
#if NETCORE
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CakeRuntime"/> class.
        /// </summary>
        public CakeRuntime()
        {
#if NETCORE
            TargetFramework = new FrameworkName(".NETStandard,Version=v1.6");
#else
            TargetFramework = new FrameworkName(".NETFramework,Version=v4.5");
#endif
        }
    }
}
