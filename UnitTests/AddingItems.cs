using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalFounders.Models;
using TechnicalFounders.Services;

namespace UnitTests
{
    [TestFixture()]
    public class AddingItemTests
    {
        // [SetUp]
        // public void SetUpData() { ... }

        [Test()]
        public async Task AddingItem_CheckingAddingItemToCollection_ShouldAddNewItem()
        {
            // Arrange

            MockDataStore mockDataStore = new MockDataStore();

            Item newItem = new Item 
            { Id = "Test", Text = "Seventh item", Description = "This is an item description." };


            // Act

            await mockDataStore.AddItemAsync(newItem);


            // Assert

            Assert.Contains(newItem, mockDataStore.Items);

        }

        // Test other AddingItem scenarios
    }
}
