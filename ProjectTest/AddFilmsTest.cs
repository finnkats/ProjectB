using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Domain.UnitTest.AddFilm;

[TestClass]
public class AddfilmsTest
{
    [ClassInitialize]
    public static void Create_Film()
    {
        List<string> GenreList = new List<string>() { "Action", "Commedy" }
        Play Movie = new("Angery Birds Movie", GenreList, true)
    }

    [TestMethod]
    public static void AddingActTest()
    {
        AddFilm.AddToJson(Movie)
    }
}