using BookloversApplication.Repositories;
using LicenseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Repositories
{
    public class TuristicObjectRepository : RepositoryBase<TuristicObject>, ITuristicObjectRepository
    {
        public TuristicObjectRepository(Context context) : base(context) { }
    }
}
