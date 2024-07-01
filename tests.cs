using NUnit.Framework;
using AVLtree;

namespace AVLtree.Tests
{
    [TestFixture]
    public class AVLTreeTests
    {
        private AVLTree<int> _avlTree;

        [SetUp]
        public void Setup()
        {
            _avlTree = new AVLTree<int>();
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
            Assert.AreEqual(3, height); // Adjust the expected height based on your AVL tree logic
        }
    }
}
