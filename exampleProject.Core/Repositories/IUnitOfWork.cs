using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exampleProject.Core.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        // veritabanına toplu kaydetme işlemini tetikleyecek olan metot.
        Task<int> CommitAsync();

        //Async olmayan işlemler için kaydetme
        int Commit();

    }
}
