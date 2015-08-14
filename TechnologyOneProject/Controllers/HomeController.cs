using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechnologyOneProject.Infastructure.Alerts;
using TechnologyOneProject.Models;
using Microsoft.Web.Mvc;
using WebGrease.Css.Extensions;


namespace TechnologyOneProject.Controllers
{
    public class HomeController : Controller
    {
        const int Billion = 4;
        const int Million = 3;
        const int Thousand = 2;
        const int Dollar = 1;
        const int CalculateCents = 2;
        private readonly List<string> _lstUnderTen;
        private readonly List<string> _lstTenTo19;
        private readonly List<string> _lstTens;
        private readonly List<string> _lstHundreds;
        private readonly List<string> _lstInputBreakUp = new List<string>();
        private int _globalLoopCounter = 0;
        private int _loopCounter = 0;

        public HomeController()
        {

            _lstUnderTen = new List<string>
            {
                Language.Zero,Language.One,Language.Two,Language.Three,Language.Four,Language.Five,Language.Six,Language.Seven,Language.Eight,Language.Nine};

            _lstTenTo19 = new List<string>
            {
                Language.Ten,Language.Eleven,Language.Twelve,Language.Thirteen,Language.Fourteen, Language.Fifteen,Language.Sixteen,Language.Seventeen,Language.Eighteen,Language.Nineteen};

            _lstTens = new List<string>
            {
                Language.Ten,Language.Twenty,Language.Thirty,Language.Fourty,Language.Fifty,Language.Sixty,Language.Seventy,Language.Eigthy,Language.Ninety
            };

            _lstHundreds = new List<string>
            {
                string.Format("{0} {1}",Language.One,Language.Hundred),
                string.Format("{0} {1}",Language.Two,Language.Hundred),
                string.Format("{0} {1}",Language.Three,Language.Hundred),
                string.Format("{0} {1}",Language.Four,Language.Hundred),
                string.Format("{0} {1}",Language.Five,Language.Hundred),
                string.Format("{0} {1}",Language.Six,Language.Hundred),
                string.Format("{0} {1}",Language.Seven,Language.Hundred),
               string.Format("{0} {1}",Language.Eight,Language.Hundred),
                string.Format("{0} {1}",Language.Nine,Language.Hundred),
            };
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
            var currentStringDollars = new StringBuilder();
            var currentStringThousands = new StringBuilder();
            var currentStringMillions = new StringBuilder();
            var currentStringBillions = new StringBuilder();
            #endregion

            if (ModelState.IsValid)
            {

                #region Split the inputValue into Array: And assign to variables
                var splitDollarsAndCents = inputNumber.Number.ToString().Split('.');
                var inputDollarString = splitDollarsAndCents[0];
                #endregion

                #region Calculate loop total

                var storeStr = string.Empty;
                var splitVal = 1;
                var splitVal2 = 0;
                for (int i = 1; i <= inputDollarString.Length; i++)
                {
                    storeStr = inputDollarString.Substring(inputDollarString.Length - i, splitVal++);
                    if (i % 3 == 0)
                    {
                        _lstInputBreakUp.Add(inputDollarString.Substring(inputDollarString.Length - i, i + splitVal2));
                        storeStr = string.Empty;
                        splitVal2 -= 3;
                        splitVal -= 3;
                    }
                }

                if (!string.IsNullOrWhiteSpace(storeStr))
                {
                    _lstInputBreakUp.Add(storeStr);
                }

                //Set Equal to the entries
                _globalLoopCounter = _lstInputBreakUp.Count;


                #endregion

                #region Loop and get strings
                for (_loopCounter = 0; _loopCounter < _globalLoopCounter; _loopCounter++)
                {
                    if (_loopCounter == 0)
                    {
                        //Hide Dollar if no value
                        var dollarStringValue = CalculateWithoutCents(GetCurrentSubString(inputDollarString, _loopCounter));
                        if (dollarStringValue.Length > 0)
                        {
                            currentStringDollars.Append(dollarStringValue + " " + Language.Dollars);
                        }

                    }
                    else if (_loopCounter == 1)
                    {
                        //Hide Thousand if no value
                        var thousandStringValue = CalculateWithoutCents(GetCurrentSubString(inputDollarString, _loopCounter));
                        if (thousandStringValue.Length > 0)
                        {
                            currentStringThousands.Append(thousandStringValue + " " + Language.Thousand);
                        }

                    }
                    else if (_loopCounter == 2)
                    {
                        var millionString = CalculateWithoutCents(GetCurrentSubString(inputDollarString, _loopCounter));
                        if (millionString.Length > 0)
                        {
                            currentStringMillions.Append(millionString + " " + Language.Million);
                        }
                        
                    }
                    else if (_loopCounter == 3)
                    {
                        currentStringBillions.Append(CalculateWithoutCents(GetCurrentSubString(inputDollarString, _loopCounter)) + " " + Language.Billion);
                    }
                }
                #endregion

                #region Build GLobal String

                if (_globalLoopCounter == Billion)
                {
                    globalString.Append(currentStringBillions + " ");
                    globalString.Append(currentStringMillions + " ");
                    globalString.Append(currentStringThousands + " ");
                    globalString.Append(CheckForAnd(_lstInputBreakUp[0]) + currentStringDollars);
                }
                else if (_globalLoopCounter == Million)
                {
                    //globalString.Append(currentStringBillions + " ");
                    globalString.Append(currentStringMillions + " ");
                    globalString.Append(currentStringThousands + " ");
                    globalString.Append(CheckForAnd(_lstInputBreakUp[0]) + currentStringDollars);
                }
                else if (_globalLoopCounter == Thousand)
                {
                    //globalString.Append(currentStringMillions + " ");
                    globalString.Append(currentStringThousands + " ");
                    globalString.Append(CheckForAnd(_lstInputBreakUp[0]) + currentStringDollars);
                }
                else if (_globalLoopCounter == Dollar)
                {
                    //globalString.Append(currentStringThousands + " ");
                    globalString.Append(currentStringDollars);
                }

                #endregion

                ViewBag.Output += globalString.ToString();

                return View(inputNumber).WithSuccess(globalString.ToString());
                //return View(retVal.ToString());
                //return View(inputNumber);
            }
            else
            {
                return View(inputNumber);

            }



        }

        private string CheckForAnd(string inputString)
        {
            //if 35 000 000 : If first is 0 and it has value then and
            if (inputString.StartsWith("0") && Convert.ToInt32(inputString) > 0)
            {
                return string.Format("{0} {1} {2}", string.Empty, Language.And, string.Empty);
            }
            return string.Empty;
        }


        private string GetCurrentSubString(string inputDollarString, int loopCounter)
        {
            return _lstInputBreakUp[loopCounter];
        }

        private StringBuilder CalculateWithoutCents(string inputValue)
        {
            var strValue = new StringBuilder();
            var dollarStringLength = inputValue.Length;

            if (Convert.ToInt32(inputValue) < 10)
            {
                GetValuesUnderTen(inputValue, strValue, _lstUnderTen);
            }
            else if (Convert.ToInt32(inputValue) < 20)
            {
                strValue = GetValues10To19(inputValue, strValue, _lstTenTo19);
            }
            else if (Convert.ToInt32(inputValue) < 100)
            {
                strValue = Calculate20To99(inputValue);
            }
            else if (Convert.ToInt32(inputValue) < 1000)
            {
                strValue = Calculate100To999(inputValue);
            }

            return strValue;
        }

        private static StringBuilder GetValues10To19(string numberValue, StringBuilder strValue, List<string> textValue)
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
                GetTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstTens);
            }
            else
            {
                //Calc all the First numbers
                GetTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstTens);
                //Calc all the last numbers
                strValue.Append(" ");
                GetValuesUnderTen((numberValue.Substring(1, 1)), strValue, _lstUnderTen);

            }

            return strValue;
        }

        private StringBuilder Calculate100To999(string numberValue)
        {
            var strValue = new StringBuilder();
            //100 200 300
            if (numberValue.Substring(1, 2) == "00")
            {
                GetTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstHundreds);
            }
            //101 102 304 506 etc: and values
            else if (numberValue.Substring(1, 1) == "0")
            {
                CreateAndValues(numberValue, strValue);
            }
            else
            {
                GetTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstHundreds);
                strValue.Append(string.Format("{0} {1} {2}", string.Empty, Language.And, string.Empty));

                //250:Stop 
                if (numberValue.Substring(2, 1) == "0")
                {
                    GetTensOrHundreds(numberValue.Substring(1, 1), strValue, _lstTens);
                }
                else
                {
                    //110 119 etc
                    if (numberValue.Substring(1, 1) == "1")
                    {
                        return GetValues10To19(numberValue.Substring(1, 2), strValue, _lstTenTo19);
                    }
                    else
                    {
                        GetTensOrHundreds(numberValue.Substring(1, 1), strValue, _lstTens);
                        strValue.Append("-");
                        GetValuesUnderTen(numberValue.Substring(2, 1), strValue, _lstUnderTen);
                    }
                }



            }

            return strValue;
        }

        private void CreateAndValues(string numberValue, StringBuilder strValue)
        {
            GetTensOrHundreds(numberValue.Substring(0, 1), strValue, _lstHundreds);
            strValue.Append(string.Format("{0} {1} {2}", string.Empty, Language.And, string.Empty));
            GetValuesUnderTen(numberValue.Substring(2, 1), strValue, _lstUnderTen);

        }

        private static StringBuilder GetValuesUnderTen(string numberValue, StringBuilder strValue, IReadOnlyList<string> textValue)
        {
            strValue.Append(textValue[Convert.ToInt32(numberValue)]);
            return strValue;
        }

        //10 100
        private static void GetTensOrHundreds(string numberValue, StringBuilder strValue, IReadOnlyList<string> textValue)
        {
            //9099: Sends 099 Ignore the 0
            if (numberValue != "0")
            {
                strValue.Append(textValue[Convert.ToInt32(numberValue) - 1]);
            }


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }




    }
}