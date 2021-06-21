﻿using System.Collections.Generic;
using System.Linq;
using Domain;

namespace EShop.Domain.Catalog
{
    public interface IValidator<in T>
    {
        DomainResult Validate(T param);
    }

    public class CatalogItemValidator : IValidator<CatalogItemContext>
    {
        public DomainResult Validate(CatalogItemContext param)
        {
            var result = Validate(param.Name, param.Description, param.Price);

            return result;
        }

        private DomainResult Validate(string name, string description, decimal price)
        {
            List<DomainError> results = new List<DomainError>();

            if (name == null || name.Length > 60 || string.IsNullOrEmpty(name))
            {
                results.Add(new DomainError($"{nameof(name)} can not be null, empty or more than 60 chars"));
            }
            
            if (description == null || description.Length > 500 || string.IsNullOrEmpty(description))
            {
                results.Add(new DomainError($"{nameof(description)} can not be null, empty or more than 500 chars"));
            }
            
            if (price <= 0)
            {
                results.Add(new DomainError($"{nameof(price)} can not be less or equals 0"));
            }

            return !results.Any() ? DomainResult.Success() : DomainResult.Error(results);
        }
        
    }
}