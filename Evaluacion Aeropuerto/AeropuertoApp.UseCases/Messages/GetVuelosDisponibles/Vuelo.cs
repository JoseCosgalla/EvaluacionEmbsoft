﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeropuertoApp.UseCases.Messages.GetVuelosDisponibles
{
    public class Vuelo
    {
        public int VueloId { get; set; }
        public string NumeroVuelo { get; set; }
        public string Aerolinea { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public DateTime FechaHoraLlegada { get; set; }
        public string AeropuertoOrigen { get; set; }
        public string AeropuertoDestino { get; set; }
        public TimeSpan DuracionViaje { get; set; }
        public decimal CostoEconomico { get; set; }
        public decimal CostoNormal { get; set; }
        public decimal CostoEjecutivo { get; set; }
        public int AsientosDisponibles { get; set; }
        // Lista de numeros de asiento ya reservados
        public int[] AsientosReservados { get; set; }
    }
}
