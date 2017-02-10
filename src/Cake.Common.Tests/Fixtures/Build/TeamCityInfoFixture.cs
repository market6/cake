﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cake.Common.Build.TeamCity.Data;
using Cake.Core;
using NSubstitute;

namespace Cake.Common.Tests.Fixtures.Build
{
    internal sealed class TeamCityInfoFixture
    {
        public ICakeEnvironment Environment { get; set; }

        public TeamCityInfoFixture()
        {
            Environment = Substitute.For<ICakeEnvironment>();

            Environment.GetEnvironmentVariable("TEAMCITY_BUILDCONF_NAME").Returns(@"Cake Build");
            Environment.GetEnvironmentVariable("BUILD_NUMBER").Returns("10");

            Environment.GetEnvironmentVariable("TEAMCITY_PROJECT_NAME").Returns("Cake");

            Environment.GetEnvironmentVariable("Git_Branch").Returns("refs/pull-requests/7/from");
        }

        public TeamCityPullRequestInfo CreatePullRequestInfo()
        {
            return new TeamCityPullRequestInfo(Environment);
        }

        public TeamCityEnvironmentInfo CreateEnvironmentInfo()
        {
            return new TeamCityEnvironmentInfo(Environment);
        }

        public TeamCityBuildInfo CreateBuildInfo()
        {
            return new TeamCityBuildInfo(Environment);
        }

        public TeamCityProjectInfo CreateProjectInfo()
        {
            return new TeamCityProjectInfo(Environment);
        }
    }
}