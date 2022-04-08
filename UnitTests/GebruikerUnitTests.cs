using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizLib.Interface;
using QuizLib.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class GebruikerUnitTests
    {
        [TestMethod]
        public void GetAll()
        {
            GebruikerStub stub = new GebruikerStub();
            GebruikerContainer gc = new GebruikerContainer(stub);

            List<Gebruiker> results = gc.GetAll();

            for (int i = 0; i < results.Count; i++)
            {
                Gebruiker g = new Gebruiker(stub.GebruikerLijst[i]);
                Assert.AreEqual(g.Naam, results[i].Naam);
                Assert.AreEqual(g.Wachtwoord, results[i].Wachtwoord);
                Assert.AreEqual(g.Email, results[i].Email);
                Assert.AreEqual(g.IsAdmin, results[i].IsAdmin);
                Assert.AreEqual(g.Id, results[i].Id);
            }
        }

        [TestMethod]
        public void GetById()
        {
            GebruikerStub stub = new GebruikerStub();
            GebruikerContainer gc = new GebruikerContainer(stub);
            int testId = 2;

            Gebruiker g = new Gebruiker(stub.GebruikerLijst.Where(x => x.Id == testId).SingleOrDefault());
            Gebruiker result = gc.GetById(testId);

            Assert.AreEqual(g.Naam, result.Naam);
            Assert.AreEqual(g.Wachtwoord, result.Wachtwoord);
            Assert.AreEqual(g.Email, result.Email);
            Assert.AreEqual(g.IsAdmin, result.IsAdmin);
            Assert.AreEqual(g.Id, result.Id);
        }

        [TestMethod]
        public void Save()
        {
            GebruikerStub stub = new GebruikerStub();
            GebruikerContainer gc = new GebruikerContainer(stub);
            Gebruiker toInsert = new Gebruiker(new GebruikerDTO()
            {
                Id = 389,
                Email = "Gebruiker389@Testmail.com",
                IsAdmin = false,
                Naam = "TestGebruiker389",
                Wachtwoord = "Wachtwoord389"
            });
            toInsert.iGebruikerDAO = stub;

            toInsert.Save();
            Gebruiker saved = gc.GetById(389);

            Assert.AreEqual(toInsert.Naam, saved.Naam);
            Assert.AreEqual(toInsert.Wachtwoord, saved.Wachtwoord);
            Assert.AreEqual(toInsert.Email, saved.Email);
            Assert.AreEqual(toInsert.IsAdmin, saved.IsAdmin);
            Assert.AreEqual(toInsert.Id, saved.Id);
        }

        [TestMethod]
        public void CheckValidLogin()
        {
            //Arrange
            GebruikerStub stub = new GebruikerStub();
            GebruikerContainer gc = new GebruikerContainer(stub);
            GebruikerDTO toLogin = new GebruikerDTO() 
            { 
                Naam = "TestGebruiker1" ,
                Wachtwoord = "TestWachtwoord1",
            };

            //Act
            Gebruiker result = gc.CheckLogin(toLogin);

            //Assert
            Assert.AreEqual(toLogin.Naam, result.Naam);
            Assert.AreEqual(toLogin.Wachtwoord, result.Wachtwoord);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void CheckInvalidLogin()
        {
            //Arrange
            GebruikerStub stub = new GebruikerStub();
            GebruikerContainer gc = new GebruikerContainer(stub);
            GebruikerDTO toLogin = new GebruikerDTO()
            {
                Naam = "TestGebruiker2",
                Wachtwoord = "TestWachtwoord1",
            };

            //Act
            Gebruiker result = gc.CheckLogin(toLogin);

            //Assert
            Assert.IsNull(result.Id);
        }
    }
}
