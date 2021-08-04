using ApiForReact.Services.Intarfaces;
using System;

namespace ApiForReact.Services.Implementations
{
    public class PageGeneratorService : IPageGeneratorService
    {
        private int _totalPage;

        public PageGeneratorService()
        {
            _totalPage = (new Random()).Next(20, 70);
        }
        public int GetTotalPage() => _totalPage;
    }
}
