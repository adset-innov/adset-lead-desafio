using Adsetdesafio.Domain.Models.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Adsetdesafio.Domain.Models.Extension
{
    public class ApplicationMapping
    {
        public static void AddMappings(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new CarMap());

        }
    }
}
