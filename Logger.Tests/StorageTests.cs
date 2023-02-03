using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Logger.Tests;

[TestClass]
public class StorageTests
{
    public Storage Storage { get; set; } = new Storage();

    [TestInitialize]
    public void TestInitialize()
    {
        Storage = new Storage();
    }

    [TestMethod]
    public void Add_Book_Success()
    {
        Book item1 = CreateBook();
        Book item2 = item1 with { };
        Assert.AreEqual<Book>(item1, item2);
        Assert.IsFalse(ReferenceEquals(item1, item2));
        Guid expectedGuid = item1.Id;
        Storage.Add(item2);
        Assert.IsTrue(Storage.Contains(item2));
        Assert.AreEqual(item1, Storage.Get(expectedGuid));
    }

    private static Book CreateBook()
    {
        return new("Title", "Author", "42");
    }

    [TestMethod]
    public void Add_BookAndStudent_Success()
    {
        Student item1 = CreateStudent();
        Student item2 = item1 with { };
        Assert.AreEqual<Student>(item1, item2);
        Assert.IsFalse(ReferenceEquals(item1, item2));
        Guid expectedGuid = item1.Id;
        Storage.Add(item2);
        Assert.IsTrue(Storage.Contains(item2));
        Assert.AreEqual(item1, Storage.Get(expectedGuid));
    }

    private static Student CreateStudent()
    {
        return new Student("Inigo", "Montoya", "apple@Microsoft.com", "CS");
    }
}
