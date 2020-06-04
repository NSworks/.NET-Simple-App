using KolProbne.DTOs.Request;
using KolProbne.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolProbne.Services
{
    public interface ICukierniaDbService
    {
        public List<ZamowResponse> GetOrders(string nazwisko);
        public List<ZamowResponse> GetOrders();
        public ZamowResponse NewOrder(int id, ZamowRequest zam);
    }
}
