﻿using IntegratorProject.src.data;
using IntegratorProject.src.dtos;
using IntegratorProject.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IntegratorProject.src.repositories.implements
{
    /// <summary>
    /// <para>Resume> Creating Kit Implements</para>
    /// <para>By: Leticia Zimerer, Michelli Oliveira and Paulo Almeida </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>
    public class KitRepository : IKit
    {
        #region Attributes
        private readonly IntegratorProjectContext _context;
        #endregion Attributes

        #region Constructors
        public KitRepository(IntegratorProjectContext context)
        {
            _context = context;
        }
        public void DeleteKit(int id)
        {
            _context.Kits.Remove(GetKitById(id));
            _context.SaveChanges();
        }
        #endregion Constructors

        #region Methods
        public KitModel GetKitById(int id)
        {
            return _context.Kits.FirstOrDefault(u => u.Id == id);
        }

        public void NewKit(NewKitDTO newkit)
        {
            _context.Kits.Add(new KitModel
            {
                Name = newkit.Name,
                ProductClass = newkit.ProductClass,
                Price = newkit.Price,
                ExpirationDate = newkit.ExpirationDate,
            });
            _context.SaveChanges();
        }

        public void UpDateKit(UpDateKitDTO upkit)
        {
            var KitModel = GetKitById(upkit.Id);
            KitModel.Name = upkit.Name;
            KitModel.ProductClass = upkit.ProductClass;
            KitModel.Price = upkit.Price;
            KitModel.ExpirationDate = upkit.ExpirationDate;

            _context.Kits.Update(KitModel);
            _context.SaveChanges();
        }

        public List<KitModel> GetAllBySearch(string namekit, string productClass, float price)
        {
            switch (namekit, productClass, price)
            {
                case (null, null, 0):
                    return _context.Kits
                        .ToList();

                case (null, null, _):
                    return _context.Kits
                        .Where(k => k.Price == price)
                        .ToList();

                case (null, _, _):
                    return _context.Kits
                        .Where(k => k.ProductClass.Contains(productClass) & k.Price == price)
                        .ToList();

                case (_, null, _):
                    return _context.Kits
                        .Where(k => k.Name.Contains(namekit) & k.Price == price)
                        .ToList();

                case (_, _, _):
                    return _context.Kits
                        .Where(k => k.Name.Contains(namekit) & k.ProductClass.Contains(productClass) & k.Price == price)
                        .ToList();
            }
        }

        public List<KitModel> GetAllKits()
        {
            return _context.Kits.ToList();
        }
        #endregion Methods
    }
}





