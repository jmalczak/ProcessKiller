namespace ProcessKiller.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessKiller.Logic;

    [TestClass]
    [DeploymentItem("ProcessList.txt")]
    public class FileReaderTest
    {
        public FileReader Execute()
        {
            return new FileReader("ProcessList.txt");
        }

        [TestMethod]
        public void FileLoadedWithoutException()
        {
            var fileReader = this.Execute();
            Assert.IsNotNull(fileReader);
        }

        [TestMethod]
        public void ReadNotIgnored()
        {
            var fileReader = this.Execute();
            var processesToKill = fileReader.ReadProcessesToKill();

            Assert.AreEqual(2, processesToKill.Count);
        }
    }
}