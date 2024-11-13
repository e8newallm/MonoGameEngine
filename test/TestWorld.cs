namespace test;

public class Tests
{
    [TestFixture]
    public class TestWorld
    {
        private World? testWorld;
        Material TestMaterial = new("test");
        Material TestMaterialTwo = new("testTwo");
        public static void TestBasicTerrainGen(int width, int height, ref Material[,] data)
        {
        }

        [SetUp]
	    public void GetReady(){}

        [TearDown]
	    public void Clean(){}

        [Test]
        public void TestBasicGeneration()
        {
            testWorld = new(20, 15, TestBasicTerrainGen);
            Assert.That(testWorld.Width, Is.EqualTo(20));
            Assert.That(testWorld.Height, Is.EqualTo(15));
            Assert.That(() => testWorld[0, 0], Throws.Nothing);
            Assert.That(() => testWorld[0, 14], Throws.Nothing);
            Assert.That(() => testWorld[19, 0], Throws.Nothing);
            Assert.That(() => testWorld[19, 14], Throws.Nothing);
            Assert.That(() => testWorld[20, 14], Throws.Exception);
            Assert.That(() => testWorld[19, 15], Throws.Exception);
            Assert.That(() => testWorld[1352345, 12312], Throws.Exception);

            for(uint x = 0; x < testWorld.Width; x++)
                for(uint y = 0; y < testWorld.Height; y++)
                    Assert.That(testWorld[x, y], Is.EqualTo(Material.Nothing));
        }
    }
}