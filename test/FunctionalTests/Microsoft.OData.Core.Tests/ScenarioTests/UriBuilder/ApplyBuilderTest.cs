﻿using Microsoft.OData.Tests.ScenarioTests.UriBuilder;
using Microsoft.OData.Tests.UriParser;
using Microsoft.OData.UriParser;
using System;
using Xunit;

namespace Microsoft.OData.Core.Tests.ScenarioTests.UriBuilder
{
    public class ApplyBuilderTest : UriBuilderTestBase
    {
        [Theory]
        [InlineData("http://gobbledygook/People?$apply=filter(Shoe eq 'blue')/groupby((FirstName,MyDog/LionWhoAteMe/AngerLevel),aggregate(LifeTime with average as avgLifeTime,FavoriteNumber with sum as sumFavoriteNumber,MyDog/LionWhoAteMe/LionHeartbeat with max as maxLionHeartbeat))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate(LifeTime with average as avgLifeTime))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate(LifeTime with countdistinct as cntdistinctLifeTime))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate(LifeTime with max as maxLifeTime))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate(LifeTime with min as minLifeTime))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate(LifeTime with sum as sumLifeTime))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate($count as cnt))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate(LifeTime with Custom.Aggregate as custLifeTime))")]
        [InlineData("http://gobbledygook/People?$apply=compute((cast(LifeTime,'Edm.Double') add MyDog/LionWhoAteMe/AngerLevel) mul 2 as lifeAngerLevel,StockQuantity div FavoriteNumber as StockNumber)")]
        [InlineData("http://gobbledygook/People?$apply=groupby((MyDog/Color,MyDog/Breed))")]
        [InlineData("http://gobbledygook/People?$apply=groupby((FirstName),aggregate(MyPaintings($count as cnt)))")]
        [InlineData("http://gobbledygook/People?$apply=expand(MyPaintings, filter(FrameColor eq 'Red'))/groupby((LifeTime),aggregate(MyPaintings($count as Count)))")]
        [InlineData("http://gobbledygook/People?$apply=expand(MyPaintings, filter(FrameColor eq 'Red'), expand(Owner, filter(FirstName eq 'Me')))/groupby((LifeTime),aggregate(MyPaintings($count as Count)))")]
        public void BuildUrlWithApply(string query)
        {
            var parser = new ODataUriParser(HardCodedTestModel.TestModel, ServiceRoot, new Uri(query));
            ODataUri odataUri = parser.ParseUri();

            Uri result = odataUri.BuildUri(ODataUrlKeyDelimiter.Parentheses);
            Assert.Equal(query, Uri.UnescapeDataString(result.OriginalString));
        }
    }
}