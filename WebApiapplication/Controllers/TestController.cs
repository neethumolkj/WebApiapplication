using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using WebApiapplication.Models;



namespace WebApiapplication.Controllers
{
    public class TestController : ApiController
    {
        SqlConnection sqlcon = new SqlConnection(@"server=USTJAVA90\MSSQLSERVER01;database=ustdb;Integrated Security=true");

        /* public DataSet Get()
          {
              SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from PLAYER", sqlcon);
              DataSet dsplr = new DataSet();
              dataAdapter.Fill(dsplr, "PLAYER");
              return dsplr;
          }

          public DataSet Get(int id)
          {
              SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from PLAYER where PLRNO=" + id, sqlcon);
              DataSet dsplr = new DataSet();
              dataAdapter.Fill(dsplr, "PLAYER");
              return dsplr;
          }*/


        public IEnumerable<Player> Get()
        {
            IList<Player> biolist = new List<Player>();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from PLAYER", sqlcon);
            DataSet dsBio = new DataSet();
            dataAdapter.Fill(dsBio, "Player");

            Player b = null;
            foreach (DataRow dr in dsBio.Tables[0].Rows)
            {
                b = new Player();
                b.Plrno = int.Parse(dr["PLRNO"].ToString());
                b.Plrname = dr["PLRNAME"].ToString();
                b.Game = dr["GAME"].ToString();
                b.Country = dr["COUNTRY"].ToString();
                biolist.Add(b);
            }
            return biolist;
        }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage msg = null;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from PLAYER where PLRNO=" + id, sqlcon);
            DataSet dsBio = new DataSet();
            dataAdapter.Fill(dsBio, "Player");
            Player b = null;
            foreach (DataRow dr in dsBio.Tables[0].Rows)
            {
                b = new Player();
                b.Plrno = int.Parse(dr[0].ToString());
                b.Plrname = dr[1].ToString();
                b.Game = dr[2].ToString();
                b.Country = dr[3].ToString();
            }
            if (b != null)
            {
                msg = Request.CreateResponse(HttpStatusCode.OK, b);
            }
            else
            {
                msg = Request.CreateResponse(HttpStatusCode.NotFound, "Player Not Found");
            }
            return msg;
        }
        public HttpResponseMessage Post([FromBody] Player b)
        {
            HttpResponseMessage msg = null;
            try
            {
                string inscmd = "Insert into PLAYER values(";
                inscmd += b.Plrno + ",'" + b.Plrname + "','" + b.Game + "','" + b.Country + "')";

                sqlcon.Open();
                SqlCommand sqlCommand = new SqlCommand(inscmd, sqlcon);
                int r = sqlCommand.ExecuteNonQuery();
                if (r > 0)
                {
                    msg = Request.CreateResponse(HttpStatusCode.OK, "Player Added....");
                    //msg.Headers.Location = new Uri(Request.RequestUri + b.RollNumber.ToString());                  
                }
            }
            catch (Exception ex)
            {
                msg = Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
            return msg;
        }
        public HttpResponseMessage Put(int id, [FromBody] Player b)
        {
            HttpResponseMessage msg = null;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from player where plrno=" + id, sqlcon);
            DataSet dsBio = new DataSet();
            dataAdapter.Fill(dsBio, "Player");
            if (dsBio.Tables["Player"].Rows.Count == 0)
            {
                msg = Request.CreateResponse(HttpStatusCode.NotFound, "Player Not Found");
            }
            else
            {
                sqlcon.Open();
                string uptcmd = "Update PLAYER set PLRNAME='" + b.Plrname + "', GAME='";
                uptcmd += b.Game + "', COUNTRY='" + b.Country + "' where PLRNO=" + b.Plrno;
                SqlCommand cmdDel = new SqlCommand(uptcmd, sqlcon);
                int r = cmdDel.ExecuteNonQuery();
                if (r > 0)
                {
                    msg = Request.CreateResponse(HttpStatusCode.OK, "Player Updated");
                }
                sqlcon.Close();
            }
            return msg;
        }

        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage msg = null;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from PLAYER where PLRNO=" + id, sqlcon);
            DataSet dsBio = new DataSet();
            dataAdapter.Fill(dsBio, "Player");
            if (dsBio.Tables["Player"].Rows.Count == 0)
            {
                msg = Request.CreateResponse(HttpStatusCode.NotFound, "Player Not Found");
            }
            else
            {
                sqlcon.Open();
                SqlCommand cmdDel = new SqlCommand("Delete from PLAYER where PLRNO=" + id, sqlcon);
                int r = cmdDel.ExecuteNonQuery();
                if (r > 0)
                {
                    msg = Request.CreateResponse(HttpStatusCode.OK, "Player Deleted");
                }
                sqlcon.Close();
            }
            return msg;
        }
    }

}