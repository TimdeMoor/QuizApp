using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizLib.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace UnitTestProject
{
    [TestClass]
    public class AntwoordUnitTests
    {
        [TestMethod]
        public void GetAll()
        {
            //Arrange
            AntwoordStub stub = new AntwoordStub();
            AntwoordContainer ac = new AntwoordContainer(stub);

            //Act
            List<Antwoord> results = ac.GetAll();

            //Assert
            Assert.AreEqual(stub.AntwoordenLijst.Count, results.Count);

            for (int i = 0; i < results.Count; i++)
            {
                Assert.AreEqual(stub.AntwoordenLijst[i].Id, results[i].Id);
                Assert.AreEqual(stub.AntwoordenLijst[i].Text, results[i].Text);
                Assert.AreEqual(stub.AntwoordenLijst[i].IsActive, results[i].IsActive);
                Assert.AreEqual(stub.AntwoordenLijst[i].IsCorrect, results[i].IsCorrect);
                Assert.AreEqual(stub.AntwoordenLijst[i].VraagId, results[i].VraagId);
            }
        }

        [TestMethod]
        public void GetById()
        {
            //Arrange
            AntwoordStub stub = new AntwoordStub();
            AntwoordContainer ac = new AntwoordContainer(stub);
            int TestId = 3;

            //Act
            Antwoord result = ac.GetById(TestId);

            //Assert
            Assert.AreEqual(TestId, result.Id);
        }

        [TestMethod]
        public void GetByVraagId()
        {
            //Arrange
            AntwoordStub stub = new AntwoordStub();
            AntwoordContainer ac = new AntwoordContainer(stub);
            int testVraagId = 1;

            //Act
            List<Antwoord> results = ac.GetByVraagId(testVraagId);
            List<AntwoordDTO> actual = stub.AntwoordenLijst.Where(x => x.VraagId == testVraagId).ToList();

            //Assert
            Assert.AreEqual(actual.Count, results.Count);

            for (int i = 0; i < results.Count; i++)
            {
                Assert.AreEqual(stub.AntwoordenLijst[i].Id, results[i].Id);
                Assert.AreEqual(stub.AntwoordenLijst[i].Text, results[i].Text);
                Assert.AreEqual(stub.AntwoordenLijst[i].IsActive, results[i].IsActive);
                Assert.AreEqual(stub.AntwoordenLijst[i].IsCorrect, results[i].IsCorrect);
                Assert.AreEqual(stub.AntwoordenLijst[i].VraagId, results[i].VraagId);
            }
        }

        [TestMethod]
        public void Save()
        {
            //Arrange
            AntwoordStub stub = new AntwoordStub();
            AntwoordContainer ac = new AntwoordContainer(stub);

            Antwoord a = new Antwoord
            (
                new AntwoordDTO()
                {
                    Id = 100,
                    IsActive = true,
                    IsCorrect = false,
                    Text = "Dit is een testAntwoord",
                    VraagId = 2,
                }
            );

            //Act
            a.iAntwoordDAO = stub;
            a.Save();

            Antwoord saved = ac.GetById((int)a.Id);

            //Assert
            Assert.AreEqual(a.Id, saved.Id);
            Assert.AreEqual(a.IsCorrect, saved.IsCorrect);
            Assert.AreEqual(a.Text, saved.Text);
            Assert.AreEqual(a.IsActive, saved.IsActive);
            Assert.AreEqual(a.VraagId, saved.VraagId);
        }

        [TestMethod]
        public void Delete()
        {
            //Arrange
            AntwoordStub stub = new AntwoordStub();
            AntwoordContainer ac = new AntwoordContainer(stub);

            int toDelete = 1;

            //Act        
            ac.Delete(stub.GetById(toDelete));

            //Assert
            Assert.AreEqual(stub.AntwoordenLijst.Count, ac.GetAll().Count());
        }
    }
}