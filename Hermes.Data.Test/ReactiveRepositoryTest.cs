using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Data.Repositories.Interfaces;
using Hermes.Data.Repositories.Reactive;
using Hermes.Data.Test.Helpers;
using NUnit.Framework;

namespace Hermes.Data.Test
{
    [TestFixture]
    public class ReactiveRepositoryTest
    {
        public IAsyncRepository<TestClass> CreateSut()
        {
            var innerRepository = new TestClassRepository();
            return new ReactiveRepository<TestClass>(innerRepository);
        }

        private IEnumerable<TestClass> ConvertToList(IObservable<TestClass> observable)
        {
            var list = new List<TestClass>();
            observable.Subscribe(list.Add);
            return list;
        }
            
        [Test]
        public void Items()
        {
            var repository = CreateSut();
            var result = ConvertToList(repository.Items);
            Assert.AreEqual(10, result.Count());
        }

        [Test]
        public async void SaveAsync()
        {
            var repository = CreateSut(); 
            await repository.InsertAsync(TestClass.CreateNew());
            
            var result = ConvertToList(repository.Items);
            Assert.AreEqual(11, result.Count());
        }

        [Test]
        public void SaveWithoutAwait()
        {
            var repository = CreateSut();

            repository.InsertAsync(TestClass.CreateNew());
            
            var result = ConvertToList(repository.Items);
            Assert.AreEqual(10, result.Count());
        }

        [Test]
        public async void DeleteAsync()
        {
            var repository = CreateSut();

            var itemToRemove = await repository.Items.FirstAsync();

            await repository.DeleteAsync(itemToRemove);

            var result = ConvertToList(repository.Items);
            Assert.AreEqual(9, result.Count());
        }

        [Test]
        public void DeleteWithoutAwait()
        {
            var repository = CreateSut();

            var itemToRemove = repository.Items.FirstAsync().Wait();

            repository.DeleteAsync(itemToRemove);
            
            var result = ConvertToList(repository.Items);
            Assert.AreEqual(10, result.Count());
        }
    }
}
