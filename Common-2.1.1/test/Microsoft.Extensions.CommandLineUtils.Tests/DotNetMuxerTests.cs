// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#if NETCOREAPP2_0 || NETCOREAPP2_1
using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace Microsoft.Extensions.CommandLineUtils
{
    public class DotNetMuxerTests
    {
        [Fact]
        public void FindsTheMuxer()
        {
            var muxerPath = DotNetMuxer.MuxerPath;
            Assert.NotNull(muxerPath);
            Assert.True(File.Exists(muxerPath), "The file did not exist");
            Assert.True(Path.IsPathRooted(muxerPath), "The path should be rooted");
            Assert.Equal("dotnet", Path.GetFileNameWithoutExtension(muxerPath), ignoreCase: true);
        }
    }
}
#endif
