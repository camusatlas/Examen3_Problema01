using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;
using Examen3_Problema01.Models;
using System.Configuration;

namespace Examen3_Problema01.Controllers
{
    public class ConsumidorController : Controller
    {
        // GET: Consumidor
        IEnumerable<Consumidor> consumidores()
        {
            List<Consumidor> lista = new List<Consumidor>();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Negocios"].ConnectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand("usp_listar_consumidor", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Consumidor
                {
                    idcont= dr.GetString(0),
                    nomcont = dr.GetString(1),
                    apecont = dr.GetString(2),
                    emailcont = dr.GetString(3),
                    idpais = dr.GetString(4),
                });
            }
            dr.Close();
            cn.Close();
            return lista;
        }

        IEnumerable<Pais> paises()
        {
            List<Pais> lista = new List<Pais>();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Negocios"].ConnectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand("usp_listar_paises", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Pais
                {
                    idpais = dr.GetString(0),
                    nompais = dr.GetString(1)
                });
            }
            dr.Close();
            cn.Close();
            return lista;
        }

        /*Insertar Consumidor*/
        string  AgregarConsumidor(Consumidor reg)
        {
            string mensaje = string.Empty;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Negocios"].ConnectionString);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_agrega_consumidor", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcont", reg.idcont);
                cmd.Parameters.AddWithValue("@nomcont", reg.nomcont);
                cmd.Parameters.AddWithValue("@apecont", reg.apecont);
                cmd.Parameters.AddWithValue("@emailcont", reg.emailcont);
                cmd.Parameters.AddWithValue("@idpais", reg.idpais);
                int i = cmd.ExecuteNonQuery();
                mensaje = $"Se creo {i} Nuevo Consumidor...";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally { cn.Close(); }
            return mensaje;
        }

        /*Actualizar Consumidor*/
        string ActualizarConsumidor(Consumidor reg)
        {
            string mensaje = string.Empty;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Negocios"].ConnectionString);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_actualizar_consumidor", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcont", reg.idcont);
                cmd.Parameters.AddWithValue("@nomcont", reg.nomcont);
                cmd.Parameters.AddWithValue("@apecont", reg.apecont);
                cmd.Parameters.AddWithValue("@emailcont", reg.emailcont);
                cmd.Parameters.AddWithValue("@idpais", reg.idpais);
                int i = cmd.ExecuteNonQuery();
                mensaje = $"Se actualizo {i} Cliente...";
            }
            catch (SqlException ex) 
            {
                mensaje = ex.Message;
            }
            finally { cn.Close(); }
            return mensaje;
        }
        
        /*Listar Consumidores*/
        public ActionResult Index()
        {
            return View(consumidores());
        }
        /*Agregar Consumidor*/
        public ActionResult Create()
        {
            ViewBag.paises = new SelectList(paises(), "idpais", "nompais");
            return View(new Consumidor());
        }
        [HttpPost] public ActionResult Create(Consumidor reg)
        {
            ViewBag.mensaje = AgregarConsumidor(reg);
            ViewBag.paises = new SelectList(paises(), "idpais", "nompais", reg.idpais);
            return View(new Consumidor());
        }

        /*Actualizar Consumidor*/
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) {id = string.Empty;}
            Consumidor reg = consumidores().FirstOrDefault(x => x.idcont == id);
            ViewBag.paises = new SelectList(paises(), "idpais", "nompais", reg.idpais);
            return View(reg);
        }
        [HttpPost]public ActionResult Edit(Consumidor reg)
        {
            ViewBag.mensaje = ActualizarConsumidor(reg);
            ViewBag.paises = new SelectList(paises(), "idpais", "nompais", reg.idpais);
            return View(new Consumidor());
        }
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) { id = string.Empty; }
            Consumidor reg = consumidores().FirstOrDefault(x => x.idcont == id);
            return View(reg);
        }
    }
}