using Arvato.IQ.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arvato.IQ.Tests.Core
{
    public class ResourceLinkTests
    {

        [Fact]
        public void Should_Throw_ArgumentException_When_Rel_Empty()
        {
            var ex =  Assert.Throws<ArgumentException>(() => new ResourceLink(string.Empty,"/resource/1","GET"));
            Assert.Equal(ex.ParamName, "rel", ignoreCase: true);
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Rel_Empty()
        {
            var ex =  Assert.Throws<ArgumentNullException>(() => new ResourceLink(null,"/resource/1","GET"));
            Assert.Equal(ex.ParamName, "rel", ignoreCase: true);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_Href_Empty()
        {
            var ex =  Assert.Throws<ArgumentException>(() => new ResourceLink("create",string.Empty,"GET"));
            Assert.Equal(ex.ParamName, "href", ignoreCase: true);
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Href_Empty()
        {
            var ex =  Assert.Throws<ArgumentNullException>(() => new ResourceLink("create",null,"GET"));
            Assert.Equal(ex.ParamName, "href", ignoreCase: true);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_Method_Empty()
        {
            var ex =  Assert.Throws<ArgumentException>(() => new ResourceLink("create","/resource/1",string.Empty));
            Assert.Equal(ex.ParamName, "method", ignoreCase: true);
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Method_Empty()
        {
            var ex =  Assert.Throws<ArgumentNullException>(() => new ResourceLink("create","/resource/1",null));
            Assert.Equal(ex.ParamName, "method", ignoreCase: true);
        }

    }
}
