
using AdSetLead.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System;
using System.Web;
using AdSetLead.Core.Interfaces.IServices;

namespace AdSetLead.Api.Services
{
    public class FotosService : IFotosService
    {
        public List<Imagem> UploadFotos(HttpRequest httpRequest)
        {
            List<Imagem> imagens = new List<Imagem>();

            if (httpRequest.Files.Count == 0)
            {
                return new List<Imagem>();
            }

            string uploadDir = Path.Combine(HostingEnvironment.MapPath("~/Uploads"));
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            for (int i = 0; i < httpRequest.Files.Count; i++)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(httpRequest.Files[i].FileName);
                string filePath = Path.Combine(uploadDir, uniqueFileName);

                httpRequest.Files[i].SaveAs(filePath);

                imagens.Add(new Imagem()
                {
                    Url = uniqueFileName,
                    CarroId = i,
                });
            }

            return imagens;
        }
    }
}