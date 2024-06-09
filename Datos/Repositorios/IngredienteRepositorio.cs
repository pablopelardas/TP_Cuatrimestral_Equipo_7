﻿using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class IngredienteRepositorio
    {
            public List<IngredienteModelo> Listar()
        {
            Entities db = new Entities();
            try
            {
                List<INGREDIENTES> ingredientes = db.INGREDIENTES.ToList();
                return Mappers.IngredienteMapper.EntidadesAModelos(ingredientes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IngredienteModelo ObtenerPorId(int id)
        {
            Entities db = new Entities();
            try
            {
                INGREDIENTES ingrediente = db.INGREDIENTES.FirstOrDefault(x => x.id_ingrediente == id);
                return Mappers.IngredienteMapper.EntidadAModelo(ingrediente);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Agregar(IngredienteModelo ingrediente)
        {
            Entities db = new Entities();
            try
            {
                INGREDIENTES entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                db.INGREDIENTES.Add(entidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Modificar(IngredienteModelo ingrediente)
        {
            Entities db = new Entities();
            try
            {
                INGREDIENTES entidad = Mappers.IngredienteMapper.ModeloAEntidad(ingrediente);
                db.Entry(entidad).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Eliminar(int id)
        {
            Entities db = new Entities();
            try
            {
                INGREDIENTES entidad = db.INGREDIENTES.FirstOrDefault(x => x.id_ingrediente == id);
                db.INGREDIENTES.Remove(entidad);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
