using Rnx.Abstractions.Buffers;
using Rnx.Core.Buffers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rnx.Tasks.Reliak.FrontMatter.Tests
{
    public class FrontMatterJsonTaskTest
    {
        [Theory]
        [InlineData(@"
{
    ""simple"": ""some text"",
    ""complex"": { ""sub1"": ""value1"", ""sub2"": ""value2"" },
    ""list"": ['item1', 'item2'],
    ""nestedlist"": { ""sub1"": ['subitem1', 'subitem2'] }
}
*This* is the content")]
        public void Test_SimpleValue_ComplexValue_ListItems_And_NestedList(string text)
        {
            // Arrange
            var elementFac = new DefaultBufferElementFactory();
            var inputBuffer = new BlockingBuffer();
            inputBuffer.Add(elementFac.Create(text));
            inputBuffer.CompleteAdding();
            var outputBuffer = new BlockingBuffer();

            // Act
            var task = new FrontMatterJsonTask();
            task.Execute(inputBuffer, outputBuffer, null);
            outputBuffer.CompleteAdding();
            var elements = outputBuffer.Elements.ToArray();

            // Assert
            Assert.Equal(1, elements.Length);
            var e = elements.First();
            Assert.Equal("*This* is the content", e.Text);
            var data = e.Data;

            Assert.Equal(data["simple"], "some text");
            Assert.Equal(data["complex.sub1"], "value1");
            Assert.Equal(data["complex.sub2"], "value2");

            var list = data["list"] as IList;
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
            Assert.True(list.Cast<string>().Any(f => f == "item1"));
            Assert.True(list.Cast<string>().Any(f => f == "item2"));

            var nestedList = data["nestedlist.sub1"] as IList;
            Assert.NotNull(nestedList);
            Assert.Equal(2, nestedList.Count);
            Assert.True(nestedList.Cast<string>().Any(f => f == "subitem1"));
            Assert.True(nestedList.Cast<string>().Any(f => f == "subitem2"));
        }

        [Fact]
        public void Test_Empty_Json_Front_Matter()
        {
            // Arrange
            var text = @"{
}
*This* is the content";
            var elementFac = new DefaultBufferElementFactory();
            var inputBuffer = new BlockingBuffer();
            inputBuffer.Add(elementFac.Create(text));
            inputBuffer.CompleteAdding();
            var outputBuffer = new BlockingBuffer();

            // Act
            var task = new FrontMatterJsonTask();
            task.Execute(inputBuffer, outputBuffer, null);
            outputBuffer.CompleteAdding();
            var elements = outputBuffer.Elements.ToArray();

            // Assert
            Assert.Equal(1, elements.Length);
            var e = elements.First();
            Assert.Equal("*This* is the content", e.Text);
        }

        [Fact]
        public void Test_Missing_Front_Matter()
        {
            // Arrange
            var text = @"*This* is the content";
            var elementFac = new DefaultBufferElementFactory();
            var inputBuffer = new BlockingBuffer();
            inputBuffer.Add(elementFac.Create(text));
            inputBuffer.CompleteAdding();
            var outputBuffer = new BlockingBuffer();

            // Act
            var task = new FrontMatterJsonTask();
            task.Execute(inputBuffer, outputBuffer, null);
            outputBuffer.CompleteAdding();
            var elements = outputBuffer.Elements.ToArray();

            // Assert
            Assert.Equal(1, elements.Length);
            var e = elements.First();
            Assert.Equal("*This* is the content", e.Text);
        }
    }
}