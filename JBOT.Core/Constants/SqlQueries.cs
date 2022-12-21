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
            	 , CONCAT(schema_name(obj.schema_id),'.' , obj.name) as Name
            	 , 'Stored Procedure' as Type
            	 , substring(par.parameters, 0, len(par.parameters)) as Parameters
            	 , NULL as ReturnType
            FROM [{0}].sys.procedures obj
            cross apply (select p.name + ' ' + TYPE_NAME(p.user_type_id) + ', ' 
                         from [{0}].sys.parameters p
                         where p.object_id = obj.object_id 
                               and p.parameter_id != 0 
                        for xml path ('') ) par (parameters)
            UNION ALL
            select 
            	   obj.object_id,
                   CONCAT(schema_name(obj.schema_id),'.' , obj.name) as Name,
                   'Scalar Function' as type,
                    substring(par.parameters, 0, len(par.parameters)) as Parameter,
                    TYPE_NAME(ret.user_type_id) as ReturnType
            from [{0}].sys.objects obj
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
    }
}
