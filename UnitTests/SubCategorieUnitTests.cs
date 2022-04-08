using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizLib.Interface;
using QuizLib.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestsProject;

namespace UnitTestProject
{
    [TestClass]
    public class SubCategorieUnitTests
    {
        [TestMethod]
        public void Delete()
        {
            //Arrange
            SubCategorieStub stub = new SubCategorieStub();
            SubCategorieContainer scc = new SubCategorieContainer(stub);
            int toDeleteId = 1;

            //Act
            scc.Delete(toDeleteId);

            //Assert
            Assert.AreEqual(stub.CategorieLijst.Count, scc.GetAll().Count);
        }

        [TestMethod]
        public void Save()
        {
            //Arrange
            SubCategorieStub stub = new SubCategorieStub();
            SubCategorieContainer scc = new SubCategorieContainer(stub);

            SubCategorieDTO toSave = new SubCategorieDTO()
            {
                IsActive = true,
                Beschrijving = "Deze testCategorie moet worden opgeslagen",
                ParentSubCategorieId = null,
                Id = 42,
                Naam = "TestCategorie",
            };
            SubCategorie toSaveEntity = new SubCategorie(toSave);
            toSaveEntity.iSubCategorieDAO = stub;

            //Act
            toSaveEntity.Save();
            SubCategorie saved = scc.GetById((int)toSave.Id);

            //Assert
            Assert.AreEqual(toSave.Id, saved.Id);
            Assert.AreEqual(toSave.Beschrijving, saved.Beschrijving);
            Assert.AreEqual(toSave.ParentSubCategorieId, saved.ParentSubCategorieId);
            Assert.AreEqual(toSave.Naam, saved.Naam);
        }

        [TestMethod]
        public void GetAll()
        {
            //Arrange
            SubCategorieStub stub = new SubCategorieStub();
            SubCategorieContainer scc = new SubCategorieContainer(stub);

            //Act
            List<SubCategorie> results = scc.GetAll();

            //Assert
            for (int i = 0; i < results.Count; i++)
            {
                Assert.AreEqual(stub.CategorieLijst[i].Naam, results[i].Naam);
                Assert.AreEqual(stub.CategorieLijst[i].Id, results[i].Id);
                Assert.AreEqual(stub.CategorieLijst[i].ParentSubCategorieId, results[i].ParentSubCategorieId);
                Assert.AreEqual(stub.CategorieLijst[i].Beschrijving, results[i].Beschrijving);
            }
        }

        [TestMethod]
        public void GetById()
        {
            //Arrange
            SubCategorieStub stub = new SubCategorieStub();
            SubCategorieContainer scc = new SubCategorieContainer(stub);
            int TestId = 1;

            SubCategorieDTO expected = stub.CategorieLijst.Where(x => x.Id == TestId).SingleOrDefault();

            //Act
            SubCategorie result = scc.GetById(TestId);

            //Assert
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.Naam, result.Naam);
            Assert.AreEqual(expected.ParentSubCategorieId, result.ParentSubCategorieId);
            Assert.AreEqual(expected.Beschrijving, result.Beschrijving);
        }

        [TestMethod]
        public void GetByName()
        {
            //Arrange
            SubCategorieStub stub = new SubCategorieStub();
            SubCategorieContainer scc = new SubCategorieContainer(stub);
            string TestName = "TestCategorie1";

            SubCategorie expected = new SubCategorie(stub.CategorieLijst.Where(x => x.Naam == TestName).SingleOrDefault());

            //Act
            SubCategorie result = scc.GetByName(TestName);

            //Assert
            Assert.AreEqual(expected.Naam, result.Naam);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.ParentSubCategorieId, result.ParentSubCategorieId);
            Assert.AreEqual(expected.Beschrijving, result.Beschrijving);
        }
    }
}
