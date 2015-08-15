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
        private static Controller SetupController(double input)
        {
            //Arrange
            var controller = new HomeController();
            //Act
            var inputNumber = new InputNumber { Number = input };
            var result = controller.Index(inputNumber).WithSuccess("","") as AlertDecoratorResult;
           return controller;
        }


        [TestMethod]
        public void Given123456InputSplitValues1EntryMustBe123()
        {
            Controller controller = SetupController(123456);
            var result = ((TechnologyOneProject.Controllers.HomeController) (controller));
           
            //Assert
            if (result != null)
            {
                Assert.AreEqual("123", result.ViewBag.InputSplitValues[1]);
                
            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void Given1234567InputSplitValues2ValueMustBe1()
        {
            Controller controller = SetupController(1234567);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("1", result.ViewBag.InputSplitValues[2]);
                
            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void GivenDot10CentsShouldBe1()
        {
            Controller controller = SetupController(.10);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("1", result.ViewBag.Cents);

            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void GivenDot1111CentsShouldBe11()
        {
            Controller controller = SetupController(.1111);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("11", result.ViewBag.Cents);

            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void Given1CentResultShouldBeOneCent()
        {
            Controller controller = SetupController(0.01);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("ONE CENT", result.ViewBag.Output);

            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void Given2CentResultShouldBeTwoCents()
        {
            Controller controller = SetupController(0.02);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("TWO CENTS", result.ViewBag.Output);

            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void Given22CentResultShouldBeTwentyDashTwoCents()
        {
            Controller controller = SetupController(0.22);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("TWENTY-TWO CENTS", result.ViewBag.Output);

            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void GivenDot1CentResultShouldBeTenCents()
        {
            Controller controller = SetupController(0.1);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("TEN CENTS", result.ViewBag.Output);

            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        [TestMethod]
        public void Given12InputSplitValues0ValueMustBe12()
        {
            Controller controller = SetupController(12);
            var result = ((TechnologyOneProject.Controllers.HomeController)(controller));

            //Assert
            if (result != null)
            {
                Assert.AreEqual("12", result.ViewBag.InputSplitValues[0]);

            }
            else
            {
                Assert.AreEqual(1, 2);
            }

        }

        
        [TestMethod]
        public void Given1CentReturnOneCent()
        {
            var result = SetupController(.1);
            var expected = "TEN CENTS";
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
            var expected = "FIVE DOLLARS";
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
            var expected = "ELEVEN DOLLARS";
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
