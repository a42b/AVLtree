using NUnit.Framework;
using AVLTree;
using System;
using System.Collections.Generic;

namespace AVLtree.Tests
{
    [TestFixture]
    public class AVLTreeTests
    {
        private AVLTree<int> _avlTree;
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _avlTree = new AVLTree<int>();
            _random = new Random();
        }

        [Test]
        public void Insert_SingleElement_ShouldContainElement()
        {
            _avlTree.Insert(10);
            Assert.IsTrue(_avlTree.Contains(10));
        }

        [Test]
        public void Insert_MultipleElements_ShouldContainElements()
        {
            _avlTree.Insert(10);
            _avlTree.Insert(20);
            _avlTree.Insert(30);

            Assert.IsTrue(_avlTree.Contains(10));
            Assert.IsTrue(_avlTree.Contains(20));
            Assert.IsTrue(_avlTree.Contains(30));
        }

        [Test]
        public void Delete_ExistingElement_ShouldNotContainElement()
        {
            _avlTree.Insert(10);
            _avlTree.Insert(20);
            _avlTree.Delete(10);

            Assert.IsFalse(_avlTree.Contains(10));
            Assert.IsTrue(_avlTree.Contains(20));
        }

        [Test]
        public void Height_AfterMultipleInsertions_ShouldBeBalanced()
        {
            _avlTree.Insert(10);
            _avlTree.Insert(20);
            _avlTree.Insert(30);
            _avlTree.Insert(40);
            _avlTree.Insert(50);

            int height = _avlTree.GetHeight();
            Assert.AreEqual(3, height);
        }

        [Test]
        public void Insert_LargeNumberOfRandomElements_ShouldBeBalanced()
        {
            HashSet<int> insertedValues = new HashSet<int>();
            while (insertedValues.Count < 1000)
            {
                int randomValue = _random.Next(1, 10000);
                if (!insertedValues.Contains(randomValue))
                {
                    _avlTree.Insert(randomValue);
                    insertedValues.Add(randomValue);
                }
            }

            Assert.IsTrue(_avlTree.IsBalanced());
            Assert.IsTrue(_avlTree.IsHeightCorrect());
        }

        [Test]
        public void InsertAndDelete_RandomElements_ShouldBeBalanced()
        {
            HashSet<int> insertedValues = new HashSet<int>();
            while (insertedValues.Count < 1000)
            {
                int randomValue = _random.Next(1, 10000);
                if (!insertedValues.Contains(randomValue))
                {
                    _avlTree.Insert(randomValue);
                    insertedValues.Add(randomValue);
                }
            }

            int deleteCount = 0;
            while (deleteCount < 500)
            {
                int randomValue = _random.Next(1, 10000);
                if (insertedValues.Contains(randomValue))
                {
                    _avlTree.Delete(randomValue);
                    insertedValues.Remove(randomValue);
                    deleteCount++;
                }
            }

            Assert.IsTrue(_avlTree.IsBalanced());
            Assert.IsTrue(_avlTree.IsHeightCorrect());
        }
    }
}
