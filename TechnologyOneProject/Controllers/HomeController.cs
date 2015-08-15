using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using TechnologyOneProject.Infastructure.Alerts;
using TechnologyOneProject.Models;

namespace TechnologyOneProject.Controllers
{
    public class HomeController : Controller
    {
        private const int FirstLoop = 0;
        private const int SecondLoop = 1;
        private const int ThirdLoop = 2;
        private const int FourthLoop = 3;
        private const int FifthLoop = 4;
        private const int SixLoop = 5;
        private const int Quadrillions = 6;
        private const int Trillion = 5;
        private const int Billion = 4;
        private const int Million = 3;
        private const int Thousand = 2;
        private const int Dollar = 1;
        private const int Ten = 10;
        private const int Twenty = 20;
        private const int Hundred = 100;
        private const int ThousandVal = 1000;
        private const int CalculateCents = 2;
        private readonly List<string> _lstHundreds;
        private readonly List<string> _lstInputBreakUp = new List<string>();
        private readonly List<string> _lstTens;
        private readonly List<string> _lstTenTo19;
        private readonly List<string> _lstUnderTen;
        private int _globalWorkingWithIndicator;
        private int _workingWithIndicator;

        public HomeController()
        {
            #region Create Lists
            _lstUnderTen = new List<string>
            {
                Language.Zero,
                Language.One,
                Language.Two,
                Language.Three,
                Language.Four,
                Language.Five,
                Language.Six,
                Language.Seven,
                Language.Eight,
                Language.Nine
            };

            _lstTenTo19 = new List<string>
            {
                Language.Ten,
                Language.Eleven,
                Language.Twelve,
                Language.Thirteen,
                Language.Fourteen,
                Language.Fifteen,
                Language.Sixteen,
                Language.Seventeen,
                Language.Eighteen,
                Language.Nineteen
            };

            _lstTens = new List<string>
            {
                Language.Ten,
                Language.Twenty,
                Language.Thirty,
                Language.Fourty,
                Language.Fifty,
                Language.Sixty,
                Language.Seventy,
                Language.Eigthy,
                Language.Ninety
            };

            _lstHundreds = new List<string>
            {
                string.Format("{0} {1}", Language.One, Language.Hundred),
                string.Format("{0} {1}", Language.Two, Language.Hundred),
                string.Format("{0} {1}", Language.Three, Language.Hundred),
                string.Format("{0} {1}", Language.Four, Language.Hundred),
                string.Format("{0} {1}", Language.Five, Language.Hundred),
                string.Format("{0} {1}", Language.Six, Language.Hundred),
                string.Format("{0} {1}", Language.Seven, Language.Hundred),
                string.Format("{0} {1}", Language.Eight, Language.Hundred),
                string.Format("{0} {1}", Language.Nine, Language.Hundred)
            };
            #endregion
        }

        public ActionResult Index()
        {
            return View(new InputNumber());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Number")] InputNumber inputNumber)
        {
            #region Variables

            var globalString = new StringBuilder();
            var stringDollars = new StringBuilder();
            var stringThousands = new StringBuilder();
            var stringMillions = new StringBuilder();
            var stringBillions = new StringBuilder();
            var stringTrillions = new StringBuilder();
            var stringQuadrillions = new StringBuilder();

            #endregion

            if (ModelState.IsValid)
            {
                try
                {
                    var splitDollarsAndCents = inputNumber.Number.ToString().Split('.');
                    var inputString = splitDollarsAndCents[0];

                    SplitInputvalueIntoArrayOfThrees(inputString);

                    ExtractStringValuesFromInput(inputString, stringDollars, stringThousands, stringMillions, stringBillions, stringTrillions, stringQuadrillions);

                    DisplayStringValues(globalString, stringQuadrillions, stringTrillions, stringBillions, stringMillions, stringThousands, stringDollars);

                    ViewBag.Output += globalString.ToString();

                    if (globalString.Length != 0)
                    {
                        return View(inputNumber).WithSuccess(globalString.ToString());
                    }

                    else
                    {
                        return View(inputNumber).WithWarning(globalString.ToString());
                    }
                }
                catch (Exception err)
                {
                    return View(inputNumber).WithError(Language.ErrorMessage + err.Message);
                }


                //return View(retVal.ToString());
                //return View(inputNumber);
            }

            return View(inputNumber);
        }

        private static string AddSpace(StringBuilder inputString)
        {
            return string.Format("{0} {1}", inputString, string.Empty);
        }

        private static string AddAndToValuesOver999(string inputString)
        {
            //if 35 000 000 : If first is 0 and it has value then and
            if (inputString.StartsWith("0") && Convert.ToInt32(inputString) > 0)
            {
                return string.Format("{0} {1} {2}", string.Empty, Language.And, string.Empty);
            }
            return string.Empty;
        }

        private StringBuilder CreateStringValues(string inputValue)
        {
            var strValue = new StringBuilder();
            var dollarStringLength = inputValue.Length;

            if (Convert.ToInt32(inputValue) < Ten)
            {
                Calculate0To9(inputValue, strValue, _lstUnderTen);
            }
            else if (Convert.ToInt32(inputValue) < Twenty)
            {
                strValue = Calculate10To19(inputValue, strValue, _lstTenTo19);
            }
            else if (Convert.ToInt32(inputValue) < Hundred)
            {
                strValue = Calculate20To99(inputValue);
            }
            else if (Convert.ToInt32(inputValue) < ThousandVal)
            {
                strValue = Calculate100To999(inputValue);
            }

            return strValue;
        }

        private static StringBuilder Calculate0To9(string numberValue, StringBuilder strValue,
            IReadOnlyList<string> textValue)
        {
            strValue.Append(textValue[Convert.ToInt32(numberValue)]);
            return strValue;
        }

        private static StringBuilder Calculate10To19(string numberValue, StringBuilder strValue, List<string> textValue)
        {
            strValue.Append(textValue[Convert.ToInt32(numberValue) - 10]);
            return strValue;
        }

        private StringBuilder Calculate20To99(string numberValue)
        {
            var strValue = new StringBuilder();

            //099
            if (numberValue.StartsWith("0"))
            {
                numberValue = numberValue.Substring(1, numberValue.Length - 1);
            }

            //20 30 40 etc
            if (numberValue.EndsWith("0"))
            {
                CalculateTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstTens);
            }
            else
            {
                //Calc all the First numbers
                CalculateTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstTens);
                //Calc all the last numbers
                strValue.Append(" ");
                Calculate0To9((numberValue.Substring(1, 1)), strValue, _lstUnderTen);
            }

            return strValue;
        }

        private StringBuilder Calculate100To999(string numberValue)
        {
            var strValue = new StringBuilder();
            //100 200 300
            if (numberValue.Substring(1, 2) == "00")
            {
                CalculateTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstHundreds);
            }
            //101 102 304 506 etc: and values
            else if (numberValue.Substring(1, 1) == "0")
            {
                CreateAndValues(numberValue, strValue);
            }
            else
            {
                CalculateTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstHundreds);
                strValue.Append(string.Format("{0} {1} {2}", string.Empty, Language.And, string.Empty));

                //250:Stop 
                if (numberValue.Substring(2, 1) == "0")
                {
                    CalculateTensOrHundreds(numberValue.Substring(1, 1), strValue, _lstTens);
                }
                else
                {
                    //110 119 etc
                    if (numberValue.Substring(1, 1) == "1")
                    {
                        return Calculate10To19(numberValue.Substring(1, 2), strValue, _lstTenTo19);
                    }
                    CalculateTensOrHundreds(numberValue.Substring(1, 1), strValue, _lstTens);
                    strValue.Append("-");
                    Calculate0To9(numberValue.Substring(2, 1), strValue, _lstUnderTen);
                }
            }

            return strValue;
        }


        private void CreateAndValues(string numberValue, StringBuilder strValue)
        {
            CalculateTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstHundreds);
            strValue.Append(string.Format("{0} {1} {2}", string.Empty, Language.And, string.Empty));
            Calculate0To9(numberValue.Substring(2, 1), strValue, _lstUnderTen);
        }


        //10 100
        private static void CalculateTensOrHundreds(string numberValue, StringBuilder strValue,
            IReadOnlyList<string> textValue)
        {
            //9099: Sends 099 Ignore the 0
            if (numberValue != "0")
            {
                strValue.Append(textValue[Convert.ToInt32(numberValue) - 1]);
            }
        }

        private void DisplayStringValues(StringBuilder globalString, StringBuilder stringQuadrillions, StringBuilder stringTrillions, StringBuilder stringBillions, StringBuilder stringMillions,
            StringBuilder stringThousands, StringBuilder stringDollars)
        {
            switch (_globalWorkingWithIndicator)
            {
                case Quadrillions:
                    globalString.Append(AddSpace(stringQuadrillions));
                    globalString.Append(AddSpace(stringTrillions));
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    break;
                case Trillion:
                    globalString.Append(AddSpace(stringTrillions));
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    break;
                case Billion:
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    break;
                case Million:
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    break;
                case Thousand:
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    break;
                case Dollar:
                    globalString.Append(stringDollars);
                    break;
            }
        }


        private void ExtractStringValuesFromInput(string inputString, StringBuilder currentStringDollars,
            StringBuilder currentStringThousands, StringBuilder currentStringMillions, StringBuilder currentStringBillions, StringBuilder currentStringTrillions, StringBuilder currentStringQuadrillions)
        {
            for (_workingWithIndicator = 0; _workingWithIndicator < _globalWorkingWithIndicator; _workingWithIndicator++)
            {
                switch (_workingWithIndicator)
                {
                    case FirstLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringDollars, Language.Dollars);
                        break;
                    case SecondLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringThousands, Language.Thousand);
                        break;
                    case ThirdLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringMillions, Language.Million);
                        break;
                    case FourthLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringBillions, Language.Billion);
                        break;
                    case FifthLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringTrillions, Language.Trillion);
                        break;
                    case SixLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringQuadrillions, Language.Quadrillion);
                        break;
                }
            }
        }

        private void CreateValuesAndAddCurrency(string inputString, StringBuilder currentStringDollars, string currency)
        {
            var stringValue =
                CreateStringValues(GetCurrentInputStringFragment(inputString, _workingWithIndicator));
            if (stringValue.Length > 0)
            {
                currentStringDollars.Append(stringValue + " " + currency);
            }
            return;
        }

        private string GetCurrentInputStringFragment(string inputDollarString, int loopCounter)
        {
            return _lstInputBreakUp[loopCounter];
        }

        private void SplitInputvalueIntoArrayOfThrees(string inputDollarString)
        {
            var inputToStore = string.Empty;
            var counter = 1;
            var secondCounter = 0;
            for (var i = 1; i <= inputDollarString.Length; i++)
            {
                //values less than 3
                inputToStore = inputDollarString.Substring(inputDollarString.Length - i, counter++);
                if (i % 3 == 0)
                {
                    _lstInputBreakUp.Add(inputDollarString.Substring(inputDollarString.Length - i, i + secondCounter));
                    inputToStore = string.Empty;
                    secondCounter -= 3;
                    counter -= 3;
                }
            }

            if (!string.IsNullOrWhiteSpace(inputToStore))
            {
                //Add if break and you have value less than 3
                _lstInputBreakUp.Add(inputToStore);
            }

            //Total Entries example 1/112/345/678
            _globalWorkingWithIndicator = _lstInputBreakUp.Count;
        }



    }



}