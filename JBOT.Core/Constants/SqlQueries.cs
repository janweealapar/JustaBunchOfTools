using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Application.Constants
{
    public static class SqlQueries
    {
        public const string GetDatabasesQuery = "SELECT database_id as Id ,Name FROM sys.databases";
        public const string GetDatabaseByIdQuery = "SELECT database_id as Id ,Name FROM sys.databases WHERE database_id='{0}'";
        public const string GetTestableObjectsQuery =
            @"SELECT 
            	   obj.object_id as ObjectId
            	 , CONCAT(sc.name,'.' , obj.name) as Name
            	 , 'Stored Procedure' as Type
            	 , substring(par.parameters, 0, len(par.parameters)) as Parameters
            	 , NULL as ReturnType
            FROM [{0}].sys.procedures obj
			LEFT join [{0}].sys.schemas sc on sc.schema_id = obj.schema_id
            cross apply (select p.name + ' ' + TYPE_NAME(p.user_type_id) + ', ' 
                         from [{0}].sys.parameters p
                         where p.object_id = obj.object_id 
                        for xml path ('') ) par (parameters)
            UNION ALL
            select 
            	   obj.object_id,
                   CONCAT(sc.name,'.' , obj.name) as Name,
                   'Scalar Function' as type,
                    substring(par.parameters, 0, len(par.parameters)) as Parameter,
                    TYPE_NAME(ret.user_type_id) as ReturnType
            from [{0}].sys.objects obj
			LEFT join [{0}].sys.schemas sc on sc.schema_id = obj.schema_id
            cross apply (select p.name + ' ' + TYPE_NAME(p.user_type_id) + ', ' 
                         from [{0}].sys.parameters p
                         where p.object_id = obj.object_id 
                               and p.parameter_id != 0 
                        for xml path ('') ) par (parameters)
            left join [{0}].sys.parameters ret
                      on obj.object_id = ret.object_id
                      and ret.parameter_id = 0
            where obj.type in ('FN')
            order by Name;";
        public const string GetTestableObjectDetailsQuery =
            @"select obj.object_id As ObjectId
            	,	CONCAT(sc.name,'.' , obj.name) as Name
            	, case obj.Type When 'P' THEN 'Stored Procedure'
            					WHEN 'FN' THEN 'Scalar Function'
            					ELSE type_desc END Type
            	, TYPE_NAME(ret.user_type_id) as ReturnType
                ,0 as Id
				,'' TestName
                ,'' Description
				,0 DatabaseId
				,'' as DatabaseName
                ,NULL as Status
                ,'' ErrorTitle
                ,'' ErrorMessage
                ,'' Server
            from [{0}].sys.objects obj
			Left join [{0}].sys.schemas sc on sc.schema_id = obj.schema_id
            left join [{0}].sys.parameters ret on obj.object_id = ret.object_id
            		and ret.parameter_id = 0
            where obj.object_id='{1}'";

        public const string GetTestableObjectParametersQuery =
            @"select 
            		p.parameter_id Id
            	,	p.Name
            	,	CAST(TYPE_NAME(p.user_type_id) as VARCHAR) DataType
                ,   max_length as MaxLength
            	,   Precision
            	,	Scale
            	,   is_output IsOutput
                ,   '' Value
            from [{0}].sys.parameters p
            where p.object_id = '{1}'";
    }

    public static class ObjectType
    {
        public const string Procedure = "Stored Procedure";
        public const string ScalarFunction = "Scalar Function";
    }
}
