using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private VietAFoodContext _context = new VietAFoodContext();
        public IGenericRepository<Account> _accountRepository;
        public IGenericRepository<Category> _categoryRepository;
        public IGenericRepository<Coupon> _couponRepository;
        public IGenericRepository<CustomerInformation> _customerInformationRepository;
        public IGenericRepository<Order> _orderRepository;
        public IGenericRepository<OrderDetail> _orderDetailRepository;
        public IGenericRepository<Product> _productRepository;

        public UnitOfWork()
        {

        }

        public IGenericRepository<Account> AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new GenericRepository<Account>(_context);
                }
                return _accountRepository;
            }
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new GenericRepository<Category>(_context);
                }
                return _categoryRepository;
            }
        }

        public IGenericRepository<Coupon> CouponRepository
        {
            get
            {
                if (_couponRepository == null)
                {
                    _couponRepository = new GenericRepository<Coupon>(_context);
                }
                return _couponRepository;
            }
        }

        public IGenericRepository<CustomerInformation> CustomerInformationRepository
        {
            get
            {
                if (_customerInformationRepository == null)
                {
                    _customerInformationRepository = new GenericRepository<CustomerInformation>(_context);
                }
                return _customerInformationRepository;
            }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        public IGenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                if (_orderDetailRepository == null)
                {
                    _orderDetailRepository = new GenericRepository<OrderDetail>(_context);
                }
                return _orderDetailRepository;
            }
        }

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new GenericRepository<Product>(_context);
                }
                return _productRepository;
            }
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async void SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
