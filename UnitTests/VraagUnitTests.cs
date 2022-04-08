using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Logic;
using QuizLib.Interface;
using QuizLib.Data;

namespace UnitTestProject
{
    [TestClass]
    public class VraagUnitTests
    {
        [TestMethod]
        public void GetAll()
        {
            //Arrange
            VraagStub stub = new VraagStub();
            VraagContainer vc = new VraagContainer(stub);
            List<Vraag> results;

            //Act
            results = vc.GetAll();

            //Assert
            for (int i = 0; i < results.Count; i++)
            {
                Assert.AreEqual(stub.vragenLijst[i].Id, results[i].Id);
                Assert.AreEqual(stub.vragenLijst[i].Text, results[i].Text);
                Assert.AreEqual(stub.vragenLijst[i].SubCategorieId, results[i].SubCategorieId);
            }
        }

        [TestMethod]
        public void GetById()
        {
            //Arrange
            int TestId = 1;
            VraagStub stub = new VraagStub();
            VraagContainer vc = new VraagContainer(stub);
            VraagDTO expected = stub.vragenLijst.Where(x => x.Id == TestId).SingleOrDefault();

            //Act
            Vraag result = vc.GetById(TestId);

            //Assert
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.Text, result.Text);
            Assert.AreEqual(expected.SubCategorieId, result.SubCategorieId);
        }

        [TestMethod]
        public void GetByText()
        {
            //Arrange
            string text = "Wat is 1 + 1?";
            VraagStub stub = new VraagStub();
            VraagContainer vc = new VraagContainer(stub);
            VraagDTO expected = stub.vragenLijst.Where(x => x.Text == text).SingleOrDefault();

            //Act
            Vraag result = vc.GetByText(text);

            //Assert
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.Text, result.Text);
            Assert.AreEqual(expected.SubCategorieId, result.SubCategorieId);
        }

        [TestMethod]
        public void Save()
        {
            //Arrange
            VraagStub stub = new VraagStub();
            VraagDTO dto = new VraagDTO()
            {
                Id = 4,
                SubCategorieId = 1,
                Text = "Is dit een testvraag?",
            };

            Vraag v = new Vraag(dto);
            VraagContainer vc = new VraagContainer(stub);
            v.iVraagDAO = stub;

            //Act
            v.Save();
            Vraag saved = vc.GetById((int)v.Id);

            //Assert
            Assert.IsNotNull(saved);
            Assert.AreEqual(v.Id, saved.Id);
        }

        [TestMethod]
        public void Delete()
        {
            //Arrange
            VraagStub stub = new VraagStub();
            VraagContainer vc = new VraagContainer(stub);
            int VraagToDeleteId = 1;

            //Act        
            vc.Delete(VraagToDeleteId);

            //Assert
            Assert.AreEqual(stub.vragenLijst.Count, vc.GetAll().Count());
        }
    }
}
