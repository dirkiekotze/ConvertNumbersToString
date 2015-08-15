using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechnologyOneProject;
using TechnologyOneProject.Controllers;
using TechnologyOneProject.Infastructure.Alerts;
using TechnologyOneProject.Models;

namespace TechnologyOneProject.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private static ViewResult SetupController(double input)
        {
            //Arrange
            var controller = new HomeController();
            //Act
            var inputNumber = new InputNumber { Number = input };
            //var result = controller.Index(inputNumber).WithSuccess() as ViewResult;
            var result = controller.Index(inputNumber) as ViewResult;

            //return View(inputNumber).WithSuccess(globalString.ToString(), string.Format("{0}{1}", Language.Currency, inputNumber.Number));

            return result;
        }

        
        [TestMethod]
        public void Given1CentReturnOneCent()
        {
            var result = SetupController(.1);
            var expected = "ONE CENT";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void Given0DollarsReturnEmptyString()
        {
            var result = SetupController(0);
            var expected = "";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Given5DollarsReturnFive()
        {
            var result = SetupController(5);
            var expected = "FIVE DOLLAR";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Given10DollarsReturnTen()
        {
            var result = SetupController(10);
            var expected = "TEN DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }


        [TestMethod]
        public void Given11DollarsReturnEleven()
        {
            var result = SetupController(11);
            var expected = "ELEVEN DOLLAR";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Given19DollarsReturnNineteen()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var inputNumber = new InputNumber { Number = 19 };
            var result = controller.Index(inputNumber) as ViewResult;
            var expected = "NINETEEN DOLLARS";

            //Assert
            if (result != null)
            {
                var actual = result.ViewBag.Output;
            }
        }

        [TestMethod]
        public void Given30DollarsReturnThirty()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var inputNumber = new InputNumber { Number = 30 };
            var result = controller.Index(inputNumber) as ViewResult;
            var expected = "THIRTEE";

            //Assert
            if (result != null)
            {
                var actual = result.ViewBag.Output;
            }
        }

        [TestMethod]
        public void Given31DollarsReturnThirteeOne()
        {
            var result = SetupController(31);
            var expected = "THIRTY-ONE DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }

        
        [TestMethod]
        public void Given66DollarsReturnSixtySix()
        {
            var result = SetupController(66);
            var expected = "SIXTY-SIX DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Given80DollarsReturnEightee()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var inputNumber = new InputNumber { Number = 80 };
            var result = controller.Index(inputNumber) as ViewResult;
            var expected = "EIGHTY DOLLARS";

            //Assert
            if (result != null)
            {
                var actual = result.ViewBag.Output;
            }
        }

        [TestMethod]
        public void Given99DollarsReturnNinetyNine()
        {
            var result = SetupController(99);
            var expected = "NINETY-NINE DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
            
        }

        [TestMethod]
        public void Given101DollarsReturnOneHundredEleven()
        {
            var result = SetupController(101);
            var expected = "ONE HUNDRED AND ONE DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void Given105DollarsReturnOneHundredAndFife()
        {
            var result = SetupController(105);
            var expected = "ONE HUNDRED AND FIVE DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void Given118DollarsReturnOneHundredAndEighteen()
        {
            var result = SetupController(118);
            var expected = "ONE HUNDRED AND EIGTHEEN DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void Given121DollarsReturnOneHundredAndTwentyOne()
        {
            var result = SetupController(118);
            var expected = "ONE HUNDRED AND EIGHTEEN DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void Given444DollarsReturnFourHundredAandFourtyFour()
        {
            var result = SetupController(444);
            var expected = "FOUR HUNDRED AND FOURTY-FOUR DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void Given500DollarsReturnFiveHundred()
        {
            var result = SetupController(500);
            var expected = "FIVE HUNDRED DOLLARS";
            var actual = string.Empty;

            //Assert
            if (result != null)
            {
                actual = result.ViewBag.Output;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }

        }

       


    }
}
