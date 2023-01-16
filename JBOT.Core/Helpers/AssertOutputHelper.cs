using JBOT.Application.Dtos;
using JBOT.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Helpers
{
    public static class AssertOutputHelper
    {
        public static bool? Assert(string expected, string actual, OperatorEnums operatorEnum)
        {
            bool IsSuccess = false;
            switch (operatorEnum)
            {
                case OperatorEnums.Equal:
                    IsSuccess = IsEqualTo(expected, actual);
                    break;
                case OperatorEnums.NotEqual:
                    IsSuccess = IsNotEqualTo(expected, actual);
                    break;
                case OperatorEnums.GreaterThan:
                    IsSuccess = IsGreaterThan(expected, actual);
                    break;
                case OperatorEnums.GreaterThanOrEqualTo:
                    IsSuccess = IsGreaterThanOrEqualTo(expected, actual);
                    break;
                case OperatorEnums.LessThan:
                    IsSuccess = IsLesserThan(expected, actual);
                    break;
                case OperatorEnums.LessThanOrEqualTo:
                    IsSuccess = IsLesserThanOrEqualTo(expected, actual);
                    break;
                default:
                    IsSuccess = false;
                    break;
            }
            return IsSuccess;
        }
        public static bool? Assert(this OutputParameterDto output)
        {
            switch (output.Operator)
            {
                case OperatorEnums.Equal:
                    output.IsSuccess = IsEqualTo(output.Expected, output.Actual);
                    break;
                case OperatorEnums.NotEqual:
                    output.IsSuccess = IsNotEqualTo(output.Expected, output.Actual);
                    break;
                case OperatorEnums.GreaterThan:
                    output.IsSuccess = IsGreaterThan(output.Expected, output.Actual);
                    break;
                case OperatorEnums.GreaterThanOrEqualTo:
                    output.IsSuccess = IsGreaterThanOrEqualTo(output.Expected, output.Actual);
                    break;
                case OperatorEnums.LessThan:
                    output.IsSuccess = IsLesserThan(output.Expected, output.Actual);
                    break;
                case OperatorEnums.LessThanOrEqualTo:
                    output.IsSuccess = IsLesserThanOrEqualTo(output.Expected, output.Actual);
                    break;
                default:
                    output.IsSuccess = false;
                    break;
            }
            return output.IsSuccess;
        }
        public static void Assert(this List<OutputParameterDto> outputs)
        {
            foreach (var output in outputs)
            {
                switch (output.Operator)
                {
                    case OperatorEnums.Equal:
                        output.IsSuccess = IsEqualTo(output.Expected,output.Actual);
                        break;
                    case OperatorEnums.NotEqual:
                        output.IsSuccess = IsNotEqualTo(output.Expected, output.Actual);
                        break;
                    case OperatorEnums.GreaterThan:
                        output.IsSuccess = IsGreaterThan(output.Expected, output.Actual);
                        break;
                    case OperatorEnums.GreaterThanOrEqualTo:
                        output.IsSuccess = IsGreaterThanOrEqualTo(output.Expected, output.Actual);
                        break;
                    case OperatorEnums.LessThan:
                        output.IsSuccess = IsLesserThan(output.Expected, output.Actual);
                        break;
                    case OperatorEnums.LessThanOrEqualTo:
                        output.IsSuccess = IsLesserThanOrEqualTo(output.Expected, output.Actual);
                        break;
                    default:
                        output.IsSuccess = false;
                        break;
                }
            }
        }

        private static bool IsEqualTo(string expected, string actual)
        {
            decimal _expected, _actual;
            DateTime _expectedInDateTime, _actualInDateTime;
            if (Decimal.TryParse(expected, out _expected) &&
                Decimal.TryParse(actual, out _actual))
            {
                return _expected == _actual;
            }
            else if (DateTime.TryParse(expected, out _expectedInDateTime) && 
                    DateTime.TryParse(actual, out _actualInDateTime))
            {
                return _expectedInDateTime == _actualInDateTime;
            }
            else
            {
                return expected == actual;
            }
        }
        private static bool IsNotEqualTo(string expected, string actual)
        {
            Decimal _expected, _actual;
            DateTime _expectedInDateTime, _actualInDateTime;
            if (Decimal.TryParse(expected, out _expected) &&
                Decimal.TryParse(actual, out _actual))
            {
                return _expected != _actual;
            }
            else if (DateTime.TryParse(expected, out _expectedInDateTime) &&
                    DateTime.TryParse(actual, out _actualInDateTime))
            {
                return _expectedInDateTime != _actualInDateTime;
            }
            else
            {
                return expected != actual;
            }
        }

        private static bool IsGreaterThan(string expected, string actual)
        {
            Decimal _expected, _actual;
            DateTime _expectedInDateTime, _actualInDateTime;
            if (Decimal.TryParse(expected, out _expected) &&
                Decimal.TryParse(actual, out _actual))
            {
                return _expected > _actual;
            }
            else if (DateTime.TryParse(expected, out _expectedInDateTime) &&
                    DateTime.TryParse(actual, out _actualInDateTime))
            {
                return _expectedInDateTime > _actualInDateTime;
            }
            else
            {
                return false;
            }
        }
        private static bool IsGreaterThanOrEqualTo(string expected, string actual)
        {
            Decimal _expected, _actual;
            DateTime _expectedInDateTime, _actualInDateTime;
            if (Decimal.TryParse(expected, out _expected) &&
                Decimal.TryParse(actual, out _actual))
            {
                return _expected >= _actual;
            }
            else if (DateTime.TryParse(expected, out _expectedInDateTime) &&
                    DateTime.TryParse(actual, out _actualInDateTime))
            {
                return _expectedInDateTime >= _actualInDateTime;
            }
            else
            {
                return false;
            }
        }

        private static bool IsLesserThan(string expected, string actual)
        {
            Decimal _expected, _actual;
            DateTime _expectedInDateTime, _actualInDateTime;
            if (Decimal.TryParse(expected, out _expected) &&
                Decimal.TryParse(actual, out _actual))
            {
                return _expected < _actual;
            }
            else if (DateTime.TryParse(expected, out _expectedInDateTime) &&
                    DateTime.TryParse(actual, out _actualInDateTime))
            {
                return _expectedInDateTime < _actualInDateTime;
            }
            else
            {
                return false;
            }
        }

        private static bool IsLesserThanOrEqualTo(string expected, string actual)
        {
            Decimal _expected, _actual;
            DateTime _expectedInDateTime, _actualInDateTime;
            if (Decimal.TryParse(expected, out _expected) &&
                Decimal.TryParse(actual, out _actual))
            {
                return _expected <= _actual;
            }
            else if (DateTime.TryParse(expected, out _expectedInDateTime) &&
                    DateTime.TryParse(actual, out _actualInDateTime))
            {
                return _expectedInDateTime <= _actualInDateTime;
            }
            else
            {
                return false;
            }
        }
    }
}
