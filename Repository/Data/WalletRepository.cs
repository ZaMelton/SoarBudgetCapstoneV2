using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.Data
{
    public class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
    {
        public WalletRepository(RepositoryDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public void CreateWallet(Wallet wallet)
        {
            Create(wallet);
        }

        public Wallet GetWallet(int? walletId)
        {
            return FindByCondition(w => w.WalletId == walletId).FirstOrDefault();
        }

        public List<Wallet> GetAllWallets()
        {
            return FindAll().ToList();
        }
    }
}
