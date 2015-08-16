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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Number")] InputNumber inputNumber)
        {
            #region Variables

            var globalString = new StringBuilder();
            var stringCents = new StringBuilder();
            var stringDollars = new StringBuilder();
            var stringThousands = new StringBuilder();
            var stringMillions = new StringBuilder();
            var stringBillions = new StringBuilder();
            var stringTrillions = new StringBuilder();
            var stringQuadrillions = new StringBuilder();
            var stringQuantillion = new StringBuilder();
            var stringSexTillion = new StringBuilder();

            #endregion

            if (!ModelState.IsValid) return View(inputNumber);

            #region Main Logic

            try
            {
                ValidateInput(inputNumber);

                var splitDollarsAndCents = inputNumber.Number.Split('.');

                var inputString = splitDollarsAndCents[0];

                CalculateCents(splitDollarsAndCents, stringCents);

                SplitInputvalueIntoArrayOfThrees(inputString);

                //Loop and get String Values
                ExtractStringValuesFromInput(inputString, stringDollars, stringThousands, stringMillions, stringBillions,
                    stringTrillions, stringQuadrillions, stringQuantillion, stringSexTillion);

                //Add Every Loop with Currency to output
                DisplayStringValues(globalString, stringSexTillion, stringQuantillion, stringQuadrillions,
                    stringTrillions, stringBillions, stringMillions,
                    stringThousands, stringDollars, stringCents);

                //For Unit Testing
                ViewBag.Output += RemoveSpaces(globalString.ToString());


                if (globalString.Length != 0)
                {
                    return View(inputNumber)
                        .WithSuccess(globalString.ToString(),
                            string.Format("{0}{1}", Language.Currency, inputNumber.Number));
                }
            }
            catch (Exception err)
            {
                return View(inputNumber).WithError(err.Message, string.Empty);
            }

            #endregion

            return View(inputNumber);
        }

        #region Private Variables

        private const int FirstLoop = 0;
        private const int SecondLoop = 1;
        private const int ThirdLoop = 2;
        private const int FourthLoop = 3;
        private const int FifthLoop = 4;
        private const int SixLoop = 5;
        private const int SeventhLoop = 6;
        private const int EightLoop = 8;
        private const int SexTillion = 8;
        private const int QuanTillion = 7;
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
        private const int CalculateCent = 2;
        private readonly List<string> _lstHundreds;
        private readonly List<string> _lstInputBreakUp = new List<string>();
        private readonly List<string> _lstTens;
        private readonly List<string> _lstTenTo19;
        private readonly List<string> _lstUnderTen;
        private string _globalError;
        private int _globalWorkingWithIndicator;
        private int _workingWithIndicator;

        #endregion

        #region Private Functions

        private static void AddCents(StringBuilder globalString, StringBuilder stringCents)
        {
            if (stringCents.Length > 0)
            {
                globalString.Append(stringCents);
            }
        }

        private static string AddAndToValuesOver999(string inputString)
        {
            //if 35 000 000 : If first is 0 and it has value then and
            if (inputString.StartsWith("0") && Convert.ToInt32(inputString) > 0)
            {
                return string.Format("{0} {1}", Language.And, string.Empty);
            }
            return string.Empty;
        }

        private static string AddSpace(StringBuilder inputString)
        {
            return string.Format("{0} {1}", inputString, string.Empty);
        }

        private void CalculateCents(string[] splitDollarsAndCents, StringBuilder stringCents)
        {
            if (splitDollarsAndCents.Length != 2) return;

            //Two decimals
            var inputLength = splitDollarsAndCents[1].Length;

            //Get the correct amount of decimals
            var inputCentString = splitDollarsAndCents[1].Substring(0,
                CalculateInputLength(splitDollarsAndCents, inputLength));

            //For Unit Testing
            ViewBag.Cents = inputCentString;
            stringCents.Append(string.Format("{0} {1} {2}", string.Empty, Language.And, string.Empty));

            //All .1 .3 .4 All the Tens
            if (inputCentString.Length.Equals(1))
            {
                CalculateTensOrHundreds(inputCentString, stringCents, _lstTens);
                stringCents.Append(string.Format("{0} {1}", string.Empty, Language.Cents));
            }
            //.01  .02  .03 All Single Cents
            else if (inputCentString.Length.Equals(2) && (inputCentString.Substring(0, 1).Equals("0")))
            {
                Calculate0To9(inputCentString, stringCents, _lstUnderTen);
                stringCents.Append(inputCentString.Equals("01")
                    ? string.Format("{0} {1}", string.Empty, Language.Cent)
                    : string.Format("{0} {1}", string.Empty, Language.Cents));
            }
            //.22 .45 .54 etc: Normal Cents
            else
            {
                if (Convert.ToInt32(inputCentString) <= 9)
                {
                    Calculate0To9(inputCentString, stringCents, _lstUnderTen);
                }
                else if (Convert.ToInt32(inputCentString) < 20)
                {
                    Calculate10To19(inputCentString, stringCents, _lstTenTo19);
                }
                else if (Convert.ToInt32(inputCentString) > 19)
                {
                    stringCents.Append(Calculate20To99(inputCentString));
                }
                stringCents.Append(string.Format("{0} {1}", string.Empty, Language.Cents));
            }
        }

        private static int CalculateInputLength(string[] splitDollarsAndCents, int inputLength)
        {
            return splitDollarsAndCents[1].Length > 2 ? 2 : inputLength;
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

            //09:Single Number
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
                strValue.Append("-");
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

        private void CreateValuesAndAddCurrency(string inputString, StringBuilder currentStringDollars, string currency)
        {
            var currentString = GetCurrentInputStringFragment(inputString, _workingWithIndicator);
            var stringValue =
                CreateStringValues(currentString);
            if (stringValue.Length <= 0) return;

            //This is complicated Calculation to add Dollars to 1000 100000 1000000 etc. But 10 20 30 40 etc should be omitted.
            if (!string.IsNullOrWhiteSpace(Convert.ToString(stringValue))
                &&
                (_lstUnderTen.Contains(Convert.ToString(stringValue)) ||
                 _lstTens.Contains(Convert.ToString(stringValue)))
                && (IsValidInputForThisTest(inputString))
                && inputString.Length > 2)
            {
                currentStringDollars.Append(
                    string.Format("{0} {1} {2}", stringValue, currency, Language.Dollars).TrimEnd());
            }
            else
            {
                currentStringDollars.Append(stringValue + " " + currency);
            }
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

        private void DisplayStringValues(StringBuilder globalString, StringBuilder stringSexTillions,
            StringBuilder stringQuantillions, StringBuilder stringQuadrillions,
            StringBuilder stringTrillions, StringBuilder stringBillions, StringBuilder stringMillions,
            StringBuilder stringThousands, StringBuilder stringDollars, StringBuilder stringCents)
        {
            switch (_globalWorkingWithIndicator)
            {
                case SexTillion:
                    globalString.Append(AddSpace(stringSexTillions));
                    globalString.Append(AddSpace(stringQuantillions));
                    globalString.Append(AddSpace(stringQuadrillions));
                    globalString.Append(AddSpace(stringTrillions));
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    AddCents(globalString, stringCents);
                    break;
                case QuanTillion:
                    globalString.Append(AddSpace(stringQuantillions));
                    globalString.Append(AddSpace(stringQuadrillions));
                    globalString.Append(AddSpace(stringTrillions));
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    AddCents(globalString, stringCents);
                    break;
                case Quadrillions:
                    globalString.Append(AddSpace(stringQuadrillions));
                    globalString.Append(AddSpace(stringTrillions));
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    AddCents(globalString, stringCents);
                    break;
                case Trillion:
                    globalString.Append(AddSpace(stringTrillions));
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    AddCents(globalString, stringCents);
                    break;
                case Billion:
                    globalString.Append(AddSpace(stringBillions));
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    AddCents(globalString, stringCents);
                    break;
                case Million:
                    globalString.Append(AddSpace(stringMillions));
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    AddCents(globalString, stringCents);
                    break;
                case Thousand:
                    globalString.Append(AddSpace(stringThousands));
                    globalString.Append(AddAndToValuesOver999(_lstInputBreakUp[0]) + stringDollars);
                    AddCents(globalString, stringCents);
                    break;
                case Dollar:
                    globalString.Append(stringDollars);
                    //Check for .1 and no other values
                    AddCents(globalString, RemoveAndLogic(globalString, stringCents));
                    break;
                default:
                    throw new Exception(Language.ErrorGettingNumber);
                    break;
            }
        }


        private void ExtractStringValuesFromInput(string inputString, StringBuilder currentStringDollars,
            StringBuilder currentStringThousands, StringBuilder currentStringMillions,
            StringBuilder currentStringBillions, StringBuilder currentStringTrillions,
            StringBuilder currentStringQuadrillions, StringBuilder currentStringQuanTillion,
            StringBuilder currentStringSexTillion)
        {
            for (_workingWithIndicator = 0;
                _workingWithIndicator < _globalWorkingWithIndicator;
                _workingWithIndicator++)
            {
                switch (_workingWithIndicator)
                {
                    case FirstLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringDollars,
                            GetCorrectDollarString(inputString));
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
                    case SeventhLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringQuanTillion, Language.Quintillion);
                        break;
                    case EightLoop:
                        CreateValuesAndAddCurrency(inputString, currentStringSexTillion, Language.SexTillion);
                        break;
                }
            }
        }

        private static string GetCorrectDollarString(string inputString)
        {
            if (inputString.Length < 3)
            {
                return Convert.ToInt32(inputString) == 1 ? Language.Dollar : Language.Dollars;
            }
            return Language.Dollars;
        }

        private string GetCurrentInputStringFragment(string inputDollarString, int loopCounter)
        {
            return _lstInputBreakUp[loopCounter];
        }


        private static bool IsValidInputForThisTest(string inputString)
        {
            var test = false;

            //This is to get values like 2000 4000 6000 5000000000 900000000 etc.
            //Its a once of senario
            for (var i = 1; i < inputString.Length; i++)
            {
                test = (inputString.Substring(i, 1) == "0");
                if (!test)
                {
                    break;
                }
            }

            return test;
        }


        private static StringBuilder RemoveAndLogic(StringBuilder globalString, StringBuilder stringCents)
        {
            return globalString.Length == 0
                ? stringCents.Replace(string.Empty + Language.And + string.Empty, "")
                : stringCents;
        }

        private string RemoveSpaces(string retValue)
        {
            return retValue.Replace("  ", "").TrimEnd();
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
                if (i%3 == 0)
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

            ViewBag.InputSplitValues = _lstInputBreakUp;
        }

        private static void ValidateInput(InputNumber inputNumber)
        {
            if (string.IsNullOrWhiteSpace(inputNumber.Number))
            {
                throw new Exception(Language.InputEmptyError);
            }
            if (inputNumber.Number.Split('.').Length > 2)
            {
                throw new Exception(Language.InvalidInput);
            }

            if (inputNumber.Number.StartsWith("."))
            {
                inputNumber.Number = inputNumber.Number.Insert(0, "0");
            }
        }

        #endregion
    }
}