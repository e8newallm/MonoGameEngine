namespace test;

public class Tests
{
    [TestFixture]
    public class TestWorld
    {
        private World? testWorld;
        static Material TestMaterial = new("test");

        [SetUp]
	    public void GetReady(){}

        [TearDown]
	    public void Clean(){}

        [Test]
        public void TestBasicGeneration()
        {
            testWorld = new(20, 15);
            Assert.That(testWorld.Width, Is.EqualTo(20));
            Assert.That(testWorld.Height, Is.EqualTo(15));
            Assert.That(() => testWorld[0, 0], Throws.Nothing);
            Assert.That(() => testWorld[0, 14], Throws.Nothing);
            Assert.That(() => testWorld[19, 0], Throws.Nothing);
            Assert.That(() => testWorld[19, 14], Throws.Nothing);
            Assert.That(() => testWorld[20, 14], Throws.Exception);
            Assert.That(() => testWorld[19, 15], Throws.Exception);
            Assert.That(() => testWorld[1352345, 12312], Throws.Exception);

            for(int x = 0; x < testWorld.Width; x++)
                for(int y = 0; y < testWorld.Height; y++)
                    Assert.That(testWorld[x, y], Is.EqualTo(Material.Nothing));
        }

        [Test]
        public void TestMaterialUsage()
        {
            testWorld = new(10, 12);
            for(int x = 0; x < testWorld.Width; x++)
                for(int y = 0; y < testWorld.Height; y++)
                    if(x + y % 2 == 0) testWorld[x, y] = TestMaterial;

            Assert.That(testWorld.Width, Is.EqualTo(10));
            Assert.That(testWorld.Height, Is.EqualTo(12));
            Assert.That(() => testWorld[0, 0], Throws.Nothing);
            Assert.That(() => testWorld[0, 11], Throws.Nothing);
            Assert.That(() => testWorld[9, 0], Throws.Nothing);
            Assert.That(() => testWorld[9, 11], Throws.Nothing);
            Assert.That(() => testWorld[10, 11], Throws.Exception);
            Assert.That(() => testWorld[9, 12], Throws.Exception);
            Assert.That(() => testWorld[45, 234], Throws.Exception);

            for(int x = 0; x < testWorld.Width; x++)
                for(int y = 0; y < testWorld.Height; y++)
                    if(x + y % 2 == 0) Assert.That(testWorld[x, y], Is.EqualTo(TestMaterial));
                    else               Assert.That(testWorld[x, y], Is.EqualTo(Material.Nothing));
        }
    }
}