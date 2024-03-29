﻿using JBOT.Application.Helpers;
using JBOT.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Dtos
{
    public class TestableObjectDetailsDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public string Server { get; set; }
        public int DatabaseId { get; set; }
        public string DatabaseName { get; set; }
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<BaseParameterDto> Parameters { get; set; } = new List<BaseParameterDto>();
        public string ReturnType { get; set; }
        public List<ParameterDto> InputParameters { get; set; }
        public List<OutputParameterDto> OutputParameters { get; set; }
        public StatusEnums? Status { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }

        public TestableObjectDetailsDto DeepCopy()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            var obj = (TestableObjectDetailsDto)bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }
    public class BaseParameterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public int MaxLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool IsOutput { get; set; }
        public string DataTypeDisplay
        {
            get
            {
                return this.GetDataTypeDisplayFormat();
            }
        }
    }
    public class ParameterDto : BaseParameterDto
    {
        public string Value { get; set; }
    }
    public class OutputParameterDto : BaseParameterDto
    {
        public OperatorEnums? Operator { get; set; }
        public string Expected { get; set; }
        public string Actual { get; set; }
        public bool? IsSuccess { get; set; }
    }
}
