using System;
using System.Runtime.Versioning;
using Cake.Core;

namespace Cake.Testing
{
    /// <summary>
    /// An implementation of a fake <see cref="ICakeRuntime"/>
    /// </summary>
    public sealed class FakeRuntime : ICakeRuntime
    {
        /// <summary>
        /// Gets or sets the target .NET framework version that the current AppDomain is targeting.
        /// </summary>
        public FrameworkName TargetFramework { get; set; }

        /// <summary>
        /// Gets a value indicating whether we're running on CoreClr.
        /// </summary>
        /// <value>
        /// <c>true</c> if we're runnning on CoreClr; otherwise, <c>false</c>.
        /// </value>
        public bool IsCoreClr { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeRuntime"/> class.
        /// </summary>
        public FakeRuntime()
        {
#if NETCORE
            TargetFramework = new FrameworkName(".NETStandard,Version=v1.6");
            IsCoreClr = true;
#else
            TargetFramework = new FrameworkName(".NETFramework,Version=v4.5");
            IsCoreClr = false;
#endif
        }
    }
}