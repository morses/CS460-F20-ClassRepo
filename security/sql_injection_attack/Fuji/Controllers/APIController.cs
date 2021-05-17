using Fuji.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Fuji.Controllers
{
    public class APIController : Controller
    {

        private readonly FujiDbContext _context;
        private readonly ApplicationDbContext _identityContext;

        public APIController(FujiDbContext context, ApplicationDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        /// <summary>
        /// ***VERY*** poorly written API endpoint to simulate checking if a user has requested to receive our newsletter.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ActionResult> IsOnMailingList(string email)
        {
            string cmd = "SELECT UserName,Email,EmailConfirmed FROM AspNetUsers WHERE Email=" + "'" + email + "'";
            return Content(await RunQueryJSON(cmd, _identityContext), "application/json");
        }

        /// <summary>
        /// ***VERY*** poorly written API endpoint to look up apple variety information.
        /// </summary>
        /// <param name="varietyName"></param>
        /// <returns></returns>
        public async Task<ActionResult> AppleInfo(string varietyName)
        {
            string cmd = "SELECT VarietyName,ScientificName FROM Apple WHERE VarietyName=" + "'" + varietyName + "'";
            return Content(await RunQueryJSON(cmd,_context), "application/json");
        }

        /// <summary>
        /// 'Perfectly reasonable' helper method to run a query on a db, gather all results
        /// and serialize to JSON
        /// </summary>
        /// <param name="q">SQL to execute, i.e. "SELECT * FROM Apples"</param>
        /// <param name="db">DbContext to use</param>
        /// <returns>Results of query as a serialized JSON object</returns>
        private async Task<string> RunQueryJSON(string q, DbContext db)
        {
            // One way to execute "raw" sql through entity framework core, aka ADO.NET
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                bool open = command.Connection.State == ConnectionState.Open;
                if (!open)
                {
                    command.Connection.Open();
                }
                try
                {
                    command.CommandText = q;
                    List<DataTable> resultSet = new List<DataTable>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (!reader.IsClosed && reader.HasRows)
                        {
                            var table = new DataTable();
                            table.Load(reader);
                            resultSet.Add(table);
                        }
                        return JsonConvert.SerializeObject(resultSet);
                        // the previous line using NewtonSoft.JSON to turn it into JSON was needed to bypass
                        // a security setting on the built-in serializer, which doesn't allow serializing a DataTable
                        // because it is vulnerable: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/dataset-datatable-dataview/security-guidance
                        // Our use of JsonConvert is apparently itself vulnerable to DoS attack
                    }
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
                finally
                {
                    if (!open)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }

    }
}
