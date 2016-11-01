using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LASearch3
{
    public class SeedStlContext
    {
        private StlContext _context;
        public SeedStlContext(StlContext context)
        {
            _context = context;
        }

        string _authority = "Barrow,Gedling,Horsham,Lichfield,Milton Keynes,Northamptonshire,Redcar and Cleveland,Runnymede,Selby,South Cambridgeshire";
        List<string> authorityData => _authority.Split(',').ToList();

        public async Task EnsureTestDataSeedAsync()
        {
            //Seed Authority
            List<Authority> auth = new List<Authority>();
            for (int i = 0; i < authorityData.Count; i++)
            {
                auth.Add(new Authority() { Name = authorityData.ElementAt(i) });
            }

            _context.Authorities.AddRange(auth);

            //Seed SearchClerk
            List<SearchClerk> searchClerk = new List<SearchClerk>()
            {
                new SearchClerk() { Id=1, Email="andrew@stl.com", Name="Andrew Smith" },
                new SearchClerk() {Id=2, Email="alex@stl.com", Name="Alex Murphy" },
                new SearchClerk() {Id=3, Email="john@stl.com", Name="Phil larby" }
            };
            _context.SearchClerks.AddRange(searchClerk);

            await _context.SaveChangesAsync();
        }
    }
}
