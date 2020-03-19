using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IWalletRepository : IRepositoryBase<Wallet>
    {
        public void CreateWallet(Wallet wallet);
        public Wallet GetWallet(int walletId);
        public List<Wallet> GetAllWallets();
    }
}
