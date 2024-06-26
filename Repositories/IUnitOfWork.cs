﻿using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Admin> AdminRepository { get; }
        public IGenericRepository<Coupon> CouponRepository { get; }
        public IGenericRepository<CustomerInformation> CustomerInformationRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
		Task<int> CommitAsync();
		void Commit();
        Task<IDbContextTransaction> BeginTransactionAsync();
	}
}
