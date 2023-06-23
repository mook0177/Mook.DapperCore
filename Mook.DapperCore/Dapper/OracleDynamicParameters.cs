using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace Mook.DapperCore
{
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters dynamicParameters;

        private readonly List<OracleParameter> oracleParameters = new();

        public OracleDynamicParameters(params string[] refCursorNames)
        {
            dynamicParameters = new DynamicParameters();
            AddRefCursorParameters(refCursorNames);
        }

        public OracleDynamicParameters(object template, params string[] refCursorNames)
        {
            dynamicParameters = new DynamicParameters(template);
            AddRefCursorParameters(refCursorNames);
        }

        private void AddRefCursorParameters(params string[] refCursorNames)
        {
            foreach (string refCursorName in refCursorNames)
            {
                var oracleParameter = new OracleParameter(refCursorName, OracleDbType.RefCursor, ParameterDirection.Output);
                oracleParameters.Add(oracleParameter);
            }
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);
            if (command is OracleCommand oracleCommand)
            {
                oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }
    }
}