using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;



namespace ORM
{
    public class TodoRepository : RepositoryConnector, Interfaces.ITodoRepository
    {
        public TodoRepository(IConfiguration config) : base(config) { }

        public void Add(ToDo obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Tarefa", obj.Tarefa);

            string sql = "INSERT INTO Todo(Tarefa) VALUES(@Tarefa)";
            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, param);
            }
        }

        public ToDo Get(int id)
        {
            string sql = $"SELECT * FROM Todo WHERE Id = {id}";
            using (var con = new SqlConnection(base.GetConnection()))
            {
                return con.Query<ToDo>(sql).FirstOrDefault();
            }

        }

        public IEnumerable<ToDo> GetAll()
        {
            IEnumerable<ToDo> retorno;
            string sql = "SELECT * FROM Todo";
            using (var con = new SqlConnection(base.GetConnection()))
            {
                retorno = con.Query<ToDo>(sql);
            }
            return retorno;
        }

        public void Remove(ToDo obj)
        {
            string sql = $"DELETE FROM Todo WHERE Id = {obj.Id}";
            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql);
            }
        }

        public void Update(ToDo obj)
        {
            string sql = $"UPDATE Todo SET Tarefa = @Tarefa WHERE Id = {obj.Id}";
            DynamicParameters param = new DynamicParameters();
            param.Add("@Tarefa", obj.Tarefa);

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, param);
            }
        }
    }
}
