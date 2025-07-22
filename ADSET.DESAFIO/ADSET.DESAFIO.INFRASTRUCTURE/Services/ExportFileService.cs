using ADSET.DESAFIO.APPLICATION.Interfaces;
using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSET.DESAFIO.INFRASTRUCTURE.Services
{
    public class ExportFileService : IExportFileService
    {
        private readonly ICarRepository _carRepository;

        public ExportFileService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<byte[]> ExportToExcelAsync()
        {
            List<Car> cars = _carRepository.QueryAll().ToList();

            using XLWorkbook workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Cars");

            ws.Row(1).Cell(1).Value = "Id";
            ws.Row(1).Cell(2).Value = "Marca";
            ws.Row(1).Cell(3).Value = "Modelo";
            ws.Row(1).Cell(4).Value = "Ano";
            ws.Row(1).Cell(5).Value = "Placa";
            ws.Row(1).Cell(6).Value = "Km";
            ws.Row(1).Cell(7).Value = "Cor";
            ws.Row(1).Cell(8).Value = "Preço";

            for (int i = 0; i < cars.Count; i++)
            {
                Car? car = cars[i];
                int row = i + 2;

                ws.Row(row).Cell(1).Value = car.Id;
                ws.Row(row).Cell(2).Value = car.Brand;
                ws.Row(row).Cell(3).Value = car.Model;
                ws.Row(row).Cell(4).Value = car.Year;
                ws.Row(row).Cell(5).Value = car.Plate;
                ws.Row(row).Cell(6).Value = car.Km;
                ws.Row(row).Cell(7).Value = car.Color;
                ws.Row(row).Cell(8).Value = car.Price;
            }

            using MemoryStream ms = new MemoryStream();
            workbook.SaveAs(ms);
            return await Task.FromResult(ms.ToArray());
        }
    }
}